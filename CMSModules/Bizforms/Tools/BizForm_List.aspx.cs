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
using CMS.FormEngine;
using CMS.SettingsProvider;
using CMS.LicenseProvider;
using CMS.CMSHelper;
using CMS.SiteProvider;
using CMS.UIControls;

public partial class CMSModules_BizForms_Tools_BizForm_List : CMSBizFormPage
{
    private CurrentUserInfo currentUser = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        currentUser = CMSContext.CurrentUser;

        if (currentUser == null)
        {
            return;
        }

        // Check 'ReadForm' permission
        if (!currentUser.IsAuthorizedPerResource("cms.form", "ReadForm"))
        {
            RedirectToCMSDeskAccessDenied("cms.form", "ReadForm");
        }

        if (DataHelper.GetNotEmpty(URLHelper.GetCurrentDomain(), "") != "")
        {
            LicenseHelper.CheckFeatureAndRedirect(URLHelper.GetCurrentDomain(), FeatureEnum.BizForms);
        }

        UniGridBizForms.OnAction += new OnActionEventHandler(UniGridBizForms_OnAction);
        UniGridBizForms.OnAfterRetrieveData += new OnAfterRetrieveData(uniGrid_OnAfterRetrieveData);
        UniGridBizForms.HideControlForZeroRows = false;
        UniGridBizForms.ZeroRowsText = GetString("general.nodatafound");
        UniGridBizForms.WhereCondition = "FormSiteID = " + CMSContext.CurrentSite.SiteID;


        // New item link
        string[,] actions = new string[1, 6];
        actions[0, 0] = HeaderActions.TYPE_HYPERLINK;
        actions[0, 1] = GetString("BizFormList.lnkNewBizForm");
        actions[0, 2] = null;
        actions[0, 3] = ResolveUrl("BizForm_New.aspx");
        actions[0, 4] = null;
        actions[0, 5] = GetImageUrl("Objects/CMS_Form/add.png");

        this.CurrentMaster.HeaderActions.Actions = actions;
        this.CurrentMaster.Title.TitleText = GetString("BizFormList.TitleText");
        this.CurrentMaster.Title.TitleImage = GetImageUrl("Objects/CMS_Form/object.png");
        this.CurrentMaster.Title.HelpTopicName = "bizforms";
    }


    private DataSet uniGrid_OnAfterRetrieveData(DataSet ds)
    {
        if (!DataHelper.DataSourceIsEmpty(ds))
        {
            int i = 0;
            while (i < ds.Tables[0].Rows.Count)
            {
                BizFormInfo form = new BizFormInfo(ds.Tables[0].Rows[i]);
                if (!form.IsFormAllowedForUser(currentUser, CMSContext.CurrentSiteName))
                {
                    ds.Tables[0].Rows.RemoveAt(i);
                }
                else
                {
                    i++;
                }
            }
        }

        return ds;
    }


    protected void UniGridBizForms_OnAction(string actionName, object actionArgument)
    {
        if (DataHelper.GetNotEmpty(actionName, String.Empty) == "edit")
        {
            URLHelper.Redirect("BizForm_Frameset.aspx?formid=" + Convert.ToString(actionArgument));
        }
        if (DataHelper.GetNotEmpty(actionName, "") == "delete")
        {
            // Check 'DeleteFormAndData' permission
            if (!currentUser.IsAuthorizedPerResource("cms.form", "DeleteFormAndData"))
            {
                RedirectToCMSDeskAccessDenied("cms.form", "DeleteFormAndData");
            }

            // Delete Bizform
            BizFormInfoProvider.DeleteBizFormInfo(ValidationHelper.GetInteger(actionArgument, 0));
        }
    }
}
