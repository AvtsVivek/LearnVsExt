cd ../../..

cd src/tasks/500740-ManifestToCode

cd src/apps/500740-ManifestToCode

# Run the following command. 
ManifestFromResources /resources:C:/Trials/Ex/LearnVsExt/src/apps/500740-ManifestToCode/images/Save.png /assembly:ManifestToCodeAssembly /manifest:MyImageManifest.imagemanifest

# The above command should create a file by the name MyImageManifest.imagemanifest in the folder src/apps/500740-ManifestToCode

# Now run the following command. The difference from the above is the assembly. Earlier it was ManifestFromResourceAssembly. Now it is ResourceAssembly 
ManifestToCode /manifest:MyImageManifest.imagemanifest /language:CSharp /namespace:ManifestToCode /imageIdClass:MyImageIds /monikerClass:MyMonikers /classAccess:public

