cd ../../..

cd src/tasks/639500-JsonRpcStdIo

cd src/apps/639500-JsonRpcStdIo


# The following build commands does not seem to work. 
# For debugging, build using visual studio only!!!
devenv /build Debug ./JsonRpcStdIo.sln

cd JsonRpcStdIoClient
dotnet run --project ./JsonRpcStdIoClient.csproj

dotnet run --project ./JsonRpcStdIoClient/JsonRpcStdIoClient.csproj

clear
