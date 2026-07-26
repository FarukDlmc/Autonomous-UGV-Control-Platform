-- Table: public.BalkarControlLogs

-- DROP TABLE IF EXISTS public."BalkarControlLogs";

CREATE TABLE IF NOT EXISTS public."BalkarControlLogs"
(
    "Id" integer NOT NULL DEFAULT nextval('"BalkarControlLogs_Id_seq"'::regclass),
    "TarihSaat" timestamp with time zone NOT NULL,
    "Kullanici" character varying(50) COLLATE pg_catalog."default" NOT NULL,
    "KomutTipi" character varying(50) COLLATE pg_catalog."default" NOT NULL,
    "KomutDetayi" character varying(255) COLLATE pg_catalog."default" NOT NULL,
    "Durum" character varying(50) COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT "BalkarControlLogs_pkey" PRIMARY KEY ("Id")
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public."BalkarControlLogs"
    OWNER to neondb_owner;