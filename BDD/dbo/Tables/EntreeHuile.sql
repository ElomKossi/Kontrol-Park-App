CREATE TABLE [dbo].[EntreeHuile] (
    [IdEntreeHuile]       INT        IDENTITY (1, 1) NOT NULL,
    [DateEntreeHuile]     DATETIME   NULL,
    [QuantiteEntreeHuile] FLOAT (53) NULL,
    [IdMecanicien]        INT        NULL,
    [IdHuile]             INT        NULL,
	[Supprimer] BIT NULL, 
    [DateSupprime] DATETIME NULL, 
    PRIMARY KEY CLUSTERED ([IdEntreeHuile] ASC),
    CONSTRAINT [FK_TechHuile_Huile] FOREIGN KEY ([IdHuile]) REFERENCES [dbo].[Huile] ([IdHuile]),
    CONSTRAINT [FK_TechHuile_Technicien] FOREIGN KEY ([IdMecanicien]) REFERENCES [dbo].[Mecanicien] ([IdMecanicien])
);



