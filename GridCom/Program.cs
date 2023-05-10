using System;
using GridCom.Data;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.IO;
using Microsoft.ML;
using Microsoft.ML.Data;
using static Microsoft.ML.DataOperationsCatalog;
using System.Diagnostics;
using GridCom.DataStructure;

namespace GridCom
{
    public class Program
    {
        private static string TrainDataRelativePath = $"Data/OutageTest50.csv";
        private static string TestDataRelativePath = $"Data/outageTrain50.csv";
        private static string TrainDataPath = GetAbsolutePath(TrainDataRelativePath);
        private static string TestDataPath = GetAbsolutePath(TestDataRelativePath);

        private static string ModelRelativePath = $"/MlModel/OutageClassification.zip";
        private static string ModelPath = GetAbsolutePath(ModelRelativePath);

        static readonly string _dataPath = Path.Combine(Environment.CurrentDirectory, "Data", "outageTrainTest.csv");
        public static void Main(string[] args)
        {
            var mlContext = new MLContext();
            //TrainTestData splitDataView = LoadData(mlContext);//2 version
            BuildTrainEvaluateAndSaveModel(mlContext);
         
            TestPrediction(mlContext);

            Console.WriteLine("=============== End of process, hit any key to finish ===============");//1ver
            var host = CreateHostBuilder(args).Build();
            CreateDbIfNotExists(host);
            host.Run();
          
        }
        private static void TestPrediction(MLContext mlContext)///одиночные прогнозы
        {
            ITransformer trainedModel = mlContext.Model.Load("OutageClassification.zip", out var modelInputSchema);
            var predictionEngine = mlContext.Model.CreatePredictionEngine<OutageData, OutagePrediction>(trainedModel);

            foreach (var outageData in OutageSampleData.outageDataList)
            {
                var prediction = predictionEngine.Predict(outageData);

                Console.WriteLine($"=============== Single Prediction  ===============");
                Console.WriteLine($"Substation: {outageData.Substation_Name} ");
                Console.WriteLine($"Month: {outageData.Date_Outage} ");
                Console.WriteLine($"Temperature: {outageData.Temperature} ");
                Console.WriteLine($"Wind speed: {outageData.Wind_speed} ");
                Console.WriteLine($"Snow: {outageData.Snow} ");
                Console.WriteLine($"Rain: {outageData.Rain} ");
                Console.WriteLine($"Thunder: {outageData.Thunder} ");

                Console.WriteLine($"Prediction Value: {prediction.Prediction} ");
                Console.WriteLine($"Prediction: {(prediction.Prediction ? "Outage could be present" : "Not present outage")} ");
                Console.WriteLine($"Probability: {prediction.Probability} ");
                Console.WriteLine($"==================================================");
                Console.WriteLine("");
                Console.WriteLine("");

            }
        }
        public static TrainTestData LoadData(MLContext mlContext)
        {

            IDataView dataView = mlContext.Data.LoadFromTextFile<OutageData>(_dataPath, hasHeader: true);
            TrainTestData splitDataView = mlContext.Data.TrainTestSplit(dataView, testFraction: 0.6);//здесь мы загружаем файл с данными,делим его на обучение и тест 20%
            return splitDataView;
        }

        private static void BuildTrainEvaluateAndSaveModel(MLContext mlContext)
        {
            // TrainTestData splitDataView = LoadData(mlContext);//2 version
            var trainingDataView = mlContext.Data.LoadFromTextFile<OutageData>(TrainDataPath, hasHeader: true, separatorChar: ';');
            //  IDataView trainingDataView = mlContext.Data.LoadFromTextFile<HeartData>("Data/HeartTraining1.csv", hasHeader: true, separatorChar: ';');
            var testDataView = mlContext.Data.LoadFromTextFile<OutageData>(TestDataPath, hasHeader: true, separatorChar: ';');

            var pipeline = mlContext.Transforms.Categorical.OneHotEncoding(
                outputColumnName: "Substation_Name_Encoded", inputColumnName: "Substation_Name")
                 .Append(mlContext.Transforms.Categorical.OneHotEncoding(
                     outputColumnName: "Date_Outage_Encoded", inputColumnName: "Date_Outage"))
                 .Append(mlContext.Transforms.Concatenate("Features",
                 "Substation_Name_Encoded", "Date_Outage_Encoded", "Temperature", "Wind_speed", "Snow", "Rain", "Thunder"))
                 .Append(mlContext.BinaryClassification.Trainers.Gam(labelColumnName: "Label", featureColumnName: "Features"));

            Console.WriteLine("=============== Training the model ===============");
            ITransformer trainedModel = pipeline.Fit(trainingDataView);

            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("=============== Finish the train model. Push Enter ===============");
            Console.WriteLine("");
            Console.WriteLine("");

            Console.WriteLine("===== Evaluating Model's accuracy with Test data =====");
            var predictions = trainedModel.Transform(testDataView);
            var metrics = mlContext.BinaryClassification.Evaluate(data: predictions, labelColumnName: "Label", scoreColumnName: "Score");

            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine($"************************************************************");
            Console.WriteLine($"*       Metrics for {trainedModel.ToString()} binary classification model      ");
            Console.WriteLine($"*-----------------------------------------------------------");
            Console.WriteLine($"*       Accuracy: {metrics.Accuracy:P2}");
            Console.WriteLine($"*       Area Under Roc Curve:      {metrics.AreaUnderRocCurve:P2}");
            Console.WriteLine($"*       Area Under PrecisionRecall Curve:  {metrics.AreaUnderPrecisionRecallCurve:P2}");
            Console.WriteLine($"*       F1Score:  {metrics.F1Score:P2}");
            Console.WriteLine($"************************************************************");
            Console.WriteLine("");
            Console.WriteLine("");

            Console.WriteLine("=============== Saving the model to a file ===============");
            mlContext.Model.Save(trainedModel, trainingDataView.Schema, "OutageClassification.zip");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("=============== Model Saved ============= ");

        }

        private static void CreateDbIfNotExists(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<GridContext>();
                   // context.Database.EnsureCreated();
                     DbInitializer.Initialize(context);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred creating the DB.");
                }
            }
        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
        private static string GetAbsolutePath(string relativePath)
        {
            FileInfo _dataRoot = new FileInfo(typeof(Program).Assembly.Location);
            string assemblyFolderPath = _dataRoot.Directory.FullName;

            string fullPath = Path.Combine(assemblyFolderPath, relativePath);

            return fullPath;

        }
    }

}
