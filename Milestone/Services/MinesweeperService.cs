using Milestone.Models;
using System.Diagnostics;

namespace Milestone.Services
{
    public class MinesweeperService
    {
        public Boolean gameWon(BoardModel board)
        {
            if (board.winner())
            {
                return true;
            }
            else {  
                return false; 
            }
        }

        public Boolean gameLost(BoardModel board, int row, int col)
        {
            if (board.Grid[row, col].Live == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Boolean isFlagged(BoardModel board, int row, int col)
        {
            if(board.Grid[row, col].ButtonState == 10)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Boolean isVisited(BoardModel board, int row, int col)
        {
            if (board.Grid[row, col].Visited == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Boolean isLive(BoardModel board, int row, int col)
        {
            if (board.Grid[row, col].Live == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void printBoards(BoardModel obj)
        {
            int boardSize = obj.getSize();

            Debug.Write("  ");
            for (int i = 0; i < boardSize; i++)
            {
                Debug.Write((i) + "   ");
            }
            Debug.WriteLine("");
            for (int row = 0; row < boardSize; row++)
            {
                for (int col = 0; col < boardSize; col++)
                {
                    if (col == 0)
                    {
                        line(boardSize);
                        Debug.WriteLine("+");
                    }
                    if (obj.Grid[row, col].Live == true)
                    {
                        Debug.Write("| * ");
                    }
                    else
                    {
                        Debug.Write("| " + obj.Grid[row, col].Neighbors + " ");
                    }
                }
                Debug.Write("|");
                Debug.Write(" " + (row));
                Debug.WriteLine("");
            }
            line(boardSize);
            Debug.WriteLine("+");
        }

        private static void line(int size)
        {
            for (int i = 0; i < size; i++)
            {
                Debug.Write("+---");
            }
        }
    }
}
