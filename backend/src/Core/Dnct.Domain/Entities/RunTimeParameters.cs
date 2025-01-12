using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dnct.Domain.Entities
{
    [Table("run_time_parmtrs")]
    public class RunTimeParameters
    {
        [Column("table_config_id")]
        public int? TableConfigId { get; set; }

        [Column("rtm_parmtrs_mstr_id")]
        public int? RuntimeParametersMasterId { get; set; }

        [Column("parmtr_val")]
        public string ParameterValue { get; set; }

        [Column("confgrtn_eff_start_ts")]
        public DateTime? ConfigurationEffectiveStartTimestamp { get; set; }

        [Column("confgrtn_eff_end_ts")]
        [Required]
        public DateTime ConfigurationEffectiveEndTimestamp { get; set; }
    }
}
