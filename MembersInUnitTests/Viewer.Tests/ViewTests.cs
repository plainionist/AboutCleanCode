using System.Drawing;
using System.Linq;
using NUnit.Framework;

namespace Viewer.Tests
{
    [TestFixture]
    public class RenderingTests
    {
        private BitmapCanvas CreateCanvas(Color background)
        {
            var canvas = new BitmapCanvas();
            canvas.Width = 1200;
            canvas.Height = 800;
            canvas.BackGround = background;
            return canvas;
        }

        private Presentation CreatePresentation()
        {
            var presentation = new Presentation();
            presentation.RenderingMode = RenderingMode.RGB;
            presentation.Font = "Arial";
            presentation.IncludeImages = true;
            presentation.Filters.Add(new ZoomFilter(1.1));

            return presentation;
        }

        [Test]
        public void Render_WithoutPresentation_BitmapIsEmpty()
        {
            var canvas = CreateCanvas(Color.Black);
            var view = new View(canvas);

            view.Render();

            Assert.That(IsEmpty(canvas.Bitmap), Is.True);
        }

        [Test]
        public void Render_WithoutDocument_BitmapIsEmpty()
        {
            var canvas = CreateCanvas(Color.Black);
            var view = new View(canvas);
            view.Presentation = CreatePresentation();

            view.Render();

            Assert.That(IsEmpty(canvas.Bitmap), Is.True);
        }

        [Test]
        public void Render_ValidDocument_BitmapIsNotEmpty()
        {
            var canvas = CreateCanvas(Color.White);
            var view = new View(canvas);
            view.Presentation = CreatePresentation();
            view.Presentation.Documents.Add(Document.Load(@"c:\testdata\doc1.pdf"));

            view.Render();

            // sufficient black pixels should indicate that text is there
            Assert.That(CountPixelsByColor(canvas.Bitmap, Color.Black), 
                Is.GreaterThan(canvas.Bitmap.Pixels.Count() / 4));
        }

        [Test]
        public void Render_GrayScale_BitmapHasNoColor()
        {
            var canvas = CreateCanvas(Color.Black);
            var view = new View(canvas);
            view.Presentation = CreatePresentation();
            view.Presentation.Documents.Add(Document.Load(@"c:\testdata\doc1.pdf"));
            view.Presentation.RenderingMode = RenderingMode.GrayScale;

            view.Render();

            Assert.That(canvas.Bitmap.Pixels.All(x => x.Color.IsGrayScale()), Is.True);
        }

        [Test]
        public void Render_EmptyDocument_BitmapIsEmpty()
        {
            var canvas = CreateCanvas(Color.Black);
            var document1 = Document.Load(@"c:\testdata\doc1.pdf");
            var view = new View(canvas);
            view.Presentation = CreatePresentation();
            document1.Clear();
            view.Presentation.Documents.Add(document1);

            view.Render();

            Assert.That(IsEmpty(canvas.Bitmap), Is.True);
        }

        private bool IsEmpty(Bitmap bitmap) =>
            bitmap.Pixels.All(x => x.Color == Color.Black);

        private int CountPixelsByColor(Bitmap bitmap, Color color) =>
            bitmap.Pixels.Where(x => x.Color == color).Count();
    }
}