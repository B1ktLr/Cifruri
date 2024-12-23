using System;
using System.Text;
using System.Collections.Generic;

class VigenereCipher
{
    static string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    // Functie pentru criptare
    public static string Encrypt(string plaintext, string key)
    {
        StringBuilder ciphertext = new StringBuilder();
        int keyIndex = 0;

        foreach (char character in plaintext)
        {
            if (char.IsLetter(character))
            {
                bool isUpper = char.IsUpper(character);
                char normalizedChar = char.ToUpper(character);
                char normalizedKey = char.ToUpper(key[keyIndex % key.Length]);

                int shift = alphabet.IndexOf(normalizedKey);
                int charIndex = alphabet.IndexOf(normalizedChar);
                int encryptedIndex = (charIndex + shift) % alphabet.Length;

                char encryptedChar = alphabet[encryptedIndex];
                ciphertext.Append(isUpper ? encryptedChar : char.ToLower(encryptedChar));

                keyIndex++;
            }
            else
            {
                ciphertext.Append(character);
            }
        }

        return ciphertext.ToString();
    }

    // Functie pentru decriptare
    public static string Decrypt(string ciphertext, string key)
    {
        StringBuilder plaintext = new StringBuilder();
        int keyIndex = 0;

        foreach (char character in ciphertext)
        {
            if (char.IsLetter(character))
            {
                bool isUpper = char.IsUpper(character);
                char normalizedChar = char.ToUpper(character);
                char normalizedKey = char.ToUpper(key[keyIndex % key.Length]);

                int shift = alphabet.IndexOf(normalizedKey);
                int charIndex = alphabet.IndexOf(normalizedChar);
                int decryptedIndex = (charIndex - shift + alphabet.Length) % alphabet.Length;

                char decryptedChar = alphabet[decryptedIndex];
                plaintext.Append(isUpper ? decryptedChar : char.ToLower(decryptedChar));

                keyIndex++;
            }
            else
            {
                plaintext.Append(character);
            }
        }

        return plaintext.ToString();
    }

    // Functie pentru criptanaliza (fara a sti cheia)
    public static void Cryptanalysis(string ciphertext)
    {
        Console.WriteLine("Criptanaliza necesita analiza frecventei si potriviri manuale.");
        Console.WriteLine("Analizati textul si cautati repetitii pentru a deduce lungimea cheii.");
        Console.WriteLine("Apoi folositi analiza frecventei literelor pentru a deduce literele cheii.");
    }

    static void Main()
    {
        while (true)
        {
            Console.Write("Introduceti textul (sau 0 pentru a iesi): ");
            string input = Console.ReadLine();

            if (input == "0")
                break;

            Console.Write("Introduceti cheia: ");
            string key = Console.ReadLine();

            Console.Write("Alegeti operatia (1 - Criptare, 2 - Decriptare, 3 - Criptanaliza): ");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    string encryptedText = Encrypt(input, key);
                    Console.WriteLine("Text criptat: " + encryptedText);
                    break;

                case 2:
                    string decryptedText = Decrypt(input, key);
                    Console.WriteLine("Text decriptat: " + decryptedText);
                    break;

                case 3:
                    Cryptanalysis(input);
                    break;

                default:
                    Console.WriteLine("Optiune invalida.");
                    break;
            }
        }
    }
}
