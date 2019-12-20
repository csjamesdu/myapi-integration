export class MoviesResponse {
  Movies: Array<MoviesItem> = new Array<MoviesItem>();

  constructor(obj?: any) {
    if (obj && obj.Movies && obj.Movies.length > 0) {
      for (const i of obj.Movies) {
        this.Movies.push(new MoviesItem(i));
      }
    }
  }
}

export class MoviesItem {
  Title: string;
  Year: number;
  ID: string;
  Type: string;
  Poster: string;

  constructor(obj?: any) {
    this.Title = obj && obj.Title || null;
    this.Year = obj && obj.Year || null;
    this.ID = obj && obj.ID || null;
    this.Type = obj && obj.Type || null;
    this.Poster = obj && obj.Poster || null;
  }
}

