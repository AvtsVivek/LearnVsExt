cd ../../..

cd src/tasks/740600-NamedPipesExOne

cd src/apps/740600-NamedPipesExOne

devenv /build Debug ./NamedPipesExOne.sln

cd NamedPipesExOneClient

dotnet run --project ./NamedPipesExOneClient.csproj

cd NamedPipesExOneServer

dotnet run --project ./NamedPipesExOneServer.csproj

clear
