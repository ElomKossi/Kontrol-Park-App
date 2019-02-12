CREATE TABLE [dbo].[Assurance] (
    [IdAssurance]      INT          IDENTITY (1, 1) NOT NULL,
    [LibelleAssurance] VARCHAR (50) NULL,
    [AdresseAssurance] VARCHAR (50) NULL,
    [EmailAssurance]   VARCHAR (50) NULL,
    [TelAssurance]     VARCHAR (15) NULL,
    [Supprimer]        BIT          NULL,
    [DateSupprimer]    DATETIME     NULL,
    PRIMARY KEY CLUSTERED ([IdAssurance] ASC)
);



