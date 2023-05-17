namespace Telephony.Core
{
    using Interfaces;
    using Telephony.IO.Interfaces;
    using Telephony.Models;
    using Telephony.Models.Interfaces;

    public class Engine : IEngine
    {
        private readonly IReader reader;
        private readonly IWriter writer;

        public Engine(IReader reader, IWriter writer) 
        {
            this.reader = reader;
            this.writer = writer;
        }
        public void Start()
        {
            string[] phoneNumbers = reader.ReadLine().Split();
            string[] urls = reader.ReadLine().Split();

            ISmartphone smartphone = new Smartphone();
            IStationaryPhone stationaryPhone = new StationaryPhone();

            foreach (string phoneNumber in phoneNumbers)
            {
                try
                {
                    if (phoneNumber.Length == 10)
                    {
                        writer.WriteLine(smartphone.Call(phoneNumber));
                    }
                    else if (phoneNumber.Length == 7)
                    {
                        writer.WriteLine(stationaryPhone.Call(phoneNumber));
                    }
                    else
                    {
                        throw new ArgumentException("Invalid number!");
                    }
                }
                catch (Exception ex)
                {
                    writer.WriteLine(ex.Message);
                }
            }

            foreach (var url in urls)
            {
                try
                {
                   writer.WriteLine(smartphone.Browse(url));
                }
                catch (Exception ex)
                {
                    writer.WriteLine(ex.Message);
                }
            }
        }
    }
}
