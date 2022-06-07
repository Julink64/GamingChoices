import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { GameService } from '../services/game.service';
import { Game, PlatformPrice } from './game';

@Component({
  selector: 'app-add-game',
  templateUrl: './add-game.component.html',
  styleUrls: ['./add-game.component.css']
})
export class AddGameComponent implements OnInit {

  baseUrl: string;

  submittedGame = new Game();

  genres = ['Jeux de Course', 'Jeux de Combat',
    'FPS', 'Jeux d\'Aventure'];

  platforms = [];
  consoles = [];

  minplayersvalues = [];
  maxplayersvalues = [];

  selectedplatforms = new Array<boolean>(this.platforms.length);
  platformsprices = new Array<number>(this.platforms.length);

  submit: boolean;

  constructor(private gameService:GameService, private http: HttpClient, @Inject('BASE_URL') baseUrl: string) { this.baseUrl = baseUrl; }

  ngOnInit() {
    this.gameService.GetConsolesList().subscribe(
      result =>  { this.consoles = result.map(o => o.name);
                   this.platforms = ['PC'].concat(this.consoles);
                  },
      error => {console.error(error);});



    for(let i = 0; i < 19; i++)
    {
      this.minplayersvalues[i] = i+2;
    }
    this.submittedGame.minPlayers = 2;

    for(let i = 0; i < 98; i++)
    {
      this.maxplayersvalues[i] = i+2;
    }

  }

  atleastoneplatformselected()
  {
    return this.selectedplatforms.some((a) => a);
  }

  atleastonepriceinvalid()
  {
    return this.platformsprices.some((a) => a && !this.ispricevalid(a));
  }

  ispricevalid(value: any)
  {
    // value = value.replace(",", ".");

    if(value.includes("-"))
      return false;

    return !isNaN(value);
  }

  isdisksizevalid(value: any)
  {
    if(value.includes("-") || value.includes("."))
      return false;

    return !isNaN(value);
  }

  assignPlatformPrices()
  {
    let pfp = [];
    for(let i = 0; i < this.platforms.length; i++)
    {
      if(this.selectedplatforms[i])
      {
        let pp = new PlatformPrice();
        pp.platform = this.platforms[i];
        pp.price = this.platformsprices[i];

        pfp.push(pp);
      }
    }

    this.submittedGame.platformPrices = pfp;
  }

  onSubmit(gameForm: NgForm) {

    if(gameForm.valid && this.atleastoneplatformselected())
    {
      this.assignPlatformPrices();

      console.log(this.submittedGame);
      this.gameService.AddGame(this.submittedGame).subscribe(result => {
        //TODO
      }
      , error => console.error(error));
    }

  }

  isMoreThanOnePlatformSelected()
  {
    let isMoreThanOnePlatformSelected = this.selectedplatforms.filter(Boolean).length > 1;

    if(!isMoreThanOnePlatformSelected)
    {
      this.submittedGame.isCrossPlatform = false;
    }

    return isMoreThanOnePlatformSelected;
  }
}

