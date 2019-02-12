CREATE TABLE [dbo].[MarqueVehicule] (
    [IdMarque]      INT          IDENTITY (1, 1) NOT NULL,
    [LibelleMarque] VARCHAR (50) NULL,
    [Supprimer]     BIT          NULL,
    [DateSupprimer] DATETIME     NULL,
    PRIMARY KEY CLUSTERED ([IdMarque] ASC)
);



