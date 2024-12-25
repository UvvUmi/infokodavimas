using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Encode
    {
        public string encrypt_aes(string input, string password)
        {
            using (Aes aesAlg = Aes.Create())
            {
                
                byte[] key = Encoding.ASCII.GetBytes(password.PadRight(32)); 
                aesAlg.Key = key;
                aesAlg.IV = new byte[16]; 

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, aesAlg.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(input);
                        }
                    }

                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }

        public string encrypt_3des(string input, string password)
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

                
                byte[] inputBytes = System.Text.Encoding.UTF8.GetBytes(input);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, tripleDes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        csEncrypt.Write(inputBytes, 0, inputBytes.Length);
                        csEncrypt.FlushFinalBlock();
                    }

                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }

    }
}
