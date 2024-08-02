using Microsoft.ML;
using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Drawing;
using System.IO;
using System;
using System.IO;
using Microsoft.ML.Transforms.Image;
using Microsoft.ML.Transforms.Onnx;

namespace HeBianGu.Models.TinyYolov3
{
    internal class Class2
    {

        private static PredictionEngine<ModelInput, ModelOutput> CreatePredictionEngine()
        {
            var mlContext = new MLContext();
            var modelPath = "Resources/tiny-yolov3-11.onnx";
            var dataView = mlContext.Data.LoadFromEnumerable(new List<ModelInput>());

            var pipeline = mlContext.Transforms.LoadImages("ImagePath", "ImagePath")
                .Append(mlContext.Transforms.ResizeImages("ImagePath", 416, 416))
                .Append(mlContext.Transforms.ExtractPixels("ImagePath"))
                .Append(mlContext.Transforms.ApplyOnnxModel(modelFile: modelPath, outputColumnNames: new[] { "grid" }, inputColumnNames: new[] { "image" }));

            var model = pipeline.Fit(dataView);
            return mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(model);
        }

    }
    public class ModelInput
    {
        [ColumnName("image")]
        [VectorType(1, 416, 416, 3)]
        public float[] Image { get; set; }
    }

    public class ModelOutput
    {
        [ColumnName("grid")]
        public float[] PredictedLabels { get; set; }
    }

}
