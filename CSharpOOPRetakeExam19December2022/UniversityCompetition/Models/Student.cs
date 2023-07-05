using System;
using System.Collections.Generic;
using UniversityCompetition.Models.Contracts;
using UniversityCompetition.Utilities.Messages;

namespace UniversityCompetition.Models
{
    public class Student : IStudent
    {
        private string firstName;
        private string lastName;
        private ICollection<int> coveredExams;

        public Student(int studentId, string firstName, string lastName)
        {
            this.Id = studentId;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.coveredExams = new List<int>();
        }

        public int Id { get; private set; }

        public string FirstName
        {
            get { return this.firstName; }

            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.NameNullOrWhitespace));
                }

                this.firstName = value;
            }
        }

        public string LastName
        {
            get { return this.lastName; }

            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.NameNullOrWhitespace));
                }

                this.lastName = value;
            }
        }

        public IReadOnlyCollection<int> CoveredExams 
            => (IReadOnlyCollection<int>)this.coveredExams;

        public IUniversity University { get; private set; }

        public void CoverExam(ISubject subject)
        {
            this.coveredExams.Add(subject.Id);
        }

        public void JoinUniversity(IUniversity university)
        {
            this.University = university;
        }
    }
}
