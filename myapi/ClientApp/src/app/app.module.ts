import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { MovieListComponent } from './movie-list/movie-list.component';
import { SingleMovieComponent } from './single-movie/single-movie.component';
import { ErrorComponent } from './error/error.component';
import { BlankComponent } from './blank/blank.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatCardModule } from '@angular/material';
import { AppHttpClient } from './services/app-http-client.service';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    MovieListComponent,
    SingleMovieComponent,
    ErrorComponent,
    BlankComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'movies', component: MovieListComponent },
      { path: 'movie/:id', component: SingleMovieComponent },
      { path: '**', component: BlankComponent },
    ]),
    BrowserAnimationsModule,
    MatCardModule,
  ],
  providers: [AppHttpClient],
  bootstrap: [AppComponent]
})
export class AppModule { }
