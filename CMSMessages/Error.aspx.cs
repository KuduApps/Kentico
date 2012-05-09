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

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.LicenseProvider;
using CMS.UIControls;

public partial class CMSMessages_Error : MessagePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!CMSAppBase.ConnectionAvailable)
        {
            // Could not connect to the database
            CSSHelper.MinifyCurrentRequest = false;

            this.titleElem.TitleText = "SQL Connection error";
            this.lblInfo.Text = "The application could not connect to the database, please check the connection string in the web.config file and SQL server availability.<br /><br /><strong>Original error:</strong><br /><br />" + CMSAppBase.ConnectionErrorMessage;
        }
        else
        {
            // Get title from URL
            string title = QueryHelper.GetText("title", "");

            if (String.IsNullOrEmpty(title))
            {
                // Get title from current context
                title = ValidationHelper.GetString(this.Context.Items["title"], String.Empty);
            }

            if (title != "")
            {
                // Display custom title
                this.titleElem.TitleText = title;
            }
            else
            {
                // Display general title
                this.titleElem.TitleText = GetString("Error.Header");
            }

            // Set message text
            string pathError = QueryHelper.GetText("aspxerrorpath", "");
            if (pathError != "")
            {
                // Display path error message
                this.lblInfo.Text = String.Format(GetString("Error.Info"), pathError);
            }
            else
            {
                // Get message from URL
                string text = QueryHelper.GetText("text", "");

                if (String.IsNullOrEmpty(text))
                {
                    // Get message from current context
                    text = ValidationHelper.GetString(this.Context.Items["text"], String.Empty);
                }

                // Display custom error message
                this.lblInfo.Text = text;
            }

            // Set button
            bool cancel = QueryHelper.GetBoolean("cancel", false);
            if (cancel)
            {
                // Display Cancel button
                this.btnCancel.Visible = true;
                this.btnCancel.Text = GetString("General.Cancel");
            }
            else
            {
                if (QueryHelper.GetBoolean("backlink", true))
                {
                    // Display link to home page
                    this.lnkBack.Visible = true;
                    this.lnkBack.Text = GetString("Error.Back");
                    this.lnkBack.NavigateUrl = "~/";
                }
            }
        }

        this.titleElem.TitleImage = GetImageUrl("Others/Messages/error.png");
    }
}
