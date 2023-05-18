namespace Shapes
{
    public class StartUp
    {
        public static void Main()
        {
            Shape shape = new Circle(10);
            Console.WriteLine(shape.CalculatePerimeter());
            Console.WriteLine(shape.CalculateArea());
            Console.WriteLine(shape.Draw());
        }
    }
}