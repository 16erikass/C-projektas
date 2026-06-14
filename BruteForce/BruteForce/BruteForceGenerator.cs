namespace BruteForce
{
    public class BruteForceGenerator
    {
        public void GenerateCombinations(string prefix, int remainingLength, string charSet, CancellationToken token, Action<string> onGenerated)
        {
            //Patikrinama, ar kita gija jau rado slaptažodį. Jei taip - iškart baigiamas darbas
            if (token.IsCancellationRequested) return;

            //Jei nebėra ko pridėti (likęs ilgis = 0), vadinasi, kombinacija baigta
            if (remainingLength == 0)
            {
                onGenerated(prefix); //Atiduodamas sugeneruotas tekstas validatoriui tikrinti
                return;
            }

            //Einame per kiekvieną abėcėlės simbolį
            for (int i = 0; i < charSet.Length; i++)
            {
                //Vėl patikriname, ar nereikia sustoti
                if (token.IsCancellationRequested) return;

                //Kviečiame tą pačią funkciją iš naujo
                //Prie dabartinio prefikso pridedame naują raidę: prefix + charSet[i]
                //Likusį ilgį sumažiname vienetu: remainingLength - 1
                GenerateCombinations(prefix + charSet[i], remainingLength - 1, charSet, token, onGenerated);
            }
        }
    }
}