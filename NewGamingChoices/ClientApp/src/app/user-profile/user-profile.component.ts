import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { GameConsole } from '../models/console';
import { GameService } from '../services/game.service';
import { GCUser } from '../services/user';
import { UserService } from '../services/user.service';

@Component({
  selector: 'user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css']
})
export class UserProfileComponent implements OnInit {

  user: GCUser;
  computerPowerValues = ["Très performant (Fait tourner tous les jeux)", "Performant (Fait tourner presque tous les jeux)", "Standard (Fait tourner certains jeux)", "Limité (Ne fait tourner que quelques jeux)", "Basique (Le navigateur et Word c'est à peu près tout)"];
  internetNetworkQualityValues = ["Excellente (> 5Mo/s)", "Bonne (> 1Mo/s)", "Correcte (> 500Ko/s)", "Mauvaise (< 500 Ko/s)"];
  consoles = [];
  selectedconsoles = [];

  userconsoles: GameConsole[];

  constructor(private userService: UserService, private gameService: GameService) {

   }

  ngOnInit() {
  this.initData();
  }

  initData() {
    this.gameService.GetConsolesList().subscribe(
      result =>  { this.consoles = result;
                   this.selectedconsoles = new Array<boolean>(result.length);
                  },
      error => {console.error(error);});

      this.userService.GetCurrentUserDetails().subscribe(
        result =>  {this.user = result;

          this.userconsoles = JSON.parse(this.user.consoles);

          for(let i=0; i < this.consoles.length; i++)
          {
            for(let j=0; j<this.userconsoles.length; j++)
            {
                if(this.userconsoles[j].id == this.consoles[i].id && this.userconsoles[j].name == this.consoles[i].name)
                {
                  this.selectedconsoles[i] = true;
                }
            }

          }
        },
        error => {console.error(error);});
  }

  onSubmit(profileForm: NgForm) {
    console.log("submit profile");

    if(profileForm.valid)
    {
      console.log('testvalid');

      let updateduserconsoles = [];
      for(let i=0; i < this.consoles.length; i++)
      {
        if(this.selectedconsoles[i])
        {
          updateduserconsoles.push(this.consoles[i]);
        }
      }
      this.user.consoles = JSON.stringify(updateduserconsoles);

      console.log(this.user);

      this.userService.UpdateUserInfo(this.user).subscribe(
        result =>  console.log("User is updated"),
        error => {console.error(error);});

    }

  }

}
