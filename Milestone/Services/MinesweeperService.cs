using Milestone.Models;
using System.Diagnostics;

namespace Milestone.Services
{
    public class MinesweeperService
    {

        public BoardModel liveSitesReader(string liveSitesString, BoardModel board)
        {
            string[] separate = liveSitesString.Split('+');


            Debug.WriteLine(separate[0][0] + " "+ separate[0][1]);
            for (int i = 0; i < separate.Length; i++)
            {
                int row = Convert.ToInt32(Convert.ToString(separate[i][0]));
                int col = Convert.ToInt32(Convert.ToString(separate[i][1]));
                board.Grid[row,col].Live = true;
            }

            return board;
        }

        public BoardModel buttonStatesReader(string buttonStatesString, BoardModel board)
        {
            int counter = 0;
            string[] separate = buttonStatesString.Split('+');
            for (int row = 0; row < board.getSize(); row++)
            {
                for (int col = 0; col < board.getSize(); col++)
                {
                    int buttonStates = Convert.ToInt32(Convert.ToString(separate[counter]));
                    board.Grid[row, col].ButtonState = buttonStates;
                    if(buttonStates != 10 && buttonStates != 11)
                    {
                        board.Grid[row, col].Visited = true;
                    }
                    counter++;
                }

            }

            return board;
        }


        public string liveSitesWriter(BoardModel board)
        {
            string liveSites = "";

            for (int row = 0; row < board.getSize(); row++)
            {
                for (int col = 0; col < board.getSize(); col++)
                {
                    if (isLive(board, row, col))
                    {
                        if (liveSites.Length == 0)
                        {

                            liveSites = row.ToString() + col.ToString();
                        }
                        else
                        {
                            liveSites = liveSites + "+" + row.ToString() + col.ToString();
                        }
                    }
                }
            }
            Debug.WriteLine(liveSites);
            return liveSites;
        }

        public string buttonStatesWriter(BoardModel board)
        {
            string buttonStates = "";

            for (int row = 0; row < board.getSize(); row++)
            {
                for (int col = 0; col < board.getSize(); col++)
                {
                    if(buttonStates.Length == 0)
                    {
                        buttonStates = board.Grid[row, col].ButtonState.ToString();
                    }
                    else
                    {
                        buttonStates = buttonStates + "+" + board.Grid[row, col].ButtonState.ToString();
                    }
                }
            }
            return buttonStates;
        }
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
            if (board.Grid[row,col].ButtonState != 10)
            {
                if (board.Grid[row, col].Live == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }return false;
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
