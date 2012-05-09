using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

using CMS.PortalControls;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.TreeEngine;
using CMS.UIControls;

using TreeNode = CMS.TreeEngine.TreeNode;

public partial class CMSWebParts_General_EditDocumentLink : CMSAbstractWebPart
{
    #region "Webpart properties"

    /// <summary>
    /// Text of the link.
    /// </summary>
    public string LinkText
    {
        get
        {
            return ValidationHelper.GetString(GetValue("LinkText"), "");
        }
        set
        {
            SetValue("LinkText", value);
        }
    }


    /// <summary>
    /// Indicates if the link should be available only for users who have the access to the CMS Desk and rights to edit the current document.
    /// </summary>
    public bool ShowOnlyWhenAuthorized
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("ShowOnlyWhenAuthorized"), false);
        }
        set
        {
            SetValue("ShowOnlyWhenAuthorized", value);
        }
    }

    #endregion


    #region "Webpart methods"

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
        if (!this.StopProcessing)
        {
            bool show = true;
            TreeNode curDoc = CMSContext.CurrentDocument;

            // Check if permissions should be checked
            if (ShowOnlyWhenAuthorized)
            {
                // Check permissions
                if (!((CMSContext.CurrentUser.IsEditor || CMSContext.CurrentUser.IsGlobalAdministrator) && CMSPage.IsUserAuthorizedPerContent() && (CMSContext.CurrentUser.IsAuthorizedPerDocument(curDoc, NodePermissionsEnum.Modify) == AuthorizationResultEnum.Allowed)))
                {
                    show = false;
                    Visible = false;
                }
            }

            if (show)
            {
                // Create edit link
                StringBuilder sb = new StringBuilder("<a href=\"~/cmsdesk/default.aspx?section=content&amp;action=edit&amp;nodeid=");
                sb.Append(curDoc.NodeID);
                sb.Append("&amp;culture=");
                sb.Append(curDoc.DocumentCulture);
                sb.Append("\">");
                sb.Append(LinkText);
                sb.Append("</a>");
                ltlEditLink.Text = sb.ToString();
            }
        }
    }

    #endregion
}
