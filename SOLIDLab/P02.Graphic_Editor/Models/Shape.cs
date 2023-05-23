namespace P02.Graphic_Editor.Models
{
    using Interfaces;
    public abstract class Shape : IShape
    {
        public override string ToString()
        {
            return $"I'm {this.GetType().Name}";
        }
    }
}
