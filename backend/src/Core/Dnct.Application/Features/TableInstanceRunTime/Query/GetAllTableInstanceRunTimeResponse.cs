using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dnct.Application.Features.TableInstanceRunTime.Query
{
    public class GetAllTableInstanceRunTimeResponse
    {
        public int? TableConfigId { get; set; }
        public int? RelatedTableConfigId { get; set; }
        public int OverrideInd { get; set; }
        public DateTime? ConfigurationEffectiveStartTimestamp { get; set; }
        public DateTime ConfigurationEffectiveEndTimestamp { get; set; }
    }
}
