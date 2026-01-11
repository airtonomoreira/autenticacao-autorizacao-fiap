using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Auth.Domain.User.Repository;

namespace Auth.Infrastructure
{
    public class UserRepository : IUserRepository
    {
        private readonly IList<Auth.Domain.User.Models.User> _users;

        public UserRepository()
        {
            _users = new List<Auth.Domain.User.Models.User>();
            _users.Add(new Auth.Domain.User.Models.User { Id = 1, Username = "manager", Password = "passw@rd", Role = "manager" });
            _users.Add(new Auth.Domain.User.Models.User { Id = 2, Username = "employee", Password = "passw@rd", Role = "employee" });
        }

        public async Task<Auth.Domain.User.Models.User?> Get(string username, string password)
        {
            return await Task.Run(() =>
             {
                 return _users.Where(x => string.Equals(x.Username, username, StringComparison.OrdinalIgnoreCase)
                          && string.Equals(x.Password, password))
                          .FirstOrDefault();
             });
        }
    }
}