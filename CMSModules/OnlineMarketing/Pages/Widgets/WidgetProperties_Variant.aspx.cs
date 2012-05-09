using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.PortalControls;
using CMS.PortalEngine;
using CMS.DataEngine;
using CMS.SettingsProvider;
using CMS.CMSHelper;

public partial class CMSModules_OnlineMarketing_Pages_Widgets_WidgetProperties_Variant : CMSDeskPage
{
    #region "Variables"

    protected string aliasPath = QueryHelper.GetString("aliaspath", "");
    protected int templateId = QueryHelper.GetInteger("templateid", 0);
    protected Guid instanceGuid = QueryHelper.GetGuid("instanceguid", Guid.Empty);
    protected string widgetId = QueryHelper.GetString("widgetid", "");
    protected VariantModeEnum variantMode = VariantModeFunctions.GetVariantModeEnum(QueryHelper.GetString("variantmode", ""));

    #endregion


    #region "Page events"

    /// <summary>
    /// PreInit event handler
    /// </summary>
    protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);
        RequireSite = false;

        LoadEditForm();
    }


    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!CMSContext.CurrentUser.IsAuthorizedPerUIElement("CMS.Content", "WidgetProperties.Variant"))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.Content", "WidgetProperties.Variant");
        }

        // Setup the buttons
        btnOnOK.Click += new EventHandler(btnOnOK_Click);
        btnOnApply.Click += new EventHandler(btnOnApply_Click);

        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "ApplyButton", ScriptHelper.GetScript(
            "function SetRefresh(refreshpage) { document.getElementById('" + this.hidRefresh.ClientID + "').value = refreshpage; } \n" +
            "function OnApplyButton(refreshpage) { SetRefresh(refreshpage); " + Page.ClientScript.GetPostBackEventReference(btnOnApply, "") + "} \n" +
            "function OnOKButton(refreshpage) { SetRefresh(refreshpage); " + Page.ClientScript.GetPostBackEventReference(btnOnOK, "") + "} \n"
        ));
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


    #region "Private methods"

    /// <summary>
    /// Loads and displays either the MVT or Content personalization edit form.
    /// </summary>
    /// <param name="forceReload">if true, then reloads the edit form content</param>
    private void LoadEditForm()
    {
        // Set the EditedObject attribute for the UIForm
        if (variantMode == VariantModeEnum.MVT)
        {
            mvtEditElem.UIFormControl.EditedObject = BaseAbstractInfoProvider.GetInfoById(PredefinedObjectType.MVTVARIANT, QueryHelper.GetInteger("variantid", 0));

            // Display MVT edit dialog
            mvtEditElem.Visible = true;
            mvtEditElem.UIFormControl.SubmitButton.Visible = false;
        }
        else if (variantMode == VariantModeEnum.ContentPersonalization)
        {
            cpEditElem.UIFormControl.EditedObject = BaseAbstractInfoProvider.GetInfoById(PredefinedObjectType.CONTENTPERSONALIZATIONVARIANT, QueryHelper.GetInteger("variantid", 0));

            // Display Content personalization edit dialog
            cpEditElem.Visible = true;
            cpEditElem.UIFormControl.SubmitButton.Visible = false;
        }
    }

    #endregion
}
