CREATE TABLE [dbo].[Profil] (
    [IdProfil]          INT          IDENTITY (1, 1) NOT NULL,
    [LibelleProfil]     VARCHAR (50) NULL,
    [ActifProfil]       BIT          NULL,
    [Actif]             BIT          NULL,
    [DateDesactivation] DATETIME     NULL,
    PRIMARY KEY CLUSTERED ([IdProfil] ASC)
);



