using System;
using System.Data;

using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.UIControls;

public partial class CMSSiteManager_Development_WebTemplates_WebTemplate_Edit : SiteManagerPage
{
    #region "Protected variables"

    protected int webTemplateId = 0;
    protected WebTemplateInfo currentWebTemplateInfo = null;

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        rfvWebTemplateDisplayName.ErrorMessage = GetString("Administration-WebTemplate_New.ErrorEmptyWebTemplateDisplayName");
        rfvWebTemplateName.ErrorMessage = GetString("Administration-WebTemplate_New.ErrorEmptyWebTemplateName");
        rfvWebTemplateFileName.ErrorMessage = GetString("Administration-PageLayout_New.ErrorEmptyWebTemplateFileName");
        rfvWebTemplateDescription.ErrorMessage = GetString("Administration-PageLayout_New.ErrorEmptyWebTemplateDescription");

        // Gets 'webtemplateid' from querystring
        webTemplateId = QueryHelper.GetInteger("webtemplateid", 0);

        if (!RequestHelper.IsPostBack())
        {
            if (!string.IsNullOrEmpty(Request.QueryString["saved"]))
            {
                lblInfo.Text = GetString("General.ChangesSaved");
            }
        }

        string currentWebTemplate = GetString("Administration-WebTemplate_New.CurrentWebTemplate");
        string title = GetString("Administration-WebTemplate_New.NewWebTemplate");
        string image = GetImageUrl("Objects/CMS_WebTemplate/new.png");
        if (webTemplateId > 0)
        {
            currentWebTemplateInfo = WebTemplateInfoProvider.GetWebTemplateInfo(webTemplateId);
            // Set edited object
            EditedObject = currentWebTemplateInfo;

            if (currentWebTemplateInfo != null)
            {
                if (!RequestHelper.IsPostBack())
                {
                    txtWebTemplateDisplayName.Text = currentWebTemplateInfo.WebTemplateDisplayName;
                    txtWebTemplateName.Text = currentWebTemplateInfo.WebTemplateName;
                    txtWebTemplateFileName.Text = currentWebTemplateInfo.WebTemplateFileName;
                    txtWebTemplateDescription.Text = currentWebTemplateInfo.WebTemplateDescription;
                    ucLicenseSelector.Value = currentWebTemplateInfo.WebTemplateLicenses;
                    ucLicensePackageSelector.Value = currentWebTemplateInfo.WebTemplatePackages;

                    // Init file uploader
                    attachmentFile.ObjectID = webTemplateId;
                    attachmentFile.ObjectType = SiteObjectType.WEBTEMPLATE;
                    attachmentFile.Category = MetaFileInfoProvider.OBJECT_CATEGORY_THUMBNAIL;
                }
                // Update title for editing
                currentWebTemplate = currentWebTemplateInfo.WebTemplateDisplayName;
            }
            title = GetString("Administration-WebTemplate_New.EditWebTemplate");
            image = GetImageUrl("Objects/CMS_WebTemplate/object.png");
        }
        else
        {
            lblUploadFile.Visible = false;
            attachmentFile.Visible = false;
        }

        // Initialize master page elements
        InitializeMasterPage(currentWebTemplate, title, image);
    }


    protected override void OnPreRender(EventArgs e)
    {
        lblError.Visible = !string.IsNullOrEmpty(lblError.Text);
        lblInfo.Visible = !string.IsNullOrEmpty(lblInfo.Text);
    }

    #endregion


    #region "Private methods"

    /// <summary>
    /// Initializes the master page elements.
    /// </summary>
    private void InitializeMasterPage(string currentWebTemplate, string title, string image)
    {
        // Set title
        CurrentMaster.Title.TitleText = title;
        CurrentMaster.Title.TitleImage = image;
        CurrentMaster.Title.HelpTopicName = "newedit_web_template";
        CurrentMaster.Title.HelpName = "helpTopic";

        // Set breadcrumbs
        string[,] pageTitleTabs = new string[2, 3];
        pageTitleTabs[0, 0] = GetString("Administration-WebTemplate_List.Title");
        pageTitleTabs[0, 1] = "~/CMSSiteManager/Development/WebTemplates/WebTemplate_List.aspx";
        pageTitleTabs[0, 2] = string.Empty;
        pageTitleTabs[1, 0] = currentWebTemplate;
        pageTitleTabs[1, 1] = string.Empty;
        pageTitleTabs[1, 2] = string.Empty;

        CurrentMaster.Title.Breadcrumbs = pageTitleTabs;
    }

    #endregion


    #region "Button handling"

    /// <summary>
    /// Handles btnOK's OnClick event - Update web template info DB.
    /// </summary>
    protected void btnOK_Click(object sender, EventArgs e)
    {
        // Finds whether required fields are not empty
        string result = new Validator().NotEmpty(txtWebTemplateDisplayName.Text, GetString("Administration-WebTemplate_New.ErrorEmptyWebTemplateDisplayName"))
            .NotEmpty(txtWebTemplateName.Text, GetString("Administration-WebTemplate_New.ErrorEmptyWebTemplateName"))
            .NotEmpty(txtWebTemplateFileName.Text, GetString("Administration-PageLayout_New.ErrorEmptyWebTemplateFileName"))
            .NotEmpty(txtWebTemplateDescription.Text, GetString("Administration-PageLayout_New.ErrorEmptyWebTemplateDescription")).Result;

        if (!ucLicenseSelector.IsValid())
        {
            result = GetString("Administration-WebTemplate_New.ErrorEmptyWebTemplateNameLicenses");
        }

        if (result == string.Empty)
        {
            WebTemplateInfo wi = WebTemplateInfoProvider.GetWebTemplateInfo(txtWebTemplateName.Text);
            // Check if entered code name is unique
            if (wi == null || wi.WebTemplateId == webTemplateId)
            {
                // New web template info
                if ((wi == null) && (currentWebTemplateInfo == null))
                {
                    wi = new WebTemplateInfo();
                    DataSet ds = WebTemplateInfoProvider.GetWebTemplates(null, null, 0, "WebTemplateID", false);
                    if (!DataHelper.DataSourceIsEmpty(ds))
                    {
                        wi.WebTemplateOrder = ds.Tables[0].Rows.Count + 1;
                    }
                    else
                    {
                        wi.WebTemplateOrder = 1;
                    }
                }
                // Current template info 
                else
                {
                    wi = currentWebTemplateInfo;
                }

                wi.WebTemplateId = webTemplateId;
                wi.WebTemplateDisplayName = txtWebTemplateDisplayName.Text;
                wi.WebTemplateName = txtWebTemplateName.Text;
                wi.WebTemplateDescription = txtWebTemplateDescription.Text;
                wi.WebTemplateFileName = txtWebTemplateFileName.Text;
                wi.WebTemplateLicenses = (string)ucLicenseSelector.Value;
                wi.WebTemplatePackages = (string)ucLicensePackageSelector.Value;
                try
                {
                    WebTemplateInfoProvider.SetWebTemplateInfo(wi);
                    URLHelper.Redirect("WebTemplate_Edit.aspx?webtemplateid=" + wi.WebTemplateId + "&saved=1");
                }
                catch (Exception ex)
                {
                    // WebTemplateInfoProvider doesn't make any unique controls
                    lblError.Text = ex.Message;
                }
            }
            else
            {
                lblError.Text = GetString("Administration-WebTemplate_New.ErrorWebTemplateNameNotUnique");
            }
        }
        else
        {
            lblError.Text = result;
        }
    }

    #endregion
}
