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

        public SavedGameAPIController()
        {
            repository = new SavedGameDAO();
        }

        [HttpGet("ShowSavedGames")]
        [ResponseType(typeof(List<SavedGameModel>))]
        public IEnumerable<SavedGameModel> Index()
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
