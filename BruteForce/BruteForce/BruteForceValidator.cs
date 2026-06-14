namespace BruteForce
{
    public class BruteForceValidator
{
    //Kintamasis, kuriame saugomas ieškomas slaptažodžio hash'as
    private readonly string _targetHash;

    public BruteForceValidator(string targetHash)
    {
        _targetHash = targetHash;
    }

    public bool IsValid(string candidatePassword)
    {
        //Spėjamą slaptažodį paverčiame į SHA-256 maišos kodą (hash)
        string candidateHash = CryptoUtils.ComputeSha256Hash(candidatePassword);

        //Palyginame gautą hash'ą su ieškomu hash'u
        //Jei jie sutampa, vadinasi, slaptažodis buvo atspėtas sėkmingai
        return candidateHash == _targetHash;
    }
}
}