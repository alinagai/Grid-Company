using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace GridCom.Models
{
    public class Outage
    {
        public int OutageID { get; set; }
        public int RESID { get; set; }
        public int SubstationID { get; set; }
        public int FeederID { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/yyyy}")]
        public DateTime Date_outage { get; set; }
        public TimeSpan Downtime { get; set; }
        public double UnderSupply { get; set; }
        public int CasesOfOutageID { get; set; }
        public bool APV { get; set; }
        public bool RPV { get; set; }
        public bool OZZ { get; set; }

        public CasesOfOutage CasesOfOutage { get; set; }
        public RES RES{ get; set; }
        public Substation Substation{ get; set; }
        public Feeder Feeder { get; set; }
       
    }
}
