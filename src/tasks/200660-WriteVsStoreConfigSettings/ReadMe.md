
# Demos how to read settings from settings store 

1. This example is similar to the 400650-AddingSimpleCommand. Expands from 400655-ReadVsStoreConfigSettings

2. The project is created exactly same way.

3. This is based on the article [**Writing to the User Settings Store**](https://learn.microsoft.com/en-us/visualstudio/extensibility/writing-to-the-user-settings-store)

4. Visual Studio Setting store is where Visual Studio, during and after installation, stores many of its settings. There is an excellent tool to visualize this. This tool comes in the form of [an extension and can be installed from here](https://marketplace.visualstudio.com/items?itemName=PaulHarrington.SettingsStoreExplorerPreview). For this exercise, **do install this extension**. This is a requirement.

![Settings Store Explorer](./images/50_50_SettingsStoreExplorer.jpg)

5. Build and Run the project. Look at Tools menu.

![Tools Menu](./images/51_50_ToolsMenu.jpg)

6. External Tools

![External Tools](./images/52_50_ExternalTools.jpg)

7. Also in the exp instance(not the regular intance of Visual Studio), observe that the Settings Store Explorer is disabled.

![Setting Store Explorer](./images/52_53_SettingStoreExplorerDisabled.jpg)

8. Enable this extension and close the exp instance. Then start the debugging again. Press F5.

9. Now ensure the extension is enabled.

![Setting Store Explorer After](./images/52_54_SettingStoreExplorerDisabledAfter.jpg)

10. Now open the Setting Store Explorer in the Exp instance of Visual Studio. View -> Other Windows -> Settings Store Explorer. Now look for `External Tools` in Users(see below)

![Setting Store Explorer Opened](./images/52_51_SettingStoreExplorer.jpg)

11. Now click the new command Tools -> Invoke WriteToUserSettingsCOmmand.  

![Tools Menu After](./images/53_50_ToolsMenuAfter.jpg)

12. You should see a message saying Installing Notepad. Now observe again.

13. External Tools After

![External Tools After](./images/54_50_ExternalToolsAfter.jpg)

14. Notice the Settings Store Explorer as well. Invoke Refresh if needed.

![External Tools After](./images/52_52_SettingStoreExplorerAfterRefresh.jpg)

15. The key here is the [ShellSettingsManager](https://learn.microsoft.com/en-us/dotnet/api/microsoft.visualstudio.shell.settings.shellsettingsmanager) class. This is the gateway class to reach for the settings stored inside the Visual Studio 

16. If you want to reset the experimental instance, do the following.

![Reset Exp Vs](./../400500-VSixBlankProjectAnalysis/images/57_50_ResetVsExpIntance.jpg)

