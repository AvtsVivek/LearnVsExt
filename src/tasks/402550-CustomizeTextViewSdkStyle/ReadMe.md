# Customizing the text view

## Reference: 
1. https://learn.microsoft.com/en-us/visualstudio/extensibility/walkthrough-customizing-the-text-view

## How this project is created. 
1. Started from the sdk style project from 400510-VSixSdkProjectIntro.

2. Added necessary references, such as presentation core etc.

3. Then add the class **TestViewCreationListener**.

4. Build and run. 

## Notes

1. Just select a text, it should be light pink. 

![Selection of text](images/52_30Selection.jpg)

2. Once the exp instance is launched, open a text(TextFile1.txt for example) file. It has a text "asdfasdf".

![Open Text File](images/52_50OpeningATextFile.jpg)

3. Select that text and then click on the solution explorer. You will see that the selection gets pink. 

4. Now, click on the code window, ensure no text is selected, then Edit -> Advanced -> View White Space. Checkout for the keyboard shortcut Ctrl + R, Ctrl + W. This also works to toggle View White Space.

![View White Spaces](images/53_50EditAdvanced.jpg)

5. Now press some tabs. You should now see them as red arrows.

![Tabs Shown as red arrows](images/54_50TabsShownAsRedArrows.jpg)

6. So this is working with text file. Note the ContentType attribute with "text" as parameter. 

7. I tried with "code", "CSharp" etc. Nothing worked. Not sure why.  

```cs
[ContentType("code")]
```

8. Also note the left margin is light green. 

9. Not clear what the following is doing. I tried changing the color of the caret, but it did not seem to work. So leaving the following aside.

```cs
regularCaretProperties[EditorFormatDefinition.ForegroundBrushId] = Brushes.Magenta;
formatMap.SetProperties("Caret", regularCaretProperties);

overwriteCaretProperties[EditorFormatDefinition.ForegroundBrushId] = Brushes.Turquoise;
formatMap.SetProperties("Overwrite Caret", overwriteCaretProperties);
```
