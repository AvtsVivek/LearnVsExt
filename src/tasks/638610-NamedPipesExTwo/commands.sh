cd ../../..

cd src/tasks/638610-NamedPipesExTwo

cd src/apps/638610-NamedPipesExTwo

devenv /build Debug ./NamedPipesExTwo.sln

cd NamedPipesExTwoClient

dotnet run --project ./NamedPipesExTwoClient.csproj

cd NamedPipesExTwoServer

dotnet run --project ./NamedPipesExTwoServer.csproj

clear
