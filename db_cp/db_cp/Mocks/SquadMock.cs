using System;
using db_cp.Models;
using db_cp.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace db_cp.Mocks
{
    public class SquadMock : DataMock, ISquadRepository
    {
        public void Add(Squad model)
        {
            _squads.Add(model);
        }

        public void Delete(int id)
        {
            Squad squad = _squads[id - 1];
            _squads.Remove(squad);
        }

        public IEnumerable<Squad> GetAll()
        {
            return _squads;
        }

        public Squad GetByID(int id)
        {
            return _squads[id - 1];
        }

        public Squad GetByName(string name)
        {
            return _squads.FirstOrDefault(elem => elem.Name == name);
        }

        public IEnumerable<Squad> GetByRating(uint rating)
        {
            return _squads.Where(elem => elem.Rating == rating);
        }

        public void Update(Squad model)
        {
            Squad squad = _squads[model.Id - 1];

            squad.CoachId = model.CoachId;
            squad.Name = model.Name;
            squad.Rating = model.Rating;

            _squads[squad.Id - 1] = squad;
        }

        public void AddSquadPlayer(int squadId, int playerId)
        {
            SquadPlayer model = new SquadPlayer
            {
                Id = _squadPlayer.Count() + 1,
                SquadId = squadId,
                PlayerId = playerId
            };

            _squadPlayer.Add(model);
        }

        public void DeleteSquadPlayer(int squadId, int playerId)
        {
            SquadPlayer squadPlayer = _squadPlayer
                .FirstOrDefault(elem => elem.SquadId == squadId &&
                                        elem.PlayerId == playerId);

            if (squadPlayer != null)
                _squadPlayer.Remove(squadPlayer);
        }

        public IEnumerable<SquadPlayer> GetAllSquadPlayer()
        {
            return _squadPlayer;
        }

        public IEnumerable<SquadPlayer> GetSquadPlayerByPlayerId(int playerId)
        {
            return _squadPlayer.Where(elem => elem.PlayerId == playerId);
        }

        public IEnumerable<Player> GetMyPlayersBySquadId(int squadId)
        {
            IEnumerable<int> mySquadPlayers = _squadPlayer
                .Where(elem => elem.SquadId == squadId)
                .Select(elem => elem.PlayerId);

            IEnumerable<Player> myPlayers = _players
                .Where(elem => mySquadPlayers.Contains(elem.Id));

            return myPlayers;
        }

        public SquadPlayer GetSquadPlayer(int squadId, int playerId)
        {
            return _squadPlayer
                .FirstOrDefault(elem => elem.SquadId == squadId &&
                                        elem.PlayerId == playerId);
        }
    }
}
