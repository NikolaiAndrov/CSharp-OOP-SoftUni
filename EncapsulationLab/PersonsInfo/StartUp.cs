namespace PersonsInfo
{
    public class StartUp
    {
        public static void Main()
        {
            var n = int.Parse(Console.ReadLine());
            var people = new List<Person>();

            for (int i = 0; i < n; i++)
            {
                try
                {
                    var personInfo = Console.ReadLine()
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries);
                    var firstName = personInfo[0];
                    var lastName = personInfo[1];
                    var age = int.Parse(personInfo[2]);
                    var salary = decimal.Parse(personInfo[3]);
                    var person = new Person(firstName, lastName, age, salary);
                    people.Add(person);
                }
                catch (Exception e)
                {

                    Console.WriteLine(e.Message);
                }
                
            }

            Team team = new Team("SoftUni");

            foreach (var person in people)
            {
                team.AddPlayer(person);
            }

            Console.WriteLine(team);
        }
    }
}