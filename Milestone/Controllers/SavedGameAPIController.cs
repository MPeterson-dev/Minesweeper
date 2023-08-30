using Microsoft.AspNetCore.Mvc;
using Milestone.Models;
using Milestone.Services;

namespace Milestone.Controllers
{
    [ApiController]
    [Route("api/")]

    public class SavedGameAPIController : Controller
    {
        SavedGameDAO repository = new SavedGameDAO();

        [HttpGet("showSavedGames")]
        public ActionResult<IEnumerable<SavedGameModel>> Index()
        {
            return repository.AllGames();
        }

        [HttpGet("showSavedGames/{id}")]
        public ActionResult <SavedGameModel> ShowOneGame(int id)
        {
            return repository.GetSavedGameByGameId(id);
        }

        [HttpGet("deleteOneGame/{id}")]
        public bool DeleteOneGame(int id)
        {
            return repository.DeleteOneGame(id);
        }
    }
}
