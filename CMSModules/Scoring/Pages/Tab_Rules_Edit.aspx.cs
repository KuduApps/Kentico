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

// Breadcrumbs
[Breadcrumbs(2)]
[Breadcrumb(0, "om.score.rulelist", "~/CMSModules/Scoring/Pages/Tab_Rules.aspx", null)]
[Breadcrumb(1, "om.score.newrule", NewObject = true)]
[Breadcrumb(1, Text = "{%EditedObject.DisplayName%}", ExistingObject = true)]

// Help
[Help("scoringrule_new", "helptopic")]
public partial class CMSModules_Scoring_Pages_Tab_Rules_Edit : CMSRulePage
{
    bool refreshBreadcrumbs = false;


    protected void Page_Load(object sender, EventArgs e)
    {
        string scoreid = QueryHelper.GetString("scoreid", "0");

        // Preserve querystring
        CurrentMaster.Title.Breadcrumbs[0, 1] = URLHelper.AddParameterToUrl(CurrentMaster.Title.Breadcrumbs[0, 1], "scoreid", scoreid);

        editElem.UIFormControl.OnAfterSave += UIFormControl_OnAfterSave;
    }


    protected void UIFormControl_OnAfterSave(object sender, EventArgs e)
    {
        RuleInfo rule = (RuleInfo)EditedObject;
        refreshBreadcrumbs = (rule != null);
    }


    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (refreshBreadcrumbs)
        {
            // Update breadcrumbs
            CurrentMaster.Title.Breadcrumbs[1, 0] = ((RuleInfo)EditedObject).RuleDisplayName;
        }
    }
}