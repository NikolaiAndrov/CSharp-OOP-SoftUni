
namespace BorderControl.Models
{
    using Interfaces;
    public class Robot : Creature, IRobot
    {
        public Robot(string name, string id)
            : base(name)
        {
            Id = id;
        }

        public string Id { get; private set; }
    }
}
