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
using CMS.DataEngine;
using CMS.SiteProvider;
using CMS.Forums;
using CMS.CMSHelper;
using CMS.LicenseProvider;
using CMS.UIControls;
using CMS.SettingsProvider;

public partial class CMSModules_Forums_Tools_Forums_Forum_General : CMSForumsPage
{
    #region "Variables"

    private int mForumId = 0;
    private bool changeMaster = false;

    #endregion


    #region "Public properties"

    /// <summary>
    /// Gets or sets the ID of the forum to edit.
    /// </summary>
    public int ForumID
    {
        get
        {
            return this.mForumId;
        }
        set
        {
            this.mForumId = value;
        }
    }

    #endregion


    protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);

        // External call
        changeMaster = QueryHelper.GetBoolean("changemaster", false);
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        int forumID = QueryHelper.GetInteger("forumid", 0);
        
        ForumContext.CheckSite(0, forumID, 0);

        this.forumEdit.ForumID = forumID;
        this.mForumId = forumID;
        this.forumEdit.OnSaved += new EventHandler(forumEdit_OnSaved);
        forumEdit.IsLiveSite = false;
    }


    protected void forumEdit_OnSaved(object sender, EventArgs e)
    {
        // Refresh tree if external parent
        if (changeMaster)
        {
            ForumInfo fi = ForumInfoProvider.GetForumInfo(this.mForumId);
            if (fi != null)
            {
                ltlScript.Text += ScriptHelper.GetScript("window.parent.parent.frames['tree'].RefreshNode(" + ScriptHelper.GetString(fi.ForumDisplayName) + ", '" + this.mForumId + "');");
            }
        }
    }
}
