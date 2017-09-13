using System.Collections.Generic;

namespace TTT
{
    class AI
    {
          public static Space MiniMax(Board state, Piece p)
          {
              Space? bestSpace = null;
              List<Space> openSpaces = state.EmptyIndex;
              Board newBoard;
              for (int i = 0; i < openSpaces.Count; i++)
              {
                  newBoard = state.Clone();
                  Space newSpace = openSpaces[i];
                  newBoard[newSpace.X, newSpace.Y] = p;
    
                  if (HeuristicFunction(newBoard) == 0 && newBoard.EmptyIndex.Count > 0)
                  {
                    Space tempMove = MiniMax(newBoard, ((Piece)(-(int)p))); //switch evaluate the H(state) by Max/Min of the current value
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
                  if (bestSpace == null || (p == Piece.X && newSpace.Utility < ((Space)bestSpace).Utility) || (p == Piece.O && newSpace.Utility > ((Space)bestSpace).Utility)) //new space rank is higher than the previous one, replace it
                  {
                    bestSpace = newSpace;
                  }
              }
              return (Space)bestSpace;
          }
          public static Piece HeuristicFunction(Board board)
          {
              int count = 0;
              //Columns Scan
              for (int x = 0; x < 3; x++)
              {
                  count = 0;
                  for (int y = 0; y < 3; y++)
                      count += (int)board.squares[x, y];
                  if (count == 3)
                      return Piece.X;
                  if (count == -3)
                      return Piece.O;
              }

              //Rows Scan
              for (int x = 0; x < 3; x++)
              {
                  count = 0;

                  for (int y = 0; y < 3; y++)
                      count += (int)board.squares[y, x];

                  if (count == 3)
                      return Piece.X;
                  if (count == -3)
                      return Piece.O;
              }

              //diagnols right to left
              count = 0;
              count += (int)board.squares[0, 0];
              count += (int)board.squares[1, 1];
              count += (int)board.squares[2, 2];
              if (count == 3)
                  return Piece.X;
              if (count == -3)
                  return Piece.O;

              //diagnols left to right
              count = 0;
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