using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace GridCom.Models
{
    public class Feeder
    {
      
        public int FeederID { get; set; }
        public int SubstationID { get; set; }
        public int Num_feeder { get; set; }
        public double Extent_feeder { get; set; }
        

        public Substation Substations { get; set; }
        public ICollection<Outage> Outages { get; set; }
        
    }
}
