namespace Book.Tests
{
    using System;

    using NUnit.Framework;
    using static System.Net.Mime.MediaTypeNames;

    [TestFixture]
    public class Tests
    {
        Book book;
        
        [SetUp]
        public void SetUp()
        {
            book = new Book("Book", "Author");
        }

        [TestCase("")]
        [TestCase(null)]
        public void EmptyOrNullBookNameShouldThrow(string name)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Book book = new Book(name, "Author");
            });
        }

        [TestCase("Name")]
        [TestCase("Name Name Name Name Name Name Name Name")]
        [TestCase("N")]
        public void ConstructorShoudSetNameCorrectly(string name)
        {
            Book book = new Book(name, "Author");

            Assert.AreEqual(name, book.BookName);
        }

        [TestCase(null)]
        [TestCase("")]
        public void NullOrEmptyAuthorNameShouldThrow(string authorName)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Book book = new Book("Book", authorName);
            });
        }

        [TestCase("Name")]
        [TestCase("Name Name Name Name Name Name Name Name Name")]
        [TestCase("N")]
        public void AuthorNameShoudBeSetProperly(string authorName)
        {
            Book book = new Book("Book", authorName);
            Assert.AreEqual(authorName, book.Author);
        }

        [Test]
        public void ConstructorShouldInitializeInternalCollectionWithCountZero()
        {
            int countUponInitialization = 0;

            Assert.AreEqual(countUponInitialization, book.FootnoteCount);
        }

        [Test]
        public void AddFootNoteShouldIncreaseCount()
        {
            book.AddFootnote(1, "First");
            book.AddFootnote(2, "Second");

            int excpectedCount = 2;

            Assert.AreEqual(excpectedCount, book.FootnoteCount);
        }

        [Test]
        public void AddSameFootnoteShoudThrow()
        {
            book.AddFootnote(1, "First");

            Assert.Throws<InvalidOperationException>(() =>
            {
                book.AddFootnote(1, "First");
            });
        }

        [Test]
        public void AddFootNoteWithSameNumberShouldThrow()
        {
            book.AddFootnote(1, "First");

            Assert.Throws<InvalidOperationException>(() =>
            {
                book.AddFootnote(1, "Second");
            });
        }

        [Test]
        public void FindNonExistingFootNoteShouldThrow()
        {
            book.AddFootnote(1, "First");
            book.AddFootnote(2, "Second");

            Assert.Throws<InvalidOperationException>(() =>
            {
                book.FindFootnote(3);
            });
        }

        [Test]
        public void FindFootNoteShouldReturnCorrecteMessage()
        {
            book.AddFootnote(1, "First");
            book.AddFootnote(2, "Second");

            string expectedMessage = $"Footnote #1: First";
            string actualMessage = book.FindFootnote(1);

            Assert.AreEqual(expectedMessage, actualMessage);
        }

        [Test]
        public void AlterFootnoteWithNonExistingFootNoteShouldThrow()
        {
            book.AddFootnote(1, "First");
            book.AddFootnote(2, "Second");

            Assert.Throws<InvalidOperationException>(() =>
            {
                book.AlterFootnote(3, "New Text");
            });
        }


        [Test]
        public void AlterFootnoteShouldChangeTextOfGivenNote()
        {
            book.AddFootnote(1, "First");
            book.AddFootnote(2, "Second");

            book.AlterFootnote(1, "New Text");

            string expectedMessage = $"Footnote #1: New Text";
            string actualMessage = book.FindFootnote(1);

            Assert.AreEqual(expectedMessage, actualMessage);
        }
    }
}