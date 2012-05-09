using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.GlobalHelper;
using CMS.OnlineMarketing;
using CMS.UIControls;

// Actions
[Actions(1)]
[Action(0, "Objects/OM_Score/add.png", "om.score.new", "New.aspx")]

// Title
[Title("Objects/OM_Score/object.png", "om.score.list", "scoring_list")]

public partial class CMSModules_Scoring_Pages_List : CMSScorePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Register script for unimenu button selection
        CMSDeskPage.AddMenuButtonSelectScript(this, "Scoring", null, "menu");
    }


    protected void Page_PreRender(object sender, EventArgs e)
    {
        // Check permissions to create new record
        CurrentMaster.HeaderActions.Enabled = CurrentUser.IsAuthorizedPerResource("cms.scoring", "modify", CurrentSiteName);
    }
}