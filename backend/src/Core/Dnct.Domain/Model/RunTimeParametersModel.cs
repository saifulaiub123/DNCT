
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Dnct.Domain.Model
{
    public class RunTimeParametersModel
    {
        public int? TableConfigId { get; set; }
        public int? RuntimeParametersMasterId { get; set; }
        public string ParameterValue { get; set; }
        public DateTime? ConfigurationEffectiveStartTimestamp { get; set; }
        public DateTime ConfigurationEffectiveEndTimestamp { get; set; }
    }
}
