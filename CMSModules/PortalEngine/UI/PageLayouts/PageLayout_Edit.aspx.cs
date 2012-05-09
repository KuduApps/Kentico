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
using CMS.CMSHelper;
using CMS.SettingsProvider;
using CMS.UIControls;
using CMS.IO;
using CMS.ExtendedControls;
using CMS.PortalEngine;

// Set edited object
[EditedObject("cms.layout", "layoutid")]

public partial class CMSModules_PortalEngine_UI_PageLayouts_PageLayout_Edit : SiteManagerPage
{
    #region "Variables"

    protected int layoutId = 0;
    protected string[,] breadcrumbs = new string[2, 3];
    protected LayoutInfo li = null;

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        lbCodeName.Text = GetString("Administration-PageLayout_New.LayoutCodeName");
        lbLayoutDisplayName.Text = GetString("Administration-PageLayout_New.LayoutDisplayName");
        lbLayoutDescription.Text = GetString("Administration-PageLayout_New.LayoutDescription");
        lbLayoutCode.Text = GetString("Administration-PageLayout_New.LayoutCode");
        rfvLayoutDisplayName.ErrorMessage = GetString("Administration-PageLayout_New.ErrorEmptyLayoutDisplayName");
        rfvLayoutCode.ErrorMessage = GetString("Administration-PageLayout_New.ErrorEmptyLayoutCode");
        rfvCodeName.ErrorMessage = GetString("Administration-PageLayout_New.ErrorEmptyLayoutCodeName");
        lblType.Text = GetString("PageLayout.Type");

        string lang = DataHelper.GetNotEmpty(SettingsHelper.AppSettings["CMSProgrammingLanguage"], "C#");
        ltlDirectives.Text = "&lt;%@ Control Language=\"" + lang + "\" ClassName=\"Simple\" Inherits=\"CMS.PortalControls.CMSAbstractLayout\" %&gt;<br />&lt;%@ Register Assembly=\"CMS.PortalControls\" Namespace=\"CMS.PortalControls\" TagPrefix=\"cc1\" %&gt;";

        if (this.drpType.Items.Count == 0)
        {
            drpType.Items.Add(new ListItem(GetString("TransformationType.Ascx"), TransformationTypeEnum.Ascx.ToString()));
            drpType.Items.Add(new ListItem(GetString("TransformationType.Html"), TransformationTypeEnum.Html.ToString()));
        }

        if (EditedObject != null)
        {
            li = EditedObject as LayoutInfo;
            if (!RequestHelper.IsPostBack())
            {
                // Load the form
                LoadData();
            }
        }
        else
        {
            if (tbCodeName.Text == "")
            {
                // default layout code content
                tbLayoutCode.Text = "<cms:CMSWebPartZone ID=\"zoneCenter\" runat=\"server\" />";
            }
        }


        // Gets page layout info of specified 'layoutid'
        if (li != null)
        {
            lblUploadFile.Text = GetString("Administration-PageLayout_New.PageLayoutThumbnail");

            UploadFile.ObjectID = li.LayoutId;
            UploadFile.Category = MetaFileInfoProvider.OBJECT_CATEGORY_THUMBNAIL;
            UploadFile.ObjectType = PortalObjectType.PAGELAYOUT;

            this.plcFile.Visible = true;
        }

        InitializeHeaderActions(li);

        if (Request.QueryString["saved"] != null && Request.QueryString["saved"] == "1")
        {
            lblInfo.Visible = true;
            lblInfo.Text = GetString("General.ChangesSaved");

            // Reload header if changes were saved
            ScriptHelper.RefreshTabHeader(Page, null);
        }

        if (li == null)
        {
            string layouts = GetString("Administration-PageLayout_New.NewLayout");
            string title = GetString("Administration-PageLayout_New.Title");
            string image = GetImageUrl("Objects/CMS_Layout/new.png");
            string currentLayout = GetString("Administration-PageLayout_New.CurrentLayout");

            breadcrumbs[0, 0] = layouts;
            breadcrumbs[0, 1] = ResolveUrl("PageLayout_List.aspx");
            breadcrumbs[0, 2] = "";
            breadcrumbs[1, 0] = currentLayout;
            breadcrumbs[1, 1] = "";
            breadcrumbs[1, 2] = "";

            this.CurrentMaster.Title.Breadcrumbs = breadcrumbs;
            this.CurrentMaster.Title.TitleText = title;
            this.CurrentMaster.Title.TitleImage = image;
            this.CurrentMaster.Title.HelpTopicName = "newedit_page_layout";
            this.CurrentMaster.Title.HelpName = "helpTopic";
        }

        this.plcCssLink.Visible = String.IsNullOrEmpty(txtLayoutCSS.Text.Trim());
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        bool isAscx = (this.drpType.SelectedValue.ToLower() == "ascx");
        if (!SettingsKeyProvider.UsingVirtualPathProvider && (li != null))
        {
            if (isAscx)
            {
                this.tbLayoutCode.Editor.ReadOnly = true;
                this.txtLayoutCSS.Enabled = false;
            }
            this.pnlCheckOutInfo.Visible = false;
            this.plcVirtualInfo.Visible = true;
            this.lblVirtualInfo.Text = String.Format(GetString("PageLayout.VirtualPathProviderNotRunning"), LayoutInfoProvider.GetLayoutUrl(li.LayoutCodeName, null));
        }

        // Setup the information and code type
        if (isAscx)
        {
            tbLayoutCode.Editor.Language = LanguageEnum.ASPNET;
            tbLayoutCode.UseAutoComplete = false;

            ltlHint.Text = GetString("Administration-PageLayout_New.Hint");
        }
        else
        {
            tbLayoutCode.Editor.Language = LanguageEnum.HTMLMixed;
            tbLayoutCode.UseAutoComplete = true;
            
            ltlHint.Text = GetString("EditLayout.HintHtml");
        }

        this.plcDirectives.Visible = isAscx;
    }


    /// <summary>
    /// Initializes header action control.
    /// </summary>
    private void InitializeHeaderActions(LayoutInfo li)
    {
        // Header actions
        string[,] actions = new string[4, 11];

        // Save button
        actions[0, 0] = HeaderActions.TYPE_SAVEBUTTON;
        actions[0, 1] = GetString("General.Save");
        actions[0, 5] = GetImageUrl("CMSModules/CMS_Content/EditMenu/save.png");
        actions[0, 6] = "save";
        actions[0, 8] = "true";

        if (SettingsKeyProvider.UsingVirtualPathProvider)
        {
            // CheckOut
            actions[1, 0] = HeaderActions.TYPE_SAVEBUTTON;
            actions[1, 1] = GetString("General.CheckOutToFile");
            actions[1, 5] = GetImageUrl("CMSModules/CMS_Content/EditMenu/checkout.png");
            actions[1, 6] = "checkout";
            actions[1, 10] = "false";

            // CheckIn
            actions[2, 0] = HeaderActions.TYPE_SAVEBUTTON;
            actions[2, 1] = GetString("General.CheckInFromFile");
            actions[2, 5] = GetImageUrl("CMSModules/CMS_Content/EditMenu/checkin.png");
            actions[2, 6] = "checkin";
            actions[2, 10] = "false";

            // UndoCheckOut
            actions[3, 0] = HeaderActions.TYPE_SAVEBUTTON;
            actions[3, 1] = GetString("General.UndoCheckout");
            actions[3, 2] = "return confirm(" + ScriptHelper.GetString(GetString("General.ConfirmUndoCheckOut")) + ");";
            actions[3, 5] = GetImageUrl("CMSModules/CMS_Content/EditMenu/undocheckout.png");
            actions[3, 6] = "undocheckout";
            actions[3, 10] = "false";
        }

        if (li != null)
        {
            if (li.LayoutCheckedOutByUserID > 0)
            {
                // Checked out by current machine
                if (li.LayoutCheckedOutMachineName.ToLower() == HTTPHelper.MachineName.ToLower())
                {
                    actions[2, 10] = "true";
                }
                if (CMSContext.CurrentUser.UserSiteManagerAdmin)
                {
                    actions[3, 10] = "true";
                }
            }
            else
            {
                actions[1, 10] = "true";
            }
        }

        this.CurrentMaster.HeaderActions.LinkCssClass = "ContentSaveLinkButton";
        this.CurrentMaster.HeaderActions.ActionPerformed += new CommandEventHandler(HeaderActions_ActionPerformed);
        this.CurrentMaster.HeaderActions.Actions = actions;
    }


    /// <summary>
    /// Actions handler.
    /// </summary>
    protected void HeaderActions_ActionPerformed(object sender, CommandEventArgs e)
    {
        string url = null;

        switch (e.CommandName.ToLower())
        {
            case "save":

                if (li != null)
                {
                    url = "PageLayout_Edit.aspx";
                }
                else
                {
                    url = "PageLayout_Frameset.aspx";
                }
                if (SaveLayout())
                {

                    URLHelper.Redirect(url + "?layoutid=" + layoutId + "&saved=1");
                }
                else
                {
                    lblInfo.Visible = false;
                }
                break;

            case "checkout":

                // Ensure version before check-out
                using (CMSActionContext context = new CMSActionContext())
                {
                    context.AllowAsyncActions = false;

                    // Save first
                    if (!SaveLayout())
                    {
                        return;
                    }
                }

                try
                {
                    SiteManagerFunctions.CheckOutLayout(li.LayoutId);
                }
                catch (Exception ex)
                {
                    this.lblError.Text = GetString("Layout.ErrorCheckout") + ": " + ex.Message;
                    this.lblError.Visible = true;
                    return;
                }

                url = URLHelper.AddParameterToUrl(URLHelper.Url.AbsoluteUri, "saved", "1");
                URLHelper.Redirect(url);
                break;

            case "checkin":

                try
                {
                    SiteManagerFunctions.CheckInLayout(li.LayoutId);
                }
                catch (Exception ex)
                {
                    this.lblError.Text = GetString("Layout.ErrorCheckin") + ": " + ex.Message;
                    this.lblError.Visible = true;
                    return;
                }

                URLHelper.Redirect(URLHelper.Url.AbsoluteUri);
                break;

            case "undocheckout":

                try
                {
                    SiteManagerFunctions.UndoCheckOutLayout(li.LayoutId);
                }
                catch (Exception ex)
                {
                    this.lblError.Text = GetString("Layout.ErrorUndoCheckout") + ": " + ex.Message;
                    this.lblError.Visible = true;
                    return;
                }

                URLHelper.Redirect(URLHelper.Url.AbsoluteUri);
                break;
        }
    }


    /// <summary>
    /// Saves the layout.
    /// </summary>
    /// <returns>Returns true if successful</returns>
    private bool SaveLayout()
    {
        string codeName = tbCodeName.Text.Trim();

        // Find out whether required fields are not empty
        string result = new Validator()
            .NotEmpty(codeName, GetString("Administration-PageLayout_New.ErrorEmptyLayoutCodeName"))
            .NotEmpty(tbLayoutDisplayName.Text, GetString("Administration-PageLayout_New.ErrorEmptyLayoutDisplayName"))
            .NotEmpty(tbLayoutCode.Text.Trim(), GetString("Administration-PageLayout_New.ErrorEmptyLayoutCode"))
            .IsCodeName(codeName, GetString("general.invalidcodename"))
            .Result;

        if (result == "")
        {
            if (li == null)
            {
                li = new LayoutInfo();
            }

            li.LayoutCodeName = codeName;
            li.LayoutDisplayName = tbLayoutDisplayName.Text.Trim();
            li.LayoutDescription = tbLayoutDescription.Text;
            li.LayoutType = LayoutInfoProvider.GetLayoutTypeEnum(this.drpType.SelectedValue);

            if (li.LayoutCheckedOutByUserID <= 0)
            {
                li.LayoutCode = tbLayoutCode.Text;
                li.LayoutCSS = txtLayoutCSS.Text;
            }

            try
            {
                LayoutInfoProvider.SetLayoutInfo(li);
                lblInfo.Visible = true;
                lblInfo.Text = GetString("General.ChangesSaved");
                breadcrumbs[1, 0] = li.LayoutDisplayName;
            }
            catch (Exception ex)
            {
                lblInfo.Visible = false;
                lblError.Visible = true;
                lblError.Text = ex.Message.Replace("%%name%%", li.LayoutCodeName);
                return false;
            }

            layoutId = li.LayoutId;

            UploadFile.ObjectID = layoutId;
            UploadFile.UploadFile();

            return !UploadFile.SavingFailed;
        }
        else
        {
            lblError.Text = result;
            lblError.Visible = true;
            return false;
        }
    }


    /// <summary>
    /// Loads data of edited layout from DB into TextBoxes.
    /// </summary>
    protected void LoadData()
    {
        if (li != null)
        {
            this.pnlCheckOutInfo.Visible = true;

            tbLayoutDisplayName.Text = li.LayoutDisplayName;
            tbLayoutDescription.Text = li.LayoutDescription;
            tbCodeName.Text = li.LayoutCodeName;

            drpType.SelectedIndex = (li.LayoutType == LayoutTypeEnum.Html ? 1 : 0);

            tbLayoutCode.Text = li.LayoutCode;
            txtLayoutCSS.Text = li.LayoutCSS;

            if (li.LayoutCheckedOutByUserID > 0)
            {
                this.tbLayoutCode.ReadOnly = true;
                this.txtLayoutCSS.ReadOnly = true;
                string username = null;
                UserInfo ui = UserInfoProvider.GetUserInfo(li.LayoutCheckedOutByUserID);
                if (ui != null)
                {
                    username = HTMLHelper.HTMLEncode(ui.FullName);
                }

                // Checked out by current machine
                if (li.LayoutCheckedOutMachineName.ToLower() == HTTPHelper.MachineName.ToLower())
                {
                    this.lblCheckOutInfo.Text = String.Format(GetString("PageLayout.CheckedOut"), Server.MapPath(li.LayoutCheckedOutFilename));
                }
                else
                {
                    this.lblCheckOutInfo.Text = String.Format(GetString("PageLayout.CheckedOutOnAnotherMachine"), li.LayoutCheckedOutMachineName, username);
                }
            }
            else
            {
                this.lblCheckOutInfo.Text = String.Format(GetString("PageLayout.CheckOutInfo"), Server.MapPath(LayoutInfoProvider.GetLayoutUrl(li.LayoutCodeName, null)));
            }
        }
    }
}
