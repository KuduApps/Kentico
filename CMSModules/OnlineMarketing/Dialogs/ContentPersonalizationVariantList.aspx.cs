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
using CMS.PortalEngine;

public partial class CMSModules_OnlineMarketing_Dialogs_ContentPersonalizationVariantList : CMSVariantDialogPage
{
    #region "Variables"

    /// <summary>
    /// Indicates whether editing a web part or a zone variant.
    /// </summary>
    private VariantTypeEnum variantType = VariantTypeEnum.Zone;

    // Setup the list control properties
    int pageTemplateId = 0;
    string zoneId = string.Empty;
    Guid instanceGuid = Guid.Empty;
    string aliasPath = string.Empty;
    string webPartId = string.Empty;

    #endregion


    #region "Page methods"

    /// <summary>
    /// Raises the <see cref="E:Init"/> event.
    /// </summary>
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("cms.contentpersonalization", "Read"))
        {
            RedirectToAccessDenied(String.Format(GetString("general.permissionresource"), "Read", "Content personalization"));
        }
       
        variantType = listElem.VariantType = VariantTypeFunctions.GetVariantTypeEnum(QueryHelper.GetString("varianttype", string.Empty));

        // Check permissions and redirect
        OnlineMarketingContext.CheckPermissions(variantType);

        // Get the alias path of the current node
        if (Node == null)
        {
            listElem.StopProcessing = true;
        }

        // Setup the list control properties
        pageTemplateId = listElem.PageTemplateID = QueryHelper.GetInteger("templateid", 0);
        zoneId = listElem.ZoneID = QueryHelper.GetString("zoneid", string.Empty);
        instanceGuid = listElem.InstanceGUID = QueryHelper.GetGuid("instanceguid", Guid.Empty);
        aliasPath = QueryHelper.GetString("aliaspath", string.Empty);
        webPartId = QueryHelper.GetString("webpartid", string.Empty);
        btnClose.Text = GetString("general.close");
    }


    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        // Setup the modal dialog
        SetCulture();
        RegisterEscScript();

        ScriptHelper.RegisterDialogScript(this);
        ScriptHelper.RegisterWOpenerScript(this);

        // Setup the title, image, help
        PageTitle title = this.CurrentMaster.Title;
        title.HelpName = "helpTopic";


        title.TitleText = GetString("contentpersonalizationvariant.list");
        title.TitleImage = GetImageUrl("Objects/OM_MVTVariant/object.png");
        // Must be set be to help icon created
        title.HelpTopicName = "contentpersonalizationvariant_list";

        // Set the dark header (+ dark help icon)
        this.CurrentMaster.PanelBody.CssClass += " DialogsPageHeader";
        title.HelpIconUrl = GetImageUrl("General/HelpLargeDark.png");

        // Get Edit action button
        CMS.UIControls.UniGridConfig.Action editAction = null;
        editAction = listElem.Grid.GridActions.Actions[0] as CMS.UIControls.UniGridConfig.Action;
        editAction.OnClick = "OpenVariantProperties({0}); return false;";

        // Set NodeID in order to check the access to the document
        listElem.NodeID = NodeID;
        listElem.AllowSorting = false;
        listElem.OnAction += new CommandEventHandler(listElem_OnAction);
    }


    /// <summary>
    /// Raises the <see cref="E:PreRender"/> event.
    /// </summary>
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        // Setup the modal dialog
        RegisterModalPageScripts();

        // Choose the correct javascript method for opening web part/zone properties
        string functionName = string.Empty;
        // Set a script to open the web part properties modal dialog
        string parameters = string.Empty;
        // Set the script to show the corresponding variant
        string setVariant = string.Empty;

        switch (variantType)
        {
            case VariantTypeEnum.WebPart:
                functionName = "ConfigureWebPart";
                parameters = "'" + zoneId + "','" + webPartId + "','" + aliasPath + "','" + instanceGuid + "'," + pageTemplateId;
                setVariant = "SetVariant('Variant_WP_" + instanceGuid.ToString("N") + "', variantId);";
                break;

            case VariantTypeEnum.Widget:
                functionName = "ConfigureWidget";
                parameters = "'" + zoneId + "','" + webPartId + "','" + aliasPath + "','" + instanceGuid + "'," + pageTemplateId;
                setVariant = "SetVariant('Variant_WP_" + instanceGuid.ToString("N") + "', variantId);";
                break;

            case VariantTypeEnum.Zone:
                functionName = "ConfigureWebPartZone";
                parameters = "'" + zoneId + "','" + aliasPath + "'," + pageTemplateId + "," + QueryHelper.GetString("islayoutzone", "false");
                setVariant = "SetVariant('Variant_Zone_" + HTMLHelper.HTMLEncode(zoneId) + "', variantId);";
                break;
        }

        // Setup the script for opening web part/zone properties
        string script = @"
            function OpenVariantProperties(variantId)
            {
                if ((wopener." + functionName + @") && (wopener.SetVariant))
                {
                    wopener." + setVariant + @"
                    wopener." + functionName + "(" + parameters + @");
                }
                window.close();
            }

            function RefreshPage() {
                if (document.getElementById('" + hdnRefreshSlider.ClientID + @"').value == '1') {
                    wopener = parent.wopener;
                    if (wopener.RefreshPage) {
                        wopener.RefreshPage();
                    }
                }
            }

            window.onunload = RefreshPage;";

        ltrScript.Text = ScriptHelper.GetScript(script);
    }


    /// <summary>
    /// Handles the OnAction event of the listElem control. This events is raised when deleting a variant in the grid or changing its position (moving up/down)
    /// </summary>
    protected void listElem_OnAction(object sender, EventArgs e)
    {
        // Set the flag that a variant has been deleted or moved up/down
        hdnRefreshSlider.Value = "1";
    }

    #endregion
}
