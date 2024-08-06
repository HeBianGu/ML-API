using HeBianGu.Models.Data;
using HeBianGu.Models.TinyYolov2;
using HeBianGu.Models.TinyYolov2.DataStructures;
using HeBianGu.Models.TinyYolov2.YoloParser;
using Microsoft.ML;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;

public static class TinyYolov2Detection
{
    public static IEnumerable<IEnumerable<IBoundingBox>> PredictObjects(string imagePath)
    {
        var modelFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "Model", "TinyYolo2_model.onnx");

        // Initialize MLContext
        MLContext mlContext = new MLContext();
        // Load Data
        var image = ImageNetData.ReadFromFile(imagePath);
        IDataView imageDataView = mlContext.Data.LoadFromEnumerable(new ImageNetData[] { image });

        // Create instance of model scorer
        var modelScorer = new OnnxModelScorer(modelFilePath, mlContext);

        // Use model to score data
        var probabilities = modelScorer.Score(imageDataView);

        // Post-process model output
        YoloOutputParser parser = new YoloOutputParser();

        return probabilities.Select(probability => parser.ParseOutputs(probability)).Select(boxes => parser.FilterBoundingBoxes(boxes, 5, .5F));
    }

}



//void DrawBoundingBox(string inputImageLocation, string outputImageLocation, string imageName, IList<YoloBoundingBox> filteredBoundingBoxes)
//{
//    Image image = Image.FromFile(Path.Combine(inputImageLocation, imageName));

//    var originalImageHeight = image.Height;
//    var originalImageWidth = image.Width;

//    foreach (var box in filteredBoundingBoxes)
//    {
//        // Get Bounding Box Dimensions
//        var x = (uint)Math.Max(box.Dimensions.X, 0);
//        var y = (uint)Math.Max(box.Dimensions.Y, 0);
//        var width = (uint)Math.Min(originalImageWidth - x, box.Dimensions.Width);
//        var height = (uint)Math.Min(originalImageHeight - y, box.Dimensions.Height);

//        // Resize To Image
//        x = (uint)originalImageWidth * x / OnnxModelScorer.ImageNetSettings.imageWidth;
//        y = (uint)originalImageHeight * y / OnnxModelScorer.ImageNetSettings.imageHeight;
//        width = (uint)originalImageWidth * width / OnnxModelScorer.ImageNetSettings.imageWidth;
//        height = (uint)originalImageHeight * height / OnnxModelScorer.ImageNetSettings.imageHeight;

//        // Bounding Box Text
//        string text = $"{box.Label} ({(box.Confidence * 100).ToString("0")}%)";

//        using (Graphics thumbnailGraphic = Graphics.FromImage(image))
//        {
//            thumbnailGraphic.CompositingQuality = CompositingQuality.HighQuality;
//            thumbnailGraphic.SmoothingMode = SmoothingMode.HighQuality;
//            thumbnailGraphic.InterpolationMode = InterpolationMode.HighQualityBicubic;

//            // Define Text Options
//            Font drawFont = new Font("Arial", 12, FontStyle.Bold);
//            SizeF size = thumbnailGraphic.MeasureString(text, drawFont);
//            SolidBrush fontBrush = new SolidBrush(Color.Black);
//            Point atPoint = new Point((int)x, (int)y - (int)size.Height - 1);

//            // Define BoundingBox options
//            Pen pen = new Pen(box.BoxColor, 3.2f);
//            SolidBrush colorBrush = new SolidBrush(box.BoxColor);

//            // Draw text on image 
//            thumbnailGraphic.FillRectangle(colorBrush, (int)x, (int)(y - size.Height - 1), (int)size.Width, (int)size.Height);
//            thumbnailGraphic.DrawString(text, drawFont, fontBrush, atPoint);

//            // Draw bounding box on image
//            thumbnailGraphic.DrawRectangle(pen, x, y, width, height);
//        }
//    }

//    if (!Directory.Exists(outputImageLocation))
//    {
//        Directory.CreateDirectory(outputImageLocation);
//    }

//    image.Save(Path.Combine(outputImageLocation, imageName));
//}

//void LogDetectedObjects(string imageName, IList<YoloBoundingBox> boundingBoxes)
//{
//    Console.WriteLine($".....The objects in the image {imageName} are detected as below....");

//    foreach (var box in boundingBoxes)
//    {
//        Console.WriteLine($"{box.Label} and its Confidence score: {box.Confidence}");
//    }

//    Console.WriteLine("");
//}
