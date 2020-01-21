using System;
using System.Collections.Generic;

namespace source
{
    abstract class Piece // Every game piece is a Piece, each child knows it's own possible movement considering an empty board
    {
        protected int x, y;
        protected bool isWhite;
        protected bool hasMoved;
        protected string type;

        public Piece(bool isWhite, int x, int y)
        {
            this.isWhite = isWhite;
            this.x = x;
            this.y = y;
            hasMoved = false;

        }
        public Piece(Piece piece)
        {
            x = piece.x;
            y = piece.y;
            isWhite = piece.isWhite;
            hasMoved = piece.hasMoved;
        }
        public int GetX()
        {
            return x;
        }
        public int GetY()
        {
            return y;
        }
        public void SetC(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public int[] GetC()
        {
            return new int[] { x, y };
        }
        public void Moved()
        {
            hasMoved = true;
        }
        public bool HasMoved()
        {
            return hasMoved;
        }
        protected int[] NextTileInDirection(int x_pre, int y_pre, int x_dif, int y_dif)
        {
            int x_nex = x_pre + x_dif;
            int y_nex = y_pre + y_dif;

            if (!Logic.IsTile(x_nex, y_nex))
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
        public bool Type(string type)
        {
            return type == this.type;
        }
        public string TypeStr()
        {
            return type;
        }
    }

    class Pawn : Piece
    {
        public Pawn(bool white, int x_in, int y_in) : base(white, x_in, y_in)
        {
            type = "pawn";
        }
        public Pawn(Piece piece) : base(piece)
        {
            type = "pawn";
        }
        public override int[][][] MovementPattern()
        {
            List<int[][]> list = new List<int[][]>();
            if(isWhite)
            {
                if (!hasMoved)
                    list.Add(new int[][] { new int[] { x, y - 1 }, new int[] { x, y - 2 } });
                else
                    list.Add(new int[][] { new int[] { x, y - 1 } });
                if (Logic.IsTile(x - 1, y - 1))
                    list.Add(new int[][] { new int[] { x - 1, y - 1 } });
                if (Logic.IsTile(x + 1, y - 1))
                    list.Add(new int[][] { new int[] { x + 1, y - 1 } });
            } else
            {
                if (!hasMoved)
                    list.Add(new int[][] { new int[] { x, y + 1 }, new int[] { x, y + 2 } });
                else
                    list.Add(new int[][] { new int[] { x, y + 1 } });
                if (Logic.IsTile(x - 1, y - 1))
                    list.Add(new int[][] { new int[] { x - 1, y + 1 } });
                if (Logic.IsTile(x + 1, y - 1))
                    list.Add(new int[][] { new int[] { x + 1, y + 1 } });
            }
            return list.ToArray();
        }
    }

    class Rook : Piece
    {
        public Rook(bool white, int x_in, int y_in) : base(white, x_in, y_in)
        {
            type = "rook";
        }
        public Rook(Piece piece) : base(piece)
        {
            type = "rook";
        }

        public override int[][][] MovementPattern()
        {
            List<int[][]> list = new List<int[][]>();
            if (Logic.IsTile(x - 1, y))
                list.Add(CreatePath(x, y, -1, 0));
            if (Logic.IsTile(x + 1, y))
                list.Add(CreatePath(x, y, +1, 0));
            if (Logic.IsTile(x, y - 1))
                list.Add(CreatePath(x, y, 0, -1));
            if (Logic.IsTile(x, y + 1))
                list.Add(CreatePath(x, y, 0, +1));
            return list.ToArray();
        }
    }

    class Knight : Piece
    {
        public Knight(bool white, int x_in, int y_in) : base(white, x_in, y_in)
        {
            type = "knight";
        }
        public Knight(Piece piece) : base(piece)
        {
            type = "knight";
        }
        public override int[][][] MovementPattern()
        {
            List<int[][]> list = new List<int[][]>();
            if (Logic.IsTile(x + 2, y - 1))
                list.Add(new int[][] { new int[] { x + 2, y - 1 } });
            if (Logic.IsTile(x + 2, y + 1))
                list.Add(new int[][] { new int[] { x + 2, y + 1 } });
            if (Logic.IsTile(x - 2, y - 1))
                list.Add(new int[][] { new int[] { x - 2, y - 1 } });
            if (Logic.IsTile(x - 2, y + 1))
                list.Add(new int[][] { new int[] { x - 2, y + 1 } });
            if (Logic.IsTile(x + 1, y - 2))
                list.Add(new int[][] { new int[] { x + 1, y - 2 } });
            if (Logic.IsTile(x - 1, y - 2))
                list.Add(new int[][] { new int[] { x - 1, y - 2 } });
            if (Logic.IsTile(x + 1, y + 2))
                list.Add(new int[][] { new int[] { x + 1, y + 2 } });
            if (Logic.IsTile(x - 1, y + 2))
                list.Add(new int[][] { new int[] { x - 1, y + 2 } });
            return list.ToArray();
        }
    }

    class Bishop : Piece
    {
        public Bishop(bool white, int x_in, int y_in) : base(white, x_in, y_in)
        {
            type = "bishop";
        }
        public Bishop(Piece piece) : base(piece)
        {
            type = "bishop";
        }
        public override int[][][] MovementPattern()
        {
            List<int[][]> list = new List<int[][]>();
            if (Logic.IsTile(x - 1, y - 1))
                list.Add(CreatePath(x, y, -1, -1));
            if (Logic.IsTile(x - 1, y + 1))
                list.Add(CreatePath(x, y, -1, +1));
            if (Logic.IsTile(x + 1, y - 1))
                list.Add(CreatePath(x, y, +1, -1));
            if (Logic.IsTile(x + 1, y + 1))
                list.Add(CreatePath(x, y, +1, +1));
            return list.ToArray();
        }
    }

    class Queen : Piece
    {
        public Queen(bool white, int x_in, int y_in) : base(white, x_in, y_in)
        {
            type = "queen";
        }
        public Queen(Piece piece) : base(piece)
        {
            type = "queen";
        }
        public override int[][][] MovementPattern()
        {
            List<int[][]> list = new List<int[][]>();
            if (Logic.IsTile(x - 1, y))
                list.Add(CreatePath(x, y, -1, 0));
            if (Logic.IsTile(x + 1, y))
                list.Add(CreatePath(x, y, +1, 0));
            if (Logic.IsTile(x, y - 1))
                list.Add(CreatePath(x, y, 0, -1));
            if (Logic.IsTile(x, y + 1))
                list.Add(CreatePath(x, y, 0, +1));
            if (Logic.IsTile(x - 1, y - 1))
                list.Add(CreatePath(x, y, -1, -1));
            if (Logic.IsTile(x - 1, y + 1))
                list.Add(CreatePath(x, y, -1, +1));
            if (Logic.IsTile(x + 1, y - 1))
                list.Add(CreatePath(x, y, +1, -1));
            if (Logic.IsTile(x + 1, y + 1))
                list.Add(CreatePath(x, y, +1, +1));
            return list.ToArray();
        }
    }

    class King : Piece
    {
        public King(bool white, int x_in, int y_in) : base(white, x_in, y_in)
        {
            type = "king";
        }
        public King(Piece piece) : base(piece)
        {
            type = "king";
        }
        public override int[][][] MovementPattern()
        {
            List<int[][]> list = new List<int[][]>();
            if (Logic.IsTile(x, y - 1))
                list.Add(new int[][] { new int[] { x, y - 1 } });
            if (Logic.IsTile(x, y + 1))
                list.Add(new int[][] { new int[] { x, y + 1 } });
            if (Logic.IsTile(x + 1, y))
                list.Add(new int[][] { new int[] { x + 1, y } });
            if (Logic.IsTile(x - 1, y))
                list.Add(new int[][] { new int[] { x - 1, y } });
            if (Logic.IsTile(x - 1, y + 1))
                list.Add(new int[][] { new int[] { x - 1, y + 1 } });
            if (Logic.IsTile(x + 1, y + 1))
                list.Add(new int[][] { new int[] { x + 1, y + 1 } });
            if (Logic.IsTile(x + 1, y - 1))
                list.Add(new int[][] { new int[] { x + 1, y - 1 } });
            if (Logic.IsTile(x - 1, y - 1))
                list.Add(new int[][] { new int[] { x - 1, y - 1 } });
            return list.ToArray();
        }
    }
}