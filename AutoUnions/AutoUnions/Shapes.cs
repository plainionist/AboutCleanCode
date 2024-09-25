namespace AutoUnions;

using Dunet;

[Union]
partial record Shape
{
    partial record Circle(double Radius);
    partial record Rectangle(double Width, double Height);
    partial record Triangle(double Base, double Height);
}