# Options Sample.

## Reference: 
1. https://github.com/microsoft/VSSDK-Extensibility-Samples/tree/master/Options

## How this project is created. 
1. Create a VSix project.

2. Added reference System.ComponentModel.Composition.

3. Added necessary references, such as presentation core etc.
![Additions to Cs Proj file](images/50_50AdditionsToCsProjFile.jpg)

4. References, and adds an asset node of type MefComponent in the vsixmanifest, etc
![Additions to .vsixmanifest file](images/51_50AdditionsToVSixManifest.jpg)

5. The above two steps can be accomplished by adding a EditorClassifier new item and then deleting it.

6. Now add the following classes.
![Adding Classes](images/52_40AddingClasses.jpg)

7. My Options General

![My Options General](images/52_50MyOptionsGeneral.jpg)

8. My Options Other

![My Options General](images/53_50MyOptionsOther.jpg)

