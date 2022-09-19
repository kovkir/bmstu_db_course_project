-- роль Администратора

CREATE ROLE admin WITH
    SUPERUSER
    NOCREATEDB
    CREATEROLE
    NOINHERIT
    NOREPLICATION
    NOBYPASSRLS
    CONNECTION LIMIT -1
    LOGIN
    PASSWORD 'admin';

-- права доступа

GRANT ALL PRIVILEGES 
    ON ALL TABLES IN SCHEMA public 
    TO admin;

-- роль Авторизированного пользователя

CREATE ROLE _user WITH
    NOSUPERUSER
    NOCREATEDB
    NOCREATEROLE
    NOINHERIT
    NOREPLICATION
    NOBYPASSRLS
    CONNECTION LIMIT -1
    LOGIN
    PASSWORD 'user';

-- права доступа

GRANT SELECT
    ON ALL TABLES IN SCHEMA public 
    TO _user;

GRANT INSERT
    ON public."Squad", 
       public."SquadPlayer", 
       public."User"
    TO _user;

GRANT DELETE
    ON public."SquadPlayer"
    TO _user;

GRANT UPDATE
    ON public."Squad" 
    TO _user;

-- роль Гостя

CREATE ROLE guest WITH
    NOSUPERUSER
    NOCREATEDB
    NOCREATEROLE
    NOINHERIT
    NOREPLICATION
    NOBYPASSRLS
    CONNECTION LIMIT -1
    LOGIN
    PASSWORD 'guest';

-- права доступа

GRANT SELECT
    ON public."Club", 
       public."Coach", 
       public."Player", 
       public."Squad", 
       public."User"
    TO guest;

GRANT INSERT
    ON public."Squad", 
       public."User"
    TO guest;

-- Удалить права доступа

-- REVOKE SELECT 
--     ON public."Club" 
--     TO _user;

-- Удалить роль

-- DROP ROLE guest;
