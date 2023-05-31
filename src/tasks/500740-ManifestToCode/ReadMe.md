## Introducing Image Manifest Tools Extension

1. References 
   1. [image service tools](https://learn.microsoft.com/en-us/visualstudio/extensibility/internals/image-service-tools)
   2. Also [image service and catalog](https://learn.microsoft.com/en-us/visualstudio/extensibility/image-service-and-catalog)
   3. [Manifest from Resources](https://learn.microsoft.com/en-us/visualstudio/extensibility/internals/manifest-from-resources)

2. Follow the steps from earlier example. Look at the earlier example [500735-ManifestFromResources](https://github.com/AvtsVivek/LearnVsExt/tree/main/src/tasks/500735-ManifestFromResources)

6. Run the following command to create a manifest file. 

```cmd
ManifestFromResources /resources:C:/Trials/Ex/LearnVsExt/src/apps/500740-ManifestToCode/images/Save.png /assembly:ManifestToCodeAssembly /manifest:MyImageManifest.imagemanifest
```

7. The above command should create a file by the name MyImageManifest.imagemanifest in the folder src/apps/500735-ManifestFromResources

