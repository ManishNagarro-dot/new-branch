-- Table: public.employers

-- DROP TABLE public.employers;

CREATE TABLE public."Employers"
(
    "Id" uuid NOT NULL DEFAULT uuid_generate_v4(),
    "Name" character varying(255) COLLATE pg_catalog."default" NOT NULL,
    "AddressId" uuid,
    "Type" character varying(255) COLLATE pg_catalog."default",
	"LastModifiedAt" timestamp,
    CONSTRAINT employers_pkey PRIMARY KEY ("Id")
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE public."Employers"
    OWNER to asetu_admin;