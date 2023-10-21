# DTE Events, Build Events

## Objective
1. This example introduces DTE Events.

2. Specifically introduces CommandEvents.


## References
1. https://learn.microsoft.com/en-us/dotnet/api/envdte.events

2. https://learn.microsoft.com/en-us/dotnet/api/envdte.commandevents

3. Earlier example, 501125-ProvideAutoLoad

## How this project is built.
1. Start from the regular VSix project.

2. Then add the following in the initialize method of the package. We get the DTE and then events from it.

```cs
var _pasteEvent = DteTwoInstance.Events.CommandEvents[pasteGuid, pasteID];
_pasteEvent.BeforeExecute += CopyRead;
```

3. Somehow this is not working now.

## Build and Run
1. Just build, launch the exp instance. 

2. Open any file, select text and try copy. Its not working.

