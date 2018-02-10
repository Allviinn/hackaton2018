CREATE TABLE User (
    Id                               INT IDENTITY(1,1) NOT NULL,
    LastName                         NVARCHAR(MAX),
    FirstName                        NVARCHAR(MAX),
    [Login]                          NVARCHAR(MAX),
    [Password]                       NVARCHAR(MAX),
    CreationDate                     DATETIME NOT NULL,
    CONSTRAINT PkUser PRIMARY KEY (Id)
);

CREATE TABLE Parcelle (
	IdParcelle                       INT IDENTITY(1,1) NOT NULL,
	Nom                              NVARCHAR(MAX),
	Ville                            NVARCHAR(MAX),
	Adresse                          NVARCHAR(MAX),
	Lat                              NVARCHAR(MAX),
	Lng                              NVARCHAR(MAX),
	CreationDate                     DATETIME          NOT NULL
	CONSTRAINT PkParcelle PRIMARY KEY (IdParcelle)
);

CREATE TABLE Evenement (
	IdEvenement                      INT IDENTITY(1,1) NOT NULL,
	CreationDate                     DATETIME          NOT NULL,
	ParcelleId                       INT               NOT NULL,
	EvenementParcelleId              INT               NOT NULL,
	CONSTRAINT PkEvenement PRIMARY KEY (IdEvenement)
);

CREATE TABLE EvenementParcelle (
	IdEvenementParcelle              INT IDENTITY(1,1) NOT NULL,
	Nom                              NVARCHAR(MAX),
	Description                      NVARCHAR(MAX)
	CONSTRAINT PkEvenementParcelle PRIMARY KEY (IdEvenementParcelle)
);


-- Evenement
ALTER TABLE Evenement ADD CONSTRAINT FkEvenementParcelle FOREIGN KEY (ParcelleId) REFERENCES Parcelle(IdParcelle);
ALTER TABLE Evenement ADD CONSTRAINT FkEvenementEvenementParcelle FOREIGN KEY (EvenementParcelleId) REFERENCES EvenementParcelle(IdEvenementParcelle);

-- User Data 
insert into [User] (LastName, FirstName, [Login], [Password], CreationDate) values ('La Fronde', 'Thierry', 'aaa', 'aaa', '12/08/1999');

-- EvenementParcelle Data 
insert into [EvenementParcelle] (Nom, Description) values 
('Arroser', 'Arroser'),
('Planter', 'Planter'),
('Labourer', 'Labourer');