## Link a content type to a file name extension

## Reference: 
1. https://learn.microsoft.com/en-us/visualstudio/extensibility/walkthrough-linking-a-content-type-to-a-file-name-extension

2. https://learn.microsoft.com/en-us/visualstudio/extensibility/walkthrough-creating-a-margin-glyph

## Steps:
1. Follow the above link. 

2. After creating the VSix project, Add Editor classifier project.

![Add Editor Classifier file](./images/50_50EditorClassifierAddNewItem.jpg)

3. Once you added Editor Classifier project item, you need to keep one file, that is EditorClassifier(IClassifier), and delete rest of the newly added files

![Delete other files](./images/50_51EditorClassifierDeleteOtherFiles.jpg)

4. I think, you can delete all the classes. Even the classifier. Not sure about this. 

4. Follow the subsequent steps in the reference.

5. Build and Test.
   1. Build the solution.

   2. Run the project by pressing F5. A second instance of Visual Studio starts.
   
   3. Make sure that the indicator margin is showing. (On the Tools menu, click Options. On the Text Editor page, make sure that Indicator margin is selected.)

   4. Open a code file that has comments. Add the word "todo" to one of the comment sections. You can try a c sharp or vb.net. Make file such as SampleCShart.cs and SampleVbDotNet.vb and put code in them as follows.
   ```vb
   ' Here we go...
   ' todo
   Class Box
      Public length As Double   ' Length of a box
      Public breadth As Double  ' Breadth of a box
      Public height As Double   ' Height of a box
   End Class
   ```

   ```cs
   
   // tod todo
   // todo

   public class TempClass
   { 
   }
   ```

   5. A yellow circle with a Red outline appears in the indicator margin to the left of the code window.

6. What is a classifier?
   1. https://learn.microsoft.com/en-us/visualstudio/extensibility/language-service-and-editor-extension-points#extend-classification-types-and-classification-formats
7. So what is a Tagger? 
8. A Tagger needs a classifier