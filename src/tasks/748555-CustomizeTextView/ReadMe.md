# Customizing the text view

## Reference: 
1. https://learn.microsoft.com/en-us/visualstudio/extensibility/walkthrough-customizing-the-text-view

## How this project is created. 
1. Create a VSix project.

2. Add Editor Classifier new item. Then delete the files. This adds some necessary references to the project.

![Added Necessary references](images/51_50AdditionsToCsProjFile.jpg)

![Manifest Changes](images/50_50AdditionsToVSixManifest.jpg)

3. Then add the class **TestViewCreationListener**.

4. Build and run. 

## Notes

1. Once the exp instance is launched, open a text(TextFile1.txt for example) file. It has a text "asdfasdf".

![Open Text File](images/52_50OpeningATextFile.jpg)

2. Select that text and then click on the solution explorer. You will see that the selection gets pink. 

3. Now, click on the code window, ensure no text is selected, then Edit -> Advanced -> View White Space.

![View White Spaces](images/53_50EditAdvanced.jpg)

4. Now press some tabs. You should now see them as red arrows.

![Tabs Shown as red arrows](images/54_50TabsShownAsRedArrows.jpg)

5. So this is working with text file. Note the ContentType attribute with "text" as parameter. 

6. I tried with "code", "CSharp" etc. Nothing worked. Not sure why.  

```cs
[ContentType("code")]
```

7. 