namespace source
{
    class Board
    {
        public Board()
        {
            ResetBoard();
        }
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

        public void ResetBoard()
        {
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    pieces[i, j] = defaultPieces[i, j];
        }

        void WhitePlays()
        {

        }

        public bool IsOccupiedAt(int x, int y)
        {
            return pieces[y, x] != null;
        }

        public Piece PieceAt(int x, int y)
        {
            return pieces[y, x];
        }

        public bool CanMoveTo(int x_org, int y_org, int x_des, int y_des)
        {
            switch (pieces[y_org, x_org].GetType().ToString())
            {
                case "source.Pawn":
                    {
                        return false;
                    }
                case "source.Rook":
                    {
                        return false;
                    }
                case "source.Knight":
                    {
                        return false;
                    }
                case "source.Bishop":
                    {
                        return false;
                    }
                case "source.Queen":
                    {
                        return false;
                    }
                case "source.King":
                    {
                        return false;
                    }
                default: return false;
            }
        }

        public void MovePiece(int x_org, int y_org, int x_des, int y_des)
        {
            pieces[y_des, x_des] = pieces[y_org, x_org];
            pieces[y_org, x_org] = null;
        }

        public void RemovePiece(int x, int y)
        {
            pieces[y, x] = null;
        }

        public void AddPiece(Piece piece, int x, int y)
        {
            pieces[y, x] = piece;
        }

    }
}