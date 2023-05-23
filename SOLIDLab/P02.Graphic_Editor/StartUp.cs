namespace P02.Graphic_Editor
{
    using Models;
    using Models.Interfaces;
    using P02.Graphic_Editor.IO;
    using P02.Graphic_Editor.IO.Interfaces;

    public class StartUp
    {
        public static void Main()
        {
            IWriter writer = new ConsoleWriter();
            IShape shape = new Rectangle();
            GraphicEditor graphicEditor = new GraphicEditor(writer);
            graphicEditor.DrawShape(shape);
        }
    }
}
