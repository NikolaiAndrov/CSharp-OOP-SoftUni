using EDriveRent.Models.Contracts;
using EDriveRent.Utilities.Messages;
using System;

namespace EDriveRent.Models
{
    public class User : IUser
    {
        private string firstName;
        private string lastName;
        private string drivingLicenseNumber;
        private double rating;
        private bool isBlocked;

        public User(string firstName, string lastName, string drivingLicenseNumber)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.DrivingLicenseNumber = drivingLicenseNumber;
            this.IsBlocked = false;
        }

        public string FirstName
        {
            get { return this.firstName; }

            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.FirstNameNull);
                }

                this.firstName = value;
            }
        }

        public string LastName
        {
            get { return this.lastName; }

            private  set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.LastNameNull);
                }

                this.lastName = value;
            }
        }

        public string DrivingLicenseNumber
        {
            get { return this.drivingLicenseNumber; }

            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.DrivingLicenseRequired);
                }

                this.drivingLicenseNumber = value;
            }
        }

        public double Rating
        {
            get {return this.rating; }

            private set { this.rating = value; }
        }

        public bool IsBlocked
        {
            get { return this.isBlocked; }

            private set { this.isBlocked = value; }
        }

        //!!!
        public void IncreaseRating()
        {
            this.Rating += 0.5;

            if (this.Rating > 10)
            {
                this.Rating = 10;
            }
        }

        //!!!
        public void DecreaseRating()
        {
            this.Rating -= 2;

            if (this.Rating < 0)
            {
                this.Rating = 0;
                this.IsBlocked = true;
            }
        }


        public override string ToString()
        {
            return $"{this.FirstName} {this.LastName} Driving license: {this.DrivingLicenseNumber} Rating: {this.Rating}";
        }
    }
}
