using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dnct.Application.Features.LoadStragegy.Query.GetAllLoadStrategy
{
    public class GetAllLoadStrategyResponse
    {
        public int? LoadStrategyId { get; set; }
        public string? LoadStrategyName { get; set; }
        public string? LoadStrategyDescription { get; set; }
    }
}
