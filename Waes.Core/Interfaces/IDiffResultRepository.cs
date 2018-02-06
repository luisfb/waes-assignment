using Waes.Core.Models;

namespace Waes.Core.Interfaces
{
    public interface IDiffResultRepository
    {
        /// <summary>
        /// Save the right side base64 for later use
        /// </summary>
        /// <param name="key">Unique Identifier</param>
        /// <param name="base64">Base64 string</param>
        void SaveRightBase64(string key, string base64);
        /// <summary>
        /// Save the left side base64 for later use
        /// </summary>
        /// <param name="key">Unique Identifier</param>
        /// <param name="base64">Base64 string</param>
        void SaveLeftBase64(string key, string base64);
        /// <summary>
        /// Save the DiffResult for later use
        /// </summary>
        /// <param name="key">Unique Identifier</param>
        /// <param name="result">DiffResult object</param>
        void SaveDiffResult(string key, DiffResult result);
        /// <summary>
        /// Get an existing DiffResult previously saved
        /// </summary>
        /// <param name="key">Unique Identifier</param>
        /// <returns>DiffResult object</returns>
        DiffResult GetDiffResult(string key);
        /// <summary>
        /// Get the right side base64 string previously saved
        /// </summary>
        /// <param name="key">Unique Identifier</param>
        /// <returns>Base64 string</returns>
        string GetRightBase64(string key);
        /// <summary>
        /// Get the left side base64 string previously saved
        /// </summary>
        /// <param name="key">Unique Identifier</param>
        /// <returns>Base64 string</returns>
        string GetLeftBase64(string key);
    }
}
