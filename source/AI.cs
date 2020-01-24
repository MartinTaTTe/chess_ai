using System;

namespace source
{
    class AI
    {
        public readonly bool white;
        private readonly int depth;

        public AI(bool white)
        {
            this.white = white;
            depth = 0;
        }
        public AI(bool white, int depth)
        {
            this.white = white;
            this.depth = depth;
        }

        public int[] MakePlay(Board board)
        {
            return BestMove(board, depth);
        }

        private int[] BestMove(Board board, int left)
        {
            int[] bestMove = new int[4];
            double max = 0;
            int[][] moves = Logic.AllPossibleMoves(board, white);
            foreach (int[] m in moves)
            {
                Board newBoard = new Board(board.AfterMove(m));
                int[][] enemyMoves = Logic.AllPossibleMoves(newBoard, !white);
                foreach (int[] e in enemyMoves)
                {
                    Board newNewBoard = new Board(newBoard.AfterMove(e));
                    if (left == 0)
                    {
                        double newMax = ValueBoard(newNewBoard);
                        if (max <= newMax)
                        {
                            max = newMax;
                            bestMove = m;
                        }
                    }
                    else
                        bestMove = BestMove(newNewBoard, left - 1);
                }
            }
            return bestMove;
        }

        private double ValueBoard(Board board)
        {
            double returns = 0;
            foreach (Piece piece in board.PiecesOf(white))
            {
                returns += piece.Value();
            }
            foreach (Piece piece in board.PiecesOf(!white))
            {
                returns -= piece.Value();
            }
            if (returns <= 0)
            {
                foreach (Piece piece in board.PiecesOf(white))
                {
                    int y;
                    if (white)
                        y = 20 + (8 - piece.GetY()) / 5;
                    else
                        y = 20 + piece.GetX() / 5;
                    returns += piece.Value() * y;
                }
            }
            return returns;
        }

    }
}