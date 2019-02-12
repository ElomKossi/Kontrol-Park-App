CREATE TABLE [dbo].[Action] (
    [IdAction]      INT           IDENTITY (1, 1) NOT NULL,
    [LibelleAction] VARCHAR (200) NULL,
    [HeureAction]   DATETIME      NULL,
    [IdConnexion]   INT           NULL,
    PRIMARY KEY CLUSTERED ([IdAction] ASC),
    CONSTRAINT [FK_Action_Action] FOREIGN KEY ([IdConnexion]) REFERENCES [dbo].[Connexion] ([IdConnexion])
);



