using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TTT
{
    class Program
    {
        public static int level = 0;
        public static bool AIGoFirst;
        static void Main(string[] args)
        {
            Console.WindowHeight = Console.LargestWindowHeight/2;
            Console.WindowWidth = Console.LargestWindowWidth/2;
            
            do
            {
                Console.Clear();
                Console.Write("What difficult should the AI be ([E]asy,[M]edium,[H]ard) : ");
                var chooseLevel = Console.ReadLine();
                if (chooseLevel == "E")
                    level = 2;
                else if(chooseLevel == "M")
                    level = 5;
                else if (chooseLevel == "H")
                    level = 10;
            } while (level == 0);
            for (var i = 0; i < 20; i++)
            {
                Console.Write("\rDetermining who go first [" + string.Concat(Enumerable.Repeat("#",i)).PadRight(19) + "]");
                Thread.Sleep(30);
            }
            var r = new Random();
            AIGoFirst = r.Next(0, 2) > 0.5;
            var goFirstResult = AIGoFirst?"AI":"PLAYER";
            Console.WriteLine($"\n{goFirstResult} GO FIRST!");
            Console.ReadKey();
            GameStart(level);
            //AIvsAV();
        }

        private static void AIvsAV()
        {
            Space AIState, AIState2;
            Board GameBoard = new Board();
            while (!GameBoard.IsFull)
            {
                
                AIState2 = new AI().MiniMax(GameBoard, Piece.O,level);
                GameBoard[AIState2.X, AIState2.Y] = Piece.O;
                GameBoard.ToString();
                if (CheckForWinners(GameBoard))
                {
                    Main(null);
                }
                Thread.Sleep(1000);
                AIState = new AI().MiniMax(GameBoard, Piece.X,level);
                GameBoard[AIState.X, AIState.Y] = Piece.X;
                GameBoard.ToString();
                if (CheckForWinners(GameBoard))
                {
                    Main(null);
                }
                Thread.Sleep(1000);
            }
        }

        private static void GameStart(int level)
        {
            Space AIState, AIState2;
            Board GameBoard = new Board();
            if (AIGoFirst)
            {
                AIState = new AI().MiniMax(GameBoard, Piece.X, level);
                GameBoard[AIState.X, AIState.Y] = Piece.X;
                GameBoard.ToString();
                if (CheckForWinners(GameBoard))
                {
                    Main(null);
                }
            }
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
                AIState = new AI().MiniMax(GameBoard, Piece.X, level);
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
