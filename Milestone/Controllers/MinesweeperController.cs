using Microsoft.AspNetCore.Mvc;
using Milestone.Models;
using System;
using System.Diagnostics;
using System.Linq;
using Milestone.Services;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Milestone.Controllers
{
    public class MinesweeperController : Controller
    {
        MinesweeperService service = new MinesweeperService();
        public static BoardModel board { get; set; }

        public IActionResult Index()
        {
            board = newGame();

            return View(board);
        }

        /// <summary>
        /// The ButtonClick function takes a string parameter representing a row and column, splits it into separate
        /// integers, and performs a flood fill operation on a board object if the cell is not flagged.
        /// </summary>
        /// <param name="rowcol">The parameter "rowcol" is a string that represents the row and column of a button click. It
        /// is in the format "row+col", where "row" and "col" are integers representing the row and column numbers
        /// respectively.</param>
        /// <returns>
        /// The method is returning a View with the name "Index" and passing the board object as the model.
        /// </returns>
        public IActionResult ButtonClick(string rowcol)
        {
            if (rowcol != null)
            {

                string[] separate = rowcol.Split('+');
                int row = Convert.ToInt32(separate[0]);
                int col = Convert.ToInt32(separate[1]);

                if (!service.isFlagged(board, row, col))
                {
                    board.FloodFill(row, col);
                }
            }
            else
            {
                Index();
            }
            return View("Index", board);
        }


        public IActionResult ShowOneButton(string rowcol)
        {
            if (rowcol != null)
            {
                string[] separate = rowcol.Split('+');
                int row = Convert.ToInt32(separate[0]);
                int col = Convert.ToInt32(separate[1]);

                if (!service.isFlagged(board, row, col) || !service.isLive(board, row, col))
                {
                    board.FloodFill(row, col);
                }
                if (service.gameLost(board, row, col))
                {
                    return PartialView("GameLost");
                }
                if (service.gameWon(board))
                {
                    return PartialView("GameWon");
                }
            }
            else
            {
                Index();
            }
            return PartialView(board);
        }

        //Called from JS, sets ButtonState to 10/flag, if 10 already then set to 11
        public IActionResult RightClickShowOneButton(string rowcol)
        {
            //get grid row and col
            string[] separate = rowcol.Split('+');
            int row = Convert.ToInt32(separate[0]);
            int col = Convert.ToInt32(separate[1]);

            //if flag already, remove flag
            if (service.isFlagged(board,row,col))
            {
                //11 is blank cell img
                board.Grid[row, col].ButtonState = 11;
                return PartialView(board);
            }
            //if visited already, dont flag
            if(service.isVisited(board,row,col))
            {
                return PartialView(board);
            }
            else
            {   //10 = flag img
                board.Grid[row, col].ButtonState = 10;
            }
                        
            return PartialView(board);
        }

        public BoardModel newGame()
        {
            BoardModel newBoard = new BoardModel(10);
            newBoard.setupLiveNeighbors(0.4);
            newBoard.calculateLiveNeighbors();
            MinesweeperService.printBoards(newBoard);
            return newBoard;
        }
    }
}
