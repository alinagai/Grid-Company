using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ML;
using Microsoft.ML.Data;
using static Microsoft.ML.DataOperationsCatalog;
using System.Diagnostics;
using GridCom.DataStructure;
using DocumentFormat.OpenXml.Drawing.Wordprocessing;
using Microsoft.AspNetCore.Components.Forms;
using DocumentFormat.OpenXml.Office2013.Excel;
using DocumentFormat.OpenXml.Spreadsheet;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
namespace GridCom.Controllers
{
    
    public class PredictController : Controller
    {
       // private static string TrainDataRelativePath = $"Data/OutageTest50.csv";

     //   private static string TrainDataPath = GetAbsolutePath(TrainDataRelativePath);
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


        /*  [HttpPost]
          public IActionResult Predict()
           {   /*   var pipeline = mlContext.Transforms.Categorical.OneHotEncoding(outputColumnName: "Substation_Name_Encoded", inputColumnName: "Substation_Name").
     Append(mlContext.Transforms.Categorical.OneHotEncoding(outputColumnName: "Date_Outage_Encoded", inputColumnName: "Date_Outage"))
              .Append(mlContext.Transforms.Concatenate("Features",
              "Substation_Name_Encoded", "Date_Outage_Encoded", "Temperature", "Wind_speed", "Snow", "Rain", "Thunder"));
        */
        /*var mlContext = new MLContext();
               ITransformer trainedModel = mlContext.Model.Load("OutageClassification.zip", out var modelInputSchema);
              var predictionEngine = mlContext.Model.CreatePredictionEngine<OutageData, OutagePrediction>(trainedModel);
               ViewBag.Result = predictionEngine;
            ViewData.Add("prediction", predictionEngine.Prediction);
        return View();       
    }*/

        /*  [HttpPost]
           public IActionResult Predict(OutageData input)
           {
               var mlContext = new MLContext();
               var outage_for_predict = new OutageData
               {
                   Substation_Name = input.Substation_Name,
                   Date_Outage = input.Date_Outage,
                   Temperature = input.Temperature,
                   Wind_speed = input.Wind_speed,
                   Snow = input.Snow,
                   Rain = input.Rain,
                   Thunder = input.Thunder,
                   Label = true
               };

               ITransformer trainedModel = mlContext.Model.Load("OutageClassification.zip", out var modelInputSchema);
               if (trainedModel !=null)
               {
                   Console.WriteLine("ZARGUZILAS");
               }
               var predictionEngine = mlContext.Model.CreatePredictionEngine<OutageData, OutagePrediction>(trainedModel);
               var prediction = predictionEngine.Predict(outage_for_predict);

               ViewData.Add("Prediction", prediction.Prediction);
               ViewData.Add("Probability", prediction.Probability);
               Console.WriteLine("PROGNOZZZZZz === {0}",prediction.Probability);
               return View();
           }

         /*[HttpPost]
         public IActionResult Predict(string substation_name, string date_outage, float temperature, float wind_speed, float snow, float rain, float thunder)
         {
             var mlContext = new MLContext();
             var trainingDataView = mlContext.Data.LoadFromTextFile<OutageData>(TrainDataPath, hasHeader: true, separatorChar: ';');
             var outage_for_predict = new List<OutageData>
             {
                new OutageData(){
                 Substation_Name = substation_name,
                 Date_Outage = date_outage,
                 Temperature = temperature,
                 Wind_speed = wind_speed,
                 Snow = snow,
                 Rain = rain,
                 Thunder = thunder,
                 Label = true }
             };
             OutageData outage_for_predict1 = new OutageData
             {
                 Substation_Name = substation_name,
                 Date_Outage = date_outage,
                 Temperature = temperature,
                 Wind_speed = wind_speed,
                 Snow = snow,
                 Rain = rain,
                 Thunder = thunder,
                 Label = true
             };
             var testdataView = mlContext.Data.LoadFromEnumerable(outage_for_predict);
             DataViewSchema predictionPipelineSchema;

             var pipeline = mlContext.Transforms.Categorical.OneHotEncoding(
                    outputColumnName: "Substation_Name_Encoded", inputColumnName: "Substation_Name")
                     .Append(mlContext.Transforms.Categorical.OneHotEncoding(
                         outputColumnName: "Date_Outage_Encoded", inputColumnName: "Date_Outage"))
                     .Append(mlContext.Transforms.Concatenate("Features",
                     "Substation_Name_Encoded", "Date_Outage_Encoded", "Temperature", "Wind_speed", "Snow", "Rain", "Thunder"))
                     .Append(mlContext.BinaryClassification.Trainers.Gam(labelColumnName: "Label", featureColumnName: "Features"));
             ITransformer trainedModel = pipeline.Fit(trainingDataView);
             var predictions = trainedModel.Transform(testdataView);
             mlContext.Model.Save(trainedModel, trainingDataView.Schema, "OutageClassification1.zip");


             // Create PredictionEngines
              ITransformer predictionPipeline = mlContext.Model.Load("OutageClassification1.zip", out predictionPipelineSchema);

              PredictionEngine<OutageData, OutagePrediction> predictionEngine = mlContext.Model.CreatePredictionEngine<OutageData, OutagePrediction>(predictionPipeline);
             OutagePrediction prediction = predictionEngine.Predict(outage_for_predict1);
             ViewBag.Result = predictionEngine;

             ViewData.Add("Prediction", prediction.Prediction);
             ViewData.Add("Probability", prediction.Probability);
             Console.WriteLine("PROGNOZZZZZz === {0}", prediction.Probability);

             return View();
         }


         private static string GetAbsolutePath(string relativePath)
         {
             FileInfo _dataRoot = new FileInfo(typeof(Program).Assembly.Location);
             string assemblyFolderPath = _dataRoot.Directory.FullName;

             string fullPath = Path.Combine(assemblyFolderPath, relativePath);

             return fullPath;

         }*/
        [HttpPost]
        public IActionResult Predict(string substation_name, string date_outage, float temperature, float wind_speed, float snow, float rain, float thunder)
        {
            var mlContext = new MLContext();
          
            OutageData outage_for_predict = new OutageData
            {
                Substation_Name = substation_name,
                Date_Outage = date_outage,
                Temperature = temperature,
                Wind_speed = wind_speed,
                Snow = snow,
                Rain = rain,
                Thunder = thunder,
                Label = true
            };
          
               DataViewSchema predictionPipelineSchema;

           
            // Create PredictionEngines
            ITransformer predictionPipeline = mlContext.Model.Load("OutageClassification.zip", out predictionPipelineSchema);

            PredictionEngine<OutageData, OutagePrediction> predictionEngine = mlContext.Model.CreatePredictionEngine<OutageData, OutagePrediction>(predictionPipeline);
            OutagePrediction prediction = predictionEngine.Predict(outage_for_predict);
            ViewBag.Result = predictionEngine;

            ViewData.Add("Prediction", prediction.Prediction);
            ViewData.Add("Probability", prediction.Probability);
            Console.WriteLine("PROGNOZZZZZz === {0}", prediction.Probability);

            return View();
        }
    }
}

