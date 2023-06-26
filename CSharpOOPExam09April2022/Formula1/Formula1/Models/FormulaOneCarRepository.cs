using Formula1.Models.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace Formula1.Models
{
    public class FormulaOneCarRepository
    {
        private ICollection<IFormulaOneCar> models;

        public FormulaOneCarRepository()
        {
            this.models = new List<IFormulaOneCar>();
        }

        public IReadOnlyCollection<IFormulaOneCar> Models
        {
            get { return (IReadOnlyCollection<IFormulaOneCar>)this.models; }
        }

        public void Add(IFormulaOneCar car)
        {
            models.Add(car);
        }

        public bool Remove(IFormulaOneCar car) => models.Remove(car);

        public IFormulaOneCar FindByName(string model)
        {
            IFormulaOneCar car = models.FirstOrDefault(x => x.Model == model);
            return car;
        }


    }
}
