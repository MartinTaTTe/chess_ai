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
            string returns = "";

            for (int j = 0; j < 8; j++)
            {
                for (int i = 0; i < 8; i++)
                {
                    if (game.PieceAt(i, j) == null)
                    {
                        returns += "[ ]";
                    } else
                    {
                        switch (game.PieceAt(i, j).GetType().ToString())
                        {
                            case "source.Pawn":
                                {
                                    returns += "[P]";
                                    break;
                                }
                            case "source.Rook":
                                {
                                    returns += "[R]";
                                    break;
                                }
                            case "source.Knight":
                                {
                                    returns += "[K]";
                                    break;
                                }
                            case "source.Bishop":
                                {
                                    returns += "[B]";
                                    break;
                                }
                            case "source.Queen":
                                {
                                    returns += "[Q]";
                                    break;
                                }
                            case "source.King":
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
