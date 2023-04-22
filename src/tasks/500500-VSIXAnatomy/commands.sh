cd ../../..

cd src/tasks/500500-VSIXAnatomy

cd src/apps/500500-VSIXAnatomy

## I dont think we can build using the following command. 
## The project is not a dotnet core project. 

dotnet build --project ./VSIXAnatomy.csproj

# For the following command to work, devenv must be added to the path environment variable. 
# First ensure you have visual studio is installed on your machine.
# On my macine, the devenv exe is available at the following location.
# C:\Program Files\Microsoft Visual Studio\2022\Professional\Common7\IDE
# Next add this to the path evnironment variable. 

# https://stackoverflow.com/a/76077904/1977871

devenv /build Debug ./VSIXAnatomy.sln



