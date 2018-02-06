using System;
using System.Collections.Generic;
using System.EnterpriseServices;

namespace Waes.Core.Models
{
    [Serializable]
    [JustInTimeActivation]
    [ObjectPooling(MinPoolSize = 10, MaxPoolSize = 1000, CreationTimeout = 1000, Enabled = true)]
    public class DiffResult
    {
        public bool EqualFiles { get; set; }
        public bool DifferentSize { get; set; }
        public Dictionary<long, long> Diffs { get; set; }
        public IEnumerable<string> Messages { get; set; } 
    }
}