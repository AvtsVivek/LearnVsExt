## Introducing Image Manifest Tools Extension

1. References 
   1. [image service tools](https://learn.microsoft.com/en-us/visualstudio/extensibility/internals/image-service-tools)
   2. Also [image service and catalog](https://learn.microsoft.com/en-us/visualstudio/extensibility/image-service-and-catalog)

2. Create a new VSIX package. Then add a new folder images. 

3. Then add a png to the images folder. Look at the earlier example(400725-ImageManifestToolsExten). [400725-ImageManifestToolsExten](https://github.com/AvtsVivek/LearnVsExt/tree/main/src/tasks/400705-AddingMonikerIcon)
   1. See the steps how to add a png file. See the following step.
   2. From the known monikers you can select one, and you can even export, it as png, jpg or gif to any location on the disc 

4. Include the added png file into the project. Then from the image properties, set include in VSIX to be true.

5. Ensure [Image Manifest Tools](https://marketplace.visualstudio.com/items?itemName=MadsKristensen.ImageManifestTools) is installed. Also see this [git hub link](https://github.com/madskristensen/MonikerManifestTools)

6. This should be part of [Extensibility Essentials pack](https://marketplace.visualstudio.com/items?itemName=MadsKristensen.ExtensibilityEssentials2022).

7. Right click and take a look at the properties of the save.png file. Irrespecitve of weather Include in VSIX is true or false, you can create a imagemanifest file. And the image manifest file will be identical irrespecitve of weather Include in VSIX is true or false.
   
   ![Save Png Properties](./images/50_50SavePngProperties.jpg)

8. If you want to switch Include in VSIX to true, then first make the build action to content. 

9. 

