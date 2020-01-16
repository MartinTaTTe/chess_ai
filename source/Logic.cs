using System.Collections.Generic;

namespace source
{
    static class Logic // Game logic for what moves can be made
    {
        public static bool IsTile(int x, int y)
        {
            return !(x < 0 || x > 7 || y < 0 || y > 7);
        }
        public static bool IsCheck(Board board, bool whitePlayer) // TODO: implement
        {
            return false;
        }
        public static int[][] PossibleMoves(Board board, Piece piece, bool whitePlayer) // Returns an array of coorrdinates. {-1}: no piece, {-2}:something went wrong in this
        {
            int paths = piece.MovementPattern().Length;
            List<int[]> list = new List<int[]>();
            int[] targetTile;
            bool pawn = piece.GetType().ToString() == "source.Pawn";
            
            if (pawn)
            {
                for (int p = 0; p < paths; p++)
                {
                    if (p == 0)
                    {
                        for (int r = 0; r < piece.MovementPattern()[p].Length; r++)
                        {
                            targetTile = piece.MovementPattern()[p][r];
                            if (!board.IsOccupiedAt(targetTile) && !IsCheck(board.AfterMove(piece.GetC(), targetTile), whitePlayer))
                                list.Add(targetTile);
                        }
                    }
                    else
                    {
                        targetTile = piece.MovementPattern()[p][0];
                        if (board.IsOccupiedAt(targetTile) && (board.GetPieceAt(targetTile).IsWhite() ^ piece.IsWhite()) && !IsCheck(board.AfterMove(piece.GetC(), targetTile), whitePlayer))
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
                            if ((board.GetPieceAt(targetTile).IsWhite() ^ piece.IsWhite()) && !IsCheck(board.AfterMove(piece.GetC(), targetTile), whitePlayer))
                                list.Add(targetTile);
                            break;
                        } 
                        else
                        {
                            if (!IsCheck(board.AfterMove(piece.GetC(), targetTile), whitePlayer))
                                list.Add(targetTile);
                        }
                    }
                }
            }
            return list.ToArray();
        }
    }
}