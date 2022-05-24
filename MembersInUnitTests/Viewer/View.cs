using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Viewer
{
    public class View
    {
        public View(ICanvas canvas)
        {
            Canvas = canvas;
        }

        public Presentation Presentation { get; set; }

        public ICanvas Canvas { get; }

        public void Render()
        {
            // TODO: we would actually do the rendering here and write e.g. 
            // a stream - just a fake implementation
            Canvas.WriteTo(this);
        }
    }
}