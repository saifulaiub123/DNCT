using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dnct.Domain.Model
{
    public class LoadStrategyModel
    {
        public int? LoadStrategyId { get; set; }
        public string? LoadStrategyName { get; set; }
        public string? LoadStrategyDescription { get; set; }
    }
}
