using System;
using System.Web.UI;
using System.Text;
using System.Web.UI.WebControls;

using CMS.GlobalHelper;
using CMS.ExtendedControls;
using CMS.CMSHelper;
using CMS.SiteProvider;
using CMS.FormControls;
using CMS.TreeEngine;

using TreeNode = CMS.TreeEngine.TreeNode;

public partial class CMSModules_Content_FormControls_Documents_SelectSinglePath : FormEngineUserControl, ICallbackEventHandler
{
    #region "Variables & constants"

    private int mSiteId = 0;
    private DialogConfiguration mConfig = null;
    private TreeProvider mTreeProvider = null;
    private string callbackResult = string.Empty;
    private int nodeIdFromPath = 0;
    private const string separator = "##SEP##";

    #endregion


    #region "Private properties"

    /// <summary>
    /// Returns TreeProvider object.
    /// </summary>
    private TreeProvider TreeProvider
    {
        get
        {
            return mTreeProvider ?? (mTreeProvider = new TreeProvider(CMSContext.CurrentUser));
        }
    }


    /// <summary>
    /// Gets the configuration for Copy and Move dialog.
    /// </summary>
    private DialogConfiguration Config
    {
        get
        {
            if (mConfig == null)
            {
                mConfig = new DialogConfiguration();
                mConfig.HideLibraries = true;
                mConfig.ContentSelectedSite = CMSContext.CurrentSiteName;
                mConfig.HideAnchor = true;
                mConfig.HideAttachments = true;
                mConfig.HideContent = false;
                mConfig.HideEmail = true;
                mConfig.HideLibraries = true;
                mConfig.HideWeb = true;
                mConfig.EditorClientID = txtNodeId.ClientID;
                mConfig.OutputFormat = OutputFormatEnum.Custom;
                mConfig.CustomFormatCode = "selectpath";
                mConfig.SelectableContent = SelectableContentEnum.AllContent;
            }
            return mConfig;
        }
    }


    /// <summary>
    /// Gets the path text box.
    /// </summary>
    private TextBox PathTextBox
    {
        get
        {
            EnsureChildControls();
            return txtPath;
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
            PathTextBox.Enabled = value;
            btnSelectPath.Enabled = value;
        }
    }


    /// <summary>
    /// Gets or sets field value.
    /// </summary>
    public override object Value
    {
        get
        {
            return PathTextBox.Text;
        }
        set
        {
            PathTextBox.Text = ValidationHelper.GetString(value, null);
        }
    }


    /// <summary>
    /// Gets selected node id.
    /// </summary>
    public int NodeId
    {
        get
        {
            return ValidationHelper.GetInteger(txtNodeId.Text, 0);
        }
    }


    /// <summary>
    /// Gets ClientID of the textbox with path.
    /// </summary>
    public override string ValueElementID
    {
        get
        {
            return PathTextBox.ClientID;
        }
    }


    /// <summary>
    /// Gets or sets the ID of the site from which the path is selected.
    /// </summary>
    public int SiteID
    {
        get
        {
            return mSiteId;
        }
        set
        {
            mSiteId = value;
            if (value > 0)
            {
                Config.ContentSites = AvailableSitesEnum.OnlySingleSite;
                SiteInfo si = SiteInfoProvider.GetSiteInfo(value);
                if (si != null)
                {
                    Config.ContentSelectedSite = si.SiteName;
                }
            }
            else
            {
                Config.ContentSites = AvailableSitesEnum.All;
            }
        }
    }


    /// <summary>
    /// Returns name of site.
    /// </summary>
    private string SiteName
    {
        get
        {
            string siteName = Config.ContentSelectedSite;
            if (string.IsNullOrEmpty(siteName))
            {
                SiteInfo si = SiteInfoProvider.GetSiteInfo(SiteID);
                if (si != null)
                {
                    siteName = si.SiteName;
                }
            }
            if (string.IsNullOrEmpty(siteName))
            {
                siteName = CMSContext.CurrentSiteName;
            }
            return siteName;
        }
    }


    /// <summary>
    /// Determines whether to allow setting permissions for selected path.
    /// </summary>
    public bool AllowSetPermissions
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("AllowSetPermissions"), false); ;
        }
        set
        {
            SetValue("AllowSetPermissions", value);
        }
    }

    #endregion


    #region "Page events"

    protected override void EnsureChildControls()
    {
        base.EnsureChildControls();
        if (txtPath == null)
        {
            pnlUpdate.LoadContainer();
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        // Register JavaScripts
        ScriptHelper.RegisterDialogScript(Page);
        btnSelectPath.OnClientClick = "modalDialog('" + GetSelectionDialogUrl() + "','PathSelection', '90%', '85%'); return false;";
        txtNodeId.TextChanged += txtNodeId_TextChanged;

        if (AllowSetPermissions)
        {
            StringBuilder urlScript = new StringBuilder();
            urlScript.AppendLine("function PerformAction(content, context)");
            urlScript.AppendLine("{");
            urlScript.AppendLine("  var arr = content.split('" + separator + "');");
            urlScript.AppendLine("  if(arr[0] == '0')");
            urlScript.AppendLine("  {");
            urlScript.AppendLine("      alert(\"" + GetString("content.documentnotexists") + "\" + \": '\" + document.getElementById(\"" + PathTextBox.ClientID + "\").value + \"'\");");
            urlScript.AppendLine("  }");
            urlScript.AppendLine("  else");
            urlScript.AppendLine("  {");
            urlScript.AppendLine("      modalDialog(arr[1], 'SetPermissions', '605', '460')");
            urlScript.AppendLine("  }");
            urlScript.AppendLine("}");

            ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "GetPermissionsUrl", ScriptHelper.GetScript(urlScript.ToString()));

            string urlRef = Page.ClientScript.GetCallbackEventReference(this, "document.getElementById('" + PathTextBox.ClientID + "').value", "PerformAction", "'SetPermissionContext'") + "; return false;";
            btnSetPermissions.OnClientClick = urlRef;

            // Disable text box if there is no current document
            if (CMSContext.CurrentDocument == null)
            {
                StringBuilder textChanged = new StringBuilder();
                textChanged.AppendLine("function TextChanged_" + ClientID + "()");
                textChanged.AppendLine("{");
                textChanged.AppendLine("    var textElem = document.getElementById('" + PathTextBox.ClientID + "');");
                textChanged.AppendLine("    if(textElem.value == null || textElem.value == '')");
                textChanged.AppendLine("    {");
                textChanged.AppendLine("        BTN_Disable('" + btnSetPermissions.ClientID + "');");
                textChanged.AppendLine("    }");
                textChanged.AppendLine("    else");
                textChanged.AppendLine("    {");
                textChanged.AppendLine("        BTN_Enable('" + btnSetPermissions.ClientID + "');");
                textChanged.AppendLine("    }");
                textChanged.AppendLine("    setTimeout('TextChanged_" + ClientID + "()', 500);");
                textChanged.AppendLine("}");
                textChanged.AppendLine("setTimeout('TextChanged_" + ClientID + "()', 500);");

                ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "TextChanged" + ClientID, ScriptHelper.GetScript(textChanged.ToString()));
            }
        }

        // Set control visibility
        btnSetPermissions.Visible = AllowSetPermissions;
    }


    void txtNodeId_TextChanged(object sender, EventArgs e)
    {
        int nodeId = ValidationHelper.GetInteger(txtNodeId.Text, 0);
        if (nodeId > 0)
        {
            TreeNode node = this.TreeProvider.SelectSingleNode(nodeId);
            if (node != null)
            {
                SiteID = node.NodeSiteID;
                PathTextBox.Text = node.NodeAliasPath;

                // Raise change event
                RaiseOnChanged();
            }
        }
    }

    #endregion


    #region "Methods"

    /// <summary>
    /// Returns Correct URL of the path selection dialog.
    /// </summary>
    private string GetSelectionDialogUrl()
    {
        string url = CMSDialogHelper.GetDialogUrl(Config, IsLiveSite, false, null, false);

        url = URLHelper.RemoveParameterFromUrl(url, "hash");
        url = URLHelper.AddParameterToUrl(url, "selectionmode", "single");
        url = URLHelper.AddParameterToUrl(url, "hash", QueryHelper.GetHash(url));
        url = URLHelper.EncodeQueryString(url);
        return url;
    }


    /// <summary>
    /// Returns Correct URL of the 'Set permissions' dialog.
    /// </summary>
    private string GetPermissionsDialogUrl(string nodeAliasPath)
    {
        string url = ResolveUrl("~/CMSModules/Content/FormControls/Documents/ChangePermissions/ChangePermissions.aspx");
        // Use current document path if not set
        if (string.IsNullOrEmpty(nodeAliasPath) && (CMSContext.CurrentDocument != null))
        {
            nodeAliasPath = CMSContext.CurrentDocument.NodeAliasPath;
        }
        nodeIdFromPath = TreePathUtils.GetNodeIdByAliasPath(SiteName, CMSContext.ResolveCurrentPath(nodeAliasPath));
        url = URLHelper.AddParameterToUrl(url, "nodeid", nodeIdFromPath.ToString());
        url = URLHelper.AddParameterToUrl(url, "hash", QueryHelper.GetHash(url));
        return url;
    }

    #endregion


    #region "Callback handling"

    /// <summary>
    /// Raises the callback event.
    /// </summary>
    /// <param name="eventArgument">Event argument</param>
    public void RaiseCallbackEvent(string eventArgument)
    {
        callbackResult = GetPermissionsDialogUrl(eventArgument);
    }


    /// <summary>
    /// Returns the result of a callback.
    /// </summary>
    public string GetCallbackResult()
    {
        return nodeIdFromPath + separator + callbackResult;
    }

    #endregion
}
