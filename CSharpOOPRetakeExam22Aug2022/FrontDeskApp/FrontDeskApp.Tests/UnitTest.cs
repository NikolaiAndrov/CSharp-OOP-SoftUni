using FrontDeskApp;
using NUnit.Framework;
using System;
using System.Linq;

namespace BookigApp.Tests
{
    public class Tests
    {
        Hotel hotel;
        Room room1;
        Room room2;
        Room room3;

        [SetUp]
        public void Setup()
        {
            hotel = new Hotel("Grand Hotel", 5);
            room1 = new Room(2, 2);
            room2 = new Room(3, 2);
            room3 = new Room(5, 2);
        }

        [TestCase("")]
        [TestCase("   ")]
        [TestCase(null)]
        public void CreatingHotelWithNullOrWhiteSpaceNameShouldThrow(string hotelName)
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                Hotel hotel = new Hotel(hotelName, 5);
            });
        }

        [TestCase("Grand Hotel")]
        [TestCase("Grand Hotel Grand Hotel Grand Hotel")]
        [TestCase("H")]
        public void CreatingHotelWithProperNameShouldWorkFine(string hotelName)
        {
            Hotel hotel = new Hotel(hotelName, 5);

            string expectedName = hotelName;
            string actualName = hotel.FullName;

            Assert.AreEqual(expectedName, actualName);
        }

        [TestCase(0)]
        [TestCase(-10)]
        [TestCase(6)]
        [TestCase(100)]
        public void CreatingHotelWithNonValidCategoryShouldThrol(int category)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Hotel hotel = new Hotel("Hotel", category);
            });
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        public void CreatingHotelWithValidCategoryShouldWorkFine(int category)
        {
            Hotel hotel = new Hotel("Hotel", category);

            int expectedCategory = category;
            int actualCategory = hotel.Category;

            Assert.AreEqual(expectedCategory, actualCategory);
        }

        [Test]
        public void InitialTurnoverShouldBeSetToZero()
        {
            double expectedTurnover = 0;
            double actualTurnover = hotel.Turnover;

            Assert.AreEqual(expectedTurnover, actualTurnover);
        }

        [Test]
        public void RoomCollectionShoudBeInitialized()
        {
            Assert.IsNotNull(hotel.Rooms);
        }

        [Test]
        public void BookingCollectionShoudBeInitialized()
        {
            Assert.IsNotNull(hotel.Bookings);
        }

        [Test]
        public void AddRoomMethodShouldWorkFine()
        {
            hotel.AddRoom(room1);
            hotel.AddRoom(room2);
            hotel.AddRoom(room3);

            int expectedCount = 3;
            int actualCount = hotel.Rooms.Count;

            Assert.AreEqual(expectedCount, actualCount);
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-100)]
        public void BookingWithInvalidAdultsCountShouldThrow(int adults)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                hotel.BookRoom(adults, 2, 2, 4);
            });
        }

        [TestCase(-1)]
        [TestCase(-100)]
        public void BookingWithInvalidChildrensCountShouldThrow(int children)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                hotel.BookRoom(3, children, 2, 4);
            });
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-100)]
        public void BookingWithInvalidResidenceDurationShouldThrow(int duration)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                hotel.BookRoom(2, 2, duration, 4);
            });
        }

        [Test]
        public void BookingMethodShouldWorkFine()
        {
            hotel.AddRoom(room1);
           
            hotel.BookRoom(1, 1, 2, 4);
            Booking booking = hotel.Bookings.First();

            int expectedBookingNumber = 1;
            int actualBookungNumber = booking.BookingNumber;

            int expectedDuration = 2;
            int actualDuration = booking.ResidenceDuration;

            double expectedTurnover = 4;
            double actualTurnover = hotel.Turnover;

            Assert.AreEqual(expectedBookingNumber, actualBookungNumber);
            Assert.AreEqual(expectedDuration, actualDuration);
            Assert.AreEqual(expectedTurnover, actualTurnover);
            Assert.AreEqual(room1, booking.Room);
        }

        [Test]
        public void BookingMethodShouldNotFindProperRoom()
        {
            hotel.AddRoom(room1);
            hotel.AddRoom(room2);
            hotel.AddRoom(room3);

            hotel.BookRoom(3, 3, 10, 10);

            int expectedBookingsCount = 0;
            int actualBookingsCount = hotel.Bookings.Count();

            Assert.AreEqual(expectedBookingsCount, actualBookingsCount);
        }
    }
}