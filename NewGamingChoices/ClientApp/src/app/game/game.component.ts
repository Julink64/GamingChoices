import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Game, PlatformPrice } from '../add-game/game';
import { GameService } from '../services/game.service';

@Component({
  selector: 'app-game',
  templateUrl: './game.component.html',
  styleUrls: ['./game.component.css']
})
export class GameComponent implements OnInit {

  game: Game;
  selectedPlatform = 0;

  constructor(private route: ActivatedRoute, private gameService: GameService) {

   }

   ngOnInit() {

    this.route.params.subscribe(params =>
      {
        if(params['gameid'])
        {
           this.gameService.GetGameDetailsById(params['gameid']).subscribe({
             next: (r) => { this.game = r;
                            console.log("jeu");
                            console.log(this.game);
                            this.updatePlatformPrices();
                         },
            error : (e) => console.error(e)});
        }
      });

      console.log(this.game);

  }

  updatePlatformPrices()
  {
    if(this.game.platformPrices)
    {
      for(let i = 0; i < this.game.platformPrices.length; i++)
      {
        console.log("testetestestestes");
        if(this.game.platformPrices[i].platform == 'PC')
        {
          console.log('testhello');
          this.selectedPlatform = i;
        }
      }
    }
  }

}
