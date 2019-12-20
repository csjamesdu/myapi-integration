
export class MoviesItemDetailModel {

  Title: string;
  Year: number;
  Poster: string;
  ID: string;
  Type: string;
  Rated: string;
  Released: string;
  Runtime: string;
  Genre: string;
  Director: string;
  Writer: string;
  Actors: string;
  Plot: string;
  Language: string;
  Country: string;
  Awards: string;
  Metascore: number;
  Rating: number;
  Votes: number;
  Price: number;

  constructor(obj?: any) {

    this.Title = obj && obj.Title || null;
    this.Year = obj && obj.Year || null;
    this.Poster = obj && obj.Poster || null;
    this.ID = obj && obj.ID || null;
    this.Type = obj && obj.Type || null;
    this.Rated = obj && obj.Rated || null;
    this.Released = obj && obj.Released || null;
    this.Runtime = obj && obj.Runtime || null;
    this.Genre = obj && obj.Genre || null;
    this.Director = obj && obj.Director || null;
    this.Writer = obj && obj.Writer || null;
    this.Actors = obj && obj.Actors || null;
    this.Plot = obj && obj.Plot || null;
    this.Language = obj && obj.Language || null;
    this.Country = obj && obj.Country || null;
    this.Awards = obj && obj.Awards || null;
    this.Metascore = obj && obj.Metascore || null;
    this.Rating = obj && obj.Rating || null;
    this.Votes = obj && obj.Votes || null;
    this.Price = obj && obj.Price || null;
  }
}
