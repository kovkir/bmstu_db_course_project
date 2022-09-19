using System;
using db_cp.Models;
using db_cp.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace db_cp.Mocks
{
    public class ClubMock : DataMock, IClubRepository
    {
        public void Add(Club model)
        {
            _clubs.Add(model);
        }

        public void Delete(int id)
        {
            Club club = _clubs[id - 1];
            _clubs.Remove(club);
        }

        public IEnumerable<Club> GetAll()
        {
            return _clubs;
        }

        public IEnumerable<Club> GetByCountry(string country)
        {
            return _clubs.Where(elem => elem.Country == country);
        }

        public IEnumerable<Club> GetByFoundationDate(uint year)
        {
            return _clubs.Where(elem => elem.FoundationDate == year);
        }

        public Club GetByID(int id)
        {
            return _clubs[id - 1];
        }

        public Club GetByName(string name)
        {
            return _clubs.FirstOrDefault(elem => elem.Name == name);
        }

        public void Update(Club model)
        {
            Club club = _clubs[model.Id - 1];

            club.Name = model.Name;
            club.Country = model.Country;
            club.FoundationDate = model.FoundationDate;

            _clubs[club.Id - 1] = club;
        }
    }
}
