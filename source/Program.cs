using System;

namespace source
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game(new Player(), new Player(), new Board());
            View view = new View(game);
            string message = "";

            while (game.Playing())
            {
                Console.WriteLine(view.Show());
                Console.WriteLine(game.Action(Console.ReadLine()));
            }
        }
    }
}
