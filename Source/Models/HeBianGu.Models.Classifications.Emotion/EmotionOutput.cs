using Microsoft.ML.Data;

namespace HeBianGu.Models.Classifications.Emotion
{
    public class EmotionOutput
    {
        [ColumnName("Plus692_Output_0")]
        public float[] Result { get; set; }
    }
}
