namespace source
{
    class Game
    {
        private Player white;
        private Player black;
        private Board board;
        private bool playing = true;
        private bool whiteTurn = true;

        public Game(Player white_in, Player black_in, Board board_in)
        {
            white = white_in;
            black = black_in;
            board = board_in;
        }

        public void ResetGame()
        {
            board.ResetBoard();
        }

        public bool OwnPiece(int x, int y)
        {
            return !(whiteTurn ^ board.PieceAt(x, y).IsWhite());
        }

        public bool Playing()
        {
            return playing;
        }

        public Piece PieceAt(int x, int y)
        {
            if (board.IsOccupiedAt(x, y))
                return board.PieceAt(x, y);
            else return null;
        }

        private bool MakePlay(string input)
        {
            if (!(input[0] >= '1' && input[0] <= '8' &&
                input[1] >= 'a' && input[1] <= 'h' &&
                input[2] >= '1' && input[2] <= '8' &&
                input[3] >= 'a' && input[3] <= 'h'))
            {
                return false;
            }
            else
            {
                int x_org = input[0] - 49;
                int y_org = input[1] - 97;
                int x_des = input[2] - 49;
                int y_des = input[3] - 97;
                if (board.IsOccupiedAt(x_org, y_org) && OwnPiece(x_org, y_org) && board.CanMoveTo(x_org, y_org, x_des, y_des))
                {
                    board.MovePiece(x_org, y_org, x_des, y_des);
                    whiteTurn = !whiteTurn;
                }
                return false;
            }
        }

        public string Action(string input)
        {
            string unknown = "Unknown command. Check below:\nQ to quit\nR to reset\nMove by 4-character command in format: [a-h][1-8][a-h][1-8]";
            string returns = "Error, something failed in Game.Action";
            switch(input.ToLower())
            {
                case "q": playing = false;
                    returns = "Quitting...";
                    break;
                case "r": ResetGame();
                    returns = "Resetting board...";
                    break;
                default: 
                    {
                        if (input.Length != 4)
                        {
                            returns = unknown;
                        }
                        else
                        {
                            if (!MakePlay(input))
                                returns = "You can't make this play.";
                            else returns = "Piece moved.";
                        }  
                        break;
                    }
            }
            return returns + "\n";
        }
    }
}
