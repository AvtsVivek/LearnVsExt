cd ../../..

cd ../../../..

cd src/tasks/638650-NamedPipesExFourMultiThreaded

cd src/apps/638650-NamedPipesExFourMultiThreaded

devenv /build Debug ./NamedPipesExFourMultiThreaded.sln

cd NamedPipesExFourMultiThreadedClient

dotnet run --project ./NamedPipesExFourMultiThreadedClient.csproj

cd ../../../..

cd ../../../

cd src/tasks/638650-NamedPipesExFourMultiThreaded

cd src/apps/638650-NamedPipesExFourMultiThreaded

cd NamedPipesExFourMultiThreadedServer

dotnet run --project ./NamedPipesExFourMultiThreadedServer.csproj

clear
