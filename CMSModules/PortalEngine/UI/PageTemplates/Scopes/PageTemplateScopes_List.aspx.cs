using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.SiteProvider;
using CMS.PortalEngine;
using CMS.CMSHelper;

public partial class CMSModules_PortalEngine_UI_PageTemplates_Scopes_PageTemplateScopes_List : CMSEditTemplatePage
{
    #region "Variables"

    int templateID = 0;
    int siteID = 0;
    PageTemplateInfo template = null;

    #endregion


    #region "Events"

    /// <summary>
    /// Page load event.
    /// </summary>
    /// <param name="sender">Sender</param>
    /// <param name="e">Arguments</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!CMSContext.CurrentUser.UserSiteManagerAdmin)
        {
            RedirectToAccessDenied(GetString("template.scopes.denied"));
        }

        // Master page settings
        CurrentMaster.DisplayControlsPanel = true;
        CurrentMaster.DisplaySiteSelectorPanel = true;

        // Get template id
        templateID = QueryHelper.GetInteger("templateid", 0);

        // Get template info
        if (templateID > 0)
        {
            template = PageTemplateInfoProvider.GetPageTemplateInfo(templateID);
        }

        if (!RequestHelper.IsPostBack())
        {
            if (template != null)
            {
                radAllPages.Checked = template.PageTemplateForAllPages;
                radSelectedScopes.Checked = !template.PageTemplateForAllPages;
            }
            siteID = QueryHelper.GetInteger("siteid", 0);
            if (siteID > 0)
            {
                selectSite.Value = siteID;
            }
        }
        else
        {
            siteID = ValidationHelper.GetInteger(selectSite.Value, 0);
        }

        // Display only assigned sites
        selectSite.UniSelector.WhereCondition = "SiteID IN (SELECT SiteID FROM CMS_PageTemplateSite WHERE PageTemplateID = " + templateID + ") OR SiteID IN (SELECT PageTemplateScopeSiteID FROM CMS_PageTemplateScope WHERE PageTemplateScopeTemplateID = " + templateID + ")";

        // Show scopes content only if option template can be used within scopes is selected
        if (radAllPages.Checked)
        {
            pnlContent.Visible = false;
        }
        else
        {
            pnlContent.Visible = true;

            // New item link
            string[,] actions = new string[1, 6];
            actions[0, 0] = HeaderActions.TYPE_HYPERLINK;
            actions[0, 1] = GetString("template.scopes.newscope");
            actions[0, 2] = null;
            actions[0, 3] = "javascript: AddNewItem()";
            actions[0, 4] = null;
            actions[0, 5] = GetImageUrl("Objects/CMS_PageTemplateScope/add.png");
            CurrentMaster.HeaderActions.Actions = actions;
        }

        // Setup unigrid
        unigridScopes.OnAction += new OnActionEventHandler(unigridScopes_OnAction);
        unigridScopes.OnExternalDataBound += new OnExternalDataBoundEventHandler(unigridScopes_OnExternalDataBound);
        unigridScopes.WhereCondition = GenerateWhereCondition();
        unigridScopes.ZeroRowsText = GetString("general.nodatafound");

        // Set site selector
        selectSite.DropDownSingleSelect.AutoPostBack = true;
        selectSite.AllowAll = false;
        selectSite.OnlyRunningSites = false;
        selectSite.UniSelector.SpecialFields = new string[1, 2] { { GetString("template.scopes.global"), "0" } };
        selectSite.UniSelector.OnSelectionChanged += new EventHandler(UniSelector_OnSelectionChanged);

        // Register correct script for new item
        ScriptHelper.RegisterClientScriptBlock(this.Page, typeof(string), "AddNewItem", ScriptHelper.GetScript(
            "function AddNewItem() { this.window.location = '" + ResolveUrl("PageTemplateScope_Edit.aspx?templateid=" + templateID.ToString() + "&siteID=" + siteID.ToString()) + "'} "));
    }


    /// <summary>
    /// On selection changed.
    /// </summary>
    /// <param name="sender">Sender</param>
    /// <param name="e">Arguments</param>
    void UniSelector_OnSelectionChanged(object sender, EventArgs e)
    {
        pnlUpdate.Update();
    }


    /// <summary>
    /// On unigrids external databond.
    /// </summary>
    /// <param name="sender">Sender</param>
    /// <param name="sourceName">Name</param>
    /// <param name="parameter">Parameter</param>    
    object unigridScopes_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        switch (sourceName.ToLower())
        {
            // Class name
            case "documenttype":
                int classID = ValidationHelper.GetInteger(parameter, 0);
                DataClassInfo dataClass = DataClassInfoProvider.GetDataClass(classID);
                if (dataClass != null)
                {
                    return dataClass.ClassDisplayName;
                }
                else
                {
                    return GetString("general.all");
                }

            // Culture
            case "culture":
                int cultureID = ValidationHelper.GetInteger(parameter, 0);
                if (cultureID > 0)
                {
                    CultureInfo culture = CultureInfoProvider.GetCultureInfo(cultureID);
                    if (culture != null)
                    {
                        return culture.CultureCode;
                    }
                }
                return GetString("general.all");

            // Levels
            case "levels":
                string levels = ValidationHelper.GetString(parameter, String.Empty);
                if (string.IsNullOrEmpty(levels))
                {
                    return GetString("general.all");
                }

                // Format levels
                levels = levels.Replace("/", String.Empty).Replace("{", " ").Replace("}",",");
                return levels.TrimEnd(',');
        }

        return null;
    }


    /// <summary>
    /// Unigrid on action.
    /// </summary>
    /// <param name="actionName">Action name</param>
    /// <param name="actionArgument">Argument</param>
    void unigridScopes_OnAction(string actionName, object actionArgument)
    {
        switch (actionName.ToLower())
        {
            case "edit":
                URLHelper.Redirect("PageTemplateScope_Edit.aspx?scopeid=" + ValidationHelper.GetString(actionArgument, "0") + "&templateid=" + templateID.ToString() + "&siteID=" + siteID.ToString());
                break;

            case "delete":
                PageTemplateScopeInfoProvider.DeletePageTemplateScopeInfo(ValidationHelper.GetInteger(actionArgument, 0));
                break;

        }
    }


    /// <summary>
    /// Radiobutton checked changed.
    /// </summary>
    /// <param name="sender">Sender</param>
    /// <param name="e">Arguments</param>
    protected void radAllPages_CheckedChanged(object sender, EventArgs e)
    {
        UpdatePageTemplateInfo();
    }


    /// <summary>
    /// Radiobutton checked changed.
    /// </summary>
    /// <param name="sender">Sender</param>
    /// <param name="e">Arguments</param>
    protected void radSelectedScopes_CheckedChanged(object sender, EventArgs e)
    {
        UpdatePageTemplateInfo();
    }


    /// <summary>
    /// Updates PageTemplateForAllPages property of template info.
    /// </summary>
    private void UpdatePageTemplateInfo()
    {
        if (template != null)
        {
            template.PageTemplateForAllPages = radAllPages.Checked;
            PageTemplateInfoProvider.SetPageTemplateInfo(template);
        }
    }


    /// <summary>
    /// Generates where condition for unigrid.
    /// </summary>    
    private string GenerateWhereCondition()
    {
        string where = "PageTemplateScopeTemplateID=" + templateID + "AND PageTemplateScopeSiteID";

        if (siteID > 0)
        {
            return where + " = " + siteID;
        }
        else
        {
            return where + " IS NULL";
        }
    }

    #endregion

}
