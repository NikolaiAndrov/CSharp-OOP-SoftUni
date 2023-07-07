using BookingApp.Models.Hotels.Contacts;
using BookingApp.Repositories.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace BookingApp.Repositories
{
    public class HotelRepository : IRepository<IHotel>
    {
        private ICollection<IHotel> hotels;

        public HotelRepository()
        {
            this.hotels = new List<IHotel>();
        }

        public void AddNew(IHotel model)
        {
            this.hotels.Add(model);
        }

        public IReadOnlyCollection<IHotel> All()
            => (IReadOnlyCollection<IHotel>)this.hotels;

        public IHotel Select(string criteria)
        {
            return this.hotels.FirstOrDefault(x => x.FullName == criteria);
        }
    }
}
