using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Domain.Commons;

public class CryptoCommon
{
    private const string initVector = "pemgail9uzpgzl88";
    private const int keysize = 256;
    private string passPhrase;

    public CryptoCommon(string pass = "AAED4554C235927C03BAD5BC313E81B0FBEB6FEC5BB60E682C70A7D5C8890B20")
    {
        this.passPhrase = pass;
    }

    public string encryptString(string plainText, bool urlFriendly = false)
    {
        byte[] initVectorBytes = Encoding.UTF8.GetBytes(initVector);
        byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText + "                                                          ");
        PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, null);
        byte[] keyBytes = password.GetBytes(keysize / 8);
        using var symmetricKey = Aes.Create("AesManaged");
        //RijndaelManaged symmetricKey = new RijndaelManaged();
        symmetricKey.Mode = CipherMode.CBC;
        ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);
        MemoryStream memoryStream = new MemoryStream();
        CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
        cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
        cryptoStream.FlushFinalBlock();
        byte[] cipherTextBytes = memoryStream.ToArray();
        memoryStream.Close();
        cryptoStream.Close();
        if (urlFriendly)
        {
            return HttpUtility.UrlEncode(Convert.ToBase64String(cipherTextBytes));
        }
        return Convert.ToBase64String(cipherTextBytes);
    }

    public string decryptString(string cipherText)
    {
        byte[] initVectorBytes = Encoding.UTF8.GetBytes(initVector);
        try
        {
            byte[] cipherTextBytes = Convert.FromBase64String(cipherText);
            PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, null);
            byte[] keyBytes = password.GetBytes(keysize / 8);
            using var symmetricKey = Aes.Create("AesManaged");
            //RijndaelManaged symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;
            ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
            MemoryStream memoryStream = new MemoryStream(cipherTextBytes);
            CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            byte[] plainTextBytes = new byte[cipherTextBytes.Length];
            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
            memoryStream.Close();
            cryptoStream.Close();
            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount).Trim();
        }
        catch
        {
            return "";
        }
    }

    public string ComputeSha256Hash(string cCadena)
    {
        using (SHA256 sha256Hash = SHA256.Create())
        {

            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(cCadena));

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString().ToUpper();
        }
    }
}
