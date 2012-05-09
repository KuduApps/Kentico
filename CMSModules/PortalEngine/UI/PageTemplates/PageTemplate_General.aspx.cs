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

using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.PortalEngine;
using CMS.UIControls;
using CMS.ExtendedControls;
using CMS.CMSHelper;

public partial class CMSModules_PortalEngine_UI_PageTemplates_PageTemplate_General : CMSEditTemplatePage
{
    #region "Variables"

    protected string pageTemplateWebParts = "";
    protected bool pageTemplateIsReusable = false;
    private PageTemplateInfo pti = null;

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Get and check template ID
        pageTemplateId = QueryHelper.GetInteger("templateid", 0);
        if (pageTemplateId <= 0)
        {
            return;
        }

        // Register refresh script
        string refreshScript = ScriptHelper.GetScript(@"function RefreshContent() {
                             if ((parent != null) && (parent.Refresh != null)) parent.Refresh();
                             var txtDisplayName = document.getElementById('" + txtTemplateDisplayName.TextBox.ClientID + @"');
                             var wopener = parent.wopener; if ((wopener != null) && (wopener.SetTemplateName)) wopener.SetTemplateName(txtDisplayName.value);
                             }");
        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "pageTemplateRefreshScript", refreshScript);                        

        // Hide teaser
        lblUploadFile.Visible = false;
        UploadFile.Visible = false;

        lblTemplateDisplayName.Text = GetString("Administration-PageTemplate_General.TemplateDisplayName");
        lblTemplateCodeName.Text = GetString("Administration-PageTemplate_General.TemplateCodeName");
        lblTemplateCategory.Text = GetString("Administration-PageTemplate_General.Category");
        lblTemplateDescription.Text = GetString("Administration-PageTemplate_General.TemplateDescription");
        lblTemplateType.Text = GetString("Administration-PageTemplate_General.Type");
        lblShowAsMasterTemplate.Text = GetString("Administration-PageTemplate_General.ShowAsMasterTemplate");
        lblInheritLevels.Text = GetString("Administration-PageTemplate_General.InheritLevels");
        lblUploadFile.Text = GetString("Administration-PageTemplate_General.lblUpload");

        rfvTemplateDisplayName.ErrorMessage = GetString("Administration-PageTemplate_General.ErrorEmptyTemplateDisplayName");
        rfvTemplateCodeName.ErrorMessage = GetString("Administration-PageTemplate_General.ErrorEmptyTemplateCodeName");

        // New item link
        string[,] actions = new string[1, 9];
        actions[0, 0] = HeaderActions.TYPE_SAVEBUTTON;
        actions[0, 1] = GetString("general.save");
        actions[0, 2] = null;
        actions[0, 3] = null;
        actions[0, 4] = null;
        actions[0, 5] = GetImageUrl("CMSModules/CMS_Content/EditMenu/save.png");
        actions[0, 6] = "lnksave_click";
        actions[0, 7] = null;
        actions[0, 8] = "true";

        this.CurrentMaster.HeaderActions.LinkCssClass = "ContentSaveLinkButton";
        this.CurrentMaster.HeaderActions.Actions = actions;
        this.CurrentMaster.HeaderActions.ActionPerformed += new CommandEventHandler(HeaderActions_ActionPerformed);


        if (!RequestHelper.IsPostBack())
        {
            FillDropDown();
        }

        pti = PageTemplateInfoProvider.GetPageTemplateInfo(pageTemplateId);
        if (pti != null)
        {
            if (!RequestHelper.IsPostBack())
            {
                // Load the initial data
                LoadData();

                lvlElem.Value = pti.InheritPageLevels;
                lvlElem.Level = 10;
            }
            else
            {
                // Load just the flags
                pageTemplateIsReusable = pti.IsReusable;
                pageTemplateWebParts = pti.WebParts;
            }

            // Show teaser if needed
            if (!pti.DisplayName.ToLower().StartsWith("ad-hoc"))
            {
                lblUploadFile.Visible = true;
                UploadFile.Visible = true;
                UploadFile.ObjectID = pageTemplateId;
                UploadFile.ObjectType = PortalObjectType.PAGETEMPLATE;
                UploadFile.Category = MetaFileInfoProvider.OBJECT_CATEGORY_THUMBNAIL;
            }

            FileSystemDialogConfiguration config = new FileSystemDialogConfiguration();
            config.DefaultPath = "CMSTemplates";
            config.AllowedExtensions = "aspx";
            config.ShowFolders = false;

            FileSystemSelector.DialogConfig = config;
            FileSystemSelector.AllowEmptyValue = false;
            FileSystemSelector.SelectedPathPrefix = "~/CMSTemplates/";
            FileSystemSelector.ValidationError = GetString("Administration-PageTemplate_General.ErrorEmptyTemplateFileName");

            // Script for dynamic hiding of inherited levels
            string script = @"
            function HideOrShowInheritLevels() {                
                var tr = document.getElementById('" + inheritLevels.ClientID + @"');                
                if (tr) {                     
                    var checkbox = document.getElementById('" + chkShowAsMasterTemplate.ClientID + @"') ;                               
                    if(checkbox != null) {
                        if(checkbox.checked == 1) {
                            tr.style.display = 'none';
                        }
                        else {
                            tr.style.display = '';
                        }
                    }
                }            
            }";

            // Hide inherited levels
            if (chkShowAsMasterTemplate.Checked)
            {
                inheritLevels.Style.Add("display", "none");
            }
            else
            {
                inheritLevels.Style.Clear();
            }

            // Register script to page
            ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "showOrHide", ScriptHelper.GetScript(script));

            chkShowAsMasterTemplate.Attributes.Add("onclick", "HideOrShowInheritLevels();");
            //Page.Focus();
            //Page.Form.Focus();
            //ScriptHelper.RegisterStartupScript(this, typeof(string), "focus", ScriptHelper.GetScript("document.getElementById('" +Page.Form.ClientID  + "').focus();"));
        }
    }

    #endregion


    #region "Methods"

    /// <summary>
    /// Fills the page type dropdown list.
    /// </summary>
    protected void FillDropDown()
    {
        drpPageType.Items.Add(new ListItem(GetString("Administration-PageTemplate_General.PortalPage"), "portal"));
        drpPageType.Items.Add(new ListItem(GetString("Administration-PageTemplate_General.AspxPage"), "aspx"));
        drpPageType.Items.Add(new ListItem(GetString("PageTemplate.CombinedPage"), "combined"));
        
        // Hide dashboard type if current template is edited from CMSDesk
        if (!QueryHelper.GetBoolean("dialog", false))
        {
            drpPageType.Items.Add(new ListItem(GetString("PageTemplate.Dashboard"), "dashboard"));
        }
    }


    /// <summary>
    /// Gets the selected page template mode.
    /// </summary>
    private PageTemplateTypeEnum SelectedPageType
    {
        get
        {
            switch (drpPageType.SelectedValue)
            {
                case "portal":
                    return PageTemplateTypeEnum.Portal;

                case "aspx":
                    return PageTemplateTypeEnum.Aspx;

                case "combined":
                    return PageTemplateTypeEnum.AspxPortal;

                case "dashboard":
                    return PageTemplateTypeEnum.Dashboard;

                default:
                    return PageTemplateTypeEnum.Unknown;
            }
        }
    }


    /// <summary>
    /// Load data of edited module from DB into TextBoxes.
    /// </summary>
    protected void LoadData()
    {
        txtTemplateDisplayName.Text = pti.DisplayName;
        txtTemplateCodeName.Text = pti.CodeName;
        txtTemplateDescription.Text = pti.Description;

        // Select category
        categorySelector.Value = pti.CategoryID.ToString();

        switch (pti.PageTemplateType)
        {
            case PageTemplateTypeEnum.Portal:
                // Portal only
                drpPageType.SelectedValue = "portal";
                break;

            case PageTemplateTypeEnum.Aspx:
                // ASPX only
                drpPageType.SelectedValue = "aspx";
                FileSystemSelector.Value = pti.FileName;
                break;


            case PageTemplateTypeEnum.AspxPortal:
                // Combined
                drpPageType.SelectedValue = "combined";
                FileSystemSelector.Value = pti.FileName;
                break;

            case PageTemplateTypeEnum.Dashboard:
                drpPageType.SelectedValue = "dashboard";
                break;
        }

        radAspx_CheckedChange(null, null);

        chkShowAsMasterTemplate.Checked = pti.ShowAsMasterTemplate;
        pageTemplateIsReusable = pti.IsReusable;
        pageTemplateWebParts = pti.WebParts;
    }


    /// <summary>
    /// Shows controls for layout editing and hides controls for template file editing.
    /// </summary>
    /// <param name="portal">Show portal controls</param>
    /// <param name="aspx">Show ASPX controls</param>
    protected void SetPageType(PageTemplateTypeEnum type)
    {
        plcAspx.Visible = ((type == PageTemplateTypeEnum.Aspx) || (type == PageTemplateTypeEnum.AspxPortal));
        plcPortal.Visible = (type == PageTemplateTypeEnum.Portal);
    }

    #endregion


    #region "Control events"

    /// <summary>
    /// Save button action.
    /// </summary>
    protected void HeaderActions_ActionPerformed(object sender, CommandEventArgs e)
    {
        switch (e.CommandName.ToLower())
        {
            case "lnksave_click":
                // Template has to exist
                if (pti == null)
                {
                    return;
                }

                // Limit text length
                txtTemplateCodeName.Text = TextHelper.LimitLength(txtTemplateCodeName.Text.Trim(), 100, "");
                txtTemplateDisplayName.Text = TextHelper.LimitLength(txtTemplateDisplayName.Text.Trim(), 200, "");

                // Finds whether required fields are not empty
                string result = String.Empty;

                result = new Validator().NotEmpty(txtTemplateDisplayName.Text, GetString("Administration-PageTemplate_General.ErrorEmptyTemplateDisplayName")).NotEmpty(txtTemplateCodeName.Text, GetString("Administration-PageTemplate_General.ErrorEmptyTemplateCodeName"))
                     .IsCodeName(txtTemplateCodeName.Text, GetString("general.invalidcodename"))
                     .Result;

                if ((result == String.Empty) && (SelectedPageType == PageTemplateTypeEnum.Aspx || SelectedPageType == PageTemplateTypeEnum.AspxPortal))
                {
                    if (!FileSystemSelector.IsValid())
                    {
                        result = FileSystemSelector.ValidationError;
                    }
                }

                // If name changed, check if new name is unique
                if ((result == String.Empty) && (String.Compare(pti.CodeName, txtTemplateCodeName.Text, true) != 0))
                {
                    if (PageTemplateInfoProvider.PageTemplateNameExists(txtTemplateCodeName.Text))
                    {
                        result = GetString("general.codenameexists");
                    }
                }

                if (result == "")
                {
                    // Update page template info                        
                    pti.DisplayName = txtTemplateDisplayName.Text;
                    pti.CodeName = txtTemplateCodeName.Text;
                    pti.Description = txtTemplateDescription.Text;
                    pti.CategoryID = Convert.ToInt32(categorySelector.Value);

                    if (SelectedPageType == PageTemplateTypeEnum.Portal)
                    {
                        pti.IsPortal = true;
                        pti.FileName = String.Empty;

                        // Save inherit levels
                        if (!chkShowAsMasterTemplate.Checked)
                        {
                            pti.InheritPageLevels = ValidationHelper.GetString(lvlElem.Value, "");
                        }
                        else
                        {
                            pti.InheritPageLevels = "/";
                        }

                        // Show hide inherit levels radio buttons
                        pti.ShowAsMasterTemplate = chkShowAsMasterTemplate.Checked;
                    }
                    else
                    {
                        // ASPX page templates
                        pti.IsPortal = false;
                        pti.FileName = FileSystemSelector.Value.ToString();
                        pti.ShowAsMasterTemplate = false;
                        pti.InheritPageLevels = "";
                    }

                    pti.PageTemplateType = SelectedPageType;

                    pti.IsReusable = pageTemplateIsReusable;
                    pti.WebParts = pageTemplateWebParts;

                    try
                    {
                        // Save the template and update the header
                        PageTemplateInfoProvider.SetPageTemplateInfo(pti);
                        ScriptHelper.RegisterStartupScript(this, typeof(string), "pageTemplateSaveScript", ScriptHelper.GetScript("RefreshContent()"));
                        lblInfo.Text = ResHelper.GetString("General.ChangesSaved");
                    }
                    catch (Exception ex)
                    {
                        lblError.Text = ex.Message;
                        lblError.Visible = true;
                    }
                }
                else
                {

                    rfvTemplateDisplayName.Visible = false;
                    rfvTemplateCodeName.Visible = false;

                    lblError.Visible = true;
                    lblError.Text = result;
                }
                break;
        }
    }


    /// <summary>
    /// Handles onCheckedChange event of radAspx radio button.
    /// </summary>
    protected void radAspx_CheckedChange(object sender, EventArgs e)
    {
        if (SelectedPageType == PageTemplateTypeEnum.Portal)
        {
            SetPageType(PageTemplateTypeEnum.Portal);
        }
        else if (SelectedPageType == PageTemplateTypeEnum.AspxPortal)
        {
            SetPageType(PageTemplateTypeEnum.AspxPortal);
        }
        else if (SelectedPageType == PageTemplateTypeEnum.Aspx)
        {
            SetPageType(PageTemplateTypeEnum.Aspx);
        }
        else
        {
            SetPageType(PageTemplateTypeEnum.Dashboard);
        }
    }

    #endregion
}
