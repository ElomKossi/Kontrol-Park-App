CREATE TABLE [dbo].[Fournisseur] (
    [IdFournisseur]      INT          IDENTITY (1, 1) NOT NULL,
    [LibelleFournisseur] VARCHAR (50) NULL,
    [AdresseFournisseur] VARCHAR (50) NULL,
    [EmailFournisseur]   VARCHAR (50) NULL,
    [TelFournisseur]     VARCHAR (15) NULL,
    [Supprimer]          BIT          NULL,
    [DateSupprimer]      DATETIME     NULL,
    PRIMARY KEY CLUSTERED ([IdFournisseur] ASC)
);



