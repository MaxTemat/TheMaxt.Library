using System;
using System.Collections.Generic;

namespace TheMaxt.Extensions.Standard
{
    /// <summary>
    /// This class adds new methods to some native types.
    /// </summary>
    public static class Extension
    {
        private static List<char> LowerCaseLetters
        {
            get => new List<char>
            {
                'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h',
                'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r',
                's', 't', 'u', 'v', 'w', 'x', 'y', 'z'
            };
        }
        private static List<char> CapitalLetters
        {
            get => new List<char>
            {
                'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H',
                'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R',
                'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'
            };
        }
        private static List<char> SpecialCharacters
        {
            get => new List<char>
            {
                '_', '@', '#', '-', '+', '=', '/', '\\',
                '[', ']', '{', '}', '|', '?', '.', '<', '>', ',',
                '!', '$', '%', '^', '&', '*', '(', ')', ':', ';', '\'', '"'
            };
        }
        private static List<char> AccentedCharacters
        {
            get => new List<char>
            {
                'é', 'ù', 'ê', 'è', 'à', 'â', 'ô'
            };
        }

        /// <summary>
        /// This method allows to know if a string starts with a lowercase letter.
        /// </summary>
        /// <param name="text">The string to inspect.</param>
        /// <param name="index">The index where to start.</param>
        /// <returns>true if string starts with a lowercase letter.</returns>
        public static bool StartWithLowerCase(this string text, int index = 0) => LowerCaseLetters.Contains(text[index]);
        /// <summary>
        /// This method allows to know if a string starts with a capital letter.
        /// </summary>
        /// <param name="text">The string to inspect.</param>
        /// <param name="index">The index where to start.</param>
        /// <returns>true if string starts with a capital letter.</returns>
        public static bool StartWithCapital(this string text, int index = 0) => CapitalLetters.Contains(text[index]);
        /// <summary>
        /// This method allows to know if a string starts with a special character
        /// </summary>
        /// <param name="text">The string to inspect.</param>
        /// <param name="index">The index where to start.</param>
        /// <returns>true if string starts with a special character</returns>
        public static bool StartWithSpecial(this string text, int index = 0) => SpecialCharacters.Contains(text[index]);
        /// <summary>
        /// This method allows to know if a string contains an accented character.
        /// </summary>
        /// <param name="text">The string to inspect.</param>
        /// <returns>true if string contains an accented character.</returns>
        public static bool HasAccented(this string text)
        {
            foreach (var character in text)
                if (AccentedCharacters.Contains(character))
                    return false;
            return true;
        }

        /// <summary>
        /// This method allows to transform a string according to pascal case.
        /// </summary>
        /// <param name="text">The string to transform.</param>
        /// <returns>new string according to pascal case.</returns>
        public static string ToPascalCase(this string text)
        {
            if (string.IsNullOrEmpty(text))
                throw new ArgumentNullException();
            if (text.Length > 1)
            {
                var a = (text[0].ToString()).ToUpper();
                var b = (text.Remove(0, 1)).ToLower();
                return a + b;
            }
            return text.ToUpper();
        }

        /// <summary>
        /// This method allows to transform a string according to camel case.
        /// </summary>
        /// <param name="text">The string to transform.</param>
        /// <returns>new string according to camel case.</returns>
        public static string ToCamelCase(this string text)
        {
            if (string.IsNullOrEmpty(text))
                throw new ArgumentNullException();
            if (text.Length > 1)
            {
                var a = (text[0].ToString()).ToLower();
                var b = (text.Remove(0, 1)).ToLower();
                return a + b;
            }
            return text.ToLower();
        }
    }
}
