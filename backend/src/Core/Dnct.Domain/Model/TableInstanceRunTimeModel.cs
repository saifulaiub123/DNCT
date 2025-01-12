
namespace Dnct.Domain.Model
{
    public class TableInstanceRunTimeModel
    {
        public int? TableConfigId { get; set; }
        public int? RelatedTableConfigId { get; set; }
        public int OverrideInd { get; set; }
        public string InstanceName { get; set; }
        public int InstanceOrder { get; set; }
        public DateTime? ConfigurationEffectiveStartTimestamp { get; set; }
        public DateTime ConfigurationEffectiveEndTimestamp { get; set; }
    }
}
