﻿namespace HeBianGu.Models.Data
{
    public interface IBoundingBox
    {
        double X { get; set; }
        double Y { get; set; }
        double Width { get; set; }
        double Height { get; set; }
    }
}
