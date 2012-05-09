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
using CMS.SiteProvider;
using CMS.CMSHelper;
using CMS.UIControls;
using CMS.EmailEngine;

public partial class CMSModules_EmailTemplates_Controls_EmailTemplateList : CMSAdminListControl
{
    #region "Variables"

    private int mSiteId = 0;
    private int mGlobalRecordValue = UniSelector.US_GLOBAL_RECORD;

    #endregion


    #region "Public properties"

    /// <summary>
    /// Gets or sets global record value (value for global item selected in site selector).
    /// </summary>
    public int GlobalRecordValue
    {
        get
        {
            return this.mGlobalRecordValue;
        }
        set
        {
            this.mGlobalRecordValue = value;
            this.gridElem.WhereCondition = CreateWhereCondition();
        }
    }


    /// <summary>
    /// Gets or sets the site ID for which the e-mail templates should be displayed.
    /// </summary>
    public int SiteID
    {
        get
        {
            return this.mSiteId;
        }
        set
        {
            this.mSiteId = value;
            this.gridElem.WhereCondition = CreateWhereCondition();
        }
    }

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        RaiseOnCheckPermissions(CMSAdminControl.PERMISSION_READ, this);

        if (StopProcessing)
        {
            return;
        }

        // Unigrid initialization
        gridElem.IsLiveSite = IsLiveSite;
        gridElem.OnAction += gridElem_OnAction;
        gridElem.WhereCondition = CreateWhereCondition();
    }


    /// <summary>
    /// Handles the UniGrid's OnAction event.
    /// </summary>
    /// <param name="actionName">Name of item (button) that throws event</param>
    /// <param name="actionArgument">ID (value of Primary key) of corresponding data row</param>
    protected void gridElem_OnAction(string actionName, object actionArgument)
    {
        switch (actionName.ToLower())
        {
            case "edit":
                SelectedItemID = ValidationHelper.GetInteger(actionArgument, 0);
                RaiseOnEdit();
                break;

            case "delete":
                if (!CheckPermissions("CMS.EmailTemplates", CMSAdminControl.PERMISSION_MODIFY))
                {
                    return;
                }

                // Get TemplateID
                int templateId = ValidationHelper.GetInteger(actionArgument, 0);
                EmailTemplateProvider.DeleteEmailTemplate(templateId);
                break;
        }

        RaiseOnAction(actionName, actionArgument);
    }


    /// <summary>
    /// Creates where condition for unigrid according to the parameters.
    /// </summary>
    private string CreateWhereCondition()
    {
        string where = string.Empty;

        if (this.mSiteId > 0)
        {
            where += "(EmailTemplateSiteID = " + this.mSiteId + ")";
        }
        else
            // Global selected
            if ((this.mSiteId == GlobalRecordValue) && CMSContext.CurrentUser.IsGlobalAdministrator)
            {
                where += "(EmailTemplateSiteID is NULL)";
            }
            else
            {
                where += "(EmailTemplateSiteID =" + CMSContext.CurrentSiteID + ")";
            }

        return where;
    }

    #endregion
}
