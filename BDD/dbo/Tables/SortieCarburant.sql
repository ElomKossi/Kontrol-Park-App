CREATE TABLE [dbo].[SortieCarburant] (
    [IdSortieCarburant]     INT        IDENTITY (1, 1) NOT NULL,
    [VolumeSortieCarburant] FLOAT (53) NULL,
    [DateSortieCarburant]   DATETIME   NULL,
    [IdVehicule]            INT        NULL,
    [IdCarburant]           INT        NULL,
    [IdMecanicien]          INT        NULL,
	[Supprimer] BIT NULL, 
    [DateSupprime] DATETIME NULL, 
    PRIMARY KEY CLUSTERED ([IdSortieCarburant] ASC),
    CONSTRAINT [FK_Consommer_Carburant] FOREIGN KEY ([IdCarburant]) REFERENCES [dbo].[Carburant] ([IdCarburant]),
    CONSTRAINT [FK_Consommer_Vehiculee] FOREIGN KEY ([IdVehicule]) REFERENCES [dbo].[Vehicule] ([IdVehicule]),
    CONSTRAINT [FK_SortieCarburant_Technicien] FOREIGN KEY ([IdMecanicien]) REFERENCES [dbo].[Mecanicien] ([IdMecanicien])
);



