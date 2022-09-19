using System;
using db_cp.Models;
using db_cp.Interfaces;
using System.Collections.Generic;
using System.Linq;
using db_cp.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace db_cp.Services
{
    public interface ISquadService
    {
        void Add(Squad squad);
        void Delete(Squad squad);
        void Update(Squad squad);

        IEnumerable<Squad> GetAll();
        Squad GetByID(int id);
        Squad GetByName(string name);

        IEnumerable<Squad> GetByRating(uint rating);
        IEnumerable<Squad> GetSortSquadsByOrder(SquadSortState sortOrder);

        void AddSquadPlayer(int squadId, int playerId);
        void DeleteSquadPlayer(int squadId, int playerId);
        void DeleteAllSquadPlayer(int playerId);

        IEnumerable<SquadPlayer> GetAllSquadPlayer();
        SquadPlayer GetSquadPlayer(int squadId, int playerId);

        IEnumerable<Player> GetMyPlayersBySquadId(int squadId);
        IEnumerable<Player> GetMyPlayersByUserLogin(string userLogin);
        int GetMyCoachIdByUserLogin(string userLogin);

        Squad UpdateMySquad(IsUpdata isUpdate, int squadId, int playerId, int coachId);
    }

    public class SquadService : ISquadService
    {
        private readonly ISquadRepository _squadRepository;
        private readonly ICoachRepository _coachRepository;
        private readonly IUserRepository _userRepository;
        private readonly AppDBContext _appDBContext;

        public SquadService(ISquadRepository squadRepository,
                            ICoachRepository coachRepository,
                            IUserRepository userRepository,
                            AppDBContext appDBContext)
        {
            _squadRepository = squadRepository;
            _coachRepository = coachRepository;
            _userRepository = userRepository;
            _appDBContext = appDBContext;
        }


        private bool IsExist(Squad squad)
        {
            return _squadRepository.GetAll().FirstOrDefault(elem =>
                    elem.Name == squad.Name) != null;
        }

        private bool IsNotExist(int id)
        {
            return _squadRepository.GetByID(id) == null;
        }

        private bool SquadPlayerIsExist(int squadId, int playerId)
        {
            return _squadRepository.GetAllSquadPlayer().FirstOrDefault(elem =>
                    elem.SquadId == squadId &&
                    elem.PlayerId == playerId) != null;
        }

        private bool SquadPlayerIsNotExist(int squadId, int playerId)
        {
            return _squadRepository.GetSquadPlayer(squadId, playerId) == null;
        }

        private void AddPlayerToMySquad(int squadId, int playerId)
        {
            _squadRepository.AddSquadPlayer(squadId, playerId);
        }

        private void AddCoachToMySquad(Squad squad, int coachId)
        {
            squad.CoachId = coachId;
            _squadRepository.Update(squad);
        }

        private void DeletePlayerFromMySquad(int squadId, int playerId)
        {
            _squadRepository.DeleteSquadPlayer(squadId, playerId);
        }

        private void DeleteCoachFromMySquad(Squad squad, int coachId)
        {
            squad.CoachId = 0;
            _squadRepository.Update(squad);
        }

        private uint SumRating(IEnumerable<Player> players)
        {
            uint sumRating = 0;

            foreach (Player player in players)
                sumRating += player.Rating;

            return sumRating;
        }

        private void UpdateMySquadRating(Squad squad)
        {
            IEnumerable<Player> players = _squadRepository.GetMyPlayersBySquadId(squad.Id);

            int numbPlayers = players.Count();
            uint newRating = 0;

            if (numbPlayers > 0)
                newRating = (uint)(SumRating(players) / numbPlayers);

            squad.Rating = newRating;
            _squadRepository.Update(squad);
        }



        public void Add(Squad squad)
        {
            if (IsExist(squad))
                throw new Exception("Состав с таким названием уже существует");

            _squadRepository.Add(squad);
        }

        public void Delete(Squad squad)
        {
            if (IsNotExist(squad.Id))
                throw new Exception("Такого состава не существует");

            _squadRepository.Delete(squad.Id);
        }

        public IEnumerable<Squad> GetAll()
        {
            return _squadRepository.GetAll();
        }

        public Squad GetByID(int id)
        {
            return _squadRepository.GetByID(id);
        }
        
        public Squad GetByName(string name)
        {
            return _squadRepository.GetByName(name);
        }

        public IEnumerable<Squad> GetByRating(uint rating)
        {
            return _squadRepository.GetByRating(rating);
        }

        public void Update(Squad squad)
        {
            if (IsNotExist(squad.Id))
                throw new Exception("Такого состава не существует");

            _squadRepository.Update(squad);
        }


        public IEnumerable<Squad> GetSortSquadsByOrder(SquadSortState sortOrder)
        {
            IEnumerable<Squad> coaches = sortOrder switch
            {
                SquadSortState.IdDesc => _squadRepository.GetAll().OrderByDescending(elem => elem.Id),

                SquadSortState.CoachSurnameAsc => _squadRepository.GetAll().OrderBy(elem => _coachRepository.GetByID(elem.CoachId).Surname),
                SquadSortState.CoachSurnameDesc => _squadRepository.GetAll().OrderByDescending(elem => _coachRepository.GetByID(elem.CoachId).Surname),

                SquadSortState.NameAsc => _squadRepository.GetAll().OrderBy(elem => elem.Name),
                SquadSortState.NameDesc => _squadRepository.GetAll().OrderByDescending(elem => elem.Name),

                SquadSortState.RatingAsc => _squadRepository.GetAll().OrderBy(elem => elem.Rating),
                SquadSortState.RatingDesc => _squadRepository.GetAll().OrderByDescending(elem => elem.Rating),

                _ => _squadRepository.GetAll().OrderBy(elem => elem.Id)
            };

            return coaches;
        }

        public void AddSquadPlayer(int squadId, int playerId)
        {
            if (SquadPlayerIsExist(squadId, playerId))
                throw new Exception("Данный футболист уже добавлен в состав");

            _squadRepository.AddSquadPlayer(squadId, playerId);
        }

        public void DeleteSquadPlayer(int squadId, int playerId)
        {
            if (SquadPlayerIsNotExist(squadId, playerId))
                throw new Exception("Такого футболиста в составе нет");

            _squadRepository.DeleteSquadPlayer(squadId, playerId);
        }

        public void DeleteAllSquadPlayer(int playerId)
        {
            IEnumerable<SquadPlayer> squadPlayerList = _squadRepository.GetSquadPlayerByPlayerId(playerId);

            foreach (SquadPlayer elem in squadPlayerList)
            {
                UpdateMySquad(IsUpdata.PlayerIsDeleted, elem.SquadId, playerId);
            }
        }

        public IEnumerable<SquadPlayer> GetAllSquadPlayer()
        {
            return _squadRepository.GetAllSquadPlayer();
        }

        public SquadPlayer GetSquadPlayer(int squadId, int playerId)
        {
            return _squadRepository.GetSquadPlayer(squadId, playerId);
        }

        public IEnumerable<Player> GetMyPlayersBySquadId(int squadId)
        {
            return _squadRepository.GetMyPlayersBySquadId(squadId);
        }

        public IEnumerable<Player> GetMyPlayersByUserLogin(string userLogin)
        {
            User user = _userRepository.GetByLogin(userLogin);
            IEnumerable<Player> myPlayers;

            if (user == null)
                myPlayers = null;
            else
                myPlayers = _squadRepository.GetMyPlayersBySquadId(user.Id);

            return myPlayers;
        }

        public int GetMyCoachIdByUserLogin(string userLogin)
        {
            User user = _userRepository.GetByLogin(userLogin);
            int myCoachId;

            if (user == null)
                myCoachId = 0;
            else
                myCoachId = _squadRepository.GetByID(user.Id).CoachId;
                
            return myCoachId;
        }

        public Squad UpdateMySquad(IsUpdata isUpdate, int squadId, int playerId = 0, int coachId = 0)
        {
            Squad squad = _squadRepository.GetByID(squadId);

            if (isUpdate == IsUpdata.PlayerIsAdded)
            {
                AddPlayerToMySquad(squadId, playerId);
            }
            else if (isUpdate == IsUpdata.CoachIsAdded)
            {
                AddCoachToMySquad(squad, coachId);
            }
            else if (isUpdate == IsUpdata.PlayerIsDeleted)
            {
                DeletePlayerFromMySquad(squadId, playerId);
            }
            else if (isUpdate == IsUpdata.CoachIsDeleted)
            {
                DeleteCoachFromMySquad(squad, coachId);
            }

            if (isUpdate != IsUpdata.IsNotUpdate)
            {
                //Реализация через процедуру БД
                //_appDBContext.Database.ExecuteSqlInterpolated($"CALL updateSquadRating({squadId})");

                //Реализация через C#
                UpdateMySquadRating(squad);

                //Реализация через триггеры;
            }

            return _squadRepository.GetByID(squadId);
        }
    }
}
