cd ../../..

cd src/tasks/580300-WebSocketExConsole

cd src/apps/580300-WebSocketExConsole

devenv /build Debug ./WebSocketExConsole.sln


## I dont think we can build using the following dotnet command. 
## The project is not a dotnet core project. 

cd WebSocketExConsoleServer

dotnet run --project ./WebSocketExConsoleServer.csproj

cd ..

cd WebSocketExConsoleClient

dotnet run --project ./WebSocketExConsoleClient.csproj

