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

using CMS.PortalControls;
using CMS.GlobalHelper;
using CMS.TreeEngine;
using CMS.CMSHelper;

public partial class CMSWebParts_Membership_Logon_currentuser : CMSAbstractWebPart
{
    #region "Public properties"

    /// <summary>
    /// Gets or sets the text of label which is displayed in front of user info text.
    /// </summary>
    public string LabelText
    {
        get
        {
            return DataHelper.GetNotEmpty(this.GetValue("LabelText"), GetString("Webparts_Membership_CurrentUser.CurrentUser"));
        }
        set
        {
            this.SetValue("LabelText", value);
            this.lblLabel.Text = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether this webpart is displayed only when user is authenticated.
    /// </summary>
    public bool ShowOnlyWhenAuthenticated
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("ShowOnlyWhenAuthenticated"), true);
        }
        set
        {
            this.SetValue("ShowOnlyWhenAuthenticated", value);
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether label text is displayed.
    /// </summary>
    public bool ShowLabelText
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("ShowLabelText"), true);
        }
        set
        {
            this.SetValue("ShowLabelText", value);
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether user full name is displayed.
    /// </summary>
    public bool ShowUserFullName
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("ShowUserFullName"), true);
        }
        set
        {
            this.SetValue("ShowUserFullName", value);
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether use name is displayed.
    /// </summary>
    public bool ShowUserName
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("ShowUserName"), true);
        }
        set
        {
            this.SetValue("ShowUserName", value);
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether the username should be hidden for external users.
    /// </summary>
    public bool HideUserNameForExternalUsers
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("HideUserNameForExternalUsers"), false);
        }
        set
        {
            this.SetValue("HideUserNameForExternalUsers", value);
        }
    }


    /// <summary>
    /// Gets or sets url used for authenticated user.
    /// </summary>
    public string AuthenticatedLinkUrl
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("AuthenticatedLinkUrl"), "");
        }
        set
        {
            this.SetValue("AuthenticatedLinkUrl", value);
        }
    }


    /// <summary>
    /// Gets or sets url used for public user.
    /// </summary>
    public string PublicLinkUrl
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("PublicLinkUrl"), "");
        }
        set
        {
            this.SetValue("PublicLinkUrl", value);
        }
    }


    /// <summary>
    /// Gets or sets the name of css for label which is displayed in front of user info text.
    /// </summary>
    public string LabelCSS
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("LabelCSS"), this.lblLabel.CssClass);
        }
        set
        {
            this.SetValue("LabelCSS", value);
            this.lblLabel.CssClass = value;
        }
    }


    /// <summary>
    /// Gets or sets the name of css which is used for user info label.
    /// </summary>
    public string UserCSS
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("UserCSS"), "CurrentUserName");
        }
        set
        {
            this.SetValue("UserCSS", value);
        }
    }

    #endregion


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
            this.EnableViewState = false;
            // Set labels text
            this.lblLabel.Text = this.LabelText;
            this.lblLabel.Visible = this.ShowLabelText;

            // According to visibility writeout fullname and username
            string userInfo = "<span class=\"" + this.UserCSS + "\">";

            // Display full name
            if (this.ShowUserFullName)
            {
                userInfo += HTMLHelper.HTMLEncode(CMSContext.CurrentUser.FullName);

                // Display user name
                if (this.ShowUserName && !(CMSContext.CurrentUser.IsExternal && this.HideUserNameForExternalUsers))
                {
                    userInfo += " (" + HTMLHelper.HTMLEncode(Functions.GetFormattedUserName(CMSContext.CurrentUser.UserName, true)) + ") ";
                }
            }
            // Display user name
            else if (this.ShowUserName && !(CMSContext.CurrentUser.IsExternal && this.HideUserNameForExternalUsers))
            {
                userInfo += HTMLHelper.HTMLEncode(Functions.GetFormattedUserName(CMSContext.CurrentUser.UserName, true));
            }

            userInfo += "</span>";

            // Set sign in or sign out url to the username link or hide it
            if (!CMSContext.CurrentUser.IsPublic())
            {
                if (this.AuthenticatedLinkUrl != String.Empty)
                {
                    userInfo = "<a href=\"" + this.AuthenticatedLinkUrl + "\">" + userInfo + "</a>";
                }
            }
            else
            {
                if (this.PublicLinkUrl != String.Empty)
                {
                    userInfo = "<a href=\"" + this.PublicLinkUrl + "\">" + userInfo + "</a>";
                }
            }

            ltrSignLink.Text = userInfo;

            // Set label CSS class
            this.lblLabel.CssClass = this.LabelCSS;
            this.lblLabel.Text = this.LabelText;
        }
    }


    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        this.Visible = true;
    }


    /// <summary>
    /// OnPreRender override.
    /// </summary>
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        SetupControl();
        // Set visibility with according to property setting and current user status
        this.Visible = (!this.ShowOnlyWhenAuthenticated || !CMSContext.CurrentUser.IsPublic());
    }
}
