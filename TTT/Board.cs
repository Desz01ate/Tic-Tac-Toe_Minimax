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
        public List<Space> GetEmptyIndex
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
        public Board Clone()
        {
            var b = new Board();
            b.squares = (Piece[,])this.squares.Clone();
            return b;
        }
        public override string ToString()
        {
            Console.Clear();
            Console.WriteLine("Tic Tac Toe using Minimax Algorithm {PLAYER = O,AI = X. PLACE BY USING COORDINATE COMBINATION OF XY (ie. 00,10,21)}\n\n");
            for (var y = 0; y < squares.GetLength(0); y++)
            {
                for (var x = 0; x < squares.GetLength(1); x++)
                {
                    Console.Write($"\t\t{squares[y, x]}");
                }
                Console.WriteLine();
            }
            return null;
        }
    }
}
