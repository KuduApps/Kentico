using System;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.OnlineMarketing;
using CMS.TreeEngine;
using CMS.SettingsProvider;
using CMS.ExtendedControls;
using CMS.FormControls;
using CMS.PortalControls;
using CMS.PortalEngine;

// Edited object
[EditedObject(OnlineMarketingObjectType.CONTENTPERSONALIZATIONVARIANT, "variantid")]
[ParentObject(PredefinedObjectType.PAGETEMPLATE, "templateid")]
public partial class CMSModules_OnlineMarketing_Dialogs_ContentPersonalizationVariantEdit : CMSVariantDialogPage
{
    #region "Variables"

    /// <summary>
    /// Indicates whether editing a web part or a zone variant.
    /// </summary>
    private VariantTypeEnum variantType = VariantTypeEnum.Zone;

    #endregion


    #region "Page methods"

    /// <summary>
    /// Raises the <see cref="E:Init"/> event.
    /// </summary>
    protected override void OnInit(EventArgs e)
    {
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("cms.contentpersonalization", "Read"))
        {
            RedirectToAccessDenied(String.Format(GetString("general.permissionresource"), "Read", "Content personalization"));
        }

        // Set the ParentObject manually tor inherited templates
        if (editElem.UIFormControl.ParentObject == null)
        {
            string aliasPath = QueryHelper.GetString("aliaspath", string.Empty);
            // Get page info for the given document
            PageInfo pi = PageInfoProvider.GetPageInfo(CMSContext.CurrentSiteName, aliasPath, CMSContext.PreferredCultureCode, null, CMSContext.CurrentSite.CombineWithDefaultCulture);
            if (pi != null)
            {
                editElem.UIFormControl.ParentObject = pi.PageTemplateInfo;
            }
        }

        // Get information whether the control is used for a web part or zone variant
        variantType = VariantTypeFunctions.GetVariantTypeEnum(QueryHelper.GetString("varianttype", string.Empty));

        base.OnInit(e);

        // Check permissions and redirect
        OnlineMarketingContext.CheckPermissions(variantType);

        // Get the alias path of the current node
        if (Node == null)
        {
            editElem.StopProcessing = true;
        }

        editElem.UIFormControl.OnBeforeSave += new EventHandler(UIFormControl_OnBeforeSaved);
    }


    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        // Setup the modal dialog
        SetCulture();
        RegisterEscScript();

        // Setup the title, image, help
        PageTitle title = this.CurrentMaster.Title;
        title.TitleText = GetString("contentpersonalizationvariant.edit");
        title.TitleImage = GetImageUrl("Objects/OM_MVTVariant/object.png");
        title.HelpName = "helpTopic";
        // Must be set be to help icon created
        title.HelpTopicName = "cpvariant_edit";

        // Set the dark header (+ dark help icon)
        this.CurrentMaster.PanelBody.CssClass += " DialogsPageHeader";
        title.HelpIconUrl = GetImageUrl("General/HelpLargeDark.png");

        ScriptHelper.RegisterDialogScript(this);
        ScriptHelper.RegisterWOpenerScript(this);
    }


    /// <summary>
    /// Raises the <see cref="E:PreRender"/> event.
    /// </summary>
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        // Setup the modal dialog
        RegisterModalPageScripts();
    }


    /// <summary>
    /// Handles the OnSaved event of the editElem control.
    /// </summary>
    protected void UIFormControl_OnBeforeSaved(object sender, EventArgs e)
    {
        if (editElem.ValidateData())
        {
            // Add the properties to the window helper
            Hashtable parameters = new Hashtable();
            parameters.Add("displayname", ValidationHelper.GetString(((FormEngineUserControl)editElem.UIFormControl.FieldControls["VariantDisplayName"]).Value, string.Empty));
            parameters.Add("codename", ValidationHelper.GetString(((FormEngineUserControl)editElem.UIFormControl.FieldControls["VariantName"]).Value, string.Empty));
            parameters.Add("description", ValidationHelper.GetString(((FormEngineUserControl)editElem.UIFormControl.FieldControls["VariantDescription"]).Value, string.Empty));
            parameters.Add("enabled", ValidationHelper.GetBoolean(((FormEngineUserControl)editElem.UIFormControl.FieldControls["VariantEnabled"]).Value, false));
            parameters.Add("condition", ValidationHelper.GetString(((FormEngineUserControl)editElem.UIFormControl.FieldControls["VariantDisplayCondition"]).Value, string.Empty));
            WindowHelper.Add("variantProperties", parameters);

            // Set a script to open the web part properties modal dialog
            string query = URLHelper.GetQuery(URLHelper.CurrentURL);
            query = URLHelper.RemoveUrlParameter(query, "nodeid");
            query = URLHelper.AddUrlParameter(query, "variantmode", VariantModeFunctions.GetVariantModeString(VariantModeEnum.ContentPersonalization));

            // Choose the correct javascript method for opening web part/zone properties
            string functionName = string.Empty;

            switch (variantType)
            {
                case VariantTypeEnum.WebPart:
                    functionName = "OnAddWebPartVariant";
                    break;

                case VariantTypeEnum.Widget:
                    functionName = "OnAddWidgetVariant";
                    query = URLHelper.RemoveUrlParameter(query, "varianttype");
                    string widgetId = QueryHelper.GetString("webpartid", string.Empty);
                    query = URLHelper.RemoveUrlParameter(query, "webpartid");
                    query = URLHelper.AddParameterToUrl(query, "widgetid", widgetId);
                    break;

                case VariantTypeEnum.Zone:
                    functionName = "OnAddWebPartZoneVariant";
                    break;
            }

            // Setup the script for opening web part/zone properties
            string script = @"
            function OpenVariantProperties()
            {
                window.close();
                if (wopener." + functionName + @")
                {
                    wopener." + functionName + "('" + query + @"');
                }
            }

            window.onload = OpenVariantProperties;";

            ltrScript.Text = ScriptHelper.GetScript(script);
        }

        // Do not save the variant. Will be saved when saving the web part/zone properties.
        editElem.UIFormControl.StopProcessing = true;
    }

    #endregion
}
