using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CMS.UIControls;
using CMS.OnlineMarketing;
using CMS.GlobalHelper;

// Edited object
[EditedObject(OnlineMarketingObjectType.SCORERULE, "ruleid")]

[Actions(1)]
[Action(0, "Objects/OM_Score/rule_add.png", "om.score.newrule", "Tab_Rules_Edit.aspx")]
public partial class CMSModules_Scoring_Pages_Tab_Rules : CMSRulePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Register script for unimenu button selection
        CMSDeskPage.AddMenuButtonSelectScript(this, "Scoring", null, "menu");
        CurrentMaster.HeaderActions.Actions[0, 3] += URLHelper.Url.Query;
    }
}