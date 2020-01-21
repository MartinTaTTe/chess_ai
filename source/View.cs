using System;

namespace source
{
    class View
    {
        public View(Game game_in)
        {
            game = game_in;
        }

        private readonly Game game;

        public void Show()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(" A B C D E F G H");
            for (int j = 0; j < 8; j++)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(8 - j);
                for (int i = 0; i < 8; i++)
                {
                    if ((i + j) % 2 == 0)
                        Console.BackgroundColor = ConsoleColor.Gray;
                    else
                        Console.BackgroundColor = ConsoleColor.DarkGray;
                    if (!game.HasPieceAt(i, j))
                        Console.Write("  ");
                    else
                    {
                        Piece piece = game.PieceAt(i, j);
                        if (piece.IsWhite())
                            Console.ForegroundColor = ConsoleColor.White;
                        else
                            Console.ForegroundColor = ConsoleColor.Black;
                        switch (piece.TypeStr())
                        {
                            case "pawn":
                                {
                                    Console.Write("P ");
                                    break;
                                }
                            case "rook":
                                {
                                    Console.Write("R ");
                                    break;
                                }
                            case "knight":
                                {
                                    Console.Write("Kn");
                                    break;
                                }
                            case "bishop":
                                {
                                    Console.Write("B ");
                                    break;
                                }
                            case "queen":
                                {
                                    Console.Write("Q ");
                                    break;
                                }
                            case "king":
                                {
                                    Console.Write("Ki");
                                    break;
                                }
                            default: break;
                        }
                    }
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                Console.Write(8 - j);
                Console.WriteLine();
            }
            Console.WriteLine(" A B C D E F G H");
        }
    }
}