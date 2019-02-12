CREATE TABLE [dbo].[Connexion] (
    [IdConnexion]    INT          IDENTITY (1, 1) NOT NULL,
    [DebutConnexion] DATETIME     NULL,
    [FinConnexion]   DATETIME     NULL,
    [SystemeOS]      VARCHAR (50) NULL,
    [Navigateur]     VARCHAR (50) NULL,
    [AdresseIP]      VARCHAR (50) NULL,
    [AdresseMac]     VARCHAR (50) NULL,
    [Machine]        VARCHAR (50) NULL,
    [IdUser]         INT          NULL,
    [Supprimer]      BIT          NULL,
    [DateSupprimer]  DATETIME     NULL,
    PRIMARY KEY CLUSTERED ([IdConnexion] ASC),
    CONSTRAINT [FK_Connexion_Utilisateur] FOREIGN KEY ([IdUser]) REFERENCES [dbo].[Utilisateur] ([IdUser])
);



