-- Получение нового рейтинга состава

CREATE OR REPLACE FUNCTION newSquadRating(squadId INT)
RETURNS DECIMAL 
AS $$
    BEGIN 
        IF ((
            SELECT COUNT(*)
            FROM public."SquadPlayer"
            WHERE "SquadId" = squadId) > 0) THEN

            RETURN (
                SELECT AVG("Rating")
                FROM public."SquadPlayer" AS sp
                JOIN public."Player" AS pl ON sp."PlayerId" = pl."Id"
                WHERE "SquadId" = squadId
            );
        ELSE 
            RETURN 0;
        END IF;
    END;
$$ LANGUAGE plpgsql;

-- Обновляет рейтинг состава после добавления или удаления футболиста (реализация с помощью триггеров)

CREATE OR REPLACE FUNCTION updateSquadRating()
RETURNS TRIGGER 
AS $$
    BEGIN
        IF (TG_OP = 'INSERT') THEN
            UPDATE public."Squad" 
            SET "Rating" = newSquadRating(NEW."SquadId")
            WHERE "Id" = NEW."SquadId";

            RETURN NEW;

	    ELSIF (TG_OP = 'DELETE') THEN
            UPDATE public."Squad" 
            SET "Rating" = newSquadRating(OLD."SquadId")
            WHERE "Id" = OLD."SquadId";

            RETURN OLD;
        END IF;
    END;
$$ LANGUAGE plpgsql;


CREATE TRIGGER insertRatingTrigger AFTER INSERT ON public."SquadPlayer"
    FOR EACH ROW EXECUTE PROCEDURE updateSquadRating();

CREATE TRIGGER deleteRatingTrigger AFTER DELETE ON public."SquadPlayer"
    FOR EACH ROW EXECUTE PROCEDURE updateSquadRating();

-- DROP TRIGGER IF EXISTS insertRatingTrigger ON public."SquadPlayer";
-- DROP TRIGGER IF EXISTS deleteRatingTrigger ON public."SquadPlayer";


-- Обновляет рейтинг состава после добавления или удаления футболиста (реализация с помощью процедуры)

CREATE OR REPLACE PROCEDURE updateSquadRating(squadId INT)
AS $$
	BEGIN
        UPDATE public."Squad" 
        SET "Rating" = newSquadRating(squadId)
        WHERE "Id" = squadId;
	END;
$$ LANGUAGE plpgsql;