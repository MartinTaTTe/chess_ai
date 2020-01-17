using System;
using System.Collections.Generic;
using System.Linq;

namespace source
{
    static class Logic // Game logic for what moves can be made
    {
        public static bool IsTile(int x, int y)
        {
            return !(x < 0 || x > 7 || y < 0 || y > 7);
        }
        public static bool IsCheck(Board board, bool whitePlayer) // whitePlayer is the player who threats the other's king
        {
            var arr = Threats(board, whitePlayer, true);
            var arr2 = Array.Find(board.PiecesOf(!whitePlayer), p => p.Type() == "king").GetC();
            var ret = Array.Exists(arr, c => c[0] == arr2[0] && c[1] == arr2[1]);
            return ret;
        }

        public static bool IsCheckMate(Board board, bool whitePlayer) // whitePlayer is the player who threats the other's king
        {
            foreach (Piece piece in board.PiecesOf(whitePlayer))
            {
                foreach (int[] move in PossibleMoves(board, piece))
                {
                    if (!IsCheck(board.AfterMove(piece.GetC(), move), whitePlayer))
                        return false;
                }
            }
            return IsCheck(board, whitePlayer);
        }

        public static int[][] AllPossibleMoves(Board board, bool whitePlayer) // array of possible moves [x_org][y_org][x_des][y_des]
        {
            List<int[]> list = new List<int[]>();
            foreach (Piece piece in board.PiecesOf(whitePlayer))
            {
                int[][] moves = PossibleMoves(board, piece);
                for (int m = 0; m < moves.Length; m++)
                    list.Add(new int[] { piece.GetX(), piece.GetY(), moves[m][0], moves[m][1] });
            }
            return list.ToArray();
        }

        public static int[][] Threats(Board board, bool whitePlayer, bool enemy) // enemy is true if looking for threats, false if looking for guards
        {
            int[] targetTile;
            List<int[]> list = new List<int[]>();
            int paths;
            foreach (Piece piece in board.PiecesOf(whitePlayer))
            {
                paths = piece.MovementPattern().Length;
                if (piece.Type() == "pawn")
                {
                    for (int p = 1; p < paths; p++)
                    {
                        targetTile = piece.MovementPattern()[p][0];
                        if (board.IsOccupiedAt(targetTile) && ((board.GetPieceAt(targetTile).IsWhite() ^ piece.IsWhite()) ^ !enemy))
                            list.Add(targetTile);
                    }
                }
                else
                {
                    for (int p = 0; p < paths; p++)
                    {
                        for (int r = 0; r < piece.MovementPattern()[p].Length; r++)
                        {
                            targetTile = piece.MovementPattern()[p][r];
                            if (board.IsOccupiedAt(targetTile) && ((board.GetPieceAt(targetTile).IsWhite() ^ piece.IsWhite()) ^ !enemy))
                            {
                                list.Add(targetTile);
                                break;
                            }
                        }
                    }
                }
            }

            return list.ToArray();
        }

        public static int[][] PossibleMoves(Board board, Piece piece) // Returns an array of coordinates
        {
            bool whitePlayer = piece.IsWhite();
            int paths = piece.MovementPattern().Length;
            List<int[]> list = new List<int[]>();
            int[] targetTile;
            
            if (piece.Type() == "pawn")
            {
                for (int p = 0; p < paths; p++)
                {
                    if (p == 0)
                    {
                        for (int r = 0; r < piece.MovementPattern()[p].Length; r++)
                        {
                            targetTile = piece.MovementPattern()[p][r];
                            if (!board.IsOccupiedAt(targetTile) && !IsCheck(board.AfterMove(piece.GetC(), targetTile), !whitePlayer))
                                list.Add(targetTile);
                        }
                    }
                    else
                    {
                        targetTile = piece.MovementPattern()[p][0];
                        if (board.IsOccupiedAt(targetTile) && (board.GetPieceAt(targetTile).IsWhite() ^ piece.IsWhite()) && !IsCheck(board.AfterMove(piece.GetC(), targetTile), !whitePlayer))
                            list.Add(targetTile);
                    }
                }
            }
            else
            {
                for (int p = 0; p < paths; p++)
                {
                    for (int r = 0; r < piece.MovementPattern()[p].Length; r++)
                    {
                        targetTile = piece.MovementPattern()[p][r];
                        if (board.IsOccupiedAt(targetTile))
                        {
                            if ((board.GetPieceAt(targetTile).IsWhite() ^ piece.IsWhite()) && !IsCheck(board.AfterMove(piece.GetC(), targetTile), !whitePlayer))
                                list.Add(targetTile);
                            break;
                        } 
                        else
                        {
                            if (!IsCheck(board.AfterMove(piece.GetC(), targetTile), !whitePlayer))
                                list.Add(targetTile);
                        }
                    }
                }
            }
            return list.ToArray();
        }
    }
}