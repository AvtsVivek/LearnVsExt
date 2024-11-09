## Objective

1. Introduces the [Text View Subsystem](https://learn.microsoft.com/en-us/visualstudio/extensibility/inside-the-editor#text-view-subsystem). 

2. The text ***view*** subsystem is responsible for formatting and displaying text.

3. This example helps in understanding **IWpfTextView** and **IVsTextView**. These represent the text view subsystem.

4. The [Text Model Subsystem](https://learn.microsoft.com/en-us/visualstudio/extensibility/inside-the-editor#text-model-subsystem) which is responsible for representing text and enabling its manipulation is the next step. 

5. Text View Subsystem, **IWpfTextView** and **IVsTextView** are mentioned in earlier examples, like 402600-ComponentModelIntro and 402500-VsTextManagerIntro

6. This example uses the IVsTextManager to get the active text view in the form of IVsTextView
```cs
var textManager = (IVsTextManager)Package.GetGlobalService(typeof(SVsTextManager));
var tempInt = textManager.GetActiveView(1, null, out IVsTextView activeView);
```

7. And for **IWpfTextView**, its a three step process. We have seen this in earlier example
   1. First get the **IComponentModel** object.
   2. From the **IComponentModel** get the **IVsEditorAdaptersFactoryService**
   3. From **IVsEditorAdaptersFactoryService** get the IWpfTextView.

8. There must be atleast one text file open in the Visual Studio Editor. Else the views will be null.

## Buid and Run.

1. There is nothing specific this application does.
2. Just invoke the command. Please a break point in the Execute method and observe the objects.

https://stackoverflow.com/questions/76888423/

https://learn.microsoft.com/en-us/dotnet/api/microsoft.visualstudio.textmanager.interop.ivstextmanager




