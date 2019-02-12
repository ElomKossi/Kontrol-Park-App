CREATE TABLE [dbo].[Mission] (
    [IdMission]          INT          IDENTITY (1, 1) NOT NULL,
    [LibelleMission]     VARCHAR (50) NULL,
    [DateDebutMission]   DATETIME     NULL,
    [DateFinMission]     DATETIME     NULL,
    [IdCategorieMission] INT          NULL,
    [IdTypeMission]      INT          NULL,
    [Cloturer]           BIT          NULL,
    [DateCloturer]       DATETIME     NULL,
    [Supprimer]          BIT          NULL,
    [DateSupprimer]      DATETIME     NULL,
    PRIMARY KEY CLUSTERED ([IdMission] ASC),
    CONSTRAINT [FK_Mission_CategorieMission] FOREIGN KEY ([IdCategorieMission]) REFERENCES [dbo].[CategorieMission] ([IdCategorieMission]),
    CONSTRAINT [FK_Mission_TypeMission] FOREIGN KEY ([IdTypeMission]) REFERENCES [dbo].[TypeMission] ([IdTypeMission])
);



