using BankLoan.Core.Contracts;
using BankLoan.Models;
using BankLoan.Models.Contracts;
using BankLoan.Repositories;
using BankLoan.Repositories.Contracts;
using BankLoan.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLoan.Core
{
    public class Controller : IController
    {
        private IRepository<ILoan> loans;
        private IRepository<IBank> banks;

        public Controller()
        {
            this.loans = new LoanRepository();
            this.banks = new BankRepository();
        }

        public string AddBank(string bankTypeName, string name)
        {
            IBank bank;

            if (bankTypeName == nameof(BranchBank))
            {
                bank = new BranchBank(name);
            }
            else if (bankTypeName == nameof(CentralBank))
            {
                bank = new CentralBank(name);
            }
            else
            {
                throw new ArgumentException(ExceptionMessages.BankTypeInvalid);
            }

            this.banks.AddModel(bank);

            return string.Format(OutputMessages.BankSuccessfullyAdded, bankTypeName);
        }

        public string AddClient(string bankName, string clientTypeName, string clientName, string id, double income)
        {
            IClient client;

            if (clientTypeName == nameof(Adult))
            {
                client = new Adult(clientName, id, income);
            }
            else if (clientTypeName == nameof(Student))
            {
                client = new Student(clientName, id, income);
            }
            else
            {
                throw new ArgumentException(ExceptionMessages.ClientTypeInvalid);
            }

            IBank bank = this.banks.FirstModel(bankName);

            if (clientTypeName == nameof(Student) && bank.GetType().Name != nameof(BranchBank) ||
                clientTypeName == nameof(Adult) && bank.GetType().Name != nameof(CentralBank))
            {
                return string.Format(OutputMessages.UnsuitableBank);
            }

            bank.AddClient(client);

            return string.Format(OutputMessages.ClientAddedSuccessfully, clientTypeName, bankName);
        }

        public string AddLoan(string loanTypeName)
        {
            ILoan loan;

            if (loanTypeName == nameof(MortgageLoan))
            {
                loan = new MortgageLoan();
            }
            else if (loanTypeName == nameof(StudentLoan))
            {
                loan = new StudentLoan();
            }
            else
            {
                throw new ArgumentException(ExceptionMessages.LoanTypeInvalid);
            }

            this.loans.AddModel(loan);

            return string.Format(OutputMessages.LoanSuccessfullyAdded, loanTypeName);
        }

        public string FinalCalculation(string bankName)
        {
            IBank bank = this.banks.FirstModel(bankName);

            double income = bank.Clients.Sum(c => c.Income);
            double loans = bank.Loans.Sum(x => x.Amount);
            double funds = income + loans;

            string fundsToReturn = $"{funds:f2}";

            return string.Format(OutputMessages.BankFundsCalculated, bankName, fundsToReturn);
        }

        public string ReturnLoan(string bankName, string loanTypeName)
        {
            IBank bank = this.banks.FirstModel(bankName);

            ILoan loan = this.loans.FirstModel(loanTypeName);

            if (loan == null)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.MissingLoanFromType, loanTypeName));
            }

            bank.AddLoan(loan);
            this.loans.RemoveModel(loan);

            return string.Format(OutputMessages.LoanReturnedSuccessfully, loanTypeName, bankName);
        }

        public string Statistics()
        {
            StringBuilder sb = new StringBuilder();

            foreach (IBank bank in this.banks.Models)
            {
                sb.AppendLine(bank.GetStatistics());
            }

            return sb.ToString().TrimEnd();
        }
    }
}
