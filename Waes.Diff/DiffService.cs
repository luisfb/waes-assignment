using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Waes.Core.Interfaces;
using Waes.Core.Models;

namespace Waes.WaesDiff
{
    public class DiffService : IDiffService
    {
        private readonly IDiffResultRepository _repository;
        
        public DiffService(IDiffResultRepository repository)
        {
            _repository = repository;
        }

        public void AddRightFileToCompare(string key, string base64)
        {
            _repository.SaveRightBase64(key, base64);
        }
        public void AddLeftFileToCompare(string key, string base64)
        {
            _repository.SaveLeftBase64(key, base64);
        }

        public void StoreDiffResult(string key, DiffResult diffResult)
        {
            _repository.SaveDiffResult(key, diffResult);
        }
        public DiffResult GetStoredDiffResult(string key)
        {
            return _repository.GetDiffResult(key);
        }

        public DiffResult CompareLeftAndRightFiles(string key)
        {
            var left = _repository.GetLeftBase64(key);
            var right = _repository.GetRightBase64(key);

            if(string.IsNullOrEmpty(left))
                return new DiffResult {Messages = new List<string>() {"Left base64 must be provided"} };

            if (string.IsNullOrEmpty(right))
                return new DiffResult { Messages = new List<string>() { "Right base64 must be provided" } };

            var leftByteArray = Convert.FromBase64String(left);
            var rightByteArray = Convert.FromBase64String(right);

            if (leftByteArray.LongLength != rightByteArray.LongLength)
            {
                return new DiffResult
                {
                    DifferentSize = true,
                    Messages = new List<string> { "Both files must be of same size." }
                };
            }

            return CompareBytes(leftByteArray, rightByteArray);
        }

        public DiffResult CompareBytes(byte[] left, byte[] right)
        {
            if (left.LongLength != right.LongLength)
                throw new InvalidEnumArgumentException("Both byte arrays must be of same size.");
            
            var offsetAndLengthDiffs = new Dictionary<long, long>();
            long currentDiffOffset = 0;
            var sameDiffGroup = false;
            var equalFiles = true;
            for (long i = 0; i < left.LongLength; i++)
            {
                if (left[i] != right[i])
                {
                    equalFiles = false;
                    if (!sameDiffGroup)
                    {
                        currentDiffOffset = i;
                        offsetAndLengthDiffs.Add(currentDiffOffset, 0);
                    }
                    sameDiffGroup = true;
                    offsetAndLengthDiffs[currentDiffOffset]++;
                }
                else
                {
                    sameDiffGroup = false;
                }
            }
            
            return new DiffResult
            {
                Diffs = offsetAndLengthDiffs,
                EqualFiles = equalFiles,
                Messages = equalFiles ? new List<string> { "Both files are equal" } : GenerateInsights(offsetAndLengthDiffs)
            };
        }

        private IEnumerable<string> GenerateInsights(Dictionary<long, long> diffs)
        {
            return diffs.Select(diff => $"Difference found at position {diff.Key} with length of {diff.Value}").ToList();
        }
        
    }
}
