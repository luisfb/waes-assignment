using Waes.Core.Interfaces;
using Waes.Core.Models;
using Waes.WaesDiff;

namespace Waes.Infrastructure.Repositories
{
    public class DiffResultRepository : IDiffResultRepository
    {
        private readonly IInMemoryRepository _inMemoryRepository;
        private const string RIGHT_SUFFIX = "_right";
        private const string LEFT_SUFFIX = "_left";
        private const string RESULT_SUFFIX = "_diffResult";

        public DiffResultRepository(IInMemoryRepository inMemoryRepository)
        {
            _inMemoryRepository = inMemoryRepository;
        }
        public void SaveRightBase64(string key, string base64)
        {
            _inMemoryRepository.Save(key + RIGHT_SUFFIX, base64);
            _inMemoryRepository.Delete(key + RESULT_SUFFIX);
        }

        public void SaveLeftBase64(string key, string base64)
        {
            _inMemoryRepository.Save(key + LEFT_SUFFIX, base64);
            _inMemoryRepository.Delete(key + RESULT_SUFFIX);
        }

        public void SaveDiffResult(string key, DiffResult result)
        {
            _inMemoryRepository.Save(key + RESULT_SUFFIX, ObjectConverter.ObjectToString(result));
        }

        public DiffResult GetDiffResult(string key)
        {
            var value = _inMemoryRepository.GetByKey(key + RESULT_SUFFIX);
            return string.IsNullOrWhiteSpace(value) ? null : ObjectConverter.StringToObject(value) as DiffResult;
        }

        public string GetRightBase64(string key)
        {
            return _inMemoryRepository.GetByKey(key + RIGHT_SUFFIX);
        }

        public string GetLeftBase64(string key)
        {
            return _inMemoryRepository.GetByKey(key + LEFT_SUFFIX);
        }
    }
}
