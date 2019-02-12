CREATE TABLE [dbo].[Carburant] (
    [IdCarburant]      INT          IDENTITY (1, 1) NOT NULL,
    [LibelleCarburant] VARCHAR (50) NULL,
    [VolumeCarburant]  FLOAT (53)   NULL,
    [Supprimer]        BIT          NULL,
    [DateSupprimer]    DATETIME     NULL,
    PRIMARY KEY CLUSTERED ([IdCarburant] ASC)
);



