-- Table: public.AiDetectionLogs

-- DROP TABLE IF EXISTS public."AiDetectionLogs";

CREATE TABLE IF NOT EXISTS public."AiDetectionLogs"
(
    "Id" integer NOT NULL DEFAULT nextval('"AiDetectionLogs_Id_seq"'::regclass),
    "TarihSaat" timestamp without time zone NOT NULL,
    "NesneAdi" character varying(100) COLLATE pg_catalog."default" NOT NULL,
    "DogrulukOrani" integer NOT NULL,
    CONSTRAINT "AiDetectionLogs_pkey" PRIMARY KEY ("Id")
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public."AiDetectionLogs"
    OWNER to neondb_owner;