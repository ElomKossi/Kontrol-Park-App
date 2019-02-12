CREATE TABLE [dbo].[TypeMission] (
    [IdTypeMission]      INT          IDENTITY (1, 1) NOT NULL,
    [LibelleTypeMission] VARCHAR (50) NULL,
    [Supprimer]          BIT          NULL,
    [DateSupprimer]      DATETIME     NULL,
    PRIMARY KEY CLUSTERED ([IdTypeMission] ASC)
);



