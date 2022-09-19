using System;
using db_cp.Models;
using System.Collections.Generic;

namespace db_cp.Interfaces
{
    public interface IPlayerRepository : IRepository <Player>
    {
        IEnumerable<Player> GetBySurname(string surname);
        IEnumerable<Player> GetByRating(uint rating);
        IEnumerable<Player> GetByCountry(string country);
        IEnumerable<Player> GetByPrice(uint minPrice, uint maxPrice);
    }
}
