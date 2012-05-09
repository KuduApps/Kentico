using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.SiteProvider;
using CMS.PortalEngine;

public partial class CMSModules_InlineControls_Controls_CKEditor_InsertInlineControls : CMSPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblAvailableControls.Text = GetString("SelectInlineControlDialog.AvailableControls");

        btnInsert.Text = GetString("General.OK");

        lstControls.DataBound += new EventHandler(lstControls_DataBound);

        if (!RequestHelper.IsPostBack())
        {
            // fill listbox with inline controls available for the current site
            ReloadControls();
        }
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        if (CMSContext.ViewMode == ViewModeEnum.LiveSite)
        {
            // Register custom css if exists
            RegisterDialogCSSLink();
            SetLiveDialogClass();
        }
    }


    /// <summary>
    /// Reloads list of available controls.
    /// </summary>
    protected void ReloadControls()
    {
        if (CMSContext.CurrentSite != null)
        {
            // get controls for selected site
            lstControls.DataSource = InlineControlInfoProvider.GetInlineControlsForSite(CMSContext.CurrentSite.SiteID, "ControlName, ControlDisplayName");
            lstControls.DataBind();

            // select and display first control of the list by default
            if (lstControls.Items.Count > 0)
            {
                lstControls.SelectedIndex = 0;
                ReloadControlProperties();
            }
            else
            {
                HideControlParameterName();
            }
        }
    }


    protected void lstControls_DataBound(object sender, EventArgs e)
    {
        this.lstControls.Items.Remove(new ListItem("Media", "Media"));
        this.lstControls.Items.Remove(new ListItem("Image control", "Image"));
        this.lstControls.Items.Remove(new ListItem("YouTube video", "YouTubeVideo"));
        this.lstControls.Items.Remove(new ListItem("Media file", "MediaFileControl"));
        this.lstControls.Items.Remove(new ListItem("Widget", "Widget"));
    }


    /// <summary>
    /// Reloads selected control displayed properties (name, description, parameter name).
    /// </summary>
    protected void ReloadControlProperties()
    {
        string selectedControlName = lstControls.SelectedValue;

        // get selected control info
        InlineControlInfo control = InlineControlInfoProvider.GetInlineControlInfo(selectedControlName);
        if (control != null)
        {
            lblControlName.Text = control.ControlDisplayName;
            lblControlDescription.Text = control.ControlDescription;

            // hide or display control parameter name textbox
            if (control.ControlParameterName != "")
            {
                DisplayControlParameterName();
                lblControlParametrName.Text = control.ControlParameterName + " : ";
            }
            else
            {
                HideControlParameterName();
            }
        }
        else
        {
            HideControlParameterName();
        }
    }


    /// <summary>
    /// Called when selected control changed - displays selected control properties.
    /// </summary>
    protected void lstControls_SelectedIndexChanged(object sender, EventArgs e)
    {
        ReloadControlProperties();
    }


    /// <summary>
    /// Called when OK button is clicked - sends selected control info back to the CK editor.
    /// </summary>
    protected void btnOK_Click(object sender, EventArgs e)
    {
        string result = "";

        if ((lstControls.SelectedValue != null) && (lstControls.SelectedValue != ""))
        {
            result = lstControls.SelectedValue;

            if (txtControlParametrName.Visible)
            {
                // add parameter name
                result += ("?" + txtControlParametrName.Text.Trim());
            }

            // get inline control macro
            result = "%%control:" + result + "%%";

            // return macro to the CK editor
            ltlScript.Text = ScriptHelper.GetScript("InsertUserControl(" + ScriptHelper.GetString(result) + ");");
        }
        else
        {
            // close window
            ltlScript.Text = ScriptHelper.GetScript("window.parent.Cancel();");
        }
    }


    /// <summary>
    /// Hides control parameter name textbox.
    /// </summary>
    protected void HideControlParameterName()
    {
        lblControlParametrName.Visible = false;
        txtControlParametrName.Visible = false;
    }


    /// <summary>
    /// Displays control parameter name textbox.
    /// </summary>
    protected void DisplayControlParameterName()
    {
        lblControlParametrName.Visible = true;
        txtControlParametrName.Visible = true;
    }
}
