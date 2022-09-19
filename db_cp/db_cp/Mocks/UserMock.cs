using System;
using db_cp.Models;
using db_cp.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace db_cp.Mocks
{
    public class UserMock : DataMock, IUserRepository
    {
        public void Add(User model)
        {
            _users.Add(model);
        }

        public void Delete(int id)
        {
            User user = _users[id - 1];
            _users.Remove(user);
        }

        public IEnumerable<User> GetAll()
        {
            return _users;
        }

        public User GetByID(int id)
        {
            return _users[id - 1];
        }

        public User GetByLogin(string login)
        {
            return _users.FirstOrDefault(elem => elem.Login == login);
        }

        public IEnumerable<User> GetByPermission(string permission)
        {
            return _users.Where(elem => elem.Permission == permission);
        }

        public void Update(User model)
        {
            User user = _users[model.Id - 1];

            user.Login = model.Login;
            user.Password = model.Password;
            user.Permission = model.Permission;

            _users[user.Id - 1] = user;
        }
    }
}
