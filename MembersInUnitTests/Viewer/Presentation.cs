using System.Collections.Generic;

namespace Viewer
{
    public class Presentation
    {
        public List<Document> Documents { get; } = new List<Document>();
        public List<Filter> Filters { get; } = new List<Filter>();
        public List<Transformation> Transformations { get; } = new List<Transformation>();
        public RenderingMode RenderingMode { get; set; }
        public string Font { get; set; }
        public bool IncludeImages { get; set; }
    }

    public enum RenderingMode
    {
        GrayScale,
        RGB
    }
}