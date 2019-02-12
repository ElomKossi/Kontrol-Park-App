CREATE TABLE [dbo].[CategorieMission] (
    [IdCategorieMission]      INT          IDENTITY (1, 1) NOT NULL,
    [LibelleCategorieMission] VARCHAR (50) NULL,
    [Supprimer]               BIT          NULL,
    [DateSupprimer]           DATETIME     NULL,
    PRIMARY KEY CLUSTERED ([IdCategorieMission] ASC)
);



