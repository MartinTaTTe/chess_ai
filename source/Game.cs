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
    }
}
