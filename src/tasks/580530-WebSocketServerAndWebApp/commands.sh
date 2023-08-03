cd ../../..

cd src/tasks/580530-WebSocketServerAndWebApp

cd src/apps/580530-WebSocketServerAndWebApp

devenv /build Debug ./WebSocketServerAndWebApp.sln


## I dont think we can build using the following dotnet command. 
## The project is not a dotnet core project. 

cd WebSocketServerAndWebPageServer

dotnet run --project ./WebSocketServerAndWebApp.csproj



