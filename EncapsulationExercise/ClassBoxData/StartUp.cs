namespace ClassBoxData
{
    public class StartUp
    {
        public static void Main()
        {
            try
            {
                var length = double.Parse(Console.ReadLine());
                var width = double.Parse(Console.ReadLine());
                var height = double.Parse(Console.ReadLine());
                var box = new Box(length, width, height);
                Console.WriteLine(box);
            }
            catch (Exception mes)
            {
                Console.WriteLine(mes.Message);
            }
            
        }
    }
}