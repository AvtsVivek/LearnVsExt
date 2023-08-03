cd ../../..

cd src/tasks/638600-NamedPipesExOne

cd src/apps/638600-NamedPipesExOne

devenv /build Debug ./NamedPipesExOne.sln

cd NamedPipesExOneClient

dotnet run --project ./NamedPipesExOneClient.csproj

cd NamedPipesExOneServer

dotnet run --project ./NamedPipesExOneServer.csproj

clear
