cd ../../..

cd src/tasks/400735-ManifestFromResources

cd src/apps/400735-ManifestFromResources

# Run the following command. 
ManifestFromResources /resources:C:/Trials/Ex/LearnVsExt/src/apps/400735-ManifestFromResources/images/Save.png /assembly:ManifestFromResourceAssembly /manifest:MyImageManifest.imagemanifest
# Or you can use the following as well.
ManifestFromResources /resources:./images/Save.png /assembly:ManifestToCodeAssembly /manifest:MyImageManifest.imagemanifest

# The above command should create a file by the name MyImageManifest.imagemanifest in the folder src/apps/400735-ManifestFromResources

# Now run the following command. The difference from the above is the assembly. Earlier it was ManifestFromResourceAssembly. Now it is ResourceAssembly 
ManifestFromResources /resources:./images/Save.png /assembly:ResourceAssembly /manifest:MyImageManifest.imagemanifest

# Now observe the difference in the file MyImageManifest.imagemanifest.

# Now add one more file. 
# Run the command once more.

ManifestFromResources /assembly:ResourceAssembly /manifest:MyImageManifest.imagemanifest /resources:"C:/Trials/Ex/LearnVsExt/src/apps/400735-ManifestFromResources/images/Save.png;C:/Trials/Ex/LearnVsExt/src/apps/400735-ManifestFromResources/images/GitHub.png" 

ManifestFromResources /assembly:ResourceAssembly /manifest:MyImageManifest.imagemanifest /resources:C:/Trials/Ex/LearnVsExt/src/apps/400735-ManifestFromResources/images 

ManifestFromResources /assembly:ManifestFromResources /manifest:MyImageManifest.imagemanifest /resources:./images 

# Now observe the imagemanifest file. GitHub.png is added.

```xml
  <ItemGroup>
    <Content Include="images\GitHub.png" />
    <Content Include="images\Save.png" />
  </ItemGroup>
```