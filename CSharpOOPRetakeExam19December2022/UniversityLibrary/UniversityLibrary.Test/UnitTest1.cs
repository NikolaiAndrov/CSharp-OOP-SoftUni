namespace UniversityLibrary.Test
{
    using NUnit.Framework;
    using System.Collections;
    using System.Collections.Generic;

    public class Tests
    {
        UniversityLibrary universityLibrary;
        TextBook textBook1;
        TextBook textBook2;
        TextBook textBook3;

        [SetUp]
        public void Setup()
        {
            universityLibrary = new UniversityLibrary();
            textBook1 = new TextBook("title1", "author1", "category1");
            textBook2 = new TextBook("title2", "author2", "category2");
            textBook3 = new TextBook("title3", "author3", "category3");
        }

        [Test]
        public void UniversityLibraryConstructorShouldInitiaalizeTextBookCollection()
        {
            Assert.IsNotNull(universityLibrary.Catalogue);
        }

        [Test]
        public void AddBookShouldWorkFine()
        {
            universityLibrary.AddTextBookToLibrary(textBook1);
            universityLibrary.AddTextBookToLibrary(textBook2);
            universityLibrary.AddTextBookToLibrary(textBook3);

            ICollection<TextBook> collection = universityLibrary.Catalogue;

            int expectedCount = 3;
            int actualCount = collection.Count;

            Assert.AreEqual(expectedCount, actualCount);
        }

        [Test]
        public void AddBookShouldReturnProperMessage()
        {
            string actualOutput = universityLibrary.AddTextBookToLibrary(textBook1);
            textBook1.InventoryNumber = 1;
            string expectedOutput = textBook1.ToString();

            bool areEqual = string.Equals(expectedOutput, actualOutput);
            Assert.IsTrue(areEqual);
        }

        [Test]
        public void LoanTextBookShouldWorkFineAndReturnProperMessage()
        {
            universityLibrary.AddTextBookToLibrary(textBook1);
            universityLibrary.AddTextBookToLibrary(textBook2);
            universityLibrary.AddTextBookToLibrary(textBook3);

            string expectedOutput = $"{textBook1.Title} loaned to Pesho.";
            string actualOutput = universityLibrary.LoanTextBook(1, "Pesho");

            Assert.AreEqual(expectedOutput, actualOutput);
            Assert.AreEqual("Pesho", textBook1.Holder);
        }

        [Test]
        public void LoanTextBookMoreThanOneToTheSameStudentShouldReturnProperMessageAndNotLoanBook()
        {
            universityLibrary.AddTextBookToLibrary(textBook1);
            universityLibrary.AddTextBookToLibrary(textBook2);
            universityLibrary.AddTextBookToLibrary(textBook3);

            universityLibrary.LoanTextBook(1, "Pesho");

            string expectedOutput = $"Pesho still hasn't returned {textBook1.Title}!";
            string actualOutput = universityLibrary.LoanTextBook(1, "Pesho");

            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [Test]
        public void ReturnTextBookShouldWorkFineAndReturnProperMessage()
        {
            universityLibrary.AddTextBookToLibrary(textBook1);
            universityLibrary.AddTextBookToLibrary(textBook2);
            universityLibrary.AddTextBookToLibrary(textBook3);

            universityLibrary.LoanTextBook(1, "Pesho");

            string expectedOutput = $"{textBook1.Title} is returned to the library.";
            string actualOutput = universityLibrary.ReturnTextBook(1);

            Assert.AreEqual(expectedOutput, actualOutput);
            Assert.AreEqual(string.Empty, textBook1.Holder);
        }
    }
}