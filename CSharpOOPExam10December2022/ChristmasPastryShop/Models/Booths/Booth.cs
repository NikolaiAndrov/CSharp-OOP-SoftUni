using ChristmasPastryShop.Models.Booths.Contracts;
using ChristmasPastryShop.Models.Cocktails.Contracts;
using ChristmasPastryShop.Models.Delicacies.Contracts;
using ChristmasPastryShop.Repositories;
using ChristmasPastryShop.Repositories.Contracts;
using ChristmasPastryShop.Utilities.Messages;
using System;
using System.Text;

namespace ChristmasPastryShop.Models.Booths
{
    public class Booth : IBooth
    {
        private int capacity;

        public Booth(int boothId, int capacity)
        {
            this.BoothId = boothId;
            this.Capacity = capacity;
            this.DelicacyMenu = new DelicacyRepository();
            this.CocktailMenu = new CocktailRepository();
            this.CurrentBill = 0;
            this.Turnover = 0;
            this.IsReserved = false;
        }

        public int BoothId { get; private set; }

        public int Capacity
        {
            get { return this.capacity; }

            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.CapacityLessThanOne));
                }

                this.capacity = value;
            }
        }

        public IRepository<IDelicacy> DelicacyMenu { get; internal set; }

        public IRepository<ICocktail> CocktailMenu { get; internal set; }

        public double CurrentBill { get; private set; }

        public double Turnover { get; private set; }

        public bool IsReserved { get; private set; }

        public void ChangeStatus()
        {
            this.IsReserved = !this.IsReserved;
        }

        public void Charge()
        {
            this.Turnover += this.CurrentBill;
            this.CurrentBill = 0;
        }

        public void UpdateCurrentBill(double amount)
        {
            this.CurrentBill += amount;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Booth: {this.BoothId}");
            sb.AppendLine($"Capacity: {this.Capacity}");
            sb.AppendLine($"Turnover: {this.Turnover:f2} lv");
            sb.AppendLine($"-Cocktail menu:");

            foreach (var coctail in this.CocktailMenu.Models)
            {
                sb.AppendLine($"--{coctail.ToString()}");
            }

            sb.AppendLine($"-Delicacy menu:");

            foreach (var delicacy in this.DelicacyMenu.Models)
            {
                sb.AppendLine($"--{delicacy.ToString()}");
            }

            return sb.ToString().Trim();
        }
    }
}
