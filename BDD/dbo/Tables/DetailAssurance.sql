CREATE TABLE [dbo].[DetailAssurance] (
    [IdDetailAssurance]   INT      NOT NULL,
    [IdAssurance]         INT      NULL,
    [IdVehicule]          INT      NULL,
    [DataDebutCouverture] DATETIME NULL,
    [DateFinCouverture]   DATETIME NULL,
    PRIMARY KEY CLUSTERED ([IdDetailAssurance] ASC),
    CONSTRAINT [FK_DetailAssurance_Assurance] FOREIGN KEY ([IdAssurance]) REFERENCES [dbo].[Assurance] ([IdAssurance]),
    CONSTRAINT [FK_DetailAssurance_Vehicule] FOREIGN KEY ([IdVehicule]) REFERENCES [dbo].[Vehicule] ([IdVehicule])
);


