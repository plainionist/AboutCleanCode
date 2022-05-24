using System.Drawing;

namespace Viewer
{
    public static class ColorExtensions
    {
        public static bool IsGrayScale(this Color color) =>
            color.R == color.G && color.R == color.B;
    }
}
