namespace HeBianGu.Models.Data
{
    public class LabelBoundingBox : BoundingBox, ILabelBoundingBox
    {
        public string Label { get; set; }
        public float Confidence { get; set; }

        public override string ToString()
        {
            return $"{Label} - {Confidence} :{X},{Y},{Width},{Height}";
        }
    }
}
