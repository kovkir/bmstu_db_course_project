using System;
using db_cp.Models;
using db_cp.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace db_cp.Services
{
    public interface IClubService
    {
        void Add(Club club);
        void Delete(Club club);
        void Update(Club club);

        IEnumerable<Club> GetAll();
        Club GetByID(int id);
        Club GetByName(string name);

        IEnumerable<Club> GetByCountry(string country);
        IEnumerable<Club> GetByFoundationDate(uint year);
        IEnumerable<Club> GetByParameters(string name, string country,
                                          uint minFoundationDate, uint maxFoundationDate);

        IEnumerable<Club> GetSortClubsByOrder(IEnumerable<Club> clubs, ClubSortState sortOrder);
    }

    public class ClubService : IClubService
    {
        private readonly IClubRepository _clubRepository;

        public ClubService(IClubRepository clubRepository)
        {
            _clubRepository = clubRepository;
        }


        private bool IsExist(Club club)
        {
            return _clubRepository.GetAll().FirstOrDefault(elem =>
                    elem.Name == club.Name) != null;
        }

        private bool IsNotExist(int id)
        {
            return _clubRepository.GetByID(id) == null;
        }



        public void Add(Club club)
        {
            if (IsExist(club))
                throw new Exception("Клуб с таким названием уже существует");

            _clubRepository.Add(club);
        }

        public void Delete(Club club)
        {
            if (IsNotExist(club.Id))
                throw new Exception("Такого клуба не существует");

            _clubRepository.Delete(club.Id);
        }

        public IEnumerable<Club> GetAll()
        {
            return _clubRepository.GetAll();
        }

        public IEnumerable<Club> GetByCountry(string country)
        {
            return _clubRepository.GetByCountry(country);
        }

        public IEnumerable<Club> GetByFoundationDate(uint year)
        {
            return _clubRepository.GetByFoundationDate(year);
        }

        public Club GetByID(int id)
        {
            return _clubRepository.GetByID(id);
        }

        public Club GetByName(string name)
        {
            return _clubRepository.GetByName(name);
        }

        public void Update(Club club)
        {
            if (IsNotExist(club.Id))
                throw new Exception("Такого клуба не существует");

            _clubRepository.Update(club);
        }

        public IEnumerable<Club> GetSortClubsByOrder(IEnumerable<Club> clubs, ClubSortState sortOrder)
        {
            IEnumerable<Club> needClubs = sortOrder switch
            {
                ClubSortState.IdDesc => clubs.OrderByDescending(elem => elem.Id),

                ClubSortState.NameAsc => clubs.OrderBy(elem => elem.Name),
                ClubSortState.NameDesc => clubs.OrderByDescending(elem => elem.Name),

                ClubSortState.CountryAsc => clubs.OrderBy(elem => elem.Country),
                ClubSortState.CountryDesc => clubs.OrderByDescending(elem => elem.Country),

                ClubSortState.FoundationDateAsc => clubs.OrderBy(elem => elem.FoundationDate),
                ClubSortState.FoundationDateDesc => clubs.OrderByDescending(elem => elem.FoundationDate),

                _ => clubs.OrderBy(elem => elem.Id),
            };

            return needClubs;
        }

        public IEnumerable<Club> GetByParameters(string name, string country,
                                                 uint minFoundationDate, uint maxFoundationDate)
        {
            IEnumerable<Club> clubs = _clubRepository.GetAll();

            if (clubs.Count() != 0 && name != null)
                clubs = clubs.Where(elem => elem.Name == name);

            if (clubs.Count() != 0 && country != null)
                clubs = clubs.Where(elem => elem.Country == country);

            if (clubs.Count() != 0 && minFoundationDate != 0)
                clubs = clubs.Where(elem => elem.FoundationDate >= minFoundationDate);

            if (clubs.Count() != 0 && maxFoundationDate != 0)
                clubs = clubs.Where(elem => elem.FoundationDate <= maxFoundationDate);

            return clubs;
        }
    }
}
