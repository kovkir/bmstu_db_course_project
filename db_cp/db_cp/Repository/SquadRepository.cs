using System;
using db_cp.Models;
using db_cp.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace db_cp.Repository
{
    public class SquadRepository : ISquadRepository
    {
        private readonly AppDBContext _appDBContext;

        public SquadRepository(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }

        public void Add(Squad model)
        {
            try
            {
                _appDBContext.Squad.Add(model);
                _appDBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                throw new Exception("Ошибка при добавлении состава");
            }
        }

        public void Delete(int id)
        {
            try
            {
                Squad squad = _appDBContext.Squad.Find(id);

                if (squad != null)
                {
                    _appDBContext.Squad.Remove(squad);
                    _appDBContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                throw new Exception("Ошибка при удалении состава");
            }
        }

        public IEnumerable<Squad> GetAll()
        {
            return _appDBContext.Squad.ToList();
        }

        public Squad GetByID(int id)
        {
            return _appDBContext.Squad.Find(id);
        }

        public Squad GetByName(string name)
        {
            return _appDBContext.Squad.FirstOrDefault(elem => elem.Name == name);
        }

        public IEnumerable<Squad> GetByRating(uint rating)
        {
            return _appDBContext.Squad.Where(elem => elem.Rating == rating).ToList();
        }

        public void Update(Squad model)
        {
            try
            {
                var curModel = _appDBContext.Squad.FirstOrDefault(elem => elem.Id == model.Id);
                _appDBContext.Entry(curModel).CurrentValues.SetValues(model);

                _appDBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                throw new Exception("Ошибка при обновлении состава");
            }
        }

        public void AddSquadPlayer(int squadId, int playerId)
        {
            SquadPlayer squadPlayer = new SquadPlayer
            {
                SquadId = squadId,
                PlayerId = playerId
            };

            try
            {
                _appDBContext.SquadPlayer.Add(squadPlayer);
                _appDBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                throw new Exception("Ошибка при добавлении связи между игроком и составом");
            }
        }

        public void DeleteSquadPlayer(int squadId, int playerId)
        {
            try
            {
                SquadPlayer squadPlayer = _appDBContext.SquadPlayer
                    .FirstOrDefault(elem => elem.SquadId == squadId &&
                                            elem.PlayerId == playerId);

                if (squadPlayer != null)
                {
                    _appDBContext.SquadPlayer.Remove(squadPlayer);
                    _appDBContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                throw new Exception("Ошибка при удалении связи между игроком и составом");
            }
        }

        public IEnumerable<SquadPlayer> GetAllSquadPlayer()
        {
            return _appDBContext.SquadPlayer.ToList();
        }

        public IEnumerable<SquadPlayer> GetSquadPlayerByPlayerId(int playerId)
        {
            return _appDBContext.SquadPlayer.Where(elem => elem.PlayerId == playerId).ToList();
        }

        public SquadPlayer GetSquadPlayer(int squadId, int playerId)
        {
            return _appDBContext.SquadPlayer
                .FirstOrDefault(elem => elem.SquadId == squadId &&
                                        elem.PlayerId == playerId);
        }

        public IEnumerable<Player> GetMyPlayersBySquadId (int squadId)
        {
            IEnumerable<int> mySquadPlayers = _appDBContext.SquadPlayer
                .Where(elem => elem.SquadId == squadId)
                .Select(elem => elem.PlayerId).ToList();

            IEnumerable<Player> myPlayers = _appDBContext.Player
                .Where(elem => mySquadPlayers.Contains(elem.Id)).ToList();

            return myPlayers;
        }
    }
}
