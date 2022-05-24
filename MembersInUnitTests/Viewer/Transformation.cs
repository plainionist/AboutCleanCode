namespace Viewer
{
    public class Transformation
    {
    }

    public class ResizeTransformation : Transformation
    {
        public ResizeTransformation(int v1, int v2)
        {
            V1 = v1;
            V2 = v2;
        }

        public int V1 { get; }
        public int V2 { get; }
    }
}