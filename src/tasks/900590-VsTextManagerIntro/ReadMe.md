## Introduces IVsTextManager and SVsTextManager

1. You can get IVsTextView object from TextManager.
2. Then you can get the active view from the text manager.

```cs
vsTextManager.GetActiveView(mustHaveFocus, null, out IVsTextView vsTextView);
```

4. Then there is IVsTextLines currentDocTextLines. This has lot of information about the text in the file.

```cs
vsTextView.GetBuffer(out IVsTextLines currentDocTextLines);
```

5. The you can cast to IVsTextBuffer as well.

```cs
var vsTextBuffer = currentDocTextLines as IVsTextBuffer;
```

6. You can get a lot of data in the file from IVsTextBuffer object.

7. You can get the full file path as well.

```cs
var persistFileFormat = vsTextBuffer as IPersistFileFormat;

persistFileFormat.GetCurFile(out string filePath, out uint pnFormatIndex);
```

8. Also you can also get IWpfTextViewHost and IWpfTextView objects as well. There are other methods to get these objects as well.  

## Reference.
1. https://stackoverflow.com/questions/76888423/
2. https://learn.microsoft.com/en-us/dotnet/api/microsoft.visualstudio.textmanager.interop.ivstextmanager


