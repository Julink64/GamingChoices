using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NewGamingChoices.Data;
using NewGamingChoices.Models;
using NewGamingChoices.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewGamingChoices.Controllers
{
   
    [ApiController]
    [Route("[controller]")]
    public class GameController : ControllerBase
    {

        private readonly ILogger<GameController> _logger;
        private ApplicationDbContext _db;

        public GameController(ILogger<GameController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        [HttpPost("[action]")]
        public IActionResult addNewGame([FromBody] Game game) // Cette méthode concerne l'ajout de jeu manuel (hors Steam)
        {
            GameService gameService = new GameService(_db);
            if(gameService.AddNewGame(game))
            {
                return Ok();
            }
            else
            {
                return Ok("Ce jeu existe déjà dans notre base de données et n'a donc pas été ajouté !");
            }

        }

        [HttpPost("[action]")]
        public IActionResult addOrUpdateGm([FromBody] GamingMood gm)
        {
            GameService gameService = new GameService(_db);
            CustomUserService userService = new CustomUserService(_db);
            ApplicationUser currentuser = userService.GetUser(this.User.Identity.Name);
            if (gameService.AddOrUpdateGamingMood(gm, currentuser))
            {
                return Ok();
            }
            else
            {
                return Ok("Une erreur est survenue lors de l'ajout ou de la mise à jour du Gaming Mood.");
            }

        }

        [Produces("application/json")]
        [HttpGet("searchgame")]
        public IActionResult SearchGame(string term)
        {
            try
            {
                var names = _db.Games.Where(p => p.Name.Contains(term)).Select(p => p.Name).ToList();
                return Ok(names);
            }
            catch
            {
                return BadRequest();
            }
        }

        [Produces("application/json")]
        [HttpGet("gamedetails")]
        public IActionResult GetGameDetails(string gameid)
        {
            try
            {
                int gameidint = int.Parse(gameid);

                var game = _db.Games.Include(g => g.PlatformPrices).FirstOrDefault(p => p.ID == gameidint);
                return Ok(game);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("[action]")]
        public List<GameConsole> getConsolesList()
        {
            GameService gameService = new GameService(_db);
            return gameService.GetConsolesList();
        }


    }
}
