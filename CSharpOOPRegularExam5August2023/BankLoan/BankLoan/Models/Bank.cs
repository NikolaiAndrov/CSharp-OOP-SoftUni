using BankLoan.Models.Contracts;
using BankLoan.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BankLoan.Models
{
    public abstract class Bank : IBank
    {

        private string name;
        private ICollection<ILoan> loans;
        private ICollection<IClient> clients;

        protected Bank(string name, int capacity)
        {
            Name = name;
            Capacity = capacity;

            this.loans = new List<ILoan>();
            this.clients = new List<IClient>();
        }

        public string Name
        {
            get => this.name;

            private set 
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.BankNameNullOrWhiteSpace);
                }

                this.name = value;
            }
        }

        public int Capacity { get; private set; }

        public IReadOnlyCollection<ILoan> Loans
            => (IReadOnlyCollection<ILoan>)this.loans;

        public IReadOnlyCollection<IClient> Clients
            => (IReadOnlyCollection<IClient>)this.clients;  

        public void AddClient(IClient Client)
        {
            if (this.Capacity == this.Clients.Count)
            {
                throw new ArgumentException("Not enough capacity for this client.");
            }

            this.clients.Add(Client);
        }

        public void AddLoan(ILoan loan)
        {
            this.loans.Add(loan);
        }

        public string GetStatistics()
        {
            string allClients = this.Clients.Any() ?
                string.Join(", ", this.Clients.Select(x => x.Name)) : "none";


            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Name: {this.Name}, Type: {this.GetType().Name}");
            sb.AppendLine($"Clients: {allClients}");
            sb.AppendLine($"Loans: {this.Loans.Count}, Sum of Rates: {this.SumRates()}");

            return sb.ToString().TrimEnd();
        }

        public void RemoveClient(IClient Client)
        {
            this.clients.Remove(Client);
        }

        public double SumRates()
            => this.loans.Sum(x => x.InterestRate);
    }
}
