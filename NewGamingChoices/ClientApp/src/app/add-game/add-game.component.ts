import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { GameService } from '../services/game.service';
import { Game } from './game';

@Component({
  selector: 'app-add-game',
  templateUrl: './add-game.component.html',
  styleUrls: ['./add-game.component.css']
})
export class AddGameComponent implements OnInit {

  baseUrl: string;

  submittedGame: Game;

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
    this.submittedGame.MinPlayers = 2;

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
    value = value.replace(",", ".");

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


  onSubmit(gameForm: NgForm) {
    console.log("submit game");
    console.log(this.submittedGame);

    if(gameForm.valid && this.atleastoneplatformselected())
    {
      console.log('testvalid');

    // let headers = new HttpHeaders({
    //   'Content-Type': 'application/json',
    // });
    // let options = { headers: headers };
    // this.http.post(this.baseUrl + 'game/addnewgame', JSON.stringify(this.submittedGame), options).subscribe(result => {

    // }, error => console.error(error));
    }

  }
}

