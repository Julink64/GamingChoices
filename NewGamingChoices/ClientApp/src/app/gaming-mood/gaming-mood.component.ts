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

  errormessage: any;
  sgerrormessage: any;

  gamesearchbarvalue: string;
  gamesuggestions = [];

  wrongsearchbuff: string;

  gamewithdetails: Game;
  selectedPlatform = 0;

  selectedgamename = "";
  selectedgm: GamingMood;

  displaybl: boolean;

  constructor(private gameService: GameService) {

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
        error => {this.errormessage = error;});
    }
    else
    {
      this.gamesuggestions = [];
    }
  }

  getSearchGameDetails()
  {
    this.sgerrormessage = null;
    this.gamewithdetails = null;
    this.gameService.GetGameDetailsByName(this.gamesearchbarvalue).subscribe({
      next: (r) => { this.gamewithdetails = r;
                  },
     error : (e) => this.sgerrormessage = e});

     //Reset search bar
     this.gamesuggestions = [];
     this.wrongsearchbuff = this.gamesearchbarvalue;
     this.gamesearchbarvalue = "";
  }

  addGamingMood()
  {
    this.errormessage = null;
    this.gameService.AddGamingMood(this.gamewithdetails.id).subscribe(
      result => { this.loadGamingMoods(); },
      error => {this.errormessage = error;});
  }

  loadGamingMoods()
  {
    this.errormessage = null;
    this.gameService.GetGamingMoods().subscribe(
      result => {this.gamingmoods = result;},
      error => {this.errormessage = error;});
  }

  selectGM(gm: GamingMood)
  {
    this.selectedgamename = gm.game.name;
    this.selectedgm = gm;
  }


  updateFavorite(gm: GamingMood)
  {
    gm.isFavAndNotBlacklisted = true;
    this.updateGM(gm);
  }

  updateUnFavUnBL(gm: GamingMood)
  {
    gm.isFavAndNotBlacklisted = null;
    this.updateGM(gm);
  }

  blacklistSelectedGM()
  {
    this.updateBlacklist(this.selectedgm);
    let indextobl = this.gamingmoods.findIndex(gm => gm.id == this.selectedgm.id);
    this.gamingmoods[indextobl].isOkToPlay = false;
  }
  updateBlacklist(gm: GamingMood)
  {
    gm.isFavAndNotBlacklisted = false;
    this.updateGM(gm);
  }

  updateIsGameDownloadedYet(gm: GamingMood)
  {
    gm.isGameDownloadedYet = !gm.isGameDownloadedYet;
    this.updateGM(gm);
  }

  updateIsOkToPlay(gm: GamingMood)
  {
    gm.isOkToPlay = !gm.isOkToPlay;
    if(gm.isFavAndNotBlacklisted != null && !gm.isFavAndNotBlacklisted)
    {
      gm.isFavAndNotBlacklisted = null;
    }

    this.updateGM(gm);
  }

  updateGM(gm: GamingMood)
  {
    this.errormessage = null;
    this.gameService.UpdateGamingMood(gm).subscribe(
      r => {},
      error => {this.errormessage = error;});

    // this.loadGamingMoods();
  }


  deleteSelectedGM()
  {
    this.errormessage = null;
    this.gameService.DeleteGamingMood(this.selectedgm.id).subscribe(
      r => {},
      error => {this.errormessage = error;});

    let indextodelete = this.gamingmoods.findIndex(gm => gm.id == this.selectedgm.id);
    this.gamingmoods.splice(indextodelete, 1);
  }
}


