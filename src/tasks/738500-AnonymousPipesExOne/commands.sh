cd ../../..

cd src/tasks/740500-AnonymousPipesExOne

cd src/apps/740500-AnonymousPipesExOne


# The following build commands does not seem to work. 
# For debugging, build using visual studio only!!!
devenv /build Debug ./AnonymousPipesExOne.sln

cd AnonymousPipesServerExOne

dotnet run --project ./AnonymousPipesServerExOne.csproj

clear
