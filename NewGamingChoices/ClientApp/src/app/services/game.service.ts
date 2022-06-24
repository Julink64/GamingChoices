import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Game, GamingMood } from '../add-game/game';

@Injectable({
  providedIn: 'root'
})
export class GameService {

  constructor(private http:HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

//#region Game
  public AddGame(submittedGame: Game): Observable<any>
  {
    let headers = new HttpHeaders({
    'Content-Type': 'application/json',
    });
    let options = { headers: headers };
    return this.http.post(this.baseUrl + 'game/addnewgame', JSON.stringify(submittedGame), options);
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

  public GetGameDetailsById(id: string): Observable<Game>
  {
    let headers = new HttpHeaders({
      'Content-Type': 'application/json',
      });
      let params = new HttpParams().set('gameid', id);

      let options = { headers: headers, params: params };
      return this.http.get<Game>(this.baseUrl + 'game/gamedetailsid', options);
  }

  public GetGameDetailsByName(name: string): Observable<Game>
  {
    let headers = new HttpHeaders({
      'Content-Type': 'application/json',
      });
      let params = new HttpParams().set('gamename', name);

      let options = { headers: headers, params: params };
      return this.http.get<Game>(this.baseUrl + 'game/gamedetailsname', options);
  }

//#endregion

//#region Console
  public GetConsolesList(): Observable<any>
  {
    let headers = new HttpHeaders({
    'Content-Type': 'application/json',
    });

    let options = { headers: headers};
    return this.http.get(this.baseUrl + 'game/getconsoleslist', options);
  }
//#endregion

//#region Gaming Mood
  public AddGamingMood(gameid: number): Observable<any>
  {
    let headers = new HttpHeaders({
    'Content-Type': 'application/json',
    });
    let options = { headers: headers };
    return this.http.post(this.baseUrl + 'game/addgm', gameid, options);
  }

  public UpdateGamingMood(gm: GamingMood): Observable<any>
  {
    let headers = new HttpHeaders({
    'Content-Type': 'application/json',
    });
    let options = { headers: headers };
    return this.http.post(this.baseUrl + 'game/updategm', JSON.stringify(gm), options);
  }

  public DeleteGamingMood(gmid: string): Observable<any>
  {
    let headers = new HttpHeaders({
    'Content-Type': 'application/json',
    });
    let options = { headers: headers };
    return this.http.post(this.baseUrl + 'game/deletegm', JSON.stringify(gmid), options);
  }

  public GetGamingMoods(): Observable<any>
  {
    let headers = new HttpHeaders({
      'Content-Type': 'application/json',
      });

      let options = { headers: headers };
      return this.http.get<GamingMood[]>(this.baseUrl + 'game/getgm', options);
  }
//#endregion
}
