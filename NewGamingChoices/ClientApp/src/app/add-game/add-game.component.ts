import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';

@Component({
  selector: 'app-add-game',
  templateUrl: './add-game.component.html',
  styleUrls: ['./add-game.component.css']
})
export class AddGameComponent implements OnInit {

  baseUrl: string;

  submittedGame = new Game();

  genres = ['Course', 'Combat',
    'FPS', 'Aventure'];

  platforms = ['PC', 'Nintendo Switch',
    'Xbox Series X', 'Xbox One', 'PlayStation 4', 'Playstation 5'];

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) { this.baseUrl = baseUrl; }

  ngOnInit() {

  }

  onSubmitGame() {
    console.log("submit game");
    console.log(this.submittedGame);

    let headers = new HttpHeaders({
      'Content-Type': 'application/json',
    });
    let options = { headers: headers };
    this.http.post(this.baseUrl + 'game/addnewgame', JSON.stringify(this.submittedGame), options).subscribe(result => {

    }, error => console.error(error));

  }
}

export class Game {
  ID: number;
  Name: string;
  Description: string;
  ThumbnailPath: string;
  MinPlayers: number;
  MaxPlayers: number;
  SteamAppId: string;
  Price: number;
  MinRequiredPower: number;
  Genre: string;
  Platform: string;
  Size: number;
  IsOnMac: boolean;
}


