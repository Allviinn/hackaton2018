CREATE TABLE User (
    Id                               INT IDENTITY(1,1) NOT NULL,
    LastName                         NVARCHAR(MAX),
    FirstName                        NVARCHAR(MAX),
    [Login]                          NVARCHAR(MAX),
    [Password]                       NVARCHAR(MAX),
    CreationDate                     DATETIME NOT NULL,
    CONSTRAINT PkUser PRIMARY KEY (Id)
);

insert into [User] (LastName, FirstName, [Login], [Password], CreationDate) values ('La Fronde', 'Thierry', 'aaa', 'aaa', '12/08/1999');