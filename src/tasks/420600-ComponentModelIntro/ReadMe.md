## Objective 

1. Introduces **SComponentModel** and **IComponentModel**. 
2. Also **IVsEditorAdaptersFactoryService**
3. While an earlier example **402500-VsTextManagerIntro** introduces Text Manager, this introduces IComponentModel.

## What is a component.

1. The following is a general definition of a component in dotnet. This is not directly related to Visual Studio SComponentModel and IComponentModel. This is given here, just to clear off any confustion that may arise.

2. The below is [taken from here](https://learn.microsoft.com/en-us/dotnet/visual-basic/developing-apps/creating-and-using-components)

   1. A component is a class that implements the System.ComponentModel.IComponent interface or that derives directly or indirectly from a class that implements IComponent. A .NET component is an object that is reusable, can interact with other objects, and provides control over external resources and design-time support.

   2. An important feature of components is that they are designable, which means that a class that is a component can be used in the Visual Studio Integrated Development Environment. A component can be added to the Toolbox, dragged and dropped onto a form, and manipulated on a design surface. Base design-time support for components is built into .NET. A component developer does not have to do any additional work to take advantage of the base design-time functionality.

   3. A control is similar to a component, as both are designable. However, a control provides a user interface, while a component does not. A control must derive from one of the base control classes: Control or Control.

3. Now that we know something about dotnet Component, lets discuss about IComponentModel and SComponentModel
   
   1. First we need to know about MEF. Its a Dependency Injection(DI) framework built by Microsoft to build loosly coupled, extensible application. Specifically Visual Studio is a looly coupled, extensible application built using MEF.  

   2. The **IComponentModel** interface present in **Microsoft.VisualStudio.ComponentModelHost** namespace wraps MEF DI container. 

   3. In any extension, we can get the IComponentModel object as follows. Here Package is Microsoft.VisualStudio.Shell.Package
   ```cs
   (IComponentModel)Package.GetGlobalService(typeof(SComponentModel));
   ```
4. Using this **IComponentModel** object you get access to all the components that Visual Studio make available to component developers. Example, **IVsEditorAdaptersFactoryService**
   
5. **IVsEditorAdaptersFactoryService** is an important interface. 
6. Now from **IVsEditorAdaptersFactoryService** we can get other services.

```cs
// Now we can use the adapter service to get the Wpf Text View. vsEditorAdaptersFactoryService
var wpfTextView = vsEditorAdaptersFactoryService.GetWpfTextView(vsTextView);

var wpfTextViewHost = vsEditorAdaptersFactoryService.GetWpfTextViewHost(vsTextView);   
```

7. We can even create text buffer like so. 
```cs
IVsTextBuffer vsTextBufferOne = vsEditorAdaptersFactoryService.CreateVsTextBufferAdapter(this.package);
```

8. But note, it will not represent any data from any currently opened documents, because its created from scractch.

9. Also note, if there is a VsTextView representing an active view, then we can get text model(the buffer) as follows.

```cs
vsTextView.GetBuffer(out IVsTextLines currentDocTextLines); //Getting Current Text Lines 
var vsTextBufferThree = currentDocTextLines as IVsTextBuffer;
ITextBuffer documentTextBufferThree = vsEditorAdaptersFactoryService.GetDocumentBuffer(vsTextBufferThree);
```

10. And also if we have a IWpfTextView object, we can get the ITextBuffer as follows. Note this pirticular point is not covered in this example. This will be covered in subsequent examples. 
```cs
ITextBuffer textBufferFromWpfView = wpfTextView.TextBuffer;
```
11. Note its still not clear what exactly is the difference between **ITextBuffer** and **IVsTextBuffer**.



# Build and Run.

1. Build the solution.

2. Run the project by pressing F5. A second instance of Experimental Visual Studio starts.

3. Open any simple text file. And then execute the command(Tools -> Invoke TestCommand). You can see that **IComponentModel** object is created.

4. Then **IVsEditorAdaptersFactoryService**. Then **IWpfTextView** and **IWpfTextViewHost** objects are also created.

5. You can see them, in the message boxes.

## References
1. Components can be visual (controls) and non-visuals.
2. https://learn.microsoft.com/en-us/dotnet/api/system.componentmodel.icomponent
3. https://learn.microsoft.com/en-us/dotnet/api/system.componentmodel.component
4. https://stackoverflow.com/a/4667263/1977871
5. https://learn.microsoft.com/en-us/dotnet/api/microsoft.visualstudio.texttemplating.vshost.mef.vsshellcomponentmodelhost
6. https://learn.microsoft.com/en-us/dotnet/api/microsoft.visualstudio.componentmodelhost
7. https://learn.microsoft.com/en-us/dotnet/framework/mef/
8. https://learn.microsoft.com/en-us/dotnet/framework/mef/attributed-programming-model-overview-mef
9. https://learn.microsoft.com/en-us/visualstudio/extensibility/managed-extensibility-framework-in-the-editor
