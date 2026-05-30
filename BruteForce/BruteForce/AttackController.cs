using System.Diagnostics;
namespace BruteForce
{
    public class AttackController
    {
        private const string CharSet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private CancellationTokenSource? _cancellationTokenSource;

        public Action<string>? OnPasswordFound { get; set; }
        public Action<TimeSpan>? OnFinished { get; set; }

        public async Task StartAttackAsync(string targetHash, bool useMultiThreading)
        {
            _cancellationTokenSource = new CancellationTokenSource();
            Stopwatch stopwatch = Stopwatch.StartNew();

            int maxProcessors = Environment.ProcessorCount;
            int threadsToUse = useMultiThreading ? Math.Max(1, maxProcessors - 1) : 1;

            List<Task> tasks = new List<Task>();
            int chunkSize = (int)Math.Ceiling((double)CharSet.Length / threadsToUse);

            for (int i = 0; i < threadsToUse; i++)
            {
                int startIdx = i * chunkSize;
                int endIdx = Math.Min(startIdx + chunkSize, CharSet.Length);
                string threadCharSet = CharSet.Substring(startIdx, endIdx - startIdx);

                tasks.Add(Task.Run(() => WorkerThread(threadCharSet, targetHash, _cancellationTokenSource.Token)));
            }

            try
            {
                await Task.WhenAll(tasks);
            }
            catch (OperationCanceledException) { }

            stopwatch.Stop();
            OnFinished?.Invoke(stopwatch.Elapsed);
        }

        public void StopAttack()
        {
            _cancellationTokenSource?.Cancel();
        }

        private void WorkerThread(string startingChars, string targetHash, CancellationToken token)
        {
            BruteForceGenerator generator = new BruteForceGenerator();
            BruteForceValidator validator = new BruteForceValidator(targetHash);

            for (int length = 1; length <= 6; length++)
            {
                foreach (char startChar in startingChars)
                {
                    if (token.IsCancellationRequested) return;

                    generator.GenerateCombinations(startChar.ToString(), length - 1, CharSet, token, (candidate) =>
                    {
                        if (validator.IsValid(candidate))
                        {
                            OnPasswordFound?.Invoke(candidate);
                            _cancellationTokenSource?.Cancel();
                        }
                    });
                }
            }
        }
    }
}