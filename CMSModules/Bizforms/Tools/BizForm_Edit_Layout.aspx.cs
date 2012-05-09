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
using CMS.SettingsProvider;
using CMS.FormEngine;
using CMS.SiteProvider;
using CMS.CMSHelper;
using CMS.Synchronization;
using CMS.UIControls;

public partial class CMSModules_BizForms_Tools_BizForm_Edit_Layout : CMSBizFormPage
{
    protected int bizFormId = 0;
    protected BizFormInfo bfi = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        // Check 'ReadForm' permission
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("cms.form", "ReadForm"))
        {
            RedirectToCMSDeskAccessDenied("cms.form", "ReadForm");
        }

        bizFormId = QueryHelper.GetInteger("formid", 0);
        layoutElem.FormType = CMSModules_AdminControls_Controls_Class_Layout.FORMTYPE_BIZFORM;
        layoutElem.ObjectID = bizFormId;
        layoutElem.OnBeforeSave += layoutElem_OnBeforeSave;
        layoutElem.OnAfterSave += layoutElem_OnAfterSave;

        // Load CSS style sheet to editor area
        if (CMSContext.CurrentSite != null)
        {
            int cssId = CMSContext.CurrentSite.SiteDefaultEditorStylesheet;
            if (cssId == 0) // Use default site CSS if none editor CSS is specified
            {
                cssId = CMSContext.CurrentSite.SiteDefaultStylesheetID;
            }
            layoutElem.CssStyleSheetID = cssId;
        }

        AttachmentTitle.TitleText = GetString("general.attachments");

        bfi = BizFormInfoProvider.GetBizFormInfo(bizFormId);
        EditedObject = bfi;
        if (!RequestHelper.IsPostBack())
        {
            if (bfi != null)
            {
                // Init file storage
                AttachmentList.SiteID = CMSContext.CurrentSiteID;
                AttachmentList.AllowPasteAttachments = true;
                AttachmentList.ObjectID = bfi.FormID;
                AttachmentList.ObjectType = FormObjectType.BIZFORM;
                AttachmentList.Category = MetaFileInfoProvider.OBJECT_CATEGORY_LAYOUT;
            }
        }
    }


    void layoutElem_OnAfterSave(object sender, EventArgs e)
    {
        // Log synchronization
        BizFormInfo bfi = BizFormInfoProvider.GetBizFormInfo(bizFormId);
        SynchronizationHelper.LogObjectChange(bfi, TaskTypeEnum.UpdateObject);
    }


    void layoutElem_OnBeforeSave(object sender, EventArgs e)
    {
        // Check 'EditForm' permission
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("cms.form", "EditForm"))
        {
            RedirectToCMSDeskAccessDenied("cms.form", "EditForm");
        }
    }


    protected void Page_PreRender(object sender, EventArgs e)
    {
        pnlAttachments.Visible = layoutElem.CustomLayoutEnabled;
    }
}
