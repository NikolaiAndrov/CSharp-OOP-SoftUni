using AquaShop.Models.Aquariums.Contracts;
using AquaShop.Models.Decorations.Contracts;
using AquaShop.Models.Fish.Contracts;
using AquaShop.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AquaShop.Models.Aquariums
{
    public abstract class Aquarium : IAquarium
    {
        private string name;
        private ICollection<IDecoration> decorations;
        private ICollection<IFish> fish;

        protected Aquarium(string name, int capacity)
        {
            this.Name = name;
            this.Capacity = capacity;
            this.decorations = new List<IDecoration>();
            this.fish = new HashSet<IFish>();
        }

        public string Name
        {
            get => this.name;

            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidAquariumName);
                }

                this.name = value;
            }
        }

        public int Capacity { get; private set; }

        public int Comfort
            => this.Decorations.Sum(d => d.Comfort);

        public ICollection<IDecoration> Decorations 
            => this.decorations;

        public ICollection<IFish> Fish
            => this.fish; 

        public void AddDecoration(IDecoration decoration)
        {
            this.decorations.Add(decoration);
        }

        public void AddFish(IFish fish)
        {
            if (this.Capacity == this.Fish.Count)
            {
                throw new InvalidOperationException(ExceptionMessages.NotEnoughCapacity);
            }

            this.fish.Add(fish);
        }

        public void Feed()
        {
            foreach (var fish in this.Fish)
            {
                fish.Eat();
            }
        }

        public string GetInfo()
        {
            string allFish = this.Fish.Any() ?
                string.Join(", ", this.Fish.Select(f => f.Name)) : "none";

            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{this.Name} ({this.GetType().Name}):");
            sb.AppendLine($"Fish: {allFish}");
            sb.AppendLine($"Decorations: {this.Decorations.Count}");
            sb.Append($"Comfort: {this.Comfort}");

            return sb.ToString();
        }

        public bool RemoveFish(IFish fish)
            => this.fish.Remove(fish);
    }
}
