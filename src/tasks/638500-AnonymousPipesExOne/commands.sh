cd ../../..

cd src/tasks/638500-AnonymousPipesExOne

cd src/apps/638500-AnonymousPipesExOne


# The following build commands does not seem to work. 
# For debugging, build using visual studio only!!!
devenv /build Debug ./AnonymousPipesExOne.sln

cd AnonymousPipesServerExOne

dotnet run --project ./AnonymousPipesServerExOne.csproj

clear
