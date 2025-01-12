
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Dnct.Domain.Model
{
    public class RunTimeParametersMasterModel
    {
        public int? RuntimeParametersMasterId { get; set; }
        public string ParameterKey { get; set; }
        public string ParameterValue { get; set; }
        public int? TableConfigId { get; set; }
        [JsonIgnore]
        public DateTime? ConfigurationEffectiveStartTimestamp { get; set; }
        [JsonIgnore]
        public DateTime ConfigurationEffectiveEndTimestamp { get; set; }

    }
}
