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
            /*int[][] plays = Logic.AllPossibleMoves(board, white);
            Random rng = new Random();
            return plays[rng.Next(plays.Length)];*/
        }

        private int[] BestMove(Board board, int left)
        {
            int[] bestMove = new int[4];
            int max = 0;
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
                        if (max <= ValueBoard(newNewBoard))
                            bestMove = e;
                    }
                    else
                        bestMove = BestMove(newNewBoard, left - 1);
                }
            }
            return bestMove;
        }

        private int ValueBoard(Board board) // TODO IMPLEMENT
        {
            int returns = 0;
            returns = board.PiecesOf(white).Length - board.PiecesOf(!white).Length;
            return returns;
        }
    }
}