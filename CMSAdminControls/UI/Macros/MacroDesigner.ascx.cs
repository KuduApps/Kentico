using System;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;

using CMS.GlobalHelper;
using CMS.FormControls;
using CMS.CMSHelper;
using CMS.UIControls;
using CMS.ExtendedControls.DragAndDrop;
using CMS.ExtendedControls;

public partial class CMSAdminControls_UI_Macros_MacroDesigner : FormEngineUserControl
{
    #region "Properties"

    public override object Value
    {
        get
        {
            return this.Condition;
        }
        set
        {
            string val = ValidationHelper.GetString(value, "");
            if (val.EndsWith("%}"))
            {
                val = val.Substring(0, val.Length - 2);
            }
            if (val.StartsWith("{%"))
            {
                val = val.Substring(2);
            }
            this.Condition = val;
        }
    }


    /// <summary>
    /// Returns the condition created by this boolean expression designer.
    /// </summary>
    public string Condition
    {
        get
        {
            if (ValidationHelper.GetInteger(this.hdnSelTab.Value, 0) == 0)
            {
                return this.designerElem.Condition;
            }
            else
            {
                return this.editorElem.Text;
            }
        }
        set
        {
            this.designerElem.Condition = value;
            this.editorElem.Text = value;
        }
    }


    /// <summary>
    /// Returns the editor object.
    /// </summary>
    public ExtendedTextArea Editor
    {
        get
        {
            return this.editorElem.Editor;
        }
    }

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        this.editorElem.Editor.UseSmallFonts = true;
        this.editorElem.TopOffset = 37;

        this.btnShowCode.Click += new EventHandler(btnShowCode_Click);
        this.btnShowDesigner.Click += new EventHandler(btnShowDesigner_Click);
        this.btnMoveGroup.Click += new EventHandler(btnMoveGroup_Click);

        if (!RequestHelper.IsPostBack())
        {
            if (ValidationHelper.GetInteger(CookieHelper.GetValue("MacroDesignerTab"), 0) == 1)
            {
                ShowCode(false);
            }
        }

        string[,] tabs = new string[2, 5];
        tabs[0, 0] = GetString("macrodesigner.designer");
        tabs[0, 1] = "if (document.getElementById('" + this.hdnSelTab.ClientID + "').value == 1) { if (confirm('" + GetString("macrodesigner.switchtodesigner") + "')) { " + ControlsHelper.GetPostBackEventReference(this.btnShowDesigner, null) + "; }} return false;";
        tabs[0, 2] = "";
        tabs[0, 3] = GetString("macrodesigner.designertooltip");
        tabs[1, 0] = GetString("macrodesigner.code");
        tabs[1, 1] = "if (document.getElementById('" + this.hdnSelTab.ClientID + "').value == 0) { " + ControlsHelper.GetPostBackEventReference(this.btnShowCode, null) + "; } return false;";
        tabs[1, 2] = "";
        tabs[1, 3] = GetString("macrodesigner.codetooltip");

        this.tabsElem.UseClientScript = true;
        this.tabsElem.UrlTarget = "_self";
        this.tabsElem.Tabs = tabs;

        // Register move script
        string script = string.Format(@"
function OnDropGroup(source, target) {{ 
  var container = target.get_container(); 
  var item = target.get_droppedItem();
  var targetPos = target.get_position(); 

  var hidden = document.getElementById('{0}')
  if (hidden != null) {{
    hidden.value = groupLocations[item.id] + ';' + groupLocations[container.id] + ';' + targetPos;
    {1}; 
  }}
}}
function MoveDesignerGroup(params, noOperationMessage) {{ 
  if (params != '') {{
    var hidden = document.getElementById('{0}')
    if (hidden != null) {{
      hidden.value = params;
      {1}; 
    }}
  }} else {{
     alert(noOperationMessage);
  }}
}}
", this.hdnMove.ClientID, ControlsHelper.GetPostBackEventReference(this.btnMoveGroup, null));

        ScriptHelper.RegisterStartupScript(this.Page, typeof(string), "DesignerOnDropGroup", script, true);
        this.ltlScript.Text = ScriptHelper.GetScript("var groupLocations = new Array();");
    }


    protected void btnMoveGroup_Click(object sender, EventArgs e)
    {
        string[] moveParams = this.hdnMove.Value.Split(';');
        if (moveParams.Length == 3)
        {
            int targetPos = ValidationHelper.GetInteger(moveParams[2], 0);
            this.MoveGroup(moveParams[0], moveParams[1], targetPos);
        }
    }


    protected void btnShowDesigner_Click(object sender, EventArgs e)
    {
        ShowDesigner();
    }


    protected void btnShowCode_Click(object sender, EventArgs e)
    {
        ShowCode();
    }


    private void ShowDesigner()
    {
        try
        {
            this.designerElem.Condition = this.editorElem.Text;

            this.pnlDesigner.Visible = true;
            this.pnlEditor.Visible = false;
            this.tabsElem.SelectedTab = 0;
            this.hdnSelTab.Value = "0";

            CookieHelper.SetValue("MacroDesignerTab", "0", DateTime.Now.AddDays(1));
        }
        catch (Exception ex)
        {
            ScriptHelper.Alert(this.Page, ex.Message);

            this.tabsElem.SelectedTab = 1;
        }
    }


    private void ShowCode()
    {
        ShowCode(true);
    }


    private void ShowCode(bool updateText)
    {
        this.pnlDesigner.Visible = false;
        this.pnlEditor.Visible = true;

        if (updateText)
        {
            this.editorElem.Text = this.designerElem.Condition;
        }
        this.tabsElem.SelectedTab = 1;
        this.hdnSelTab.Value = "1";

        CookieHelper.SetValue("MacroDesignerTab", "1", DateTime.Now.AddDays(1));
    }

    #endregion


    #region "Methods"

    /// <summary>
    /// Rebuilds the designer according to a text in the editor.
    /// </summary>
    public void RebuildDesigner()
    {
        this.designerElem.Condition = this.editorElem.Text;
    }


    /// <summary>
    /// Moves the group within the designer
    /// </summary>
    /// <param name="sourcePath">Source path</param>
    /// <param name="targetPath">Target path</param>
    /// <param name="targetPos">Target position</param>
    public void MoveGroup(string sourcePath, string targetPath, int targetPos)
    {
        this.designerElem.StoreData();
        this.designerElem.MoveGroup(sourcePath, targetPath, targetPos);
    }


    #endregion
}