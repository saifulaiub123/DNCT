using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dnct.Domain.Model
{
    public class TblLoadStrategyModel
    {
        public int? TableConfigId { get; set; }
        public int? LoadStrategyId { get; set; }
        public DateTime? ConfigurationEffectiveStartTimestamp { get; set; }
        public DateTime ConfigurationEffectiveEndTimestamp { get; set; }
    }
}
