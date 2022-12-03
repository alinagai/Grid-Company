using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace GridCom.Models
{
    public class RES
    {
        
        public int RESID { get; set; }
        public string Name_RES { get; set; }
        public string FullName_RES { get; set; }
        public string Adress_RES { get; set; }

        public ICollection<Substation> Substations { get; set; }
        public ICollection<Outage> Outages { get; set; }
       
    }
}
