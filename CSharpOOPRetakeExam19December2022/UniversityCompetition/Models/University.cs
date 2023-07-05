using System;
using System.Collections.Generic;
using UniversityCompetition.Models.Contracts;
using UniversityCompetition.Utilities.Messages;

namespace UniversityCompetition.Models
{
    public class University : IUniversity
    {
        private string name;
        private string category;
        private int capacity;

        public University(int universityId, string universityName, string category, int capacity, ICollection<int> requiredSubjects)
        {
            this.Id = universityId;
            this.Name = universityName;
            this.Category = category;
            this.Capacity = capacity;
            this.RequiredSubjects = (IReadOnlyCollection<int>)requiredSubjects;
        }

        public int Id { get; private set; }

        public string Name
        {
            get { return this.name; }

            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.NameNullOrWhitespace));
                }

                this.name = value;
            }
        }

        public string Category
        {
            get { return this.category; }

            private set
            {
                if (value != "Technical" && value != "Economical" && value != "Humanity")
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.CategoryNotAllowed, value));
                }

                this.category = value;
            }
        }

        public int Capacity
        {
            get { return this.capacity; }

            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.CapacityNegative));
                }

                this.capacity = value;
            }
        }

        public IReadOnlyCollection<int> RequiredSubjects { get; private set; }
    }
}
