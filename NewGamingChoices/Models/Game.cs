using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NewGamingChoices.Models
{
    [Table("Game")]
    public class Game
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ThumbnailPath { get; set; }

        public int MinPlayers { get; set; }
        public int? MaxPlayers { get; set; }

        public string SteamAppId { get; set; }
        public decimal Price { get; set; }
        public int MinRequiredPower { get; set; }

        public string Genre { get; set; }
        public string Platform { get; set; }
        public int Size { get; set; }

        public bool IsOnMac { get; set; }

        //public int ReviewStatus { get; set; }

    }
}