using Microsoft.AspNetCore.Mvc;
using Milestone.Models;
using System;
using System.Diagnostics;
using System.Linq;
using Milestone.Services;

namespace Milestone.Controllers
{
    public class MinesweeperController : Controller
    {
        public static BoardModel board { get; set; }
        static List<ButtonModel> buttons = new List<ButtonModel>();

        public IActionResult Index()
        {
            board = newGame();

            return View(board);
        }

        public IActionResult ButtonClick(string rowcol)
        {
            string[] separate = rowcol.Split('+');
            int row = Convert.ToInt32(separate[0]);
            int col = Convert.ToInt32(separate[1]);

            if (board.Grid[row,col].ButtonState != 10)
            {
                board.FloodFill(row, col);

                //if(service.lostGame(board, row, col))
                //{
                //    return View("GameOver");
                //}
                //if(Service.gameWon(board, row, col))
                //{
                //    return View("GameWon");
                //}
            }
            return View("Index", board);
        }

        public IActionResult ShowOneButton(string rowcol)
        {
            string[] separate = rowcol.Split('+');
            int row = Convert.ToInt32(separate[0]);
            int col = Convert.ToInt32(separate[1]);

            if (board.Grid[row, col].ButtonState != 10)
            {
                board.FloodFill(row, col);
            }


            return PartialView(board.Grid[row, col]);
        }


        public BoardModel newGame()
        {
            BoardModel newBoard = new BoardModel(10);
            newBoard.setupLiveNeighbors(0.4);
            newBoard.calculateLiveNeighbors();
            GameService.printBoards(newBoard);
            return newBoard;
        }

        public IActionResult RightClick(string rowcol)
        {
            return View("Index");
        }





        //private List<ButtonModel> resetGame()
        //{
        //    buttons = new List<ButtonModel>();

        //    board.resetBoard();
        //    board.setupLiveNeighbors(0.4);
        //    board.calculateLiveNeighbors();
        //    printBoards(board);

        //    for (int row = 0; row < board.getSize(); row++)
        //    {
        //        for (int col = 0; col < board.getSize(); col++)
        //        {
        //            buttons.Add(new ButtonModel(row, col, 0));
        //        }
        //    }

        //    return buttons;
        //}

        //private void setButtonState()
        //{
        //    for (int row = 0; row < board.getSize(); row++)
        //    {
        //        for (int col = 0; col < board.getSize(); col++)
        //        {
        //            if (board.Grid[row,col].Visited == true )
        //            {
        //                int rowcol = Convert.ToInt32(row.ToString() + col.ToString());
        //                buttons.ElementAt(rowcol).numOfNeighbors = board.Grid[row, col].Neighbors;
        //                buttons.ElementAt(rowcol).ButtonState = 2;
        //            }
        //        }
        //    }
        //}

        //private void revealButtons()
        //{
        //    for (int row = 0; row < board.getSize(); row++)
        //    {
        //        for (int col = 0; col < board.getSize(); col++)
        //        {
        //            int rowcol = Convert.ToInt32(row.ToString() + col.ToString());
        //            if (board.Grid[row,col].Live == true)
        //            {
        //                buttons.ElementAt(rowcol).ButtonState = 1;
        //            }
        //            else
        //            {
        //                buttons.ElementAt(rowcol).numOfNeighbors = board.Grid[row, col].Neighbors;
        //                buttons.ElementAt(rowcol).ButtonState = 2;
        //            }
        //        }
        //    }
        //}
    }
}
