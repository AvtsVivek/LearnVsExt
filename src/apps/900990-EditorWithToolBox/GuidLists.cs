using System;

namespace EditorWithToolBox
{
    /// <summary>
    /// This class contains a list of GUIDs specific to this sample, 
    /// especially the package GUID and the commands group GUID. 
    /// </summary>
    public static class GuidStrings
    {
        public const string GuidClientPackage   = "6d688609-4f87-4b6c-9ea5-7ca739dae8de";
        public const string GuidClientCmdSet    = "4cb5bd71-ddfe-49ba-92bc-dd5ecc9efe57";
        public const string GuidEditorFactory   = "c99ac59f-4bbb-4ee0-889a-047cc0652f73";
    }
    /// <summary>
    /// List of the GUID objects.
    /// </summary>
    internal static class GuidList
    {
        public static readonly Guid guidEditorCmdSet = new Guid(GuidStrings.GuidClientCmdSet);
        public static readonly Guid guidEditorFactory = new Guid(GuidStrings.GuidEditorFactory);
    };
}
