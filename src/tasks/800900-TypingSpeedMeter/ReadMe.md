# Typing Speed Meter

## Reference: 
1. https://github.com/microsoft/VSSDK-Extensibility-Samples/tree/master/Typing_Speed_Meter

## How this project is created. 
1. Create a VSix project.

2. Added reference System.ComponentModel.Composition.

3. Added necessary references, such as presentation core etc.
![Additions to Cs Proj file](images/50_50AdditionsToCsProjFile.jpg)

4. References, and adds an asset node of type MefComponent in the vsixmanifest, etc
![Additions to .vsixmanifest file](images/51_50AdditionsToVSixManifest.jpg)

5. The above two steps can be accomplished by adding a EditorClassifier new item and then deleting it.

6. Add the UserControl, TypingSpeedMeter.TypingSpeedControl

7. Add the png images, Adornment_large.png and Adornment_small.png

8. Add the above images to the source.extension.vsixmanifest file.

![Additions to VSix Manifest](images/52_50VSixMainfestFile.jpg)

9. Add classes TypingSpeedMeter, VsTextViewListener, TypeCharFilter, AdornmentFactory.

10. Build and Run the app. Experimental instance runs. Open any text file, then type somehting at full speed.

![Open Text file](images/53_50TypeingSpeedOpenFile.jpg)

11.      







# Notes
1. I am not sure this is working. Need to check again. The highlighting seems to be working the same. 
