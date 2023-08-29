

1. This example is similar to the 400650-AddingSimpleCommand. 

2. The project is created exactly same way.

3. This is based on the article [**How to: Use the activity log**](https://learn.microsoft.com/en-us/visualstudio/extensibility/how-to-use-the-activity-log)

4. We will try to write an entry to the activity log.

5. Add the following code in the InitializeAsync method. This simply gets the log service and then adds information level logs.

```cs
var log = await GetServiceAsync(typeof(SVsActivityLog)) as IVsActivityLog;

if (log == null) return;

int hr = log.LogEntry((UInt32)__ACTIVITYLOG_ENTRYTYPE.ALE_INFORMATION,
    this.ToString(),
    string.Format(CultureInfo.CurrentCulture,
    "Called for: {0}", this.ToString()));

hr = log.LogEntry((UInt32)__ACTIVITYLOG_ENTRYTYPE.ALE_INFORMATION,
    this.ToString(),
    string.Format(CultureInfo.CurrentCulture,
    "Test info. asdfasdf", this.ToString()));  
```

6. Now run commands something like the following(see the commands.sh file). Here we are launching the exp instance in which the extensioni is installed, and then we are also enabling logging as well. So when we invoke our extension, it logs. And you can see that logs in the file provided.

```cmd
devenv.exe /RootSuffix Exp /log C:\Temp\MyVSLog.xml ./UseSVsActivityLogService.sln
```

7. Now check for the logs in the file C:\Temp\MyVSLog.xml.

8. Some references.
   1. https://learn.microsoft.com/en-us/visualstudio/ide/reference/devenv-command-line-switches
   2. https://learn.microsoft.com/en-us/visualstudio/ide/reference/log-devenv-exe
   3. https://learn.microsoft.com/en-us/visualstudio/extensibility/using-and-providing-services
   4. https://learn.microsoft.com/en-us/visualstudio/extensibility/internals/service-essentials
