namespace HeBianGu.Models.Data
{
    public interface ILabelBoundingBox : IBoundingBox
    {
        string Label { get; set; }
        float Confidence { get; set; }
    }
}
