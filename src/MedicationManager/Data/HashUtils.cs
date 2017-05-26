using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.Security.Cryptography;

namespace MedicationManager.Data
{
    public class HashUtils 
    {
        // Hashing and Checking Password Values
        
        public static string ComputeHash(string passwordText, string hashAlgorithm, byte[] saltBytes)
        {
            if (saltBytes == null)
            {
                int minSaltSize = 4;
                int maxSaltSize = 8;

                Random randomNumber = new Random();
                int saltSize = randomNumber.Next(minSaltSize, maxSaltSize);

                saltBytes = new byte[saltSize];

                RandomNumberGenerator rng = RandomNumberGenerator.Create();

                rng.GetBytes(saltBytes);
            }

            byte[] passwordTextBytes = Encoding.UTF8.GetBytes(passwordText);

            byte[] passwordTextWithSaltBytes = new byte[passwordText.Length + saltBytes.Length];

            for (int i = 0; i < passwordText.Length; i++)
            {
                passwordTextWithSaltBytes[i] = passwordTextBytes[i];
            }

            for (int i = 0; i < saltBytes.Length; i++)
            {
                passwordTextWithSaltBytes[passwordTextBytes.Length + i] = saltBytes[i];
            }

            HashAlgorithm hash;

            if (hashAlgorithm == null)
            {
                hashAlgorithm = "SHA256";
            }

            switch (hashAlgorithm.ToUpper())
            {
                case "SHA1":
                    hash = SHA1.Create();
                    break;

                case "SHA256":
                    hash = SHA256.Create();
                    break;

                case "SHA384":
                    hash = SHA384.Create();
                    break;

                case "SHA512":
                    hash = SHA512.Create();
                    break;

                default:
                    hash = MD5.Create();
                    break;
            }

            byte[] hashBytes = hash.ComputeHash(passwordTextWithSaltBytes);

            byte[] hashWithSaltBytes = new byte[hashBytes.Length + saltBytes.Length];

            for (int i = 0; i < hashBytes.Length; i++)
            {
                hashWithSaltBytes[i] = hashBytes[i];
            }

            for (int i = 0; i < saltBytes.Length; i++)
            {
                hashWithSaltBytes[hashBytes.Length + i] = saltBytes[i];
            }

            string hashValue = Convert.ToBase64String(hashWithSaltBytes);

            return hashValue;
        }

        
        public static bool VerifyHash(string passwordText, string hashAlgorithm, string hashValue)
        {
            byte[] hashWithSaltBytes = Convert.FromBase64String(hashValue);

            int hashSizeInBits;
            int hashSizeInBytes;

            if (hashAlgorithm == null)
            {
                hashAlgorithm = "SHA256";
            }

            switch (hashAlgorithm.ToUpper())
            {
                case "SHA1":
                    hashSizeInBits = 160;
                    break;

                case "SHA256":
                    hashSizeInBits = 256;
                    break;

                case "SHA384":
                    hashSizeInBits = 384;
                    break;

                case "SHA512":
                    hashSizeInBits = 512;
                    break;

                default:
                    hashSizeInBits = 128;
                    break;
            }

            hashSizeInBytes = hashSizeInBits / 8;

            if (hashWithSaltBytes.Length < hashSizeInBytes)
            {
                return false;
            }

            byte[] saltBytes = new byte[hashWithSaltBytes.Length - hashSizeInBytes];

            for (int i = 0; i < saltBytes.Length; i++)
            {
                saltBytes[i] = hashWithSaltBytes[hashSizeInBytes + i];
            }

            string expectedHashString = ComputeHash(passwordText, hashAlgorithm, saltBytes);

            return (hashValue == expectedHashString);
        }

        /* Code adapted from http://www.obviex.com/samples/hash.aspx and updated to be compatible
         * with .NET Core.
         * Original code samples released under the dual open source licence GPL v.3. */
    }
}
