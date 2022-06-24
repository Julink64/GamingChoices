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
        public IActionResult addgm([FromBody] int gameid)
        {
            GameService gameService = new GameService(_db);
            ApplicationUser currentuser = getCurrentUser();

            if (gameService.AddGamingMood(gameid, currentuser))
            {
                return Ok();
            }
            else
            {
                return Ok("Une erreur est survenue lors de l'ajout du Gaming Mood.");
            }

        }

        [HttpPost("[action]")]
        public IActionResult updategm([FromBody] GamingMood gm)
        {
            GameService gameService = new GameService(_db);
            ApplicationUser currentuser = getCurrentUser();

            if (gameService.UpdateGamingMood(gm, currentuser))
            {
                return Ok();
            }
            else
            {
                return Ok("Une erreur est survenue lors de la mise à jour du Gaming Mood.");
            }

        }

        [HttpPost("[action]")]
        public IActionResult deletegm([FromBody] string gmid)
        {
            GameService gameService = new GameService(_db);

            if (gameService.DeleteGamingMood(gmid))
            {
                return Ok();
            }
            else
            {
                return Ok("Une erreur est survenue lors de la suppression du Gaming Mood.");
            }

        }

        [Produces("application/json")]
        [HttpGet("getgm")]
        public IActionResult getgm()
        {
            try
            { 
                ApplicationUser currentuser = getCurrentUser();

                var usergms = currentuser.GamingMoods;
                List<GamingMood> orderedgm = new List<GamingMood>();
                orderedgm.AddRange(usergms.Where(gm => gm.IsFavAndNotBlacklisted.HasValue && gm.IsFavAndNotBlacklisted.Value).OrderBy(gm => gm.Game.Name));
                orderedgm.AddRange(currentuser.GamingMoods.Where(gm => !gm.IsFavAndNotBlacklisted.HasValue).OrderBy(gm => gm.Game.Name));
                orderedgm.AddRange(currentuser.GamingMoods.Where(gm => gm.IsFavAndNotBlacklisted.HasValue && !gm.IsFavAndNotBlacklisted.Value).OrderBy(gm => gm.Game.Name));

                return Ok(orderedgm);
            }
            catch
            {
                return BadRequest();
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
        [HttpGet("gamedetailsid")]
        public IActionResult GetGameDetailsById(string gameid)
        {
            try
            {
                int gameidint = int.Parse(gameid);

                var game = _db.Games.Include(g => g.PlatformPrices.OrderBy(pp => pp.Platform != "PC")).FirstOrDefault(p => p.ID == gameidint);
                return Ok(game);
            }
            catch
            {
                return BadRequest();
            }
        }

        [Produces("application/json")]
        [HttpGet("gamedetailsname")]
        public IActionResult GetGameDetailsByName(string gamename)
        {
            try
            {
                var game = _db.Games.Include(g => g.PlatformPrices.OrderBy(pp => pp.Platform != "PC")).FirstOrDefault(p => p.Name == gamename);
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

        private ApplicationUser getCurrentUser()
        {
            CustomUserService userService = new CustomUserService(_db);
            return userService.GetUserById(this.User.Claims.ToList()[5].Value); // Cette méthode est à confirmer
        }

    }
}
