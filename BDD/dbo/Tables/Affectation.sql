CREATE TABLE [dbo].[Affectation] (
    [IdAffectation]   INT      IDENTITY (1, 1) NOT NULL,
    [IdMission]       INT      NULL,
    [IdVehicule]      INT      NULL,
    [IdConducteur]    INT      NULL,
    [DateAffectation] DATETIME NULL,
    PRIMARY KEY CLUSTERED ([IdAffectation] ASC),
    CONSTRAINT [FK_Affectation_Conducteur] FOREIGN KEY ([IdConducteur]) REFERENCES [dbo].[Conducteur] ([IdConducteur]),
    CONSTRAINT [FK_Affectation_Mission] FOREIGN KEY ([IdMission]) REFERENCES [dbo].[Mission] ([IdMission]),
    CONSTRAINT [FK_Affectation_Vehicule] FOREIGN KEY ([IdVehicule]) REFERENCES [dbo].[Vehicule] ([IdVehicule])
);



