using Microsoft.AspNetCore.Mvc;
using Milestone.Models;
using System;
using System.Diagnostics;

namespace Milestone.Controllers
{
    public class MinesweeperController : Controller
    {
        BoardModel board = new BoardModel(10);
        static List<ButtonModel> buttons = new List<ButtonModel>();

        public IActionResult Index()
        {
            buttons = new List<ButtonModel>();

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
            }
            else
            {
                buttons.ElementAt(bN).ButtonState = 1;
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
    }
}
