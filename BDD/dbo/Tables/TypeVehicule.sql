CREATE TABLE [dbo].[TypeVehicule] (
    [IdType]        INT          IDENTITY (1, 1) NOT NULL,
    [LibelleType]   VARCHAR (50) NULL,
    [Supprimer]     BIT          NULL,
    [DateSupprimer] DATETIME     NULL,
    PRIMARY KEY CLUSTERED ([IdType] ASC)
);



