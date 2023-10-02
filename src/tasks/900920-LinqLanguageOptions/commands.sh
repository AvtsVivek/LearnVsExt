cd ../../..

cd src/tasks/900920-LinqLanguageOptions

cd src/apps/900920-LinqLanguageOptions

## I dont think we can build using the following dotnet command. 
## The project is not a dotnet core project. 

dotnet build --project ./LinqLanguageOptions.csproj

# For the following command to work, devenv must be added to the path environment variable. 
# First ensure you have visual studio is installed on your machine.
# On my macine, the devenv exe is available at the following location.
# C:\Program Files\Microsoft Visual Studio\2022\Professional\Common7\IDE
# Next add this to the path evnironment variable. 

# https://stackoverflow.com/a/76077904/1977871

# The following build commands does not seem to work. 
# For debugging, build using visual studio only!!!
devenv /build Debug ./LinqLanguageOptions.sln

# If you just want to start and run visual studi in experimental mode, run the following command.
devenv.exe /RootSuffix Exp ./LinqLanguageOptions.sln

pwd

Get-ChildItem

cd bin/debug

Get-ChildItem

# Now to install the extension, first ensure all the instances of Visual Studio are closed.
# Now simply run the following command to install the extension

./LinqLanguageOptions.vsix


# Once installed, open the logs. You will see something like. 
# YourUserName should be replaced with your user name. 
# The extension has been installed to C:\Users\YourUserName\AppData\Local\Microsoft\VisualStudio\17.0_c9ef2fd3\Extensions\fyp2abr3.n2t\


