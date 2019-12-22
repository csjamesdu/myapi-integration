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
- The poster urls are all replaced by valid resources.

### Run 
- Under Visual Studio 2019: Please select debugging "myapi" and run debugging
- Under VSCode : Either Debugging or Run Without Debugging


### Development RoadMap

#### Imeplemented as at 22/12/2019:
- Query supplier APIs and get sample responses;
- Initiate Angular Project with sample response as In Memory Data; Repo: https://github.com/csjamesdu/movie-world.git
- Develop backend services to connect with third party APIs and process the response data
- Integrate Angular into the Dotnet Core Project 
- Add retry policy, in memory cache mechanism and in memory database at backend
- Polishing front-end style and add error handling mechanism with http-interceptor

#### Future
- Build docker image to containerize the project
- Deploy docker on the AZURE cloud platform