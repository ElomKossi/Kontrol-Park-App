CREATE TABLE [dbo].[Huile] (
    [IdHuile]       INT          IDENTITY (1, 1) NOT NULL,
    [LibelleHuile]  VARCHAR (50) NULL,
    [VolumeHuile]   FLOAT (53)   NULL,
    [Supprimer]     BIT          NULL,
    [DateSupprimer] DATETIME     NULL,
    PRIMARY KEY CLUSTERED ([IdHuile] ASC)
);



