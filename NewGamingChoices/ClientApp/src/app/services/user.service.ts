import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { Friend } from '../friends-list/Friend';
import { GCUser } from './user';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private authorizeService: AuthorizeService, private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  public GetCurrentUserIdentifier(): string
  {
    let useridentifier= null;

    this.authorizeService.getUser().pipe(map(u => u && u.name)).subscribe(
      data => {useridentifier = data;}
    );

    return useridentifier;
  }

  public GetCurrentUserDetails(): Observable<GCUser>
  {
    return this.GetUser(this.GetCurrentUserIdentifier());
  }

  public GetUser(username: string): Observable<GCUser>
  {
    let headers = new HttpHeaders({
    'Content-Type': 'application/json',
    });
    let params = new HttpParams().set('username', username);

    let options = { headers: headers, params: params };
    return this.http.get<GCUser>(this.baseUrl + 'user/getuser', options);
  }

  public UpdateUserInfo(user: GCUser): Observable<any>
  {
    let headers = new HttpHeaders({
      'Content-Type': 'application/json',
      });

      let options = { headers: headers };
      return this.http.post<GCUser>(this.baseUrl + 'user/updateuser', user, options);
  }

  //#region friends

  public SearchFriend(term: string): Observable<any>
  {
    let headers = new HttpHeaders({
      'Content-Type': 'application/json',
      });
      let params = new HttpParams().set('term', term);

      let options = { headers: headers, params: params };
      return this.http.get<string[]>(this.baseUrl + 'user/searchfriend', options);
  }

  public SendFriendRequest(friend: Friend): Observable<any>
  {
    let headers = new HttpHeaders({
      'Content-Type': 'application/json',
      });

      let options = { headers: headers };
      return this.http.post<string>(this.baseUrl + 'user/sendfr', {id: friend.id}, options);
  }

  public AddFriend(friend: Friend): Observable<any>
  {
    let headers = new HttpHeaders({
      'Content-Type': 'application/json',
      });

      let options = { headers: headers };
      return this.http.post<string>(this.baseUrl + 'user/addfriend', {id: friend.id}, options);
  }

  public DeleteFriendRequest(friend: Friend): Observable<any>
  {
    let headers = new HttpHeaders({
      'Content-Type': 'application/json',
      });

      let options = { headers: headers };
      return this.http.post<string>(this.baseUrl + 'user/deleteFR', {id: friend.id}, options);
  }

  public DeleteFriend(friend: Friend): Observable<any>
  {
    let headers = new HttpHeaders({
      'Content-Type': 'application/json',
      });

      let options = { headers: headers };
      return this.http.post<string>(this.baseUrl + 'user/deleteFriend', {id: friend.id}, options);
  }

  //#endregion

}
