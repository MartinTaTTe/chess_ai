﻿using System;
using System.Threading;

namespace source
{
    class Program
    {
        static void Main(string[] args)
        {
            RunGame(1000);
            //RunTest();
        }

        static void RunGame(int sleep)
        {
            Game game = new Game(new Board(), new AI(true, 1), new AI(false));
            View view = new View(game);
            string message;

            Console.BackgroundColor = ConsoleColor.DarkYellow;
            Console.Clear();

            while (game.Playing())
            {
                view.Show();
                Thread.Sleep(sleep);
                message = game.Action(Console.ReadLine());
                
                
                Console.WriteLine(message);
            }
        }

        static void RunTest()
        {
            Test test = new Test("xQ.R....b.P.....PP........p.PP....k.........P..................X");
            View view = new View(new Game(test.board));
            view.Show();

            test.TestThreats(false, true);
            test.TestAllPossibleMoves(true);
            test.TestIsCheck(false);
            test.TestIsCheckMate(false);
        }
    }
}
