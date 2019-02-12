CREATE TABLE [dbo].[TypeCarburant] (
    [IdTypeCarb]      INT          IDENTITY (1, 1) NOT NULL,
    [LibelleTypeCarb] VARCHAR (50) NULL,
    [Supprimer]       BIT          NULL,
    [DateSupprimer]   DATETIME     NULL,
    PRIMARY KEY CLUSTERED ([IdTypeCarb] ASC)
);



