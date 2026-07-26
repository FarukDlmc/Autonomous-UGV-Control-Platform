-- Table: public.BalkarSystemLogs

-- DROP TABLE IF EXISTS public."BalkarSystemLogs";

CREATE TABLE IF NOT EXISTS public."BalkarSystemLogs"
(
    "Id" integer NOT NULL DEFAULT nextval('"BalkarSystemLogs_Id_seq"'::regclass),
    "TarihSaat" timestamp with time zone NOT NULL,
    "Module" character varying(50) COLLATE pg_catalog."default" NOT NULL,
    "LogLevel" character varying(20) COLLATE pg_catalog."default" NOT NULL,
    "Message" text COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT "BalkarSystemLogs_pkey" PRIMARY KEY ("Id")
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public."BalkarSystemLogs"
    OWNER to neondb_owner;