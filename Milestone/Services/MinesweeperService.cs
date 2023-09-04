using Milestone.Models;
using System.Diagnostics;

namespace Milestone.Services
{
    public class MinesweeperService
    {
        /// <summary>
        /// The function takes a string of live sites and updates the corresponding cells in a board model to be live.
        /// </summary>
        /// <param name="liveSitesString">A string that contains information about live sites on a board. Each live site is
        /// represented by a pair of coordinates, separated by a "+" symbol. For example, "01+23+45" represents live sites
        /// at coordinates (0,1), (2,3), and (4,5).</param>
        /// <param name="BoardModel">The BoardModel is a class that represents the game board. It contains a grid of cells,
        /// where each cell can be either live or dead. The grid is represented as a two-dimensional array.</param>
        /// <returns>
        /// The method is returning the updated BoardModel object.
        /// </returns>
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

        /// <summary>
        /// The function buttonStatesReader takes a string of button states and updates the corresponding buttons in a board
        /// model.
        /// </summary>
        /// <param name="buttonStatesString">A string containing the button states separated by '+'. Each button state
        /// represents the state of a button on the board.</param>
        /// <param name="BoardModel">The BoardModel is a class that represents the state of a game board. It contains a grid
        /// of cells, where each cell has a ButtonState property and a Visited property. The ButtonState property represents
        /// the state of a button on the board, and the Visited property indicates whether the cell has</param>
        /// <returns>
        /// The method is returning the updated BoardModel object.
        /// </returns>
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

        /// <summary>
        /// The function "liveSitesWriter" takes a board model as input and returns a string representation of the live
        /// sites on the board.
        /// </summary>
        /// <param name="BoardModel">The BoardModel is a class that represents a game board. It likely contains information
        /// about the size of the board and the state of each cell (whether it is live or not).</param>
        /// <returns>
        /// The method is returning a string that represents the live sites on the board.
        /// </returns>
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

        /// <summary>
        /// The function "buttonStatesWriter" takes a BoardModel object and returns a string representation of the button
        /// states in the board.
        /// </summary>
        /// <param name="BoardModel">The BoardModel is a class that represents a game board. It contains a grid of buttons,
        /// where each button has a ButtonState property that represents its current state (e.g. pressed, unpressed,
        /// flagged, etc.). The board has a getSize() method that returns the size of the grid.</param>
        /// <returns>
        /// The method is returning a string that represents the button states of the board.
        /// </returns>
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

        /// <summary>
        /// The function "gameWon" checks if the board has a winner and returns true if it does, otherwise it returns false.
        /// </summary>
        /// <param name="BoardModel">The BoardModel is a class that represents the game board. It likely contains
        /// information about the current state of the board, such as the positions of the players and any game
        /// pieces.</param>
        /// <returns>
        /// The method is returning a Boolean value, either true or false.
        /// </returns>
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

        /// <summary>
        /// The function checks if the game is lost by determining if the button at the specified row and column on the
        /// board is a live mine.
        /// </summary>
        /// <param name="BoardModel">The BoardModel is a class that represents the game board. It contains a grid of cells,
        /// where each cell has a ButtonState and a Live property. The ButtonState represents the state of the cell's button
        /// (e.g., flagged, revealed, hidden), and the Live property indicates whether the cell</param>
        /// <param name="row">The row index of the button on the game board.</param>
        /// <param name="col">The col parameter represents the column index of the cell in the board grid.</param>
        /// <returns>
        /// The method is returning a Boolean value.
        /// </returns>
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

        /// <summary>
        /// The function "isFlagged" checks if a specific button on a board is flagged.
        /// </summary>
        /// <param name="BoardModel">The BoardModel is a class or data structure that represents the game board. It likely
        /// contains information about the state of each cell on the board, such as whether it is flagged or not, whether it
        /// contains a mine, and so on.</param>
        /// <param name="row">The row parameter represents the row index of the cell in the board grid.</param>
        /// <param name="col">The column index of the cell in the board grid.</param>
        /// <returns>
        /// The method isFlagged returns a Boolean value. It returns true if the ButtonState of the cell at the specified
        /// row and column in the board's grid is equal to 10. Otherwise, it returns false.
        /// </returns>
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

        /// <summary>
        /// The function checks if a specific cell on a board has been visited or not.
        /// </summary>
        /// <param name="BoardModel">The BoardModel is a class that represents the game board. It likely contains
        /// information about the size of the board, the cells on the board, and any other relevant data for the
        /// game.</param>
        /// <param name="row">The row index of the cell in the board grid.</param>
        /// <param name="col">The col parameter represents the column index of the cell in the board grid that you want to
        /// check if it has been visited.</param>
        /// <returns>
        /// The method isVisited returns a Boolean value. It returns true if the cell at the specified row and column in the
        /// board has been visited, and false otherwise.
        /// </returns>
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

        /// <summary>
        /// The function checks if a cell in a board is live or not.
        /// </summary>
        /// <param name="BoardModel">The BoardModel is a class or data structure that represents the game board. It likely
        /// contains information about the size of the board and the state of each cell on the board.</param>
        /// <param name="row">The row parameter represents the row index of the cell in the board grid.</param>
        /// <param name="col">The col parameter represents the column index of the cell in the board grid.</param>
        /// <returns>
        /// The method isLive returns a Boolean value. It returns true if the cell at the specified row and column in the
        /// board is live, and false otherwise.
        /// </returns>
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

        /// <summary>
        /// The function "printBoards" prints a board with live cells represented by "*" and the number of neighbors for
        /// each cell.
        /// </summary>
        /// <param name="BoardModel">The BoardModel is a class that represents a game board. It contains a grid of cells,
        /// where each cell can either be alive or dead. The grid is represented by a two-dimensional array called "Grid".
        /// Each cell in the grid has a property called "Live" which indicates whether the cell is</param>
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

        /// <summary>
        /// The line function prints a line of plus and dash characters, with each line consisting of a specified number of
        /// segments.
        /// </summary>
        /// <param name="size">The size parameter represents the number of times the loop will iterate. In this case, it
        /// determines how many times the string "+---" will be printed.</param>
        private static void line(int size)
        {
            for (int i = 0; i < size; i++)
            {
                Debug.Write("+---");
            }
        }
    }
}
