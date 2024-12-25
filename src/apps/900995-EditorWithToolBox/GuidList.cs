using System;

namespace EditorWithToolBox
{
    /// <summary>
    /// This class contains a list of GUIDs specific to this sample, 
    /// especially the package GUID and the commands group GUID. 
    /// </summary>
    public static class GuidStrings
    {
        public const string GuidClientPackage = "bc185dbd-dc11-4f6a-a1f0-6e74e06c179f";
        public const string GuidClientCmdSet = "95a70d22-87af-43ec-b089-8a08ee24df80";
        public const string GuidEditorFactory = "5db6e65f-4ddb-4445-be63-2eef6a668025";
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
