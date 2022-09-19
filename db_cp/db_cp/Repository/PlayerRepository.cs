using System;
using db_cp.Models;
using db_cp.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace db_cp.Repository
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly AppDBContext _appDBContext;

        public PlayerRepository(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }

        public void Add(Player model)
        {
            try
            {
                _appDBContext.Player.Add(model);
                _appDBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                throw new Exception("Ошибка при добавлении игрока");
            }
        }

        public void Delete(int id)
        {
            try
            {
                Player player = _appDBContext.Player.Find(id);

                if (player != null)
                {
                    _appDBContext.Player.Remove(player);
                    _appDBContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                throw new Exception("Ошибка при удалении игрока");
            }
        }

        public IEnumerable<Player> GetAll()
        {
            return _appDBContext.Player.ToList();
        }

        public IEnumerable<Player> GetByCountry(string country)
        {
            return _appDBContext.Player.Where(elem => elem.Country == country).ToList();
        }

        public Player GetByID(int id)
        {
            return _appDBContext.Player.Find(id);
        }

        public IEnumerable<Player> GetByPrice(uint minPrice, uint maxPrice)
        {
            return _appDBContext.Player.Where(elem => elem.Price >= minPrice && elem.Price <= maxPrice).ToList();
        }

        public IEnumerable<Player> GetByRating(uint rating)
        {
            return _appDBContext.Player.Where(elem => elem.Rating == rating).ToList();
        }

        public IEnumerable<Player> GetBySurname(string surname)
        {
            return _appDBContext.Player.Where(elem => elem.Surname == surname).ToList();
        }

        public void Update(Player model)
        {
            try
            {
                var curModel = _appDBContext.Player.FirstOrDefault(elem => elem.Id == model.Id);
                _appDBContext.Entry(curModel).CurrentValues.SetValues(model);

                _appDBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                throw new Exception("Ошибка при обновлении игрока");
            }
        }
    }
}
