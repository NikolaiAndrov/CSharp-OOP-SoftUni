using EDriveRent.Models.Contracts;
using EDriveRent.Repositories.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace EDriveRent.Repositories
{
    public class UserRepository : IRepository<IUser>
    {
        private ICollection<IUser> users;

        public UserRepository()
        {
            this.users = new List<IUser>();
        }

        public void AddModel(IUser model)
        {
            this.users.Add(model);
        }

        public IUser FindById(string identifier)
            => this.users.FirstOrDefault(u => u.DrivingLicenseNumber == identifier);

        public IReadOnlyCollection<IUser> GetAll()
            => (IReadOnlyCollection<IUser>)this.users;

        public bool RemoveById(string identifier)
        {
            IUser user = this.users.FirstOrDefault(u => u.DrivingLicenseNumber == identifier);
            return this.users.Remove(user);
        }
    }
}
