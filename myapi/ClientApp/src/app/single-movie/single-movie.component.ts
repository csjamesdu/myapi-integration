import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AppHttpClient } from '../services/app-http-client.service';
import { switchMap } from 'rxjs/internal/operators';
import { MoviesItemDetailModel } from '../models/movies.detail.model';

/*const API_URL = 'https://myapi-movie-world.azurewebsites.net/';*/
const API_URL = 'https://localhost:5001/';

@Component({
  selector: 'app-single-movie',
  templateUrl: './single-movie.component.html',
  styleUrls: ['./single-movie.component.css']
})
export class SingleMovieComponent implements OnInit {

  id: number;
  movieDetail: MoviesItemDetailModel = new MoviesItemDetailModel();
  moviePrice: MoviesItemDetailModel = new MoviesItemDetailModel();
  onLoadingDetail: boolean = true;
  onLoadingPrice: boolean = true;
  providedBy: string;


  constructor(private route: ActivatedRoute, private httpClient: AppHttpClient) { }

  ngOnInit() {

    this.route.paramMap.pipe(switchMap((param: any) => {
      this.id = param.params.id;     
      return this.httpClient.get(API_URL + 'api/MyMovies/' + this.id);
    })).subscribe(response => {
      this.onLoadingDetail = false;
      this.movieDetail = new MoviesItemDetailModel(response);   
    });

    this.route.paramMap.pipe(switchMap((param: any) => {
      this.id = param.params.id;
      return this.httpClient.get(API_URL + 'api/Price/' + this.id);
    })).subscribe(response => {
      this.onLoadingPrice = false;
      this.moviePrice = new MoviesItemDetailModel(response);
      this.providedBy = this.getProvider(response); 
    });
  }

  getProvider(response: any): string {
    let prefix = response.ID.substring(0, 2);
    if (prefix == 'cw') return 'Cinema World';
    else if (prefix == 'fw') return 'Film World';
    else return 'Unknown';
  }

}
