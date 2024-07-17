using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineerWorkSplace.Models
{
    internal class ESPD
    {
        public bool ServiceStatus { get; set; }
        public bool EthernetAccess {  get; set; }
        public int PingESPD { get; set; }
        public bool ProxyAccess { get; set; }
        public int PingProxy { get; set; }
    }
}
