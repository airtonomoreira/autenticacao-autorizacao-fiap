using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.Domain.User.Repository
{
    public interface IUserRepository
    {
        Task<Auth.Domain.User.Models.User?> Get(string username, string password);
    }
}