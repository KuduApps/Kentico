using System;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.ExtendedControls;
using CMS.GlobalHelper;
using CMS.IO;
using CMS.PortalControls;
using CMS.PortalEngine;
using CMS.SettingsProvider;
using CMS.SiteProvider;
using CMS.UIControls;
using System.Text;

public partial class CMSModules_PortalEngine_Controls_Layout_EditLayout : EditLayout
{
    #region "Variables"

    protected string culture = null;

    protected CurrentUserInfo user = null;

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Check permissions for CMS Desk -> Content -> Design -> Edit layout
        // It is placed here because this control is loaded dynamically by CMSPlaceHolder
        user = CMSContext.CurrentUser;
        if (!user.IsAuthorizedPerUIElement("CMS.Content", "Design.EditLayout"))
        {
            CMSPage.RedirectToCMSDeskUIElementAccessDenied("CMS.Content", "Design.EditLayout");
        }

        // Use UI culture for strings
        culture = user.PreferredUICultureCode;

        string saveText = ResHelper.GetString("general.save", culture);
        imgSave.ImageUrl = GetImageUrl("CMSModules/CMS_Content/EditMenu/save.png");
        imgSave.AlternateText = saveText;
        ltlSave.Text = saveText;

        string checkOutText = ResHelper.GetString("general.checkout", culture);
        imgCheckOut.ImageUrl = GetImageUrl("CMSModules/CMS_Content/EditMenu/checkout.png");
        imgCheckOut.AlternateText = checkOutText;
        ltlCheckOut.Text = checkOutText;

        string checkInText = ResHelper.GetString("general.checkinfromfile", culture);
        imgCheckIn.ImageUrl = GetImageUrl("CMSModules/CMS_Content/EditMenu/checkin.png");
        imgCheckIn.AlternateText = checkInText;
        ltlCheckIn.Text = checkInText;

        btnUndoCheckOut.OnClientClick = "return confirm(" + ScriptHelper.GetString(ResHelper.GetString("General.ConfirmUndoCheckOut", culture)) + ");";

        imgUndoCheckOut.ImageUrl = GetImageUrl("~/App_Themes/Default/Images/CMSModules/CMS_Content/EditMenu/undocheckout.png");
        imgUndoCheckOut.AlternateText = ResHelper.GetString("general.undocheckout", culture);
        ltlUndoCheckOut.Text = ResHelper.GetString("PageLayout.DiscardCheckOut", culture);

        lblType.Text = ResHelper.GetString("PageLayout.Type");

        if (this.drpType.Items.Count == 0)
        {
            drpType.Items.Add(new ListItem(ResHelper.GetString("TransformationType.Ascx"), TransformationTypeEnum.Ascx.ToString()));
            drpType.Items.Add(new ListItem(ResHelper.GetString("TransformationType.Html"), TransformationTypeEnum.Html.ToString()));
        }

        string lang = DataHelper.GetNotEmpty(SettingsHelper.AppSettings["CMSProgrammingLanguage"], "C#");
        ltlDirectives.Text = "&lt;%@ Control Language=\"" + lang + "\" ClassName=\"Simple\" Inherits=\"CMS.PortalControls.CMSAbstractLayout\" %&gt;<br />&lt;%@ Register Assembly=\"CMS.PortalControls\" Namespace=\"CMS.PortalControls\" TagPrefix=\"cc1\" %&gt;";

        // Disable buttons and inform about not usign virtual path provider
        if (!SettingsKeyProvider.UsingVirtualPathProvider)
        {
            this.btnCheckOut.Visible = false;
            this.btnCheckIn.Visible = false;
            this.btnUndoCheckOut.Visible = false;

            txtLayout.ReadOnly = true;
        }

        this.txtLayout.Editor.AutoSize = true;
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        bool editingEnabled = true;

        LayoutInfo li = this.PagePlaceholder.LayoutInfo;
        PageTemplateInfo pti = this.PagePlaceholder.PageTemplateInfo;

        // Layout save button script
        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "LayoutSave", ScriptHelper.GetScript(
            "function SaveDocument() { layoutChanged = true; " + this.Page.ClientScript.GetPostBackEventReference(this.btnSaveLayout, "") + "; } \n"
        ));

        string tmpLayoutCode = "";
        int tmpCheckUser = 0;
        string tmpMachine = "";
        string tmpFilename = "";
        string tmpLayoutUrl = "";
        string layoutUrl = "";

        LayoutTypeEnum layoutType = LayoutTypeEnum.Ascx;

        if (li != null)
        {
            tmpLayoutCode = li.LayoutCode;

            tmpCheckUser = li.LayoutCheckedOutByUserID;
            tmpMachine = li.LayoutCheckedOutMachineName;
            tmpFilename = li.LayoutCheckedOutFilename;
            tmpLayoutUrl = LayoutInfoProvider.GetLayoutUrl(li.LayoutCodeName, null, li.LayoutType);

            layoutUrl = tmpLayoutUrl;
            layoutType = li.LayoutType;
        }

        // Get layout information
        if ((li == null) && (pti != null))
        {
            tmpLayoutCode = pti.PageTemplateLayout;

            tmpCheckUser = pti.PageTemplateLayoutCheckedOutByUserID;
            tmpMachine = pti.PageTemplateLayoutCheckedOutMachineName;
            tmpFilename = pti.PageTemplateLayoutCheckedOutFileName;

            layoutUrl = PageTemplateInfoProvider.GetLayoutUrl(pti.CodeName, null, pti.PageTemplateLayoutType);
            layoutType = pti.PageTemplateLayoutType;

            if (pti.IsReusable)
            {
                tmpLayoutUrl = PageTemplateInfoProvider.GetLayoutUrl(pti.CodeName, null, pti.PageTemplateLayoutType);
            }
            else
            {
                tmpLayoutUrl = PageTemplateInfoProvider.GetAdhocLayoutUrl(pti.CodeName, null, pti.PageTemplateLayoutType);
            }
        }

        if (!RequestHelper.IsPostBack())
        {
            drpType.SelectedIndex = (layoutType == LayoutTypeEnum.Html ? 1 : 0);

            this.txtLayout.Text = tmpLayoutCode;
        }

        this.plcUndoCheckOut.Visible = false;
        this.plcCheckOut.Visible = false;
        this.plcCheckIn.Visible = false;

        // Check checked-out status
        if (tmpCheckUser > 0)
        {
            this.txtLayout.ReadOnly = true;

            string username = null;
            UserInfo ui = UserInfoProvider.GetUserInfo(tmpCheckUser);
            if (ui != null)
            {
                username = HTMLHelper.HTMLEncode(ui.FullName);
            }

            // Checked out by current machine
            if ((HttpContext.Current != null) && (tmpMachine.ToLower() == HTTPHelper.MachineName.ToLower()))
            {
                this.plcCheckIn.Visible = true;

                this.lblCheckOutInfo.Text = String.Format(ResHelper.GetString("PageLayout.CheckedOut", culture), HttpContext.Current.Server.MapPath(tmpFilename));
            }
            else
            {
                this.lblCheckOutInfo.Text = String.Format(ResHelper.GetString("PageLayout.CheckedOutOnAnotherMachine", culture), HTMLHelper.HTMLEncode(tmpMachine), HTMLHelper.HTMLEncode(username));
            }
            //this.lblCheckOutInfo.Text = String.Format(ResHelper.GetString("PageLayout.CheckedOutOnAnotherMachine"), li.LayoutCheckedOutMachineName, username);

            if (user.IsGlobalAdministrator)
            {
                this.plcUndoCheckOut.Visible = true;
            }
        }
        else
        {
            if (HttpContext.Current != null)
            {
                this.lblCheckOutInfo.Text = String.Format(ResHelper.GetString("PageLayout.CheckOutInfo", culture), HttpContext.Current.Server.MapPath(tmpLayoutUrl));
            }
            this.plcCheckOut.Visible = true;
            this.txtLayout.ReadOnly = false;
        }

        bool isAscx = (this.drpType.SelectedValue.ToLower() == "ascx");

        this.btnSaveLayout.Visible = !isAscx || SettingsKeyProvider.UsingVirtualPathProvider;

        // Disable items when virtual path provider is disabled
        if (isAscx && !SettingsKeyProvider.UsingVirtualPathProvider && (pti != null))
        {
            this.lblVirtualInfo.Text = String.Format(ResHelper.GetString("TemplateLayout.VirtualPathProviderNotRunning", culture), PageTemplateInfoProvider.GetLayoutUrl(pti.CodeName, null));
            this.plcVirtualInfo.Visible = true;
            this.pnlCheckOutInfo.Visible = false;

            editingEnabled = false;
        }

        string info = null;

        // Setup the information and code type
        if (isAscx)
        {
            txtLayout.Editor.Language = LanguageEnum.ASPNET;
            txtLayout.UseAutoComplete = false;

            info = ResHelper.GetString("Administration-PageLayout_New.Hint", culture);

            // Check the edit code permission
            if (!user.IsAuthorizedPerResource("CMS.Design", "EditCode"))
            {
                editingEnabled = false;
                info = ResHelper.GetString("EditCode.NotAllowed", culture);
            }
        }
        else
        {
            txtLayout.Editor.Language = LanguageEnum.HTMLMixed;
            txtLayout.UseAutoComplete = true;

            info = ResHelper.GetString("EditLayout.HintHtml", culture);
        }

        if (!String.IsNullOrEmpty(lblLayoutInfo.Text))
        {
            lblLayoutInfo.Text += "&nbsp;&nbsp;";
        }

        lblLayoutInfo.Text += info;

        this.lblLayoutInfo.Visible = (this.lblLayoutInfo.Text != "");

        this.txtLayout.ReadOnly = !editingEnabled;
        this.plcActions.Visible = editingEnabled;

        // Disable editor for layout or template checked out
        if (li != null)
        {
            if (li.LayoutCheckedOutByUserID > 0)
            {
                txtLayout.ReadOnly = true;
            }
        }
        else if (pti != null)
        {
            if (pti.PageTemplateLayoutCheckedOutByUserID > 0)
            {
                txtLayout.ReadOnly = true;
            }
        }
    }


    /// <summary>
    /// Saves the layout code.
    /// </summary>
    /// <returns>Returns true if save successful</returns>
    private bool SaveLayout()
    {
        LayoutInfo li = this.PagePlaceholder.LayoutInfo;
        PageTemplateInfo pti = this.PagePlaceholder.PageTemplateInfo;

        if ((li != null) || (pti != null))
        {
            string mPath = "";
            if ((HttpContext.Current != null) && (HttpContext.Current.Server != null))
            {
                mPath = HttpContext.Current.Server.UrlPathEncode(URLHelper.ApplicationPath);
            }

            LayoutTypeEnum layoutType = LayoutInfoProvider.GetLayoutTypeEnum(this.drpType.SelectedValue);

            // Check the permissions
            if ((layoutType != LayoutTypeEnum.Ascx) || user.IsAuthorizedPerResource("CMS.Design", "EditCode"))
            {
                if (li != null)
                {
                    // Shared layout
                    li.LayoutType = layoutType;
                    li.LayoutCode = HTMLHelper.UnResolveUrls(this.txtLayout.Text, mPath);

                    LayoutInfoProvider.SetLayoutInfo(li);
                }
                else
                {
                    // Custom layout
                    pti.PageTemplateLayoutType = layoutType;
                    pti.PageTemplateLayout = HTMLHelper.UnResolveUrls(this.txtLayout.Text, mPath);

                    PageTemplateInfoProvider.SetPageTemplateInfo(pti);
                }
            }
            this.lblLayoutInfo.Text = ResHelper.GetString("General.ChangesSaved", culture);

            // Resave page template XML
            if (layoutType == LayoutTypeEnum.Ascx)
            {
                PageTemplateInfo ti = this.PagePlaceholder.PageInfo.PageTemplateInfo;
                if ((ti != null) && (ti.PageTemplateId > 0))
                {
                    try
                    {
                        // Load the layout
                        CMSAbstractLayout layout = (CMSAbstractLayout)this.Page.LoadControl(LayoutInfoProvider.GetLayoutUrl(li.LayoutCodeName, li.LayoutVersionGUID));
                        layout.ID = "abstractLayout";

                        // Remove zones without web parts
                        ArrayList removeZones = new ArrayList();
                        foreach (WebPartZoneInstance zone in ti.WebPartZones)
                        {
                            if (zone.WebParts.Count <= 0)
                            {
                                removeZones.Add(zone);
                            }
                        }
                        foreach (WebPartZoneInstance zone in removeZones)
                        {
                            ti.WebPartZones.Remove(zone);
                        }

                        // Ensure all zones
                        foreach (DictionaryEntry zone in layout.WebPartZones)
                        {
                            ti.EnsureZone(((CMSWebPartZone)zone.Value).ID);
                        }

                        this.Controls.Remove(layout);

                        // Save the page template
                        PageTemplateInfoProvider.SetPageTemplateInfo(ti);
                    }
                    catch
                    {
                    }
                }
            }

            return true;
        }
        else
        {
            return false;
        }
    }


    /// <summary>
    /// Save layout code.
    /// </summary>
    protected void btnSaveLayout_Click(object sender, EventArgs e)
    {
        SaveLayout();
    }


    /// <summary>
    /// Check out layout event handler.
    /// </summary>
    protected void btnCheckOut_Click(object sender, EventArgs e)
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

        LayoutInfo li = this.PagePlaceholder.LayoutInfo;
        PageTemplateInfo pti = this.PagePlaceholder.PageTemplateInfo;

        if ((li != null) || (pti != null))
        {
            try
            {
                string filename = "";
                string tmpCode = "";
                LayoutTypeEnum layoutType = LayoutTypeEnum.Ascx;

                if (li != null)
                {
                    filename = LayoutInfoProvider.GetLayoutUrl(li.LayoutCodeName, null, li.LayoutType);
                    layoutType = li.LayoutType;

                    tmpCode = li.LayoutCode;
                }
                else
                {
                    filename = PageTemplateInfoProvider.GetLayoutUrl(pti.CodeName, null, pti.PageTemplateLayoutType);
                    layoutType = pti.PageTemplateLayoutType;

                    tmpCode = pti.PageTemplateLayout;
                }

                // Write the code to the file
                string fullfilename = "";
                if (HttpContext.Current != null)
                {
                    fullfilename = HttpContext.Current.Server.MapPath(filename);
                    DirectoryHelper.EnsureDiskPath(fullfilename, HttpContext.Current.Server.MapPath("~/"));
                }

                StringBuilder sb = new StringBuilder();
                if (layoutType == LayoutTypeEnum.Ascx)
                {
                    sb.Append(LayoutInfoProvider.GetLayoutDirectives());
                }
                sb.Append(tmpCode);

                string content = HTMLHelper.EnsureLineEnding(sb.ToString(), "\r\n");
                File.WriteAllText(fullfilename, content);

                // Set the layout data
                if (li != null)
                {
                    // Shared layout
                    li.LayoutCheckedOutByUserID = user.UserID;
                    li.LayoutCheckedOutMachineName = "";
                    if (HttpContext.Current != null)
                    {
                        li.LayoutCheckedOutMachineName = HTTPHelper.MachineName;
                    }
                    li.LayoutCheckedOutFilename = filename;

                    LayoutInfoProvider.SetLayoutInfo(li);
                }
                else
                {
                    // Page template layout
                    pti.PageTemplateLayoutCheckedOutByUserID = user.UserID;
                    pti.PageTemplateLayoutCheckedOutMachineName = "";
                    if (HttpContext.Current != null)
                    {
                        pti.PageTemplateLayoutCheckedOutMachineName = HTTPHelper.MachineName;
                    }
                    pti.PageTemplateLayoutCheckedOutFileName = filename;

                    PageTemplateInfoProvider.SetPageTemplateInfo(pti);
                }
            }
            catch
            {
                return;
            }
        }
    }


    /// <summary>
    /// Discard check out event handler.
    /// </summary>
    protected void btnUndoCheckOut_Click(object sender, EventArgs e)
    {
        LayoutInfo li = this.PagePlaceholder.LayoutInfo;
        PageTemplateInfo pti = this.PagePlaceholder.PageTemplateInfo;

        if ((li != null) || (pti != null))
        {
            string filename = "";
            string tmpFileName = "";

            if (li != null)
            {
                tmpFileName = li.LayoutCheckedOutFilename;
            }
            else
            {
                tmpFileName = pti.PageTemplateLayoutCheckedOutFileName;
            }

            if (HttpContext.Current != null)
            {
                filename = HttpContext.Current.Server.MapPath(tmpFileName);
            }
            if (File.Exists(filename))
            {
                File.Delete(filename);
            }

            if (li != null)
            {
                li.LayoutCheckedOutByUserID = 0;
                li.LayoutCheckedOutFilename = "";
                li.LayoutCheckedOutMachineName = "";
                LayoutInfoProvider.SetLayoutInfo(li);
            }
            else
            {
                pti.PageTemplateLayoutCheckedOutByUserID = 0;
                pti.PageTemplateLayoutCheckedOutFileName = "";
                pti.PageTemplateLayoutCheckedOutMachineName = "";
                PageTemplateInfoProvider.SetPageTemplateInfo(pti);
            }
        }
    }


    /// <summary>
    /// Check in event handler.
    /// </summary>
    protected void btnCheckIn_Click(object sender, EventArgs e)
    {
        LayoutInfo li = this.PagePlaceholder.LayoutInfo;
        PageTemplateInfo pti = this.PagePlaceholder.PageTemplateInfo;

        if ((li != null) || (pti != null))
        {
            string filename = "";
            string tmpFileName = "";

            if (li != null)
            {
                tmpFileName = li.LayoutCheckedOutFilename;
            }
            else
            {
                tmpFileName = pti.PageTemplateLayoutCheckedOutFileName;
            }

            if (HttpContext.Current != null)
            {
                filename = HttpContext.Current.Server.MapPath(tmpFileName);
            }
            StreamReader sr = StreamReader.New(filename);

            // Read away the directive lines
            int skiplines = LayoutInfoProvider.GetLayoutDirectives().Split('\n').Length - 1;
            for (int i = 0; i < skiplines; i++)
            {
                sr.ReadLine();
            }

            string newcode = sr.ReadToEnd();
            sr.Close();
            File.Delete(filename);

            if (li != null)
            {
                li.LayoutCheckedOutByUserID = 0;
                li.LayoutCheckedOutFilename = "";
                li.LayoutCheckedOutMachineName = "";
                li.LayoutCode = newcode;
                LayoutInfoProvider.SetLayoutInfo(li);
            }
            else
            {
                pti.PageTemplateLayoutCheckedOutByUserID = 0;
                pti.PageTemplateLayoutCheckedOutFileName = "";
                pti.PageTemplateLayoutCheckedOutMachineName = "";
                pti.PageTemplateLayout = newcode;
                PageTemplateInfoProvider.SetPageTemplateInfo(pti);
            }

            txtLayout.Text = newcode;
        }        
    }

    #endregion
}
