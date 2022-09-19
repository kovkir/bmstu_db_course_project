using System;
using db_cp.Models;
using db_cp.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace db_cp.Services
{
    public interface IPlayerService
    {
        void Add(Player player);
        void Delete(Player player);
        void Update(Player player);

        IEnumerable<Player> GetAll();
        Player GetByID(int id);

        IEnumerable<Player> GetBySurname(string surname);
        IEnumerable<Player> GetByRating(uint rating);
        IEnumerable<Player> GetByCountry(string country);
        IEnumerable<Player> GetByPrice(uint minPrice, uint maxPrice);
        IEnumerable<Player> GetByClubName(string clubName);
        IEnumerable<Player> GetByParameters(string surname, string country, string clubName,
                                            uint minPrice, uint maxPrice, uint minRating, uint maxRating, int squadId);

        IEnumerable<Player> GetSortPlayersByOrder(IEnumerable<Player> players, PlayerSortState sortOrder);
    }

    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly IClubRepository _clubRepository;
        private readonly ISquadRepository _squadRepository;

        public PlayerService(IPlayerRepository playerRepository,
                             IClubRepository clubRepository,
                             ISquadRepository squadRepository)
        {
            _playerRepository = playerRepository;
            _clubRepository = clubRepository;
            _squadRepository = squadRepository;
        }


        private bool IsExist(Player player)
        {
            return _playerRepository.GetAll().FirstOrDefault(elem =>
                    elem.Surname == player.Surname &&
                    elem.ClubId  == player.ClubId  &&
                    elem.Country == player.Country &&
                    elem.Rating  == player.Rating) != null;
        }

        private bool IsNotExist(int id)
        {
            return _playerRepository.GetByID(id) == null;
        }



        public void Add(Player player)
        {
            if (IsExist(player))
                throw new Exception("Такой футболист уже существует");

            _playerRepository.Add(player);
        }

        public void Delete(Player player)
        {
            if (IsNotExist(player.Id))
                throw new Exception("Такого футболиста не существует");

            _playerRepository.Delete(player.Id);
        }

        public IEnumerable<Player> GetAll()
        {
            return _playerRepository.GetAll();
        }

        public IEnumerable<Player> GetByCountry(string country)
        {
            return _playerRepository.GetByCountry(country);
        }

        public Player GetByID(int id)
        {
            return _playerRepository.GetByID(id);
        }

        public IEnumerable<Player> GetByPrice(uint minPrice, uint maxPrice)
        {
            return _playerRepository.GetByPrice(minPrice, maxPrice);
        }

        public IEnumerable<Player> GetByRating(uint rating)
        {
            return _playerRepository.GetByRating(rating);
        }

        public IEnumerable<Player> GetBySurname(string surname)
        {
            return _playerRepository.GetBySurname(surname);
        }

        public IEnumerable<Player> GetByClubName(string clubName)
        {
            Club club = _clubRepository.GetByName(clubName);

            if (club == null)
                return null;
            else
                return _playerRepository.GetAll().Where(elem => elem.ClubId == club.Id);
        }

        public void Update(Player player)
        {
            if (IsNotExist(player.Id))
                throw new Exception("Такого футболиста не существует");

            _playerRepository.Update(player);
        }

        public IEnumerable<Player> GetSortPlayersByOrder(IEnumerable<Player> players, PlayerSortState sortOrder)
        {
            IEnumerable<Player> needPlayers = sortOrder switch
            {
                PlayerSortState.IdDesc => players.OrderByDescending(elem => elem.Id),

                PlayerSortState.SurnameAsc => players.OrderBy(elem => elem.Surname),
                PlayerSortState.SurnameDesc => players.OrderByDescending(elem => elem.Surname),

                PlayerSortState.RatingAsc => players.OrderBy(elem => elem.Rating),
                PlayerSortState.RatingDesc => players.OrderByDescending(elem => elem.Rating),

                PlayerSortState.CountryAsc => players.OrderBy(elem => elem.Country),
                PlayerSortState.CountryDesc => players.OrderByDescending(elem => elem.Country),

                PlayerSortState.ClubNameAsc => players.OrderBy(elem => _clubRepository.GetByID(elem.ClubId).Name),
                PlayerSortState.ClubNameDesc => players.OrderByDescending(elem => _clubRepository.GetByID(elem.ClubId).Name),

                PlayerSortState.PriceAsc => players.OrderBy(elem => elem.Price),
                PlayerSortState.PriceDesc => players.OrderByDescending(elem => elem.Price),

                _ => players.OrderBy(elem => elem.Id)
            };

            return needPlayers;
        }

        public IEnumerable<Player> GetByParameters(string surname, string country, string clubName,
                                                   uint minPrice, uint maxPrice, uint minRating, uint maxRating,
                                                   int squadId)
        {
            IEnumerable<Player> players;

            if (squadId != 0)
                players = _squadRepository.GetMyPlayersBySquadId(squadId);
            else
                players = _playerRepository.GetAll();

            if (players.Count() != 0 && surname != null)
                players = players.Where(elem => elem.Surname == surname);

            if (players.Count() != 0 && country != null)
                players = players.Where(elem => elem.Country == country);

            if (players.Count() != 0 && minRating != 0)
                players = players.Where(elem => elem.Rating >= minRating);

            if (players.Count() != 0 && maxRating != 0)
                players = players.Where(elem => elem.Rating <= maxRating);

            if (players.Count() != 0 && minPrice != 0)
                players = players.Where(elem => elem.Price >= minPrice);

            if (players.Count() != 0 && maxPrice != 0)
                players = players.Where(elem => elem.Price <= maxPrice);

            if (players.Count() != 0 && clubName != null)
            {
                Club club = _clubRepository.GetByName(clubName);

                if (club == null)
                    players = Enumerable.Empty<Player>();
                else
                    players = players.Where(elem => elem.ClubId == club.Id);
            }

            return players;
        }
    }
}
