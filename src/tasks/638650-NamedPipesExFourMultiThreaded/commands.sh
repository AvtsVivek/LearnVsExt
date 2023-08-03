cd ../../..

cd ../../../..

cd src/tasks/738650-NamedPipesExFourMultiThreaded

cd src/apps/738650-NamedPipesExFourMultiThreaded

devenv /build Debug ./NamedPipesExFourMultiThreaded.sln

cd NamedPipesExFourMultiThreadedClient

dotnet run --project ./NamedPipesExFourMultiThreadedClient.csproj

cd ../../../..

cd ../../../

cd src/tasks/738650-NamedPipesExFourMultiThreaded

cd src/apps/738650-NamedPipesExFourMultiThreaded

cd NamedPipesExFourMultiThreadedServer

dotnet run --project ./NamedPipesExFourMultiThreadedServer.csproj

clear
