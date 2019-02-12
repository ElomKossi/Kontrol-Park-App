CREATE TABLE [dbo].[Droit] (
    [IdDroit]       INT          IDENTITY (1, 1) NOT NULL,
    [LibelleDroit]  VARCHAR (50) NULL,
    [ActifDroit]    BIT          NULL,
    [Supprimer]     BIT          NULL,
    [DateSupprimer] DATETIME     NULL,
    PRIMARY KEY CLUSTERED ([IdDroit] ASC)
);



