﻿using System;
using System.Collections.Generic;
using System.Text;
using System;
using System.IO;
using System.Security.Cryptography;

namespace SecureMedMail.Util.Encryption
{
    class AesFileEncryption
    {
        private String inputFile;


        static byte[] EncryptStringToBytes(string plainText, byte[] Key, byte[] IV)
        {
            // Check arguments. 
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("Key");
            byte[] encrypted;
            // Create an RijndaelManaged object 
            // with the specified key and IV. 
            using (RijndaelManaged rijAlg = new RijndaelManaged())
            {
                rijAlg.Key = Key;
                rijAlg.IV = IV;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);

                // Create the streams used for encryption. 
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {

                        //csEncrypt.Write();
                        
                        /*
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {

                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        */
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }


            // Return the encrypted bytes from the memory stream. 
            return encrypted;

        }

        static string DecryptStringFromBytes(byte[] cipherText, byte[] Key, byte[] IV)
        {
            // Check arguments. 
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("Key");

            // Declare the string used to hold 
            // the decrypted text. 
            string plaintext = null;

            // Create an RijndaelManaged object 
            // with the specified key and IV. 
            using (RijndaelManaged rijAlg = new RijndaelManaged())
            {
                rijAlg.Key = Key;
                rijAlg.IV = IV;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

                // Create the streams used for decryption. 
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {

                            // Read the decrypted bytes from the decrypting stream 
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }

            }

            return plaintext;

        }

        // Print the byte array in a readable format. 
        public static void PrintByteArray(byte[] array)
        {
            int i;
            for (i = 0; i < array.Length; i++)
            {
                Console.Write(String.Format("{0:X2}", array[i]));
                if ((i % 4) == 3) Console.Write(" ");
            }
            Console.WriteLine();
        }

        public static void Main2()
        {
            try
            {

                string original = "Here is some data to encrypt!";

                SHA256 mySHA256 = SHA256Managed.Create();

                byte [] hash = mySHA256.ComputeHash(Encoding.ASCII.GetBytes(original));

                PrintByteArray(hash);


                // Create a new instance of the RijndaelManaged 
                // class.  This generates a new key and initialization  
                // vector (IV). 
//                using (RijndaelManaged myRijndael = new RijndaelManaged())
//                {
//
//                    myRijndael.GenerateKey();
//                    myRijndael.GenerateIV();
//                    // Encrypt the string to an array of bytes. 
//                    byte[] encrypted = EncryptStringToBytes(original, myRijndael.Key, myRijndael.IV);
//
//                    // Decrypt the bytes to a string. 
//                    string roundtrip = DecryptStringFromBytes(encrypted, myRijndael.Key, myRijndael.IV);
//
//                    //Display the original data and the decrypted data.
//                    Console.WriteLine("Original:   {0}", original);
//                    Console.WriteLine("Round Trip: {0}", roundtrip);
//                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e.Message);
            }
        }


    }
}
