
1. Shows how to Assign key board shortcut to a command.

2. Builds on 500650-AddingSimpleCommand. 

3. Create a project in similar steps.

4. Now add the following KeyBindings xml to the vsct file.

```xml
<KeyBindings>
    <KeyBinding guid="guidAssignKeyboardShortcutPackageCmdSet" id="TestCommandId" editor="guidVSStd97" key1="S" mod1="Alt"></KeyBinding>
</KeyBindings>
```

6. Not sure what editor attribute **guidVSStd97** means.

5. Reference.
   1. https://learn.microsoft.com/en-us/visualstudio/extensibility/keybinding-element#attributes