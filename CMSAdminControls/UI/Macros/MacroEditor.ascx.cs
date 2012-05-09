using System;
using System.Web.UI;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Collections;
using System.Web.UI.WebControls;

using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.CMSHelper;
using CMS.PortalEngine;
using CMS.TreeEngine;
using CMS.SettingsProvider;
using CMS.ExtendedControls;

public partial class CMSAdminControls_UI_Macros_MacroEditor : CMSUserControl, ICallbackEventHandler
{
    #region "Variables"

    /// <summary>
    /// List of macro namespaces for which the methods are permanently available (visible in AutoCompletion without any context).
    /// </summary>
    protected List<string> mNamespaceUsings = null;

    private static MacroResolver mResolver = null;
    private static List<string> mMethodList = null;
    private List<string> mUsingList = null;
    private bool mUseAutoComplete = true;
    private bool mShowAutoCompletionAbove = false;
    private bool mMixedMode = true;
    private int mTopOffset = 0;
    private int mLeftOffset = 0;
    private string mEditorFocusLostScript = null;

    #endregion


    #region "Properties"

    /// <summary>
    /// Gets the list of all available special commands.
    /// </summary>
    private static List<string> CommandsList
    {
        get
        {
            if (mMethodList == null)
            {
                mMethodList = new List<string>();

                // Add flow control commands
                mMethodList.Add("for\nfor (i = 0; i < 10; i++) { | }");
                mMethodList.Add("foreach\nforeach (x in collection) { | }");
                mMethodList.Add("if\nif (|) {  }");
                mMethodList.Add("while\nwhile( | ) {  }");
                mMethodList.Add("break\nbreak;");
                mMethodList.Add("continue\ncontinue;");
                mMethodList.Add("return\nreturn;");
                mMethodList.Add("print\nprint(|);");
                mMethodList.Add("println\nprintln(|);");
                mMethodList.Add("null\nnull");
            }
            return mMethodList;
        }
    }


    /// <summary>
    /// Gets the list of all available special commands.
    /// </summary>
    private List<string> UsingList
    {
        get
        {
            if (mUsingList == null)
            {
                mUsingList = new List<string>();

                if (this.NamespaceUsings != null)
                {
                    foreach (string name in this.NamespaceUsings)
                    {
                        object namespaceInstance = Resolver.GetNamespaceInstance(name);
                        if (namespaceInstance != null)
                        {
                            List<MacroMethod> methods = MacroMethods.GetMethodsForObject(namespaceInstance, false, true);
                            FillFromMethodList(mUsingList, methods);
                        }
                    }
                }
            }
            return mUsingList;
        }
    }


    /// <summary>
    /// Gets or sets the list of macro namespaces for which the methods are permanently available (visible in AutoCompletion without any context).
    /// </summary>
    public List<string> NamespaceUsings
    {
        get
        {
            return mNamespaceUsings;
        }
        set
        {
            mNamespaceUsings = value;
        }
    }


    /// <summary>
    /// Gets or sets the list of macro namespaces which are just method containers (Math, Countries, ...).
    /// </summary>
    private List<string> NamespacesInUse
    {
        get
        {
            return new List<string>() { "Math", "Transformation", "Countries" };
        }
    }


    /// <summary>
    /// Gets or sets context resolver used for getting autocomplete options.
    /// </summary>
    public MacroResolver Resolver
    {
        get
        {
            if (mResolver == null)
            {
                mResolver = CMSContext.CurrentResolver.CreateContextChild();
            }
            return mResolver;
        }
        set
        {
            mResolver = value;
        }
    }


    /// <summary>
    /// Indicates whether the autocompletion feature is enabled or not.
    /// </summary>
    public bool UseAutoComplete
    {
        get
        {
            return this.mUseAutoComplete && Editor.SyntaxHighlightingEnabled;
        }
        set
        {
            this.mUseAutoComplete = value;
        }
    }


    /// <summary>
    /// Gets or sets the code which is executed when editor lost focus.
    /// </summary>
    public string EditorFocusLostScript
    {
        get
        {
            return this.mEditorFocusLostScript;
        }
        set
        {
            this.mEditorFocusLostScript = value;
        }
    }


    /// <summary>
    /// Indicates whether the editor is in pure macro editing mode (whole text is considered as macro) or mixed mode, where auto completion is fired only inside {%%} environment.
    /// </summary>
    public bool MixedMode
    {
        get
        {
            return this.mMixedMode;
        }
        set
        {
            this.mMixedMode = value;
        }
    }


    /// <summary>
    /// Gets or sets the left offset of the autocomplete control (to position it correctly).
    /// </summary>
    public int LeftOffset
    {
        get
        {
            return mLeftOffset;
        }
        set
        {
            mLeftOffset = value;
        }
    }


    /// <summary>
    /// Gets or sets the top offset of the autocomplete control (to position it correctly).
    /// </summary>
    public int TopOffset
    {
        get
        {
            return mTopOffset;
        }
        set
        {
            mTopOffset = value;
        }
    }


    /// <summary>
    /// If true, tree is shown above the editor, otherwise it is below (default position is below).
    /// </summary>
    public bool ShowAutoCompletionAbove
    {
        get
        {
            return mShowAutoCompletionAbove;
        }
        set
        {
            mShowAutoCompletionAbove = value;
        }
    }


    /// <summary>
    /// Gets the name of java script object of the auto completion extender.
    /// </summary>
    public string AutoCompletionObject
    {
        get
        {
            return "autoCompleteObj_" + this.ClientID;
        }
    }

    #endregion


    #region "Mirrored editor properties"

    /// <summary>
    /// Gets the editor control.
    /// </summary>
    public ExtendedTextArea Editor
    {
        get
        {
            return this.txtCode;
        }
    }


    /// <summary>
    /// Gets if syntax highlighting is enabled for this control.
    /// </summary>
    public bool SyntaxHighlightingEnabled
    {
        get
        {
            return Editor.SyntaxHighlightingEnabled;
        }
    }


    /// <summary>
    /// Gets or sets whether the control is read only
    /// </summary>
    public bool ReadOnly
    {
        get
        {
            return Editor.ReadOnly;
        }
        set
        {
            Editor.ReadOnly = value;
        }
    }


    /// <summary>
    /// Gets or sets the width of the editor
    /// </summary>
    public Unit Width
    {
        get
        {
            return Editor.Width;
        }
        set
        {
            Editor.Width = value;
        }
    }


    /// <summary>
    /// Gets or sets the height of the editor
    /// </summary>
    public Unit Height
    {
        get
        {
            return Editor.Height;
        }
        set
        {
            Editor.Height = value;
        }
    }


    /// <summary>
    /// Gets or sets the editor text.
    /// </summary>
    public string Text
    {
        get
        {
            return Editor.Text;
        }
        set
        {
            Editor.Text = value;
        }
    }

    #endregion


    #region "Auto complete methods"

    /// <summary>
    /// Handles autocomplete action.
    /// </summary>
    /// <param name="macro">Macro expression</param>
    /// <param name="position">Caret position</param>
    /// <param name="isShowContext">If true, method context should be returned, otherwise hints are returned</param>
    /// <param name="previousLines">Text of previous lines</param>
    private string AutoComplete(string macro, int position, bool isShowContext, string previousLines)
    {
        // Do lexical analysis, supress errors
        List<MacroElement> tokens = MacroElement.ParseExpression(macro, true);

        // Variables declared on actual line;
        List<string> inlineVariables = new List<string>();

        // Locate current element, find all created variables
        MacroElement currentElement = null;
        int currentElementIndex = -1;
        for (int i = 0; i < tokens.Count; i++)
        {
            MacroElement el = tokens[i];
            MacroElement elNext = (i < tokens.Count - 1 ? tokens[i + 1] : null);
            if (elNext == null || ((el.StartIndex <= position) && (elNext.StartIndex > position)))
            {
                currentElement = el;
                currentElementIndex = i;
                break;
            }
            if ((el.Type == ElementType.Operator) && (el.Expression == "=") && (i > 0))
            {
                // Add variable declaration to special fields
                MacroElement elPrev = tokens[i - 1];
                if (elPrev.Type == ElementType.Identifier)
                {
                    inlineVariables.Add(elPrev.Expression);
                }
            }
        }

        if (isShowContext)
        {
            return GetContext(tokens, currentElement, currentElementIndex);
        }
        else
        {
            // If we are in the middle of the comment or a constant, do not show any help
            if ((currentElement != null) && ((currentElement.Type == ElementType.Boolean) || (currentElement.Type == ElementType.Double) ||
                (currentElement.Type == ElementType.Integer) || (currentElement.Type == ElementType.String)))
            {
                return "";
            }
            else
            {
                return GetHints(tokens, currentElement, currentElementIndex, previousLines, inlineVariables);
            }
        }
    }


    /// <summary>
    /// Returns context help (current method parameters).
    /// </summary>
    /// <param name="tokens">Lexems from lexical analysis</param>
    /// <param name="currentElement">Current lexem</param>
    /// <param name="currentElementIndex">Index of the current lexem</param>
    private string GetContext(List<MacroElement> tokens, MacroElement currentElement, int currentElementIndex)
    {
        string methodName = "";
        int paramNumber = 0;
        int brackets = 0;
        bool withoutFirstParam = false;
        for (int i = currentElementIndex; i >= 0; i--)
        {
            ElementType type = tokens[i].Type;

            if (brackets == 0)
            {
                // Count number of commas before current element (it's the current number of parameter)
                if (type == ElementType.Comma)
                {
                    paramNumber++;
                }

                // We need to take context to the left side of current lexem
                // It needs to preserve the structure (deepnes withing the brackets)
                if (type == ElementType.LeftBracket)
                {
                    if ((i > 0) && (tokens[i - 1].Type == ElementType.Identifier))
                    {
                        methodName = tokens[i - 1].Expression;
                        if (i > 2)
                        {
                            bool isNamespaceCall = false;
                            if (tokens[i - 3].Type == ElementType.Identifier)
                            {
                                string exprToCheck = tokens[i - 3].Expression;
                                foreach (string item in NamespacesInUse)
                                {
                                    if (item.ToLower() == exprToCheck.ToLower())
                                    {
                                        isNamespaceCall = true;
                                        break;
                                    }
                                }
                            }

                            withoutFirstParam = (tokens[i - 2].Type == ElementType.Dot) && !isNamespaceCall;
                        }
                    }
                    break;
                }
                else if (type == ElementType.LeftIndexer)
                {
                    return "Indexer: Type number or name of the element.";
                }
            }

            // Ensure correct deepnes within the brackets
            if ((type == ElementType.RightBracket) || (type == ElementType.RightIndexer))
            {
                brackets++;
            }
            else if ((type == ElementType.LeftBracket) || (type == ElementType.LeftIndexer))
            {
                brackets--;
            }
        }

        MacroMethod method = MacroMethods.GetMethod(methodName);
        if (method != null)
        {
            return GetMethodString(method, paramNumber, withoutFirstParam);
        }

        // Method not known or there is syntax error in the expression
        return "";
    }


    /// <summary>
    /// Returns method help (return type, name and parameters), highlites current parameter.
    /// </summary>
    /// <param name="method">Method object</param>
    /// <param name="paramNumber">Number of the parameter which should be highlighted</param>
    /// <param name="withoutFirstParam">If true, first parameter is not rendered</param>
    private static string GetMethodString(MacroMethod method, int paramNumber, bool withoutFirstParam)
    {
        string currentParamComment = "";
        string currentParamName = "";
        string parameters = "";
        if (method.ParameterDefinition != null)
        {
            int stratParam = (withoutFirstParam ? 1 : 0);
            object[,] methodParameters = method.ParameterDefinition;
            for (int i = stratParam; i <= methodParameters.GetUpperBound(0); i++)
            {
                string paramtype;
                if (methodParameters[i, 1] is Type)
                {
                    paramtype = ((Type)methodParameters[i, 1]).Name;
                }
                else
                {
                    paramtype = methodParameters[i, 1].ToString();
                }
                string param = paramtype + " " + methodParameters[i, 0].ToString();
                if (i >= method.MinimumParameters)
                {
                    param = "<i>" + param + "</i>";
                }
                if (i - stratParam == paramNumber)
                {
                    currentParamName = methodParameters[i, 0].ToString();
                    currentParamComment = methodParameters[i, 2].ToString();
                    param = "<strong>" + param + "</strong>";
                }
                parameters += ", " + param;
            }
            if (parameters != "")
            {
                parameters = parameters.Substring(2);
            }
        }

        string type = (method.Type == null ? "object" : ((Type)method.Type).Name);
        string methodComment = (string.IsNullOrEmpty(method.Comment) ? "" : "<strong>" + method.Comment + "</strong><br/><br />");
        string paramComment = (string.IsNullOrEmpty(currentParamComment) ? "" : "<br/><br/><strong>" + currentParamName + ":&nbsp;</strong>" + currentParamComment);

        return methodComment + type + " " + method.Name + "(" + parameters + ")" + paramComment;
    }


    /// <summary>
    /// Returns list of possible hints.
    /// </summary>
    /// <param name="tokens">Lexems from lexical analysis</param>
    /// <param name="currentElement">Current lexem</param>
    /// <param name="currentElementIndex">Index of the current lexem</param>
    /// <param name="previousLines">Previous lines to get previous variable declarations</param>
    private string GetHints(List<MacroElement> tokens, MacroElement currentElement, int currentElementIndex, string previousLines, List<string> inlineVariables)
    {
        // Get lists with methods and properties
        List<string> members = new List<string>();

        string dataMemeberContext = "";
        object obj = null;

        List<string> dataProperties = new List<string>();

        if (currentElement == null)
        {
            GetPropertiesFromContext(members, null, "");
        }
        else
        {
            // Properties autocomplete
            bool showAllProperties = false;
            if ((currentElement.Type == ElementType.LeftBracket) || (currentElement.Type == ElementType.LeftIndexer) ||
                (currentElement.Type == ElementType.BlockStart) || (currentElement.Type == ElementType.BlockEnd) ||
                (currentElement.Type == ElementType.Comma))
            {
                showAllProperties = true;
            }

            if (!showAllProperties)
            {
                // We need to find current datamember context
                int brackets = 0;
                for (int i = currentElementIndex - 1; i >= 0; i--)
                {
                    ElementType type = tokens[i].Type;

                    if (brackets == 0)
                    {
                        // We need to take context to the left side of current lexem
                        // It needs to preserve the structure (deepnes withing the brackets)
                        if ((type == ElementType.Comma) || (type == ElementType.Operator) ||
                            (type == ElementType.LeftBracket) || (type == ElementType.LeftIndexer))
                        {
                            break;
                        }
                    }

                    // Append this part
                    dataMemeberContext = tokens[i].Expression + dataMemeberContext;

                    // Ensure correct deepnes within the brackets
                    if ((type == ElementType.RightBracket) || (type == ElementType.RightIndexer))
                    {
                        brackets++;
                    }
                    else if ((type == ElementType.LeftBracket) || (type == ElementType.LeftIndexer))
                    {
                        brackets--;
                    }
                }
                dataMemeberContext = dataMemeberContext.TrimEnd('.');
            }

            // Analyze code before current line to find variable declarations (makes sense only for root hints - i.e. datamember context is empty)
            if (string.IsNullOrEmpty(dataMemeberContext))
            {
                List<MacroElement> vartokens = MacroElement.ParseExpression(previousLines, true);
                List<string> variables = new List<string>();

                for (int i = 0; i < vartokens.Count; i++)
                {
                    MacroElement el = vartokens[i];
                    if ((el.Type == ElementType.Operator) && (el.Expression == "=") && (i > 0))
                    {
                        // Add variable declaration to special fields
                        MacroElement elPrev = vartokens[i - 1];
                        if (elPrev.Type == ElementType.Identifier)
                        {
                            variables.Add(elPrev.Expression);
                        }
                    }
                }

                // Add variables
                members.AddRange(variables);
                members.AddRange(inlineVariables);
            }

            obj = GetPropertiesFromContext(members, dataProperties, dataMemeberContext);
        }

        // Add proper methods
        if (string.IsNullOrEmpty(dataMemeberContext))
        {
            // Add commands only for empty contexts
            members.AddRange(CommandsList);

            // Add namespaces objects
            members.Add("Math");
            members.Add("Transformation");
            members.Add("Countries");

            // List of methods for any type
            FillFromMethodList(members, MacroMethods.GetMethodsForObject(null));
        }
        else
        {
            if (obj == null)
            {
                // If the object is null we will offer string methods because these are the most common methods
                obj = "";
            }

            // Get the list of suitable methods for resulting type
            FillFromMethodList(members, MacroMethods.GetMethodsForObject(obj));
        }

        // Add methods which belongs to permanently registered namespaces
        members.AddRange(UsingList);

        // Sort everything
        members.Sort();

        // Add data properties at the end
        if (dataProperties.Count > 0)
        {
            members.Add("----");
            members.AddRange(dataProperties);
        }

        // Remove duplicities
        List<string> finalList = new List<string>();
        foreach (string member in members)
        {
            if (!finalList.Contains(member))
            {
                finalList.Add(member);
            }
        }

        StringBuilder sb = new StringBuilder();
        foreach (string member in finalList)
        {
            sb.Append("$" + member);
        }
        return sb.ToString().TrimStart('$');
    }


    /// <summary>
    /// Resolves the data member context and adds correct properties options to a given list.
    /// </summary>
    /// <param name="properties">List of properties where to add </param>
    /// <param name="dataProperties">List of data properties</param>
    /// <param name="dataMemberContext">Data member context (macro part)</param>
    private object GetPropertiesFromContext(List<string> properties, List<string> dataProperties, string dataMemberContext)
    {
        if (string.IsNullOrEmpty(dataMemberContext))
        {
            // Add CMSContext.Current
            properties.AddRange(CMSContext.Current.Properties);

            // Add all resolver special values
            properties.AddRange(MacroResolver.SpecialValues);

            // Add all members contained in given resolver

            // Source tables
            if (Resolver.SourceTables != null)
            {
                foreach (Hashtable table in Resolver.SourceTables)
                {
                    foreach (string key in table.Keys)
                    {
                        properties.Add(key);
                    }
                }
            }

            // Special macros
            if (Resolver.SpecialMacros != null)
            {
                int length = Resolver.SpecialMacros.GetUpperBound(0);
                for (int i = 0; i <= length; i++)
                {
                    properties.Add(Resolver.SpecialMacros[i, 0]);
                }
            }

            // Source parameters
            if (Resolver.SourceParameters != null)
            {
                int length = Resolver.SourceParameters.GetUpperBound(0);
                for (int i = 0; i <= length; i++)
                {
                    properties.Add(ValidationHelper.GetString(Resolver.SourceParameters[i, 0], ""));
                }
            }

            // Dynamic macros
            if (Resolver.DynamicParameters != null)
            {
                foreach (string key in Resolver.DynamicParameters.Keys)
                {
                    properties.Add(key);
                }
            }

            // Named source data
            properties.AddRange(Resolver.NamedSourceDataKeys);

            return null;
        }
        else
        {
            object obj = null;
            bool match = false;
            bool securityCheckPassed = false;

            // Disable security check and resolve the macro
            Resolver.UserName = CMSContext.CurrentUser.UserName;
            Resolver.CheckIntegrity = false;
            Resolver.ResolveDataMacro(dataMemberContext, ref obj, out match, true, out securityCheckPassed);

            if (securityCheckPassed)
            {
                if (match)
                {
                    // Fill at most MaxMacroNodes properties to AutoCompletion data
                    FillObjectProperties(properties, dataProperties, obj);
                    if (dataProperties.Count > MacroResolver.MaxMacroNodes)
                    {
                        dataProperties.RemoveRange(MacroResolver.MaxMacroNodes, dataProperties.Count - MacroResolver.MaxMacroNodes);
                    }
                }
            }
            else
            {
                properties.Add(GetString("macrodesigner.accessdenied"));
            }

            return obj;
        }
    }


    /// <summary>
    /// Fills given list with properties of given object.
    /// </summary>
    /// <param name="properties">List to fill</param>
    /// <param name="dataProperties">List of data properties</param>
    /// <param name="obj">Object the properties of which should be added to the list</param>
    private static void FillObjectProperties(List<string> properties, List<string> dataProperties, object obj)
    {
        if (obj != null)
        {
            if (obj is DataRow)
            {
                // DataRow source, bind column names
                DataRow dr = (DataRow)obj;
                foreach (DataColumn col in dr.Table.Columns)
                {
                    properties.Add(col.ColumnName);
                }
            }
            else if (obj is DataRowView)
            {
                // DataRowView source, bind column names
                DataRowView dr = (DataRowView)obj;
                foreach (DataColumn col in dr.DataView.Table.Columns)
                {
                    properties.Add(col.ColumnName);
                }
            }
            else if (obj is AbstractContext)
            {
                AbstractContext context = (AbstractContext)obj;
                properties.AddRange(context.Properties);
            }
            else if (obj is IHierarchicalObject)
            {
                // Data container source
                IHierarchicalObject hc = (IHierarchicalObject)obj;
                properties.AddRange(hc.Properties);
            }
            else if (obj is IDataContainer)
            {
                // Data container source
                IDataContainer dc = (IDataContainer)obj;
                properties.AddRange(dc.ColumnNames);
            }

            // Named enumerable objects
            if (obj is INamedEnumerable)
            {
                INamedEnumerable namedCol = (INamedEnumerable)obj;
                if (namedCol.ItemsHaveNames)
                {
                    IEnumerator enumerator = namedCol.GetNamedEnumerator();
                    while (enumerator.MoveNext())
                    {
                        // Named item
                        string name = namedCol.GetObjectName(enumerator.Current);
                        if (ValidationHelper.IsIdentifier(name))
                        {
                            dataProperties.Add(name);
                        }
                        else
                        {
                            dataProperties.Add("[\"" + name + "\"]");
                        }
                    }
                }
            }

            // Special case for TreeNode and PageInfo - append ediable parts
            if ((obj is CMS.TreeEngine.TreeNode) || (obj is PageInfo))
            {
                EditableItems eItems = null;

                if (obj is CMS.TreeEngine.TreeNode)
                {
                    // TreeNode editable fields
                    CMS.TreeEngine.TreeNode node = (CMS.TreeEngine.TreeNode)obj;
                    eItems = node.DocumentContent;
                }
                else
                {
                    PageInfo pi = (PageInfo)obj;
                    eItems = pi.EditableItems;
                }

                // Editable regions
                foreach (string item in eItems.EditableRegions.Keys)
                {
                    properties.Add(item);
                }

                // Editable webparts
                foreach (string item in eItems.EditableWebParts.Keys)
                {
                    properties.Add(item);
                }
            }
        }
    }


    /// <summary>
    /// Fills the autoCompletionList with strings formated for AutoCompletion.
    /// </summary>
    /// <param name="autoCompletionList">Target string list</param>
    /// <param name="methodList">Source MacroMethod list</param>
    private static void FillFromMethodList(List<string> autoCompletionList, List<MacroMethod> methodList)
    {
        foreach (MacroMethod method in methodList)
        {
            string m = method.Name.Trim().ToLower();
            if (!MacroElement.IsValidOperator(m) && !MacroElement.IsWordOperator(m))
            {
                if ((m != "logtodebug") || CMSContext.CurrentUser.IsGlobalAdministrator)
                {
                    // At sign at the end is to distinguish between method and property in the script afterwards
                    if (string.IsNullOrEmpty(method.Snippet))
                    {
                        autoCompletionList.Add(method.Name + "\n");
                    }
                    else
                    {
                        autoCompletionList.Add(method.Name + "\n" + method.Snippet);
                    }
                }
            }
        }
    }


    /// <summary>
    /// Returns JS code which registers help for all the methods.
    /// </summary>
    private string GetJSMethodsHashTable()
    {
        StringBuilder sb = new StringBuilder();
        foreach (MacroMethod m in MacroMethods.Methods.Values)
        {
            string name = m.Name;
            string comment = GetMethodString(m, -1, false);

            sb.AppendLine("methodComments[" + ScriptHelper.GetString(name + "\n") + "] = " + ScriptHelper.GetString(comment) + ";");
        }
        return sb.ToString();
    }

    #endregion


    #region "Page events"

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        // Hide all the hints at the beginning
        this.pnlContext.Style.Add("display", "none");
        this.pnlQuickContext.Style.Add("display", "none");
        this.pnlHints.Style.Add("display", "none");

        string editorObjName = "autoCompleteObj_" + this.ClientID;

        if (this.UseAutoComplete)
        {
            // Register main macro editor scripts
            ScriptHelper.RegisterJQuery(this.Page);

            ScriptHelper.RegisterScriptFile(this.Page, "MacroEditor.js");

            ScriptHelper.RegisterClientScriptBlock(this.Page, typeof(string), "MacroEditorConstants",
                "var METHOD_IMG = '" + GetImageUrl("/Design/Controls/MacroEditor/method.png") + "';" +
                "var SNIPPET_IMG = '" + GetImageUrl("/Design/Controls/MacroEditor/snippet.png") + "';" +
                "var PROPERTY_IMG = '" + GetImageUrl("/Design/Controls/MacroEditor/property.png") + "';", true);

            ScriptHelper.RegisterClientScriptBlock(this.Page, typeof(string), "MacroEditorCallbackScript_" + this.ClientID,
                "function handleCallback_" + this.ClientID + "(value, context) {" +
                "    if (context == 'context') {" +
                "        " + editorObjName + ".fillContext(value);" +
                "    } else {" +
                "        " + editorObjName + ".fillHints(value);" +
                "    }" +
                "}", true);
        }
    }

    protected override void Render(HtmlTextWriter writer)
    {
        base.Render(writer);

        if (this.UseAutoComplete)
        {
            // We need to generate this script on Render since extended area does that that late as well
            // and editor object is not available before
            string cbRefShowHint = Page.ClientScript.GetCallbackEventReference(this, "this.callbackArgument", "handleCallback_" + this.ClientID, "'hint'", true);
            string cbRefShowContext = Page.ClientScript.GetCallbackEventReference(this, "this.callbackArgument", "handleCallback_" + this.ClientID, "'context'", true);

            string script =
                "var " + this.AutoCompletionObject + " = new AutoCompleteExtender(" + txtCode.EditorID + ", document.getElementById('" + this.pnlHints.ClientID + "'), document.getElementById('" + this.pnlContext.ClientID + "'), document.getElementById('" + this.pnlQuickContext.ClientID + "'), " + (MixedMode ? "true" : "false") + ", " + TopOffset + ", " + LeftOffset + "); \n" +
                this.AutoCompletionObject + ".callbackContext = function() { " + cbRefShowContext + "; }\n" +
                this.AutoCompletionObject + ".callbackHint = function() { " + cbRefShowHint + "; }\n" +
                this.AutoCompletionObject + ".editorFocusLost = function editorFocusLost_" + this.ClientID + "() {" + EditorFocusLostScript + "}\n";

            if (ShowAutoCompletionAbove)
            {
                script += this.AutoCompletionObject + ".forceAbove = true; \n";
            }

            ScriptHelper.RegisterStartupScript(this.Page, typeof(string), "MacroEditorScript_" + this.ClientID, script, true);
            ScriptHelper.RegisterStartupScript(this.Page, typeof(string), "MacroEditorMethodsScript", GetJSMethodsHashTable(), true);
        }
    }

    #endregion


    #region ICallbackEventHandler Members

    string wholeText;
    string text = "";
    int position = 0;
    bool isShowContext = false;

    public string GetCallbackResult()
    {
        if (position == 0)
        {
            return AutoComplete(text, text.Length, isShowContext, wholeText);
        }
        else
        {
            return AutoComplete(text, position, isShowContext, wholeText);
        }
    }


    public void RaiseCallbackEvent(string eventArgument)
    {
        string[] args = eventArgument.Split(new char[] { '\n' });
        if (args.Length == 5)
        {
            isShowContext = (args[3].ToLower() == "context");
            wholeText = args[4];
            position = ValidationHelper.GetInteger(args[2], 0);
            if (args[1] == "")
            {
                text = args[0];
            }
            else
            {
                // Normal character - needs to be placed to correct position
                text = args[0].Substring(0, position) + args[1] + args[0].Substring(position);
            }
        }
    }

    #endregion
}