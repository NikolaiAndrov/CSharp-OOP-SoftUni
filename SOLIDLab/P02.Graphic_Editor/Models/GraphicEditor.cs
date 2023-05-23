namespace P02.Graphic_Editor.Models
{
    using Interfaces;
    using P02.Graphic_Editor.IO.Interfaces;

    public class GraphicEditor
    {
        private readonly IWriter writer;
        public GraphicEditor(IWriter writer)
        {
            this.writer = writer;
        }
        public void DrawShape(IShape shape)
        {
            writer.WriteLine(shape.ToString());
        }
    }
}
