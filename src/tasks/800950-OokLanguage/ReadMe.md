# Typing Speed Meter

## How this project is created. 
1. Create a VSix project.

2. Added reference System.ComponentModel.Composition.

3. Added necessary references, such as presentation core etc.
![Additions to Cs Proj file](images/50_50AdditionsToCsProjFile.jpg)

4. References, and adds an asset node of type MefComponent in the vsixmanifest, etc
![Additions to .vsixmanifest file](images/51_50AdditionsToVSixManifest.jpg)

5. The above two steps can be accomplished by adding a EditorClassifier new item and then deleting it.

6. Now add the other files from the above reference. 

7. Build and Run.

# Opening the file.
![Open the ook file](images/52_50OpeningOokFile.jpg)

1. WpfTextViewCreationListener is also available. This also works instead of VsTextViewCreationListener
2. 

# Notes
Run the sample

1. For steps to run, take a look at the Reference 1. 

2. To run the sample, hit F5 or choose the Debug > Start Debugging menu command. A new instance of Visual Studio will launch under the experimental hive.

3. Once loaded, open a file with the .ook filename extension. This sample includes an example .ook file: test.ook
   1. Instances of "Ook!" are colored Purple
   2. Instances of "Ook?" are colored Green
   3. Instances of "Ook." are colored Yellow

4. Test Completion: Click anywhere in the file and press Space. An auto-complete dialog should appear at the location of the text caret

5. Test QuickInfo: Position the mouse cursor over a valid token. Text appears in a grey box describing the purpose of the token.

# References
1. https://github.com/microsoft/VSSDK-Extensibility-Samples/tree/master/Ook_Language_Integration
2. 
3. 