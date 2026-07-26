-- Table: public.telemetry_logs

-- DROP TABLE IF EXISTS public.telemetry_logs;

CREATE TABLE IF NOT EXISTS public.telemetry_logs
(
    id integer NOT NULL DEFAULT nextval('telemetry_logs_id_seq'::regclass),
    tarih_saat timestamp without time zone DEFAULT CURRENT_TIMESTAMP,
    anlik_hiz integer NOT NULL,
    yon_acisi character varying(50) COLLATE pg_catalog."default" NOT NULL,
    surus_modu character varying(50) COLLATE pg_catalog."default" NOT NULL,
    "IsArmed" boolean NOT NULL DEFAULT false,
    "PixhawkLink" character varying(50) COLLATE pg_catalog."default",
    "LatencyMs" integer NOT NULL DEFAULT 0,
    "MotorLPwm" integer NOT NULL DEFAULT 0,
    "MotorRPwm" integer NOT NULL DEFAULT 0,
    "SteerPwm" integer NOT NULL DEFAULT 0,
    "RpiCpuTemp" double precision NOT NULL DEFAULT 0,
    "SystemLoadPct" integer NOT NULL DEFAULT 0,
    CONSTRAINT telemetry_logs_pkey PRIMARY KEY (id)
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.telemetry_logs
    OWNER to neondb_owner;