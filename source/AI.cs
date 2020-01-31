using System;

namespace source
{
    class AI
    {
        private readonly Random rng = new Random();
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
            double min = 0;
            int[][] moves = Logic.AllPossibleMoves(board, white);
            int[][] threats = Logic.Threats(board, white, true);
            foreach (int[] t in threats)
            {
                int newMax = board.GetPieceAt(t[0], t[1]).Value() - board.GetPieceAt(t[2], t[3]).Value();
                if (max < newMax)
                {
                    max = newMax;
                    bestMove = t;
                }
                if (max != 0)
                    return bestMove;
            }
            foreach (int[] m in moves)
            {
                Board newBoard = new Board(board.AfterMove(m));
                if (Logic.IsCheckMate(newBoard, white))
                {
                    return m;
                }
                if (ValueBoard(board) > ValueBoard(newBoard))
                {
                    continue;
                }
                int[][] enemyMoves = Logic.AllPossibleMoves(newBoard, !white);

                foreach (int[] e in enemyMoves)
                {
                    Board newNewBoard = new Board(newBoard.AfterMove(e));
                    if (ValueBoard(board) > ValueBoard(newNewBoard))
                    {
                        continue;
                    }
                    if (left == 0)
                    {
                        double newMin = ValueBoard(newNewBoard);
                        if (newMin <= min)
                            min = newMin;
                    }
                    else
                        bestMove = BestMove(newNewBoard, left - 1);
                }
                if (max < min)
                {
                    max = min;
                    bestMove = m;
                }
            }
            if (bestMove == null)
                bestMove = moves[rng.Next(moves.Length)];
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
            return returns;
        }

    }
}