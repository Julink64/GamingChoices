import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Game } from '../add-game/game';

@Injectable({
  providedIn: 'root'
})
export class GameService {

  constructor(private http:HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

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
}
