using BankLoan.Models.Contracts;
using BankLoan.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLoan.Repositories
{
    public class LoanRepository : IRepository<ILoan>
    {
        private ICollection<ILoan> loans;

        public LoanRepository()
        {
            this.loans = new List<ILoan>();
        }

        public IReadOnlyCollection<ILoan> Models 
            => (IReadOnlyCollection<ILoan>)this.loans;

        public void AddModel(ILoan model)
        {
            this.loans.Add(model);
        }

        public ILoan FirstModel(string name)
            => this.loans.FirstOrDefault(x => x.GetType().Name == name);

        public bool RemoveModel(ILoan model)
            => this.loans.Remove(model);
    }
}
