import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
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
    let headers = new HttpHeaders({
    'Content-Type': 'application/json',
    });
    let params = new HttpParams().set('useremail', this.GetCurrentUserIdentifier());

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
}
