"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var MoviesResponse = /** @class */ (function () {
    function MoviesResponse(obj) {
        this.Movies = new Array();
        if (obj && obj.Movies && obj.Movies.length > 0) {
            for (var _i = 0, _a = obj.Movies; _i < _a.length; _i++) {
                var i = _a[_i];
                this.Movies.push(new MoviesItem(i));
            }
        }
    }
    return MoviesResponse;
}());
exports.MoviesResponse = MoviesResponse;
var MoviesItem = /** @class */ (function () {
    function MoviesItem(obj) {
        this.Title = obj && obj.Title || null;
        this.Year = obj && obj.Year || null;
        this.ID = obj && obj.ID || null;
        this.Type = obj && obj.Type || null;
        this.Poster = obj && obj.Poster || null;
    }
    return MoviesItem;
}());
exports.MoviesItem = MoviesItem;
//# sourceMappingURL=movies.model.js.map