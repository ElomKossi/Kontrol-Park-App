CREATE TABLE [dbo].[Intervention] (
    [IdIntervention]            INT          IDENTITY (1, 1) NOT NULL,
    [LibelleIdIntervention]     VARCHAR (50) NULL,
    [DescriptionIdIntervention] TEXT         NULL,
    [DateDebut]                 DATETIME     NULL,
    [DateFin]                   DATETIME     NULL,
    [IdTypeIntervention]        INT          NULL,
    [Supprimer]                 BIT          NULL,
    [DateSupprimer]             DATETIME     NULL,
    [IdVehicule]                INT          NULL,
    PRIMARY KEY CLUSTERED ([IdIntervention] ASC),
    CONSTRAINT [FK_Intervention_TypeIntervention] FOREIGN KEY ([IdTypeIntervention]) REFERENCES [dbo].[TypeIntervention] ([IdTypeIntervention]),
    CONSTRAINT [FK_Intervention_Vehicule] FOREIGN KEY ([IdVehicule]) REFERENCES [dbo].[Vehicule] ([IdVehicule])
);



