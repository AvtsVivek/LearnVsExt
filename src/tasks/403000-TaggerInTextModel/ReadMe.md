## Objective 

1. Introduces **ITag** and **ITagger**.

2. Also introduces **Span**, **Classifier** etc. 

3. Open a text file in Visual Studio. Then by invoking a command, can we get a count of any specific word(a tag, let that be **todo** for this case) be shown in a message box? Note we are not intrested in how this todo is displayed in the editor. Like we are not talking about adornments, highlighting etc. Thoese will be next steps. So are not going into Text View Subsystem. We will stick to Text Model Subsystem only. 

4. The text buffer belongs to the text model subsystem and not text view subsystem(hope I am not wrong here). This buffer can be manipulated, versioned etc, but cannot be displayed. A view consumes this buffer to show it in the editor.

5. So back to the question, how to get the word count of a given word(todo)? One way is to use the DTE to get to the document, read it and scan for the word in question.

6. But can we use the concept of tagging here?

## Notes
1. What is a **Span**. A [span is a continuous piece of text](https://learn.microsoft.com/en-us/visualstudio/extensibility/inside-the-editor#spans-and-normalizedspancollections).

## Build and Run

1. Open any text file which has some todo in it.

![Invoke Command](images/49_50ToolsShowToDoWordCountCommand.jpg)

2. The count we can see.

![Execute Command](images/50_50OpenTrialFileExecuteCommand.jpg)

3. We have TodoTagger.cs. In it, we have code to filter comment as follows. If we remove the following, then the number of todo will be 6 instead of 5. 

```cs
//if the classification is a comment
if (classificationSpan.ClassificationType.Classification.ToLower().Contains("comment"))
{
    ...
}
```

4. 



