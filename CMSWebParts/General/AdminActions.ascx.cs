using System;
using System.Data;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

using CMS.PortalControls;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.UIControls;
using CMS.TreeEngine;

public partial class CMSWebParts_General_AdminActions : CMSAbstractWebPart
{
    #region "Properties"

    /// <summary>
    /// Display only to global administrator.
    /// </summary>
    public bool DisplayOnlyToGlobalAdministrator
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("DisplayOnlyToGlobalAdministrator"), false);
        }
        set
        {
            this.SetValue("DisplayOnlyToGlobalAdministrator", value);
        }
    }


    /// <summary>
    /// Check permissions.
    /// </summary>
    public bool CheckPermissions
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("CheckPermissions"), false);
        }
        set
        {
            this.SetValue("CheckPermissions", value);
        }
    }


    /// <summary>
    /// Separator.
    /// </summary>
    public string Separator
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("Separator"), null);
        }
        set
        {
            this.SetValue("Separator", value);
        }
    }


    /// <summary>
    /// Show cms desk link.
    /// </summary>
    public bool ShowCMSDeskLink
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("ShowCMSDeskLink"), true);
        }
        set
        {
            this.SetValue("ShowCMSDeskLink", value);
        }
    }


    /// <summary>
    /// CMS Desk link text.
    /// </summary>
    public string CMSDeskLinkText
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("CMSDeskLinkText"), "CMS Desk");
        }
        set
        {
            this.SetValue("CMSDeskLinkText", value);
        }
    }


    /// <summary>
    /// CMS Desk text.
    /// </summary>
    public string CMSDeskText
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("CMSDeskText"), "{0}");
        }
        set
        {
            this.SetValue("CMSDeskText", value);
        }
    }


    /// <summary>
    /// Show site manager link.
    /// </summary>
    public bool ShowCMSSiteManagerLink
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("ShowCMSSiteManagerLink"), true);
        }
        set
        {
            this.SetValue("ShowCMSSiteManagerLink", value);
        }
    }


    /// <summary>
    /// Site manager link text.
    /// </summary>
    public string CMSSiteManagerLinkText
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("CMSSiteManagerLinkText"), "Site manager");
        }
        set
        {
            this.SetValue("CMSSiteManagerLinkText", value);
        }
    }


    /// <summary>
    /// Site manager text.
    /// </summary>
    public string CMSSiteManagerText
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("CMSSiteManagerText"), "{0}");
        }
        set
        {
            this.SetValue("CMSSiteManagerText", value);
        }
    }


    /// <summary>
    /// Show edit document link.
    /// </summary>
    public bool ShowEditDocumentLink
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("ShowEditDocumentLink"), false);
        }
        set
        {
            this.SetValue("ShowEditDocumentLink", value);
        }
    }


    /// <summary>
    /// Edit document link text.
    /// </summary>
    public string EditDocumentLinkText
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("EditDocumentLinkText"), "Edit document");
        }
        set
        {
            this.SetValue("EditDocumentLinkText", value);
        }
    }


    /// <summary>
    /// Edit document text.
    /// </summary>
    public string EditDocumentText
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("EditDocumentText"), "{0}");
        }
        set
        {
            this.SetValue("EditDocumentText", value);
        }
    }


    /// <summary>
    /// Default user name for logon page.
    /// </summary>
    public string DefaultUserName
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("DefaultUserName"), null);
        }
        set
        {
            this.SetValue("DefaultUserName", value);
        }
    }

    #endregion


    #region "Methods"

    /// <summary>
    /// Content loaded event handler.
    /// </summary>
    public override void OnContentLoaded()
    {
        base.OnContentLoaded();
        SetupControl();
    }


    /// <summary>
    /// Initializes the control properties.
    /// </summary>
    protected void SetupControl()
    {
        if (this.StopProcessing)
        {
            // Do not process
        }
        else
        {
            CurrentUserInfo uinfo = CMSContext.CurrentUser;

            if (uinfo.IsGlobalAdministrator || !DisplayOnlyToGlobalAdministrator)
            {
                // Create new string builder for links
                StringBuilder sb = new StringBuilder();

                // Store current site name
                string curSiteName = CMSContext.CurrentSiteName;
                // Get default user name
                string queryStringKey = (string.IsNullOrEmpty(DefaultUserName)) ? null : "?username=" + DefaultUserName;
                bool separatorNeeded = false;

                // If cms desk link is shown
                if (ShowCMSDeskLink && (!CheckPermissions || uinfo.CheckEditor(curSiteName)))
                {
                    string url = ResolveUrl("~/cmsdesk/default.aspx");
                    if (!string.IsNullOrEmpty(DefaultUserName))
                    {
                        url = URLHelper.AddParameterToUrl(url, "username", DefaultUserName);
                    }
                    if (CMSContext.CurrentPageInfo != null)
                    {
                        url = URLHelper.AddParameterToUrl(url, "nodeid", CMSContext.CurrentPageInfo.NodeId.ToString());
                    }
                    sb.AppendFormat(CMSDeskText, string.Concat("<a href=\"", URLHelper.EncodeQueryString(url), "\">", CMSDeskLinkText, "</a>"));

                    separatorNeeded = true;
                }

                // If site manager link is shown
                if (ShowCMSSiteManagerLink && (!CheckPermissions || uinfo.UserSiteManagerAdmin))
                {
                    // Check if separator needed
                    if (separatorNeeded)
                    {
                        sb.Append(Separator);
                    }

                    string url = ResolveUrl("~/cmssitemanager/default.aspx");
                    if (!string.IsNullOrEmpty(DefaultUserName))
                    {
                        url = URLHelper.AddParameterToUrl(url, "username", DefaultUserName);
                    }
                    sb.AppendFormat(CMSSiteManagerText, string.Concat("<a href=\"", url, "\">", CMSSiteManagerLinkText, "</a>"));

                    separatorNeeded = true;
                }

                // If edit document link is shown
                if (ShowEditDocumentLink && (!CheckPermissions || (uinfo.CheckEditor(curSiteName) && CMSPage.IsUserAuthorizedPerContent() && (uinfo.IsAuthorizedPerDocument(CMSContext.CurrentDocument, NodePermissionsEnum.Modify) == AuthorizationResultEnum.Allowed))))
                {
                    // Check if separator needed
                    if (separatorNeeded)
                    {
                        sb.Append(" " + Separator + " ");
                    }

                    sb.AppendFormat(EditDocumentText, string.Concat("<a href=\"", URLHelper.EncodeQueryString(UIHelper.GetDocumentEditUrl(CMSContext.CurrentDocument.NodeID, CMSContext.CurrentDocumentCulture.CultureCode)), "\">", EditDocumentLinkText, "</a>"));
                }

                ltlAdminActions.Text = sb.ToString();
            }
        }
    }


    /// <summary>
    /// Reloads the control data.
    /// </summary>
    public override void ReloadData()
    {
        base.ReloadData();

        SetupControl();
    }

    #endregion
}