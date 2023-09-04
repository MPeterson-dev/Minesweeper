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

        /// <summary>
        /// The function initializes a new game board and returns it to the view.
        /// </summary>
        /// <returns>
        /// The method is returning a View with the board as the model.
        /// </returns>
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

        /// <summary>
        /// The function takes a string parameter representing a row and column, splits it into separate integers, performs
        /// some checks on the board, and returns a partial view.
        /// </summary>
        /// <param name="rowcol">The parameter "rowcol" is a string that represents the row and column of a button on a game
        /// board. It is in the format "row+col", where "row" and "col" are integers representing the row and column numbers
        /// respectively.</param>
        /// <returns>
        /// The method is returning a PartialView.
        /// </returns>
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


        /// <summary>
        /// The function handles the right-click event on a button in a grid, updating the button state based on whether it
        /// is flagged, visited, or neither.
        /// </summary>
        /// <param name="rowcol">The parameter "rowcol" is a string that represents the row and column of a button on a
        /// grid. It is in the format "row+col", where "row" and "col" are integers representing the row and column numbers
        /// respectively.</param>
        /// <returns>
        /// The method is returning a PartialView of the board.
        /// </returns>
        public IActionResult RightClickShowOneButton(string rowcol)
        {
            string[] separate = rowcol.Split('+');
            int row = Convert.ToInt32(separate[0]);
            int col = Convert.ToInt32(separate[1]);

            if (service.isFlagged(board,row,col))
            {
                board.Grid[row, col].ButtonState = 11;
                return PartialView(board);
            }
            if(service.isVisited(board,row,col))
            {
                return PartialView(board);
            }
            else
            {
                board.Grid[row, col].ButtonState = 10;
            }                        
            return PartialView(board);
        }

        /// <summary>
        /// The SaveGame function saves the current game state, including button states and live sites, to the database.
        /// </summary>
        /// <param name="SavedGameModel">A model class that represents a saved game. It contains properties such as userId,
        /// gameId, gameName, liveSites, time, date, and buttonStates.</param>
        public void SaveGame(SavedGameModel savedGame)
        {
            string buttonStates = service.buttonStatesWriter(board);
            string liveSites = service.liveSitesWriter(board);

            SavedGameModel savedGameModel = new SavedGameModel(LoginController.userId,1,savedGame.GameName, liveSites, board.time, board.date, buttonStates);

            savedGameRepository.Insert(savedGameModel);
        }

        /// <summary>
        /// The function "RestoreGamePage" returns a view called "ShowSavedGames" with the saved games associated with the
        /// current user.
        /// </summary>
        /// <returns>
        /// The method is returning a View called "ShowSavedGames" with the saved games retrieved from the
        /// savedGameRepository.GetSavedGameByUserId method as the model.
        /// </returns>
        public IActionResult RestoreGamePage()
        {            
            return View("ShowSavedGames", savedGameRepository.GetSavedGameByUserId(LoginController.userId));
        }

        /// <summary>
        /// The DeleteGame function deletes a game with the specified id and redirects to the RestoreGamePage in the
        /// Minesweeper controller.
        /// </summary>
        /// <param name="id">The id parameter is an integer that represents the unique identifier of the game that needs to
        /// be deleted.</param>
        /// <returns>
        /// The method is returning an ActionResult.
        /// </returns>
        public ActionResult DeleteGame(int id)
        {
            savedGameRepository.DeleteOneGame(id);

            return RedirectToAction("RestoreGamePage", "Minesweeper");
        }

        /// <summary>
        /// The function restores a saved game by retrieving the saved game data, creating a new board, updating the board
        /// with the saved game data, and then returning the updated board to the Index view.
        /// </summary>
        /// <param name="id">The `id` parameter is an integer that represents the ID of the saved game that needs to be
        /// restored.</param>
        /// <returns>
        /// The method is returning an ActionResult, which is typically used to return a view in ASP.NET MVC. In this case,
        /// it is returning a view called "Index" with the newBoard object as the model.
        /// </returns>
        public ActionResult RestoreGame(int id)
        {
            SavedGameModel savedGameModel = new SavedGameModel();
            savedGameModel = savedGameRepository.GetSavedGameByGameId(id);
            BoardModel newBoard = new BoardModel(10);
            newBoard = service.liveSitesReader(savedGameModel.LiveSites, newBoard);
            newBoard.calculateLiveNeighbors();

            newBoard = service.buttonStatesReader(savedGameModel.ButtonStates, newBoard);
            newBoard.time = savedGameModel.Time;
            newBoard.date = savedGameModel.Date;
            MinesweeperService.printBoards(newBoard);
            board = newBoard;
            return View("Index", newBoard);
        }

        /// <summary>
        /// The function "newGame" creates a new instance of the BoardModel class, sets up live neighbors on the board,
        /// calculates the number of live neighbors for each cell, prints the board, and returns the new board.
        /// </summary>
        /// <returns>
        /// The method is returning a BoardModel object.
        /// </returns>
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
