using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversityCompetition.Core.Contracts;
using UniversityCompetition.Models;
using UniversityCompetition.Models.Contracts;
using UniversityCompetition.Repositories;
using UniversityCompetition.Repositories.Contracts;
using UniversityCompetition.Utilities.Messages;

namespace UniversityCompetition.Core
{
    public class Controller : IController
    {
        private readonly IRepository<ISubject> subjects;
        private readonly IRepository<IStudent> students;
        private readonly IRepository<IUniversity> universities;
        public Controller() 
        {
            this.subjects = new SubjectRepository();
            this.students = new StudentRepository();
            this.universities = new UniversityRepository();
        }

        public string AddSubject(string subjectName, string subjectType)
        {
            ISubject subject;

            if (subjectType == "TechnicalSubject")
            {
                subject = new TechnicalSubject(this.subjects.Models.Count + 1, subjectName);
            }
            else if (subjectType == "EconomicalSubject")
            {
                subject = new EconomicalSubject(this.subjects.Models.Count + 1, subjectName);
            }
            else if (subjectType == "HumanitySubject")
            {
                subject = new HumanitySubject(this.subjects.Models.Count + 1, subjectName);
            }
            else
            {
                return string.Format(OutputMessages.SubjectTypeNotSupported, subjectType);
            }

            ISubject existingSubject = this.subjects.FindByName(subjectName);

            if (existingSubject != null)
            {
                return string.Format(OutputMessages.AlreadyAddedSubject, subjectName);
            }

            this.subjects.AddModel(subject);

            return string.Format(OutputMessages.SubjectAddedSuccessfully, subjectType, subjectName, this.subjects.GetType().Name);
        }

        public string AddUniversity(string universityName, string category, int capacity, List<string> requiredSubjects)
        {
            IUniversity university = this.universities.FindByName(universityName);

            if (university != null)
            {
                return string.Format(OutputMessages.AlreadyAddedUniversity, universityName);
            }

            ICollection<int> requiredSubjectsIds = new List<int>();

            foreach (var subject in requiredSubjects)
            {
                ISubject subjectFound = this.subjects.FindByName(subject);

                if (subjectFound != null)
                {
                    requiredSubjectsIds.Add(subjectFound.Id);
                }
            }

            university = new University(this.universities.Models.Count + 1, universityName, category, capacity, requiredSubjectsIds);
            this.universities.AddModel(university);

            return string.Format(OutputMessages.UniversityAddedSuccessfully, universityName, this.universities.GetType().Name);
        }

        public string AddStudent(string firstName, string lastName)
        {
            string fullName = $"{firstName} {lastName}";
            IStudent student = this.students.FindByName(fullName);

            if (student != null)
            {
                return string.Format(OutputMessages.AlreadyAddedStudent, firstName, lastName);
            }

            student = new Student(this.students.Models.Count + 1, firstName, lastName);
            this.students.AddModel(student);
            return string.Format(OutputMessages.StudentAddedSuccessfully, firstName, lastName, this.students.GetType().Name);
        }

        public string TakeExam(int studentId, int subjectId)
        {
            IStudent student = this.students.FindById(studentId);
            ISubject subject = this.subjects.FindById(subjectId);

            if (student == null)
            {
                return string.Format(OutputMessages.InvalidStudentId);
            }

            if (subject == null)
            {
                return string.Format(OutputMessages.InvalidSubjectId);
            }

            IReadOnlyCollection<int> coveredExams = student.CoveredExams;

            if (coveredExams.Contains(subjectId))
            {
                return string.Format(OutputMessages.StudentAlreadyCoveredThatExam, student.FirstName, student.LastName, subject.Name);
            }

            student.CoverExam(subject);

            return string.Format(OutputMessages.StudentSuccessfullyCoveredExam, student.FirstName, student.LastName, subject.Name);
        }

        public string ApplyToUniversity(string studentName, string universityName)
        {
            string[] fullName = studentName.Split();
            string firstName = fullName[0];
            string lastName = fullName[1];

            IStudent student = this.students.FindByName(studentName);
            IUniversity university = this.universities.FindByName(universityName);

            if (student == null)
            {
                return string.Format(OutputMessages.StudentNotRegitered, firstName, lastName);
            }

            if (university == null)
            {
                return string.Format(OutputMessages.UniversityNotRegitered, universityName);
            }

            ICollection<int> studentCoveredExams = student.CoveredExams.OrderBy(x => x).ToHashSet();
            ICollection<int> requiredUniversityExams = university.RequiredSubjects.OrderBy(x => x).ToHashSet();

            bool allExamsCovered = true;

            foreach (var exam in requiredUniversityExams)
            {
                if (!studentCoveredExams.Contains(exam))
                {
                    allExamsCovered = false;
                    break;
                }
            }

            if (!allExamsCovered)
            {
                return string.Format(OutputMessages.StudentHasToCoverExams, studentName, universityName);
            }

            IUniversity studentUniversity = student.University;

            if (studentUniversity != null && studentUniversity.Name == university.Name && studentUniversity.Id == university.Id)
            {
                return string.Format(OutputMessages.StudentAlreadyJoined, student.FirstName, student.LastName, university.Name);
            }

            int studentsAdmitted = StudentsAdmitted(university);
            if (studentsAdmitted == university.Capacity)
            {
                return null;
            }

            student.JoinUniversity(university);

            return string.Format(OutputMessages.StudentSuccessfullyJoined, student.FirstName, student.LastName, university.Name);
        }

        public string UniversityReport(int universityId)
        {
            IUniversity university = this.universities.FindById(universityId);

            if (university == null)
            {
                return null;
            }

            int studentsAdmitted = StudentsAdmitted(university);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"*** {university.Name} ***");
            sb.AppendLine($"Profile: {university.Category}");
            sb.AppendLine($"Students admitted: {studentsAdmitted}");
            sb.AppendLine($"University vacancy: {university.Capacity - studentsAdmitted}");
            return sb.ToString().TrimEnd();
        }

        private int StudentsAdmitted(IUniversity university)
        {
            IReadOnlyCollection<IStudent> allStudents = this.students.Models;

            int studentsAdmitted = 0;

            foreach (IStudent student in allStudents)
            {
                if (student.University == university)
                {
                    studentsAdmitted++;
                }
            }

            return studentsAdmitted;
        }
    }
}
