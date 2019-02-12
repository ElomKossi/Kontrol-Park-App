CREATE TABLE [dbo].[Conducteur] (
    [IdConducteur]             INT          IDENTITY (1, 1) NOT NULL,
    [NumCNIConducteur]         VARCHAR (50) NULL,
    [NomConducteur]            VARCHAR (50) NULL,
    [PrenomConducteur]         VARCHAR (50) NULL,
    [DateNaissanceConducteur]  DATETIME     NULL,
    [LieuxNaissanceConducteur] VARCHAR (50) NULL,
    [SexeConducteur]           VARCHAR (50) NULL,
    [AdresseConducteur]        VARCHAR (50) NULL,
    [TelConducteur]            VARCHAR (15) NULL,
    [EmailConducteur]          VARCHAR (50) NULL,
    [DateEmbaucheConducteur]   DATETIME     NULL,
	[NumPermis]				   VARCHAR (50) NULL,
    [DateExpire]			   DATETIME     NULL,
    [EnActivite]               BIT          NULL,
    [EnMission]                BIT          NULL,
    [DateDesactivation]        DATETIME     NULL,
    PRIMARY KEY CLUSTERED ([IdConducteur] ASC)
);



