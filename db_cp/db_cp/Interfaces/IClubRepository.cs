using System;
using System.Collections.Generic;
using db_cp.Models;

namespace db_cp.Interfaces
{
    public interface IClubRepository : IRepository<Club>
    {
        Club GetByName(string name);

        IEnumerable<Club> GetByCountry(string country);
        IEnumerable<Club> GetByFoundationDate(uint year);
    }
}
