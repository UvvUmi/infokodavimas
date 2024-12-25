using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Decode
    {
        public string decrypt_aes(string encryptedInput, string password)
        {
            using (Aes aesAlg = Aes.Create())
            {
                
                byte[] key = Encoding.ASCII.GetBytes(password.PadRight(32)); 
                aesAlg.Key = key;
                aesAlg.IV = new byte[16]; 

                byte[] cipherText = Convert.FromBase64String(encryptedInput);

                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, aesAlg.CreateDecryptor(), CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
        }

        public string decrypt_3des(string encryptedInput, string password)
        {
            using (TripleDES tripleDes = TripleDES.Create())
            {
                
                using (SHA256 sha256 = SHA256.Create())
                {
                    byte[] passwordBytes = System.Text.Encoding.UTF8.GetBytes(password);
                    byte[] hashedPassword = sha256.ComputeHash(passwordBytes);
                    byte[] key = new byte[24];
                    Array.Copy(hashedPassword, key, 24); 
                    tripleDes.Key = key;
                }

                tripleDes.IV = new byte[8];

                using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(encryptedInput)))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, tripleDes.CreateDecryptor(), CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
        }
    }
}
