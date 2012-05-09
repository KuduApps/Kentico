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
using CMS.CMSHelper;
using CMS.FormEngine;
using CMS.UIControls;
using CMS.Synchronization;
using CMS.SettingsProvider;

public partial class CMSModules_BizForms_Tools_AlternativeForms_AlternativeForms_List : CMSBizFormPage
{
    #region "Private variables"

    private int formId = 0;      // BizForm id
    private BizFormInfo bfi = null;

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        // Check 'ReadForm' permission
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("cms.form", "ReadForm"))
        {
            RedirectToCMSDeskAccessDenied("cms.form", "ReadForm");
        }

        formId = QueryHelper.GetInteger("formid", 0);
        bfi = BizFormInfoProvider.GetBizFormInfo(formId);
        EditedObject = bfi;

        if (bfi == null)
        {
            lblError.Visible = true;
            lblError.Text = GetString("general.invalidid");
            return;
        }

        // Init alternative forms listing
        listElem.FormClassID = bfi.FormClassID;
        listElem.OnEdit += listElem_OnEdit;
        listElem.OnDelete += listElem_OnDelete;

        // New item link
        string[,] actions = new string[1, 6];
        actions[0, 0] = HeaderActions.TYPE_HYPERLINK;
        actions[0, 1] = GetString("altforms.newformlink");
        actions[0, 2] = null;
        actions[0, 3] = ResolveUrl("AlternativeForms_New.aspx?formid=" + formId.ToString());
        actions[0, 4] = null;
        actions[0, 5] = GetImageUrl("Objects/CMS_AlternativeForm/add.png");
        this.CurrentMaster.HeaderActions.Actions = actions;
    }


    void listElem_OnEdit(object sender, object actionArgument)
    {
        URLHelper.Redirect("AlternativeForms_Frameset.aspx?formid=" + formId.ToString() +
            "&altformid=" + ValidationHelper.GetInteger(actionArgument, 0));
    }


    void listElem_OnDelete(object sender, object actionArgument)
    {
        // Check 'EditForm' permission
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("cms.form", "EditForm"))
        {
            RedirectToCMSDeskAccessDenied("cms.form", "EditForm");
        }
        AlternativeFormInfoProvider.DeleteAlternativeFormInfo(ValidationHelper.GetInteger(actionArgument, 0));

        // Required to log staging task, alternative form is not binded to bizform as child
        using (CMSActionContext context = new CMSActionContext())
        {
            context.CreateVersion = false;

            // Log synchronization
            SynchronizationHelper.LogObjectChange(bfi, TaskTypeEnum.UpdateObject);
        }
    }
}
