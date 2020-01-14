namespace source
{
    class Board
    {
        private Piece[,] defaultPieces = new Piece[8, 8]
        {
            { new Rook(true, 0, 0), new Knight(true, 1, 0), new Bishop(true, 2, 0), new Queen(true, 3, 0), new King(true, 4, 0), new Bishop(true, 5 ,0), new Knight(true, 6, 0), new Rook(true, 7, 0) },
            { new Pawn(true, 0, 1), new Pawn(true, 1, 1), new Pawn(true, 2, 1), new Pawn(true, 3, 1), new Pawn(true, 4, 1), new Pawn(true, 5, 1), new Pawn(true, 6, 1), new Pawn(true, 7, 1) },
            { null, null, null, null, null, null, null, null },
            { null, null, null, null, null, null, null, null },
            { null, null, null, null, null, null, null, null },
            { null, null, null, null, null, null, null, null },
            { new Pawn(false, 0, 6), new Pawn(false, 1, 6), new Pawn(false, 2, 6), new Pawn(false, 3, 6), new Pawn(false, 4, 6), new Pawn(false, 5, 6), new Pawn(false, 6, 6), new Pawn(false, 7, 6) },
            { new Rook(false, 0, 7), new Knight(false, 1, 7), new Bishop(false, 2, 7), new Queen(false, 3, 7), new King(false, 4, 7), new Bishop(false, 5 ,7), new Knight(false, 6, 7), new Rook(false, 7, 7) },
        };

        private Piece[,] pieces = new Piece[8, 8];

        void ResetBoard()
        {
            defaultPieces.CopyTo(pieces, 0);
        }
    }
}