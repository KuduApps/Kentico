using System;

using CMS.CMSHelper;
using CMS.DataEngine;
using CMS.ExtendedControls;
using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.UIControls;
using CMS.SiteProvider;

public partial class CMSModules_DocumentTypes_Pages_Development_DocumentType_Edit_Query_Edit : CMSModalDesignPage
{
    #region "Variables"

    private bool mDialogMode;


    private bool mIsEditMode;


    private Query mQuery;


    private int mQueryClassId;

    #endregion


    #region "Methods"

    protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);

        RequireSite = false;

        mDialogMode = QueryHelper.GetBoolean("editonlycode", false);

        if (mDialogMode)
        {
            // Check permissions - user must have the permission to edit the code
            if (!CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.Design", "EditSQLCode"))
            {
                RedirectToCMSDeskAccessDenied("CMS.Design", "EditSQLCode");
            }

            // Page has been opened from CMSDesk
            MasterPageFile = "~/CMSMasterPages/UI/Dialogs/ModalDialogPage.master";
        }
        else
        {
            // Page opened from Site Manager
            CheckAccessToSiteManager();
        }
    }


    protected void Page_Init(object sender, EventArgs e)
    {
        GetParameters();

        if (mIsEditMode)
        {
            SetEditedObject(mQuery, "~/CMSModules/DocumentTypes/Pages/Development/DocumentType_Edit_Query_Frameset.aspx");
        }

        // Set QueryEdit params - must be initialized before load
        queryEdit.RefreshPageURL = "~/CMSModules/DocumentTypes/Pages/Development/DocumentType_Edit_Query_Edit.aspx";
        queryEdit.ClassID = mQueryClassId;
        queryEdit.QueryID = mQuery != null ? mQuery.QueryId : 0;
        queryEdit.DialogMode = mDialogMode;
        queryEdit.EditMode = mIsEditMode;
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        UserInfo ui = CMSContext.CurrentUser;
        queryEdit.IsSiteManager = !mDialogMode && ui.UserSiteManagerAdmin;

        if (mDialogMode)
        {
            SetDialogMode();
        }
        else
        {
            if (!TabMode)
            {
                InitBreadcrumbs();
            }
        }

        // Reload header if changes were saved
        if (TabMode && (QueryHelper.GetInteger("saved", 0) == 1))
        {
            ScriptHelper.RefreshTabHeader(Page, null);
        }      
    }


    private void GetParameters()
    {
        // Get query depending on whether this was invoked from a dialog or site manager
        if (mDialogMode)
        {
            string queryName = QueryHelper.GetString("name", string.Empty);
            if (queryName != string.Empty)
            {
                mQuery = QueryProvider.GetQuery(queryName, false);

                // If edit was called from dialog but wrong query is specified, return
                if (mQuery == null)
                {
                    ShowError(GetString("query.querynotexist").Replace("%%code%%", queryName));
                    queryEdit.Visible = false;
                    mIsEditMode = true;
                    return;
                }
            }
        }
        else
        {
            int queryId = QueryHelper.GetInteger("queryid", 0);
            if (queryId > 0)
            {
                mQuery = QueryProvider.GetQuery(queryId);
            }
        }

        // If query not specified, a new query will be created, so get document type's ID
        mIsEditMode = (mQuery != null);

        if (!mIsEditMode)
        {
            mQueryClassId = QueryHelper.GetInteger("documenttypeid", 0);
        }
    }


    private void SetDialogMode()
    {
        if (!TabMode)
        {
            SetTitle(mIsEditMode ? "Objects/CMS_Query/object.png" : "Objects/CMS_Query/query_new.png", mIsEditMode ? GetString("query.edit") : GetString("query.new"), "newedit_query", "helpTopic");
        }
        CurrentMaster.PanelFooter.CssClass = "FloatRight";

        if (!mIsEditMode)
        {
            // Add save button
            LocalizedButton btnSave = new LocalizedButton
            {
                ID = "btnSave",
                ResourceString = "general.save",
                CssClass = "SubmitButton",
                Visible = queryEdit.Visible,
                EnableViewState = false
            };
            btnSave.Click += (sender, e) => queryEdit.Save(false);
            CurrentMaster.PanelFooter.Controls.Add(btnSave);
        }

        // Add save & close button
        LocalizedButton btnSaveAndClose = new LocalizedButton
        {
            ID = "btnSaveAndClose",
            ResourceString = "general.saveandclose",
            CssClass = "LongSubmitButton",
            Visible = queryEdit.Visible,
            EnableViewState = false
        };
        btnSaveAndClose.Click += (sender, e) => queryEdit.Save(true);
        CurrentMaster.PanelFooter.Controls.Add(btnSaveAndClose);

        // Add close button every time
        CurrentMaster.PanelFooter.Controls.Add(new LocalizedButton
        {
            ID = "btnClose",
            ResourceString = "general.close",
            EnableViewState = false,
            OnClientClick = "window.top.close(); return false;",
            CssClass = "SubmitButton"
        });

        // Set refresh URL so that we don't lose dialog mode        
        queryEdit.RefreshPageURL = "~/CMSModules/DocumentTypes/Pages/Development/DocumentType_Edit_Query_Edit.aspx";
    }


    private void InitBreadcrumbs()
    {
        InitBreadcrumbs(2);

        int documentTypeId = mQueryClassId != 0 ? mQueryClassId : mQuery.QueryClassId;
        SetBreadcrumb(0, GetString("DocumentType_Edit_Query_Edit.Queries"), string.Format("~/CMSModules/DocumentTypes/Pages/Development/DocumentType_Edit_Query_List.aspx?documenttypeid={0}", documentTypeId), TabMode ? "_parent" : null, null);
        SetBreadcrumb(1, mIsEditMode ? mQuery.QueryName : GetString("DocumentType_Edit_Query_Edit.NewQueryName"), null, null, null);
    }

    #endregion
}