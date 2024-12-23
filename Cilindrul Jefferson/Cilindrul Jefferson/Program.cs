using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class JeffersonDiskCipher
{
    // Generare permutare aleatorie a literelor
    private static string GenerateRandomPermutation()
    {
        Random rand = new Random();
        List<char> alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToList();
        for (int i = 0; i < alphabet.Count; i++)
        {
            int j = rand.Next(i, alphabet.Count);
            char temp = alphabet[i];
            alphabet[i] = alphabet[j];
            alphabet[j] = temp;
        }
        return new string(alphabet.ToArray());
    }

    // Generare cilindru Jefferson cu n discuri
    private static List<string> GenerateCylinder(int n)
    {
        List<string> cylinder = new List<string>();
        for (int i = 0; i < n; i++)
        {
            cylinder.Add(GenerateRandomPermutation());
        }
        return cylinder;
    }

    // Functie pentru criptare
    public static string Encrypt(string plaintext, List<string> cylinder, List<int> key)
    {
        StringBuilder ciphertext = new StringBuilder();
        List<string> orderedDisks = key.Select(k => cylinder[k - 1]).ToList();

        foreach (char c in plaintext.ToUpper())
        {
            if (char.IsLetter(c))
            {
                int index = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".IndexOf(c);
                char encryptedChar = orderedDisks[0][index];
                ciphertext.Append(encryptedChar);
            }
            else
            {
                ciphertext.Append(c);
            }
        }
        return ciphertext.ToString();
    }

    // Functie pentru decriptare
    public static string Decrypt(string ciphertext, List<string> cylinder, List<int> key)
    {
        StringBuilder plaintext = new StringBuilder();
        List<string> orderedDisks = key.Select(k => cylinder[k - 1]).ToList();

        foreach (char c in ciphertext.ToUpper())
        {
            if (char.IsLetter(c))
            {
                int index = orderedDisks[0].IndexOf(c);
                char decryptedChar = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"[index];
                plaintext.Append(decryptedChar);
            }
            else
            {
                plaintext.Append(c);
            }
        }
        return plaintext.ToString();
    }

    static void Main()
    {

        Console.Write("Introduceti numarul de discuri (n): ");
        int n = int.Parse(Console.ReadLine());


        List<string> cylinder = GenerateCylinder(n);


        Console.WriteLine("\nPermutatiile discurilor:");
        for (int i = 0; i < n; i++)
        {
            Console.WriteLine($"Disc {i + 1}: {cylinder[i]}");
        }

        Console.Write("Introduceti cheia (permutare de la 1 la n, de exemplu: 3 1 2): ");
        List<int> key = Console.ReadLine()
                               .Split(' ')
                               .Select(int.Parse)
                               .ToList();

        if (key.Count != n || key.Distinct().Count() != n)
        {
            Console.WriteLine("Cheia nu este valida! Trebuie sa contina valori unice de la 1 la n.");
            return;
        }

        // Criptare/Decriptare
        while (true)
        {
            Console.Write("\nAlegeti operatia (1 - Criptare, 2 - Decriptare, 0 - Iesire): ");
            int choice = int.Parse(Console.ReadLine());

            if (choice == 0) break;

            Console.Write("Introduceti textul: ");
            string inputText = Console.ReadLine();

            switch (choice)
            {
                case 1:
                    string encryptedText = Encrypt(inputText, cylinder, key);
                    Console.WriteLine("Text criptat: " + encryptedText);
                    break;

                case 2:
                    string decryptedText = Decrypt(inputText, cylinder, key);
                    Console.WriteLine("Text decriptat: " + decryptedText);
                    break;

                default:
                    Console.WriteLine("Optiune invalida.");
                    break;
            }
        }
    }
}
