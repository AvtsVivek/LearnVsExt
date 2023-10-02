using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqLanguageOptions
{

    internal class AppConstants
    {
        //Lanaguage Names
        public const string LinqLanguageName = "Linq";
        public const string LinqExt = ".linq";
        public const string LinqBaselanguageName = "CSharp";
        public const string LinqTmpExt = ".tmp";

        //ComboBox Text Color Message:
        public const string RunningSelectQueryMsgColor = "Running Select Query Method Message Color";
        public const string ResultsCodeTextColor = "Select LINQ Results Code Color";
        public const string QueryEqualsMsgColor = "Selected LINQ Query Results Equal Message Color.";
        public const string ResultColor = "Select LINQ Results Text Color";
        public const string ExceptionAdditionMsgColor = "Select LINQ Results Error Text Color";

        //ToolWindow Messages
        public const string AdvanceOptionText = "LINQ Language Editor Advanced Option Settings";
        public const string NoActiveDocument = "No Active Document View or LINQ Query Selection!\r\nPlease Select LINQ Query Statement or Method in Active Document,\r\nthen try again!";
        public const string RunningSelectQuery = "Running Selected LINQ Query.\r\nPlease Wait!";
        public const string CurrentSelectionQueryMethod = "Current Selection Query Method Results";
        public const string RunningSelectQueryMethod = "Running Selected LINQ Query Method.\r\n\r\nPlease Wait!";
        public const string ExceptionAdditionMessage = "Try Selecting the complete LINQ Query code line or the entire LINQ Query Method code block!";
        public const string RunSelectedLinqMethod = "Run Selected LINQ Query Statement or Method.";
        public const string CurrentLinqMethodSupport = "Selected LINQ Query is not supported yet!";
        public const string SelectResultVariableNotFound = "Result Variable for LINQ Query not found!";
        public const string CompilaitonFailure = "LINQ Query Compilation Failure!";
        public const string LinqQueryEquals = "Current Selected LINQ Query Results: =";

        //ToolWindow Names
        public const string LinqEditorToolWindowTitle = "LINQ Query Tool Window";
        public const string SolutionToolWindowsFolderName = "ToolWindows";
        public const string LinqAdvancedOptionPage = "Advanced";
        public const string LinqQueryTextHeader = "Selected LINQ Query:";
        public const string PaneGuid = "A938BB26-03F8-4861-B920-6792A7D4F07C";

        //Package Class
        public const string ProvideFileIcon = "KnownMonikers.RegistrationScript";
        public const string ProvideMenuResource = "Menus.ctmenu";

        //LINQ Templates
        public const string LinqStatementTemplate = "using System;\r\nusing System.Collections.Generic;\r\nusing System.Diagnostics;\r\nusing System.Linq;\r\nusing System.Text;\r\nusing System.Threading.Tasks;\r\nnamespace {namespace}\r\n{\r\n\tpublic class {itemname}\r\n\t{\r\n\t\tpublic static void {methodname}()\r\n\t\t{\r\n\t\t\t{$}\r\n\t\t}\r\n\t}\r\n}";
        public const string LinqMethodTemplate = "using System;\r\nusing System.Collections.Generic;\r\nusing System.Diagnostics;\r\nusing System.Linq;\r\nusing System.Text;\r\nusing System.Threading.Tasks;\r\nnamespace {namespace}\r\n{\r\n\tpublic class {itemname}\r\n\t{\r\n\t\t{$}\r\n\t}\r\n}";
        public const string VoidMain = "void Main()";

        //Default LINQ Result Variable Name:
        public const string LinqResultText = "result";
        public const string LinqResultMessageText = "Select LINQ Results Variable to Return";
        public const string LinqResultVarMessageText = "was not found!\r\nWould you like to Select a different LINQ Query return variable from the Selected LINQ Query?";

        //Where to Open Editor
        public const string LinqEditorOpenPreviewTabMessage = "Open Linq Query and result in Visual Studio Preview Tab";

        //Enable Toolwindow for results
        public const string LinqEditorResultsInToolWindow = "Enable Tool Window for Linq Query and results";

        //Default CheckBox Settings:
        public const bool EnableToolWindowResults = true;
        public const bool OpenInVSPreviewTab = true;

        //Default Colors:
        public const string LinqRunningSelectQueryMsgColor = "LightBlue";
        public const string LinqCodeResultsColor = "LightGreen";
        public const string LinqResultsEqualMsgColor = "LightBlue";
        public const string LinqResultsColor = "Yellow";
        public const string LinqExceptionAdditionMsgColor = "Red";

        //CScriptImports
        public const string SystemImport = "System";
        public const string SystemLinqImport = $"{SystemImport}.Linq";
        public const string SystemCollectionsImport = $"{SystemImport}.Collections";
        public const string SystemCollectionsGenericImports = $"{SystemCollectionsImport}.Generic";
        public const string SystemDiagnosticsImports = $"{SystemImport}.Diagnostics";

        //Options Settings:
        public const string OptionCategoryResults = "Results";
        public const string LinqAdvancedOptionPageGuid = "05F5CC22-0DF4-4D38-9B25-F54AAF567201";

        //LinqLanguageEditorProjectFileSettings
        public const string ProjectItemGroup = "ItemGroup";
        public const string ProjectCompile = "Compile";
        public const string ProjectInclude = "Include";
        public const string ProjectNone = "None";

        //MsgDialog Settings:
        public const string ResultVarChangeMsg = "The current default LINQ result variable does not exist in this selected LINQ Query!\r\n\r\nPlease select a LINQ result variable that exists in the selected LINQ Query from the list below.\r\n\r\nThen click the [OK] button to run the selected LINQ Query again.";
        public const string RadioButtonName = "radio";
    }
}
