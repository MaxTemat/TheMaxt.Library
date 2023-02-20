using System.Text;

namespace TheMaxt.Cryptography
{
    /// <summary>
    /// This class allows data to be hashed using the MD5 algorithm
    /// </summary>
    public static class MD5
    {
        /// <summary>
        /// This method allows to hash a string according to the MD5 algorithm
        /// </summary>
        /// <param name="Text">The string to hash</param>
        /// <returns>Returns the hashed string</returns>
        public static string Hash(string Text)
        {
            System.Security.Cryptography.MD5 md5Hash = System.Security.Cryptography.MD5.Create();
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(Text));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }
    }
}
