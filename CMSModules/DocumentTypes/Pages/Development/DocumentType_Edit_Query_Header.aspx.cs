using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.DataEngine;

public partial class CMSModules_DocumentTypes_Pages_Development_DocumentType_Edit_Query_Header : CMSModalDesignPage
{
    #region "Variables"

    private bool mDialogMode = false;

    #endregion


    protected override void OnPreInit(EventArgs e)
    {
        RequireSite = false;

        mDialogMode = QueryHelper.GetBoolean("editonlycode", false);

        if (mDialogMode)
        {
            MasterPageFile = "~/CMSMasterPages/UI/Dialogs/TabsHeader.master";
        }
        else
        {
            // Page opened from Site Manager
            CheckAccessToSiteManager();
        }

        base.OnPreInit(e);
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        Query query = null;

        if (PersistentEditedObject == null)
        {
            // Get query depending on whether this was invoked from a dialog or site manager
            if (mDialogMode)
            {
                string queryName = QueryHelper.GetString("name", string.Empty);
                if (queryName != string.Empty)
                {
                    PersistentEditedObject = QueryProvider.GetQuery(queryName, false);
                }
            }
            else
            {
                int queryId = QueryHelper.GetInteger("queryid", 0);
                if (queryId > 0)
                {
                    PersistentEditedObject = QueryProvider.GetQuery(queryId);
                }
            }
        }

        query = PersistentEditedObject as Query;

        // Initialize breadcrumbs and tabs
        if (query != null)
        {
            SetEditedObject(query, null);

            if (!mDialogMode)
            {
                InitBreadcrumbs(2);
                SetBreadcrumb(0, GetString("DocumentType_Edit_Query_Edit.Queries"), ResolveUrl("~/CMSModules/DocumentTypes/Pages/Development/DocumentType_Edit_Query_List.aspx?documenttypeid=" + query.QueryClassId), "_parent", null);
                SetBreadcrumb(1, query.QueryName, null, null, null);
            }
            else
            {
                SetTitle("Objects/CMS_Query/object.png", GetString("query.edit"), null, "helpTopic");

                string selector = QueryHelper.GetString("selectorid", string.Empty);
                if (!string.IsNullOrEmpty(selector) && RequestHelper.IsPostBack())
                {
                    // Add selector refresh
                    string script =
                        string.Format(@"var wopener = window.top.opener ? window.top.opener : window.top.dialogArguments;
		                                if (wopener && wopener.US_SelectNewValue_{0}) {{ wopener.US_SelectNewValue_{0}('{1}'); }}",
                                        selector, query.QueryFullName);

                    ScriptHelper.RegisterStartupScript(this, GetType(), "UpdateSelector", script, true);
                }
            }

            string helpTopic = "newedit_query";

            // Set page title
            DataClassInfo classObj = DataClassInfoProvider.GetDataClass(query.QueryClassId);
            if ((classObj != null) && classObj.ClassIsCustomTable)
            {
                helpTopic = "customtable_edit_newedit_query";
            }

            SetHelp(helpTopic, "helpTopic");

            // Set tabs number and ensure additional tab
            InitTabs(1, "q_edit_content");
            
            string url = "~/CMSModules/DocumentTypes/Pages/Development/DocumentType_Edit_Query_Edit.aspx" + URLHelper.Url.Query;
            url = URLHelper.RemoveParameterFromUrl(url, "saved");
            
            if (mDialogMode)
            {
                url = URLHelper.AddParameterToUrl(url,"name",query.QueryFullName);
            }
            else
            {
                url = URLHelper.AddParameterToUrl(url, "queryid", query.QueryId.ToString());
            }
            SetTab(0, GetString("general.general"), ResolveUrl(url), "SetHelpTopic('helpTopic', '" + helpTopic + "');");
        }
    }
}
