using Microsoft.ML.Data;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HeBianGu.Models.TinyYolov2.DataStructures
{
    public class ImageNetData
    {
        [LoadColumn(0)]
        public string ImagePath;

        [LoadColumn(1)]
        public string Label;

        public static ImageNetData ReadFromFile(string imagePath)
        {
            return new ImageNetData { ImagePath = imagePath, Label = Path.GetFileName(imagePath) };
        }
    }
}