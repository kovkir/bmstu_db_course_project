using System;
using db_cp.Models;
using db_cp.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace db_cp.Services
{
    public interface IUserService
    {
        void Add(User user);
        void Delete(User user);
        void Update(User user);

        IEnumerable<User> GetAll();
        User GetByID(int id);
        User GetByLogin(string login);

        IEnumerable<User> GetByPermission(string permission);

        IEnumerable<User> GetSortUsersByOrder(UserSortState sortOrder);
    }

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ISquadRepository _squadRepository;

        public UserService(IUserRepository userRepository, ISquadRepository squadRepository)
        {
            _userRepository = userRepository;
            _squadRepository = squadRepository;
        }


        private bool IsExist(User user)
        {
            return _userRepository.GetAll().FirstOrDefault(elem =>
                    elem.Login == user.Login) != null;
        }

        private bool IsNotExist(int id)
        {
            return _userRepository.GetByID(id) == null;
        }



        public void Add(User user)
        {
            if (IsExist(user))
                throw new Exception("Пользователь с таким логином уже существует");

            _userRepository.Add(user);
        }

        public void Delete(User user)
        {
            if (IsNotExist(user.Id))
                throw new Exception("Такого пользователя не существует");

            _userRepository.Delete(user.Id);
        }

        public IEnumerable<User> GetAll()
        {
            return _userRepository.GetAll();
        }

        public User GetByID(int id)
        {
            return _userRepository.GetByID(id);
        }

        public User GetByLogin(string login)
        {
            return _userRepository.GetByLogin(login);
        }

        public IEnumerable<User> GetByPermission(string permission)
        {
            return _userRepository.GetByPermission(permission);
        }

        public void Update(User user)
        {
            if (IsNotExist(user.Id))
                throw new Exception("Такого пользователя не существует");

            _userRepository.Update(user);
        }

        public IEnumerable<User> GetSortUsersByOrder(UserSortState sortOrder)
        {
            IEnumerable<User> users = sortOrder switch
            {
                UserSortState.IdDesc => _userRepository.GetAll().OrderByDescending(elem => elem.Id),

                UserSortState.LoginAsc => _userRepository.GetAll().OrderBy(elem => elem.Login),
                UserSortState.LoginDesc => _userRepository.GetAll().OrderByDescending(elem => elem.Login),

                UserSortState.PermissionAsc => _userRepository.GetAll().OrderBy(elem => elem.Permission),
                UserSortState.PermissionDesc => _userRepository.GetAll().OrderByDescending(elem => elem.Permission),

                UserSortState.RatingSquadAsc => _userRepository.GetAll().OrderBy(elem => _squadRepository.GetByID(elem.Id).Rating),
                UserSortState.RatingSquadDesc => _userRepository.GetAll().OrderByDescending(elem => _squadRepository.GetByID(elem.Id).Rating),

                _ => _userRepository.GetAll().OrderBy(elem => elem.Id)
            };

            return users;
        }
    }
}
