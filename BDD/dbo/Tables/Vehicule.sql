CREATE TABLE [dbo].[Vehicule] (
    [IdVehicule]         INT          IDENTITY (1, 1) NOT NULL,
    [NomVehicule]        VARCHAR (50) NULL,
    [NumChassis]         VARCHAR (50) NULL,
    [Immatriculation]    VARCHAR (50) NULL,
    [DateAcquisition]    DATETIME     NULL,
    [DateExpireGarantie] DATETIME     NULL,
    [IdFournisseur]      INT          NULL,
    [IdTypeCarb]         INT          NULL,
    [IdMarque]           INT          NULL,
    [IdType]             INT          NULL,
    [EnActivite]         BIT          NULL,
    [EnMission]          BIT          NULL,
    [DateDesactivation]  DATETIME     NULL,
    PRIMARY KEY CLUSTERED ([IdVehicule] ASC),
    CONSTRAINT [FK_Vehicule_Fournisseur] FOREIGN KEY ([IdFournisseur]) REFERENCES [dbo].[Fournisseur] ([IdFournisseur]),
    CONSTRAINT [FK_Vehicule_Marque] FOREIGN KEY ([IdMarque]) REFERENCES [dbo].[MarqueVehicule] ([IdMarque]),
    CONSTRAINT [FK_Vehicule_Type] FOREIGN KEY ([IdType]) REFERENCES [dbo].[TypeVehicule] ([IdType]),
    CONSTRAINT [FK_Vehicule_TypeCarburant] FOREIGN KEY ([IdTypeCarb]) REFERENCES [dbo].[TypeCarburant] ([IdTypeCarb])
);



