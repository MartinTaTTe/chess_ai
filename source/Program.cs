using System;
using System.Threading;

namespace source
{
    class Program
    {
        static void Main(string[] args)
        {
            RunGame();
        }

        static void RunGame()
        {
            Game game = new Game(new Board(), new AI(true), new AI(false));
            View view = new View(game);
            string message;
            Console.BackgroundColor = ConsoleColor.DarkYellow;
            view.Show();

            while (game.Playing())
            {
                message = game.Action("p");
                Console.Clear();
                view.Show();
                Console.WriteLine(message);
                Thread.Sleep(500);
            }
        }

        static void RunTest()
        {

        }
    }
}
