CREATE TABLE [dbo].[Utilisateur] (
    [IdUser]                 INT           IDENTITY (1, 1) NOT NULL,
    [NumCNIUser]             VARCHAR (50)  NULL,
    [NomUser]                VARCHAR (50)  NULL,
    [PrenomUser]             VARCHAR (50)  NULL,
    [DateNaissanceUser]      DATETIME      NULL,
    [LieuxNaissanceUser]     VARCHAR (50)  NULL,
    [SexeUser]               VARCHAR (50)  NULL,
    [AdresseUser]            VARCHAR (50)  NULL,
    [TelUser]                VARCHAR (15)  NULL,
    [EmailUser]              VARCHAR (50)  NULL,
    [DateEmbaucheUser]       DATETIME      NULL,
    [DateCreationCompteUser] DATETIME      NULL,
    [Identifiant]            VARCHAR (50)  NULL,
    [MotDePasse]             VARCHAR (200) NULL,
    [DateModificationPass]   DATETIME      NULL,
    [Tentative]              INT           NULL,
    [EstConnecte]            BIT           NULL,
    [EnActivite]             BIT           NULL,
    [DateDesactivation]      DATETIME      NULL,
    [IdProfil]               INT           NULL,
    PRIMARY KEY CLUSTERED ([IdUser] ASC),
    CONSTRAINT [FK_Utilisateur_Profil] FOREIGN KEY ([IdProfil]) REFERENCES [dbo].[Profil] ([IdProfil])
);



