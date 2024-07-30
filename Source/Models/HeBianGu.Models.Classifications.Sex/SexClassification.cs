using HeBianGu_Models_Classifications_Sex;

namespace HeBianGu.Models.Classifications.Sex
{
    public class SexClassification
    {
        public static IOrderedEnumerable<KeyValuePair<string, float>> PredictAllLabels(byte[] imageBytes)
        {
            //var imageBytes = File.ReadAllBytes(@"C:\Users\LENOVO\Pictures\图像分类\人物\0348134dd5de2a80f148522228b33263.jpg");
            var sampleData = new SexClassificationMLModel.ModelInput()
            {
                ImageSource = imageBytes,
            };
            // Make a single prediction on the sample data and print results.
            return SexClassificationMLModel.PredictAllLabels(sampleData);
        }

        public static string PredictLabel(byte[] imageBytes)
        {
            return PredictAllLabels(imageBytes)?.FirstOrDefault().Key;
        }
    }
}
