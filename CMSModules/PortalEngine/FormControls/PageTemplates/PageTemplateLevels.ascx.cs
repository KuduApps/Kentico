using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.FormControls;
using CMS.GlobalHelper;

public partial class CMSModules_PortalEngine_FormControls_PageTemplates_PageTemplateLevels : FormEngineUserControl
{
    #region "Variables"

    private bool mDocummentSettings = false;

    #endregion


    #region "Public properties"

    /// <summary>
    /// Gets or sets tree path - if set is created from TreePath.
    /// </summary>
    public string TreePath
    {
        get
        {
            return treeElem.TreePath;
        }
        set
        {
            treeElem.TreePath = value;
        }
    }


    /// <summary>
    /// Gets or sets Level, levels are rendered only if TreePath is not set. 
    /// </summary>
    public int Level
    {
        get
        {
            return treeElem.Level;
        }
        set
        {
            treeElem.Level = value;
        }
    }


    /// <summary>
    /// Gets or sets value.
    /// </summary>
    public override object Value
    {
        get
        {
            // Inherit all
            if (radInheritAll.Checked)
            {
                treeElem.Value = String.Empty;
                ltlScript.Text = ScriptHelper.GetScript("document.getElementById('treeSpan').style.display = \"none\";");
                return String.Empty;
            }

            // Do not inherit any content
            if (radNonInherit.Checked)
            {
                treeElem.Value = String.Empty;
                ltlScript.Text = ScriptHelper.GetScript("document.getElementById('treeSpan').style.display = \"none\";");
                return "/";
            }

            // Inherit from master
            if (radInheritMaster.Checked)
            {
                treeElem.Value = String.Empty;
                ltlScript.Text = ScriptHelper.GetScript("document.getElementById('treeSpan').style.display = \"none\";");
                return "\\";
            }

            return treeElem.Value;
        }
        set
        {
            treeElem.Value = value;
        }
    }


    /// <summary>
    /// Document settings.
    /// </summary>
    public bool DocumentSettings
    {
        get
        {
            return mDocummentSettings;
        }
        set
        {
            mDocummentSettings = value;
        }
    }

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {

        if (DocumentSettings)
        {
            radInheritAll.Text = GetString("InheritLevels.UseTemplateSettigns");
        }
        else
        {
            radInheritAll.Text = GetString("InheritLevels.InheritAll");
        }
        radNonInherit.Text = GetString("InheritLevels.NonInherit");
        radSelect.Text = GetString("InheritLevels.Select");
        radInheritMaster.Text = GetString("InheritLevels.InheritMaster");

        if (!RequestHelper.IsPostBack())
        {
            radInheritAll.Checked = true;
            string treeValue = ValidationHelper.GetString(treeElem.Value, string.Empty);

            // Do not inherit any content
            if (treeValue == "/")
            {
                radNonInherit.Checked = true;
            }
            // Inherit from master
            else if (treeValue == "\\")
            {
                radInheritMaster.Checked = true;
            }
            //  Inherited levels
            else if (!String.IsNullOrEmpty(treeValue))
            {
                radSelect.Checked = true;
                ltlScript.Text = ScriptHelper.GetScript("document.getElementById('treeSpan').style.display = \"inline\";");
            }

            radInheritAll.Attributes.Add("onclick", "ShowOrHideTree(false);");
            radSelect.Attributes.Add("onclick", "ShowOrHideTree(true);");
            radNonInherit.Attributes.Add("onclick", "ShowOrHideTree(false);");
            radInheritMaster.Attributes.Add("onclick", "ShowOrHideTree(false);"); 
        }
        else
        {
            // Show/hide tree after postback
            ltlScript.Text = ScriptHelper.GetScript("ShowOrHideTree(" + radSelect.Checked.ToString().ToLower() + ");");
        }
    }

    #endregion
}
