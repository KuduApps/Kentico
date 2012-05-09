using System;
using System.Text;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.SettingsProvider;

public partial class CMSAdminControls_UI_UIProfiles_UIElementEdit : CMSUserControl
{
    #region "Variables"

    private int mElementID = 0;
    private int mResourceID = 0;
    private int mParentID = 0;
    private UIElementInfo elemInfo = null;

    #endregion


    #region "Properties"

    /// <summary>
    /// Current element id.
    /// </summary>
    public int ElementID
    {
        get
        {
            return this.mElementID;
        }
        set
        {
            this.mElementID = value;
        }
    }


    /// <summary>
    /// Current resource id.
    /// </summary>
    public int ResourceID
    {
        get
        {
            return this.mResourceID;
        }
        set
        {
            this.mResourceID = value;
        }
    }


    /// <summary>
    /// Current parent id.
    /// </summary>
    public int ParentID
    {
        get
        {
            return this.mParentID;
        }
        set
        {
            this.mParentID = value;
        }
    }

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        this.plcCMSVersion.Visible = SettingsKeyProvider.DevelopmentMode;

        if (ElementID > 0)
        {
            elemInfo = UIElementInfoProvider.GetUIElementInfo(ElementID);
            if ((!RequestHelper.IsPostBack()) && (elemInfo != null))
            {
                // Load selected element
                this.txtDisplayName.Text = elemInfo.ElementDisplayName;
                this.txtName.Text = elemInfo.ElementName;
                this.chkCustom.Checked = elemInfo.ElementIsCustom;
                this.txtCaption.Text = elemInfo.ElementCaption;
                this.txtTargetURL.Text = elemInfo.ElementTargetURL;
                this.txtIconPath.Text = elemInfo.ElementIconPath;
                this.txtDescription.Text = elemInfo.ElementDescription;
                this.radRegular.Checked = (elemInfo.ElementSize == UIElementSizeEnum.Regular);
                this.radLarge.Checked = (elemInfo.ElementSize == UIElementSizeEnum.Large);
                this.elemSelector.ModuleID = elemInfo.ElementResourceID;
                // Exclude current element and childs from dropdown list
                this.elemSelector.WhereCondition = "ElementIDPath NOT LIKE N'" + elemInfo.ElementIDPath + "%'";

                if (this.plcCMSVersion.Visible)
                {
                    this.versionSelector.Value = elemInfo.ElementFromVersion;
                }
            }
        }
        else
        {
            // Hide parent selector for creating new element
            this.plcParentElem.Visible = false;
            this.elemSelector.StopProcessing = true;

            // Set Is custom to default value
            if (!URLHelper.IsPostback())
            {
                this.chkCustom.Checked = (SettingsKeyProvider.DevelopmentMode ? false : true);
            }
        }
        this.rfvDisplayName.ErrorMessage = GetString("general.requiresdisplayname");
        this.rfvName.ErrorMessage = GetString("general.requirescodename");

        if (!URLHelper.IsPostback())
        {
            if (QueryHelper.GetBoolean("saved", false))
            {
                this.lblInfo.ResourceString = "general.changessaved";
                this.lblInfo.Visible = true;
            }
        }
    }


    protected void Page_PreRender(object sender, EventArgs e)
    {
        if ((!RequestHelper.IsPostBack()) && (elemInfo != null))
        {
            this.elemSelector.ElementID = elemInfo.ElementParentID;
        }
    }


    protected void btnOk_Click(object sender, EventArgs e)
    {
        string result = ValidateForm();
        if (String.IsNullOrEmpty(result))
        {
            if (ElementID == 0)
            {
                // Create new UI element info
                elemInfo = new UIElementInfo();
                elemInfo.ElementResourceID = ResourceID;
                elemInfo.ElementParentID = ParentID;
                elemInfo.ElementFromVersion = string.Empty;
                elemInfo.ElementOrder = UIElementInfoProvider.GetLastElementOrder(ParentID) + 1;
            }
            else
            {
                // If parent changed set last order
                if (elemSelector.ElementID != elemInfo.ElementParentID)
                {
                    elemInfo.ElementOrder = UIElementInfoProvider.GetLastElementOrder(elemSelector.ElementID) + 1;
                }
                elemInfo.ElementParentID = this.elemSelector.ElementID;
            }
            elemInfo.ElementDisplayName = this.txtDisplayName.Text.Trim();
            elemInfo.ElementName = this.txtName.Text.Trim();
            elemInfo.ElementIsCustom = this.chkCustom.Checked;
            elemInfo.ElementCaption = this.txtCaption.Text.Trim();
            elemInfo.ElementDisplayName = this.txtDisplayName.Text.Trim();
            elemInfo.ElementTargetURL = this.txtTargetURL.Text.Trim();
            elemInfo.ElementIconPath = this.txtIconPath.Text.Trim();
            elemInfo.ElementDescription = this.txtDescription.Text.Trim();
            elemInfo.ElementSize = (this.radRegular.Checked ? UIElementSizeEnum.Regular : UIElementSizeEnum.Large);
            if (plcCMSVersion.Visible)
            {
                elemInfo.ElementFromVersion = this.versionSelector.Value.ToString();
            }

            // Set UI element info
            UIElementInfoProvider.SetUIElementInfo(elemInfo);

            // Get updated element info (ElementIDPath was changed in DB)
            elemInfo = UIElementInfoProvider.GetUIElementInfo(elemInfo.ElementID);

            // Reload header and content after save
            StringBuilder sb = new StringBuilder();
            sb.Append("if (window.parent != null) {");
            if (ElementID == 0)
            {
                sb.Append("if (window.parent.parent.frames['uicontent'] != null) {");
                sb.Append("window.parent.parent.frames['uicontent'].location = '" + ResolveUrl("~/CMSModules/Modules/Pages/Development/Module_UI_EditFrameset.aspx") + "?moduleID=" + ResourceID + "&elementId=" + elemInfo.ElementID + "&parentId=" + elemInfo.ElementParentID + "&saved=1';");
                sb.Append("}");
                sb.Append("if (window.parent.frames['uicontent'] != null) {");
                sb.Append("window.parent.frames['uicontent'].location = '" + ResolveUrl("~/CMSModules/Modules/Pages/Development/Module_UI_EditFrameset.aspx") + "?moduleID=" + ResourceID + "&elementId=" + elemInfo.ElementID + "&parentId=" + elemInfo.ElementParentID + "&saved=1';");
                sb.Append("}");
            }
            else
            {
                sb.Append("if (window.parent.parent.frames['header'] != null) {");
                sb.Append("window.parent.parent.frames['header'].location = '" + ResolveUrl("~/CMSModules/Modules/Pages/Development/Module_UI_Header.aspx") + "?moduleID=" + ResourceID + "&elementId=" + elemInfo.ElementID + "&parentId=" + elemInfo.ElementParentID + "';");
                sb.Append("}");
                sb.Append("if (window.parent.frames['header'] != null) {");
                sb.Append("window.parent.frames['header'].location = '" + ResolveUrl("~/CMSModules/Modules/Pages/Development/Module_UI_Header.aspx") + "?moduleID=" + ResourceID + "&elementId=" + elemInfo.ElementID + "&parentId=" + elemInfo.ElementParentID + "';");
                sb.Append("}");
            }
            sb.Append("if (window.parent.parent.frames['tree'] != null) {");
            sb.Append("window.parent.parent.frames['tree'].location = '" + ResolveUrl("~/CMSModules/Modules/Pages/Development/Module_UI_Tree.aspx") + "?moduleID=" + ResourceID + "&path=" + elemInfo.ElementIDPath + "&elementId=" + elemInfo.ElementID + "&parentId=" + elemInfo.ElementParentID + "';");
            sb.Append("}");
            sb.Append("if (window.parent.frames['tree'] != null) {");
            sb.Append("window.parent.frames['tree'].location = '" + ResolveUrl("~/CMSModules/Modules/Pages/Development/Module_UI_Tree.aspx") + "?moduleID=" + ResourceID + "&path=" + elemInfo.ElementIDPath + "&elementId=" + elemInfo.ElementID + "&parentId=" + elemInfo.ElementParentID + "';");
            sb.Append("}");
            sb.Append("}");

            this.lblInfo.ResourceString = "general.changessaved";
            this.lblInfo.Visible = true;

            this.ltlScript.Text = ScriptHelper.GetScript(sb.ToString());
        }
        else
        {
            lblError.Visible = true;
            lblError.Text = result;
        }
    }

    #endregion


    #region "Private methods"

    private string ValidateForm()
    {
        // Finds whether required fields are not empty or codename is in requested form
        string result = new Validator()
            .NotEmpty(txtDisplayName.Text.Trim(), GetString("general.requiresdisplayname"))
            .NotEmpty(txtName.Text.Trim(), GetString("general.requirescodename"))
            .IsCodeName(txtName.Text.Trim(), GetString("general.invalidcodename"))
            .Result;

        if(String.IsNullOrEmpty(result) && plcCMSVersion.Visible)
        {
            if (string.IsNullOrEmpty(versionSelector.Value.ToString()))
            {
                result = GetString("general.requirescmsversion");
            }
        }

        if (String.IsNullOrEmpty(result))
        {
            // Check if code name is unique
            UIElementInfo elemInfo = UIElementInfoProvider.GetUIElementInfo(ResourceID, txtName.Text.Trim());
            if ((elemInfo != null) && (elemInfo.ElementID != this.ElementID))
            {
                result = GetString("general.uniquecodenameerror");
            }
        }

        return result;
    }

    #endregion
}
