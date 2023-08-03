cd ../../..

cd src/tasks/738610-NamedPipesExTwo

cd src/apps/738610-NamedPipesExTwo

devenv /build Debug ./NamedPipesExTwo.sln

cd NamedPipesExTwoClient

dotnet run --project ./NamedPipesExTwoClient.csproj

cd NamedPipesExTwoServer

dotnet run --project ./NamedPipesExTwoServer.csproj

clear
