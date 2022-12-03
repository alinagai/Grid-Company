using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
namespace GridCom.Models
{
    public class CasesOfOutage
    {
        public int CasesOfOutageID { get; set; }
        public string Case_outage { get; set; }
        public ICollection<Outage> Outage { get; set; }
    }
}

