import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Game, GamingMood } from '../add-game/game';

@Injectable({
  providedIn: 'root'
})
export class GameService {

  constructor(private http:HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  public AddGame(submittedGame: Game): Observable<any>
  {
    let headers = new HttpHeaders({
    'Content-Type': 'application/json',
    });
    let options = { headers: headers };
    return this.http.post(this.baseUrl + 'game/addnewgame', JSON.stringify(submittedGame), options);
  }

  public AddOrUpdateGamingMood(gm: GamingMood): Observable<any>
  {
    let headers = new HttpHeaders({
    'Content-Type': 'application/json',
    });
    let options = { headers: headers };
    return this.http.post(this.baseUrl + 'game/addorupdategm', JSON.stringify(gm), options);
  }

  public GetConsolesList(): Observable<any>
  {
    let headers = new HttpHeaders({
    'Content-Type': 'application/json',
    });

    let options = { headers: headers};
    return this.http.get(this.baseUrl + 'game/getconsoleslist', options);
  }

  public SearchGame(term: string): Observable<any>
  {
    let headers = new HttpHeaders({
      'Content-Type': 'application/json',
      });
      let params = new HttpParams().set('term', term);

      let options = { headers: headers, params: params };
      return this.http.get<Game[]>(this.baseUrl + 'game/searchgame', options);
  }

  public GetGameDetails(id: string): Observable<Game>
  {
    let headers = new HttpHeaders({
      'Content-Type': 'application/json',
      });
      let params = new HttpParams().set('gameid', id);

      let options = { headers: headers, params: params };
      return this.http.get<Game>(this.baseUrl + 'game/gamedetails', options);
  }
}
