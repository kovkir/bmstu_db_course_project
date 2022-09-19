-- Поиск клубов по названию

CREATE OR REPLACE FUNCTION getClubsByName(name TEXT)
RETURNS TABLE (
    _clubId INTEGER,
    _name TEXT,
    _country TEXT,
    _foundationDate BIGINT
) 
AS $$
    BEGIN 
        IF (name IS NOT NULL) THEN
            RETURN QUERY
            SELECT *
            FROM public."Club"
            WHERE "Name" = name;
        ELSE
            RETURN QUERY
            SELECT *
            FROM public."Club";
        END IF;
    END;
$$ LANGUAGE plpgsql;

-- Поиск клубов по названия страны

CREATE OR REPLACE FUNCTION getClubsByCountry(country TEXT)
RETURNS TABLE (
    _clubId INTEGER,
    _name TEXT,
    _country TEXT,
    _foundationDate BIGINT
) 
AS $$
    BEGIN 
        IF (country IS NOT NULL) THEN
            RETURN QUERY
            SELECT *
            FROM public."Club"
            WHERE "Country" = country;
        ELSE
            RETURN QUERY
            SELECT *
            FROM public."Club";
        END IF;
    END;
$$ LANGUAGE plpgsql;

-- Поиск клубов по дате основания

CREATE OR REPLACE FUNCTION getClubsByFoundationDate(minFoundDate BIGINT, maxFoundDate BIGINT)
RETURNS TABLE (
    _clubId INTEGER,
    _name TEXT,
    _country TEXT,
    _foundationDate BIGINT
) 
AS $$
    BEGIN 
        IF (minFoundDate > 0 AND maxFoundDate = 0) THEN
            RETURN QUERY
            SELECT *
            FROM public."Club"
            WHERE "FoundationDate" >= minFoundDate;

        ELSEIF (minFoundDate = 0 AND maxFoundDate > 0) THEN
            RETURN QUERY
            SELECT *
            FROM public."Club"
            WHERE "FoundationDate" <= maxFoundDate;
            
        ELSEIF (minFoundDate > 0 AND maxFoundDate > 0) THEN
            RETURN QUERY
            SELECT *
            FROM public."Club"
            WHERE "FoundationDate" >= minFoundDate AND
                  "FoundationDate" <= maxFoundDate;
        ELSE
            RETURN QUERY
            SELECT *
            FROM public."Club";
        END IF;
    END;
$$ LANGUAGE plpgsql;

-- Поиск клубов по заданным параметрам

CREATE OR REPLACE FUNCTION getClubsByParameters(_name TEXT, _country TEXT, _minFoundDate BIGINT, _maxFoundDate BIGINT)
RETURNS TABLE (
    Id INTEGER,
    Name TEXT,
    Country TEXT,
    FoundationDate BIGINT
) 
AS $$
    BEGIN 
        RETURN QUERY
            SELECT *
            FROM getClubsByName(_name) 
        INTERSECT
            SELECT *
            FROM getClubsByCountry(_country) 
        INTERSECT
            SELECT *
            FROM getClubsByFoundationDate(_minFoundDate, _maxFoundDate);
    END;
$$ LANGUAGE plpgsql;

-- Пример запроса с вызовом функции

SELECT *
FROM getClubsByParameters('FC Barcelona', 'Spain', 1800, 2000);
