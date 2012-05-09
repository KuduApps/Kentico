using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.UIControls;
using CMS.SiteProvider;
using CMS.FormEngine;
using CMS.OnlineMarketing;

[Security(Resource = "CMS.ContactManagement", Permission = "ReadActivities")]
public partial class CMSModules_ContactManagement_Controls_UI_ActivityDetails_BizFormDetails : CMSModalPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Check permissions
        if (ActivityHelper.AuthorizedReadActivity(CMSContext.CurrentSiteID, true))
        {
            if (!QueryHelper.ValidateHash("hash"))
            {
                return;
            }

            int bizId = QueryHelper.GetInteger("bizid", 0);
            int recId = QueryHelper.GetInteger("recid", 0);

            if ((bizId > 0) && (recId > 0))
            {
                BizFormInfo bfi = BizFormInfoProvider.GetBizFormInfo(bizId);

                if (bfi == null)
                {
                    return;
                }

                bizRecord.ItemID = recId;
                bizRecord.SiteName = SiteInfoProvider.GetSiteName(bfi.FormSiteID);
                bizRecord.FormName = bfi.FormName;
            }

            CurrentMaster.Title.TitleText = GetString("om.activitydetals.viewrecorddetail");
            CurrentMaster.Title.TitleImage = GetImagePath("Objects/OM_Activity/object.png");
        }
    }


    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (bizRecord.BasicForm != null)
        {
            bizRecord.BasicForm.SubmitButton.Visible = false;
        }
    }
}
