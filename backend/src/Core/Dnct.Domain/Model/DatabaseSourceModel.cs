using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dnct.Domain.Model
{
    public class DatabaseSourceModel
    {
        public int ServerId { get; set; }
        public string HostIp { get; set; }
        public int DatabaseSourceId { get; set; }
        public string DataBaseName { get; set; }
        public string TableName { get; set; }
    }
}
