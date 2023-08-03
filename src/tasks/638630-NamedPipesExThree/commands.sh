cd ../../..

cd src/tasks/638630-NamedPipesExThree

cd src/apps/638630-NamedPipesExThree

devenv /build Debug ./NamedPipesExThree.sln

cd NamedPipesExThreeClient

dotnet run --project ./NamedPipesExThreeClient.csproj

cd NamedPipesExThreeServer

dotnet run --project ./NamedPipesExThreeServer.csproj

clear
