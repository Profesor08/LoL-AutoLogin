using System;
using System.Security.Cryptography;
using System.Text;

namespace LoL_AutoLogin
{

    class Encryption
    {

        public static string Encrypt(string plainTextData)
        {
            var csp = new RSACryptoServiceProvider();
            csp.FromXmlString(StoreKey.GetKey());
            var bytesPlainTextData = Encoding.Unicode.GetBytes(plainTextData);
            var bytesCypherText = csp.Encrypt(bytesPlainTextData, false);
            return Convert.ToBase64String(bytesCypherText);
        }

        public static string Decrypt(string cypherText)
        {
            var bytesCypherText = Convert.FromBase64String(cypherText);
            var csp = new RSACryptoServiceProvider();
            csp.FromXmlString(StoreKey.GetKey());
            var bytesPlainTextData = csp.Decrypt(bytesCypherText, false);
            return Encoding.Unicode.GetString(bytesPlainTextData);
        }

    }
}
