using System.Numerics;
using System.Text;

namespace Application.Helpers;

public static class CommonHelper
{
    public static bool isInRange(int num, int min, int max)
    {
        return (num >= min && num <= max);
    }

    public static string CrearPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password, 12);
    }

    public static bool VerificarPassword(string InPassword, string HashedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(InPassword, HashedPassword);
    }

    public static string ConvertirBase10aBase58(ulong nBase10)
    {
        const string CARACTERES_BASE58 = "123456789ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz";
        StringBuilder cResultado = new StringBuilder();
        while (nBase10 > 0)
        {
            ulong nResiduo = nBase10 % 58;
            cResultado.Insert(0, CARACTERES_BASE58[(int)nResiduo]);
            nBase10 /= 58;
        }
        return cResultado.ToString();
    }

    public static BigInteger ConvertirBase58aBase10(string nBase58)
    {
        const string CARACTERES_BASE58 = "123456789ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz";
        BigInteger cResultado = 0;
        for (int i = 0; i < nBase58.Length; i++)
        {
            char cCaracter = nBase58[i];
            int nValCaracter = CARACTERES_BASE58.IndexOf(cCaracter);
            cResultado = cResultado * 58 + nValCaracter;
        }
        return cResultado;
    }
}
