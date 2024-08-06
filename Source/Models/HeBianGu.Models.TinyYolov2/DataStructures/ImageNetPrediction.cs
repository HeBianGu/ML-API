using Microsoft.ML.Data;

namespace HeBianGu.Models.TinyYolov2.DataStructures
{
    public class ImageNetPrediction
    {
        [ColumnName("grid")]
        public float[] PredictedLabels;
    }
}
