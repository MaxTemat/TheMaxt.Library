using System;
using System.Net;
using System.Threading.Tasks;

namespace TheMaxt.Essentials
{
    /// <summary>
    /// This class allows to  download data from a url
    /// </summary>
    public class DownloadData
    {
        /// <summary>
        /// <see cref="WebClient"/> object
        /// </summary>
        public WebClient Client { get; }
        /// <summary>
        /// Creates new instance of <see cref="DownloadData"/> and initializes <see cref="Client"/> object
        /// </summary>
        public DownloadData()
        {
            Client = new WebClient();
        }
        /// <summary>
        /// This asynchronous method allows to  download data from a url as file
        /// </summary>
        /// <param name="url">Download url</param>
        /// <param name="destination">File storage path</param>
        /// <param name="fileName">Name of file</param>
        public async Task DownloadFileAsync(string url, string destination, string fileName) =>
            await Client.DownloadFileTaskAsync(url, $@"{destination}\{fileName}");
        /// <summary>
        /// This asynchronous method allows to  download data from a url as byte array
        /// </summary>
        /// <param name="url">Download url</param>
        /// <returns>Returns a byte array that represents the data</returns>
        public async Task<byte[]> DownloadByteAsync(string url) =>
            await Client.DownloadDataTaskAsync(url);
        /// <summary>
        /// This asynchronous method allows to  download data from a url as string
        /// </summary>
        /// <param name="url">Download url</param>
        /// <returns>Returns a string that represents the data</returns>
        public async Task<string> DownloadStringAsync(string url) =>
           await Client.DownloadStringTaskAsync(url);
    }
}
