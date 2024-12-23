using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;

class SubstitutionCipher
{
    // Functie pentru criptare folosind o cheie personalizata
    public static string Encrypt(string text, Dictionary<char, char> key)
    {
        StringBuilder result = new StringBuilder();

        foreach (char character in text)
        {
            if (char.IsLetter(character))
            {
                char encryptedChar = char.IsUpper(character) ? key[character] : char.ToLower(key[char.ToUpper(character)]);
                result.Append(encryptedChar);
            }
            else
            {
                result.Append(character);
            }
        }

        return result.ToString();
    }

    // Functie pentru decriptare folosind o cheie inversata
    public static string Decrypt(string text, Dictionary<char, char> key)
    {
        // Construim cheia inversata
        Dictionary<char, char> reverseKey = new Dictionary<char, char>();
        foreach (var pair in key)
        {
            reverseKey[pair.Value] = pair.Key;
        }

        return Encrypt(text, reverseKey);
    }

    // Functie pentru criptanaliza bazata pe frecventa literelor
    public static void Cryptanalysis(string text)
    {
        Console.WriteLine("Criptanaliza bazata pe frecventa literelor:");

        // Frecvente in limba engleza
        string frequencyOrder = "ETAOINSHRDLCUMWFGYPBVKJXQZ";

        // Calculam frecventa literelor in textul criptat
        Dictionary<char, int> freq = new Dictionary<char, int>();
        foreach (char c in text.ToUpper())
        {
            if (char.IsLetter(c))
            {
                if (!freq.ContainsKey(c))
                    freq[c] = 0;
                freq[c]++;
            }
        }

        // Sortam literele dupa frecventa descrescatoare
        var sortedFreq = freq.OrderByDescending(x => x.Value).Select(x => x.Key).ToList();

        // Construim o cheie estimata
        Dictionary<char, char> estimatedKey = new Dictionary<char, char>();
        for (int i = 0; i < sortedFreq.Count && i < frequencyOrder.Length; i++)
        {
            estimatedKey[sortedFreq[i]] = frequencyOrder[i];
        }

        // Decriptam textul folosind cheia estimata
        StringBuilder result = new StringBuilder();
        foreach (char character in text)
        {
            if (char.IsLetter(character))
            {
                char decryptedChar = char.IsUpper(character)
                    ? estimatedKey[character]
                    : char.ToLower(estimatedKey[char.ToUpper(character)]);
                result.Append(decryptedChar);
            }
            else
            {
                result.Append(character);
            }
        }

        Console.WriteLine("Text decriptat aproximativ: " + result.ToString());
    }

    // Functia principala
    static void Main()
    {
        Dictionary<char, char> key = new Dictionary<char, char>
        {
            {'A', 'M'}, {'B', 'I'}, {'C', 'P'}, {'D', 'O'}, {'E', 'D'},
            {'F', 'S'}, {'G', 'Q'}, {'H', 'R'}, {'I', 'N'}, {'J', 'T'},
            {'K', 'U'}, {'L', 'V'}, {'M', 'W'}, {'N', 'X'}, {'O', 'Y'},
            {'P', 'Z'}, {'Q', 'A'}, {'R', 'B'}, {'S', 'C'}, {'T', 'E'},
            {'U', 'F'}, {'V', 'G'}, {'W', 'H'}, {'X', 'J'}, {'Y', 'K'},
            {'Z', 'L'}
        };

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
