using Microsoft.AspNetCore.Mvc;
using Milestone.Models;
using System;
using System.Diagnostics;
using System.Linq;

namespace Milestone.Controllers
{
    public class MinesweeperController : Controller
    {
        static BoardModel board = new BoardModel(10);
        static List<ButtonModel> buttons = new List<ButtonModel>();

        public IActionResult Index()
        {
            buttons = resetGame();

            return View("Index", buttons);
        }

        public IActionResult handleButtonClick(string buttonNumber)
        {
            int bN = int.Parse(buttonNumber);
            int row = buttons.ElementAt(bN).Row;
            int col = buttons.ElementAt(bN).Col;
            if (board.Grid[row, col].Live == false)
            {
                buttons.ElementAt(bN).ButtonState = 2;
                board.Grid[row, col].Visited = true;
                board.FloodFill(row, col);
                setButtonState();
            }
            else
            {
                revealButtons();
            }
            if (board.winner() == true)
            {
                return View("GameWin");
            }
            return View("Index", buttons);
        }

        private static void printBoards(BoardModel obj)
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

        private List<ButtonModel> resetGame()
        {
            buttons = new List<ButtonModel>();

            board.resetBoard();
            board.setupLiveNeighbors(0.4);
            board.calculateLiveNeighbors();
            printBoards(board);

            for (int row = 0; row < board.getSize(); row++)
            {
                for (int col = 0; col < board.getSize(); col++)
                {
                    buttons.Add(new ButtonModel(row, col, 0));
                }
            }

            return buttons;
        }

        private void setButtonState()
        {
            for (int row = 0; row < board.getSize(); row++)
            {
                for (int col = 0; col < board.getSize(); col++)
                {
                    if (board.Grid[row,col].Visited == true )
                    {
                        int rowcol = Convert.ToInt32(row.ToString() + col.ToString());
                        buttons.ElementAt(rowcol).numOfNeighbors = board.Grid[row, col].Neighbors;
                        buttons.ElementAt(rowcol).ButtonState = 2;
                    }
                }
            }
        }

        private void revealButtons()
        {
            for (int row = 0; row < board.getSize(); row++)
            {
                for (int col = 0; col < board.getSize(); col++)
                {
                    int rowcol = Convert.ToInt32(row.ToString() + col.ToString());
                    if (board.Grid[row,col].Live == true)
                    {
                        buttons.ElementAt(rowcol).ButtonState = 1;
                    }
                    else
                    {
                        buttons.ElementAt(rowcol).numOfNeighbors = board.Grid[row, col].Neighbors;
                        buttons.ElementAt(rowcol).ButtonState = 2;
                    }
                }
            }
        }
    }
}
