using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTT
{
    class Program
    {
        static void Main(string[] args)
        {
            Space AIState;
            Board GameBoard = new Board();
            while (!GameBoard.IsFull)
            {
                int x, y;
                //Player Turn
                do
                {
                    var coord = "";
                    do
                    {
                        GameBoard.ToString();
                        Console.Write("\n\nWhere to place? : ");
                        coord = Console.ReadLine();
                    } while (coord.Length != 2);
                    x = (int)System.Char.GetNumericValue(coord[0]);
                    y = (int)System.Char.GetNumericValue(coord[1]);
                } while (!GameBoard.GetEmptySpaces.Contains(new Space(x, y)));
                GameBoard[x, y] = Piece.O;
                if (CheckForWinners(GameBoard))
                {
                    Main(null);
                }
#region .
                /*
                if (GameBoard.GetEmptyIndex.Count == GameBoard.Size)
                {
                    var r = new Random();
                    AIState = new Space(r.Next(0, 3), r.Next(0, 3));
                }
                //End Player Turn
                //AI Turn
                else
                {
                    AIState = AI.MiniMax(GameBoard, Piece.X);
                }
                */
#endregion
                //End Player Turn
                //AI Turn
                AIState = AI.MiniMax(GameBoard, Piece.X);
                GameBoard[AIState.X, AIState.Y] = Piece.X;
                GameBoard.ToString();
                if (CheckForWinners(GameBoard))
                {
                    Main(null);
                }
                //End AI Turn
            }
        }
        public static bool CheckForWinners(Board state)
        {
            Piece? p = AI.HeuristicFunction(state);
            if (p == Piece.X)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("AI WIN!");
                Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.Gray;
                return true;
            }
            else if (p == Piece.O)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("YOU WIN!");
                Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.Gray;
                return true;
            }
            else if (state.IsFull)
            {
                Console.WriteLine("DRAW!");
                Console.ReadLine();
                return true;
            }
            return false;
        }
    }
}
