cd ../../..

cd ../../../..

cd src/tasks/739530-JsonRpcNamedPipe

cd src/apps/739530-JsonRpcNamedPipe

devenv /build Debug ./JsonRpcNamedPipe.sln

cd JsonRpcNamedPipeClient

dotnet run --project ./JsonRpcNamedPipeClient.csproj

cd ../../../..

cd ../../../

cd src/tasks/739530-JsonRpcNamedPipe

cd src/apps/739530-JsonRpcNamedPipe

cd JsonRpcNamedPipeServer

dotnet run --project ./JsonRpcNamedPipeServer.csproj

clear