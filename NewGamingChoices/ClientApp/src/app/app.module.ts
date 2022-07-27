import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { ApiAuthorizationModule } from 'src/api-authorization/api-authorization.module';
import { AuthorizeGuard } from 'src/api-authorization/authorize.guard';
import { AuthorizeInterceptor } from 'src/api-authorization/authorize.interceptor';
import { AddGameComponent } from './add-game/add-game.component';
import { UserProfileComponent } from './user-profile/user-profile.component';
import { GamingMoodComponent } from './gaming-mood/gaming-mood.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import {MatAutocompleteModule} from '@angular/material/autocomplete';
import { GameComponent } from './game/game.component';
import { BooleanToTextPipe, DiscSizePipe, RequiredPowerPipe } from './services/custompipes.pipe';
import { RedirectGMGuard } from './redirectgm.guard';
import { FriendsListComponent } from './friends-list/friends-list.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    FetchDataComponent,
    AddGameComponent,
    UserProfileComponent,
    GamingMoodComponent,
    GameComponent,
    BooleanToTextPipe,
    DiscSizePipe,
    RequiredPowerPipe,
    FriendsListComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    ApiAuthorizationModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full', canActivate: [RedirectGMGuard] },

      { path: 'fetch-data', component: FetchDataComponent, canActivate: [AuthorizeGuard] },
      { path: 'add-game', component: AddGameComponent, canActivate: [AuthorizeGuard] },
      { path: 'profile', component: UserProfileComponent, canActivate: [AuthorizeGuard] },
      // { path: 'profile', component: UserProfileComponent },
      { path: 'gaming-mood', component: GamingMoodComponent, canActivate: [AuthorizeGuard] },
      // { path: 'gaming-mood', component: GamingMoodComponent },
      { path: 'friends-list', component: FriendsListComponent, canActivate: [AuthorizeGuard] },
      { path: 'game/:gameid', component: GameComponent, canActivate: [AuthorizeGuard] },
      { path: '**', redirectTo: ''}
    ]),
    BrowserAnimationsModule,
    MatAutocompleteModule,
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
