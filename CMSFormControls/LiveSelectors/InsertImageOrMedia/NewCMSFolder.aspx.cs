using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.GlobalHelper;

public partial class CMSFormControls_LiveSelectors_InsertImageOrMedia_NewCMSFolder : CMSLiveModalPage
{
    #region "Private variables"

    private int mParentNodeId = 0;        

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        if (QueryHelper.ValidateHash("hash"))
        {
            // Initialize controls
            SetupControls();
        }
        else
        {
            this.createFolder.Visible = false;
            string url = ResolveUrl("~/CMSMessages/Error.aspx?title=" + GetString("dialogs.badhashtitle") + "&text=" + GetString("dialogs.badhashtext") + "&cancel=1");
            ScriptHelper.RegisterStartupScript(Page, typeof(string), "redirect", ScriptHelper.GetScript("if (window.parent != null) { window.parent.location = '" + url + "' }"));
        }
    }


    /// <summary>
    /// Initializes controls.
    /// </summary>
    private void SetupControls()
    {
        // Get data from query string
        this.mParentNodeId = QueryHelper.GetInteger("nodeid", 0);

        this.createFolder.OnFolderChange += new CMSModules_Content_Controls_Dialogs_LinkMediaSelector_NewFolder.OnFolderChangeEventHandler(createFolder_OnFolderChange);
        this.createFolder.CancelClick += new CMSModules_Content_Controls_Dialogs_LinkMediaSelector_NewFolder.OnCancelClickEventHandler(createFolder_CancelClick);
        this.createFolder.IsLiveSite = true;

        // Initialize information on library
        this.createFolder.ParentNodeID = this.mParentNodeId;

        this.Page.Header.Title = GetString("dialogs.newfoldertitle");

        this.CurrentMaster.Title.TitleText = GetString("dialogs.folder.new");
        this.CurrentMaster.Title.TitleImage = ResolveUrl(GetImageUrl("CMSModules/CMS_Content/Dialogs/newfolder.png"));
    }


    #region "Event handlers"

    protected void createFolder_CancelClick()
    {
        this.ltlScript.Text = ScriptHelper.GetScript("wopener.SetAction('cancelfolder', ''); wopener.RaiseHiddenPostBack(); window.close();");
    }


    protected void createFolder_OnFolderChange(int nodeToSelect)
    {
        this.ltlScript.Text = ScriptHelper.GetScript("wopener.SetAction('newfolder', '" + nodeToSelect.ToString() + "'); wopener.RaiseHiddenPostBack(); window.close();");
    }

    #endregion
}
