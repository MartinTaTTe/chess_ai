using System;
using System.Collections.Generic;

namespace source
{
    abstract class Piece
    {
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
        public Pawn(bool white, int x_in, int y_in) : base(white, x_in, y_in)
        {

        }
        public override int[,] CanMoveTo()
        {
            if(isWhite)
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
        public Rook(bool white, int x_in, int y_in) : base(white, x_in, y_in)
        {

        }
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
        public Knight(bool white, int x_in, int y_in) : base(white, x_in, y_in)
        {

        }
        public override int[,] CanMoveTo()
        {
           return new int[8, 2] { { x - 1, y + 2 }, { x + 1, y + 2 }, { x - 1, y - 2 }, { x + 1, y - 2 }, { x - 2, y + 1 }, { x - 2, y - 1 }, { x + 2, y + 1 }, { x + 2, y - 1 } };
        }
    }

    class Bishop : Piece
    {
        public Bishop(bool white, int x_in, int y_in) : base(white, x_in, y_in)
        {

        }
        public override int[,] CanMoveTo()
        {
            Logic logic = new Logic();

            List<int[]> list = new List<int[]>();

            int i = 1, j = 1;

            while (logic.IsTile(x + i, y + j))
            {
                list.Add(new int[] { x + i, y + j });
                i++; j++;
            }

            i = -1; j = 1;

            while (logic.IsTile(x + i, y + j))
            {
                list.Add(new int[] { x + i, y + j });
                i--; j++;
            }

            i = -1; j = -1;

            while (logic.IsTile(x + i, y + j))
            {
                list.Add(new int[] { x + i, y + j });
                i--; j--;
            }

            i = 1; j = -1;

            while (logic.IsTile(x + i, y + j))
            {
                list.Add(new int[] { x + i, y + j });
                i++; j--;
            }

            int l = list.Count;
            int[,] returns = new int[l, 2];

            for (int ii = 0; ii < l; ii++)
            {
                returns[ii, 0] = list[ii][0];
                returns[ii, 1] = list[ii][1];
            }

            return returns;
        }
    }

    class Queen : Piece
    {
        public Queen(bool white, int x_in, int y_in) : base(white, x_in, y_in)
        {

        }
        int[,] RookCanMoveTo()
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
        int[,] BishopCanMoveTo()
        {
            Logic logic = new Logic();

            List<int[]> list = new List<int[]>();

            int i = 1, j = 1;

            while (logic.IsTile(x + i, y + j))
            {
                list.Add(new int[] { x + i, y + j });
                i++; j++;
            }

            i = -1; j = 1;

            while (logic.IsTile(x + i, y + j))
            {
                list.Add(new int[] { x + i, y + j });
                i--; j++;
            }

            i = -1; j = -1;

            while (logic.IsTile(x + i, y + j))
            {
                list.Add(new int[] { x + i, y + j });
                i--; j--;
            }

            i = 1; j = -1;

            while (logic.IsTile(x + i, y + j))
            {
                list.Add(new int[] { x + i, y + j });
                i++; j--;
            }

            int l = list.Count;
            int[,] returns = new int[l, 2];

            for (int ii = 0; ii < l; ii++)
            {
                returns[ii, 0] = list[ii][0];
                returns[ii, 1] = list[ii][1];
            }

            return returns;
        }

        public override int[,] CanMoveTo()
        {

            int[,] arr1 = RookCanMoveTo();
            int[,] arr2 = BishopCanMoveTo();
            int[,] returns = new int[arr1.Length + arr2.Length, 2];
            Array.Copy(arr1, returns, arr1.Length);
            Array.Copy(arr2, 0, returns, arr1.Length, arr2.Length);

            return returns;
        }
    }

    class King : Piece
    {
        public King(bool white, int x_in, int y_in) : base(white, x_in, y_in)
        {

        }
        public override int[,] CanMoveTo()
        {
            return new int[8, 2] { { x - 1, y }, { x + 1, y }, { x, y + 1 }, { x, y - 1 }, { x - 1, y + 1 }, { x + 1, y + 1 }, { x + 1, y - 1 }, { x - 1, y - 1 } };
        }
    }
}