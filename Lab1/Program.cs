using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    class Program
    {
        public const int ALPHABET_SIZE = 26;
        public const string FILE_PATH = "C:\\Users\\dtrotsky\\source\\repos\\bsuir\\Lab1\\Lab1\\bin\\Debug\\data.txt";
        static void Main(string[] args)
        {
            int mode;
            List<int> convertedInput;
            List<int> convertedKey;
            using (StreamReader sr = new StreamReader(FILE_PATH))
            {
                mode = int.Parse(sr.ReadLine());
                convertedInput = ConvertStringToDigits(sr.ReadLine());
                if (mode == 1 || mode == 2)
                    convertedKey = ConvertStringToDigits(ExpandString(sr.ReadLine(), convertedInput.Count));
                else
                {
                    convertedKey = ConvertStringToDigits(ExpandString(((char)(int.Parse(sr.ReadLine()) + 'A')).ToString(), convertedInput.Count));
                }
            }
            if (mode == 1 || mode == 3)
                Encrypt(convertedInput, convertedKey);
            else
                Decrypt(convertedInput, convertedKey);
            Console.ReadKey();
        }

        public static void Encrypt(List<int> convertedInput, List<int> convertedKey)
        {
            List<int> encryptedDigits = Enumerable.Range(0, convertedInput.Count).Select(i => ModAdd(convertedInput.ElementAt(i), convertedKey.ElementAt(i), ALPHABET_SIZE)).ToList();
            string encryptedText = ConvertDigitsToString(encryptedDigits);
            foreach (char element in encryptedText)
                Console.Write(element + "\t");
        }

        public static void Decrypt(List<int> encryptedDigits, List<int> convertedKey)
        {
            List<int> decryptedDigits = Enumerable.Range(0, encryptedDigits.Count).Select(i => ModSub(encryptedDigits.ElementAt(i), convertedKey.ElementAt(i), ALPHABET_SIZE)).ToList();
            string decryptedText = ConvertDigitsToString(decryptedDigits);
            foreach (char element in decryptedText)
                Console.Write(element + "\t");
        }

        public static List<int> ConvertStringToDigits(string input)
        {
            return new List<int>(input.Select(c => char.ToUpper(c) - 'A'));
        }

        public static string ConvertDigitsToString(List<int> input)
        {
            return string.Concat(input.Select(i => Convert.ToChar(i + 'A')));
        }

        public static string ExpandString(string input, int size)
        {
            if (input.Length == size)
                return input;
            if (input.Length > size)
                return string.Concat(input.Take(size));
            else
            {
                StringBuilder sb = new StringBuilder(input, size);
                while (sb.Length < size)
                    sb.Append(input);
                return string.Concat(sb.ToString().Take(size));
            }
        }

        public static int ModAdd(int a, int b, int mod)
        {
            return (a + b) % mod;
        }
        public static int ModSub(int a, int b, int mod)
        {
            while (a - b < 0)
                a += mod;
            return (a - b) % mod;
        }
    }
}
