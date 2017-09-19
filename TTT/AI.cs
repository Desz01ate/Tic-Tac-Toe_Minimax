using System.Collections.Generic;

namespace TTT
{
    class AI
    {
        public static Space MiniMax(Board state, Piece p)
        {
            return Max(state, p);
        }
        public static Space Max(Board state,Piece p)
        {
            Space? bestSpace = null;
            List<Space> openSpaces = state.GetEmptyIndex;
            Board newBoard;
            for (int i = 0; i < openSpaces.Count; i++)
            {
                newBoard = state.Clone();
                Space newSpace = openSpaces[i];
                newBoard[newSpace.X, newSpace.Y] = p;

                if (HeuristicFunction(newBoard) == 0 && newBoard.GetEmptyIndex.Count > 0)
                {
                    var piece = p == Piece.O ? Piece.X : Piece.O;
                    Space tempMove = Min(newBoard, piece); 
                    newSpace.Utility = tempMove.Utility;
                }
                else
                {
                    if (HeuristicFunction(newBoard) == Piece._)
                        newSpace.Utility = 0;
                    else if (HeuristicFunction(newBoard) == Piece.X)
                        newSpace.Utility = -1;
                    else if (HeuristicFunction(newBoard) == Piece.O)
                        newSpace.Utility = 1;
                }
                if (bestSpace == null || (p == Piece.X && newSpace.Utility < ((Space)bestSpace).Utility)) //new space rank is higher than the previous one, replace it
                {
                    bestSpace = newSpace;
                }
            }
            return (Space)bestSpace;
        }
        public static Space Min(Board state,Piece p)
        {
            Space? bestSpace = null;
            List<Space> openSpaces = state.GetEmptyIndex;
            Board newBoard;
            for (int i = 0; i < openSpaces.Count; i++)
            {
                newBoard = state.Clone();
                Space newSpace = openSpaces[i];
                newBoard[newSpace.X, newSpace.Y] = p;
                if (HeuristicFunction(newBoard) == 0 && newBoard.GetEmptyIndex.Count > 0)
                {
                    var piece = p == Piece.O ? Piece.X : Piece.O;
                    Space tempMove = Max(newBoard, piece);
                    newSpace.Utility = tempMove.Utility;
                }
                else
                {
                    if (HeuristicFunction(newBoard) == Piece._)
                        newSpace.Utility = 0;
                    else if (HeuristicFunction(newBoard) == Piece.X)
                        newSpace.Utility = -1;
                    else if (HeuristicFunction(newBoard) == Piece.O)
                        newSpace.Utility = 1;
                }
                if (bestSpace == null || (p == Piece.O && newSpace.Utility > ((Space)bestSpace).Utility)) //new space rank is higher than the previous one, replace it
                {
                    bestSpace = newSpace;
                }
            }
            return (Space)bestSpace;
        }
        public static Piece HeuristicFunction(Board board)
        {
            int count_x = 0;
            int count_y = 0;
            for (int x = 0; x < board.squares.GetLength(1); x++)
            {
                count_x = 0;
                count_y = 0;
                for (int y = 0; y < board.squares.GetLength(0); y++)
                {
                    count_x += (int)board.squares[x, y]; //row-scan
                    count_y += (int)board.squares[y, x]; //column-scan
                }
                if (count_x == 3 || count_y == 3)
                    return Piece.X;
                if (count_x == -3 || count_y == -3)
                    return Piece.O;
            }

            var count = 0; //diagonal 1 
            count += (int)board.squares[0, 0];
            count += (int)board.squares[1, 1];
            count += (int)board.squares[2, 2];
            if (count == 3)
                return Piece.X;
            if (count == -3)
                return Piece.O;

            count = 0; //diagonal 2
            count += (int)board.squares[0, 2];
            count += (int)board.squares[1, 1];
            count += (int)board.squares[2, 0];
            if (count == 3)
                return Piece.X;
            if (count == -3)
                return Piece.O;
            return Piece._;
        }
    }
}