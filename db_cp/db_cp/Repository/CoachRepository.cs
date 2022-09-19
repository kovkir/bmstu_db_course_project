using System;
using db_cp.Models;
using db_cp.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace db_cp.Repository
{
    public class CoachRepository : ICoachRepository
    {
        private readonly AppDBContext _appDBContext;

        public CoachRepository(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }

        public void Add(Coach model)
        {
            try
            {
                _appDBContext.Coach.Add(model);
                _appDBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                throw new Exception("Ошибка при добавлении тренера");
            }
        }

        public void Delete(int id)
        {
            try
            {
                Coach coach = _appDBContext.Coach.Find(id);

                if (coach != null)
                {
                    _appDBContext.Coach.Remove(coach);
                    _appDBContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                throw new Exception("Ошибка при удалении тренера");
            }
        }

        public IEnumerable<Coach> GetAll()
        {
            return _appDBContext.Coach.ToList();
        }

        public IEnumerable<Coach> GetByCountry(string country)
        {
            return _appDBContext.Coach.Where(elem => elem.Country == country).ToList();
        }

        public Coach GetByID(int id)
        {
            return _appDBContext.Coach.Find(id);
        }

        public IEnumerable<Coach> GetBySurname(string surname)
        {
            return _appDBContext.Coach.Where(elem => elem.Surname == surname).ToList();
        }

        public IEnumerable<Coach> GetByWorkExperience(uint workExperience)
        {
            return _appDBContext.Coach.Where(elem => elem.WorkExperience == workExperience).ToList();
        }

        public void Update(Coach model)
        {
            try
            {
                var curModel = _appDBContext.Coach.FirstOrDefault(elem => elem.Id == model.Id);
                _appDBContext.Entry(curModel).CurrentValues.SetValues(model);

                _appDBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                throw new Exception("Ошибка при обновлении тренера");
            }
        }
    }
}
