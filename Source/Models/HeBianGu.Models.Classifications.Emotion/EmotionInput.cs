using Microsoft.ML.Data;
using Microsoft.ML.Transforms.Image;

namespace HeBianGu.Models.Classifications.Emotion
{
    public class EmotionInput
    {
        [ImageType(64, 64)]
        public MLImage Image { get; set; }
    }
}
