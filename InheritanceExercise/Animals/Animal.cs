
namespace Animals
{
    using System;
    using System.Text;
    public abstract class Animal
    {
        private string name;
        private int age;
        private string gender;
        protected Animal(string name, int age, string gender)
        {
            Name = name;
            Age = age;
            Gender = gender;
        }

        public string Name
        {
            get { return name; }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Invalid Name");
                }
                name = value;
            }
        }
        public int Age 
        {
            get { return age; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentNullException("Invalid Age");
                }
                age = value;
            }
        }
        public string Gender
        {
            get { return gender; }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Invalid Gender");
                }
                gender = value;
            }
        }

        public abstract string ProduceSound();

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{this.GetType().Name}");
            sb.AppendLine($"{this.Name} {this.Age} {this.Gender}");
            sb.Append(this.ProduceSound());
            return sb.ToString();
        }
    }
}
