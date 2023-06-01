namespace DatabaseExtended.Tests
{
    using ExtendedDatabase;
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class ExtendedDatabaseTests
    {
        [Test]
        public void TestingConstructorShouldWorkProperly()
        {
            Person p1 = new Person(123, "Nikol");
            Person p2 = new Person(12345, "Ivana");

            Database database = new Database(p1, p2);
            int expectedCount = 2;
            int actualCount = database.Count;
            Assert.AreEqual(expectedCount, actualCount);
        }

        [Test]
        public void TestingConstructorShouldThrowException()
        {
            Person[] people = new Person[17];

            for (int i = 0; i < 17; i++)
            {
                people[i] = new Person(i, $"Name{i}");
            }

            Assert.Throws<ArgumentException>(() =>
            {
                Database database = new Database(people);
            }, "Provided data length should be in range [0..16]!");
        }

        [Test]
        public void AddShouldWorkProperly()
        {
            Person p1 = new Person(123, "Nikol");
            Person p2 = new Person(12345, "Ivana");
            Database database = new Database(p1, p2);

            Person p3 = new Person(1234, "Jerry");
            database.Add(p3);

            int expectedCount = 3;
            int actualCount = database.Count;
            Assert.AreEqual(expectedCount, actualCount);
        }

        [Test]
        public void AddAboveFullCapacityShouldThrowException()
        {
            Database database = new Database();

            Assert.Throws<InvalidOperationException>(() => 
            {
                for (int i = 0; i < 17; i++)
                {
                    Person person = new Person(i, $"Name{i}");
                    database.Add(person);
                }
            }, "Array's capacity must be exactly 16 integers!");
        }

        [Test]
        public void AddTwoPeopleWithSameNameShouldThrowException()
        {
            Person p1 = new Person(123, "Nikol");
            Person p2 = new Person(12345, "Nikol");

            Database database = new Database(p1);

            Assert.Throws<InvalidOperationException>(() =>
            {
                database.Add(p2);
            }, "There is already user with this username!");
        }

        [Test]
        public void AddTwoPeopleWithSameIdShouldThrowException()
        {
            Person p1 = new Person(123, "Nikolina");
            Person p2 = new Person(123, "Nikol");

            Database database = new Database(p1);

            Assert.Throws<InvalidOperationException>(() =>
            {
                database.Add(p2);
            }, "There is already user with this Id!");
        }

        [Test]
        public void RemoveShouldWorkProperly()
        {
            Person[] people = new Person[16];

            for (int i = 0; i < 16; i++)
            {
                people[i] = new Person(i, $"Name{i}");
            }

            Database database = new Database(people);

            database.Remove();
            database.Remove();
            database.Remove();

            int expectedCount = people.Length - 3;
            int actualCount = database.Count;

            Assert.AreEqual(expectedCount, actualCount);
        }

        [Test]
        public void RemoveFromEmptyDBShouldThrowException()
        {
            Person p1 = new Person(123, "Nikol");
            Person p2 = new Person(12345, "Nikolina");

            Database database = new Database(p1, p2);

            Assert.Throws<InvalidOperationException>(() =>
            {
                for (int i = 1; i <= 3; i++)
                {
                    database.Remove();
                }
            });
        }

        [Test]
        public void FindByUsernameShouldWorkProperly()
        {
            Person p1 = new Person(123, "Nikol");
            Person p2 = new Person(12345, "Nikolina");
            Person p3 = new Person(555, "Siana");

            Database database = new Database(p1, p2, p3);
            Person personToFind = database.FindByUsername("Nikol");

            Assert.AreEqual(p1.UserName, personToFind.UserName);
        }

        [Test]
        public void FindByUserNameShouldThrowExceptionMissingName()
        {
            Person p1 = new Person(123, "Nikol");

            Database database = new Database(p1);

            Assert.Throws<InvalidOperationException>(() =>
            {
                Person personToFind = database.FindByUsername("Ivana");
            }, "No user is present by this username!");
        }

        [TestCase(null)]
        [TestCase("")]
        public void FindByUserNameShouldThrowExceptionNullOrEmpty(string name)
        {
            Person p1 = new Person(123, "Nikol");

            Database database = new Database(p1);

            Assert.Throws<ArgumentNullException>(() =>
            {
                Person personToFind = database.FindByUsername(name);
            });
        }

        [Test]
        public void FindByIDShouldWorkProperly()
        {
            Person p1 = new Person(123, "Nikol");
            Person p2 = new Person(12345, "Nikolina");
            Person p3 = new Person(555, "Siana");

            Database database = new Database(p1, p2, p3);
            Person personToFind = database.FindById(123);

            Assert.AreEqual(p1.Id, personToFind.Id);
        }

        [Test]
        public void FindByIDShouldThrowExceptionMissingID()
        {
            Person p1 = new Person(123, "Nikol");

            Database database = new Database(p1);

            Assert.Throws<InvalidOperationException>(() =>
            {
                database.FindById(12345);
            });
        }


        [Test]
        public void FindByIDShouldThrowExceptionNegativeID()
        {
            Person p1 = new Person(123, "Nikol");

            Database database = new Database(p1);

            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                database.FindById(-12345);
            });
        }
    }
}