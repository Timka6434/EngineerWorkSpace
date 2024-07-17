using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineerWorkSplace.Models
{
    internal class Certificates
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Position { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int DaysLeftToExpire => (EndTime - DateTime.Now).Days;
    }
}
