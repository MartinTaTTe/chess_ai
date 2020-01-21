using System;

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
            Game game = new Game(new Board());//, new AI(true), new AI(false));
            View view = new View(game);
            string message;

            Console.WriteLine(view.Show());

            while (game.Playing())
            {
                message = game.Action(Console.ReadLine());
                Console.Clear();
                Console.WriteLine(view.Show());
                Console.WriteLine(message);
            }
        }

        static void RunTest()
        {

        }
    }
}
