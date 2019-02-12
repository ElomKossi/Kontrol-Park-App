CREATE TABLE [dbo].[Piece] (
    [IdPiece]       INT          IDENTITY (1, 1) NOT NULL,
    [LibellePiece]  VARCHAR (50) NULL,
    [Utilise] BIT NULL, 
    [Supprimer]     BIT          NULL,
    [DateSupprimer] DATETIME     NULL,
    PRIMARY KEY CLUSTERED ([IdPiece] ASC)
);



