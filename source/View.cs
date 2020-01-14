namespace source
{
    class View
    {
        Game game = new Game(new Player(), new Player(), new Board());
        Board board = new Board();

        public string Show()
        {
            return board.CanMoveTo(0, 1, 1, 1).ToString();
        }
    }
}
