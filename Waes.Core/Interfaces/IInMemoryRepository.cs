namespace Waes.Core.Interfaces
{
    public interface IInMemoryRepository
    {
        /// <summary>
        /// Get from memory a string previously saved
        /// </summary>
        /// <param name="key">Unique Identifier</param>
        /// <returns>The saved string</returns>
        string GetByKey(string key);
        /// <summary>
        /// Save a string in memory
        /// </summary>
        /// <param name="key">Unique Identifier</param>
        /// <param name="value">The string to be saved</param>
        void Save(string key, string value);
        /// <summary>
        /// Delete from memory a string value previously saved
        /// </summary>
        /// <param name="key">Unique Identifier</param>
        void Delete(string key);
    }
}
