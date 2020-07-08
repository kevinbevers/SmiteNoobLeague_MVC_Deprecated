using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace SNL_LogicLayer.LogicHelpers
{
    public static class PasswordEncryption
    {
        public static string CreateShaHash(string input)
        {
            // Step 1, calculate MD5 hash from input
            SHA512 sHA512 = SHA512.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = sHA512.ComputeHash(inputBytes);

            // Step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("X2"));
            }
            return sb.ToString();
        }

        public static bool CheckPassword(string UserInput, string DbPass)
        {
            bool Correct = false;
            var hashedInput = CreateShaHash(UserInput);

            if (hashedInput == DbPass)
            {
                //password is correct
                Correct = true;
            }

            return Correct;
        }
    }
}
