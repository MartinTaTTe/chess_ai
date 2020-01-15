using System.Collections.Generic;

namespace source
{
    class Logic
    {
        public Logic()
        {
            
        }
        public bool IsTile(int x, int y)
        {
            return !(x < 0 || x > 7 || y < 0 || y > 7);
        }

        public bool CanMoveTo(Board board, int x_org, int y_org, int x_des, int y_des) //TODO: implement this function properly
        {
            bool returns = false;
            if (board.IsOccupiedAt(x_org, y_org))
            {
                switch (board.GetPieceAt(x_org, y_org).GetType().ToString())
                {
                    case "source.Pawn":
                        {
                            returns = true;
                            break;
                        }
                    case "source.Rook":
                        {
                            returns = true;
                            break;
                        }
                    case "source.Knight":
                        {
                            returns = true;
                            break;
                        }
                    case "source.Bishop":
                        {
                            returns = true;
                            break;
                        }
                    case "source.Queen":
                        {
                            returns = true;
                            break;
                        }
                    case "source.King":
                        {
                            returns = true;
                            break;
                        }
                    default: break;
                }
            }
            return returns;
        }
    }
}