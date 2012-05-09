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
using CMS.CMSHelper;
using CMS.SettingsProvider;
using CMS.UIControls;
using CMS.ExtendedControls;

public partial class CMSModules_PortalEngine_UI_PageTemplates_PageTemplate_Layouts : CMSEditTemplatePage
{
    #region "Variables"

    protected int templateId = 0;
    protected PageTemplateInfo pti = null;
    protected CurrentUserInfo user = null;

    #endregion


    #region "Page events"

    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        user = CMSContext.CurrentUser;

        // Get the template
        templateId = QueryHelper.GetInteger("templateid", 0);
        pti = PageTemplateInfoProvider.GetPageTemplateInfo(templateId);

        radCustom.Text = GetString("TemplateLayout.Custom");
        radShared.Text = GetString("TemplateLayout.Shared");
        lblType.Text = GetString("PageLayout.Type");
        lbLayoutCode.Text = GetString("Administration-PageLayout_New.LayoutCode");

        string lang = DataHelper.GetNotEmpty(SettingsHelper.AppSettings["CMSProgrammingLanguage"], "C#");
        ltlDirectives.Text = "&lt;%@ Control Language=\"" + lang + "\" ClassName=\"Simple\" Inherits=\"CMS.PortalControls.CMSAbstractLayout\" %&gt;<br />&lt;%@ Register Assembly=\"CMS.PortalControls\" Namespace=\"CMS.PortalControls\" TagPrefix=\"cc1\" %&gt;";

        if (this.drpType.Items.Count == 0)
        {
            drpType.Items.Add(new ListItem(GetString("TransformationType.Ascx"), TransformationTypeEnum.Ascx.ToString()));
            drpType.Items.Add(new ListItem(GetString("TransformationType.Html"), TransformationTypeEnum.Html.ToString()));
        }

        if (!RequestHelper.IsPostBack())
        {
            ReloadData();
        }

        InitializeHeaderActions();
    }


    /// <summary>
    /// Raises the <see cref="E:PreRender"/> event.
    /// </summary>
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        bool editingEnabled = true;

        // Disable items when virtual path provider is disabled
        if (!SettingsKeyProvider.UsingVirtualPathProvider && (pti != null))
        {
            this.lblVirtualInfo.Text = String.Format(GetString("TemplateLayout.VirtualPathProviderNotRunning"), PageTemplateInfoProvider.GetLayoutUrl(pti.CodeName, null));
            this.plcVirtualInfo.Visible = true;
            this.pnlCheckOutInfo.Visible = false;

            editingEnabled = false;
            this.txtCustomCSS.ReadOnly = true;
            this.drpType.Enabled = false;
        }

        string info = null;

        // Setup the information and code type
        bool isAscx = (this.drpType.SelectedValue.ToLower() == "ascx");
        if (isAscx)
        {
            txtCustom.Editor.Language = LanguageEnum.ASPNET;
            txtCustom.UseAutoComplete = false;

            info = GetString("Administration-PageLayout_New.Hint");

            // Check the edit code permission
            if (!user.IsAuthorizedPerResource("CMS.Design", "EditCode"))
            {
                editingEnabled = false;
                info = ResHelper.GetString("EditCode.NotAllowed");
            }
        }
        else
        {
            txtCustom.Editor.Language = LanguageEnum.HTMLMixed;
            txtCustom.UseAutoComplete = true;

            info = GetString("EditLayout.HintHtml");
        }

        this.ltlHint.Text = info;
        this.txtCustom.ReadOnly = !editingEnabled;

        this.plcDirectives.Visible = isAscx;

        this.plcCssLink.Visible = String.IsNullOrEmpty(txtCustomCSS.Text.Trim());
        this.lnkStyles.Visible = radCustom.Checked;
    }


    /// <summary>
    /// Actions handler.
    /// </summary>
    protected void HeaderActions_ActionPerformed(object sender, CommandEventArgs e)
    {
        switch (e.CommandName.ToLower())
        {
            case "save":
                {
                    SaveLayout();

                    // Reload header actions
                    InitializeHeaderActions();
                    this.CurrentMaster.HeaderActions.ReloadData();
                }
                break;

            case "checkout":
                {
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
                        SiteManagerFunctions.CheckOutTemplateLayout(templateId);
                    }
                    catch (Exception ex)
                    {
                        this.lblError.Text = GetString("Layout.ErrorCheckout") + ": " + ex.Message;
                        this.lblError.Visible = true;
                        return;
                    }

                    URLHelper.Redirect(URLHelper.Url.AbsoluteUri);
                }
                break;

            case "checkin":
                {
                    // Check in the layout
                    try
                    {
                        SiteManagerFunctions.CheckInTemplateLayout(templateId);
                    }
                    catch (Exception ex)
                    {
                        this.lblError.Text = GetString("Layout.ErrorCheckin") + ": " + ex.Message;
                        this.lblError.Visible = true;
                        return;
                    }

                    URLHelper.Redirect(URLHelper.Url.AbsoluteUri);
                }
                break;

            case "undocheckout":
                {
                    // Undo the checkout
                    try
                    {
                        SiteManagerFunctions.UndoCheckOutTemplateLayout(templateId);
                    }
                    catch (Exception ex)
                    {
                        this.lblError.Text = GetString("Layout.ErrorUndoCheckout") + ": " + ex.Message;
                        this.lblError.Visible = true;
                        return;
                    }

                    URLHelper.Redirect(URLHelper.Url.AbsoluteUri);
                }
                break;
        }

    }


    /// <summary>
    /// Radio button changed.
    /// </summary>
    protected void radShared_CheckedChanged(object sender, EventArgs e)
    {
        if (radCustom.Checked)
        {
            txtCustom.ReadOnly = false;
            txtCustomCSS.ReadOnly = false;

            drpType.Enabled = true;

            selectShared.Enabled = false;
        }
        else
        {
            drpType.Enabled = false;

            txtCustom.ReadOnly = true;
            txtCustomCSS.ReadOnly = true;

            selectShared.Enabled = true;

            LayoutInfo li = LayoutInfoProvider.GetLayoutInfo(ValidationHelper.GetInteger(selectShared.Value, 0));
            if (li != null)
            {
                txtCustom.Text = li.LayoutCode;
                txtCustomCSS.Text = li.LayoutCSS;
            }
        }
    }


    /// <summary>
    /// DropDownlist change.
    /// </summary>
    protected void selectShared_Changed(object sender, EventArgs ea)
    {
        LayoutInfo li = LayoutInfoProvider.GetLayoutInfo(ValidationHelper.GetInteger(selectShared.Value, 0));
        if (li != null)
        {
            txtCustom.Text = li.LayoutCode;
            txtCustom.ReadOnly = true;

            txtCustomCSS.Text = li.LayoutCSS;
            txtCustomCSS.ReadOnly = true;
        }
    }

    #endregion


    #region "Other methods"

    /// <summary>
    /// Initializes header action control.
    /// </summary>
    private void InitializeHeaderActions()
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

        if (pti != null)
        {
            // Check whether current layout is not shared and if is checked out by current user
            if ((pti.LayoutID == 0) && (pti.PageTemplateLayoutCheckedOutByUserID > 0))
            {
                // Checked out by current machine
                if (pti.PageTemplateLayoutCheckedOutMachineName.ToLower() == HTTPHelper.MachineName.ToLower())
                {
                    actions[2, 10] = "true";
                }
                if (CMSContext.CurrentUser.IsGlobalAdministrator)
                {
                    actions[3, 10] = "true";
                }
            }
            else
            {
                actions[1, 10] = "true";
            }
            if (pti.LayoutID > 0)
            {
                actions[1, 10] = "false";
            }
        }

        CurrentMaster.HeaderActions.LinkCssClass = "ContentSaveLinkButton";
        CurrentMaster.HeaderActions.ActionPerformed += new CommandEventHandler(HeaderActions_ActionPerformed);
        CurrentMaster.HeaderActions.Actions = actions;
    }


    /// <summary>
    /// Reload data.
    /// </summary>
    public void ReloadData()
    {
        if (templateId > 0)
        {
            PageTemplateInfo pti = PageTemplateInfoProvider.GetPageTemplateInfo(templateId);

            if (pti != null)
            {
                // For ASPX, do not allow to change the layout
                if (pti.PageTemplateType == PageTemplateTypeEnum.Aspx)
                {
                    plcContent.Visible = false;
                    pnlCheckOutInfo.Visible = false;
                    lblAspxInfo.Visible = true;
                    lblAspxInfo.Text = GetString("TemplateLayout.ASPXtemplate");

                    return;
                }

                this.pnlCheckOutInfo.Visible = true;

                if (pti.PageTemplateLayoutCheckedOutByUserID > 0)
                {
                    this.txtCustom.ReadOnly = true;
                    this.txtCustomCSS.ReadOnly = true;
                    string username = null;
                    UserInfo ui = UserInfoProvider.GetUserInfo(pti.PageTemplateLayoutCheckedOutByUserID);
                    if (ui != null)
                    {
                        username = HTMLHelper.HTMLEncode(ui.FullName);
                    }

                    // Checked out by current machine
                    if (pti.PageTemplateLayoutCheckedOutMachineName.ToLower() == HTTPHelper.MachineName.ToLower())
                    {
                        this.lblCheckOutInfo.Text = String.Format(GetString("PageLayout.CheckedOut"), Server.MapPath(pti.PageTemplateLayoutCheckedOutFileName));
                    }
                    else
                    {
                        this.lblCheckOutInfo.Text = String.Format(GetString("PageLayout.CheckedOutOnAnotherMachine"), pti.PageTemplateLayoutCheckedOutFileName, username);
                    }
                }
                else
                {
                    string url = null;
                    if (pti.IsReusable)
                    {
                        url = Server.MapPath(PageTemplateInfoProvider.GetLayoutUrl(pti.CodeName, null, pti.PageTemplateLayoutType));
                    }
                    else
                    {
                        url = Server.MapPath(PageTemplateInfoProvider.GetAdhocLayoutUrl(pti.CodeName, null, pti.PageTemplateLayoutType));
                    }

                    this.lblCheckOutInfo.Text = String.Format(GetString("PageLayout.CheckOutInfo"), url);
                }

                if (ValidationHelper.GetInteger(pti.LayoutID, 0) > 0)
                {
                    // Shared layout
                    pnlCheckOutInfo.Visible = false;

                    radShared.Checked = true;

                    txtCustom.ReadOnly = true;
                    txtCustomCSS.ReadOnly = true;

                    drpType.Enabled = false;

                    selectShared.Enabled = true;

                    try
                    {
                        // Load layout content
                        selectShared.Value = pti.LayoutID.ToString();

                        LayoutInfo li = LayoutInfoProvider.GetLayoutInfo(pti.LayoutID);
                        if (li != null)
                        {
                            txtCustom.Text = li.LayoutCode;
                            txtCustomCSS.Text = li.LayoutCSS;

                            drpType.SelectedIndex = (li.LayoutType == LayoutTypeEnum.Html ? 1 : 0);
                        }
                    }
                    catch
                    {
                    }
                }
                else
                {
                    // Custom layout
                    radCustom.Checked = true;
                    txtCustom.Text = pti.PageTemplateLayout;
                    txtCustom.ReadOnly = false;

                    txtCustomCSS.Text = pti.PageTemplateCSS;
                    txtCustomCSS.ReadOnly = false;

                    drpType.SelectedIndex = (pti.PageTemplateLayoutType == LayoutTypeEnum.Html ? 1 : 0);
                    drpType.Enabled = true;
                    selectShared.Enabled = false;
                }
            }
        }
    }


    /// <summary>
    /// Save layout.
    /// </summary>
    public bool SaveLayout()
    {
        PageTemplateInfo pti = PageTemplateInfoProvider.GetPageTemplateInfo(templateId);

        if (pti != null)
        {
            if (radShared.Checked)
            {
                // Shared layout
                pnlCheckOutInfo.Visible = false;
                pti.LayoutID = ValidationHelper.GetInteger(selectShared.Value, 0);
                pti.PageTemplateLayout = "";
                pti.PageTemplateCSS = "";
            }
            else
            {
                LayoutTypeEnum layoutType = LayoutInfoProvider.GetLayoutTypeEnum(this.drpType.SelectedValue);

                if ((layoutType != LayoutTypeEnum.Ascx) || user.IsAuthorizedPerResource("CMS.Design", "EditCode"))
                {
                    // Custom layout
                    pnlCheckOutInfo.Visible = true;
                    pti.LayoutID = 0;
                    pti.PageTemplateLayoutType = layoutType;
                    pti.PageTemplateLayout = txtCustom.Text;
                    pti.PageTemplateCSS = txtCustomCSS.Text;
                }
            }

            PageTemplateInfoProvider.SetPageTemplateInfo(pti);

            lblInfo.Visible = true;
            lblInfo.Text = GetString("General.ChangesSaved");

            return true;
        }

        return false;
    }

    #endregion
}
