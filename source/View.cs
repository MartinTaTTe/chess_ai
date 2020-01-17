namespace source
{
    class View
    {
        public View(Game game_in)
        {
            game = game_in;
        }

        private readonly Game game;

        public string Show()
        {
            string returns = "  A  B  C  D  E  F  G  H \n";

            for (int j = 0; j < 8; j++)
            {
                returns += 8 - j;
                for (int i = 0; i < 8; i++)
                {
                    if (game.PieceAt(i, j) == null)
                    {
                        returns += "[ ]";
                    } else
                    {
                        switch (game.PieceAt(i, j).Type())
                        {
                            case "pawn":
                                {
                                    returns += "[P]";
                                    break;
                                }
                            case "rook":
                                {
                                    returns += "[R]";
                                    break;
                                }
                            case "knight":
                                {
                                    returns += "[K]";
                                    break;
                                }
                            case "bishop":
                                {
                                    returns += "[B]";
                                    break;
                                }
                            case "queen":
                                {
                                    returns += "[Q]";
                                    break;
                                }
                            case "king":
                                {
                                    returns += "[X]";
                                    break;
                                }
                            default: break;
                        }
                    }
                    
                }
                returns += "\n";
            }

            return returns;
        }
    }
}
