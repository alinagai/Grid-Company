using System;
using Microsoft.ML.Data;

namespace GridCom  .DataStructure
{
    public class OutagePrediction
    {
        [ColumnName("PredictedLabel")]
        public bool Prediction;

        public float Probability;

        public float Score;
    }
}



