using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dnct.Domain.Entities
{
    [Table("run_time_parmtrs_mstr")]
    public class RunTimeParametersMaster
    {
        [Column("rtm_parmtrs_mstr_id")]
        public int? Id { get; set; }

        [Column("parmtr_key")]
        public string ParameterKey { get; set; }

        [Column("row_instr_ts")]
        public DateTime? RowInsertTimestamp { get; set; }

        [Column("row_upd_ts")]
        public DateTime? RowUpdateTimestamp { get; set; }
    }
}
