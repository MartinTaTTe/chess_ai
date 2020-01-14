namespace source
{
    class View
    {
        Game game = new Game(new Player(), new Player(), new Board());

        public string Show()
        {
            return "Game of Chess";
        }
    }
}
