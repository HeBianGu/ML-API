using HeBianGu.Models.Data;
using HeBianGu_Models_Detections_Eye;
using Microsoft.ML;
using Microsoft.ML.Data;

namespace HeBianGu.Models.Detections.Eye
{
    public static class EyeDetection
    {
        /// <summary>
        /// 根据图片路径预测物体，返回BoundingBox列表
        /// </summary>
        /// <param name="imagePath"></param>
        /// <returns></returns>
        public static IEnumerable<IBoundingBox> PredictObjects(string imagePath)
        {
            var input = new EyeDetectMLModel.ModelInput { Image = MLImage.CreateFromFile(imagePath) };
            var modelOutput = EyeDetectMLModel.PredictEngine.Value.Predict(input);
            return GetBoundingBoxes(modelOutput);
        }

        /// <summary>
        /// 根据输出结果返回BoundingBox列表
        /// </summary>
        /// <param name="output"></param>
        /// <param name="scoreThreshold"></param>
        /// <returns></returns>
        private static List<IBoundingBox> GetBoundingBoxes(EyeDetectMLModel.ModelOutput output, float scoreThreshold = 0.5f)
        {
            var boundingBoxes = new List<IBoundingBox>();

            if (output.PredictedBoundingBoxes == null || output.PredictedBoundingBoxes.Length == 0)
                return boundingBoxes;

            for (int i = 0; i < output.PredictedBoundingBoxes.Length; i += 4)
            {
                var score = output.Score[i / 4];
                if (score < scoreThreshold)
                    continue;

                var label = output.PredictedLabel[i / 4];
                var x = output.PredictedBoundingBoxes[i];
                var y = output.PredictedBoundingBoxes[i + 1];
                var width = output.PredictedBoundingBoxes[i + 2] - x;
                var height = output.PredictedBoundingBoxes[i + 3] - y;

                var boundingBox = new LabelBoundingBox
                {
                    Label = label,
                    Confidence = score,
                    X = x,
                    Y = y,
                    Width = width,
                    Height = height
                };

                boundingBoxes.Add(boundingBox);
            }

            return boundingBoxes;
        }
    }
}
