namespace ShapeLibrary
{
    public class ShapeCalculator
    {
        public static double CalculateArea(IShape shape)
        {
            return shape.GetArea();
        }
    }
}