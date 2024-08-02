//using System;
//using System.Collections.Generic;
//using System.Drawing;
//using System.IO;
//using System.Linq;
//using Microsoft.ML;
//using Microsoft.ML.Data;
//namespace HeBianGu.Models.TinyYolov3
//{
//    public class TinyYolov3
//    {
//        private static readonly Lazy<PredictionEngine<ModelInput, ModelOutput>> PredictionEngine = new Lazy<PredictionEngine<ModelInput, ModelOutput>>(CreatePredictionEngine);

//        private static PredictionEngine<ModelInput, ModelOutput> CreatePredictionEngine()
//        {
//            var mlContext = new MLContext();
//            var modelPath = "model.onnx";
//            var dataView = mlContext.Data.LoadFromEnumerable(new List<ModelInput>());

//            var pipeline = mlContext.Transforms.LoadImages("ImagePath", "ImagePath")
//                .Append(mlContext.Transforms.ResizeImages("ImagePath", 224, 224))
//                .Append(mlContext.Transforms.ExtractPixels("ImagePath"))
//                .Append(mlContext.Transforms.ApplyOnnxModel(modelFile: modelPath, outputColumnNames: new[] { "output" }, inputColumnNames: new[] { "input" }));

//            var model = pipeline.Fit(dataView);
//            return mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(model);
//        }

//        public static IOrderedEnumerable<KeyValuePair<string, float>> PredictAllLabels(byte[] imageBytes)
//        {
//            using var ms = new MemoryStream(imageBytes);
//            var image = new Bitmap(ms);
//            var input = new ModelInput { Image = ImageToFloatArray(image) };
//            var prediction = PredictionEngine.Value.Predict(input);

//            return prediction.PredictedLabels
//                .Select((score, index) => new KeyValuePair<string, float>(index.ToString(), score))
//                .OrderByDescending(kv => kv.Value);
//        }

//        public static string PredictLabel(byte[] imageBytes)
//        {
//            return PredictAllLabels(imageBytes)?.FirstOrDefault().Key;
//        }

//        //private static float[] ImageToFloatArray(Bitmap image)
//        //{
//        //    var floatArray = new float[1 * 224 * 224 * 3];
//        //    var index = 0;
//        //    for (int y = 0; y < image.Height; y++)
//        //    {
//        //        for (int x = 0; x < image.Width; x++)
//        //        {
//        //            var color = image.GetPixel(x, y);
//        //            floatArray[index++] = color.R / 255f;
//        //            floatArray[index++] = color.G / 255f;
//        //            floatArray[index++] = color.B / 255f;
//        //        }
//        //    }
//        //    return floatArray;
//        //}
//    }

//    public class ModelInput
//    {
//        [ColumnName("input")]
//        [VectorType(1, 224, 224, 3)]
//        public float[] Image { get; set; }
//    }

//    public class ModelOutput
//    {
//        [ColumnName("output")]
//        public float[] PredictedLabels { get; set; }
//    }
//}
