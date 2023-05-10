namespace Animals
{
    using System;
    using System.Collections.Generic;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            List<Animal> animals = new List<Animal>();

            string animalType;

            while ((animalType = Console.ReadLine()) != "Beast!")
            {
                try
                {
                    Animal animal = null;

                    string[] animalInfo = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
                    string name = animalInfo[0];
                    int age = int.Parse(animalInfo[1]);
                    string gender = animalInfo[2];

                    if (animalType == "Dog")
                    {
                        animal = new Dog(name, age, gender);
                    }
                    else if (animalType == "Cat")
                    {
                        animal = new Cat(name, age, gender);
                    }
                    else if (animalType == "Frog")
                    {
                        animal = new Frog(name, age, gender);
                    }
                    else if (animalType == "Tomcat")
                    {
                        animal = new Tomcat(name, age);
                    }
                    else if (animalType == "Kitten")
                    {
                        animal = new Kitten(name, age);
                    }

                    animals.Add(animal);
                }
                catch (Exception)
                {

                    Console.WriteLine("Invalid input!");
                }
            }

            foreach (Animal animal in animals)
            {
                Console.WriteLine(animal);
            }
        }
    }
}
