using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.PortalEngine;
using CMS.DataEngine;
using CMS.CMSHelper;

public partial class CMSModules_OnlineMarketing_Pages_WebParts_WebPartProperties_personalized : CMSWebPartPropertiesPage
{
    #region "Page events"

    /// <summary>
    /// Raises the <see cref="E:PreInit"/> event.
    /// </summary>
    protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);

        // Set the EditedObject attribute for the UIForm
        if (variantMode == VariantModeEnum.MVT)
        {
            mvtEditElem.UIFormControl.EditedObject = BaseAbstractInfoProvider.GetInfoById(PredefinedObjectType.MVTVARIANT, QueryHelper.GetInteger("variantid", 0));
        }
        else if (variantMode == VariantModeEnum.ContentPersonalization)
        {
            cpEditElem.UIFormControl.EditedObject = BaseAbstractInfoProvider.GetInfoById(PredefinedObjectType.CONTENTPERSONALIZATIONVARIANT, QueryHelper.GetInteger("variantid", 0));
        }
    }


    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        CurrentUserInfo ui = CMSContext.CurrentUser;
        if (!ui.IsAuthorizedPerUIElement("CMS.Content", "WebPartProperties.Variant"))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.Content", "WebPartProperties.Variant");
        }

        // Check permissions (based on variant type)
        if (variantMode == VariantModeEnum.MVT)
        {
            if (!ui.IsAuthorizedPerResource("CMS.MVTest", "Read"))
            {
                // Not authorised for MV test - Read.
                RedirectToInformation(String.Format(GetString("general.permissionresource"), "Read", "CMS.MVTest"));
            }
        }
        else if (variantMode == VariantModeEnum.ContentPersonalization)
        {
            if (!ui.IsAuthorizedPerResource("CMS.ContentPersonalization", "Read"))
            {
                // Not authorised for Content personalization - Read.
                RedirectToInformation(String.Format(GetString("general.permissionresource"), "Read", "CMS.ContentPersonalization"));
            }
        }

        if (!SettingsKeyProvider.UsingVirtualPathProvider)
        {
            this.lblInfo.Text = GetString("WebPartCode.ProviderNotRunning");
            this.lblInfo.CssClass = "ErrorLabel";
        }
        else
        {
            // Setup the buttons
            btnOnOK.Click += new EventHandler(btnOnOK_Click);
            btnOnApply.Click += new EventHandler(btnOnApply_Click);

            ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "ApplyButton", ScriptHelper.GetScript(
                "function SetRefresh(refreshpage) { document.getElementById('" + this.hidRefresh.ClientID + "').value = refreshpage; } \n" +
                "function OnApplyButton(refreshpage) { SetRefresh(refreshpage); " + Page.ClientScript.GetPostBackEventReference(btnOnApply, "") + "} \n" +
                "function OnOKButton(refreshpage) { SetRefresh(refreshpage); " + Page.ClientScript.GetPostBackEventReference(btnOnOK, "") + "} \n"
            ));
        }

        if (variantMode == VariantModeEnum.MVT)
        {
            // Display MVT edit dialog
            mvtEditElem.Visible = true;
            mvtEditElem.UIFormControl.SubmitButton.Visible = false;
        }
        else if (variantMode == VariantModeEnum.ContentPersonalization)
        {
            // Display Content personalization edit dialog
            cpEditElem.Visible = true;
            cpEditElem.UIFormControl.SubmitButton.Visible = false;
        }
    }


    /// <summary>
    /// Saves the webpart properties and closes the window.
    /// </summary>
    protected void btnOnOK_Click(object sender, EventArgs e)
    {
        // Save webpart properties
        if (Save())
        {
            bool refresh = ValidationHelper.GetBoolean(this.hidRefresh.Value, false);

            string script = "";
            if (refresh)
            {
                script = "RefreshPage(); \n";
            }

            // Close the window
            ltlScript.Text += ScriptHelper.GetScript(script + "top.window.close();");
        }
    }


    /// <summary>
    /// Saves the webpart properties.
    /// </summary>
    protected void btnOnApply_Click(object sender, EventArgs e)
    {
        Save();
    }

    #endregion


    #region "Public methods"

    /// <summary>
    /// Saves webpart properties.
    /// </summary>
    public bool Save()
    {
        if (variantMode == VariantModeEnum.MVT)
        {
            return mvtEditElem.UIFormControl.SaveData(null);
        }
        else if (variantMode == VariantModeEnum.ContentPersonalization)
        {
            return cpEditElem.UIFormControl.SaveData(null);
        }

        return false;
    }

    #endregion
}

