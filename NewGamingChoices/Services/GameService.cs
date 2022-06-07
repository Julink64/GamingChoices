using NewGamingChoices.Data;
using NewGamingChoices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewGamingChoices.Services
{
    public class GameService
    {
        private ApplicationDbContext _db { get; set; }

        public GameService(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool AddNewGame(Game game)
        {
            // Le nom définit l'unicité d'un jeu : s'il est présent sur d'autres plateformes, il suffira d'éditer sa fiche existante

            var existinggame = _db.Games.FirstOrDefault(g => g.Name.Replace(" ", string.Empty).ToUpper() == game.Name.Replace(" ", string.Empty).ToUpper());
            // TODO : Trouver une meilleure méthode de comparaison qui puisse être prise en compte par SQL

            if(existinggame != null)
            { // Le jeu existe déjà, on n'insère rien
                return false;
            }

            _db.Games.Add(game);
            _db.SaveChanges();
            return true;
        }

        public bool AddOrUpdateGamingMood(GamingMood gamingmood, ApplicationUser currentuser)
        {
            var existinggm = currentuser.GamingMoods.FirstOrDefault(gm => gm.Game.ID == gamingmood.Game.ID && gm.Console?.ID == gamingmood.Console?.ID);
            if(existinggm != null)
            {
                if (gamingmood.IsFavAndNotBlacklisted.HasValue && !gamingmood.IsFavAndNotBlacklisted.Value) // If blacklisted, then never ok to play
                    gamingmood.IsOkToPlay = false;

                existinggm.IsFavAndNotBlacklisted = gamingmood.IsFavAndNotBlacklisted;
                existinggm.IsGameDownloadedYet = gamingmood.IsGameDownloadedYet;
                existinggm.IsOkToPlay = gamingmood.IsOkToPlay;

                _db.SaveChanges();
                return true;
            }
            else
            {
                gamingmood.IsOkToPlay = true;
                currentuser.GamingMoods.Add(gamingmood);
                _db.SaveChanges();
                return true;
            }
        }

        public void UpdateGameSteamId(Game game)
        {
            var existinggame = _db.Games.FirstOrDefault(g => g.Name.IsSimilarTo(game.Name));

            if (existinggame != null && string.IsNullOrEmpty(existinggame.SteamAppId))
            {
                existinggame.SteamAppId = game.SteamAppId;
            }
        }

        public List<GameConsole> GetConsolesList()
        {
            return _db.Consoles.OrderByDescending(x => x.ID).ToList();
        }
    }
}
