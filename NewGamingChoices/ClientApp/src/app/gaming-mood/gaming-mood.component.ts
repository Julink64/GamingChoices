import { Component, OnInit } from '@angular/core';
import { Game, GamingMood } from '../add-game/game';
import { GameService } from '../services/game.service';

@Component({
  selector: 'app-gaming-mood',
  templateUrl: './gaming-mood.component.html',
  styleUrls: ['./gaming-mood.component.css']
})
export class GamingMoodComponent implements OnInit {

  gamingmoods: GamingMood[];

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
    this.loadGamingMoods();
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

  addGamingMood()
  {
    console.log(this.gamewithdetails);
    this.gameService.AddGamingMood(this.gamewithdetails.id).subscribe(
      result => { this.loadGamingMoods(); },
      error => {console.error(error);});


  }

  loadGamingMoods()
  {
    this.gameService.GetGamingMoods().subscribe(
      result => {this.gamingmoods = result;},
      error => {console.error(error);});
  }

  updateIsGameDownloadedYet(gm: GamingMood)
  {
    gm.isGameDownloadedYet = !gm.isGameDownloadedYet;
    this.updateGM(gm);
  }

  updateIsOkToPlay(gm: GamingMood)
  {
    gm.isOkToPlay = !gm.isOkToPlay;
    this.updateGM(gm);
  }

  updateGM(gm: GamingMood)
  {
    this.gameService.UpdateGamingMood(gm).subscribe(
      error => {console.error(error);});

    // this.loadGamingMoods();
  }

}


