using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace GridCom.Models
{
    public class Substation
    {
       
        public int SubstationID { get; set; }
        public string Name_PS { get; set; }
        public int RESID { get; set; }
        
        public RES RES { get; set; }
        public ICollection<Feeder> Feeders { get; set; }
        public ICollection<Outage> Outages { get; set; }
      
    }
}
