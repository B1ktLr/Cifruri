using System;
using System.Collections.Generic;
using System.Text;

class PlayfairCipher
{
    static string alphabet = "ABCDEFGHIKLMNOPQRSTUVWXYZ"; // 'J' este combinată cu 'I'

    // Crearea matricei 5x5
    static char[,] GenerateMatrix(string key)
    {
        key = key.ToUpper().Replace("J", "I"); // J devine I pentru Playfair
        StringBuilder matrix = new StringBuilder(key);

        foreach (char c in alphabet)
        {
            if (!matrix.ToString().Contains(c.ToString()))
            {
                matrix.Append(c);
            }
        }

        char[,] matrixArray = new char[5, 5];
        int index = 0;
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                matrixArray[i, j] = matrix[index++];
            }
        }

        return matrixArray;
    }

    // Funcție pentru găsirea poziției unui caracter în matricea Playfair
    static (int, int) FindPosition(char[,] matrix, char c)
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (matrix[i, j] == c)
                {
                    return (i, j);
                }
            }
        }
        return (-1, -1);
    }

    // Funcție pentru criptare
    public static string Encrypt(string plaintext, string key)
    {
        plaintext = plaintext.ToUpper().Replace("J", "I").Replace(" ", "");

        // Adăugăm un caracter suplimentar dacă este nevoie (ex: "X") pentru a face numărul de caractere par
        if (plaintext.Length % 2 != 0)
        {
            plaintext += "X";
        }

        char[,] matrix = GenerateMatrix(key);
        StringBuilder ciphertext = new StringBuilder();

        for (int i = 0; i < plaintext.Length; i += 2)
        {
            char firstChar = plaintext[i];
            char secondChar = plaintext[i + 1];

            int row1, col1, row2, col2;
            (row1, col1) = FindPosition(matrix, firstChar);
            (row2, col2) = FindPosition(matrix, secondChar);

            if (row1 == row2)
            {
                // Dacă caracterele sunt pe aceeași linie, le mutăm la dreapta
                col1 = (col1 + 1) % 5;
                col2 = (col2 + 1) % 5;
            }
            else if (col1 == col2)
            {
                // Dacă caracterele sunt pe aceeași coloană, le mutăm în jos
                row1 = (row1 + 1) % 5;
                row2 = (row2 + 1) % 5;
            }
            else
            {
                // Dacă sunt pe coloană și linie diferite, schimbăm coloană și linie
                int temp = col1;
                col1 = col2;
                col2 = temp;
            }

            ciphertext.Append(matrix[row1, col1]);
            ciphertext.Append(matrix[row2, col2]);
        }

        return ciphertext.ToString();
    }

    // Funcție pentru decriptare
    public static string Decrypt(string ciphertext, string key)
    {
        ciphertext = ciphertext.ToUpper().Replace(" ", "");
        char[,] matrix = GenerateMatrix(key);
        StringBuilder plaintext = new StringBuilder();

        for (int i = 0; i < ciphertext.Length; i += 2)
        {
            char firstChar = ciphertext[i];
            char secondChar = ciphertext[i + 1];

            int row1, col1, row2, col2;
            (row1, col1) = FindPosition(matrix, firstChar);
            (row2, col2) = FindPosition(matrix, secondChar);

            if (row1 == row2)
            {
                // Dacă caracterele sunt pe aceeași linie, le mutăm la stânga
                col1 = (col1 + 4) % 5;
                col2 = (col2 + 4) % 5;
            }
            else if (col1 == col2)
            {
                // Dacă caracterele sunt pe aceeași coloană, le mutăm în sus
                row1 = (row1 + 4) % 5;
                row2 = (row2 + 4) % 5;
            }
            else
            {
                // Dacă sunt pe coloană și linie diferite, schimbăm coloană și linie
                int temp = col1;
                col1 = col2;
                col2 = temp;
            }

            plaintext.Append(matrix[row1, col1]);
            plaintext.Append(matrix[row2, col2]);
        }

        return plaintext.ToString();
    }

    // Funcție pentru criptanaliza Cifrei Playfair (descriere)
    public static void Cryptanalysis()
    {
        Console.WriteLine("Criptanaliza Cifrei Playfair poate fi realizată folosind tehnici de frecventă.");
        Console.WriteLine("Tehnici precum analiza bigramelor si observarea de tipare repetitive sunt folosite.");
        Console.WriteLine("Folosind exemple de texte criptate si presupunând un mesaj clasic, se pot deduce cheia si modelul.");
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
                    Cryptanalysis();
                    break;

                default:
                    Console.WriteLine("Optiune invalida.");
                    break;
            }
        }
    }
}
