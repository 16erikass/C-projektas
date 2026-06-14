using System.Security.Cryptography;
using System.Text;

public static class CryptoUtils
{
    //Statinė druska - slaptas tekstas, kuris bus pridedamas prie kiekvieno slaptažodžio
    //Tai reiškia, nors slaptažodis bus 123, sistema hash'uos "123MySuperSecretStaticSalt2026!"
    private const string StaticSalt = "MySuperSecretStaticSalt2026!";

    public static string ComputeSha256Hash(string rawData)
    {
        //'using' užtikrina, kad SHA256 objekto užimta atmintis bus iškart išlaisvinta, kai baigsis darbas
        using (SHA256 sha256Hash = SHA256.Create())
        {
            //Apjungiamas slaptažodis su druska (rawData + StaticSalt)
            //Paverčiame šį tekstą į baitų masyvą
            //Sušifruojame hash kodą - gauname 32 baitų masyvą
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData + StaticSalt));

            //Šešioliktainio (Hex) teksto kūrimas iš baitų
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                //Kiekvieną baitą paverčiame į 2 simbolių šešioliktainę raidę/skaičių
                //"x2" formatas užtikrina, kad mažosios raidės bus naudojamos, o vienženkliai skaičiai gaus nulį priekyje
                builder.Append(bytes[i].ToString("x2"));
            }

            //Grąžinama pilną 64 simbolių tekstinę eilutė
            return builder.ToString();
        }
    }
}