﻿using BookingApp.Models.Bookings;
using BookingApp.Models.Bookings.Contracts;
using BookingApp.Models.Hotels.Contacts;
using BookingApp.Models.Rooms.Contracts;
using BookingApp.Repositories;
using BookingApp.Repositories.Contracts;
using BookingApp.Utilities.Messages;
using System;

namespace BookingApp.Models.Hotels
{
    public class Hotel : IHotel
    {
        private string fullName;
        private int category;

        public Hotel(string fullName, int category)
        {
            this.FullName = fullName;
            this.Category = category;
            this.Rooms = new RoomRepository();
            this.Bookings = new BookingRepository();
        }

        public string FullName
        {
            get { return this.fullName; }

            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.HotelNameNullOrEmpty));
                }
                
                this.fullName = value;
            }
        }

        public int Category
        {
            get { return this.category; }

            private set
            {
                if (value < 1 || value > 5)
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidCategory));
                }

                this.category = value;
            }
        }

        public double Turnover
            => this.TotalTutnover();

        public IRepository<IRoom> Rooms { get; set; }

        public IRepository<IBooking> Bookings { get; set; }

        private double TotalTutnover()
        {
            double total = 0;

            foreach (Booking booking in this.Bookings.All())
            {
                total += booking.TotalPaid();
            }

            return Math.Round(total, 2);
        }
    }
}
