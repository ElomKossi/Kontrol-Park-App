CREATE TABLE [dbo].[ConducteurPermis] (
    [IdConducteurPermis] INT          IDENTITY (1, 1) NOT NULL,
    [IdCategoriePermis]  INT          NULL,
    [IdConducteur]       INT          NULL,
    [Supprimer]          BIT          NULL,
    [DateSupprimer]      DATETIME     NULL,
    PRIMARY KEY CLUSTERED ([IdConducteurPermis] ASC),
    CONSTRAINT [FK_ConducteurPermis_CategoriePermis] FOREIGN KEY ([IdCategoriePermis]) REFERENCES [dbo].[CategoriePermis] ([IdCategoriePermis]),
    CONSTRAINT [FK_ConducteurPermis_Conducteur] FOREIGN KEY ([IdConducteur]) REFERENCES [dbo].[Conducteur] ([IdConducteur])
);



