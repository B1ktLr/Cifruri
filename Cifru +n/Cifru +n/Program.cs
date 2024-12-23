using System;
using System.Text;

class CaesarCipher
{
    // Functie pentru criptare
    public static string Encrypt(string text, int n)
    {
        StringBuilder result = new StringBuilder();

        foreach (char character in text)
        {
            if (char.IsLetter(character))
            {
                char offset = char.IsUpper(character) ? 'A' : 'a';
                char encryptedChar = (char)(((character + n - offset) % 26) + offset);
                result.Append(encryptedChar);
            }
            else
            {
                result.Append(character);
            }
        }

        return result.ToString();
    }

    // Functie pentru decriptare
    public static string Decrypt(string text, int n)
    {
        return Encrypt(text, 26 - n);
    }

    // Functie pentru criptanaliza prin forta bruta
    public static void Cryptanalysis(string text)
    {
        Console.WriteLine("Criptanaliza: ");
        for (int n = 1; n <= 25; n++)
        {
            string decryptedText = Decrypt(text, n);
            Console.WriteLine($"Cheie {n}: {decryptedText}");
        }
    }

    // Functia principala
    static void Main()
    {
        while (true)
        {
            Console.Write("Introduceti textul (sau 0 pentru a iesi): ");
            string input = Console.ReadLine();

            if (input == "0")
                break;

            Console.Write("Alegeti operatia (1 - Criptare, 2 - Decriptare, 3 - Criptanaliza): ");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Console.Write("Introduceti cheia (n): ");
                    int encryptKey = int.Parse(Console.ReadLine());
                    string encryptedText = Encrypt(input, encryptKey);
                    Console.WriteLine("Text criptat: " + encryptedText);
                    break;

                case 2:
                    Console.Write("Introduceti cheia (n): ");
                    int decryptKey = int.Parse(Console.ReadLine());
                    string decryptedText = Decrypt(input, decryptKey);
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