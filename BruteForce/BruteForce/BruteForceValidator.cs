public class BruteForceValidator
{
    private readonly string _targetHash;

    public BruteForceValidator(string targetHash)
    {
        _targetHash = targetHash;
    }

    public bool IsValid(string candidatePassword)
    {
        string candidateHash = CryptoUtils.ComputeSha256Hash(candidatePassword);
        return candidateHash == _targetHash;
    }
}