using System;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.LicenseProvider;
using CMS.SiteProvider;
using CMS.UIControls;
using CMS.WorkflowEngine;
using CMS.SettingsProvider;

public partial class CMSModules_Workflows_Workflow_Scope_Edit : SiteManagerPage
{
    #region "Constants"

    private const string ALL_DOCTYPES = "-1";

    #endregion


    #region "Private vairables"

    private int? mWorkflowScopeId = null;
    private int workflowId = 0;
    private int siteId = 0;
    private WorkflowScopeInfo mCurrentScopeInfo = null;

    #endregion


    #region "Private properties"

    private int WorkflowScopeId
    {
        get
        {
            if (mWorkflowScopeId == null)
            {
                mWorkflowScopeId = QueryHelper.GetInteger("scopeid", 0);
            }
            return mWorkflowScopeId.Value;
        }
    }


    private WorkflowScopeInfo CurrentScopeInfo
    {
        get
        {
            if (mCurrentScopeInfo == null)
            {
                mCurrentScopeInfo = WorkflowScopeInfoProvider.GetWorkflowScopeInfo(WorkflowScopeId);
                // Set edited object
                EditedObject = mCurrentScopeInfo;
            }
            return mCurrentScopeInfo;
        }
    }

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Set selector mode
        selectClassNames.UniSelector.SelectionMode = SelectionModeEnum.SingleDropDownList;
        selectClassNames.UniSelector.AllowAll = true;

        string startingAliasPath = string.Empty;
        int classId = 0;
        int cultureId = 0;


        if (WorkflowScopeId > 0)
        {
            if (CurrentScopeInfo != null)
            {
                workflowId = CurrentScopeInfo.ScopeWorkflowID;
                siteId = CurrentScopeInfo.ScopeSiteID;
                startingAliasPath = CurrentScopeInfo.ScopeStartingPath;
                classId = CurrentScopeInfo.ScopeClassID;
                cultureId = CurrentScopeInfo.ScopeCultureID;
            }
        }
        else
        {
            workflowId = QueryHelper.GetInteger("workflowid", 0);
            siteId = QueryHelper.GetInteger("siteid", 0);
        }

        // Check languages on site
        SiteInfo si = SiteInfoProvider.GetSiteInfo(siteId);
        if (si != null)
        {
            // Check whether workflow is enabled for specified site
            LicenseHelper.CheckFeatureAndRedirect(URLHelper.GetDomainName(si.DomainName), FeatureEnum.WorkflowVersioning);

            if (!CultureInfoProvider.LicenseVersionCheck(si.DomainName, FeatureEnum.Multilingual, VersionActionEnum.Edit))
            {
                lblError.Text = GetString("licenselimitation.siteculturesexceeded");
                lblError.Visible = true;
                pnlForm.Enabled = false;

            }
            // Set selector's where condition
            selectClassNames.UniSelector.WhereCondition = "ClassID IN (SELECT ClassID FROM CMS_ClassSite WHERE SiteID=" + si.SiteID + ") AND ClassIsDocumentType = 1";
        }

        // Culture selector
        cultureSelector.UseCultureCode = false;
        cultureSelector.SpecialFields = new string[,] { { GetString("general.selectall"), "0" } };
        cultureSelector.SiteID = siteId;

        pathElem.SiteID = siteId;

        // Scripts for path selector
        if (!RequestHelper.IsPostBack())
        {
            pathElem.Value = startingAliasPath;

            string className = DataClassInfoProvider.GetClassName(classId);
            if (!string.IsNullOrEmpty(className))
            {
                selectClassNames.Value = className;
            }

            // Show that the scope was created updated successfully
            if (QueryHelper.GetString("saved", string.Empty) == "1")
            {
                lblInfo.Visible = true;
                lblInfo.Text = GetString("General.ChangesSaved");
            }

            cultureSelector.Value = cultureId;
        }

        string workflowScopes = GetString("Development-Workflow_Scope_New.Scopes");
        string workflowScopesUrl = "~/CMSModules/Workflows/Workflow_Scopes.aspx?workflowid=" + workflowId + "&siteid=" + siteId;
        string currentScope = "";
        if (WorkflowScopeId > 0)
        {
            currentScope = GetString("Development-Workflow_Scope_New.ScopeID") + WorkflowScopeId;
        }
        else
        {
            currentScope = GetString("Development-Workflow_Scope_New.Scope");
        }

        // Initialize page title
        InitializeMasterPage(workflowScopes, workflowScopesUrl, currentScope);

        // Hide culture selector due to license restrictions
        plcCulture.Visible = LicenseHelper.CheckFeature(URLHelper.GetCurrentDomain(), FeatureEnum.Multilingual);
    }

    #endregion


    #region "Other methods"

    /// <summary>
    /// Initializes the master page elements.
    /// </summary>
    private void InitializeMasterPage(string workflowScopes, string workflowScopesUrl, string currentScope)
    {
        // Set master page title
        CurrentMaster.Title.HelpTopicName = "newedit_scope";
        CurrentMaster.Title.HelpName = "helpTopic";

        // Initialize breadcrumbs
        string[,] pageTitleTabs = new string[2, 3];
        pageTitleTabs[0, 0] = workflowScopes;
        pageTitleTabs[0, 1] = workflowScopesUrl;
        pageTitleTabs[0, 2] = "workflowsContent";
        pageTitleTabs[1, 0] = currentScope;
        pageTitleTabs[1, 1] = string.Empty;
        pageTitleTabs[1, 2] = string.Empty;

        CurrentMaster.Title.Breadcrumbs = pageTitleTabs;
    }

    #endregion


    #region "Button handling"

    /// <summary>
    /// Saves data of edited workflow scope from TextBoxes into DB.
    /// </summary>
    protected void btnOK_Click(object sender, EventArgs e)
    {
        // finds whether required fields are not empty
        string result = new Validator().NotEmpty(pathElem.Value, GetString("Development-Workflow_Scope_Edit.RequiresStartingAliasPath")).Result;

        string className = ValidationHelper.GetString(selectClassNames.Value, ALL_DOCTYPES);
        int classId = 0;
        if (className != ALL_DOCTYPES)
        {
            // Get class ID
            DataClassInfo dci = DataClassInfoProvider.GetDataClass(className);
            classId = dci.ClassID;
        }

        if (result == string.Empty)
        {
            if (WorkflowScopeId > 0)
            {
                if (CurrentScopeInfo != null)
                {
                    if (!pathElem.Value.ToString().StartsWith("/"))
                    {
                        pathElem.Value = "/" + pathElem.Value;
                    }
                    CurrentScopeInfo.ScopeStartingPath = pathElem.Value.ToString().Trim();
                    CurrentScopeInfo.ScopeClassID = classId;
                    CurrentScopeInfo.ScopeID = WorkflowScopeId;
                    CurrentScopeInfo.ScopeCultureID = ValidationHelper.GetInteger(cultureSelector.Value, 0);
                    WorkflowScopeInfoProvider.SetWorkflowScopeInfo(CurrentScopeInfo);
                    lblInfo.Visible = true;
                    lblInfo.Text = GetString("General.ChangesSaved");
                }
            }
            else
            {
                if (workflowId > 0)
                {
                    if (siteId > 0)
                    {
                        if (!pathElem.Value.ToString().StartsWith("/"))
                        {
                            pathElem.Value = "/" + pathElem.Value;
                        }
                        WorkflowScopeInfo wsi = new WorkflowScopeInfo();
                        wsi.ScopeStartingPath = pathElem.Value.ToString().Trim();
                        wsi.ScopeClassID = classId;
                        wsi.ScopeCultureID = ValidationHelper.GetInteger(cultureSelector.Value, 0);
                        wsi.ScopeSiteID = siteId;
                        wsi.ScopeWorkflowID = workflowId;
                        try
                        {
                            WorkflowScopeInfoProvider.SetWorkflowScopeInfo(wsi);
                            URLHelper.Redirect("Workflow_Scope_Edit.aspx?scopeid=" + wsi.ScopeID + "&saved=1");
                        }
                        catch (Exception ex)
                        {
                            lblError.Visible = true;
                            lblError.Text = ex.Message;
                        }
                    }
                    else
                    {
                        lblError.Visible = true;
                        lblError.Text = GetString("Development-Workflow_Scope_Edit.NoSiteIdGiven");
                    }
                }
                else
                {
                    lblError.Visible = true;
                    lblError.Text = GetString("Development-Workflow_Scope_Edit.NoDocumentTypeGiven");
                }
            }
        }
        else
        {
            lblError.Visible = true;
            lblError.Text = result;
        }
    }

    #endregion
}
