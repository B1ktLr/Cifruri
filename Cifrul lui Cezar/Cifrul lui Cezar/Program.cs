using System;
using System.Text;

class CaesarCipher
{
    // Functie pentru criptare
    public static string Encrypt(string text, int schimbare)
    {
        StringBuilder result = new StringBuilder();

        foreach (char character in text)
        {
            if (char.IsLetter(character))
            {
                char offset = char.IsUpper(character) ? 'A' : 'a';
                char encryptedChar = (char)(((character + schimbare - offset) % 26) + offset);
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
    public static string Decrypt(string text, int shift)
    {
        return Encrypt(text, 26 - shift);
    }

    // Functie pentru criptanaliza prin forta bruta
    public static void Cryptanalysis(string text)
    {
        Console.WriteLine("Criptanaliza: ");
        for (int shift = 1; shift <= 3; shift++)
        {
            string decryptedText = Decrypt(text, shift);
            Console.WriteLine($"Schimbare {shift}: {decryptedText}");
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
                    string encryptedText = Encrypt(input, 3);
                    Console.WriteLine("Text criptat: " + encryptedText);
                    break;

                case 2:
                    string decryptedText = Decrypt(input, 3);
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
