CREATE TABLE [dbo].[Mecanicien] (
    [IdMecanicien]             INT          IDENTITY (1, 1) NOT NULL,
    [NumCNIMecanicien]         VARCHAR (50) NULL,
    [NomMecanicien]            VARCHAR (50) NULL,
    [PrenomMecanicien]         VARCHAR (50) NULL,
    [DateNaissanceMecanicien]  DATETIME     NULL,
    [LieuxNaissanceMecanicien] VARCHAR (50) NULL,
    [SexeMecanicien]           VARCHAR (50) NULL,
    [AdresseMecanicien]        VARCHAR (50) NULL,
    [TelMecanicien]            VARCHAR (15) NULL,
    [EmailMecanicien]          VARCHAR (50) NULL,
    [DateEmbaucheMecanicien]   DATETIME     NULL,
    [EnActivite]               BIT          NULL,
    [DateDesactivation]        DATETIME     NULL,
    PRIMARY KEY CLUSTERED ([IdMecanicien] ASC)
);



