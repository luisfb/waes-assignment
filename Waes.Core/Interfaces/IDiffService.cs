using Waes.Core.Models;

namespace Waes.Core.Interfaces
{
    public interface IDiffService
    {
        /// <summary>
        /// Save the right side base64 to be used with the method CompareLeftAndRightFiles
        /// </summary>
        /// <param name="key">Unique Identifier</param>
        /// <param name="base64">Base64 string</param>
        void AddRightFileToCompare(string key, string base64);
        /// <summary>
        /// Save the left side base64 to be used with the method CompareLeftAndRightFiles
        /// </summary>
        /// <param name="key">Unique Identifier</param>
        /// <param name="base64">Base64 string</param>
        void AddLeftFileToCompare(string key, string base64);
        /// <summary>
        /// Execute the comparison between the left and right files
        /// </summary>
        /// <param name="key">Unique Identifier</param>
        /// <returns>DiffResult object</returns>
        DiffResult CompareLeftAndRightFiles(string key);
        /// <summary>
        /// Execute the comparison between two byte arrays
        /// </summary>
        /// <param name="left">Left side file to be compared</param>
        /// <param name="right">Right side file to be compared</param>
        /// <returns>DiffResult object</returns>
        DiffResult CompareBytes(byte[] left, byte[] right);
        /// <summary>
        /// Save a DiffResult object in memory
        /// </summary>
        /// <param name="key">Unique Identifier</param>
        /// <param name="diffResult">DiffResult object to be saved</param>
        void StoreDiffResult(string key, DiffResult diffResult);
        /// <summary>
        /// Get a DiffResult object previously saved
        /// </summary>
        /// <param name="key">Unique Identifier</param>
        /// <returns>DiffResult object</returns>
        DiffResult GetStoredDiffResult(string key);
    }
}
