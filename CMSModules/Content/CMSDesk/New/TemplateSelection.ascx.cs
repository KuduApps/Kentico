using System;
using System.Web;
using System.Web.UI;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.PortalEngine;
using CMS.SiteProvider;
using CMS.TreeEngine;

public partial class CMSModules_Content_CMSDesk_New_TemplateSelection : CMSUserControl
{
    #region "Properties"

    /// <summary>
    /// Returns true if some option is available.
    /// </summary>
    public bool SomeOptionAvailable
    {
        get
        {
            return this.plcRadioButtons.Visible;
        }
    }


    /// <summary>
    /// Document ID for the selection.
    /// </summary>
    public int DocumentID
    {
        get
        {
            return templateSelector.DocumentID;
        }
        set
        {
            templateSelector.DocumentID = value;
        }
    }


    /// <summary>
    /// Parent Node ID.
    /// </summary>
    public int ParentNodeID
    {
        get;
        set;
    }


    /// <summary>
    /// State indicate which selector is currently used
    /// </summary>
    public int TemplateSelectionState
    {
        get;
        set;
    }


    /// <summary>
    /// Returns true if the newly created template is empty.
    /// </summary>
    public bool NewTemplateIsEmpty
    {
        get
        {
            return radCreateBlank.Checked || radCreateEmpty.Checked;
        }
    }

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        // Check the first radio button which is visible by default
        if (!URLHelper.IsPostback())
        {
            if (plcRadioButtons.IsHidden || plcRadioButtonsNew.IsHidden)
            {
                this.radInherit.Checked = true;
            }
            else
            {
                if (!plcUseTemplate.IsHidden)
                {
                    radUseTemplate.Checked = true;
                }
                else if (!plcInherit.IsHidden)
                {
                    radInherit.Checked = true;
                }
                else if (!plcCreateBlank.IsHidden)
                {
                    radCreateBlank.Checked = true;
                }
                else if (!plcCreateEmpty.IsHidden)
                {
                    radCreateEmpty.Checked = true;
                }

                this.plcRadioButtons.Visible = !(plcUseTemplate.IsHidden && plcInherit.IsHidden && plcCreateBlank.IsHidden && plcCreateEmpty.IsHidden);
            }

            if (!SomeOptionAvailable)
            {
                RedirectToUINotAvailable();
            }
        }

        // Check authorization per resource
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.Design", "Design"))
        {
            radCreateBlank.Visible = false;
            radCreateEmpty.Visible = false;
            layoutSelector.Visible = false;

            if (plcUseTemplate.IsHidden && plcInherit.IsHidden && !plcCreateBlank.IsHidden)
            {
                RedirectToAccessDenied("CMS.Design", "Design");
            }
        }

        radUseTemplate.CheckedChanged += radOptions_CheckedChanged;
        radInherit.CheckedChanged += radOptions_CheckedChanged;
        radCreateBlank.CheckedChanged += radOptions_CheckedChanged;
        radCreateEmpty.CheckedChanged += radOptions_CheckedChanged;

        // Disable startup focus functionality
        templateSelector.UseStartUpFocus = false;

        LoadControls();
    }


    /// <summary>
    /// Gets the validation script for the selector.
    /// </summary>
    public string GetValidationScript()
    {
        if (!radInherit.Checked && !radCreateEmpty.Checked)
        {
            string errorMessage = String.Empty;
            if (radCreateBlank.Checked)
            {
                errorMessage = ScriptHelper.GetString(GetString("NewPage.LayoutError"));
            }
            else
            {
                errorMessage = ScriptHelper.GetString(GetString("newpage.templateerror"));
            }

            return 
@"if (UniFlat_GetSelectedValue) { 
    value = UniFlat_GetSelectedValue();
    value = value.replace(/^\\s+|\\s+$/g, '');
    if (value == '') {
        errorLabel.style.display = '';
        errorLabel.innerHTML = " + errorMessage + @"; 
        resizearea();
        return false;
    }
}";
        }

        return null;
    }


    /// <summary>
    /// Handles radio button change.
    /// </summary>
    protected void radOptions_CheckedChanged(object sender, EventArgs e)
    {
        // Template selector needs to reload its tree
        if (radUseTemplate.Checked)
        {
            templateSelector.ResetToDefault();

            // Reload template tree
            templateSelector.ReloadData(false);

            // Recalculate the items count
            templateSelector.RegisterRefreshPageSizeScript(true);

            // Enable startup focus functionality
            templateSelector.UseStartUpFocus = true;
        }
        else if (radCreateBlank.Checked)
        {
            layoutSelector.UniFlatSelector.ResetToDefault();

            // Recalculate the items count
            layoutSelector.RegisterRefreshPageSizeScript(true);

            // Enable startup focus functionality
            layoutSelector.UniFlatSelector.UseStartUpFocus = true;
        }

        // Start rezise after radio change
        ScriptHelper.RegisterStartupScript(this.Page, typeof(string), "ResizeRecount", ScriptHelper.GetScript("resizearea();"));

        // Update panel
        pnlUpdate.Update();
    }


    /// <summary>
    /// Returns template name of parent node.
    /// </summary>    
    private string GetParentNodePageTemplate()
    {
        TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);
        TreeNode node = tree.SelectSingleNode(ParentNodeID, CMSContext.PreferredCultureCode, tree.CombineWithDefaultCulture, false);
        if (node != null)
        {
            int templateId = ValidationHelper.GetInteger(node.GetValue("DocumentPageTemplateID"), 0);
            bool inherited = false;

            // May be inherited template
            if ((templateId == 0) && (node.NodeParentID > 0))
            {
                // Get inherited page template
                object currentPageTemplateId = node.GetValue("DocumentPageTemplateID");
                node.SetValue("DocumentPageTemplateID", DBNull.Value);
                node.LoadInheritedValues(new string[] { "DocumentPageTemplateID" }, false);
                templateId = ValidationHelper.GetInteger(node.GetValue("DocumentPageTemplateID"), 0);
                node.SetValue("DocumentPageTemplateID", currentPageTemplateId);
                inherited = true;
            }

            if (templateId > 0)
            {
                PageTemplateInfo pti = PageTemplateInfoProvider.GetPageTemplateInfo(templateId);
                if (pti != null)
                {
                    string templateName = pti.DisplayName;
                    if (inherited)
                    {
                        templateName += " (inherited)";
                    }
                    return templateName;
                }
            }
        }

        return String.Empty;
    }


    /// <summary>
    /// Setups and load control selected by radio buttons.
    /// </summary>
    private void LoadControls()
    {
        if (radUseTemplate.Checked)
        {
            TemplateSelectionState = 0;
            ShowTemplateSelector(true);
            ShowLayoutSelector(false);
            ShowInherited(false);
        }
        else if (radInherit.Checked)
        {
            TemplateSelectionState = 1;
            lblIngerited.Text = GetString("NewPage.InheritedTemplateName").Replace("##TEMPLATENAME##", GetParentNodePageTemplate());
            ShowTemplateSelector(false);
            ShowLayoutSelector(false);
            ShowInherited(true);
        }
        else if (radCreateBlank.Checked)
        {
            TemplateSelectionState = 2;
            ShowTemplateSelector(false);
            ShowLayoutSelector(true);
            ShowInherited(false);
        }
        else
        {
            TemplateSelectionState = 3;
            lblIngerited.Text = GetString("NewPage.BlankTemplate");
            ShowTemplateSelector(false);
            ShowLayoutSelector(false);
            ShowInherited(true);
        }
    }


    /// <summary>
    /// Enables or disables template selector.
    /// </summary>    
    private void ShowTemplateSelector(bool show)
    {
        templateSelector.Visible = show;
        templateSelector.StopProcessing = !show;
    }


    /// <summary>
    /// Enables or disables layout selector.
    /// </summary>
    private void ShowLayoutSelector(bool show)
    {
        plcLayout.Visible = show;
        layoutSelector.StopProcessing = !show;
    }


    /// <summary>
    /// Enables or disables inherited label.
    /// </summary>
    private void ShowInherited(bool show)
    {
        plcInherited.Visible = show;
    }


    /// <summary>
    /// Ensures the template from the selection and returns the template ID.
    /// </summary>
    /// <param name="documentName">Document name for the ad-hoc template</param>
    /// <param name="errorMessage">Returns the error message</param>
    public int EnsureTemplate(string documentName, ref string errorMessage)
    {
        int result = 0;

        // Template selection
        if (radUseTemplate.Checked)
        {
            // Template page
            int templateId = ValidationHelper.GetInteger(templateSelector.SelectedItem, 0);
            if (templateId > 0)
            {
                result = templateId;
            }
            else
            {
                errorMessage = GetString("NewPage.TemplateError");
            }
        }
        else if (radInherit.Checked)
        {
            // Inherited page           
        }
        else if (radCreateBlank.Checked || radCreateEmpty.Checked)
        {
            // Create custom template info for the page
            PageTemplateInfo templateInfo = new PageTemplateInfo(true);

            // Prepare ad-hoc template name
            string displayName = "Ad-hoc: " + documentName;

            if (radCreateBlank.Checked)
            {
                // Blank page with layout
                int layoutId = ValidationHelper.GetInteger(layoutSelector.SelectedItem, 0);
                if (layoutId > 0)
                {
                    templateInfo.LayoutID = layoutId;

                    // Copy layout to selected template
                    if (chkLayoutPageTemplate.Checked)
                    {
                        templateInfo.LayoutID = 0;
                        LayoutInfo li = LayoutInfoProvider.GetLayoutInfo(layoutId);
                        if (li != null)
                        {
                            templateInfo.PageTemplateLayout = li.LayoutCode;
                            templateInfo.PageTemplateLayoutType = li.LayoutType;
                        }
                        else
                        {
                            errorMessage = GetString("NewPage.LayoutError");
                        }
                    }
                }
                else
                {
                    errorMessage = GetString("NewPage.LayoutError");
                }
            }
            else if (radCreateEmpty.Checked)
            {
                // Empty template
                templateInfo.LayoutID = 0;
                templateInfo.PageTemplateLayout = "<cc1:CMSWebPartZone ID=\"zoneLeft\" runat=\"server\" />";
                templateInfo.PageTemplateLayoutType = LayoutTypeEnum.Ascx;
            }

            if (String.IsNullOrEmpty(errorMessage))
            {
                // Create ad-hoc template
                templateInfo = PageTemplateInfoProvider.CloneTemplateAsAdHoc(templateInfo, displayName, CMSContext.CurrentSiteID);

                // Set inherit only master 
                templateInfo.InheritPageLevels = "\\";
                PageTemplateInfoProvider.SetPageTemplateInfo(templateInfo);

                if (CMSContext.CurrentSite != null)
                {
                    PageTemplateInfoProvider.AddPageTemplateToSite(templateInfo.PageTemplateId, CMSContext.CurrentSiteID);
                }

                // Assign the template to document
                result = templateInfo.PageTemplateId;
            }
        }

        // Reload the template selector in case of error
        if (!String.IsNullOrEmpty(errorMessage))
        {
            if (radUseTemplate.Checked) 
            {
                templateSelector.ReloadData();
            }
        }

        return result;
    }
}
