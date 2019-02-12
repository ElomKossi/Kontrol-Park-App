CREATE TABLE [dbo].[AspectVehicule] (
    [IdAspect]      INT          IDENTITY (1, 1) NOT NULL,
    [LibelleAspect] VARCHAR (50) NULL,
    [Supprimer]     BIT          NULL,
    [DateSupprimer] DATETIME     NULL,
    PRIMARY KEY CLUSTERED ([IdAspect] ASC)
);



