using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

[TestFixture]
public class HeroRepositoryTests
{
    HeroRepository heroRepository;
    Hero hero1;
    Hero hero2;
    Hero hero3;

    [SetUp]
    public void SetUp()
    {
        heroRepository = new HeroRepository();
        hero1 = new Hero("hero1", 1);
        hero2 = new Hero("hero2", 2);
        hero3 = new Hero("hero3", 3);
    }

    [Test]
    public void ConstructorShouldInitializeHeroesCollection()
    {
        Assert.IsNotNull(heroRepository.Heroes);
    }

    [Test]
    public void HeroCollectionShouldBeWithCountZeroUponInitialization()
    {
        int expectedCoun = 0;
        Assert.AreEqual(expectedCoun, heroRepository.Heroes.Count);
    }

    [Test]
    public void CreateHeroWuthNullShouldThrow()
    {
        Hero hero = null;

        Assert.Throws<ArgumentNullException>(() =>
        {
            heroRepository.Create(hero);
        }, $"{nameof(hero)}, Hero is null");
    }

    [Test]
    public void CreateHeroShouldWorkProperlyAndReturnProperMessage()
    {
        string expectedMessage = $"Successfully added hero {hero1.Name} with level {hero1.Level}";
        string actualMessage = heroRepository.Create(hero1);

        Assert.AreEqual(expectedMessage, actualMessage);
    }

    [Test]
    public void CreateHeroShuldWorkCorrectlyAndAddHeroesToCollection()
    {
        heroRepository.Create(hero1);
        heroRepository.Create(hero2);
        heroRepository.Create(hero3);

        int expectedCount = 3;

        Assert.AreEqual(expectedCount, heroRepository.Heroes.Count);
    }
    

    [Test]
    public void CreateSameHeroSecondTimeShouldThrow()
    {
        heroRepository.Create(hero1);
        heroRepository.Create(hero2);
        heroRepository.Create(hero3);

        Assert.Throws<InvalidOperationException>(() =>
        {
            heroRepository.Create(hero2);
        }, $"Hero with name {hero2.Name} already exists");
    }

    [Test]
    public void CreateHeroWithSameNameSecondTimeShouldThrow()
    {
        Hero hero = new Hero("hero2", 100);

        heroRepository.Create(hero1);
        heroRepository.Create(hero2);
        heroRepository.Create(hero3);

        Assert.Throws<InvalidOperationException>(() =>
        {
            heroRepository.Create(hero);
        }, $"Hero with name {hero.Name} already exists");
    }

    [TestCase(null)]
    [TestCase("")]
    [TestCase("   ")]
    public void RemoveNullEmptyOrWhiteSpaceShouldThrow(string name)
    {
        Assert.Throws<ArgumentNullException>(() =>
        {
            heroRepository.Remove(name);
        }, $"{nameof(name)} Name cannot be null");
    }

    [Test]
    public void RemoveShouldRemoveHeroAndReturnTrue()
    {
        heroRepository.Create(hero1);
        heroRepository.Create(hero2);
        heroRepository.Create(hero3);

        Assert.IsTrue(heroRepository.Remove(hero3.Name));
    }

    [Test]
    public void RemoveShouldRemoveHeroAndDecreaseCount()
    {
        heroRepository.Create(hero1);
        heroRepository.Create(hero2);
        heroRepository.Create(hero3);

        heroRepository.Remove(hero3.Name);

        int expectedCount = 2;

        Assert.AreEqual(expectedCount, heroRepository.Heroes.Count);
    }

    [Test]
    public void RemoveNonExistingHeroShouldReturnFalse()
    {
        heroRepository.Create(hero1);
        heroRepository.Create(hero2);
        heroRepository.Create(hero3);

        Assert.IsFalse(heroRepository.Remove("some hero"));
    }

    [Test]
    public void GetHeroWithHighestLevelShouldWorkCorrectly()
    {
        heroRepository.Create(hero3);
        heroRepository.Create(hero1);
        heroRepository.Create(hero2);

        Hero hero = heroRepository.GetHeroWithHighestLevel();

        Assert.AreEqual(hero3, hero);
        Assert.AreEqual(hero3.Name, hero.Name);
        Assert.AreEqual(hero3.Level, hero.Level);
    }

    [Test]
    public void GetHeroWithNonExistingNameShouldReturnNull()
    {
        heroRepository.Create(hero3);
        heroRepository.Create(hero1);
        heroRepository.Create(hero2);

        Hero hero = heroRepository.GetHero("some hero");
        Assert.IsNull(hero);
    }

    [Test]
    public void GetHeroShouldReturnHeroWithGivenName()
    {
        heroRepository.Create(hero1);
        heroRepository.Create(hero2);
        heroRepository.Create(hero3);

        Hero hero = heroRepository.GetHero("hero1");

        Assert.AreEqual(hero1, hero);
        Assert.AreEqual(hero1.Name, hero.Name);
        Assert.AreEqual(hero1.Level, hero.Level);
    }

    [Test]
    public void HeroesShouldReturnExpectedCollection()
    {
        ICollection<Hero> expectedHeroes = new List<Hero>();
        expectedHeroes.Add(hero1);
        expectedHeroes.Add(hero2);
        expectedHeroes.Add(hero3);

        heroRepository.Create(hero1);
        heroRepository.Create(hero2);
        heroRepository.Create(hero3);

        CollectionAssert.AreEqual(expectedHeroes, heroRepository.Heroes);
    }
}