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

        public void SaveGame(SavedGameModel savedGame)
        {

            string buttonStates = service.buttonStatesWriter(board);
            string liveSites = service.liveSitesWriter(board);

            SavedGameModel savedGameModel = new SavedGameModel(LoginController.userId,1,savedGame.GameName, liveSites, board.time, board.date, buttonStates);

            savedGameRepository.Insert(savedGameModel);
        }

        public IActionResult RestoreGamePage()
        {            
            return View("ShowSavedGames", savedGameRepository.GetSavedGameByUserId(LoginController.userId));
        }

        public ActionResult DeleteGame(int id)
        {
            savedGameRepository.DeleteOneGame(id);

            return RedirectToAction("Index", "Minesweeper");
        }

        public ActionResult RestoreGame(int id)
        {
            SavedGameModel savedGameModel = new SavedGameModel();
            savedGameModel = savedGameRepository.GetSavedGameByGameId(id);
            BoardModel newBoard = new BoardModel(10);
            newBoard = service.liveSitesReader(savedGameModel.LiveSites, newBoard);
            newBoard = service.buttonStatesReader(savedGameModel.ButtonStates, newBoard);
            newBoard.time = savedGameModel.Time;
            newBoard.date = savedGameModel.Date;
            newBoard.calculateLiveNeighbors();
            MinesweeperService.printBoards(newBoard);
            return View("Index", newBoard);
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
