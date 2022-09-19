using System;
using db_cp.Models;
using db_cp.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace db_cp.Mocks
{
    public class DataMock
    {
        static public List<Club> _clubs = new List<Club>()
        {
            new Club {
                Id = 1,
                Name = "Paris Saint-Germain",
                Country = "France",
                FoundationDate = 1970
            },
            new Club {
                Id = 2,
                Name = "Manchester United",
                Country = "England",
                FoundationDate = 1878
            }
        };

        static public List<Coach> _coaches = new List<Coach>()
        {
            new Coach {
                Id = 1,
                Surname = "Guardiola",
                Country = "Spain",
                WorkExperience = 15
            },
            new Coach {
                Id = 2,
                Surname = "Klopp",
                Country = "Germany",
                WorkExperience = 21
            },
            new Coach {
                Id = 3,
                Surname = "Zidane",
                Country = "France",
                WorkExperience = 9
            }
        };

        static public List<Player> _players = new List<Player>()
        {
            new Player {
                Id = 1,
                ClubId = 1,
                Surname = "Messi",
                Rating = 93,
                Country = "Argentina",
                Price = 250000
            },
            new Player {
                Id = 2,
                ClubId = 2,
                Surname = "Ronaldo",
                Rating = 91,
                Country = "Portugal",
                Price = 110000
            },
            new Player {
                Id = 3,
                ClubId = 1,
                Surname = "Mbappe",
                Rating = 91,
                Country = "France",
                Price = 180000
            }
        };

        static public List<SquadPlayer> _squadPlayer = new List<SquadPlayer>();

        static public List<Squad> _squads = new List<Squad>()
        {
            new Squad {
                Id = 1,
                CoachId = 1,
                Name = "Legend 17",
                Rating = 90
            },
            new Squad {
                Id = 2,
                CoachId = 2,
                Name = "Champions",
                Rating = 82
            },
            new Squad {
                Id = 3,
                CoachId = 3,
                Name = "Pink Rabbit",
                Rating = 86
            }
        };

        static public List<User> _users = new List<User>()
        {
            new User {
                Id = 1,
                Login = "aaa",
                Password = "111",
                Permission = "admin"
            },
            new User {
                Id = 2,
                Login = "bbb",
                Password = "222",
                Permission = "user"
            },
            new User {
                Id = 3,
                Login = "ccc",
                Password = "333",
                Permission = "user"
            }
        };
    }
}
