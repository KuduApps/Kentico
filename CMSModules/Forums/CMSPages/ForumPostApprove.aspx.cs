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

using CMS.Forums;
using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.CMSHelper;
using CMS.ExtendedControls;

public partial class CMSModules_Forums_CMSPages_ForumPostApprove : CMSForumsPage
{
    #region "Page events"

    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        // Setup the modal dialog
        SetCulture();
        RegisterEscScript();

        // Get post ID
        int postId = QueryHelper.GetInteger("postid", 0);

        // Set the post ID
        PostApprove.PostID = postId;
        PostApproveFooter.PostID = postId;

        // Page title
        this.CurrentMaster.Title.TitleText = GetString("forums_forumnewpost_header.preview");
        this.CurrentMaster.Title.TitleImage = GetImageUrl("Objects/Forums_ForumPost/object.png");
    }


    /// <summary>
    /// Raises the <see cref="E:PreRender"/> event.
    /// </summary>
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        // Setup the modal dialog
        RegisterModalPageScripts();
    }

    #endregion
}
