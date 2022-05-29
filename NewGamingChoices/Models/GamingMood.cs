using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewGamingChoices.Models
{
    public class GamingMood
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        public Game Game { get; set; }

        /// <summary>
        /// null if Computer
        /// </summary>
        public GameConsole Console { get; set; }

        public bool IsOkToPlay { get; set; }

        public bool IsNeverOkToPlay { get; set; }

        public bool IsGameDownloadedYet { get; set; }

        //public TimeSpan FavourDuration { get; set; }

        public GamingMood(Game game)
        {
            Game = game;
            IsOkToPlay = true;
        }

        private GamingMood()
        { }
    }
}
