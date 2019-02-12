CREATE TABLE [dbo].[CategoriePermis] (
    [IdCategoriePermis]      INT          IDENTITY (1, 1) NOT NULL,
    [LibelleCategoriePermis] VARCHAR (50) NULL,
    [Supprimer]              BIT          NULL,
    [DateSupprimer]          DATETIME     NULL,
    PRIMARY KEY CLUSTERED ([IdCategoriePermis] ASC)
);



