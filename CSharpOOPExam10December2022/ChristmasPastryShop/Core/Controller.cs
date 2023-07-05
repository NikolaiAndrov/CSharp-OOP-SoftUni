using ChristmasPastryShop.Core.Contracts;
using ChristmasPastryShop.Models.Booths;
using ChristmasPastryShop.Models.Booths.Contracts;
using ChristmasPastryShop.Models.Cocktails;
using ChristmasPastryShop.Models.Cocktails.Contracts;
using ChristmasPastryShop.Models.Delicacies;
using ChristmasPastryShop.Models.Delicacies.Contracts;
using ChristmasPastryShop.Repositories;
using ChristmasPastryShop.Repositories.Contracts;
using ChristmasPastryShop.Utilities.Messages;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChristmasPastryShop.Core
{
    public class Controller : IController
    {
        private IRepository<IBooth> booths;

        public Controller()
        {
            this.booths = new BoothRepository();
        }

        public string AddBooth(int capacity)
        {
            int id = this.booths.Models.Count + 1;
            IBooth booth = new Booth(id, capacity);
            this.booths.AddModel(booth);
            return string.Format(OutputMessages.NewBoothAdded, id, capacity);
        }

        public string AddDelicacy(int boothId, string delicacyTypeName, string delicacyName)
        {
            IDelicacy delicacy;

            if (delicacyTypeName == "Gingerbread")
            {
                delicacy = new Gingerbread(delicacyName);
            }
            else if (delicacyTypeName == "Stolen")
            {
                delicacy = new Stolen(delicacyName);
            }
            else
            {
                return string.Format(OutputMessages.InvalidDelicacyType, delicacyTypeName);
            }

            IBooth booth = FindBoothById(this.booths, boothId);

            IReadOnlyCollection<IDelicacy> delicacies = booth.DelicacyMenu.Models;

            foreach (var dl in delicacies)
            {
                if (dl.Name == delicacy.Name)
                {
                    return string.Format(OutputMessages.DelicacyAlreadyAdded, delicacy.Name);
                }
            }

            booth.DelicacyMenu.AddModel(delicacy);

            return string.Format(OutputMessages.NewDelicacyAdded, delicacy.GetType().Name, delicacy.Name);
        }

        public string AddCocktail(int boothId, string cocktailTypeName, string cocktailName, string size)
        {
            ICocktail cocktail;
            string currentSize = GetSize(size);

            if (cocktailTypeName == "Hibernation")
            {
                if (currentSize == null)
                {
                    return string.Format(OutputMessages.InvalidCocktailSize, size);
                }

                cocktail = new Hibernation(cocktailName, currentSize);
            }
            else if (cocktailTypeName == "MulledWine")
            {
                if (currentSize == null)
                {
                    return string.Format(OutputMessages.InvalidCocktailSize, size);
                }

                cocktail = new MulledWine(cocktailName, currentSize);
            }
            else
            {
                return string.Format(OutputMessages.InvalidCocktailType, cocktailTypeName);
            }

            IBooth booth = FindBoothById(this.booths, boothId);
            ICollection<ICocktail> cocktails = booth.CocktailMenu.Models.ToList();

            foreach (var item in cocktails)
            {
                if (item.Name == cocktail.Name && item.Size == cocktail.Size)
                {
                    return string.Format(OutputMessages.CocktailAlreadyAdded, cocktail.Size, cocktail.Name);
                }
            }

            booth.CocktailMenu.AddModel(cocktail);

            return string.Format(OutputMessages.NewCocktailAdded, cocktail.Size, cocktail.Name, cocktail.GetType().Name);
        }

        public string ReserveBooth(int countOfPeople)
        {
            ICollection<IBooth> allBooths = this.booths.Models
                .Where(b => b.IsReserved == false && b.Capacity >= countOfPeople)
                .ToList();

            if (allBooths == null || allBooths.Count == 0)
            {
                return string.Format(OutputMessages.NoAvailableBooth, countOfPeople);
            }

            allBooths = allBooths.OrderBy(b => b.Capacity)
                .ThenByDescending(b => b.BoothId)
                .ToList();

            IBooth booth = allBooths.First();
            booth.ChangeStatus();

            return string.Format(OutputMessages.BoothReservedSuccessfully, booth.BoothId, countOfPeople);
        }

        public string TryOrder(int boothId, string order)
        {
            string[] itemInfo = order.Split("/");
            string itemTypeName = itemInfo[0];
            string itemName = itemInfo[1];
            int itemsCount = int.Parse(itemInfo[2]);

            ICocktail cocktail = null;
            IDelicacy delicacy = null;

            if (itemTypeName == "Hibernation")
            {
                string sizeGiven = itemInfo[3];
                cocktail = new Hibernation(itemName, sizeGiven);
            }
            else if (itemTypeName == "MulledWine")
            {
                string sizeGiven = itemInfo[3];
                cocktail = new MulledWine(itemName, sizeGiven);
            }
            else if (itemTypeName == "Gingerbread")
            {
                delicacy = new Gingerbread(itemName);
            }
            else if (itemTypeName == "Stolen")
            {
                delicacy = new Stolen(itemName);
            }
            else
            {
                return string.Format(OutputMessages.NotRecognizedType, itemTypeName);
            }


            if (cocktail != null)
            {
                IBooth booth = FindBoothById(this.booths, boothId);
                ICollection<ICocktail> cocktails = booth.CocktailMenu.Models.ToList();

                bool nonExistingItem = true;
                bool isDifferent = false;

                foreach (var item in cocktails)
                {
                    if (item.Name == cocktail.Name)
                    {
                        nonExistingItem = false;
                    }

                    if (item.Name == cocktail.Name && item.Size != cocktail.Size)
                    {
                        isDifferent = true;
                    }

                    if (item.Name == cocktail.Name && item.Size == cocktail.Size)
                    {
                        booth.UpdateCurrentBill(cocktail.Price * itemsCount);
                        break;
                    }
                }

                if (nonExistingItem)
                {
                    return string.Format(OutputMessages.NotRecognizedItemName, itemTypeName, itemName);
                }

                if (isDifferent)
                {
                    return string.Format(OutputMessages.NotRecognizedItemName, cocktail.Size, cocktail.Name);
                }
            }
            else if (delicacy != null)
            {
                IBooth booth = FindBoothById(this.booths, boothId);
                ICollection<IDelicacy> delicacies = booth.DelicacyMenu.Models.ToHashSet();

                bool notFount = true;

                foreach (var item in delicacies)
                {
                    if (item.Name == delicacy.Name)
                    {
                        booth.UpdateCurrentBill(delicacy.Price * itemsCount);
                        notFount = false;
                        break;
                    }
                }

                if (notFount)
                {
                    return string.Format(OutputMessages.NotRecognizedItemName, itemTypeName, itemName);
                }
            }

            return string.Format(OutputMessages.SuccessfullyOrdered, boothId, itemsCount, itemName);
        }

        public string LeaveBooth(int boothId)
        {
            IBooth booth = FindBoothById(this.booths, boothId);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Bill {booth.CurrentBill:f2} lv");
            sb.AppendLine($"Booth {booth.BoothId} is now available!");

            booth.Charge();
            booth.ChangeStatus();

            return sb.ToString().Trim();
        }

        public string BoothReport(int boothId)
        {
            IBooth booth = FindBoothById(this.booths, boothId);
            return booth.ToString();
        }

        private IBooth FindBoothById(IRepository<IBooth> booths, int boothId)
        {
            IBooth booth = null;

            IReadOnlyCollection<IBooth> allBooths = booths.Models;

            foreach (var currentBooth in allBooths)
            {
                if (currentBooth.BoothId == boothId)
                {
                    booth = currentBooth;
                    break;
                }
            }

            return booth;
        }
        private string GetSize(string size)
        {
            if (size == "Small")
            {
                return "Small";
            }
            else if (size == "Middle")
            {
                return "Middle";
            }
            else if (size == "Large")
            {
                return "Large";
            }

            return null;
        }
    }
}
