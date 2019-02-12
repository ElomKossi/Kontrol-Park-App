CREATE TABLE [dbo].[SortieHuile] (
    [IdSortieHuile]     INT        IDENTITY (1, 1) NOT NULL,
    [VolumeSortieHuile] FLOAT (53) NULL,
    [DateSortieHuile]   DATETIME   NULL,
    [IdVehicule]        INT        NULL,
    [IdHuile]           INT        NULL,
    [IdMecanicien]      INT        NULL,
	[Supprimer] BIT NULL, 
    [DateSupprime] DATETIME NULL, 
    PRIMARY KEY CLUSTERED ([IdSortieHuile] ASC),
    CONSTRAINT [FK_ConsommerHuile_Huile] FOREIGN KEY ([IdHuile]) REFERENCES [dbo].[Huile] ([IdHuile]),
    CONSTRAINT [FK_ConsommerHuile_Vehicule] FOREIGN KEY ([IdVehicule]) REFERENCES [dbo].[Vehicule] ([IdVehicule]),
    CONSTRAINT [FK_SortieHuile_Technicien] FOREIGN KEY ([IdMecanicien]) REFERENCES [dbo].[Mecanicien] ([IdMecanicien])
);



