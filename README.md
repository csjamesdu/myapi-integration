
#Platform:
1. Use Angular as front-end SPA

2. Use .Net Core 3.1 as backend service Host.

#Features:
1. Query two different third party APIs with 3 retry attempts and combine the results;

2. Cache API query results with In Memory Cache mechanism provided by the platform;

3. Write the movies list into the In Memory DB as backup after API query sucessfully;
 
#Adjustments:
1. The Id of each movie is unified by cutting off the prefix.

2. The poster uri are all substitued by valid resources.

#Run Project in IDE:
Under Visual Studio - Please select debugging myapi and run debugging;

Under VS Code - Either Debugging or Run Without Debugging;