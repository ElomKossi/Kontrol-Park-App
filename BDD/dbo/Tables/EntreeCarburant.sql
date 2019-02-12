CREATE TABLE [dbo].[EntreeCarburant] (
    [IdEntreeCarburant]   INT        IDENTITY (1, 1) NOT NULL,
    [DateEntreeCarburant] DATETIME   NULL,
    [QuantiteEntreeCarb]  FLOAT (53) NULL,
    [IdMecanicien]        INT        NULL,
    [IdCarburant]         INT        NULL,
    [Supprimer] BIT NULL, 
    [DateSupprime] DATETIME NULL, 
    PRIMARY KEY CLUSTERED ([IdEntreeCarburant] ASC),
    CONSTRAINT [FK_TechCarburant_Carburant] FOREIGN KEY ([IdCarburant]) REFERENCES [dbo].[Carburant] ([IdCarburant]),
    CONSTRAINT [FK_TechCarburant_Technicien] FOREIGN KEY ([IdMecanicien]) REFERENCES [dbo].[Mecanicien] ([IdMecanicien])
);



