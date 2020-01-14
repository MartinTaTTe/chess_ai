namespace source
{
    abstract class Piece
    {
        protected bool alive = true;
        protected int x, y;
        protected bool isWhite;

        public Piece(bool white, int x_in, int y_in)
        {
            isWhite = white;
            x = x_in;
            y = y_in;

        }

        public abstract int[,] CanMoveTo();

    }

    class Pawn : Piece
    {
        public override int[,] CanMoveTo()
        {
            if(this.isWhite)
            {
                return new int[3, 2] { { x - 1, y + 1 }, { x, y + 1 }, { x + 1, y + 1 } };
            } else
            {
                return new int[3, 2] { { x - 1, y - 1 }, { x, y - 1 }, { x + 1, y - 1 } };
            }

        }
    }

    class Rook : Piece
    {
        public override int[,] CanMoveTo()
        {
            int[,] returns = new int[14, 2];
            int j = 0;
            for (int i = 0; i < 8; i++)
            {
                if (i != x)
                {
                    returns[j, 0] = i;
                    returns[j, 1] = y;
                }
                j++;
            }
            for (int i = 0; i < 8; i++)
            {
                if (i != y)
                {
                    returns[j, 0] = x;
                    returns[j, 1] = i;
                }
                j++;
            }
            return returns;
        }
    }

    class Knight : Piece
    {
        public override int[,] CanMoveTo()
        {
           return new int[8, 2] { { x - 1, y + 2 }, { x + 1, y + 2 }, { x - 1, y - 2 }, { x + 1, y - 2 }, { x - 2, y + 1 }, { x - 2, y - 1 }, { x + 2, y + 1 }, { x + 2, y - 1 } };
        }
    }

    class Bishop : Piece
    {
        public override int[,] CanMoveTo()
        {
            Logic logic = new Logic();
            bool IsTile(int x, int y)
            {
                return !(x < 0 || x > 7 || y < 0 || y > 7);
            }

            List<int[]> returns = new List<int[2]>();
            
            int i = 1, j = 1;
            
            while (logic.IsTile(x + i, y + j))
            {
                returns.Add(x + i, y + j);
                i++; j++;
            }
           
            int i = -1, j = 1;

            while (logic.IsTile(x + i, y + j))
            {
                returns.Add(x + i, y + j);
                i--; j++;
            }

            int i = -1, j = -1;

            while (logic.IsTile(x + i, y + j))
            {
                returns.Add(x + i, y + j);
                i--; j--;
            }

            int i = 1, j = -1;

            while (logic.IsTile(x + i, y + j))
            {
                returns.Add(x + i, y + j);
                i++; j--;
            }

            return returns.toArray;
        }

    }

    class Queen : Piece
    {
        private Rook rook = new Rook();
        private Bishop bishop = new Bishop();
        public override int[,] CanMoveTo()
        {
            int[,] arr1 = rook.CanMoveTo();
            int[,] arr2 = bishop.CanMoveTo();
            int l = arr1.Length;
            Array.Resize<int>(ref arr1, l + arr2.Length);
            Array.Copy(arr2, 0, arr1, l, arr2.Length);
            return arr1;
        }
    }

    class King : Piece
    {
        public override int[,] CanMoveTo()
        {
            return new int[8, 2] { { x - 1, y }, { x + 1, y }, { x, y + 1 }, { x, y - 1 }, { x - 1, y + 1 }, { x + 1, y + 1 }, { x + 1, y - 1 }, { x - 1, y - 1 } };
        }
    }
}