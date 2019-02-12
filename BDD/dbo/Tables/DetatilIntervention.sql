CREATE TABLE [dbo].[DetatilIntervention] (
    [IdInterventionPiece] INT IDENTITY (1, 1) NOT NULL,
    [IdIntervention]      INT NULL,
    [IdPiece]             INT NULL,
    PRIMARY KEY CLUSTERED ([IdInterventionPiece] ASC),
    CONSTRAINT [FK_DetatilIntervention_Intervention] FOREIGN KEY ([IdIntervention]) REFERENCES [dbo].[Intervention] ([IdIntervention]),
    CONSTRAINT [FK_DetatilIntervention_Piece] FOREIGN KEY ([IdPiece]) REFERENCES [dbo].[Piece] ([IdPiece])
);



