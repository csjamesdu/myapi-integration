# Movie World

This project implements a web app that queries from two different API providers and presents the available movies as well as the best price for each movie.
  - Front-End: Angular 8 as integrated SPA
  - Back-End: Dotnet Core 3.1 as Host Server
  

### Run 
#### Web Host: 
	-Azure: https://myapi-movie-world.azurewebsites.net/
#### Local Debug:
- Under Visual Studio 2019: Please select debugging "myapi" and run debugging.
- Under VSCode : Either Debugging or Run Without Debugging.
- Localhost should be automatically launched within the default browser.  

### Features

  - Query two different third party APIs with 3 retry attempts and combine the results;
  - Write the movies list into the In Memory DB as backups after API query respond sucessfully;
  - Cache API query results including movie list and details with In Memory Cache mechanism provided by the platform;
  - Movie price will not be cached, it will be querid directly through API each time;
  

### Adjustments
- The ID of each movie is unified by cutting off the prefix.
- The poster urls are all replaced by valid resources.

### Development RoadMap

#### Implemented as at 22/12/2019:
- Query supplier APIs and get sample responses;
- Initiate Angular Project with sample response as In Memory Data; Repo: https://github.com/csjamesdu/movie-world.git
- Develop backend controller and services to connect with third party APIs and process the response data
- Integrate Angular into the Dotnet Core Project 
- Add retry policy, in memory cache mechanism and in memory database at backend to enhance resilience
- Polish front-end style and add error handling mechanism with http-interceptor

#### Future Possibilities:
- Build docker image to containerize the project
- Deploy docker on the AZURE cloud platform