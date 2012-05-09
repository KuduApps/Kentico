using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.OnlineMarketing;
using CMS.SettingsProvider;
using CMS.WebAnalytics;

public partial class CMSModules_OnlineMarketing_Pages_Tools_ABVariant_Edit : CMSABTestPage
{
    #region "Methods"

    protected override void OnInit(EventArgs e)
    {
        // Get the ID from query string variantId
        editElem.ABVariantID = QueryHelper.GetInteger("variantId", 0);
        editElem.OnSaved += new EventHandler(editElem_OnSaved);

        base.OnInit(e);
    }


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

        PageTitle title = this.CurrentMaster.Title;
        int testID = QueryHelper.GetInteger("abTestID", 0);
        editElem.TestID = testID;

        // Prepare the header
        string name = "";
        title.HelpTopicName = "variant_edit";
        if (editElem.VariantObj != null)
        {
            name = editElem.VariantObj.ABVariantDisplayName;
            title.TitleImage = GetImageUrl("Objects/CMS_Variant/object.png");
        }
        else
        {
            name = GetString("abtesting.variant.new");
            title.TitleImage = GetImageUrl("Objects/CMS_Variant/new.png");
        }

        // Prepare the breadcrumbs       
        string[,] breadcrumbs = new string[2, 3];
        breadcrumbs[0, 0] = GetString("abtesting.variant.list");
        breadcrumbs[0, 1] = "~/CMSModules/OnlineMarketing/Pages/Tools/ABVariant/List.aspx?abTestID=" + testID;
        breadcrumbs[1, 0] = name;

        // Set the title
        title.Breadcrumbs = breadcrumbs;
    }


    protected void editElem_OnSaved(object sender, EventArgs e)
    {
        URLHelper.Redirect("Edit.aspx?saved=1&abTestID=" + QueryHelper.GetInteger("abTestID", 0) + "&variantId=" + editElem.ItemID);
    }

    #endregion
}