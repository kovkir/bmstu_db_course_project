using System;
using db_cp.Models;
using db_cp.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace db_cp.Mocks
{
    public class PlayerMock : DataMock, IPlayerRepository
    {
        public void Add(Player model)
        {
            _players.Add(model);
        }

        public void Delete(int id)
        {
            Player player = _players[id - 1];
            _players.Remove(player);
        }

        public IEnumerable<Player> GetAll()
        {
            return _players;
        }

        public IEnumerable<Player> GetByCountry(string country)
        {
            return _players.Where(elem => elem.Country == country);
        }

        public Player GetByID(int id)
        {
            return _players[id - 1];
        }

        public IEnumerable<Player> GetByPrice(uint minPrice, uint maxPrice)
        {
            return _players.Where(elem => elem.Price >= minPrice && elem.Price <= maxPrice);
        }

        public IEnumerable<Player> GetByRating(uint rating)
        {
            return _players.Where(elem => elem.Rating == rating);
        }

        public IEnumerable<Player> GetBySurname(string surname)
        {
            return _players.Where(elem => elem.Surname == surname);
        }

        public void Update(Player model)
        {
            Player player = _players[model.Id - 1];

            player.ClubId = model.ClubId;
            player.Surname = model.Surname;
            player.Rating = model.Rating;
            player.Country = model.Country;
            player.Price = model.Price;

            _players[player.Id - 1] = player;
        }
    }
}
