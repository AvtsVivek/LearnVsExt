cd ../../..

cd src/tasks/500740-ManifestToCode

cd src/apps/500740-ManifestToCode

# Run the following command. 
ManifestFromResources /resources:C:/Trials/Ex/LearnVsExt/src/apps/500740-ManifestToCode/images/Save.png /assembly:ManifestToCodeAssembly /manifest:MyImageManifest.imagemanifest

# The above command should create a file by the name MyImageManifest.imagemanifest in the folder src/apps/500740-ManifestToCode

# Now run the following command. The difference from the above is the assembly. Earlier it was ManifestFromResourceAssembly. Now it is ResourceAssembly 
ManifestToCode /manifest:MyImageManifest.imagemanifest /language:CSharp
ManifestFromResources /resources:C:/Trials/Ex/LearnVsExt/src/apps/500735-ManifestFromResources/images/Save.png /assembly:ResourceAssembly /manifest:MyImageManifest.imagemanifest

# Now observe the difference in the file MyImageManifest.imagemanifest.

# Now add one more file. 
# Run the command once more.

ManifestFromResources /assembly:ResourceAssembly /manifest:MyImageManifest.imagemanifest /resources:"C:/Trials/Ex/LearnVsExt/src/apps/500735-ManifestFromResources/images/Save.png;C:/Trials/Ex/LearnVsExt/src/apps/500735-ManifestFromResources/images/GitHub.png" 

# Now observe the imagemanifest file. GitHub.png is added.

```xml
  <ItemGroup>
    <Content Include="images\GitHub.png" />
    <Content Include="images\Save.png" />
  </ItemGroup>
```

ManifestToCode /manifest:D:\MyManifest.imagemanifest /language:CSharp