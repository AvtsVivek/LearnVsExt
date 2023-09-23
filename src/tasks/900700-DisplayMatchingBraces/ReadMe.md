## Display Matching Baraces

https://learn.microsoft.com/en-us/visualstudio/extensibility/walkthrough-displaying-matching-braces

https://github.com/MicrosoftDocs/visualstudio-docs/blob/main/docs/extensibility/walkthrough-displaying-matching-braces.md

## How this project is built.

1. Create a VSix project.

2. Added necessary references, such as presentation core etc.
![Additions to Cs Proj file](images/50_50AdditionsToCsProjFile.jpg)

3. References, and adds an asset node of type MefComponent in the vsixmanifest, etc
![Additions to .vsixmanifest file](images/51_50AdditionsToVSixManifest.jpg)

4. The above two steps can be accomplished by adding a EditorClassifier new item and then deleting it.

## How to run.



