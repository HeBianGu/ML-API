namespace HeBianGu.Models.Classifications.Emotion
{
    public static class EmotionClassification
    {
        private static EmotionPrediction emotionPrediction = new EmotionPrediction();

        public static string PredictLabel(string path)
        {
            return emotionPrediction.Predict(path);
        }
    }
}
