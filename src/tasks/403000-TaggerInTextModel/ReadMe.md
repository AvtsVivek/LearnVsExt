## Objective 

1. Introduces **ITag** and **ITagger**. 

2. Open a text file in Visual Studio. Then by invoking a command, can we get a count of any specific word(a tag) shown as a message box? 

3. One way is to use the DTE to get to the document, read it and scan for the word in question.

4. But can we use the concept of tagging here?

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



