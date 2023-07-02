using NUnit.Framework;

namespace RobotFactory.Tests
{
    public class Tests
    {
        Factory factory;
        

        [SetUp]
        public void Setup()
        {
            factory = new Factory("Factory", 3);
        }

        [Test]
        public void ConstructorShouldSetNameCorrectly()
        {
            string expectedName = "Factory";

            Assert.AreEqual(expectedName, factory.Name);
        }

        [Test]
        public void ConstructorShouldSetCapacityCorrectly() 
        {
            int expectedCapacity = 3;

            Assert.AreEqual(expectedCapacity, factory.Capacity);
        }

        [Test]
        public void ConstructorShouldInitializeRobotsCollectionCorrectly()
        {
            Assert.IsNotNull(factory.Robots);
        }

        [Test]
        public void ConstructorShouldInitializeSupplementsCollectionCorrectly()
        {
            Assert.IsNotNull(factory.Supplements);
        }

        [Test]
        public void NamePropertyShouldChangeTheNameOfTheFactory()
        {
            string newName = "NewName";
            factory.Name = newName;
            Assert.AreEqual(newName, factory.Name);
        }

        [Test]
        public void CapacityPropertyShouldChangeTheCapacityOfTheFactory()
        {
            int expectedCapacity = 5;
            factory.Capacity = expectedCapacity;

            Assert.AreEqual(expectedCapacity, factory.Capacity);
        }

        [Test]
        public void FactoryShouldProduceRobotCorrectly()
        {
            Robot robot = new Robot("model1", 100, 1306);

            string actualOutput = factory.ProduceRobot("model1", 100, 1306);
            string expectedOutput = $"Produced --> {robot}";

            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [Test]
        public void ProducingRobotsShouldAddThemToRobotsCollection()
        {
            factory.ProduceRobot("model1", 100, 1306);
            factory.ProduceRobot("model2", 100, 1306);
            factory.ProduceRobot("model3", 100, 1306);

            int expectedCount = 3;

            Assert.AreEqual(expectedCount, factory.Robots.Count);
        }

        [Test]
        public void ProduceingRobotsShoulAddTheCorrectRobotToCollection()
        {
            string model = "robot";
            double price = 100;
            int interfaceStandart = 9106;

            factory.ProduceRobot(model, price, interfaceStandart);
            Robot robot = factory.Robots[0];

            Assert.AreEqual(model, robot.Model);
            Assert.AreEqual(price, robot.Price);
            Assert.AreEqual(interfaceStandart, robot.InterfaceStandard);
        }

        [Test]
        public void ProducingRobotsMoreThanCapacityShouldReturnProperMessage()
        {
            factory.ProduceRobot("model1", 100, 1306);
            factory.ProduceRobot("model2", 100, 1306);
            factory.ProduceRobot("model3", 100, 1306);

            string expectedOutput = factory.ProduceRobot("model3", 100, 1306);
            string actualOutput = "The factory is unable to produce more robots for this production day!";

            int expectedCount = 3;

            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [Test]
        public void ProducingRobotsMoreThanCapacityShouldNotAddThemToTheFactory()
        {
            factory.ProduceRobot("model1", 100, 1306);
            factory.ProduceRobot("model2", 100, 1306);
            factory.ProduceRobot("model3", 100, 1306);
            factory.ProduceRobot("model3", 100, 1306);

            int expectedCount = 3;
            int actualCount = factory.Robots.Count;

            Assert.AreEqual(expectedCount, actualCount);
        }

        [Test]
        public void ProduceSupplementMethodShoulReturnProperMessage()
        {
            Supplement supplement = new Supplement("suplement", 1306);

            string expectedMessage = supplement.ToString();
            string actualMessage = factory.ProduceSupplement("suplement", 1306);

            Assert.AreEqual(expectedMessage, actualMessage);
        }

        [Test]
        public void ProduceSupplementMethodShouldAddSupplementsToSupplementsCollection()
        {
            factory.ProduceSupplement("suplement1", 1306);
            factory.ProduceSupplement("suplement2", 1306);
            factory.ProduceSupplement("suplement3", 1306);

            int expectedCount = 3;

            Assert.AreEqual(expectedCount, factory.Supplements.Count);
        }

        [Test]
        public void ProduceSupplementMethodShouldCreatedTheEcpectedSupplement()
        {
            string name = "supplement";
            int interfaceStandard = 9106;

            factory.ProduceSupplement(name, interfaceStandard);
            Supplement supplement = factory.Supplements[0];

            Assert.AreEqual(name, supplement.Name);
            Assert.AreEqual(interfaceStandard, supplement.InterfaceStandard);
        }

        [Test]
        public void UpgradeRobotWithExistingSupplementShouldReturnFalse()
        {
            factory.ProduceRobot("robot", 100, 9106);
            factory.ProduceSupplement("supplement", 9106);

            Robot robot = factory.Robots[0];
            Supplement supplement = factory.Supplements[0];
            robot.Supplements.Add(supplement);

            Assert.IsFalse(factory.UpgradeRobot(robot, supplement));
        }

        [Test]
        public void UpgradeRobotWithDifferentInterfaceStandardShouldReturnFalse()
        {
            factory.ProduceRobot("robot", 100, 9106);
            factory.ProduceSupplement("supplement", 9100);

            Robot robot = factory.Robots[0];
            Supplement supplement = factory.Supplements[0];

            Assert.IsFalse(factory.UpgradeRobot(robot, supplement));
        }

        [Test]
        public void UpgradeRobotShouldWorkFine()
        {
            factory.ProduceRobot("robot", 100, 9106);
            factory.ProduceSupplement("supplement", 9106);

            Robot robot = factory.Robots[0];
            Supplement supplement = factory.Supplements[0];

            Assert.IsTrue(factory.UpgradeRobot(robot, supplement));
        }

        [Test]
        public void UpgradeRobotShouldWorkFineAndTheSupplementShouldBeTheSame()
        {
            string supplementName = "supplement";
            int supplementInterface = 9106;

            factory.ProduceRobot("robot", 100, 9106);
            factory.ProduceSupplement(supplementName, supplementInterface);

            Robot robot = factory.Robots[0];
            Supplement supplement = factory.Supplements[0];
            factory.UpgradeRobot(robot, supplement);

            Supplement supplement1 = robot.Supplements[0];

            Assert.AreEqual(supplementName, supplement1.Name);
            Assert.AreEqual(supplementInterface, supplement1.InterfaceStandard);
        }

        [Test]
        public void SellRobotShouldReturnTheExactRobot()
        {
            string model = "robot";
            double price = 99;
            int interfaceStandart = 1306;
            factory.ProduceRobot(model, price, interfaceStandart);
            factory.ProduceRobot("model2", 1000, 1306);
            factory.ProduceRobot("model3", 1000, 1306);

            Robot robot = factory.SellRobot(100);

            Assert.AreEqual(model, robot.Model);
            Assert.AreEqual(price, robot.Price);
            Assert.AreEqual(interfaceStandart, robot.InterfaceStandard);
        }


        [Test]
        public void SellRobotShouldReturnNull()
        {
            factory.ProduceRobot("model1", 10000, 1306);
            factory.ProduceRobot("model2", 1000, 1306);
            factory.ProduceRobot("model3", 1000, 1306);

            Robot robot = factory.SellRobot(100);

            Assert.IsNull(robot);
        }
    }
}