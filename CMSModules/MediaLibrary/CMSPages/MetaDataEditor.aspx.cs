using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.SiteProvider;
using CMS.CMSHelper;
using CMS.SettingsProvider;

public partial class CMSModules_MediaLibrary_CMSPages_MetaDataEditor : CMSLiveModalPage
{
    #region "Properties"

    /// <summary>
    /// Returns the site name from query string 'sitename' or 'siteid' if present, otherwise CMSContext.CurrentSiteName.
    /// </summary>
    protected new string CurrentSiteName
    {
        get
        {
            if (mCurrentSiteName == null)
            {
                mCurrentSiteName = QueryHelper.GetString("sitename", CMSContext.CurrentSiteName);

                int siteId = QueryHelper.GetInteger("siteid", 0);

                SiteInfo site = SiteInfoProvider.GetSiteInfo(siteId);
                if (site != null)
                {
                    mCurrentSiteName = site.SiteName;
                }
            }
            return mCurrentSiteName;
        }
    }

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Initialize modal page
        this.RegisterEscScript();

        if (QueryHelper.ValidateHash("hash"))
        {
            Guid guid = QueryHelper.GetGuid("mediafileguid", Guid.Empty);
            string title = GetString("general.editmetadata");
            Page.Title = title;
            CurrentMaster.Title.TitleText = title;
            // Default image
            CurrentMaster.Title.TitleImage = GetImageUrl("Design/Controls/MetaDataEditor/Title.png");

            CurrentMaster.Body.Attributes["onbeforeunload"] = "if (wopener.EditDialogStateUpdate) { wopener.EditDialogStateUpdate('false'); }";
            CurrentMaster.Body.Attributes["onunload"] = "if (wopener.imageEdit_Refresh) { wopener.imageEdit_Refresh('" + ScriptHelper.GetString(guid.ToString(), false) + "|" + ScriptHelper.GetString(CurrentSiteName, false) + "'); }";

            btnSave.Click += new EventHandler(btnSave_Click);

            AddNoCacheTag();

            // Set metadata editor properties
            metaDataEditor.ObjectGuid = guid;
            metaDataEditor.ObjectType = PredefinedObjectType.MEDIAFILE;
            metaDataEditor.SiteName = CurrentSiteName;
            metaDataEditor.GetObjectExtension += new CMSModules_MediaLibrary_Controls_MediaLibrary_MediaFileMetaDataEditor.OnGetObjectExtension(metaDataEditor_GetObjectExtension);
        }
        else
        {
            // Hide all controls
            metaDataEditor.Visible = false;
            btnSave.Visible = false;

            string url = ResolveUrl("~/CMSMessages/Error.aspx?title=" + GetString("dialogs.badhashtitle") + "&text=" + GetString("dialogs.badhashtext") + "&cancel=1");
            ltlScript.Text = ScriptHelper.GetScript("window.location = '" + url + "';");
        }
    }


    /// <summary>
    /// Sets title of image according to file extension.
    /// </summary>
    /// <param name="extension">File extension</param>
    private void metaDataEditor_GetObjectExtension(string extension)
    {
        CurrentMaster.Title.TitleImage = GetFileIconUrl(extension, null);
    }


    /// <summary>
    /// Save meta data of media file.
    /// </summary>
    /// <param name="sender">Sender</param>
    /// <param name="e">Argument</param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (metaDataEditor.SaveMetadata())
        {
            string script = "close();)";
            ltlScript.Text = ScriptHelper.GetScript(script);
        }
    }

    #endregion
}