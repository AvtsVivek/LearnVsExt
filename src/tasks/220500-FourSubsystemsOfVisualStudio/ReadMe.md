## Objective

1. This also introduces [subsystems inside Visual Studio Editor](https://learn.microsoft.com/en-us/visualstudio/extensibility/inside-the-editor#overview-of-the-subsystems).

## Introduction

1. There are 4 subsystems in Visual Studio editor. 

   1. [Text Model Subsystem.](https://learn.microsoft.com/en-us/visualstudio/extensibility/inside-the-editor#text-model-subsystem). The text model subsystem is responsible for representing text and enabling its manipulation. The text model subsystem contains the ITextBuffer interface, which describes the sequence of characters that is to be displayed by the editor.
   
   2. [Text View Subsystem](https://learn.microsoft.com/en-us/visualstudio/extensibility/inside-the-editor#text-view-subsystem). The text view subsystem is responsible for formatting and displaying text.

   3. [Classification Subsystem](https://learn.microsoft.com/en-us/visualstudio/extensibility/inside-the-editor#classification-subsystem). The classification subsystem is responsible for determining font properties for text. A classifier breaks the text into different classes, for example, **keyword** or **comment**.
   
   4. [Operations Subsystem](https://learn.microsoft.com/en-us/visualstudio/extensibility/inside-the-editor#operations-subsystem). The operations subsystem defines editor behavior. It provides the implementation for Visual Studio editor commands and the undo system.

## Reference.
1. https://stackoverflow.com/questions/76888423/

2. https://learn.microsoft.com/en-us/dotnet/api/microsoft.visualstudio.textmanager.interop.ivstextmanager

3. https://learn.microsoft.com/en-us/visualstudio/extensibility/inside-the-editor


