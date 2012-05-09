using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.OnlineMarketing;
using CMS.UIControls;

// Title
[Title("Objects/OM_Score/new.png", "om.score.new", "scoring_new")]

// Breadcrumbs
[Breadcrumbs(2)]
[Breadcrumb(0, "om.score.list", "~/CMSModules/Scoring/Pages/List.aspx", null)]
[Breadcrumb(1, "om.score.new")]

public partial class CMSModules_Scoring_Pages_New : CMSScorePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }
}