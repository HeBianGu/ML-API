using HeBianGu.Models.Data;
using System.Drawing;

namespace HeBianGu.Models.TinyYolov2.YoloParser
{
    public class BoundingBoxDimensions : DimensionsBase { }

    public class YoloBoundingBox : LabelBoundingBox
    {
        public RectangleF Rect
        {
            get { return new RectangleF((float)this.X, (float)this.Y, (float)this.Width, (float)this.Height); }
        }
        public Color BoxColor { get; set; }
    }
}