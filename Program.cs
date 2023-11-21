using System;
using System.Security.Cryptography;
using System.Text;

class Program
{
            
    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("Enter the password to hash (or type 'exit' to quit):");
            string? password = Console.ReadLine();

            if (string.IsNullOrEmpty(password))
            {
                Console.WriteLine("Password cannot be empty. Please enter a valid password.");
                continue; // Go back to the start of the loop for a new input
            }

            if (password.Equals("exit", StringComparison.OrdinalIgnoreCase))
            {
                break; // Exit the loop if the user enters 'exit'
            }

            byte[] salt = GenerateSalt();
            byte[] hashedPassword = HashPassword(password, salt);

            Console.WriteLine("Salt (Base64): " + Convert.ToBase64String(salt));
            Console.WriteLine("Hashed Password (Base64): " + Convert.ToBase64String(hashedPassword));
            Console.WriteLine(); // Add an empty line for better readability
        }
    }


    public static byte[] GenerateSalt(int size = 16)
    {
        var salt = new byte[size];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }
        return salt;
    }

    public static byte[] HashPassword(string password, byte[] salt, int size = 64)
    {
        using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000, HashAlgorithmName.SHA512))
        {
            return pbkdf2.GetBytes(size);
        }
    }
}
