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

        SavedGameDAO savedGameRepository = new SavedGameDAO();

        public IActionResult Index()
        {
            board = newGame();

            return View(board);
        }
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

        //Called from JS, inserts saved game into repository
        public IActionResult SaveGame(SavedGameModel aSavedGame)
        {
            savedGameRepository.Insert(aSavedGame);
            return View("Index");
        }

        public IActionResult RetrieveSavedGameModelProperties()
        {
            //TODO: Retrieve properties from gameboard to pass into partial view using JS/Ajax. Partial view then passes to SaveGame using JS/Ajax.
            return View("Index");
        }
        public BoardModel newGame()
        {
            BoardModel newBoard = new BoardModel(10);
            newBoard.setupLiveNeighbors(0.4);
            newBoard.calculateLiveNeighbors();
            MinesweeperService.printBoards(newBoard);
            return newBoard;
        }

        public IActionResult RetrieveGameJSON(int id)
        {
            return Json(savedGameRepository.GetSavedGameByGameId(id));
        }
    }
}
