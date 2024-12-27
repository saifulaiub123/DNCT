using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dnct.Domain.Entities
{
    public class UserQueries
    {
        [Key]
        [Column("usr_qry_id")]
        public int? UserQueryId { get; set; }
        [Column("table_config_id")]
        public int? TableConfigId { get; set; }

        [Column("usr_qry")]
        public string UserQuery { get; set; }

        [Column("base_query_ind")]
        public int? BaseQueryIndicator { get; set; }

        [Column("qry_order_ind")]
        public int? QueryOrderIndicator { get; set; }

        [Column("row_instr_ts")]
        public DateTime? RowInsertTimestamp { get; set; }
    }
}
