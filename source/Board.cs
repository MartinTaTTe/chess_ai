using System;

namespace source
{
    class Board
    {
        public Board()
        {
            ResetBoard();
        }
        private readonly Piece[,] defaultPieces = new Piece[8, 8]
        {
            { new Rook(false, 0, 0), new Knight(false, 1, 0), new Bishop(false, 2, 0), new Queen(false, 3, 0), new King(false, 4, 0), new Bishop(false, 5 ,0), new Knight(false, 6, 0), new Rook(false, 7, 0) },
            { new Pawn(false, 0, 1), new Pawn(false, 1, 1), new Pawn(false, 2, 1), new Pawn(false, 3, 1), new Pawn(false, 4, 1), new Pawn(false, 5, 1), new Pawn(false, 6, 1), new Pawn(false, 7, 1) },
            { null, null, null, null, null, null, null, null },
            { null, null, null, null, null, null, null, null },
            { null, null, null, null, null, null, null, null },
            { null, null, null, null, null, null, null, null },
            { new Pawn(true, 0, 6), new Pawn(true, 1, 6), new Pawn(true, 2, 6), new Pawn(true, 3, 6), new Pawn(true, 4, 6), new Pawn(true, 5, 6), new Pawn(true, 6, 6), new Pawn(true, 7, 6) },
            { new Rook(true, 0, 7), new Knight(true, 1, 7), new Bishop(true, 2, 7), new Queen(true, 3, 7), new King(true, 4, 7), new Bishop(true, 5 ,7), new Knight(true, 6, 7), new Rook(true, 7, 7) },
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
            try
            {
                if (pieces[y, x] == null)
                {
                    throw new ArgumentNullException();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
            }
            
            return pieces[y, x];
        }

        public bool CanMoveTo(int x_org, int y_org, int x_des, int y_des) //TODO: implement this function properly
        {
            bool returns = false;
            if (IsOccupiedAt(x_org, y_org))
            { 
                switch (pieces[y_org, x_org].GetType().ToString())
                {
                    case "source.Pawn":
                        {
                            returns = true;
                            break;
                        }
                    case "source.Rook":
                        {
                            returns = true;
                            break;
                        }
                    case "source.Knight":
                        {
                            returns = true;
                            break;
                        }
                    case "source.Bishop":
                        {
                            returns = true;
                            break;
                        }
                    case "source.Queen":
                        {
                            returns = true;
                            break;
                        }
                    case "source.King":
                        {
                            returns = true;
                            break;
                        }
                    default: break;
                }
            }
            return returns;
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