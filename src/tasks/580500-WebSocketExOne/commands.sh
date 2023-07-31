cd ../../..

cd src/tasks/580500-WebSocketExOne

cd src/apps/580500-WebSocketExOne

devenv /build Debug ./WebSocketExOne.sln


## I dont think we can build using the following dotnet command. 
## The project is not a dotnet core project. 

cd WebSocketExOneServer

dotnet run --project ./WebSocketExOneServer.csproj

cd ..

cd WebSocketExOneClient

dotnet run --project ./WebSocketExOneClient.csproj

