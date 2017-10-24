using System.Collections.Generic;

namespace TTT
{
    class AI
    {
        public static Space MiniMax(Board state, Piece p)
        {
            return Max(state, p);
        }
        private static Space Max(Board state,Piece p)
        {
            Space? bestSpace = null;
            List<Space> openSpaces = state.GetEmptySpaces; //determine the free spaces first
            Board newBoard;
            for (int i = 0; i < openSpaces.Count; i++) //looping over the free spaces 
            {
                newBoard = state.Clone(); //clone the current board
                Space newSpace = openSpaces[i];
                newBoard[newSpace.X, newSpace.Y] = p; //try moving on the current board using the new free space

                if (HeuristicFunction(newBoard) == 0 && newBoard.GetEmptySpaces.Count > 0) 
                //if the heuristic is calculated and still not able to win also there are more spaces to play then we try to push-down the opponent win rate
                {
                    var piece = p == Piece.O ? Piece.X : Piece.O; //let's switch the player piece
                    Space tempMove = Min(newBoard, piece);  //we need to go deeper :p
                    newSpace.Utility = tempMove.Utility; //memorize the utility of the new space
                }
                else //in case there is a NOT ZERO from heuristic, it's mean that maybe X or O is catching there so let's memorize the utility too
                {
                    #region Logic Description
                    /*
                    if (HeuristicFunction(newBoard) == Piece._)
                        newSpace.Utility = 0;
                    else if (HeuristicFunction(newBoard) == Piece.X)
                        newSpace.Utility = -1;
                    else if (HeuristicFunction(newBoard) == Piece.O)
                        newSpace.Utility = 1;
                    */
                    #endregion
                    newSpace.Utility = (int)HeuristicFunction(newBoard) * -1; 
                }
                if (bestSpace == null || (p == Piece.X && newSpace.Utility < ((Space)bestSpace).Utility)) //if new space utility is higher for AI than the previous one, just grab it :p
                {
                    bestSpace = newSpace;
                }
            }
            return (Space)bestSpace;
        }
        private static Space Min(Board state,Piece p)
        {
            Space? bestSpace = null;
            List<Space> openSpaces = state.GetEmptySpaces;
            Board newBoard;
            for (int i = 0; i < openSpaces.Count; i++)
            {
                newBoard = state.Clone();
                Space newSpace = openSpaces[i];
                newBoard[newSpace.X, newSpace.Y] = p;

                if (HeuristicFunction(newBoard) == 0 && newBoard.GetEmptySpaces.Count > 0)
                {
                    var piece = p == Piece.O ? Piece.X : Piece.O;
                    Space tempMove = Max(newBoard, piece);
                    newSpace.Utility = tempMove.Utility;
                }
                else
                {
                    #region Logic Description
                    /*
                    if (HeuristicFunction(newBoard) == Piece._)
                        newSpace.Utility = 0;
                    else if (HeuristicFunction(newBoard) == Piece.X)
                        newSpace.Utility = -1;
                    else if (HeuristicFunction(newBoard) == Piece.O)
                        newSpace.Utility = 1;
                        */
                    #endregion
                    newSpace.Utility = (int)HeuristicFunction(newBoard) * -1;
                }
                if (bestSpace == null || (p == Piece.O && newSpace.Utility > ((Space)bestSpace).Utility)) //if new space utility is lower for opponent than the previous one, just grab it :p
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
            for (int x = 0; x < board.squares.GetLength(0); x++)
            {
                count_x = 0;
                count_y = 0;
                for (int y = 0; y < board.squares.GetLength(1); y++)
                {
                    count_x += (int)board.squares[x, y]; //row-scan
                    count_y += (int)board.squares[y, x]; //column-scan
                }
                if (count_x == 3 || count_y == 3) //X is on vertical/horizontal THREE-IN-A-ROW
                    return Piece.X;
                if (count_x == -3 || count_y == -3) //O is on vertical/horizontal THREE-IN-A-ROW
                    return Piece.O;
            }

            var count = 0; //diagonal 1 
            count += (int)board.squares[0, 0];
            count += (int)board.squares[1, 1];
            count += (int)board.squares[2, 2];
            if (count == 3) //X is on diagonal THREE-IN-A-ROW
                return Piece.X;
            if (count == -3) //O is on diagonal THREE-IN-A-ROW
                return Piece.O;

            count = 0; //diagonal 2
            count += (int)board.squares[0, 2];
            count += (int)board.squares[1, 1];
            count += (int)board.squares[2, 0];
            if (count == 3) //X is on diagonal THREE-IN-A-ROW
                return Piece.X;
            if (count == -3) //O is on diagonal THREE-IN-A-ROW
                return Piece.O;
            return 0; //NOTHING WAS MATCHED SO RETURN 0
        }
    }
}