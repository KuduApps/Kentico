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
using CMS.UIControls;

public partial class CMSSiteManager_Sites_Site_Edit_DomainAliases : SiteManagerPage
{
    protected int siteId = 0;
    protected string siteName = "";
    protected DataSet mDs = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        siteId = QueryHelper.GetInteger("siteid", 0);

        // Masterpage initialization - New item link
        string[,] actions = new string[1, 6];
        actions[0, 0] = HeaderActions.TYPE_HYPERLINK;
        actions[0, 1] = GetString("Sites-DomainAliases.NewAlias");
        actions[0, 2] = null;
        actions[0, 3] = ResolveUrl("Site_Edit_DomainAlias_Edit.aspx?&siteId=" + siteId);
        actions[0, 4] = null;
        actions[0, 5] = GetImageUrl("Objects/CMS_SiteDomainAlias/add.png"); 
        CurrentMaster.HeaderActions.Actions = actions;

        if (siteId > 0)
        {
            UniGridAliases.WhereCondition = "SiteID = " + siteId;
        }

        //Set unigrid
        UniGridAliases.OnAction += new OnActionEventHandler(UniGridAliases_OnAction);
        UniGridAliases.OnExternalDataBound += new OnExternalDataBoundEventHandler(UniGridAliases_OnExternalDataBound);

        UniGridAliases.HideControlForZeroRows = true;
    }


    protected void Page_PreRender(object sender, EventArgs e)
    {
        // Hide filter if sites fit one page
        DataSet ds = UniGridAliases.GridView.DataSource as DataSet;
        if (String.IsNullOrEmpty(UniGridAliases.WhereClause) && (DataHelper.DataSourceIsEmpty(ds) || (ds.Tables[0].Rows.Count <= UniGridAliases.GridView.PageSize)))
        {
            UniGridAliases.FilterPlaceHolder.Visible = false;
        }
    }


    protected object UniGridAliases_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        switch (sourceName.ToLower())
        {
            case "sitedefaultvisitorculture":

                // Get visitor culture
                string defaultCulture = DataHelper.GetDataRowViewValue((DataRowView)parameter, "SiteDefaultVisitorCulture") as string;

                // If not set it is Automatic
                if (String.IsNullOrEmpty(defaultCulture))
                {
                    return GetString("Site_Edit.Automatic");
                }
                else
                {
                    CultureInfo ci = CultureInfoProvider.GetCultureInfo(defaultCulture);
                    if (ci != null)
                    {
                        return ci.CultureName;
                    }
                }

                break;
        }

        return String.Empty;
    }


    /// <summary>
    /// UniGrid actions.
    /// </summary>
    protected void UniGridAliases_OnAction(string actionName, object actionArgument)
    {
        if (actionName == "edit")
        {
            URLHelper.Redirect("Site_Edit_DomainAlias_Edit.aspx?siteId=" + siteId.ToString() + "&domainAliasId=" + actionArgument.ToString());
        }
        else if (actionName == "delete")
        {
            int aliasId = ValidationHelper.GetInteger(actionArgument, 0);

            SiteDomainAliasInfoProvider.DeleteSiteDomainAliasInfo(aliasId);
            UniGridAliases.ReloadData();
        }
    }
}
