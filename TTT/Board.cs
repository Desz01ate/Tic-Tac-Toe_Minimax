using System;
using System.Collections.Generic;

namespace TTT
{
    public enum Piece
    {
        X = 1,
        O = -1,
        _ = 0,
    }
    public struct Space
    {
        public int X;
        public int Y;
        public double Utility;

        public Space(int x, int y)
        {
            this.X = x;
            this.Y = y;
            Utility = 0;
        }
    }
    class Board
    {
        public Piece[,] squares;
        public Piece this[int x, int y]
        {
            get
            {
                return squares[x, y];
            }
            set
            {
                squares[x, y] = value;
            }
        }
        public Board()
        {
            squares = new Piece[3, 3] { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } };
        }
        public int Size { get { return 9; } }
        public bool IsFull
        {
            get
            {
                foreach (Piece i in squares)
                    if (i == Piece._) { return false; }
                return true;
            }
        }
        public List<Space> GetEmptySpaces
        {
            get
            {
                List<Space> empty = new List<Space>();

                for (int x = 0; x <= 2; x++)
                    for (int y = 0; y <= 2; y++)
                        if (squares[x, y] == Piece._)
                            empty.Add(new Space(x, y));

                return empty;
            }
        }
        public int Rank { get;  set; }

        public Board Clone()
        {
            var newCloneBoard = new Board();
            newCloneBoard.squares = (Piece[,])this.squares.Clone();
            newCloneBoard.Rank = this.Rank;
            return newCloneBoard;
        }
        public override string ToString()
        {
            Console.Clear();
            var mode = "";
            if(Program.level == 2)
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                mode = "EASY";
            }
            else if(Program.level == 4)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                mode = "MEDIUM";
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                mode = "HARD";
            }
            var arr = new[]{
                        @"            |                                                               |       ",
                        @"            |                     Tic Tac Toe                               |       ",
                        @"            |                using Minimax Algorithm                        |       ",
                        @"            |                  (PLAYER = O,AI = X)                          |       ",
                        @"            |          _______                          ________            |       ",
                       $@"            |         |ooooooo|      ____  {mode.PadLeft(6)} Mode | __  __ |           |       ",
                        @"            |         |[]+++[]|     [____]             |/  \/  \|           |       ",
                        @"            |         |+ ___ +|     ]()()[             |\__/\__/|           |       ",
                        @"            |         |:|   |:|   ___\__/___           |[][][][]|           |       ",
                        @"            |         |:|___|:|  |__|    |__|          |++++++++|           |       ",
                        @"            |         |[]===[]|   |_|_/\_|_|           | ______ |           |       ",
                        @"            |________ ||||||||| _ | | __ | | _________ ||______|| __________|       ",
                        @"           /          |_______|   |_|[::]|_|           |________|           \       ",
                        @"          /                       \_|_||_|_/                                 \      ",
                        @"         /                          |_||_|        Beat Me                     \     ",
                        @"        /                          _|_||_|_           If you can               \    ",
                        @"       /                  ____    |___||___|                                    \   ",
                        @"      /                                                                          \  "
            };
            Console.WriteLine("\n\n");
            foreach (string line in arr)
                Console.WriteLine(line);
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Gray;
            for (var y = 0; y < squares.GetLength(0); y++)
            {
                for (var x = 0; x < squares.GetLength(1); x++)
                {

                    var avMove = Convert.ToString(squares[y, x]);
                    if(squares[y,x] == Piece._)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        avMove = $"{y}{x}";

                    }
                    else
                    {
                        Console.ForegroundColor = avMove == Piece.O.ToString() ? ConsoleColor.White : ConsoleColor.Red;
                    }
                    Console.Write($"\t\t{avMove}\t\t");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    if(x != squares.GetLength(1) - 1)
                    {
                        Console.Write("|");
                    }
                    //Console.Write($"\t\t{squares[y, x]}");
                }
                Console.WriteLine("\n");
            }                          
            return null;                    
        }                                   
    }
}
