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

using CMS.PortalEngine;
using CMS.PortalControls;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.WorkflowEngine;

using TreeNode = CMS.TreeEngine.TreeNode;

public partial class CMSWebParts_General_pageplaceholder : CMSAbstractWebPart
{
    #region "Properties"

    /// <summary>
    /// Gets or sets the value than indicates whether placeholder checks access permissions for the editable web parts content.
    /// </summary>
    public bool CheckPermissions
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("CheckPermissions"), this.partPlaceholder.CheckPermissions);
        }
        set
        {
            this.SetValue("CheckPermissions", value);
            this.partPlaceholder.CheckPermissions = value;
        }
    }


    /// <summary>
    /// Gets or sets the default page temaplte of the page place holder.
    /// </summary>
    public string PageTemplate
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("PageTemplate"), "");
        }
        set
        {
            this.SetValue("PageTemplate", value);
        }
    }


    /// <summary>
    /// If true, the default template is used also on subpages of the document, otherwise the default template is used only on current document while the child documents use standard inheritance rules.
    /// </summary>
    public bool UseDefaultTemplateOnSubPages
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("UseDefaultTemplateOnSubPages"), false);

        }
        set
        {
            this.SetValue("UseDefaultTemplateOnSubPages", value);
        }
    }


    /// <summary>
    /// Gets or sets the path of the document to display within this placeholder if the default page template is used.
    /// </summary>
    public string Path
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("Path"), "");
        }
        set
        {
            this.SetValue("Path", value);
        }
    }


    /// <summary>
    /// Gets or sets the cache minutes.
    /// </summary>
    public override int CacheMinutes
    {
        get
        {
            return base.CacheMinutes;
        }
        set
        {
            base.CacheMinutes = value;
            partPlaceholder.CacheMinutes = value;
        }
    }

    #endregion


    /// <summary>
    /// Content loaded event handler.
    /// </summary>
    public override void OnContentLoaded()
    {
        base.OnContentLoaded();

        this.partPlaceholder.ID = this.ID;

        SetupControl();
    }


    /// <summary>
    /// Initializes the control properties.
    /// </summary>
    protected void SetupControl()
    {
        if (this.StopProcessing)
        {
            // Do nothing
        }
        else
        {
            this.partPlaceholder.CheckPermissions = this.CheckPermissions;
            this.partPlaceholder.CacheMinutes = this.CacheMinutes;

            // Load content only when default page template or path is defined
            string templateName = this.PageTemplate;
            string path = this.Path;

            if ((templateName != "") || (path != ""))
            {
                ViewModeEnum viewMode = ViewModeEnum.Unknown;

                // Process template only if the control is on the last hierarchy page
                PageInfo currentPage = this.PagePlaceholder.PageInfo;
                PageInfo usePage = null;
                PageTemplateInfo ti = null;

                if (String.IsNullOrEmpty(path))
                {
                    // Use the same page
                    usePage = this.PagePlaceholder.PageInfo;

                    if (this.UseDefaultTemplateOnSubPages || (currentPage.ChildPageInfo == null) || (currentPage.ChildPageInfo.PageTemplateInfo == null) || (currentPage.ChildPageInfo.PageTemplateInfo.PageTemplateId == 0))
                    {
                        ti = PageTemplateInfoProvider.GetPageTemplateInfo(templateName);
                    }
                }
                else
                {
                    // Resolve the path first
                    path = CMSContext.ResolveCurrentPath(path);
                                        
                    // Get specific page
                    usePage = PageInfoProvider.GetPageInfo(CMSContext.CurrentSiteName, path, CMSContext.PreferredCultureCode, null, false);
                    if (this.PortalManager.ViewMode != ViewModeEnum.LiveSite)
                    {
                        viewMode = ViewModeEnum.Preview;

                        // Get current document content
                        if (usePage != null)
                        {
                            TreeNode node = DocumentHelper.GetDocument(usePage.DocumentId, null);
                            if (node != null)
                            {
                                usePage.LoadVersion(node);
                            }
                        }
                    }

                    // Get the appropriate page template
                    if (String.IsNullOrEmpty(templateName))
                    {
                        ti = usePage.PageTemplateInfo;
                    }
                    else
                    {
                        ti = PageTemplateInfoProvider.GetPageTemplateInfo(templateName);
                    }
                }

                if ((usePage != null) && (ti != null))
                {
                    // If same template as current page, avoid cycling
                    if (ti.PageTemplateId == currentPage.PageTemplateInfo.PageTemplateId)
                    {
                        this.lblError.Text = GetString("WebPart.PagePlaceHolder.CurrentTemplateNotAllowed");
                        this.lblError.Visible = true;
                    }
                    else
                    {
                        usePage = usePage.Clone();
                        usePage.DocumentPageTemplateID = ti.PageTemplateId;
                        usePage.PageTemplateInfo = ti;
                        

                        // Load the current page info with the template and document
                        if (viewMode != ViewModeEnum.Unknown)
                        {
                            this.partPlaceholder.ViewMode = viewMode;
                        }

                        this.partPlaceholder.UsingDefaultPageTemplate = true;
                        this.partPlaceholder.PageLevel = this.PagePlaceholder.PageLevel;
                        this.partPlaceholder.LoadContent(usePage, true);
                    }
                }
            }
        }
    }
}

