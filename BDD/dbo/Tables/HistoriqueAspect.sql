CREATE TABLE [dbo].[HistoriqueAspect] (
    [Id]         INT      IDENTITY (1, 1) NOT NULL,
    [IdVehicule] INT      NULL,
    [IdAspect]   INT      NULL,
    [DateMAJ]    DATETIME NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_HistoriqueAspect_AspectVehicule] FOREIGN KEY ([IdAspect]) REFERENCES [dbo].[AspectVehicule] ([IdAspect]),
    CONSTRAINT [FK_HistoriqueAspect_Vehicule] FOREIGN KEY ([IdVehicule]) REFERENCES [dbo].[Vehicule] ([IdVehicule])
);



