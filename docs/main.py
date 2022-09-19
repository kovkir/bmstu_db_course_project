import psycopg2
from faker import *
import random 

class FootballDB:

    def __init__(self):
        try:
            self.__connection = psycopg2.connect(
                host     = 'localhost', 
                user     = 'postgres', 
                password = '1801', 
                database = 'db_cp'
                )
            self.__connection.autocommit = True
            self.__cursor = self.__connection.cursor()
            print("PostgreSQL connection opened ✅\n")

        except Exception as err:
            print("Error while working with PostgreSQL ❌\n", err)
            return

    def __del__(self):
        if self.__connection:
            self.__cursor.close()
            self.__connection.close()
            print("PostgreSQL connection closed ✅\n")

    def drop_tables(self):
        try:
            self.__cursor.execute(
                """
                --sql
                
                DROP TABLE public."Club", public."Coach", public."Player", public."Squad", public."User", public."SquadPlayer", public."__EFMigrationsHistory"
                """
            )
            print("Tables have been deleted ✅\n")

        except Exception as err:
            print("Error while working with PostgreSQL ❌\n", err)

    def copy_data(self):
        self.__cursor.execute(
                """
                --sql
                
                copy public."Club"("Name", "Country", "FoundationDate")                 from '/Users/Shared/tables/club.csv'   delimiter ',' csv;
                copy public."Coach"("Surname", "Country", "WorkExperience")             from '/Users/Shared/tables/coach.csv'  delimiter ',' csv;
                copy public."Player"("ClubId", "Surname", "Rating", "Country", "Price") from '/Users/Shared/tables/player.csv' delimiter ',' csv;
                copy public."Squad"("CoachId", "Name", "Rating")                        from '/Users/Shared/tables/squad.csv'  delimiter ',' csv;
                copy public."User"("Login", "Password", "Permission")                   from '/Users/Shared/tables/user.csv'   delimiter ',' csv;
                """
            )

def generate_player_table_data():

    f_players = open('./fifa/fifa_players.csv', 'r')
    data = f_players.read().split("\n")
    data = data[:-1]
    f_players.close()

    players = []
    for player in data:
        players.append([0] + player.split(","))

    f_clubs = open('./fifa/fifa_clubs.csv', 'r')
    data = f_clubs.read().split("\n")
    data = data[:-1]
    f_clubs.close()

    clubs = []
    for club in data:
        clubs.append(club.split(","))

    for i in range(len(clubs)):
        for j in range(len(players)):
            if clubs[i][0] == players[j][4]:
                players[j][0] = i + 1

    for i in range(len(players)):
        price = int((int(players[i][2]) - 70) * 5000 * (1 + random.randint(1, 10) / 10))
        players[i].append(price)

    new_file = open('./tables/player.csv', 'w')

    for player in players:
        line = "{0},{1},{2},{3},{4}\n".format(
            player[0], player[1], player[2], player[3], player[5])
    
        new_file.write(line)

    new_file.close()

    print("Player data was created ✅\n")

def generate_club_table_data():

    f_clubs = open('./fifa/fifa_clubs.csv', 'r')
    data = f_clubs.read().split("\n")
    data = data[:-1]
    f_clubs.close()

    clubs = []
    for club in data:
        clubs.append(club.split(","))

    for i in range(len(clubs)):
        foundation_date = random.randint(1857, 2010)
        clubs[i].append(foundation_date)

    new_file = open('./tables/club.csv', 'w')

    for club in clubs:
        line = "{0},{1},{2}\n".format(
            club[0], club[1], club[2])
    
        new_file.write(line)

    new_file.close()

    print("Club data was created ✅\n")

def generate_coach_table_data():

    faker = Faker()
    new_file = open('./tables/coach.csv', 'w')

    for i in range (1000):
        surname = faker.last_name()

        country = faker.country()
        while len(country) > 18:
            country = faker.country()

        work_experience = random.randint(1, 40)

        line = "{0},{1},{2}\n".format(surname, country, work_experience)
        new_file.write(line)
    
    new_file.close()

    print("Coach data was created ✅\n")

if __name__ == "__main__":
    db = FootballDB()

    # db.drop_tables()

    # generate_player_table_data()
    # generate_club_table_data()
    # generate_coach_table_data()

    # db.copy_data()
