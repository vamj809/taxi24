--- Create "Database Schema" ---
CREATE SCHEMA taxi24db
    AUTHORIZATION qoszeacd;
	
--- Table.Conductores ---
CREATE TABLE taxi24db."Conductores"
(
    "Disponible" boolean NOT NULL DEFAULT true,
    "ID" integer NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 ),
    "Latitud" double precision,
    "Longitud" double precision,
    CONSTRAINT "ConductorID" PRIMARY KEY ("ID")
)
WITH (
    OIDS = FALSE
);

--- Table.Pasajeros ---
CREATE TABLE taxi24db."Pasajeros"
(
    "ID" integer NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 ),
    "Latitud" double precision,
    "Longitud" double precision,
    CONSTRAINT "PasajeroID" PRIMARY KEY ("ID")
)
WITH (
    OIDS = FALSE
);
	
--- Table.Viajes ---
CREATE TABLE taxi24db."Viajes"
(
    "ID" integer NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 ),
    "PasajeroID" integer NOT NULL,
    "ConductorID" integer,
    "Completado" boolean NOT NULL DEFAULT false,
    CONSTRAINT "ViajeID" PRIMARY KEY ("ID"),
    CONSTRAINT "PasajeroViajes" FOREIGN KEY ("PasajeroID")
        REFERENCES taxi24db."Pasajeros" ("ID") MATCH SIMPLE
        ON UPDATE CASCADE
        ON DELETE CASCADE
        NOT VALID,
    CONSTRAINT "ConductorViajes" FOREIGN KEY ("ConductorID")
        REFERENCES taxi24db."Conductores" ("ID") MATCH SIMPLE
        ON UPDATE CASCADE
        ON DELETE CASCADE
        NOT VALID
)
WITH (
    OIDS = FALSE
);

--- Permissions ---
ALTER TABLE taxi24db."Conductores"
    OWNER to qoszeacd;
ALTER TABLE taxi24db."Pasajeros"
    OWNER to qoszeacd;
ALTER TABLE taxi24db."Viajes"
    OWNER to qoszeacd;