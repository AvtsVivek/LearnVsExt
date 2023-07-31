cd ../../..

cd src/tasks/580520-WebSocketServerAndWebPage

cd src/apps/580520-WebSocketServerAndWebPage

devenv /build Debug ./WebSocketServerAndWebPage.sln


## I dont think we can build using the following dotnet command. 
## The project is not a dotnet core project. 

cd WebSocketServerAndWebPageServer

dotnet run --project ./WebSocketServerAndWebPageServer.csproj



