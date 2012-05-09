using System;

using CMS.UIControls;
using CMS.GlobalHelper;

public partial class CMSFormControls_Selectors_InsertImageOrMedia_NewCMSFolder : CMSModalPage
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
        mParentNodeId = QueryHelper.GetInteger("nodeid", 0);

        createFolder.OnFolderChange += createFolder_OnFolderChange;
        createFolder.CancelClick += createFolder_CancelClick;
        createFolder.IsLiveSite = false;

        // Initialize information on library
        createFolder.ParentNodeID = mParentNodeId;

        Page.Header.Title = GetString("dialogs.newfoldertitle");

        this.CurrentMaster.Title.TitleText = GetString("dialogs.folder.new");
        this.CurrentMaster.Title.TitleImage = ResolveUrl(GetImageUrl("CMSModules/CMS_Content/Dialogs/newfolder.png"));
    }


    #region "Event handlers"

    protected void createFolder_CancelClick()
    {
        ltlScript.Text = ScriptHelper.GetScript("wopener.SetAction('cancelfolder', ''); wopener.RaiseHiddenPostBack(); window.close();");
    }


    protected void createFolder_OnFolderChange(int nodeToSelect)
    {
        ltlScript.Text = ScriptHelper.GetScript("wopener.SetAction('newfolder', '" + nodeToSelect + "'); wopener.RaiseHiddenPostBack(); window.close();");
    }

    #endregion
}
