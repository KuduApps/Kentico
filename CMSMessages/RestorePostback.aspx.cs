using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Specialized;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.LicenseProvider;
using CMS.UIControls;
using CMS.OutputFilter;
using CMS.URLRewritingEngine;
using CMS.IO;

public partial class CMSMessages_RestorePostback : MessagePage
{
    #region "Variables"

    protected string okText = null;
    protected string cancelText = null;
    protected string mBodyParams = null;

    #endregion

  
    protected void Page_Load(object sender, EventArgs e)
    {
        this.titleElem.TitleText = GetString("RestorePostback.Header");
        this.titleElem.TitleImage = GetImageUrl("Others/Messages/info.png");

        this.lblInfo.Text = GetString("RestorePostback.Info");

        okText = GetString("General.OK");
        cancelText = GetString("General.Cancel");

        // Add the state fields
        SavedFormState state = FormStateHelper.GetSavedState();
        if (state != null)
        {
            // Render the hidden fields for the form items
            NameValueCollection form = state.Form;
            foreach (string name in form.Keys)
            {
                ltlValues.Text += "<input type=\"hidden\" name=\"" + name + "\" value=\"" + HttpUtility.HtmlEncode(form[name]) + "\" />\n";
            }

            state.Delete();
        }
        else
        {
            this.lblInfo.Text = GetString("RestorePostback.InfoNotSaved");
            
            if (Request.RawUrl.IndexOf("/RestorePostback.aspx", StringComparison.InvariantCultureIgnoreCase) < 0)
            {
                mBodyParams = "onload=\"document.location.replace(document.location.href); return false;\"";
            }

            this.plcOK.Visible = false;
            cancelText = GetString("General.OK");
        }
    }


    /// <summary>
    /// Overrided render which removes everything unnecessary.
    /// </summary>
    protected override void Render(HtmlTextWriter writer)
    {
        StringBuilder stringBuilder = new StringBuilder();
        StringWriter stringWriter = new StringWriter(stringBuilder);
        HtmlTextWriter htmlWriter = new HtmlTextWriter(stringWriter);

        base.Render(htmlWriter);

        string html = stringBuilder.ToString();

        // Remove the action attribute
        int start = html.IndexOf("action=\"");
        int end = html.IndexOf("\"", start + 8) + 1;

        html = html.Remove(start, end - start + 1);

        // Remove anything generated at the end of the form
        start = html.IndexOf("##formend##");
        end = html.IndexOf("</form>");

        html = html.Remove(start, end - start);

        // Remove anything generated at the beginning of the form
        start = html.IndexOf("<form");
        start = html.IndexOf(">", start + 1) + 1;
        end = html.IndexOf("##formstart##") + 13;

        html = html.Remove(start, end - start);

        // Write the response
        writer.Write(html);
    }


    /// <summary>
    /// Error handling.
    /// </summary>
    protected override void OnError(EventArgs e)
    {
        base.OnError(e);

        // Delete the saved state
        SavedFormState state = FormStateHelper.GetSavedState();
        if (state != null)
        {
            state.Delete();
        }

        // Redirect to the same page
        Response.Redirect(URLRewriter.RawUrl);
    }


    /// <summary>
    /// Disable handler base tag.
    /// </summary>
    protected override void OnInit(EventArgs e)
    {
        UseBaseTagForHandlerPage = false;
        base.OnInit(e);
    }
}
