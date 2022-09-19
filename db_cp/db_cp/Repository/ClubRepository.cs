using System;
using db_cp.Models;
using db_cp.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace db_cp.Repository
{
    public class ClubRepository : IClubRepository
    {
        private readonly AppDBContext _appDBContext;

        public ClubRepository(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }

        public void Add(Club model)
        {
            try
            {
                _appDBContext.Club.Add(model);
                _appDBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                throw new Exception("Ошибка при добавлении клуба");
            }
        }

        public void Delete(int id)
        {
            try
            {
                Club club = _appDBContext.Club.Find(id);

                if (club != null)
                {
                    _appDBContext.Club.Remove(club);
                    _appDBContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                throw new Exception("Ошибка при удалении клуба");
            }
        }

        public IEnumerable<Club> GetAll()
        {
            return _appDBContext.Club.ToList();
        }

        public IEnumerable<Club> GetByCountry(string country)
        {
            return _appDBContext.Club.Where(elem => elem.Country == country).ToList();
        }

        public IEnumerable<Club> GetByFoundationDate(uint year)
        {
            return _appDBContext.Club.Where(elem => elem.FoundationDate == year).ToList();
        }

        public Club GetByID(int id)
        {
            return _appDBContext.Club.Find(id);
        }

        public Club GetByName(string name)
        {
            return _appDBContext.Club.FirstOrDefault(elem => elem.Name == name);
        }

        public void Update(Club model)
        {
            try
            {
                var curModel = _appDBContext.Club.FirstOrDefault(elem => elem.Id == model.Id);
                _appDBContext.Entry(curModel).CurrentValues.SetValues(model);

                _appDBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                throw new Exception("Ошибка при обновлении клуба");
            }
        }
    }
}
