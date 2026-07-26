-- Table: public.BalkarTelemetryLogs

-- DROP TABLE IF EXISTS public."BalkarTelemetryLogs";

CREATE TABLE IF NOT EXISTS public."BalkarTelemetryLogs"
(
    "Id" integer NOT NULL DEFAULT nextval('"BalkarTelemetryLogs_Id_seq"'::regclass),
    "TarihSaat" timestamp with time zone NOT NULL,
    "Lat" double precision NOT NULL,
    "Lon" double precision NOT NULL,
    "Alt" double precision NOT NULL,
    "Pitch" double precision NOT NULL,
    "Roll" double precision NOT NULL,
    "Yaw" double precision NOT NULL,
    "Throttle" integer NOT NULL,
    "Steering" integer NOT NULL,
    "Ping" integer NOT NULL,
    CONSTRAINT "BalkarTelemetryLogs_pkey" PRIMARY KEY ("Id")
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public."BalkarTelemetryLogs"
    OWNER to neondb_owner;