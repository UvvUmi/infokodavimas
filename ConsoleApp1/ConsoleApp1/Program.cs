using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int cryptChoice, decryptChoice;
            string encrypted;
            Console.WriteLine("1 - Sifravimas | 2 - Issifravimas");
            string genChoice = Console.ReadLine();

            if (genChoice != "2")
            {
                Console.WriteLine("Iveskite teksta sifravimui: ");
                string input = Console.ReadLine();
                File.WriteAllText("originalas.txt", input);
                Console.WriteLine("Iveskite slaptazodi: ");
                string key = Console.ReadLine();
                Console.WriteLine("1 - AES || 2 - 3DES");

                while (true) {
                    try
                    {
                        cryptChoice = Int32.Parse(Console.ReadLine());
                        if (cryptChoice == 1 || cryptChoice == 2) { break; }
                        else { Console.WriteLine("Neteisingas pasirinkimas!"); }
                    }
                    catch { Console.WriteLine("Tik skaiciai!"); }
                }

                Encode encoder = new Encode();

                switch (cryptChoice)
                {
                    case 1:
                        encrypted = encoder.encrypt_aes(input, key);
                        File.WriteAllText("uzkoduota.txt.aes", encrypted);
                        break;
                    case 2:
                        encrypted = encoder.encrypt_3des(input, key);
                        File.WriteAllText("uzkoduota.txt.3des", encrypted);
                        break;
                }
                Console.ReadLine();
            }

            else
            {
                Console.WriteLine("Issifravimas 1 - AES || 2 - 3DES");
                while (true)
                {
                    try
                    {
                        decryptChoice = Int32.Parse(Console.ReadLine());
                        if (decryptChoice == 1 || decryptChoice == 2) { break; }
                        else { Console.WriteLine("Neteisingas pasirinkimas!"); }
                    }
                    catch { Console.WriteLine("Tik skaiciai!"); }
                }

                Decode decoder = new Decode();

                switch (decryptChoice)
                {
                    case 1:
                        try
                        {
                            string encryptedInput = File.ReadAllText("uzkoduota.txt.aes");
                            Console.WriteLine("Iveskite slaptazodi");
                            string pass = Console.ReadLine();
                            string decodedInput = decoder.decrypt_aes(encryptedInput, pass);
                            File.WriteAllText("dekoduotaAES.txt", decodedInput);
                        }
                        catch { Console.WriteLine("Ivyko klaida: Failas nerastas arba neteisingas slaptazodis!"); }
                        break;
                    case 2:
                        try
                        {
                            string encryptedInput = File.ReadAllText("uzkoduota.txt.3des");
                            Console.WriteLine("Iveskite slaptazodi");
                            string pass = Console.ReadLine();
                            string decodedInput = decoder.decrypt_3des(encryptedInput, pass);
                            File.WriteAllText("dekoduota3DES.txt", decodedInput);
                        }
                        catch { Console.WriteLine("Ivyko klaida: Failas nerastas arba neteisingas slaptazodis!"); }
                        break;
                }
                Console.ReadLine();

            }

        }
    }
}

