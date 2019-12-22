import { Component, OnInit } from '@angular/core';
import { AppHttpClient } from '../services/app-http-client.service';
import { MoviesItemDetailModel } from '../models/movies.detail.model';
import { MoviesResponse } from '../models/movies.model';

const API_URL = 'https://localhost:5001/';

@Component({
  selector: 'app-movie-list',
  templateUrl: './movie-list.component.html',
  styleUrls: ['./movie-list.component.css']
})
export class MovieListComponent implements OnInit {

  allMovies: MoviesResponse = new MoviesResponse();
  singleDetail: MoviesItemDetailModel = new MoviesItemDetailModel();
  onLoading: boolean = true;

  constructor(private httpClient: AppHttpClient) { }

  ngOnInit() {

    this.requestMovies().subscribe(response => {
      this.onLoading = false;
      this.allMovies = new MoviesResponse(response);
      this.allMovies.Movies.sort((a, b) =>
        (a.Year > b.Year) ? -1 : 1
      );
    });
  }

  requestMovies(): any {
    return this.httpClient.get(API_URL + 'api/MyMovies');
  }
}
