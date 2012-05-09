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

using CMS.FormControls;
using CMS.GlobalHelper;
using CMS.SiteProvider;

public partial class CMSModules_Badges_FormControls_ViewBadgeImage : FormEngineUserControl
{
    private int mValue;

    #region "Public properties"

    /// <summary>
    /// Gets or sets field value.
    /// </summary>
    public override object Value
    {
        get
        {
            return mValue;
        }
        set
        {
            mValue = ValidationHelper.GetInteger(value, 0);
        }
    }

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        if (mValue > 0)
        {
            BadgeInfo bi = BadgeInfoProvider.GetBadgeInfo(mValue);
            if (bi != null)
            {
                string displayName = HTMLHelper.HTMLEncode(ResHelper.LocalizeString(bi.BadgeDisplayName));
                imgBadge.ImageUrl = ResolveUrl(HTMLHelper.HTMLEncode(bi.BadgeImageURL));
                imgBadge.AlternateText = displayName;
                imgBadge.Attributes.Add("title", displayName);
                // Add style information to the image - limit size
                imgBadge.Attributes.Add("style","border-width:0px;");
                return;
            }
        }

        imgBadge.Visible = false;
    }

    #endregion
}
