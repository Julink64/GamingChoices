import { Component, OnInit } from '@angular/core';
import { UserService } from '../services/user.service';
import { Friend } from './Friend';

@Component({
  selector: 'app-friends-list',
  templateUrl: './friends-list.component.html',
  styleUrls: ['./friends-list.component.css']
})
export class FriendsListComponent implements OnInit {

  errormessage: any;

  successmessage: string;
  successtitle: string;

  friendslist: Friend[];
  askingfriendslist: Friend[];

  friendsearchbarvalue: string;
  friendsuggestions = [];

  wrongsearchbuff: string;

  friendwithdetails: Friend;
  selectedFriendForDeletion: Friend;

  constructor(private userService: UserService) { }

  ngOnInit() {
    this.userService.GetCurrentUserDetails().subscribe(
      result =>  { this.friendslist = result.friendsList;
                   this.askingfriendslist = result.askedFriendsList;},
      error => {this.errormessage = error;});
  }

  updateFriendSugg()
  {
    if(this.friendsearchbarvalue)
    {
      this.userService.SearchFriend(this.friendsearchbarvalue).subscribe(
        result =>  { this.friendsuggestions = result; },
        error => {this.errormessage = error;});
    }
  }

  getSearchFriendDetails()
  {
    this.successmessage = null;
    this.successtitle = null;
    this.errormessage = null;
    this.friendwithdetails = null;
    this.userService.GetUser(this.friendsearchbarvalue).subscribe({
      next: (r) => {
        if(r)
        {
          this.friendwithdetails = {id: r.id, userName: r.userName}
        }
                  },
     error : (e) => this.errormessage = e});

     //Reset search bar
     this.friendsuggestions = [];
     this.wrongsearchbuff = this.friendsearchbarvalue;
     this.friendsearchbarvalue = "";
  }

  sendFR()
  {
    this.errormessage = null;
    this.userService.SendFriendRequest(this.friendwithdetails).subscribe(
      r => {this.successtitle = "Demande envoyée !"; this.successmessage = "Demande d'ami envoyée avec succès à " + this.friendwithdetails.userName + " !"},
      error => {this.errormessage = error;});
  }

  addFriend(friend: Friend)
  {
    this.errormessage = null;
    this.userService.AddFriend(friend).subscribe(
      r => {this.successtitle = "Nouvel ami !"; this.successmessage = friend.userName + " et toi êtes à présent amis !"},
      error => {this.errormessage = error;});

    this.friendslist.push(friend);

    this.deleteFR(friend);
  }

  deleteFR(friend: Friend)
  {
    this.errormessage = null;
    this.userService.DeleteFriendRequest(friend).subscribe(
      r => {},
      error => {this.errormessage = error;});

    const index = this.askingfriendslist.indexOf(friend, 0);
    this.askingfriendslist.splice(index, 1);
  }

  deleteSelectedFriend()
  {
    this.errormessage = null;
    this.userService.DeleteFriend(this.selectedFriendForDeletion).subscribe(
      r => {this.successtitle = "Ami retiré"; this.successmessage = this.selectedFriendForDeletion.userName + " a bien été retiré de la liste d'amis."},
      error => {this.errormessage = error;});

    const index = this.friendslist.indexOf(this.selectedFriendForDeletion, 0);
    this.friendslist.splice(index, 1);
  }

}
