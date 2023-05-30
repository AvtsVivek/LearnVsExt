cd ../../..

cd src/tasks/500735-ManifestFromResources

cd src/apps/500735-ManifestFromResources

# Run the following command. 
ManifestFromResources /resources:C:/Trials/Ex/LearnVsExt/src/apps/500735-ManifestFromResources/images/Save.png /assembly:ManifestFromResourceAssembly /manifest:MyImageManifest.imagemanifest

# The above command should create a file by the name MyImageManifest.imagemanifest in the folder src/apps/500735-ManifestFromResources

# Now run the following command. The difference from the above is the assembly. Earlier it was ManifestFromResourceAssembly. Now it is ResourceAssembly 
ManifestFromResources /resources:C:/Trials/Ex/LearnVsExt/src/apps/500735-ManifestFromResources/images/Save.png /assembly:ResourceAssembly /manifest:MyImageManifest.imagemanifest

# Now observe the difference in the file MyImageManifest.imagemanifest. 

