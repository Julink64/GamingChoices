using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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


    }
}
