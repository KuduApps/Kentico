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

public partial class CMSAdminControls_UI_Macros_MacroDesignerGroup : MacroDesignerGroup
{
    #region "Variables"

    /// <summary>
    /// Drag and drop extender for the group
    /// </summary>
    protected DragAndDropExtender extDragDrop = null;

    #endregion


    #region "Properties"

    /// <summary>
    /// Returns the operator of the group.
    /// </summary>
    public override string GroupOperator
    {
        get
        {
            if (radOr.Checked)
            {
                return "||";
            }
            else
            {
                return "&&";
            }
        }
        set
        {
            if (value == "||")
            {
                radOr.Checked = true;
            }
            else
            {
                radAnd.Checked = true;
            }
        }
    }


    /// <summary>
    /// Returns the condition created by this boolean expression designer.
    /// </summary>
    public override string Condition
    {
        get
        {
            this.StoreData();
            return this.CurrentGroup.Condition;
        }
        set
        {
            // Parse the condition and build the tree from it
            List<MacroElement> elements = MacroElement.ParseExpression(value, true);
            this.CurrentGroup = MacroDesignerTree.BuildTreeFromExpression(elements, 0, elements.Count - 1);

            // Rebuild the designer according to the new condition
            this.BuildDesigner(true);
        }
    }


    /// <summary>
    /// Gets or sets the current macro structure.
    /// </summary>
    public override MacroDesignerTree CurrentGroup
    {
        get
        {
            MacroDesignerTree group = ViewState["CurrentGroup"] as MacroDesignerTree;
            if (group == null)
            {
                group = new MacroDesignerTree();
                ViewState["CurrentGroup"] = group;
            }
            return group;
        }
        set
        {
            ViewState["CurrentGroup"] = value;
        }
    }

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        this.btnAddGroup.Click += new ImageClickEventHandler(btnAddGroup_Click);
        this.btnAddExp.Click += new ImageClickEventHandler(btnAddExp_Click);
        this.btnRemove.Click += new ImageClickEventHandler(btnRemove_Click);
        this.btnRemoveExpression.Click += new ImageClickEventHandler(btnRemove_Click);

        this.btnAddGroup.ImageUrl = GetImageUrl("/Design/Controls/MacroDesigner/AddGroup.png");
        this.btnAddExp.ImageUrl = GetImageUrl("/Design/Controls/MacroDesigner/AddExpression.png");
        this.btnRemove.ImageUrl = GetImageUrl("/CMSModules/CMS_PortalEngine/Widgets/DeleteDashboard.png");
        this.btnRemoveExpression.ImageUrl = GetImageUrl("/Design/Controls/MacroDesigner/Remove.png");
        this.btnGroupContextMenu.ImageUrl = GetImageUrl("/Design/Controls/UniGrid/Actions/Menu.png");
        this.btnExprContextMenu.ImageUrl = GetImageUrl("/Design/Controls/UniGrid/Actions/Menu.png");
        this.imgMove.ImageUrl = GetImageUrl("/Design/Controls/MacroDesigner/Move.png");

        this.BuildDesigner(false);
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        this.cmcExpr.MenuControlPath = "~/CMSAdminControls/UI/Macros/MacroDesignerMenu.ascx";
        this.cmcGroup.MenuControlPath = "~/CMSAdminControls/UI/Macros/MacroDesignerMenu.ascx";
        this.cmcExprHandle.MenuControlPath = "~/CMSAdminControls/UI/Macros/MacroDesignerMenu.ascx";
        this.cmcGroupHeader.MenuControlPath = "~/CMSAdminControls/UI/Macros/MacroDesignerMenu.ascx";

        int groupCount = (this.CurrentGroup.ParentGroup == null ? 0 : this.CurrentGroup.ParentGroup.ChildGroups.Count);

        this.cmcExpr.Parameter = PrepareContextMenuParameters(this.CurrentGroup.IDPath, groupCount);
        this.cmcGroup.Parameter = PrepareContextMenuParameters(this.CurrentGroup.IDPath, groupCount);
        this.cmcExprHandle.Parameter = PrepareContextMenuParameters(this.CurrentGroup.IDPath, groupCount);
        this.cmcGroupHeader.Parameter = PrepareContextMenuParameters(this.CurrentGroup.IDPath, groupCount);

        bool isLeaf = this.CurrentGroup.IsLeaf;

        // Set correct styles
        this.pnlMacroGroup.CssClass = (isLeaf ? "MacroDesignerExpression" : "MacroDesignerGroup");
        this.pnlGroups.CssClass = (isLeaf ? "" : "MacroDesignerChildGroups");

        // Display correct controls according to type
        this.pnlOperator.Visible = (this.CurrentGroup.Position > 0);
        this.pnlRemoveGroup.Visible = (this.CurrentGroup.ParentGroup != null) && !isLeaf;
        this.pnlRemoveExpression.Visible = (this.CurrentGroup.ParentGroup != null) && isLeaf;
        this.pnlHandle.Visible = isLeaf;
        this.pnlButtons.Visible = !isLeaf;
        this.pnlHeader.Visible = !isLeaf;
        this.pnlGroups.Attributes.Add("IDPath", this.CurrentGroup.IDPath);

        // Display correct tooltips
        this.btnAddGroup.ToolTip = GetString("macrodesigner.addgroup");
        this.btnAddExp.ToolTip = GetString("macrodesigner.addexpression");
        this.btnRemove.ToolTip = GetString("macrodesigner.removegroup");
        this.btnRemoveExpression.ToolTip = GetString("macrodesigner.removeexpression");
        this.imgMove.ToolTip = GetString("macrodesigner.moveexpression");

        this.pnlGroupHandle.Attributes.Add("onmousedown", "return false;");
        this.pnlGroupHandle.Attributes.Add("onclick", "return false;");

        this.pnlItemHandle.Attributes.Add("onmousedown", "return false;");
        this.pnlItemHandle.Attributes.Add("onclick", "return false;");

        if (isLeaf)
        {
            this.pnlItemHandle.Attributes.Add("onmouseover", "ActivateBorder('" + this.pnlMacroGroup.ClientID + "', 'MacroDesignerExpression');");
            this.pnlItemHandle.Attributes.Add("onmouseout", "DeactivateBorder('" + this.pnlMacroGroup.ClientID + "', 'MacroDesignerExpression');");
        }
        else
        {
            this.pnlMacroGroup.CssClass += " G";

            if (this.CurrentGroup.Level > 0)
            {
                this.pnlHeader.Attributes.Add("onmouseover", "ActivateBorder('" + this.pnlMacroGroup.ClientID + "', 'MacroDesignerGroup');");
                this.pnlHeader.Attributes.Add("onmouseout", "DeactivateBorder('" + this.pnlMacroGroup.ClientID + "', 'MacroDesignerGroup');");
            }
            else
            {
                // First group is not movable
                this.pnlHeader.Attributes["style"] = "cursor: default;";
            }
        }

        // Register the scripts
        string script = @"
function ActivateBorder(elementId, className) {
  var e = document.getElementById(elementId);
  if (e != null) {
    e.className = e.className.replace(className, className + 'Active');
  }
}

function DeactivateBorder(elementId, className) {
  var e = document.getElementById(elementId);
  if (e != null) {
    e.className = e.className.replace(className + 'Active', className);
  }
}
";
        ScriptHelper.RegisterStartupScript(this.Page, typeof(string), "ActivateDeactivateBorder", script, true);

        // Register IDPath of the group;
        this.ltlScript.Text = ScriptHelper.GetScript("groupLocations['" + this.pnlGroups.ClientID + "'] = '" + this.CurrentGroup.IDPath + "'; groupLocations['" + this.ClientID + "'] = '" + this.CurrentGroup.IDPath + "';");
    }


    protected override void Render(HtmlTextWriter writer)
    {
        writer.Write("<div id=\"{0}\" class=\"MacroElement\">", this.ClientID);

        base.Render(writer);

        writer.Write("</div>");
    }


    protected void btnRemove_Click(object sender, ImageClickEventArgs e)
    {
        this.CurrentGroup.ParentGroup.RemoveGroup(this.CurrentGroup.Position);
        RebuildDesigner();
    }


    protected void btnAddExp_Click(object sender, ImageClickEventArgs e)
    {
        MacroDesignerGroup root = GetRootGroup();
        root.StoreData();

        MacroDesignerTree item = this.CurrentGroup.AddExpression();
        MacroDesignerGroup ctrl = (MacroDesignerGroup)this.AddGroup(item.IDPath);
        ctrl.CurrentGroup = item;

        root.BuildDesigner(true);
    }


    protected void btnAddGroup_Click(object sender, ImageClickEventArgs e)
    {
        MacroDesignerGroup root = GetRootGroup();
        root.StoreData();

        MacroDesignerTree item = this.CurrentGroup.AddGroup();
        this.AddGroup(item.IDPath);

        root.BuildDesigner(true);
    }

    #endregion


    #region "Methods"

    /// <summary>
    /// Create parameters for context menu (format [up]@[down]@[parent]).
    /// </summary>
    /// <param name="idPath">IDPath</param>
    private static string PrepareContextMenuParameters(string idPath, int itemCount)
    {
        string retval = "";
        if (!string.IsNullOrEmpty(idPath))
        {
            string[] path = idPath.Split('.');
            int currentPos = ValidationHelper.GetInteger(path[path.Length - 1], 0);
            if (path.Length == 1)
            {
                string upParams = "";
                if (currentPos > 0)
                {
                    upParams = idPath + ";;" + (currentPos - 1);
                }
                string downParams = "";
                if (itemCount > currentPos + 1)
                {
                    downParams = idPath + ";;" + (currentPos + 1);
                }

                retval = upParams + "@" + downParams + "@";
            }
            else
            {
                string targetPathBase = string.Join(".", path, 0, path.Length - 2);

                string upParams = "";
                if (currentPos > 0)
                {
                    upParams = idPath + ";" + targetPathBase + "." + path[path.Length - 2] + ";" + (currentPos - 1);
                }
                string downParams = "";
                if (itemCount > currentPos + 1)
                {
                    downParams = idPath + ";" + targetPathBase + "." + path[path.Length - 2] + ";" + (currentPos + 1);
                }
                string parentParams = idPath + ";" + targetPathBase + ";" + ValidationHelper.GetInteger(path[path.Length - 2], 0);

                retval = upParams + "@" + downParams + "@" + parentParams;
            }
        }
        return ScriptHelper.GetString(retval);
    }


    /// <summary>
    /// Returns the root group.
    /// </summary>
    /// <param name="recursive">If true, BuldDesigner is called to child groups as well</param>
    public MacroDesignerGroup GetRootGroup()
    {
        MacroDesignerGroup group = this;
        while ((group.Parent != null) && (group.Parent.Parent.Parent != null) && (group.Parent.Parent.Parent is MacroDesignerGroup))
        {
            group = (MacroDesignerGroup)group.Parent.Parent.Parent;
        }
        return group;
    }


    /// <summary>
    /// Saves the current data to the MacroDesignerTree object.
    /// </summary>
    public override void StoreData()
    {
        if (this.CurrentGroup.IsLeaf)
        {
            this.CurrentGroup.GroupOperator = this.GroupOperator;
            if (this.pnlGroups.Controls.Count > 0)
            {
                MacroBoolExpression expr = (MacroBoolExpression)this.pnlGroups.Controls[0];
                this.CurrentGroup.LeftExpression = expr.LeftExpression;
                this.CurrentGroup.RightExpression = expr.RightExpression;
                this.CurrentGroup.Operator = expr.Operator;
            }
        }
        else
        {
            this.CurrentGroup.GroupOperator = this.GroupOperator;
            foreach (Control item in this.pnlGroups.Controls)
            {
                if (item is MacroDesignerGroup)
                {
                    MacroDesignerGroup group = (MacroDesignerGroup)item;
                    group.StoreData();
                }
            }
        }
    }


    /// <summary>
    /// Builds the designer controls structure.
    /// </summary>
    public override void BuildDesigner(bool recursive)
    {
        // Append child groups and expressions
        this.pnlGroups.Controls.Clear();

        if (this.CurrentGroup.IsLeaf)
        {
            plcNoItems.Visible = false;

            this.AddExpression(this.CurrentGroup.LeftExpression, this.CurrentGroup.RightExpression, this.CurrentGroup.Operator, this.CurrentGroup.IDPath);
        }
        else
        {
            if (this.CurrentGroup.Level == 0)
            {
                plcNoItems.Visible = true;
            }

            // Add child groups
            foreach (MacroDesignerTree child in this.CurrentGroup.ChildGroups)
            {
                MacroDesignerGroup ctrl = (MacroDesignerGroup)this.AddGroup(child.IDPath);
                ctrl.GroupOperator = child.GroupOperator;
                ctrl.CurrentGroup = child;
                if (recursive)
                {
                    ctrl.BuildDesigner(true);
                }

                plcNoItems.Visible = false;
            }

            // Drop cue
            Panel pnlCue = new Panel();
            pnlCue.ID = "pnlCue";
            pnlCue.CssClass = "MacroDesignerCue";
            pnlGroups.Controls.Add(pnlCue);

            pnlCue.Controls.Add(new LiteralControl("&nbsp;"));
            pnlCue.Style.Add("display", "none");

            // Create drag and drop extender
            extDragDrop = new DragAndDropExtender();

            extDragDrop.ID = "extDragDrop";
            extDragDrop.TargetControlID = pnlGroups.ID;
            extDragDrop.DragItemClass = "MacroElement";
            extDragDrop.DragItemHandleClass = "MacroElementHandle";
            extDragDrop.DropCueID = pnlCue.ID;
            extDragDrop.OnClientDrop = "OnDropGroup";

            pnlGroups.Controls.Add(extDragDrop);
        }
    }


    /// <summary>
    /// Rebuilds the whole structure according to the MacroDesignerTree object.
    /// </summary>
    public void RebuildDesigner()
    {
        MacroDesignerGroup root = GetRootGroup();
        root.StoreData();
        root.BuildDesigner(true);
    }


    /// <summary>
    /// Moves the group to given location.
    /// </summary>
    /// <param name="sourcePath">Position path of the source</param>
    /// <param name="targetPath">Position path of the target</param>
    /// <param name="targetPos">Position within the target</param>
    public void MoveGroup(string sourcePath, string targetPath, int targetPos)
    {
        MacroDesignerGroup root = GetRootGroup();
        root.StoreData();
        root.CurrentGroup.MoveGroup(sourcePath, targetPath, targetPos);
        root.BuildDesigner(true);
    }


    /// <summary>
    /// Loads new group control into the child panel controls collection.
    /// </summary>
    /// <param name="idPath">IDPath to ensure unique ID of the control</param>
    public Control AddGroup(string idPath)
    {
        // Add control to a controls collection
        Control group = this.Page.LoadControl("~/CMSAdminControls/UI/Macros/MacroDesignerGroup.ascx");
        group.ID = "g" + idPath.Replace(".", "_");
        this.pnlGroups.Controls.Add(group);

        return group;
    }


    /// <summary>
    /// Adds an expression to a child panel controls collection.
    /// </summary>
    /// <param name="left">Left part of the expression</param>
    /// <param name="right">Right part of the expression</param>
    /// <param name="op">Operator to use</param>
    /// <param name="idPath">IDPath to ensure unique ID of the control</param>
    public void AddExpression(string left, string right, string op, string idPath)
    {
        // Add control to a controls collection
        MacroBoolExpression expr = (MacroBoolExpression)this.Page.LoadControl("~/CMSAdminControls/UI/Macros/MacroBoolExpression.ascx");
        expr.LeftExpression = left;
        expr.RightExpression = right;
        expr.Operator = op;
        expr.ID = "e" + idPath.Replace(".", "_");

        this.pnlGroups.Controls.Add(expr);
    }

    #endregion
}