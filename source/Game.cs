using System;
using System.Linq;

namespace source
{
    class Game
    {
        public readonly AI ai1;
        public readonly AI ai2;
        private readonly bool hasAi1;
        private readonly bool hasAi2;
        private readonly Board board = new Board();
        private bool playing = true;
        private bool whiteTurn = true;
        public Game(Board board_in)
        {
            board = board_in;
        }
        public Game(Board board_in, AI ai1)
        {
            board = board_in;
            this.ai1 = ai1;
            hasAi1 = true;
        }
        public Game(Board board_in, AI ai1, AI ai2)
        {
            board = board_in;
            this.ai1 = ai1;
            this.ai2 = ai2;
            hasAi1 = true;
            hasAi2 = true;
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

        public bool HasPieceAt(int x, int y)
        {
            return board.IsOccupiedAt(x, y);
        }
        public Piece PieceAt(int x, int y)
        {
            if (board.IsOccupiedAt(x, y))
                return board.GetPieceAt(x, y);
            else
            {
                Console.WriteLine("Error in Game.PieceAt, returning null.");
                Console.ReadKey();
                return null;
            }
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
            if (hasAi1 && ai1.white == whiteTurn)
            {
                board.MovePiece(ai1.MakePlay(board));
                whiteTurn = !whiteTurn;
                return "AI 1 played.";
            }
            else if (hasAi2 && ai2.white == whiteTurn)
            {
                board.MovePiece(ai2.MakePlay(board));
                whiteTurn = !whiteTurn;
                return "AI 2 played.";
            }
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
            if (Logic.IsCheckMate(board, !whiteTurn)) //TODO: FIX GAME OVER LOGIC !!!!
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
