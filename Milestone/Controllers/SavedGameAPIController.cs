using Microsoft.AspNetCore.Mvc;
using Milestone.Models;
using Milestone.Services;
using System.Web.Http.Description;

namespace Milestone.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class SavedGameAPIController : ControllerBase
    {
        SavedGameDAO repository = new SavedGameDAO();

        /* The `public SavedGameAPIController()` is a constructor method for the `SavedGameAPIController` class. It is
        responsible for initializing the `repository` variable with a new instance of the `SavedGameDAO` class. This
        allows the controller to have access to the methods and properties of the `SavedGameDAO` class, which is used
        for interacting with the data storage (e.g., a database) to perform CRUD operations related to saved games. */
        public SavedGameAPIController()
        {
            repository = new SavedGameDAO();
        }

        /// <summary>
        /// The above function is an HTTP GET endpoint that returns a list of all saved games.
        /// </summary>
        /// <returns>
        /// The method is returning an IEnumerable of SavedGameModel objects.
        /// </returns>
        [HttpGet("ShowSavedGames")]
        [ResponseType(typeof(List<SavedGameModel>))]
        public IEnumerable<SavedGameModel> Index()
        {
            return repository.AllGames();
        }

        /// <summary>
        /// The function "ShowOneGame" returns a saved game model based on the provided game ID.
        /// </summary>
        /// <param name="id">The "id" parameter is an integer that represents the unique identifier of a saved game.</param>
        /// <returns>
        /// The method is returning an ActionResult of type SavedGameModel.
        /// </returns>
        [HttpGet("showSavedGames/{id}")]
        public ActionResult <SavedGameModel> ShowOneGame(int id)
        {
            return repository.GetSavedGameByGameId(id);
        }

        /// <summary>
        /// The above function is an HTTP GET endpoint that deletes a game with the specified ID from the repository and
        /// returns a boolean indicating whether the deletion was successful.
        /// </summary>
        /// <param name="id">The id parameter is an integer that represents the unique identifier of a game that needs to be
        /// deleted.</param>
        /// <returns>
        /// A boolean value is being returned.
        /// </returns>
        [HttpGet("deleteOneGame/{id}")]
        public bool DeleteOneGame(int id)
        {
            return repository.DeleteOneGame(id);
        }
    }
}
