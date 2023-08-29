cd ../../..

cd src/tasks/400665-UseSVsActivityLogService

cd src/apps/400665-UseSVsActivityLogService

## I dont think we can build using the following dotnet command. 
## The project is not a dotnet core project. 

dotnet build --project ./UseSVsActivityLogService.csproj

# For the following command to work, devenv must be added to the path environment variable. 
# First ensure you have visual studio is installed on your machine.
# On my macine, the devenv exe is available at the following location.
# C:\Program Files\Microsoft Visual Studio\2022\Professional\Common7\IDE
# Next add this to the path evnironment variable. 

# https://stackoverflow.com/a/76077904/1977871

devenv /build Debug ./UseSVsActivityLogService.sln
# If you just want to start and run visual studi in experimental mode, run the following command.
devenv.exe /RootSuffix Exp ./UseSVsActivityLogService.sln 

# This example is about logging. If you want to see the logs run the following.
devenv.exe /log C:\Temp\MyVSLog.xml

# The problem with the above command is it will output logs. But we want to test our logging code.
# For that we have to install the out extension in an experimental instance along with logs.
# So run the following command. Here we are using log switch.
# Also note this starts visual studio in experimental mode and without any solution loaded.
devenv.exe /RootSuffix Exp /log C:\Temp\MyVSLog.xml 

# If you want any solution to be loaded, use the following.
devenv.exe /RootSuffix Exp /log C:\Temp\MyVSLog.xml ./UseSVsActivityLogService.sln


pwd

Get-ChildItem

cd bin/debug

Get-ChildItem

# Now to install the extension, first ensure all the instances of Visual Studio are closed.
# Now simply run the following command to install the extension

./WriteVsStoreConfigSettings.vsix


# Once installed, open the logs. You will see something like. 
# YourUserName should be replaced with your user name. 
# The extension has been installed to C:\Users\YourUserName\AppData\Local\Microsoft\VisualStudio\17.0_c9ef2fd3\Extensions\fyp2abr3.n2t\



