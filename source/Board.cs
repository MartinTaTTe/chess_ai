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
        public Board(string str)
        {
            foreach (char c in str)
            {
                int i = 0;
                switch (c)
                {
                    case 'r':
                        currentPosition[i / 8, i % 8] = new Rook(true, i % 8, i / 8);
                        break;
                    case 'k':
                        currentPosition[i / 8, i % 8] = new Knight(true, i % 8, i / 8);
                        break;
                    case 'b':
                        currentPosition[i / 8, i % 8] = new Bishop(true, i % 8, i / 8);
                        break;
                    case 'q':
                        currentPosition[i / 8, i % 8] = new Queen(true, i % 8, i / 8);
                        break;
                    case 'x':
                        currentPosition[i / 8, i % 8] = new King(true, i % 8, i / 8);
                        break;
                    case 'p':
                        currentPosition[i / 8, i % 8] = new Pawn(true, i % 8, i / 8);
                        break;
                    case 'R':
                        currentPosition[i / 8, i % 8] = new Rook(false, i % 8, i / 8);
                        break;
                    case 'K':
                        currentPosition[i / 8, i % 8] = new Knight(false, i % 8, i / 8);
                        break;
                    case 'B':
                        currentPosition[i / 8, i % 8] = new Bishop(false, i % 8, i / 8);
                        break;
                    case 'Q':
                        currentPosition[i / 8, i % 8] = new Queen(false, i % 8, i / 8);
                        break;
                    case 'X':
                        currentPosition[i / 8, i % 8] = new King(false, i % 8, i / 8);
                        break;
                    case 'P':
                        currentPosition[i / 8, i % 8] = new Pawn(false, i % 8, i / 8);
                        break;
                    case ' ':
                        break;
                    default:
                        Console.WriteLine("Invalid character detected while constructing new board.");
                        break;
                }
                i++;
            }
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
            GetPieceAt(x_des, y_des).Moved();
            GetPieceAt(x_des, y_des).SetC(x_des, y_des);
        }
        public void MovePiece(int[] c_org, int[] c_des)
        {
            MovePiece(c_org[0], c_org[1], c_des[0], c_des[1]);
        }
        public void MovePiece(int[] c)
        {
            MovePiece(c[0], c[1], c[2], c[3]);
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

        public bool Castle(bool whitePlayer, bool left)
        {
            if (whitePlayer && IsOccupiedAt(4, 7) && GetPieceAt(4, 7).Type("king") && !GetPieceAt(4, 7).HasMoved())
            { 
                if (left)
                { // check if rook is in position
                    if (IsOccupiedAt(0, 7)&& GetPieceAt(0, 7).Type("rook")&& !GetPieceAt(0, 7).HasMoved())
                    { // check the path is clear
                        if (!(IsOccupiedAt(1, 7) || IsOccupiedAt(2, 7) || IsOccupiedAt(3, 7)) && Array.Exists(Logic.Threats(this, !whitePlayer, true), c => c[0] == 2 && c[1] == 7))
                        {
                            MovePiece(0, 7, 3, 7);
                            MovePiece(4, 7, 2, 7);
                            return true;
                        }
                    }
                }
                else
                { // check if rook is in position
                    if (IsOccupiedAt(7, 7) && GetPieceAt(7, 7).Type("rook") && !GetPieceAt(7, 7).HasMoved())
                    { // check the path is clear
                        if (!(IsOccupiedAt(5, 7) || IsOccupiedAt(6, 7)) && Array.Exists(Logic.Threats(this, !whitePlayer, true), c => c[0] == 6 && c[1] == 7))
                        {
                            MovePiece(7, 7, 5, 7);
                            MovePiece(4, 7, 6, 7);
                            return true;
                        }
                    }

                }
            }
            else 
            {
                if (IsOccupiedAt(4, 0) && GetPieceAt(4, 0).Type("king") && !GetPieceAt(4, 0).HasMoved())
                { // check if rook is in position
                    if (left)
                    {
                        if (IsOccupiedAt(0, 0) && GetPieceAt(0, 0).Type("rook") && !GetPieceAt(0, 0).HasMoved())
                        { // check the path is clear
                            if (!(IsOccupiedAt(1, 0) || IsOccupiedAt(2, 0) || IsOccupiedAt(3, 0)) && Array.Exists(Logic.Threats(this, !whitePlayer, true), c => c[0] == 2 && c[1] == 0))
                            {
                                MovePiece(0, 0, 3, 0);
                                MovePiece(4, 0, 2, 0);
                                return true;
                            }
                        }
                    }
                    else
                    { // check if rook is in position
                        if (IsOccupiedAt(7, 0) && GetPieceAt(7, 0).Type("rook") && !GetPieceAt(7, 0).HasMoved())
                        { // check the path is clear
                            if (!(IsOccupiedAt(5, 0) || IsOccupiedAt(6, 0)) && Array.Exists(Logic.Threats(this, !whitePlayer, true), c => c[0] == 6 && c[1] == 0))
                            {
                                MovePiece(7, 0, 5, 0);
                                MovePiece(4, 0, 6, 0);
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }
    }
}