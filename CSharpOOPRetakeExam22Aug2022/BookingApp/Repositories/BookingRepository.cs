using BookingApp.Models.Bookings.Contracts;
using BookingApp.Repositories.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace BookingApp.Repositories
{
    public class BookingRepository : IRepository<IBooking>
    {
        private ICollection<IBooking> bookings;

        public BookingRepository()
        {
            this.bookings = new List<IBooking>();
        }

        public void AddNew(IBooking model)
        {
            this.bookings.Add(model);
        }

        public IReadOnlyCollection<IBooking> All()
            => (IReadOnlyCollection<IBooking>)this.bookings;

        public IBooking Select(string criteria)
        {
            int bookingNumber = int.Parse(criteria);
            return this.bookings.FirstOrDefault(x => x.BookingNumber == bookingNumber);
        }
    }
}
