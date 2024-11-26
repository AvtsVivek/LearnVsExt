## Objective 

1. Introduces **IContentTypeRegistryService** and **ContentType**.


## Notes
1. Build and Run. The following list of ContentTypes that are registered for the visual studio editor. You can see that as a message box.

```txt
A total of 95 content Types are found.
Here they follow
-------------------------------------------
any, Basic, Basic Signature Help, 
C#_LSP, C/C++, C/C++ Signature Help, ClangFormat, 
CMake, CMakePresets, CMakeSettings, code, 
code++, code-languageserver-base, code-languageserver-preview, code-languageserver-textmate-brace, 
code-languageserver-textmate-color, code-languageserver-textmate-indentation, code-languageserver-textmate-structure, code-textmate-commentselection, 
code-textmate-ontypeindentation, Command, ConsoleOutput, CppProperties, 
CSharp, CSharp Signature Help, css, css.extensions, 
cssLSPClient, cssLSPServer, EmbeddedCodeContentType, EmbeddedInlinePromptContentType, 
EmbeddedPromptContentType, EmbeddedWindowedPromptContentType, F#, F# Signature Help, 
FindResults, FSharpInteractive, GaiaExampleContentType, handlebars, 
HLSL, htc, HTML, html-delegation, 
htmlLSPClient, htmlLSPServer, HTMLProjection, Immediate, 
inert, intellisense, Interactive Command, Interactive Content, 
Interactive Output, InteractiveMarkdown, JavaScript, JSON, 
languageserver-base, languageserver-base Signature Help, LegacyRazor, LegacyRazorCoreCSharp, 
LegacyRazorCSharp, LegacyRazorVisualBasic, LESS, Memory, 
mustache, Output, plaintext, projection, 
quickinfo, Register, Rest, Roslyn Languages, 
RoslynPreviewContentType, SCSS, sighelp, sighelp-doc, 
snippet picker, Specialized CSharp and VB Interactive Command, srf, text, 
T-SQL90, TypeScript, TypeScript Signature Help, TypeScript.Pug, 
underscore, UnitTestSummary, UNKNOWN, VB_LSP, 
vbscript, vs-markdown, WebForms, WebFormsProjection, 
wsh, XAML, XML, yaml
```


## References
1. https://learn.microsoft.com/en-us/dotnet/api/microsoft.visualstudio.utilities.icontenttyperegistryservice

2. https://learn.microsoft.com/en-us/visualstudio/extensibility/language-service-and-editor-extension-points#extend-content-types

## Need to look into 
1. https://learn.microsoft.com/en-us/dotnet/api/microsoft.visualstudio.shell.providelanguageextensionattribute
2. https://learn.microsoft.com/en-us/dotnet/api/microsoft.visualstudio.utilities.fileextensiontocontenttypedefinition
3. https://learn.microsoft.com/en-us/dotnet/api/microsoft.visualstudio.utilities.ifileextensionregistryservice

