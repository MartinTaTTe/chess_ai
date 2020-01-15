using System;
using System.Collections.Generic;

namespace source
{
    abstract class Piece // Every game piece is a Piece, each child knows it's own possible movement considering an empty board
    {
        protected int x, y;
        protected bool isWhite;
        protected Logic logic = new Logic();

        public Piece(bool white, int x_in, int y_in)
        {
            isWhite = white;
            x = x_in;
            y = y_in;

        }
        protected int[] NextTileInDirection(int x_pre, int y_pre, int x_dif, int y_dif)
        {
            int x_nex = x_pre + x_dif;
            int y_nex = y_pre + y_dif;

            if (!logic.IsTile(x_nex, y_nex))
                return new int[] { -1 };
            else
                return new int[] { x_nex, y_nex };
        }
        protected int[][] CreatePath(int x_org, int y_org, int x_dif, int y_dif)
        {
            List<int[]> list = new List<int[]>();

            int[] next = NextTileInDirection(x_org, y_org, x_dif, y_dif);
            if (next[0] == -1)
                return new int[][] { new int[] { -1 } };
            else
            {
                while (next[0] != -1)
                {
                    list.Add(next);
                    next = NextTileInDirection(next[0], next[1], x_dif, y_dif);
                }
                return list.ToArray();
            }
        }
        public abstract int[][][] MovementPattern(); // 3D jagged array where the dimensions are [directions][tiles in direction][coordinates]
        public bool IsWhite()
        {
            return isWhite;
        }

    }

    class Pawn : Piece
    {
        public Pawn(bool white, int x_in, int y_in) : base(white, x_in, y_in)
        {

        }
        public override int[][][] MovementPattern()
        {
            if(isWhite)
            {
                if (y == 6)
                    return new int[][][] 
                    { 
                        new int[][] { new int[] { x - 1, y - 1 } },
                        new int[][] { new int[] { x + 1, y - 1 } },
                        new int[][] { new int[] { x, y - 1 }, new int[] { x, y - 2 } }
                    };
                else
                    return new int[][][]
                    {
                        new int[][] { new int[] { x - 1, y - 1 } },
                        new int[][] { new int[] { x + 1, y - 1 } },
                        new int[][] { new int[] { x, y - 1 } }
                    };
            } else
            {
                if (y == 1)
                    return new int[][][]
                    {
                        new int[][] { new int[] { x - 1, y + 1 } },
                        new int[][] { new int[] { x + 1, y + 1 } },
                        new int[][] { new int[] { x, y + 1 }, new int[] { x, y + 2 } }
                    };
                else
                    return new int[][][]
                    {
                        new int[][] { new int[] { x - 1, y + 1 } },
                        new int[][] { new int[] { x + 1, y + 1 } },
                        new int[][] { new int[] { x, y + 1 } }
                    };
            }
        }
    }

    class Rook : Piece
    {
        public Rook(bool white, int x_in, int y_in) : base(white, x_in, y_in)
        {

        }
        
        public override int[][][] MovementPattern()
        {
            List<int[][]> list = new List<int[][]>();
            if (logic.IsTile(x - 1, y))
                list.Add(CreatePath(x, y, -1, 0));
            if (logic.IsTile(x + 1, y))
                list.Add(CreatePath(x, y, +1, 0));
            if (logic.IsTile(x, y - 1))
                list.Add(CreatePath(x, y, 0, -1));
            if (logic.IsTile(x, y + 1))
                list.Add(CreatePath(x, y, 0, +1));
            return list.ToArray();
        }
    }

    class Knight : Piece
    {
        public Knight(bool white, int x_in, int y_in) : base(white, x_in, y_in)
        {

        }
        public override int[][][] MovementPattern()
        {
            return new int[][][]
                     {
                        new int[][] { new int[] { x + 2, y - 1 } },
                        new int[][] { new int[] { x + 2, y + 1 } },
                        new int[][] { new int[] { x - 2, y - 1 } },
                        new int[][] { new int[] { x - 2, y + 1 } },
                        new int[][] { new int[] { x + 1, y - 2 } },
                        new int[][] { new int[] { x - 1, y - 2 } },
                        new int[][] { new int[] { x + 1, y + 2 } },
                        new int[][] { new int[] { x - 1, y + 2 } }
                     };
        }
    }

    class Bishop : Piece
    {
        public Bishop(bool white, int x_in, int y_in) : base(white, x_in, y_in)
        {

        }
        public override int[][][] MovementPattern()
        {
            List<int[][]> list = new List<int[][]>();
            if (logic.IsTile(x - 1, y - 1))
                list.Add(CreatePath(x, y, -1, -1));
            if (logic.IsTile(x - 1, y + 1))
                list.Add(CreatePath(x, y, -1, +1));
            if (logic.IsTile(x + 1, y - 1))
                list.Add(CreatePath(x, y, +1, -1));
            if (logic.IsTile(x + 1, y + 1))
                list.Add(CreatePath(x, y, +1, +1));
            return list.ToArray();
        }
    }

    class Queen : Piece
    {
        public Queen(bool white, int x_in, int y_in) : base(white, x_in, y_in)
        {

        }
        public override int[][][] MovementPattern()
        {
            List<int[][]> list = new List<int[][]>();
            if (logic.IsTile(x - 1, y))
                list.Add(CreatePath(x, y, -1, 0));
            if (logic.IsTile(x + 1, y))
                list.Add(CreatePath(x, y, +1, 0));
            if (logic.IsTile(x, y - 1))
                list.Add(CreatePath(x, y, 0, -1));
            if (logic.IsTile(x, y + 1))
                list.Add(CreatePath(x, y, 0, +1));
            if (logic.IsTile(x - 1, y - 1))
                list.Add(CreatePath(x, y, -1, -1));
            if (logic.IsTile(x - 1, y + 1))
                list.Add(CreatePath(x, y, -1, +1));
            if (logic.IsTile(x + 1, y - 1))
                list.Add(CreatePath(x, y, +1, -1));
            if (logic.IsTile(x + 1, y + 1))
                list.Add(CreatePath(x, y, +1, +1));
            return list.ToArray();
        }
    }

    class King : Piece
    {
        public King(bool white, int x_in, int y_in) : base(white, x_in, y_in)
        {

        }
        public override int[][][] MovementPattern()
        {
            return new int[][][]
                     {
                        new int[][] { new int[] { x, y - 1 } },
                        new int[][] { new int[] { x, y + 1 } },
                        new int[][] { new int[] { x + 1, y - 1 } },
                        new int[][] { new int[] { x + 1, y + 1 } },
                        new int[][] { new int[] { x + 1, y } },
                        new int[][] { new int[] { x - 1, y } },
                        new int[][] { new int[] { x - 1, y + 2 } },
                        new int[][] { new int[] { x - 1, y - 2 } }
                     };
        }
    }
}