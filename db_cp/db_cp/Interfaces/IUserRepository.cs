using System;
using db_cp.Models;
using System.Collections.Generic;

namespace db_cp.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        User GetByLogin(string login);

        IEnumerable<User> GetByPermission(string permission);
    }
}
