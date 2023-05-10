using System;
using Microsoft.ML.Data;

namespace GridCom.DataStructure
{
    public class OutageData
    {
        [LoadColumn(0)]
        public string Substation_Name { get; set; }
        //   [LoadColumn(1)]
        //    public string Equipment_Name;
        [LoadColumn(1)]
        public string Date_Outage { get; set; }
        [LoadColumn(2)]
        public float Temperature { get; set; }
        [LoadColumn(3)]
        public float Wind_speed { get; set; }
        [LoadColumn(4)]
        public float Snow { get; set; }
        [LoadColumn(5)]
        public float Rain { get; set; }
        [LoadColumn(6)]
        public float Thunder { get; set; }
        [LoadColumn(7)]
        public bool Label { get; set; } //Outage
    }
}
