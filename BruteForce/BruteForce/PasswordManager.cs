using System.Text;

public class PasswordManager
{
    //Naudojamas tas pats 62 simbolių rinkinys, kad sugeneruotas slaptažodis sutaptų su atakos valdiklio abėcėle
    private const string CharSet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

    //Pseudoatstiktinių skaičių generatorius simbolių parinkimui ir ilgio nustatymui
    private readonly Random _random = new Random();
    public string GenerateTargetHash(out string actualPassword)
    {
        //Nustatomas atsitiktinio slaptažodžio ilgis
        //Bus sugeneruotas 4 arba 5 simbolių ilgis.
        int length = _random.Next(4, 6);

        StringBuilder password = new StringBuilder();

        //Cikle atsitiktinai renkame simbolius iš CharSet abėcėlės
        for (int i = 0; i < length; i++)
        {
            //_random.Next(CharSet.Length) grąžina skaičių nuo 0 iki 61 (CharSet indeksą)
            password.Append(CharSet[_random.Next(CharSet.Length)]);
        }

        //Įrašome sugeneruotą tekstą į 'out' kintamąjį
        actualPassword = password.ToString();

        //Sšifruojame šio slaptažodžio hash'ą su druska (naudojant CryptoUtils) ir jį grąžiname
        return CryptoUtils.ComputeSha256Hash(actualPassword);
    }
}