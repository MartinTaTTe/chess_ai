using System;
using System.Collections.Generic;
using System.Text;

namespace source
{
    class Test
    {
        public Board board;
        public Test(string str)
        {
            board = new Board(str);
        }

        public void TestThreats(bool whitePlayer, bool enemy)
        {
            Console.WriteLine("Threats:");
            foreach (int[] coords in Logic.Threats(board, whitePlayer, enemy))
            {
                foreach (int i in coords)
                {
                    Console.Write($"{i} ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("End of coordinates.");
        }

        public void TestAllPossibleMoves(bool whitePlayer)
        {
            Console.WriteLine("All possible moves:");
            foreach (int[] coords in Logic.AllPossibleMoves(board, whitePlayer))
            {
                foreach (int i in coords)
                {
                    Console.Write($"{i} ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("End of coordinates.");
        }

        public void TestIsCheck(bool whitePlayer)
        {
            Console.WriteLine($"It is check: {Logic.IsCheck(board, whitePlayer)}");
        }

        public void TestIsCheckMate(bool whitePlayer)
        {
            Console.WriteLine($"It is checkmate: {Logic.IsCheckMate(board, whitePlayer)}");
        }
    }
}
