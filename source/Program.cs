using System;

namespace source
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game(new Player(), new Player(), new Board());
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
    }
}
