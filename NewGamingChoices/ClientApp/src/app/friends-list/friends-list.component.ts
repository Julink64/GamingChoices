import { Component, OnInit } from '@angular/core';
import { UserService } from '../services/user.service';
import { Friend } from './Friend';

@Component({
  selector: 'app-friends-list',
  templateUrl: './friends-list.component.html',
  styleUrls: ['./friends-list.component.css']
})
export class FriendsListComponent implements OnInit {

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
      error => {console.error(error);});
  }

  updateFriendSugg()
  {
    if(this.friendsearchbarvalue)
    {
      this.userService.SearchFriend(this.friendsearchbarvalue).subscribe(
        result =>  { this.friendsuggestions = result; },
        error => {console.error(error);});
    }
  }

  getSearchFriendDetails()
  {
    this.friendwithdetails = null;
    this.userService.GetUser(this.friendsearchbarvalue).subscribe({
      next: (r) => {
        if(r)
        {
          this.friendwithdetails = {id: r.id, userName: r.userName}
        }
                  },
     error : (e) => console.error(e)});

     //Reset search bar
     this.friendsuggestions = [];
     this.wrongsearchbuff = this.friendsearchbarvalue;
     this.friendsearchbarvalue = "";
  }

  sendFR()
  {
    this.userService.SendFriendRequest(this.friendwithdetails).subscribe(
      error => {console.error(error);});
  }

  addFriend(friend: Friend)
  {
    this.userService.AddFriend(friend).subscribe(
      error => {console.error(error);});

    this.friendslist.push(friend);

    this.deleteFR(friend);
  }

  deleteFR(friend: Friend)
  {
    this.userService.DeleteFriendRequest(friend).subscribe(
      error => {console.error(error);});

    const index = this.askingfriendslist.indexOf(friend, 0);
    this.askingfriendslist.splice(index, 1);
  }

  deleteSelectedFriend()
  {
    this.userService.DeleteFriend(this.selectedFriendForDeletion).subscribe(
      error => {console.error(error);});

    const index = this.friendslist.indexOf(this.selectedFriendForDeletion, 0);
    this.friendslist.splice(index, 1);
  }

}
