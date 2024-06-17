
using Microsoft.Extensions.Configuration;

namespace Domain.Commons;

public static class ConnectionCommon
{
    private static CryptoCommon strDecrypt = new CryptoCommon();

    public static string CreateSqlConnection(string cadena)
    {
        try
        {
            cadena = cadena.Replace(" ", "").Trim();
            string stringCrypted = "";
            string lastString = ";";
            int firstPosition = 0;
            int lastPosition = 0;
            List<string> Connections = new List<string>
                {
                    "User=",
                    "Password="
                };
            foreach (var item in Connections)
            {
                stringCrypted = cadena;
                firstPosition = cadena.IndexOf(item) + item.Length;
                stringCrypted = stringCrypted.Remove(0, firstPosition);
                lastPosition = firstPosition + stringCrypted.IndexOf(lastString);
                stringCrypted = cadena.Substring(firstPosition, lastPosition - firstPosition);
                cadena = cadena.Replace(item + stringCrypted, item + strDecrypt.decryptString(stringCrypted));
            }
            return cadena;
        }
        catch
        {
            return "";
        }
    }

    public static IConfigurationSection CreateMongoConnection(IConfigurationSection Configuration)
    {
        try
        {
            string cadena = Configuration["ConnectionString"];
            cadena = cadena.Replace(" ", "").Trim().Replace("mongodb:", "");
            cadena = ReplaceStringBeetBeen(cadena, "//", ":");
            cadena = ReplaceStringBeetBeen(cadena, ":", "@");
            Configuration["ConnectionString"] = "mongodb:" + cadena;
            return Configuration;
        }
        catch
        {
            return Configuration;
        }
    }

    public static IConfigurationSection CreateMongoConnection(IConfigurationSection Configuration, string? mongoDbServer, string? mongoDbName, string? mongoDbUser, string? mongoDbPassword)
    {
        Configuration["ConnectionString"] = $"mongodb://{strDecrypt.decryptString(mongoDbUser ?? "")}:{strDecrypt.decryptString(mongoDbPassword ?? "")}@{mongoDbServer ?? ""}";
        Configuration["DatabaseName"] = mongoDbName ?? "";
        return Configuration;
    }

    private static string ReplaceStringBeetBeen(string cadena, string item, string lastString)
    {
        int firstPosition = 0;
        int lastPosition = 0;
        string stringCrypted = "";

        stringCrypted = cadena;
        firstPosition = cadena.IndexOf(item) + item.Length;
        stringCrypted = stringCrypted.Remove(0, firstPosition);
        lastPosition = firstPosition + stringCrypted.IndexOf(lastString);
        stringCrypted = cadena.Substring(firstPosition, lastPosition - firstPosition);
        cadena = cadena.Replace(item + stringCrypted, item + strDecrypt.decryptString(stringCrypted));

        return cadena;
    }
}
