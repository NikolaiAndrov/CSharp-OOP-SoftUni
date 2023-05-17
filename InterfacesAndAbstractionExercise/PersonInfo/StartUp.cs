namespace PersonInfo
{
    public class StartUp
    {
        public static void Main()
        {
            var name = Console.ReadLine();
            var age = int.Parse(Console.ReadLine());
            var id = Console.ReadLine();
            var birthdate = Console.ReadLine();

            IIdentifiable person = new Citizen(name, age, id, birthdate);
           // IPerson person = new Citizen(name, age, id, birthdate);
           // IBirthable person = new Citizen(name, age, id, birthdate);
           // Citizen person = new Citizen(name, age, id, birthdate);

            Console.WriteLine(person.Name);
            Console.WriteLine(person.Age);
            Console.WriteLine(person.Id);
           // Console.WriteLine(person.Birthdate);

        }
    }
}