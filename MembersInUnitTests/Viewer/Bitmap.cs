using System;
using System.Collections.Generic;
using System.Drawing;

namespace Viewer
{
    public class Bitmap
    {
        public double Width { get; set; }
        public double Height { get; set; }

        public IReadOnlyCollection<Pixel> Pixels { get; set; }
    }

    public class Pixel
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Color Color { get; set; }
    }
}