using System;
using System.Collections.Generic;

namespace source
{
    class Board // Game board with pieces and functions to (re)move and add pieces.
    {
        public Board(Board board)
        {
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    currentPosition[i, j] = board.currentPosition[i, j];
        }
        public Board()
        {
            ResetBoard();
        }

        private static readonly Piece[,] startPosition = new Piece[8, 8]
        {
            { new Rook(false, 0, 0), new Knight(false, 1, 0), new Bishop(false, 2, 0), new Queen(false, 3, 0), new King(false, 4, 0), new Bishop(false, 5 ,0), new Knight(false, 6, 0), new Rook(false, 7, 0) },
            { new Pawn(false, 0, 1), new Pawn(false, 1, 1), new Pawn(false, 2, 1), new Pawn(false, 3, 1), new Pawn(false, 4, 1), new Pawn(false, 5, 1), new Pawn(false, 6, 1), new Pawn(false, 7, 1) },
            { null, null, null, null, null, null, null, null },
            { null, null, null, null, null, null, null, null },
            { null, null, null, null, null, null, null, null },
            { null, null, null, null, null, null, null, null },
            { new Pawn(true, 0, 6), new Pawn(true, 1, 6), new Pawn(true, 2, 6), new Pawn(true, 3, 6), new Pawn(true, 4, 6), new Pawn(true, 5, 6), new Pawn(true, 6, 6), new Pawn(true, 7, 6) },
            { new Rook(true, 0, 7), new Knight(true, 1, 7), new Bishop(true, 2, 7), new Queen(true, 3, 7), new King(true, 4, 7), new Bishop(true, 5 ,7), new Knight(true, 6, 7), new Rook(true, 7, 7) }
        };

        private readonly Piece[,] currentPosition = new Piece[8, 8];

        public void ResetBoard()
        {
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    currentPosition[i, j] = startPosition[i, j];
        }

        public Piece[] PiecesOf(bool whitePlayer)
        {
            List<Piece> list = new List<Piece>();
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (IsOccupiedAt(i, j) && !(GetPieceAt(i, j).IsWhite() ^ whitePlayer))
                        list.Add(GetPieceAt(i, j));
                }
            }
            return list.ToArray();
        }

        public bool IsOccupiedAt(int x, int y)
        {
            try
            {
                if (x < 0 || y < 0)
                {
                    throw new IndexOutOfRangeException();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught. Board.IsOccupiedAt received negative values.", e);
            }
            return currentPosition[y, x] != null;
        }
        public bool IsOccupiedAt(int[] coords)
        {
            return IsOccupiedAt(coords[0], coords[1]);
        }

        public Piece GetPieceAt(int x, int y) // use IsOccupiedAt first to check if piece exists
        {
            try
            {
                if (currentPosition[y, x] == null)
                {
                    throw new ArgumentNullException();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught. Board.GetPieceAt used without using Board.IsOccupiedAt first.", e);
            }
            
            return currentPosition[y, x];
        }
        public Piece GetPieceAt(int[] coords)
        {
            return GetPieceAt(coords[0], coords[1]);
        }

        public void MovePiece(int x_org, int y_org, int x_des, int y_des)
        {
            currentPosition[y_des, x_des] = currentPosition[y_org, x_org];
            currentPosition[y_org, x_org] = null;
        }
        public void MovePiece(int[] c_org, int[] c_des)
        {
            MovePiece(c_org[0], c_org[1], c_des[0], c_des[1]);
        }

        public Board AfterMove(int x_org, int y_org, int x_des, int y_des)
        {
            Board board = new Board(this);
            board.MovePiece(x_org, y_org, x_des, y_des);
            return board;
        }
        public Board AfterMove(int[] c_org, int[] c_des)
        {
            return AfterMove(c_org[0], c_org[1], c_des[0], c_des[1]);
        }

        public void RemovePiece(int x, int y)
        {
            currentPosition[y, x] = null;
        }
        public void RemovePiece(int[] coords)
        {
            RemovePiece(coords[0], coords[1]);
        }

        public void AddPiece(Piece piece, int x, int y)
        {
            currentPosition[y, x] = piece;
        }
        public void AddPiece(Piece piece, int[] coords)
        {
            AddPiece(piece, coords[0], coords[1]);
        }
    }
}