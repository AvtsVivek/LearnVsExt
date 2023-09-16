# Introduces SComponentModel and IComponentModel

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
   4. Using this service you get access to all the components that Visual Studio make available to component developers. 

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
