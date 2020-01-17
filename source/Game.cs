using System;
using System.Linq;

namespace source
{
    class Game
    {
        private Player white;
        private Player black;
        private readonly Board board = new Board();
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
            return board.IsOccupiedAt(x, y) && !(whiteTurn ^ board.GetPieceAt(x, y).IsWhite());
        }

        public bool Playing()
        {
            return playing;
        }

        public Piece PieceAt(int x, int y)
        {
            if (board.IsOccupiedAt(x, y))
                return board.GetPieceAt(x, y);
            else return null;
        }

        private bool MakePlay(string input)
        {
            if (!(input[0] >= 'a' && input[0] <= 'h' &&
                input[1] >= '1' && input[1] <= '8' &&
                input[2] >= 'a' && input[2] <= 'h'&&
                input[3] >= '1' && input[3] <= '8'))
            {
                return false;
            }
            else
            {
                int x_org = input[0] - 97;
                int y_org = 56 - input[1];
                int x_des = input[2] - 97;
                int y_des = 56 - input[3];
                if (OwnPiece(x_org, y_org))
                {
                    if (Array.Exists(Logic.PossibleMoves(board, board.GetPieceAt(x_org, y_org)), c => c[0] == x_des && c[1] == y_des))
                    {
                        board.MovePiece(x_org, y_org, x_des, y_des);
                        board.GetPieceAt(x_des, y_des).HasMoved();
                        board.GetPieceAt(x_des, y_des).SetCoords(x_des, y_des);
                        whiteTurn = !whiteTurn;
                        return true;
                    }
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
                    returns = "Board reset.";
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
