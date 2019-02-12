CREATE TABLE [dbo].[MecaIntervention] (
    [IdTechIntervention] INT      IDENTITY (1, 1) NOT NULL,
    [DateIntervention]   DATETIME NULL,
    [IdMecanicien]       INT      NULL,
    [IdIntervention]     INT      NULL,
    PRIMARY KEY CLUSTERED ([IdTechIntervention] ASC),
    CONSTRAINT [FK_TechIntervention_Intervention] FOREIGN KEY ([IdIntervention]) REFERENCES [dbo].[Intervention] ([IdIntervention]),
    CONSTRAINT [FK_TechIntervention_Technicien] FOREIGN KEY ([IdMecanicien]) REFERENCES [dbo].[Mecanicien] ([IdMecanicien])
);



