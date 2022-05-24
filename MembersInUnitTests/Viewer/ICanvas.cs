using System.Drawing;

namespace Viewer
{
    public interface ICanvas
    {
        Color BackGround { get; set; }

        void WriteTo(View view);

        /* ... */
    }
}
