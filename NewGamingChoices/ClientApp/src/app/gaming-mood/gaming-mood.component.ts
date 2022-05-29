import { Component, OnInit } from '@angular/core';
import { Game } from '../add-game/game';

@Component({
  selector: 'app-gaming-mood',
  templateUrl: './gaming-mood.component.html',
  styleUrls: ['./gaming-mood.component.css']
})
export class GamingMoodComponent implements OnInit {

  gamingmoods = [];


  constructor() {

    this.gamingmoods.push("Minecraft");
    this.gamingmoods.push("Jeu avec un nom franchement extrèmement vraiment très long");
    this.gamingmoods.push("Super Smash Bros Ultimate");
    this.gamingmoods.push("Overwatch");
    this.gamingmoods.push("Rocket League");
    this.gamingmoods.push("Don't Starve Together");

   }

  ngOnInit() {
  }

}
