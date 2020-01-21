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

        private bool MakePlay(int[] input)
        {
            if (OwnPiece(input[0], input[1]))
                {
                if (Array.Exists(Logic.PossibleMoves(board, board.GetPieceAt(input[0], input[1])), c => c[0] == input[2] && c[1] == input[3]))
                {
                    board.MovePiece(input[0], input[1], input[2], input[3]);
                    whiteTurn = !whiteTurn;
                    return true;
                }
            }
            return false;
        }

        public string Action(string input)
        {
            string unknown = "Unknown command. Check below:\nQ to quit\nR to reset\nMove by 4-character command in format: [a-h][1-8][a-h][1-8]\nCL or CR to castle either to the left or to the right";
            string invalid = "You can't make this play.";
            string returns = "Error, something failed in Game.Action";
            switch(input.ToLower())
            {
                case "q":
                    playing = false;
                    returns = "Quitting...";
                    break;
                case "r":
                    ResetGame();
                    returns = "Board reset.";
                    break;
                case "cr":
                    if (board.Castle(whiteTurn, false))
                        returns = "Castle played.";
                    else
                        returns = invalid;
                    break;
                case "cl":
                    if (board.Castle(whiteTurn, true))
                        returns = "Castle played.";
                    else
                        returns = invalid;
                    break;
                default: 
                    {
                        int[] play = Logic.InputConverter(input);
                        if (play[0] == -1)
                        {
                            returns = unknown;
                        }
                        else
                        {
                            if (!MakePlay(play))
                                returns = invalid;
                            else
                                returns = "Piece moved.";
                        }  
                        break;
                    }
            }
            if (Logic.IsCheckMate(board, !whiteTurn))
            {
                playing = false;
                if (whiteTurn)
                    return "Black player wins!";
                else return "White player wins!";
            }
            return returns + "\n";
        }
    }
}
