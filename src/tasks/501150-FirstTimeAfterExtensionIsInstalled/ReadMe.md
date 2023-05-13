
## Rule Based UI Context.

1. This example starts from the earlier example ProvideAutoLoad. 
2. Scenerio: Potencially run some code to alert the user when first time Visual Studio is run after the extension is installed. So a user has installed your extension. And after the installation, the user runs Visual Studio, and now you want visual studio to pop up a message box and say Thanks to the user as heas choosen to install the extension and give it a try. And this message box should pop up only this time and not bother the user again later. So the second time, that message box should not pop up again after.

3. Reference: 
   1. https://www.youtube.com/watch?v=p328QcgZObs&t=760s
   2. https://learn.microsoft.com/en-us/visualstudio/extensibility/how-to-use-rule-based-ui-context-for-visual-studio-extensions?view=vs-2022

4. We will be using this: **UserSettingsStoreQuery:<query>**

5. 