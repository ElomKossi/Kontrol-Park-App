CREATE TABLE [dbo].[TypeIntervention] (
    [IdTypeIntervention]      INT          IDENTITY (1, 1) NOT NULL,
    [LibelleTypeIntervention] VARCHAR (50) NULL,
    [Supprimer]               BIT          NULL,
    PRIMARY KEY CLUSTERED ([IdTypeIntervention] ASC)
);



