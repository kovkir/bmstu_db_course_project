using System;
using db_cp.Models;
using db_cp.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace db_cp.Mocks
{
    public class CoachMock : DataMock, ICoachRepository
    {
        public void Add(Coach model)
        {
            _coaches.Add(model);
        }

        public void Delete(int id)
        {
            Coach coach = _coaches[id - 1];
            _coaches.Remove(coach);
        }

        public IEnumerable<Coach> GetAll()
        {
            return _coaches;
        }

        public IEnumerable<Coach> GetByCountry(string country)
        {
            return _coaches.Where(elem => elem.Country == country);
        }

        public Coach GetByID(int id)
        {
            return _coaches[id - 1];
        }

        public IEnumerable<Coach> GetBySurname(string surname)
        {
            return _coaches.Where(elem => elem.Surname == surname);
        }

        public IEnumerable<Coach> GetByWorkExperience(uint workExperience)
        {
            return _coaches.Where(elem => elem.WorkExperience == workExperience);
        }

        public void Update(Coach model)
        {
            Coach coach = _coaches[model.Id - 1];

            coach.Surname = model.Surname;
            coach.Country = model.Country;
            coach.WorkExperience = model.WorkExperience;

            _coaches[coach.Id - 1] = coach;
        }
    }
}
