using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTT
{
    class Program
    {
        static Board state;
        static void Main(string[] args)
        {
            Space s;
            state = new Board();
            while (!state.IsFull)
            {
                
                int x, y;
                do
                { 
                    var coord = "";
                    do
                    {
                       
                        state.ToString();
                        Console.Write("\n\nWhere to place? : ");
                        coord = Console.ReadLine();
                    } while (coord.Length != 2);
                    x = (int)System.Char.GetNumericValue(coord[0]);
                    y = (int)System.Char.GetNumericValue(coord[1]);
                }while(!state.EmptyIndex.Contains(new Space(x,y)));
                state[x, y] = Piece.O;
                if (CheckForWinners())
                {
                    Main(null);
                }
                if(state.EmptyIndex.Count == state.Size)
                {
                    var r = new Random();
                    s = new Space(r.Next(0, 3), r.Next(0, 3));
                }
                else
                {
                    s = AI.MiniMax(state, Piece.X);
                }
                state[s.X, s.Y] = Piece.X;
                state.ToString();
                if (CheckForWinners())
                {
                    Main(null);
                }
            }
        }
        public static bool CheckForWinners()
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
            else if (p == Piece.O) //sad that this one will never be reached.
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
