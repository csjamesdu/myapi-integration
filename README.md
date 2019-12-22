# Movie World

This project implements a web app that query from two different API providers and present the available movies as well as the best price for each movie.
  - Angular 8 as integrated SPA
  - Dotnet Core 3.1 as Backend Host

### Features

  - Query two different third party APIs with 3 retry attempts and combine the results;
  - Cache API query results with In Memory Cache mechanism provided by the platform;
  - Write the movies list into the In Memory DB as backup after API query sucessfully;

### Adjustments
- The Id of each movie is unified by cutting off the prefix.
- The poster uri are all substitued by valid resources.

### Run 
- Under Visual Studio 2019: Please select debugging "myapi" and run debugging
- Under VSCode : Either Debugging or Run Without Debugging
