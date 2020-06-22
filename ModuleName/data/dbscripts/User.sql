CREATE TABLE public."Users"
(
    "Id" uuid NOT NULL DEFAULT uuid_generate_v4(),
    "Name" character varying(255) COLLATE pg_catalog."default" NOT NULL,
    "Age" integer NOT NULL,
    "SkillId" uuid,
    "AddressId" uuid,
    "Type" character varying(255) COLLATE pg_catalog."default",
	"LastModifiedAt" timestamp,
    CONSTRAINT users_pkey PRIMARY KEY ("Id")
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE public."Users"
    OWNER to asetu_admin;