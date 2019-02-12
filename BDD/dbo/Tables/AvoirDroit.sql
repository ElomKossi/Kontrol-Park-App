CREATE TABLE [dbo].[AvoirDroit] (
    [IdDroitProfil] INT      IDENTITY (1, 1) NOT NULL,
    [IdDroit]       INT      NULL,
    [IdProfil]      INT      NULL,
    [Supprimer]     BIT      NULL,
    [DateSupprimer] DATETIME NULL,
    PRIMARY KEY CLUSTERED ([IdDroitProfil] ASC),
    CONSTRAINT [FK_AvoirDroit_Droit] FOREIGN KEY ([IdDroit]) REFERENCES [dbo].[Droit] ([IdDroit]),
    CONSTRAINT [FK_AvoirDroit_Profil] FOREIGN KEY ([IdProfil]) REFERENCES [dbo].[Profil] ([IdProfil])
);



