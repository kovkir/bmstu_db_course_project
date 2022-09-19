using System;
using System.Collections.Generic;
using db_cp.Models;

namespace db_cp.Interfaces
{
    public interface ICoachRepository : IRepository<Coach>
    {
        IEnumerable<Coach> GetBySurname(string surname);
        IEnumerable<Coach> GetByCountry(string country);
        IEnumerable<Coach> GetByWorkExperience(uint workExperience);
    }
}
