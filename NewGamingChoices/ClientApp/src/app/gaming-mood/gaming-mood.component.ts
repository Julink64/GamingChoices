import { Component, OnInit } from '@angular/core';
import { Game } from '../add-game/game';
import { GameService } from '../services/game.service';

@Component({
  selector: 'app-gaming-mood',
  templateUrl: './gaming-mood.component.html',
  styleUrls: ['./gaming-mood.component.css']
})
export class GamingMoodComponent implements OnInit {

  gamingmoods = [];

  gamesearchbarvalue: string;
  gamesuggestions = [];

  gamewithdetails: Game;
  selectedPlatform = 0;

  displaybl: boolean;

  constructor(private gameService: GameService) {

    // this.gamingmoods.push("Minecraft");
    // this.gamingmoods.push("Jeu avec un nom franchement extrèmement vraiment très long");
    // this.gamingmoods.push("Super Smash Bros Ultimate");
    // this.gamingmoods.push("Overwatch");
    // this.gamingmoods.push("Rocket League");
    // this.gamingmoods.push("Don't Starve Together");

   }

  ngOnInit() {
  }

  updateGameSugg()
  {
    if(this.gamesearchbarvalue)
    {
      this.gameService.SearchGame(this.gamesearchbarvalue).subscribe(
        result =>  { this.gamesuggestions = result; },
        error => {console.error(error);});
    }
  }

  getSearchGameDetails()
  {
    this.gamewithdetails = null;
    this.gameService.GetGameDetailsByName(this.gamesearchbarvalue).subscribe({
      next: (r) => { this.gamewithdetails = r;
                  },
     error : (e) => console.error(e)});

     //Reset search bar
     this.gamesuggestions = [];
     this.gamesearchbarvalue = "";
  }

}
