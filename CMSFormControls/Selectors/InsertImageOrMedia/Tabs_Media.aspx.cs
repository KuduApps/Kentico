using System;
using System.Web.UI;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.ExtendedControls;
using CMS.CMSHelper;
using CMS.SiteProvider;

public partial class CMSFormControls_Selectors_InsertImageOrMedia_Tabs_Media : CMSModalPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string source = QueryHelper.GetString("source", "");
        MediaSourceEnum src = CMSDialogHelper.GetMediaSource(source);

        if (src == MediaSourceEnum.Content)
        {
            // Check site availability
            if (!ResourceSiteInfoProvider.IsResourceOnSite("CMS.Content", CMSContext.CurrentSiteName))
            {
                RedirectToResourceNotAvailableOnSite("CMS.Content");
            }
        }

        // Check UIProfile
        string output = QueryHelper.GetString("output", "");
        bool checkUI = true;
        if ((output == "copy") || (output == "move") || (output == "link") || (output == "linkdoc") || (output == "relationship") || (output == "selectpath"))
        {
            checkUI = false;
        }

        if (checkUI)
        {
            string errorMessage = "";

            OutputFormatEnum outputFormat = CMSDialogHelper.GetOutputFormat(output, QueryHelper.GetBoolean("link", false));
            if ((outputFormat == OutputFormatEnum.HTMLLink) && !CMSContext.CurrentUser.IsAuthorizedPerUIElement("CMS.WYSIWYGEditor", "InsertLink"))
            {
                errorMessage = "InsertLink";
            }
            else if ((outputFormat == OutputFormatEnum.HTMLMedia) && !CMSContext.CurrentUser.IsAuthorizedPerUIElement("CMS.WYSIWYGEditor", "InsertImageOrMedia"))
            {
                errorMessage = "InsertImageOrMedia";
            }

            if (errorMessage != "")
            {
                RedirectToCMSDeskUIElementAccessDenied("CMS.WYSIWYGEditor", errorMessage);
                return;
            }

            switch (src)
            {
                case MediaSourceEnum.DocumentAttachments:
                case MediaSourceEnum.Attachment:
                    if (!CMSContext.CurrentUser.IsAuthorizedPerUIElement("CMS.MediaDialog", "AttachmentsTab"))
                    {
                        errorMessage = "AttachmentsTab";
                    }
                    break;

                case MediaSourceEnum.Content:
                    if (!CMSContext.CurrentUser.IsAuthorizedPerUIElement("CMS.MediaDialog", "ContentTab"))
                    {
                        errorMessage = "ContentTab";
                    }
                    break;
            }
            if (errorMessage != "")
            {
                RedirectToCMSDeskUIElementAccessDenied("CMS.MediaDialog", errorMessage);
                return;
            }
        }

        if (QueryHelper.ValidateHash("hash"))
        {
            ScriptHelper.RegisterJQuery(Page);
            CMSDialogHelper.RegisterDialogHelper(Page);
            ScriptHelper.RegisterStartupScript(Page, typeof(Page), "InitResizers", ScriptHelper.GetScript("InitResizers();"));

            linkMedia.InitFromQueryString();
        }
        else
        {
            string url = ResolveUrl("~/CMSMessages/Error.aspx?title=" + GetString("dialogs.badhashtitle") + "&text=" + GetString("dialogs.badhashtext") + "&cancel=1");
            ltlScript.Text = ScriptHelper.GetScript("if (window.parent != null) { window.parent.location = '" + url + "' }");
        }
    }
}
