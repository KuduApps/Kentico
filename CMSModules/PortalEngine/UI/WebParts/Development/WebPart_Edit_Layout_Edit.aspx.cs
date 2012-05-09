using System;
using System.Data;
using System.Web.UI.WebControls;

using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.SiteProvider;
using CMS.PortalEngine;
using CMS.UIControls;
using CMS.SettingsProvider;

// Set edited object
[EditedObject("cms.webpartlayout", "layoutId")]

// Set help
[Help("newedit_webpart_layout", "helpTopic")]

public partial class CMSModules_PortalEngine_UI_WebParts_Development_WebPart_Edit_Layout_Edit : SiteManagerPage
{
    #region "Variables"

    protected string mSave = null;
    protected string mCheckIn = null;
    protected string mCheckOut = null;
    protected string mUndoCheckOut = null;

    protected int layoutId = 0;

    WebPartLayoutInfo wpli = null;
    WebPartInfo wpi = null;

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        Title = "Web part layout properties";

        // Get the layout
        layoutId = QueryHelper.GetInteger("layoutId", 0);
        wpli = WebPartLayoutInfoProvider.GetWebPartLayoutInfo(layoutId);
        if (wpli != null)
        {
            wpi = WebPartInfoProvider.GetWebPartInfo(wpli.WebPartLayoutWebPartID);
        }

        // Init GUI
        mCheckOut = GetString("WebPartLayout.CheckOut");
        mCheckIn = GetString("WebPartLayout.CheckIn");
        mUndoCheckOut = GetString("WebPartLayout.DiscardCheckOut");
        mSave = GetString("General.Save");

        lblDisplayName.Text = GetString("WebPartEditLayoutEdit.lblDisplayName");
        lblCodeName.Text = GetString("WebPartEditLayoutEdit.lblCodeName");
        lblCode.Text = GetString("WebPartEditLayoutEdit.lblCode");
        lblDescription.Text = GetString("WebPartEditLayoutEdit.lblDescription");

        rfvDisplayName.ErrorMessage = GetString("general.requiresdisplayname");
        rfvCodeName.ErrorMessage = GetString("webparteditlayoutedit.rfvcodenamerequired");

        LoadData();

        this.plcCssLink.Visible = String.IsNullOrEmpty(tbCSS.Text.Trim());
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        // Disable items when virtual path provider is disabled
        if (!SettingsKeyProvider.UsingVirtualPathProvider && (wpli != null) && (wpi != null))
        {
            lblVirtualInfo.Text = String.Format(GetString("WebPartLayout.VirtualPathProviderNotRunning"), WebPartLayoutInfoProvider.GetWebPartLayoutUrl(wpi.WebPartName, wpli.WebPartLayoutCodeName, null));
            plcVirtualInfo.Visible = true;
            pnlCheckOutInfo.Visible = false;
            etaCode.Enabled = false;
            tbCSS.Enabled = false;
        }
    }


    /// <summary>
    /// Loads data of edited layout from DB into TextBoxes.
    /// </summary>
    protected void LoadData()
    {
        // Initialize default flag values
        bool displayCheckIn = false;
        bool displayUndoCheckOut = false;
        bool displayCheckOut = false;

        if (wpli != null)
        {
            pnlCheckOutInfo.Visible = true;

            string codeNameOnly = wpli.WebPartLayoutCodeName;
            int pos = codeNameOnly.IndexOf('.');
            if (pos >= 0)
            {
                codeNameOnly = codeNameOnly.Substring(pos + 1, codeNameOnly.Length - pos - 1);
            }

            if (!RequestHelper.IsPostBack())
            {
                txtDisplayName.Text = wpli.WebPartLayoutDisplayName;
                txtCodeName.Text = codeNameOnly;
                txtDescription.Text = wpli.WebPartLayoutDescription;
                etaCode.Text = wpli.WebPartLayoutCode;
                tbCSS.Text = wpli.WebPartLayoutCSS;
            }

            if (wpli.WebPartLayoutCheckedOutByUserID > 0)
            {
                etaCode.Enabled = false;
                tbCSS.Enabled = false;

                string username = null;
                UserInfo ui = UserInfoProvider.GetUserInfo(wpli.WebPartLayoutCheckedOutByUserID);
                if (ui != null)
                {
                    username = HTMLHelper.HTMLEncode(ui.FullName);
                }

                // Checked out by current machine
                if (wpli.WebPartLayoutCheckedOutMachineName.ToLower() == HTTPHelper.MachineName.ToLower())
                {
                    displayCheckIn = true;

                    lblCheckOutInfo.Text = String.Format(GetString("WebPartEditLayoutEdit.CheckedOut"), Server.MapPath(wpli.WebPartLayoutCheckedOutFilename));
                }
                else
                {
                    lblCheckOutInfo.Text = String.Format(GetString("WebPartEditLayoutEdit.CheckedOutOnAnotherMachine"), wpli.WebPartLayoutCheckedOutMachineName, username);
                }

                if (CMSContext.CurrentUser.IsGlobalAdministrator)
                {
                    displayUndoCheckOut = true;
                }
            }
            else if (wpi != null)
            {
                lblCheckOutInfo.Text = String.Format(GetString("WebPartEditLayoutEdit.CheckOutInfo"), Server.MapPath(WebPartLayoutInfoProvider.GetWebPartLayoutUrl(wpi.WebPartName, wpli.WebPartLayoutCodeName, null)));

                displayCheckOut = true;
            }
        }
        else
        {
            lblError.Text = GetString("WebPartEditLayoutEdit.InvalidLayoutID");
            lblError.Visible = true;
        }

        InitializeMasterPage(displayCheckIn, displayCheckOut, displayUndoCheckOut);

        if (QueryHelper.GetInteger("saved", 0) == 1)
        {
            lblInfo.Text = GetString("general.changessaved");
            lblInfo.Visible = true;

            // Reload header if changes were saved
            ScriptHelper.RefreshTabHeader(Page, null);
        }
    }


    /// <summary>
    /// Initializes the master page elements.
    /// </summary>
    private void InitializeMasterPage(bool displayCheckIn, bool displayCheckOut, bool displayUndoCheckOut)
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
            actions[1, 1] = GetString("General.CheckOut");
            actions[1, 5] = GetImageUrl("CMSModules/CMS_Content/EditMenu/checkout.png");
            actions[1, 6] = "checkout";
            actions[1, 10] = "false";

            // CheckIn
            actions[2, 0] = HeaderActions.TYPE_SAVEBUTTON;
            actions[2, 1] = GetString("General.CheckIn");
            actions[2, 5] = GetImageUrl("CMSModules/CMS_Content/EditMenu/checkin.png");
            actions[2, 6] = "checkin";
            actions[2, 10] = "false";

            // UndoCheckOut
            actions[3, 0] = HeaderActions.TYPE_SAVEBUTTON;
            actions[3, 1] = GetString("general.undocheckout");
            actions[3, 2] = "return confirm(" + ScriptHelper.GetString(GetString("General.ConfirmUndoCheckOut")) + ");";
            actions[3, 5] = GetImageUrl("CMSModules/CMS_Content/EditMenu/undocheckout.png");
            actions[3, 6] = "undocheckout";
            actions[3, 10] = "false";

            if (displayCheckIn)
            {
                actions[2, 10] = "true";
            }

            if (displayCheckOut)
            {
                actions[1, 10] = "true";
            }

            if (displayUndoCheckOut)
            {
                actions[3, 10] = "true";
            }
        }

        CurrentMaster.HeaderActions.LinkCssClass = "ContentSaveLinkButton";
        CurrentMaster.HeaderActions.ActionPerformed += HeaderActions_ActionPerformed;
        CurrentMaster.HeaderActions.Actions = actions;
    }


    void HeaderActions_ActionPerformed(object sender, CommandEventArgs e)
    {
        switch (e.CommandName.ToLower())
        {
            case "save":
                btnSave_Click(sender, e);
                break;

            case "checkout":
                btnCheckOut_Click(sender, e);
                break;

            case "checkin":
                btnCheckIn_Click(sender, e);
                break;

            case "undocheckout":
                btnUndoCheckOut_Click(sender, e);
                break;
        }
    }


    /// <summary>
    /// Save layout code.
    /// </summary>
    protected bool SaveData()
    {
        // Remove "." due to virtual path provider replacement
        txtCodeName.Text = txtCodeName.Text.Replace(".", "");

        txtDisplayName.Text = txtDisplayName.Text.Trim();
        txtCodeName.Text = txtCodeName.Text.Trim();

        string errorMessage = new Validator()
            .NotEmpty(txtCodeName.Text, rfvCodeName.ErrorMessage)
            .NotEmpty(txtDisplayName.Text, rfvDisplayName.ErrorMessage)
            .IsCodeName(txtCodeName.Text, GetString("general.invalidcodename")).Result;

        int webPartId = ValidationHelper.GetInteger(Request.QueryString["webpartId"], 0);
        WebPartInfo webPartInfo = WebPartInfoProvider.GetWebPartInfo(webPartId);
        if (webPartInfo == null)
        {
            errorMessage = GetString("WebPartEditLayoutEdit.InvalidWebPartID");
        }

        if (errorMessage != String.Empty)
        {
            lblError.Text = errorMessage;
            lblError.Visible = true;
            return false;
        }

        // Get layout info
        WebPartLayoutInfo webPartLayoutInfo = WebPartLayoutInfoProvider.GetWebPartLayoutInfo(layoutId);

        if (webPartLayoutInfo != null)
        {
            // Get layout info using its code name - layout code name must be unique
            DataSet ds = WebPartLayoutInfoProvider.GetWebPartLayouts("WebPartLayoutCodeName = '" + WebPartLayoutInfoProvider.GetWebPartLayoutFullCodeName(webPartInfo.WebPartName, txtCodeName.Text) + "'", null);

            // Find anything?
            if (!DataHelper.DataSourceIsEmpty(ds))
            {
                WebPartLayoutInfo temp = new WebPartLayoutInfo(ds.Tables[0].Rows[0]);
                // Is it the same layout?
                if ((ds.Tables[0].Rows.Count > 1) || (temp.WebPartLayoutID != webPartLayoutInfo.WebPartLayoutID))
                {
                    lblError.Text = String.Format(GetString("WebPartEditLayoutEdit.CodeNameAlreadyExist"), txtCodeName.Text);
                    lblError.Visible = true;
                    return false;
                }
            }

            webPartLayoutInfo.WebPartLayoutCodeName = txtCodeName.Text;
            webPartLayoutInfo.WebPartLayoutDisplayName = txtDisplayName.Text;
            webPartLayoutInfo.WebPartLayoutDescription = txtDescription.Text;

            if (!webPartLayoutInfo.Generalized.IsCheckedOut)
            {
                webPartLayoutInfo.WebPartLayoutCode = etaCode.Text;
                webPartLayoutInfo.WebPartLayoutCSS = tbCSS.Text;
            }

            WebPartLayoutInfoProvider.SetWebPartLayoutInfo(webPartLayoutInfo);

            // Reload header if changes were saved
            if (TabMode)
            {
                ScriptHelper.RefreshTabHeader(Page, null);
            }
        }
        return true;
    }


    protected void btnCheckOut_Click(object sender, EventArgs e)
    {
        // Ensure version before check-out
        using (CMSActionContext context = new CMSActionContext())
        {
            context.AllowAsyncActions = false;

            // Save first
            if (!SaveData())
            {
                return;
            }
        }

        try
        {
            SiteManagerFunctions.CheckOutWebPartLayout(layoutId);
        }
        catch (Exception ex)
        {
            lblError.Text = GetString("WebPartLayout.ErrorCheckout") + ": " + ex.Message;
            lblError.Visible = true;
            return;
        }

        URLHelper.Redirect(URLHelper.AddParameterToUrl(URLHelper.Url.AbsoluteUri, "saved", "1"));
    }


    protected void btnUndoCheckOut_Click(object sender, EventArgs e)
    {
        try
        {
            SiteManagerFunctions.UndoCheckOutWebPartLayout(layoutId);
        }
        catch (Exception ex)
        {
            lblError.Text = GetString("WebPartLayout.ErrorUndoCheckout") + ": " + ex.Message;
            lblError.Visible = true;
            return;
        }

        URLHelper.Redirect(URLHelper.Url.AbsoluteUri);
    }


    protected void btnCheckIn_Click(object sender, EventArgs e)
    {
        try
        {
            SiteManagerFunctions.CheckInWebPartLayout(layoutId);
        }
        catch (Exception ex)
        {
            lblError.Text = GetString("WebPartLayout.ErrorCheckin") + ": " + ex.Message;
            lblError.Visible = true;
            return;
        }

        URLHelper.Redirect(URLHelper.Url.AbsoluteUri);
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (SaveData())
        {
            lblInfo.Text = GetString("general.changessaved");
            lblInfo.Visible = true;
        }
    }
}
