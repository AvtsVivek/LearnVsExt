cd ../../..

cd src/tasks/400740-ManifestToCode

cd src/apps/400740-ManifestToCode

# Run the following command. 
ManifestFromResources /resources:C:/Trials/Ex/LearnVsExt/src/apps/400740-ManifestToCode/images/Save.png /assembly:ManifestToCodeAssembly /manifest:MyImageManifest.imagemanifest
# Or you can use the following as well.
ManifestFromResources /resources:./images/Save.png /assembly:ManifestToCodeAssembly /manifest:MyImageManifest.imagemanifest
ManifestFromResources /resources:C:/Trials/Ex/LearnVsExt/src/apps/400740-ManifestToCode/images/ /assembly:C:/Trials/Ex/LearnVsExt/src/apps/400740-ManifestToCode/bin/Debug/ManifestToCode /manifest:MyImageManifest.imagemanifest

ManifestFromResources /assembly:ManifestToCode /manifest:MyImageManifest.imagemanifest /resources:./images 

# The above command should create a file by the name MyImageManifest.imagemanifest in the folder src/apps/400740-ManifestToCode

# Now run the following command. This will now generates the cs files.
ManifestToCode /manifest:./MyImageManifest.imagemanifest /language:CSharp /namespace:ManifestToCode /imageIdClass:MyImageIds /monikerClass:MyMonikers /classAccess:public

