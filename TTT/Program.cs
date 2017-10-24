using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TTT
{
    class Program
    {
        public static int level = 0;
        static void Main(string[] args)
        {
            Console.WindowHeight = Console.LargestWindowHeight;
            Console.WindowWidth = Console.LargestWindowWidth;
            
            do
            {
                Console.Clear();
                Console.Write("What difficult should the AI be ([E]asy,[M]edium,[H]ard) : ");
                var chooseLevel = Console.ReadLine();
                if (chooseLevel == "E")
                    level = 1;
                else if (chooseLevel == "M")
                    level = 2;
                else if (chooseLevel == "H")
                    level = 999;
            } while (level == 0);
            GameStart(level);
        }

        private static void GameStart(int level)
        {
            Space AIState, AIState2;
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
                AIState = AI.MiniMax(GameBoard, Piece.X, level);
                GameBoard[AIState.X, AIState.Y] = Piece.X;
                GameBoard.ToString();
                if (CheckForWinners(GameBoard))
                {
                    Main(null);
                }
            }
        }

        public static bool CheckForWinners(Board state)
        {
            Piece? p = AI.HeuristicFunction(state);
            state.ToString();
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
