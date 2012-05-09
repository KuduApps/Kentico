using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using CMS.GlobalHelper;
using CMS.TreeEngine;
using CMS.CMSHelper;
using CMS.ExtendedControls;
using CMS.FormEngine;
using CMS.SiteProvider;

using TreeNode = CMS.TreeEngine.TreeNode;

public partial class CMSModules_Content_FormControls_Documents_SelectDocument : CMS.FormControls.FormEngineUserControl
{
    #region "Variables"

    private bool mEnableSiteSelection = false;
    private DialogConfiguration mConfig = null;
    private TreeProvider mTreeProvider = null;

    #endregion


    #region "Private properties"

    /// <summary>
    /// Returns TreeProvider object.
    /// </summary>
    private TreeProvider TreeProvider
    {
        get
        {
            if (this.mTreeProvider == null)
            {
                mTreeProvider = new TreeProvider(CMSContext.CurrentUser);
            }
            return this.mTreeProvider;
        }
    }


    /// <summary>
    /// Gets the configuration for Copy and Move dialog.
    /// </summary>
    private DialogConfiguration Config
    {
        get
        {
            if (this.mConfig == null)
            {
                this.mConfig = new DialogConfiguration();
                this.mConfig.HideLibraries = true;
                this.mConfig.HideAnchor = true;
                this.mConfig.HideAttachments = true;
                this.mConfig.HideContent = false;
                this.mConfig.HideEmail = true;
                this.mConfig.HideLibraries = true;
                this.mConfig.HideWeb = true;
                this.mConfig.EditorClientID = this.txtGuid.ClientID;
                this.mConfig.ContentSelectedSite = CMSContext.CurrentSiteName;
                this.mConfig.OutputFormat = OutputFormatEnum.Custom;
                this.mConfig.CustomFormatCode = "selectpath";
                this.mConfig.SelectableContent = SelectableContentEnum.AllContent;
            }
            return this.mConfig;
        }
    }

    #endregion


    #region "Public properties"

    /// <summary>
    /// Gets or sets the enabled state of the control.
    /// </summary>
    public override bool Enabled
    {
        get
        {
            return base.Enabled;
        }
        set
        {
            base.Enabled = value;
            this.txtName.Enabled = value;
            this.btnSelect.Enabled = value;
            this.btnClear.Enabled = value;
        }
    }


    /// <summary>
    /// Gets or sets field value.
    /// </summary>
    public override object Value
    {
        get
        {
            return txtGuid.Text.Trim();
        }
        set
        {
            if (value != null)
            {
                txtGuid.Text = value.ToString();
                SetAliasPath(value, FieldIsInteger());
            }
        }
    }


    /// <summary>
    /// Gets ClientID of the textbox with path.
    /// </summary>
    public override string ValueElementID
    {
        get
        {
            return this.txtName.ClientID;
        }
    }


    /// <summary>
    /// Determines whether to enable site selection or not.
    /// </summary>
    public bool EnableSiteSelection
    {
        get
        {
            return this.mEnableSiteSelection;
        }
        set
        {
            this.mEnableSiteSelection = value;
            this.Config.ContentSites = (value ? AvailableSitesEnum.All : AvailableSitesEnum.OnlyCurrentSite);
        }
    }


    /// <summary>
    /// Gets or sets the content starting path.
    /// </summary>
    public string ContentStartingPath
    {
        get
        {
            return this.Config.ContentStartingPath;
        }
        set
        {
            this.Config.ContentStartingPath = value;
        }
    }

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Hide guid textbox
        this.txtGuid.Attributes.Add("style", "display: none");

        ScriptHelper.RegisterDialogScript(this.Page);
        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "ClearDocument", ScriptHelper.GetScript("function ClearDocument(txtClientID, hiddenClientId){ document.getElementById(txtClientID).value = ''; document.getElementById(hiddenClientId).value=''; }"));

        this.btnSelect.Text = GetString("general.select");
        this.btnSelect.OnClientClick = "modalDialog('" + GetDialogUrl() + "','PathSelection', '90%', '85%'); return false;";

        this.btnClear.Text = GetString("FormControls_SelectDocument.btnClear");
        this.btnClear.OnClientClick = "ClearDocument('" + txtName.ClientID + "', '" + txtGuid.ClientID + "'); return false;";

        this.txtName.Attributes.Add("readonly", "readonly");
        this.txtGuid.TextChanged += new EventHandler(txtGuid_TextChanged);
    }


    /// <summary>
    /// Returns TRUE if parent form field is integer.
    /// </summary>
    /// <returns></returns>
    private bool FieldIsInteger()
    {
        return ((this.FieldInfo != null) && ((this.FieldInfo.DataType == FormFieldDataTypeEnum.Integer) || (this.FieldInfo.DataType == FormFieldDataTypeEnum.LongInteger)));
    }


    void txtGuid_TextChanged(object sender, EventArgs e)
    {
        int nodeId = ValidationHelper.GetInteger(txtGuid.Text, 0);

        if (FieldIsInteger())
        {
            txtName.Text = GetNodeName(nodeId);
            return;
        }

        if (nodeId > 0)
        {
            TreeNode node = this.TreeProvider.SelectSingleNode(nodeId, TreeProvider.ALL_CULTURES, true);
            if (node != null)
            {
                string site = (node.NodeSiteID != CMSContext.CurrentSiteID ? ";" + node.NodeSiteName : "");
                txtName.Text = node.NodeAliasPath;
                txtGuid.Text = node.NodeGUID.ToString() + site;
            }
        }
    }


    /// <summary>
    /// Clears selected value.
    /// </summary>
    public void Clear()
    {
        txtGuid.Text = String.Empty;
        txtName.Text = String.Empty;
    }
    
    #endregion


    #region "Private methods"

    private void SetAliasPath(object valueObj, bool isNodeId)
    {
        if (valueObj != null)
        {
            if (!isNodeId)
            {
                string[] split = valueObj.ToString().Split(';');
                string siteName = null;
                // Check if site name is presented in value
                if (split.Length > 1)
                {
                    siteName = split[1];
                }
                else
                {
                    if ((Form != null) && (Form.EditedObject != null))
                    {
                        TreeNode editedNode = Form.EditedObject as TreeNode;
                        if (editedNode != null)
                        {
                            int linkedSiteId = editedNode.OriginalNodeSiteID;
                            if ((linkedSiteId > 0) && (linkedSiteId != CMSContext.CurrentSiteID))
                            {
                                siteName = SiteInfoProvider.GetSiteName(linkedSiteId);
                            }
                        }
                    }
                }

                if (ValidationHelper.GetGuid(split[0], Guid.Empty) != Guid.Empty)
                {
                    this.txtName.Text = GetNodeName(split[0], siteName);
                }
            }
            else
            {
                this.txtName.Text = GetNodeName(ValidationHelper.GetInteger(valueObj, 0));
            }
        }
    }


    /// <summary>
    /// Returns Correct URL of the copy or move dialog.
    /// </summary>
    private string GetDialogUrl()
    {
        string url = CMSDialogHelper.GetDialogUrl(this.Config, this.IsLiveSite, false, null, false);
        
        url = URLHelper.RemoveParameterFromUrl(url, "hash");
        url = URLHelper.AddParameterToUrl(url, "selectionmode", "single");
        url = URLHelper.AddParameterToUrl(url, "hash", QueryHelper.GetHash(url));
        url = URLHelper.EncodeQueryString(url);
        return url;
    }


    /// <summary>
    /// Gets node name from guid.
    /// </summary>
    /// <param name="objGuid">Guid object</param>
    /// <param name="siteName">Site name</param>
    private string GetNodeName(string objGuid, string siteName)
    {
        Guid nodeGuid = ValidationHelper.GetGuid(objGuid, Guid.Empty);
        if (nodeGuid != Guid.Empty)
        {
            if (String.IsNullOrEmpty(siteName))
            {
                siteName = CMSContext.CurrentSiteName;
            }
            TreeNode node = this.TreeProvider.SelectSingleNode(nodeGuid, TreeProvider.ALL_CULTURES, siteName);
            if (node != null)
            {
                return node.NodeAliasPath;
            }
        }

        return "";
    }


    /// <summary>
    /// Gets node name from node ID.
    /// </summary>
    /// <param name="nodeId">Node ID</param>
    private string GetNodeName(int nodeId)
    {
        if (nodeId > 0)
        {
            TreeNode node = this.TreeProvider.SelectSingleNode(nodeId, TreeProvider.ALL_CULTURES, true);
            if (node != null)
            {
                return node.NodeAliasPath;
            }
        }
        return "";
    }


    /// <summary>
    /// Returns WHERE condition for selected form.
    /// </summary>
    public override string GetWhereCondition()
    {
        // Return correct WHERE condition for integer if none value is selected
        if ((this.FieldInfo != null) && ((this.FieldInfo.DataType == FormFieldDataTypeEnum.Integer) || (this.FieldInfo.DataType == FormFieldDataTypeEnum.LongInteger)))
        {
            int id = ValidationHelper.GetInteger(this.Value, 0);
            if (id > 0)
            {
                return base.GetWhereCondition();
            }
        }
        return null;
    }

    #endregion
}
