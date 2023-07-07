using NUnit.Framework;
using System;
using System.Numerics;
using System.Xml.Linq;

namespace FootballTeam.Tests
{
    public class Tests
    {
        private FootballTeam team;

        private FootballPlayer player1;
        private FootballPlayer player2;
        private FootballPlayer player3;

        [SetUp]
        public void Setup()
        {
            this.team = new FootballTeam("Bulgaria", 15);
            this.player1 = new FootballPlayer("Stoichkov", 1, "Forward");
            this.player2 = new FootballPlayer("Berbatov", 2, "Forward");
            this.player3 = new FootballPlayer("Bojinov", 3, "Forward");
        }

        [TestCase(null)]
        [TestCase("")]
        public void TestingConstructorNameWithNullOrEmptyStringShouldThrow(string name)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                FootballTeam footballTeam = new FootballTeam(name, 15);
            });
        }

        [TestCase("B")]
        [TestCase("Bulgaria")]
        [TestCase("BulgariaBulgariaBulgariaBulgariaBulgariaBulgaria")]
        public void TestingConstructorNameShouldWorkFine(string name)
        {
            FootballTeam footballTeam = new FootballTeam(name, 15);
            Assert.AreEqual(name, footballTeam.Name);
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(14)]
        public void TestingCapacityPropertyWithLessThan15sHouldThrow(int capacity)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                FootballTeam footballTeam = new FootballTeam("Bulgaria", capacity);
            });
        }

        [TestCase(15)]
        [TestCase(100)]
        [TestCase(10000)]
        public void TestingCapacityPropertyShouldWorkFine(int capacity)
        {
            FootballTeam footballTeam = new FootballTeam("Bulgaria", capacity);

            Assert.AreEqual(capacity, footballTeam.Capacity);
        }

        [Test]
        public void ConstructorShouldInitializeInternalCollectionPlayers()
        {
            FootballTeam footballTeam = new FootballTeam("Bulgaria", 25);

            Assert.IsNotNull(footballTeam.Players);
        }

        [Test]
        public void AddPlayerShouldReturnProperMessage()
        {
            string expectedMessage = $"Added player {this.player1.Name} in position {this.player1.Position} with number {this.player1.PlayerNumber}";
            string actualMessage = this.team.AddNewPlayer(this.player1);

            Assert.AreEqual(expectedMessage, actualMessage);
        }

        [Test]
        public void AddPlayerShouldAddPlayersToPlayersCollection()
        {
            this.team.AddNewPlayer(this.player1);
            this.team.AddNewPlayer(this.player2);
            this.team.AddNewPlayer(this.player3);

            int expectedCount = 3;
            int actualCount = this.team.Players.Count;

            Assert.AreEqual(expectedCount, actualCount);
        }

        [Test]
        public void AddingPlayersOverCapacityShoudNotAddThemAndReturnProperMessage()
        {
            string position = "Forward";

            for (int i = 1; i <= 15; i++)
            {
                FootballPlayer player = new FootballPlayer(i.ToString(), i, position);
                this.team.AddNewPlayer(player);
            }

            string expectedMessage = "No more positions available!";
            string actualMessage = this.team.AddNewPlayer(this.player1);

            int expectedCount = 15;
            int actualCount = this.team.Players.Count;

            Assert.AreEqual(expectedMessage, actualMessage);

            Assert.AreEqual(expectedCount, actualCount);
        }

        [Test]
        public void PickPlayerShouldFindPlayerWithGivenName()
        {
            this.team.AddNewPlayer(this.player1);
            this.team.AddNewPlayer(this.player2);
            this.team.AddNewPlayer(this.player3);

            FootballPlayer playerFound = this.team.PickPlayer("Stoichkov");
            string expectedName = "Stoichkov";
            string actualName = playerFound.Name;

            Assert.AreEqual(expectedName, actualName);
        }

        [Test]
        public void PickNonExistingPlayerShouldReturnNull()
        {
            this.team.AddNewPlayer(this.player1);
            this.team.AddNewPlayer(this.player2);
            this.team.AddNewPlayer(this.player3);

            FootballPlayer playerFound = this.team.PickPlayer("Pesho");

            Assert.IsNull(playerFound);
        }

        [Test]
        public void PlayerScoreMethodShouldReturnProperMessageWhenPlayerScores()
        {
            this.team.AddNewPlayer(this.player1);
            this.team.AddNewPlayer(this.player2);
            this.team.AddNewPlayer(this.player3);

            string actualMessage = this.team.PlayerScore(1);
            string expectedMessage = $"{this.player1.Name} scored and now has {this.player1.ScoredGoals} for this season!";

            Assert.AreEqual(expectedMessage, actualMessage);
        }

        [Test]
        public void PlayerScoreMethodShouldIncreasePlayerScoredGoals()
        {
            this.team.AddNewPlayer(this.player1);
            this.team.AddNewPlayer(this.player2);
            this.team.AddNewPlayer(this.player3);

            this.team.PlayerScore(1);
            this.team.PlayerScore(1);
            this.team.PlayerScore(1);

            int expectedGoalsScored = 3;
            int actualGoalsScored = this.player1.ScoredGoals;

            Assert.AreEqual(expectedGoalsScored, actualGoalsScored);
        }
    }
}