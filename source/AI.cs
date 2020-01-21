using System;

namespace source
{
    class AI
    {
        public readonly bool white;

        public AI(bool white)
        {
            this.white = white;
        }

        public int[] MakePlay(Board board)
        {
            int[][] plays = Logic.AllPossibleMoves(board, white);
            Random rng = new Random();
            return plays[rng.Next(plays.Length)];
        }
    }
}