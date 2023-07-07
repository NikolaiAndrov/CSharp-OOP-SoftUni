using BookingApp.Core.Contracts;
using BookingApp.Models.Bookings;
using BookingApp.Models.Bookings.Contracts;
using BookingApp.Models.Hotels;
using BookingApp.Models.Hotels.Contacts;
using BookingApp.Models.Rooms;
using BookingApp.Models.Rooms.Contracts;
using BookingApp.Repositories;
using BookingApp.Repositories.Contracts;
using BookingApp.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookingApp.Core
{
    public class Controller : IController
    {
        private readonly IRepository<IHotel> hotels;

        public Controller()
        {
            this.hotels = new HotelRepository();
        }

        public string AddHotel(string hotelName, int category)
        {
            IHotel hotel = this.hotels.Select(hotelName);

            if (hotel != null)
            {
                return string.Format(OutputMessages.HotelAlreadyRegistered, hotel.FullName);
            }

            hotel = new Hotel(hotelName, category);
            this.hotels.AddNew(hotel);

            return string.Format(OutputMessages.HotelSuccessfullyRegistered, hotel.Category, hotel.FullName);
        }

        public string UploadRoomTypes(string hotelName, string roomTypeName)
        {
            IHotel hotel = this.hotels.Select(hotelName);

            if (hotel == null)
            {
                return string.Format(OutputMessages.HotelNameInvalid, hotelName);
            }

            IRoom room = hotel.Rooms.Select(roomTypeName);

            if (room != null)
            {
                return string.Format(OutputMessages.RoomTypeAlreadyCreated);
            }

            if (roomTypeName == "Apartment")
            {
                room = new Apartment();
            }
            else if (roomTypeName == "DoubleBed")
            {
                room = new DoubleBed();
            }
            else if (roomTypeName == "Studio")
            {
                room = new Studio();
            }
            else
            {
                throw new ArgumentException(string.Format(ExceptionMessages.RoomTypeIncorrect));
            }

            hotel.Rooms.AddNew(room);

            return string.Format(OutputMessages.RoomTypeAdded, room.GetType().Name, hotel.FullName);
        }

        public string SetRoomPrices(string hotelName, string roomTypeName, double price)
        {
            IHotel hotel = this.hotels.Select(hotelName);

            if (hotel == null)
            {
                return string.Format(OutputMessages.HotelNameInvalid, hotelName);
            }

            if (roomTypeName != "Apartment" && 
                roomTypeName != "DoubleBed" &&
                roomTypeName != "Studio")
            {
                throw new ArgumentException(string.Format(ExceptionMessages.RoomTypeIncorrect));
            }

            IRoom room = hotel.Rooms.Select(roomTypeName);

            if (room == null)
            {
                return string.Format(OutputMessages.RoomTypeNotCreated);
            }

            if (room.PricePerNight > 0)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.PriceAlreadySet));
            }

            room.SetPrice(price);

            return string.Format(OutputMessages.PriceSetSuccessfully, room.GetType().Name, hotel.FullName);
        }

        public string BookAvailableRoom(int adults, int children, int duration, int category)
        {
            IHotel hotelFound = null;
            IRoom roomFound = null;
            int bedsNeeded = adults + children;

            bool hotelNotFound = true;
            int diff = int.MaxValue;

            foreach (var hotel in this.hotels.All().OrderBy(h => h.FullName))
            {
                if (hotel.Category == category)
                {
                    hotelNotFound = false;

                    foreach (var room in hotel.Rooms.All().Where(r => r.PricePerNight > 0).OrderBy(r => r.BedCapacity))
                    {
                        if (room.PricePerNight > 0 && room.BedCapacity >= bedsNeeded)
                        {
                            int currentDiff = room.BedCapacity - bedsNeeded;

                            if (currentDiff < diff)
                            {
                                diff = currentDiff;

                                hotelFound = hotel;
                                roomFound = room;
                            }
                        }
                    }
                }
            }

            if (hotelNotFound)
            {
                return string.Format(OutputMessages.CategoryInvalid, category);
            }

            if (roomFound == null)
            {
                return string.Format(OutputMessages.RoomNotAppropriate);
            }

            int bookingNumber = hotelFound.Bookings.All().Count() + 1;
            IBooking booking = new Booking(roomFound, duration, adults, children, bookingNumber);
            hotelFound.Bookings.AddNew(booking);

            return string.Format(OutputMessages.BookingSuccessful, booking.BookingNumber, hotelFound.FullName);
        }

        public string HotelReport(string hotelName)
        {
            ICollection<IHotel> hotels = this.hotels
                .All()
                .ToList()
                .FindAll(h => h.FullName == hotelName);

            StringBuilder sb = new StringBuilder();

            if (hotels.Count == 0 || hotels == null) 
            {
                sb.AppendLine(string.Format(OutputMessages.HotelNameInvalid, hotelName));
                return sb.ToString().Trim();
            }

            foreach (var hotel in hotels)
            {

                sb.AppendLine($"Hotel name: {hotel.FullName}");
                sb.AppendLine($"--{hotel.Category} star hotel");
                sb.AppendLine($"--Turnover: {hotel.Turnover:F2} $");
                sb.AppendLine("--Bookings:");
                sb.AppendLine();

                ICollection<IBooking> bookings = hotel.Bookings.All().ToArray();

                if (bookings.Count == 0 || bookings == null)
                {
                    sb.AppendLine("none");
                }

                foreach (var booking in bookings)
                {
                    sb.AppendLine(booking.BookingSummary());
                    sb.AppendLine();
                }
            }

            return sb.ToString().Trim();
        }
    }
}
