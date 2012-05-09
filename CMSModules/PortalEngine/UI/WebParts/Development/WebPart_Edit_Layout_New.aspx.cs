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
using CMS.SettingsProvider;
using CMS.PortalEngine;
using CMS.UIControls;
using CMS.IO;

public partial class CMSModules_PortalEngine_UI_WebParts_Development_WebPart_Edit_Layout_New : SiteManagerPage
{
    // Path to web parts
    const string WEBPART_PATH = "~/CMSWebParts/";

    protected void Page_Load(object sender, EventArgs e)
    {
        // Init GUI
        lblDisplayName.Text = GetString("WebPartEditLayoutNew.lblDisplayName");
        lblCodeName.Text = GetString("WebPartEditLayoutNew.lblCodeName");
        lblCode.Text = GetString("WebPartEditLayoutNew.lblCode");
        lblDescription.Text = GetString("WebPartEditLayoutNew.lblDescription");

        rfvDisplayName.ErrorMessage = GetString("general.requiresdisplayname");
        rfvCodeName.ErrorMessage = GetString("general.requirescodename");

        // Get web part info
        int webPartId = QueryHelper.GetInteger("webpartId", 0);
        WebPartInfo wpi = WebPartInfoProvider.GetWebPartInfo(webPartId);

        // Check webpartid
        if (wpi == null)
        {
            lblError.Text = GetString("WebPartEditLayoutNew.InvalidWebPartID");
            lblError.Visible = true;
            return;
        }

        InitBreadCrumbs(webPartId);

        if (!RequestHelper.IsPostBack())
        {
            // Get default layout code
            string srcFile = this.Server.MapPath(WEBPART_PATH + wpi.WebPartFileName);
            if (File.Exists(srcFile))
            {
                tbCode.Text = File.ReadAllText(srcFile);
                tbCSS.Text = wpi.WebPartCSS;
            }
        }

        InitializeMasterPage();

        this.plcCssLink.Visible = String.IsNullOrEmpty(tbCSS.Text.Trim());
    }

    /// <summary>
    /// Initializes the master page elements.
    /// </summary>
    private void InitializeMasterPage()
    {
        // Header actions
        string[,] actions = new string[1, 11];

        // Save button
        actions[0, 0] = HeaderActions.TYPE_SAVEBUTTON;
        actions[0, 1] = GetString("General.Save");
        actions[0, 5] = GetImageUrl("CMSModules/CMS_Content/EditMenu/save.png");
        actions[0, 6] = "save";
        actions[0, 8] = "true";

        CurrentMaster.HeaderActions.LinkCssClass = "ContentSaveLinkButton";
        CurrentMaster.HeaderActions.ActionPerformed += HeaderActions_ActionPerformed;
        CurrentMaster.HeaderActions.Actions = actions;
    }


    private void HeaderActions_ActionPerformed(object sender, CommandEventArgs e)
    {
        switch (e.CommandName.ToLower())
        {
            case "save":
                Save();
                break;
        }
    }


    /// <summary>
    /// Saves the form data.
    /// </summary>
    private void Save()
    {
        // Remove "." due to virtual path provider replacement
        txtCodeName.Text = txtCodeName.Text.Replace(".", "");

        txtDisplayName.Text = txtDisplayName.Text.Trim();
        txtCodeName.Text = txtCodeName.Text.Trim();

        string errorMessage = new Validator()
            .NotEmpty(txtCodeName.Text, rfvCodeName.ErrorMessage)
            .NotEmpty(txtDisplayName.Text, rfvDisplayName.ErrorMessage)
            .IsCodeName(txtCodeName.Text, GetString("general.invalidcodename"))
            .Result;

        if (errorMessage != String.Empty)
        {
            lblError.Text = errorMessage;
            lblError.Visible = true;
            return;
        }

        // Check web part id
        int webPartId = QueryHelper.GetInteger("webpartId", 0);
        WebPartInfo wpi = WebPartInfoProvider.GetWebPartInfo(webPartId);
        if (wpi == null)
        {
            lblError.Text = GetString("WebPartEditLayoutNew.InvalidWebPartID");
            lblError.Visible = true;
            return;
        }

        // Check web part layout code name
        WebPartLayoutInfo tempwpli = WebPartLayoutInfoProvider.GetWebPartLayoutInfo(wpi.WebPartName, txtCodeName.Text);
        if (tempwpli != null)
        {
            lblError.Text = GetString("WebPartEditLayoutNew.CodeNameAlreadyExist");
            lblError.Visible = true;
            return;
        }


        // Create and fill info structure
        WebPartLayoutInfo wpli = new WebPartLayoutInfo();

        wpli.WebPartLayoutID = 0;
        wpli.WebPartLayoutCodeName = txtCodeName.Text;
        wpli.WebPartLayoutDisplayName = txtDisplayName.Text;
        wpli.WebPartLayoutCode = tbCode.Text;
        wpli.WebPartLayoutCSS = tbCSS.Text;
        wpli.WebPartLayoutDescription = txtDescription.Text;
        wpli.WebPartLayoutWebPartID = webPartId;

        WebPartLayoutInfoProvider.SetWebPartLayoutInfo(wpli);

        URLHelper.Redirect("WebPart_Edit_Layout_Frameset.aspx?layoutID=" + wpli.WebPartLayoutID + "&webpartID=" + webPartId);
    }


    private void InitBreadCrumbs(int webpartId)
    {
        InitBreadcrumbs(2);
        SetBreadcrumb(0, GetString("WebParts.Layout"), ResolveUrl(String.Format("WebPart_Edit_Layout.aspx?webpartid={0}", webpartId)), "_self", null);
        SetBreadcrumb(1, GetString("webparts_layout_newlayout"), null, null, null);
    }
}
