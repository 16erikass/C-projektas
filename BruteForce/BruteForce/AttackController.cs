using System.Diagnostics;

namespace BruteForce
{
    public class AttackController
    {
        //Visų galimų simbolių sąrašas, iš kurių generuojami slaptažodžiai
        private const string CharSet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        //Įrankis, leidžiantis vienu metu sustabdyti visas dirbančias gijas
        private CancellationTokenSource? _cancellationTokenSource;

        //Įvykiai, kuriais valdiklis praneša išorei (pvz vartotojo sąsajai) apie progresą
        public Action<string>? OnPasswordFound { get; set; } //Iškviečiamas radus slaptažodį
        public Action<TimeSpan>? OnFinished { get; set; }     //Iškviečiamas baigus darbą

        public async Task StartAttackAsync(string targetHash, bool useMultiThreading)
        {
            _cancellationTokenSource = new CancellationTokenSource();
            Stopwatch stopwatch = Stopwatch.StartNew(); //Pradeda matuoti atakos laiką

            //Nustatoma, kiek gijų bus naudojama 
            //Jei įjungtas MultiThreading, paima visus branduolius atėmus vieną (kad kompiuteris visiškai neužlužtu)
            int maxProcessors = Environment.ProcessorCount;
            int threadsToUse = useMultiThreading ? Math.Max(1, maxProcessors - 1) : 1;

            List<Task> tasks = new List<Task>();

            //Padalina visą simbolių rinkinį į vienodus „gabalus“ (chunks) pagal gijų skaičių
            int chunkSize = (int)Math.Ceiling((double)CharSet.Length / threadsToUse);

            for (int i = 0; i < threadsToUse; i++)
            {
                int startIdx = i * chunkSize;
                int endIdx = Math.Min(startIdx + chunkSize, CharSet.Length);

                //Kiekviena gija gaus tik tam tikrą abėcėlės dalį (pvz., pirma gija tikrina žodžius iš 'a-m', antra 'n-z' ir t.t.)
                string threadCharSet = CharSet.Substring(startIdx, endIdx - startIdx);

                //Paleidžia darbinį metodą fone (atskiroje gijoje)
                tasks.Add(Task.Run(() => WorkerThread(threadCharSet, targetHash, _cancellationTokenSource.Token)));
            }

            try
            {
                //laukiama, kol visos gijos baigs darbą
                await Task.WhenAll(tasks);
            }
            catch (OperationCanceledException) { } //Ši klaida iškrenta, kai atšaukiama užduotis (radus slaptažodį)

            stopwatch.Stop(); //Stabdomas laiko matavimas
            OnFinished?.Invoke(stopwatch.Elapsed); //Pranešama sistemai, kiek laiko užtrukom
        }

        public void StopAttack()
        {
            _cancellationTokenSource?.Cancel();
        }

        private void WorkerThread(string startingChars, string targetHash, CancellationToken token)
        {
            //Sukuriami vietiniai objektai gijos viduje, kad jie „nesipjautų“ tarpusavyje
            BruteForceGenerator generator = new BruteForceGenerator();
            BruteForceValidator validator = new BruteForceValidator(targetHash);

            //Tikrinamas slaptažodžiu didėjančio ilgio tvarka: nuo 1 iki 6 simbolių
            for (int length = 1; length <= 6; length++)
            {
                //Einama per visus šiai gijai priskirtus pradinius simbolius
                foreach (char startChar in startingChars)
                {
                    //Kas kartą patikrinama, ar niekas neliepė sustabdyti atakos
                    if (token.IsCancellationRequested) return;

                    //Generuojamos visos likusios kombinacijas tam tikram ilgiui
                    generator.GenerateCombinations(startChar.ToString(), length - 1, CharSet, token, (candidate) =>
                    {
                       //Ši funkcija iškviečiama kiekvienam sugeneruotam tekstui (kandidatui)
                        if (validator.IsValid(candidate)) //Jei kandidato hash sutampa su ieškomu
                        {
                            OnPasswordFound?.Invoke(candidate); //Pranešama sėkmę į išorę
                            _cancellationTokenSource?.Cancel(); //Duodame komandą visoms kitoms gijoms iškart sustoti
                        }
                    });
                }
            }
        }
    }
}