## Introducing Image Manifest Tools Extension

1. References 
   1. [image service tools](https://learn.microsoft.com/en-us/visualstudio/extensibility/internals/image-service-tools)
   2. Also [image service and catalog](https://learn.microsoft.com/en-us/visualstudio/extensibility/image-service-and-catalog)
   3. [Manifest from Resources](https://learn.microsoft.com/en-us/visualstudio/extensibility/internals/manifest-from-resources)

2. Follow the steps from earlier example. Look at the earlier example [400735-ManifestFromResources](https://github.com/AvtsVivek/LearnVsExt/tree/main/src/tasks/400735-ManifestFromResources)

3. Ensure you are in the folder src/apps/400740-ManifestToCode. Run the following command to create a manifest file. 

```cmd
ManifestFromResources /resources:C:/Trials/Ex/LearnVsExt/src/apps/400740-ManifestToCode/images/Save.png /assembly:ManifestToCodeAssembly /manifest:MyImageManifest.imagemanifest
```

4. The above command should create a file by the name MyImageManifest.imagemanifest in the folder src/apps/400740-ManifestToCode

5. Next run the following command to generate cs files from the manifest file.

```cmd
ManifestToCode /manifest:MyImageManifest.imagemanifest /language:CSharp /namespace:ManifestToCode /imageIdClass:MyImageIds /monikerClass:MyMonikers /classAccess:public
```

6. The above command generats the cs files. Next include files in the project. Build the project and ensure that it builds.
