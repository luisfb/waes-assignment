using System.Collections.Generic;
using System.EnterpriseServices;
using Newtonsoft.Json;
using Waes.Core.Models;

namespace Waes.App.DTO
{
    [JustInTimeActivation]
    [ObjectPooling(MinPoolSize = 10, MaxPoolSize = 1000, CreationTimeout = 1000, Enabled = true)]
    public class DiffResultDto
    {
        public DiffResultDto(DiffResult diffResult)
        {
            EqualFiles = diffResult.EqualFiles;
            DifferentSize = diffResult.DifferentSize;
            Messages = diffResult.Messages;
        }

        [JsonProperty(PropertyName = "equalFiles", NullValueHandling = NullValueHandling.Ignore)]
        public bool? EqualFiles { get; set; }

        [JsonProperty(PropertyName = "differentSize", NullValueHandling = NullValueHandling.Ignore)]
        public bool? DifferentSize { get; set; }

        [JsonProperty(PropertyName = "messages", NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<string> Messages { get; set; }

    }
}