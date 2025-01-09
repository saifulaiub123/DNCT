using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Dnct.Domain.Entities
{
    [Table("load_strategy")]
    public class LoadStrategy
    {
        [Column("load_stratgy_id")]
        public int? LoadStrategyId { get; set; }

        [Column("load_stratgy_name")]
        [MaxLength(500)]
        public string? LoadStrategyName { get; set; }

        [Column("load_stratgy_description")]
        [MaxLength(900)]
        public string? LoadStrategyDescription { get; set; }
    }
}
