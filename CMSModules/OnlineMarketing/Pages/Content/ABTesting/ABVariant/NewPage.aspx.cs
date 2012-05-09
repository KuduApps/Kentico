using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.ExtendedControls;
using CMS.CMSHelper;
using CMS.WebAnalytics;
using CMS.SettingsProvider;
using CMS.OnlineMarketing;

public partial class CMSModules_OnlineMarketing_Pages_Content_ABTesting_ABVariant_NewPage : CMSContentPage, IPostBackEventHandler
{
    #region "Page methods and events"

    /// <summary>
    /// Raises the <see cref="E:Init"/> event.
    /// </summary>
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        // Check module permissions
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("cms.ABTest", "Read"))
        {
            RedirectToAccessDenied(String.Format(GetString("general.permissionresource"), "read", "A/B testing"));
        }

        // Check UI Permissions
        if ((CMSContext.CurrentUser == null) || (!CMSContext.CurrentUser.IsAuthorizedPerUIElement("CMS.Content", "New.ABTestVariant")))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.Content", "New.ABTestVariant");
        }
    }


    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        String siteName = CMSContext.CurrentSiteName;

        // Display disabled information
        if (!AnalyticsHelper.AnalyticsEnabled(siteName))
        {
            this.pnlDisabled.Visible = true;
            this.lblDisabled.Text = GetString("WebAnalytics.Disabled") + "<br/>";
        }


        if (!ABTestInfoProvider.ABTestingEnabled(siteName))
        {
            this.pnlDisabled.Visible = true;
            this.lblABTestingDisabled.Text = GetString("abtesting.disabled");
        }

        ScriptHelper.RegisterProgress(Page);

        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "SaveDocument",
           ScriptHelper.GetScript("function SaveDocument(nodeId, createAnother) { " + ControlsHelper.GetPostBackEventReference(this, "#", false).Replace("'#'", "createAnother+''") + "}"));
    }

    #endregion


    #region "IPostBackEventHandler members"

    /// <summary>
    /// Raises event postback event.
    /// </summary>
    /// <param name="eventArgument"></param>
    public void RaisePostBackEvent(string eventArgument)
    {
        // Check the permissions
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("cms.abtest", "Manage"))
        {
            ucNewPage.ErrorLabel.Text = GetString("om.abtest.nomanagepermission");
            return;
        }

        bool createAnother = ValidationHelper.GetBoolean(eventArgument, false);

        // Create document
        int newNodeID = ucNewPage.Save(createAnother);
        if (newNodeID != 0)
        {
            // Refresh tree
            string script = null;
            if (createAnother)
            {
                int parentID = QueryHelper.GetInteger("nodeID", 0);
                if (parentID != 0)
                {
                    script = ScriptHelper.GetScript("parent.RefreshTree(" + parentID + ", " + parentID + "); parent.CreateAnother();");
                }
            }
            else
            {
                script = ScriptHelper.GetScript("parent.RefreshTree(" + newNodeID + ", " + newNodeID + ");parent.SelectNode(" + newNodeID + ");");
            }
            ScriptHelper.RegisterClientScriptBlock(Page, typeof(string), "Refresh", script);
        }
    }

    #endregion
}
