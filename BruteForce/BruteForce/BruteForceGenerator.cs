public class BruteForceGenerator
{
    public void GenerateCombinations(string prefix, int remainingLength, string charSet, CancellationToken token, Action<string> onGenerated)
    {
        if (token.IsCancellationRequested) return;

        if (remainingLength == 0)
        {
            onGenerated(prefix);
            return;
        }

        for (int i = 0; i < charSet.Length; i++)
        {
            if (token.IsCancellationRequested) return;
            GenerateCombinations(prefix + charSet[i], remainingLength - 1, charSet, token, onGenerated);
        }
    }
}