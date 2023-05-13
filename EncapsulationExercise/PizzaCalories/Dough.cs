namespace PizzaCalories
{
    public class Dough
    {
        private string flourType;
        private string bakingTechnique;
        private double weight;

        public Dough(string flourType, string bakingTechnique, double weight)
        {
            FlourType = flourType.ToLower();
            BakingTechnique = bakingTechnique.ToLower();
            Weight = weight;
        }

        public string FlourType
        {
            get
            {
                return flourType;
            }
            private set
            {
                if (value != "white" && value != "wholegrain")
                {
                    throw new ArgumentException("Invalid type of dough.");
                }
                flourType = value;
            }
        }

        public string BakingTechnique
        {
            get
            {
                return bakingTechnique;
            }
            private set
            {
                if (value != "crispy" && value != "chewy" && value != "homemade")
                {
                    throw new ArgumentException("Invalid type of dough.");
                }

                bakingTechnique = value;
            }
        }

        public double Weight
        {
            get
            {
                return weight;
            }
            private set
            {
                if (value < 1 || value > 200)
                {
                    throw new ArgumentException("Dough weight should be in the range [1..200].");
                }
                weight = value;
            }
        }

        public double TotalCalories
        {
            get
            {
                return CalculateCalories();
            }
        }

        private double CalculateCalories()
        {
            double flourCalories = 0;
            double bakingCalories = 0;

            if (FlourType == "white")
            {
                flourCalories = 1.5;
            }
            else if (FlourType == "wholegrain")
            {
                flourCalories = 1.0;
            }

            if (BakingTechnique == "crispy")
            {
                bakingCalories = 0.9;
            }
            else if (BakingTechnique == "chewy")
            {
                bakingCalories = 1.1;
            }
            else if (BakingTechnique == "homemade")
            {
                bakingCalories = 1.0;
            }

            return (2 * Weight) * flourCalories * bakingCalories;
            
        }
    }
}
