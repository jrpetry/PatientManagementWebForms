using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace PatientManager.Utils
{
    /// <summary>
    /// Researched on https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.aes?view=net-8.0#code-try-2
    /// </summary>
    public static class Encryption
    {

        static byte[] Key = Encoding.ASCII.GetBytes(System.Configuration.ConfigurationManager.AppSettings["EncryptionKey"]);
        static byte[] IV = Encoding.ASCII.GetBytes(System.Configuration.ConfigurationManager.AppSettings["EncryptionVector"]);

        public static string Encrypt(string original)
        {
            string plainText = "";
            using (Aes myAes = Aes.Create())
            {
                byte[] encryptedBytes = EncryptStringToBytes_Aes(original);
                plainText = Convert.ToBase64String(encryptedBytes);
            }
            return plainText;
        }

        public static string Decrypt(string encrypted)
        {
            string plainText = "";
            using (Aes myAes = Aes.Create())
            {
                byte[] encryptedBytes = Convert.FromBase64String(encrypted);
                plainText = DecryptStringFromBytes_Aes(encryptedBytes);
            }
            return plainText;
        }


        static byte[] EncryptStringToBytes_Aes(string plainText)
        {
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            byte[] encrypted;

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            return encrypted;
        }

        static string DecryptStringFromBytes_Aes(byte[] cipherText)
        {
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            string plaintext = null;

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
            return plaintext;
        }
    }
}