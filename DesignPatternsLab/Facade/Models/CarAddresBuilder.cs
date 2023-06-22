namespace Facade.Models
{
    public class CarAddresBuilder : CarBuilderFacade
    {
        public CarAddresBuilder(Car car)
        {
            Car = car;
        }

        public CarAddresBuilder InCity(string city)
        {
            Car.City = city;
            return this;
        }

        public CarAddresBuilder AtAdress(string adress)
        {
            Car.Adress = adress;
            return this;
        }
    }
}
