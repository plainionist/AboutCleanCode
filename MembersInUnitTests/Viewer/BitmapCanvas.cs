using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Viewer
{
    public class BitmapCanvas : ICanvas
    {
        public Bitmap Bitmap { get; private set; }

        public int Width { get; set; }
        public int Height { get; set; }

        public Color BackGround { get; set; }

        public void WriteTo(View view)
        {
            Bitmap = new Bitmap()
            {
                Width = Width,
                Height = Height,
                Pixels = GetPixels(view, Width, Height).ToList(),
            };
        }

        private IEnumerable<Pixel> GetPixels(View view, int width, int height)
        {
            var textColor = BackGround == Color.White ? Color.Black : Color.White;
            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    if (view.Presentation == null || view.Presentation.Documents.Count == 0 || view.Presentation.Documents.All(x => x.IsEmpty))
                    {
                        yield return new Pixel
                        {
                            X = x,
                            Y = y,
                            Color = Color.Black
                        };
                    }
                    else if (view.Presentation != null && view.Presentation.RenderingMode == RenderingMode.GrayScale)
                    {
                        yield return new Pixel
                        {
                            X = x,
                            Y = y,
                            Color = Color.FromArgb(x % 200, x % 200, x % 200) // fake
                        };
                    }
                    else
                    {
                        yield return new Pixel
                        {
                            X = x,
                            Y = y,
                            Color = x < width / 2 ? textColor : Color.Black // simulate some percentage of text
                        };
                    }
                }
            }
        }
    }
}
