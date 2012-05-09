using System;
using System.Xml;
using System.Web.UI;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.IO;
using CMS.PortalEngine;
using CMS.SettingsProvider;
using CMS.SiteProvider;
using CMS.UIControls;

public partial class CMSModules_PortalEngine_UI_WebParts_webpartproperties_layout : CMSWebPartPropertiesPage
{
    #region "Variables"

    protected string mSave = null;
    protected string mCheckIn = null;
    protected string mCheckOut = null;
    protected string mUndoCheckOut = null;

    WebPartInfo webPartInfo = null;

    /// <summary>
    /// Current page info.
    /// </summary>
    PageInfo pi = null;

    /// <summary>
    /// Page template info.
    /// </summary>
    PageTemplateInfo pti = null;


    /// <summary>
    /// Current web part.
    /// </summary>
    WebPartInstance webPart = null;

    string mLayoutCodeName = null;

    #endregion


    #region "Properties"

    /// <summary>
    /// Gets code name of edited layout.
    /// </summary>
    private string LayoutCodeName
    {
        get
        {
            if (mLayoutCodeName == null)
            {
                mLayoutCodeName = QueryHelper.GetString("layoutcodename", String.Empty);
            }
            return mLayoutCodeName;
        }
        set
        {
            mLayoutCodeName = value;
        }
    }

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Check permissions for web part properties UI
        CurrentUserInfo currentUser = CMSContext.CurrentUser;
        if (!currentUser.IsAuthorizedPerUIElement("CMS.Content", "WebPartProperties.Layout"))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.Content", "WebPartProperties.Layout");
        }

        // Check saved
        bool saved = false;
        if (QueryHelper.GetBoolean("saved", false))
        {
            saved = true;
            lblInfo.Visible = true;
            lblInfo.Text = GetString("General.ChangesSaved");
        }

        // Init GUI
        mCheckOut = GetString("WebPartLayout.CheckOut");
        mCheckIn = GetString("WebPartLayout.CheckIn");
        mUndoCheckOut = GetString("WebPartLayout.DiscardCheckOut");
        mSave = GetString("General.Save");

        this.imgSave.ImageUrl = GetImageUrl("CMSModules/CMS_Content/EditMenu/Save.png");

        this.imgCheckIn.ImageUrl = GetImageUrl("CMSModules/CMS_Content/EditMenu/checkin.png");
        this.imgCheckOut.ImageUrl = GetImageUrl("CMSModules/CMS_Content/EditMenu/checkout.png");

        this.imgUndoCheckOut.ImageUrl = GetImageUrl("CMSModules/CMS_Content/EditMenu/undocheckout.png");
        this.btnUndoCheckOut.OnClientClick = "return confirm(" + ScriptHelper.GetString(GetString("General.ConfirmUndoCheckOut")) + ");";

        if (webpartId != "")
        {
            // Get pageinfo
            pi = GetPageInfo(aliasPath, templateId);
            if (pi == null)
            {
                this.lblInfo.Text = GetString("WebPartProperties.WebPartNotFound");
                this.pnlFormArea.Visible = false;
                return;
            }

            // Get page template
            pti = pi.PageTemplateInfo;
            if ((pti != null) && ((pti.TemplateInstance != null)))
            {
                webPart = pti.TemplateInstance.GetWebPart(instanceGuid, zoneVariantId, variantId) ?? pti.GetWebPart(webpartId);
            }
        }

        // If the web part is not found, do not continue
        if (webPart == null)
        {
            this.lblInfo.Text = GetString("WebPartProperties.WebPartNotFound");
            this.pnlFormArea.Visible = false;

            return;
        }
        else
        {
            if (String.IsNullOrEmpty(LayoutCodeName))
            {
                // Get the current layout name
                LayoutCodeName = ValidationHelper.GetString(webPart.GetValue("WebPartLayout"), "");
            }
        }

        WebPartInfo wpi = WebPartInfoProvider.GetWebPartInfo(webPart.WebPartType);
        if (wpi != null)
        {
            // Load the web part information
            webPartInfo = wpi;
            bool loaded = false;

            if (!RequestHelper.IsPostBack())
            {
                pnlMenu.Visible = false;
                pnlCheckOutInfo.Visible = false;

                if (LayoutCodeName != "")
                {
                    WebPartLayoutInfo wpli = WebPartLayoutInfoProvider.GetWebPartLayoutInfo(wpi.WebPartName, LayoutCodeName);
                    if (wpli != null)
                    {
                        if ((LayoutCodeName != "|default|") && (LayoutCodeName != "|new|"))
                        {
                            SetEditedObject(wpli, "WebPartProperties_layout_frameset_frameset.aspx");
                        }

                        pnlMenu.Visible = true;
                        pnlCheckOutInfo.Visible = true;

                        // Read-only code text area
                        etaCode.ReadOnly = false;
                        etaCSS.ReadOnly = false;

                        // Set checkout panel
                        SetCheckPanel(wpli);

                        etaCode.Text = wpli.WebPartLayoutCode;
                        etaCSS.Text = wpli.WebPartLayoutCSS;
                        loaded = true;
                    }
                }

                if (!loaded)
                {
                    string fileName = webPartInfo.WebPartFileName;

                    if (webPartInfo.WebPartParentID > 0)
                    {
                        WebPartInfo pwpi = WebPartInfoProvider.GetWebPartInfo(webPartInfo.WebPartParentID);
                        if (pwpi != null)
                        {
                            fileName = pwpi.WebPartFileName;
                        }
                    }

                    if (!fileName.StartsWith("~"))
                    {
                        fileName = "~/CMSWebparts/" + fileName;
                    }

                    // Check if filename exist
                    if (!FileHelper.FileExists(fileName))
                    {
                        lblError.Text = GetString("WebPartProperties.FileNotExist");
                        lblError.Visible = true;
                        plcContent.Visible = false;
                    }
                    else
                    {
                        // Load default web part layout code
                        etaCode.Text = File.ReadAllText(Server.MapPath(fileName));

                        // Load default web part CSS
                        etaCSS.Text = wpi.WebPartCSS;
                    }
                }
            }
        }

        btnOnOK.Click += new EventHandler(btnOnOK_Click);
        
        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "ApplyButton", ScriptHelper.GetScript(
               "function SetRefresh(refreshpage) { document.getElementById('" + this.hidRefresh.ClientID + "').value = refreshpage; } \n" +
               "function OnApplyButton(refreshpage) { SetRefresh(refreshpage); " + Page.ClientScript.GetPostBackEventReference(lnkSave, "") + "} \n" +
               "function OnOKButton(refreshpage) { SetRefresh(refreshpage); " + Page.ClientScript.GetPostBackEventReference(btnOnOK, "") + "} \n"
           ));

        if (saved && (LayoutCodeName == "|new|"))
        {
            // Refresh menu
            string query = URLHelper.Url.Query;
            query = URLHelper.AddParameterToUrl(query, "layoutcodename", webPart.GetValue("WebPartLayout").ToString());
            query = URLHelper.AddParameterToUrl(query, "reload", "true");

            string scriptText = ScriptHelper.GetScript(@"parent.frames['webpartpropertiesmenu'].location = 'webpartproperties_layout_menu.aspx" + query + "';");
            ScriptHelper.RegisterStartupScript(this, typeof(string), "ReloadAfterNewLayout", scriptText);
        }

        if (!RequestHelper.IsPostBack())
        {
            InitLayoutForm();
        }

        this.plcCssLink.Visible = String.IsNullOrEmpty(etaCSS.Text.Trim());
        this.lnkStyles.Visible = !String.IsNullOrEmpty(LayoutCodeName) && (LayoutCodeName != "|default|");
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        if (!CurrentUser.IsAuthorizedPerResource("CMS.Design", "EditCode"))
        {
            ltlHint.Text = ResHelper.GetString("EditCode.NotAllowedNoHtml");
            plcHint.Visible = true;

            etaCode.ReadOnly = true;
        }

        if (!SettingsKeyProvider.UsingVirtualPathProvider)
        {
            this.lblVirtualInfo.Text = GetString("WebPartLayout.ProviderNotRunning");
            this.plcVirtualInfo.Visible = true;
            this.pnlCheckOutInfo.Visible = false;
            this.pnlMenu.Visible = false;
            etaCode.Enabled = false;
            etaCSS.Enabled = false;
        }
    }


    /// <summary>
    /// Selected index changed.
    /// </summary>
    private void InitLayoutForm()
    {

        if (webPartInfo != null)
        {
            if (!String.IsNullOrEmpty(LayoutCodeName))
            {
                if (LayoutCodeName == "|new|")
                {
                    // New layout
                    plcDescription.Visible = true;
                    plcValues.Visible = true;
                    etaCode.ReadOnly = false;
                    etaCSS.ReadOnly = false;
                    etaCode.Rows = 19;
                    pnlMenu.Visible = false;
                    pnlCheckOutInfo.Visible = false;

                    // Prefill with default layout
                    etaCode.Text = GetDefaultCode();
                }
                else
                {
                    etaCode.Rows = 24;
                    plcDescription.Visible = false;
                    plcValues.Visible = false;
                    etaCode.ReadOnly = false;

                    if (LayoutCodeName == "|default|")
                    {
                        // Get default code and disable editing
                        etaCode.Text = GetDefaultCode();

                        etaCode.ReadOnly = true;
                        etaCSS.ReadOnly = true;
                        etaCSS.Text = webPartInfo.WebPartCSS;

                        pnlMenu.Visible = false;
                        pnlCheckOutInfo.Visible = false;
                    }
                    else
                    {
                        // Other layouts
                        etaCode.Rows = 18;

                        pnlMenu.Visible = true;
                        pnlCheckOutInfo.Visible = true;
                        etaCode.Text = "Loading...";

                        WebPartInfo wpi = WebPartInfoProvider.GetWebPartInfo(webPart.WebPartType);
                        if (wpi != null)
                        {
                            // Get the layout code from layout info
                            WebPartLayoutInfo wpli = WebPartLayoutInfoProvider.GetWebPartLayoutInfo(wpi.WebPartName, LayoutCodeName);
                            if (wpli != null)
                            {
                                SetCheckPanel(null);

                                etaCode.Text = wpli.WebPartLayoutCode;
                                etaCSS.ReadOnly = false;
                                etaCSS.Text = wpli.WebPartLayoutCSS;
                            }
                        }
                    }
                }
            }
        }
        else
        {

        }
    }


    /// <summary>
    /// Gets the default layout code for the web part
    /// </summary>
    protected string GetDefaultCode()
    {
        string fileName = webPartInfo.WebPartFileName;
        if (webPartInfo.WebPartParentID > 0)
        {
            WebPartInfo pwpi = WebPartInfoProvider.GetWebPartInfo(webPartInfo.WebPartParentID);
            if (pwpi != null)
            {
                fileName = pwpi.WebPartFileName;
            }
        }

        return File.ReadAllText(Server.MapPath("~/CMSWebparts/" + fileName));
    }


    /// <summary>
    /// Save new layout.
    /// </summary>
    protected bool Save()
    {
        if (webPartInfo != null)
        {
            // Remove "." due to virtual path provider replacement
            txtLayoutName.Text = txtLayoutName.Text.Replace(".", "");

            string result = new Validator().NotEmpty(txtLayoutName.Text, GetString("WebPartPropertise.errCodeName")).NotEmpty(txtLayoutDisplayName.Text, GetString("WebPartPropertise.errDisplayName")).IsCodeName(txtLayoutName.Text, GetString("WebPartPropertise.errCodeNameFormat")).Result;

            if (result == "")
            {
                WebPartLayoutInfo tmpLayInfo = WebPartLayoutInfoProvider.GetWebPartLayoutInfo(webPartInfo.WebPartName, txtLayoutName.Text.Trim());
                if (tmpLayInfo == null)
                {
                    WebPartLayoutInfo wpli = new WebPartLayoutInfo();

                    wpli.WebPartLayoutCodeName = txtLayoutName.Text.Trim();

                    wpli.WebPartLayoutDescription = txtDescription.Text;
                    wpli.WebPartLayoutDisplayName = txtLayoutDisplayName.Text;
                    
                    if (CurrentUser.IsAuthorizedPerResource("CMS.Design", "EditCode"))
                    {
                        wpli.WebPartLayoutCode = etaCode.Text;
                    }
                    else
                    {
                        wpli.WebPartLayoutCode = GetDefaultCode();
                    }
                    
                    wpli.WebPartLayoutCSS = etaCSS.Text;
                    wpli.WebPartLayoutWebPartID = webPartInfo.WebPartID;
                    
                    WebPartLayoutInfoProvider.SetWebPartLayoutInfo(wpli);

                    LayoutCodeName = wpli.WebPartLayoutCodeName;

                    txtDescription.Text = "";
                    txtLayoutDisplayName.Text = "";
                    txtLayoutName.Text = "";

                    plcDescription.Visible = false;
                    plcValues.Visible = false;
                    etaCode.Rows = 17;

                    lblInfo.Visible = true;
                    lblInfo.Text = GetString("General.ChangesSaved");

                    pnlMenu.Visible = true;
                    pnlCheckOutInfo.Visible = true;
                }
                else
                {
                    lblError.Visible = true;
                    lblError.Text = GetString("WebPartPropertise.CodeNameAllreadyExists");
                    etaCode.Rows = 17;
                    return false;
                }
            }
            else
            {
                lblError.Visible = true;
                lblError.Text = result;
                etaCode.Rows = 17;
                return false;
            }
        }

        return true;
    }


    /// <summary>
    /// Sets current layout.
    /// </summary>
    protected void SetCurrentLayout(bool saveToWebPartInstance, bool updateLayout)
    {
        if ((webPart != null) && (LayoutCodeName != "|new|"))
        {
            if (saveToWebPartInstance)
            {
                if (LayoutCodeName == "|default|")
                {
                    webPart.SetValue("WebPartLayout", "");
                }
                else
                {
                    webPart.SetValue("WebPartLayout", LayoutCodeName);
                }

                bool isWebPartVariant = (variantId > 0) || (zoneVariantId > 0) || isNewVariant;
                if (!isWebPartVariant)
                {
                    // Update page template
                    PageTemplateInfoProvider.SetPageTemplateInfo(pti);
                }
                else
                {
                    // Save the variant properties
                    if ((webPart != null)
                        && (webPart.ParentZone != null)
                        && (webPart.ParentZone.ParentTemplateInstance != null)
                        && (webPart.ParentZone.ParentTemplateInstance.ParentPageTemplate != null))
                    {
                        XmlDocument doc = new XmlDocument();
                        XmlNode xmlWebParts = null;

                        if (zoneVariantId > 0)
                        {
                            // This webpart is in a zone variant therefore save the whole variant webparts
                            xmlWebParts = webPart.ParentZone.GetXmlNode(doc);
                            if (webPart.VariantMode == VariantModeEnum.MVT)
                            {
                                ModuleCommands.OnlineMarketingSaveMVTVariantWebParts(zoneVariantId, xmlWebParts);
                            }
                            else if (webPart.VariantMode == VariantModeEnum.ContentPersonalization)
                            {
                                ModuleCommands.OnlineMarketingSaveContentPersonalizationVariantWebParts(zoneVariantId, xmlWebParts);
                            }
                        }
                        else if (variantId > 0)
                        {
                            // This webpart is a web part variant
                            xmlWebParts = webPart.GetXmlNode(doc);
                            if (webPart.VariantMode == VariantModeEnum.MVT)
                            {
                                ModuleCommands.OnlineMarketingSaveMVTVariantWebParts(variantId, xmlWebParts);
                            }
                            else if (webPart.VariantMode == VariantModeEnum.ContentPersonalization)
                            {
                                ModuleCommands.OnlineMarketingSaveContentPersonalizationVariantWebParts(variantId, xmlWebParts);
                            }
                        }
                    }
                }
            }

            string parameters = this.aliasPath + "/" + this.zoneId + "/" + this.webpartId;
            string cacheName = "CMSVirtualWebParts|" + parameters.ToLower().TrimStart('/');

            CacheHelper.Remove(cacheName);

            if (updateLayout)
            {
                WebPartInfo wpi = WebPartInfoProvider.GetWebPartInfo(webPart.WebPartType);
                if (wpi != null)
                {
                    WebPartLayoutInfo wpli = WebPartLayoutInfoProvider.GetWebPartLayoutInfo(wpi.WebPartName, LayoutCodeName);
                    if (wpli != null)
                    {
                        if (CurrentUser.IsAuthorizedPerResource("CMS.Design", "EditCode"))
                        {
                            wpli.WebPartLayoutCode = etaCode.Text;
                        }
                        wpli.WebPartLayoutCSS = etaCSS.Text;

                        WebPartLayoutInfoProvider.SetWebPartLayoutInfo(wpli);
                    }
                }
            }
        }
    }


    /// <summary>
    /// Saves the webpart properties and closes the window.
    /// </summary>
    protected void btnOnOK_Click(object sender, EventArgs e)
    {
        bool err = true;

        bool update = true;
        if (LayoutCodeName == "|new|")
        {
            err = Save();
            update = false;
        }

        SetCurrentLayout(true, update);

        bool refresh = ValidationHelper.GetBoolean(this.hidRefresh.Value, false);

        string script = "";
        if (refresh)
        {
            script = "RefreshPage(); \n";
        }

        // Close the window
        if (err)
        {
            ltlScript.Text += ScriptHelper.GetScript(script + "top.window.close();");
        }
    }


    /// <summary>
    /// Saves the webpart properties.
    /// </summary>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        // New layout
        bool err = true;
        bool update = true;
        if (LayoutCodeName == "|new|")
        {
            err = Save();
            update = false;
        }

        SetCurrentLayout(true, update);

        if (err)
        {
            string url = URLHelper.Url.AbsoluteUri;
            url = URLHelper.UpdateParameterInUrl(url, "saved", "1");
            URLHelper.Redirect(url);
        }
    }


    /// <summary>
    /// Check out.
    /// </summary>
    protected void btnCheckOut_Click(object sender, EventArgs e)
    {
        // Ensure version before check-out
        using (CMSActionContext context = new CMSActionContext())
        {
            context.AllowAsyncActions = false;

            SetCurrentLayout(false, true);
        }

        try
        {
            WebPartInfo wpi = WebPartInfoProvider.GetWebPartInfo(webPart.WebPartType);
            if (wpi != null)
            {
                WebPartLayoutInfo wpli = WebPartLayoutInfoProvider.GetWebPartLayoutInfo(wpi.WebPartName, LayoutCodeName);
                if (wpli != null)
                {
                    SiteManagerFunctions.CheckOutWebPartLayout(wpli.WebPartLayoutID);
                }
            }
        }
        catch (Exception ex)
        {
            this.lblError.Text = GetString("WebPartLayout.ErrorCheckout") + ": " + ex.Message;
            this.lblError.Visible = true;
            return;
        }

        SetCheckPanel(null);
    }


    /// <summary>
    /// Undo check out.
    /// </summary>
    protected void btnUndoCheckOut_Click(object sender, EventArgs e)
    {
        try
        {
            WebPartInfo wpi = WebPartInfoProvider.GetWebPartInfo(webPart.WebPartType);
            if (wpi != null)
            {
                WebPartLayoutInfo wpli = WebPartLayoutInfoProvider.GetWebPartLayoutInfo(wpi.WebPartName, LayoutCodeName);
                if (wpli != null)
                {
                    SiteManagerFunctions.UndoCheckOutWebPartLayout(wpli.WebPartLayoutID);
                    etaCode.ReadOnly = false;
                    etaCode.Enabled = true;
                    etaCSS.ReadOnly = false;
                    etaCSS.Enabled = true;
                }
            }
        }
        catch (Exception ex)
        {
            this.lblError.Text = GetString("WebPartLayout.ErrorUndoCheckout") + ": " + ex.Message;
            this.lblError.Visible = true;
            return;
        }


        SetCheckPanel(null);
    }


    /// <summary>
    /// Check in.
    /// </summary>
    protected void btnCheckIn_Click(object sender, EventArgs e)
    {
        try
        {
            WebPartInfo wpi = WebPartInfoProvider.GetWebPartInfo(webPart.WebPartType);
            if (wpi != null)
            {
                WebPartLayoutInfo wpli = WebPartLayoutInfoProvider.GetWebPartLayoutInfo(wpi.WebPartName, LayoutCodeName);
                if (wpli != null)
                {
                    SiteManagerFunctions.CheckInWebPartLayout(wpli.WebPartLayoutID);
                    etaCode.ReadOnly = false;
                    etaCode.Enabled = true;
                    etaCSS.ReadOnly = false;
                    etaCSS.Enabled = true;
                }
            }
        }
        catch (Exception ex)
        {
            this.lblError.Text = GetString("WebPartLayout.ErrorCheckin") + ": " + ex.Message;
            this.lblError.Visible = true;
            return;
        }


        SetCheckPanel(null);
    }


    /// <summary>
    /// Sets check out/in/undo panel
    /// </summary>
    protected void SetCheckPanel(WebPartLayoutInfo mwpli)
    {
        WebPartInfo wpi = WebPartInfoProvider.GetWebPartInfo(webPart.WebPartType);
        WebPartLayoutInfo wpli = mwpli;
        if (wpi != null)
        {
            if (wpli == null)
            {
                wpli = WebPartLayoutInfoProvider.GetWebPartLayoutInfo(wpi.WebPartName, LayoutCodeName);
            }
        }

        if (wpli != null)
        {
            this.pnlCheckOutInfo.Visible = true;

            if (wpli.WebPartLayoutCheckedOutByUserID > 0)
            {
                etaCode.ReadOnly = true;
                etaCSS.ReadOnly = true;

                string username = null;
                UserInfo ui = UserInfoProvider.GetUserInfo(wpli.WebPartLayoutCheckedOutByUserID);
                if (ui != null)
                {
                    username = HTMLHelper.HTMLEncode(ui.FullName);
                }

                plcCheckOut.Visible = false;

                // Checked out by current machine
                if (wpli.WebPartLayoutCheckedOutMachineName.ToLower() == HTTPHelper.MachineName.ToLower())
                {
                    this.plcCheckIn.Visible = true;

                    this.lblCheckOutInfo.Text = String.Format(GetString("WebPartEditLayoutEdit.CheckedOut"), Server.MapPath(wpli.WebPartLayoutCheckedOutFilename));
                }
                else
                {
                    this.lblCheckOutInfo.Text = String.Format(GetString("WebPartEditLayoutEdit.CheckedOutOnAnotherMachine"), wpli.WebPartLayoutCheckedOutMachineName, username);
                }

                if (CMSContext.CurrentUser.IsGlobalAdministrator)
                {
                    this.plcUndoCheckOut.Visible = true;
                }
            }
            else
            {
                wpi = WebPartInfoProvider.GetWebPartInfo(wpli.WebPartLayoutWebPartID);
                if (wpi != null)
                {
                    this.lblCheckOutInfo.Text = String.Format(GetString("WebPartEditLayoutEdit.CheckOutInfo"), Server.MapPath(WebPartLayoutInfoProvider.GetWebPartLayoutUrl(wpi.WebPartName, wpli.WebPartLayoutCodeName, null)));

                    this.plcCheckOut.Visible = true;
                    this.plcCheckIn.Visible = false;
                    this.plcUndoCheckOut.Visible = false;
                }
            }
        }
    }

    #endregion
}
