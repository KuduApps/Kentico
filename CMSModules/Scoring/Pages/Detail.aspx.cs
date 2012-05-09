using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.OnlineMarketing;

public partial class CMSModules_Scoring_Pages_Detail : CMSScorePage
{
    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        RegisterEscScript();

        frameContent.Attributes.Add("src", "../Pages/Frameset.aspx" + URLHelper.Url.Query + "&dialogmode=1");
        frameFooter.Attributes.Add("src", "../Pages/DetailFooter.aspx" + URLHelper.Url.Query);
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        RegisterModalPageScripts();
    }

    #endregion
}