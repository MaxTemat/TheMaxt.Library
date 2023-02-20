using System.IO;
using System.IO.Compression;

namespace TheMaxt.Essentials.Standard
{
    /// <summary>
    /// This class allows to compress and decompress files by using <see cref="GZipStream"/> algorithm
    /// </summary>
    public static class Zip
    {
        /// <summary>
        /// This method allows to compress a file
        /// </summary>
        /// <param name="filePath">The path of the file to compress</param>
        /// <param name="compressedFilePath">The path of the compressed file</param>
        public static void Compress(string filePath, string compressedFilePath)
        {
            using (FileStream inputStream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            using (FileStream outputStream = new FileStream(compressedFilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            using (GZipStream gzip = new GZipStream(outputStream, CompressionMode.Decompress))
                inputStream.CopyTo(gzip);
        }
        /// <summary>
        /// This method allows to decompress a file
        /// </summary>
        /// <param name="decompressedFilePath">The path of the decompressed file</param>
        /// <param name="compressedFilePath">The path of the compressed file</param>
        public static void Decompress(string compressedFilePath, string decompressedFilePath)
        {
            using (FileStream inputStream = new FileStream(compressedFilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            using (FileStream outputStream = new FileStream(decompressedFilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            using (GZipStream gzip = new GZipStream(inputStream, CompressionMode.Decompress))
                gzip.CopyTo(outputStream);
        }
    }
}
