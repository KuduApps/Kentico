using System;
using System.Web;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.ExtendedControls;
using CMS.GlobalHelper;
using CMS.PortalControls;
using CMS.PortalEngine;
using CMS.UIControls;
using System.Collections;

public partial class CMSModules_Widgets_LiveDialogs_WidgetProperties_Header : LivePage
{
    #region "Variables"

    protected string widgetId = "";
    protected string widgetName = "";
    protected string aliasPath = "";
    protected int templateId = 0;
    bool isInline = false;

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        RegisterDialogCSSLink();
        SetLiveDialogClass();

        CMSDialogHelper.RegisterDialogHelper(this.Page);
        ScriptHelper.RegisterWOpenerScript(this.Page);
        ScriptHelper.RegisterScriptFile(this.Page, "Dialogs/HTMLEditor.js");

        // Public user is not allowed for widgets
        if (!CMSContext.CurrentUser.IsAuthenticated())
        {
            RedirectToAccessDenied(GetString("widgets.security.notallowed"));
        }

        widgetId = QueryHelper.GetString("widgetid", "");
        widgetName = QueryHelper.GetString("widgetname", "");
        aliasPath = QueryHelper.GetString("aliasPath", "");
        isInline = QueryHelper.GetBoolean("inline", false);
        bool isNew = QueryHelper.GetBoolean("isnew", false);

        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), ScriptHelper.NEWWINDOW_SCRIPT_KEY, ScriptHelper.NewWindowScript);

        // initialize page title
        PageTitle.TitleText = GetString("Widgets.Properties.Title");
        PageTitle.TitleImage = GetImageUrl("CMSModules/CMS_PortalEngine/Widgetproperties.png");

        if (!RequestHelper.IsPostBack())
        {
            InitalizeMenu();
        }

        //if inline edit register postback for get widget data (from JS editor)
        if (isInline && !isNew)
        {
            if (!RequestHelper.IsPostBack())
            {
                ltlScript.Text = ScriptHelper.GetScript("function DoHiddenPostback(){" + Page.ClientScript.GetPostBackEventReference(btnHidden, "") + "}");
                ltlScript.Text += ScriptHelper.GetScript("GetSelected('" + hdnSelected.ClientID + "');");

            }
        }
    }


    /// <summary>
    /// Initializes menu.
    /// </summary>
    protected void InitalizeMenu()
    {
        string zoneId = QueryHelper.GetString("zoneid", "");
        Guid instanceGuid = QueryHelper.GetGuid("instanceguid", Guid.Empty);
        bool isNewWidget = QueryHelper.GetBoolean("isnew", false);
        WidgetZoneTypeEnum zoneType = WidgetZoneTypeEnum.None;

        if (!String.IsNullOrEmpty(widgetId) || !String.IsNullOrEmpty(widgetName))
        {
            WidgetInfo wi = null;

            // get pageinfo
            PageInfo pi = null;
            try
            {
                pi = CMSWebPartPropertiesPage.GetPageInfo(aliasPath, templateId);
            }
            catch (PageNotFoundException)
            {
                // Do not throw exception if page info not found (e.g. bad alias path)
            }

            if (pi == null)
            {
                this.Visible = false;
                return;
            }

            // Get template instance
            PageTemplateInstance templateInstance = CMSPortalManager.GetTemplateInstanceForEditing(pi);
            if (templateInstance != null)
            {
                if (templateInstance != null)
                {
                    // Get zone type
                    WebPartZoneInstance zoneInstance = templateInstance.GetZone(zoneId);

                    if (zoneInstance != null)
                    {
                        zoneType = zoneInstance.WidgetZoneType;
                    }

                    // Get web part
                    WebPartInstance widget = templateInstance.GetWebPart(instanceGuid, widgetId);

                    if ((widget != null) && widget.IsWidget)
                    {
                        // WebPartType = codename, get widget by codename 
                        wi = WidgetInfoProvider.GetWidgetInfo(widget.WebPartType);
                    }
                }
            }

            // New widget
            if (isNewWidget)
            {
                int id = ValidationHelper.GetInteger(widgetId, 0);
                if (id > 0)
                {
                    wi = WidgetInfoProvider.GetWidgetInfo(id);
                }
                else if (!String.IsNullOrEmpty(widgetName))
                {
                    wi = WidgetInfoProvider.GetWidgetInfo(widgetName);
                }
            }

            // Get widget info from name if not found yet
            if ((wi == null) && (!String.IsNullOrEmpty(widgetName)))
            {
                wi = WidgetInfoProvider.GetWidgetInfo(widgetName);
            }

            if (wi != null)
            {
                PageTitle.TitleText = GetString("Widgets.Properties.Title") + " (" + HTMLHelper.HTMLEncode(ResHelper.LocalizeString(wi.WidgetDisplayName)) + ")";
            }

            // If no zonetype defined or not inline dont show documentation 
            string documentationUrl = String.Empty;
            switch (zoneType)
            {
                case WidgetZoneTypeEnum.Dashboard:
                case WidgetZoneTypeEnum.Editor:
                case WidgetZoneTypeEnum.Group:
                case WidgetZoneTypeEnum.User:
                    documentationUrl = ResolveUrl("~/CMSModules/Widgets/LiveDialogs/WidgetDocumentation.aspx");
                    break;

                // If no zone set dont create documentation link
                default:
                    if (isInline)
                    {
                        documentationUrl = ResolveUrl("~/CMSModules/Widgets/Dialogs/WidgetDocumentation.aspx");
                    }
                    else
                    {
                        return;
                    }
                    break;
            }

            // Generate documentation link
            Literal ltr = new Literal();
            PageTitle.RightPlaceHolder.Controls.Add(ltr);

            // Ensure correct parameters in documentation url
            documentationUrl += URLHelper.GetQuery(URLHelper.CurrentURL);
            if (!String.IsNullOrEmpty(widgetName))
            {
                documentationUrl = URLHelper.UpdateParameterInUrl(documentationUrl, "widgetname", widgetName);
            }
            if (!String.IsNullOrEmpty(widgetId))
            {
                documentationUrl = URLHelper.UpdateParameterInUrl(documentationUrl, "widgetid", widgetId);
            }
            string docScript = "NewWindow('" + documentationUrl + "', 'WebPartPropertiesDocumentation', 800, 800); return false;";

            ltr.Text = "<table cellpadding=\"0\" cellspacing=\"0\"><tr><td>";
            ltr.Text += "<a onclick=\"" + docScript + "\" href=\"#\"><img src=\"" + ResolveUrl(GetImageUrl("CMSModules/CMS_PortalEngine/Documentation.png")) + "\" style=\"border-width: 0px;\"></a>";
            ltr.Text += "</td>";
            ltr.Text += "<td>";
            ltr.Text += "<a onclick=\"" + docScript + "\" href=\"#\">" + GetString("WebPartPropertie.DocumentationLink") + "</a>";
            ltr.Text += "</td></tr></table>";
        }
    }


    /// <summary>
    /// Save widget state to definition.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnHidden_Click(object sender, EventArgs e)
    {
        string value = HttpUtility.UrlDecode(hdnSelected.Value);
        if (!String.IsNullOrEmpty(value))
        {
            // Parse defininiton 
            Hashtable parameters = CMSDialogHelper.GetHashTableFromString(value);
            // Trim control name
            if (parameters["name"] != null)
            {
                widgetName = parameters["name"].ToString();

            }

            InitalizeMenu();

            SessionHelper.SetValue("WidgetDefinition", value);
        }

        string dialogUrl = "~/CMSModules/Widgets/LiveDialogs/widgetproperties_properties_frameset.aspx" + URLHelper.Url.Query;

        ltlScript.Text = ScriptHelper.GetScript("if (window.parent.frames['widgetpropertiescontent']) { window.parent.frames['widgetpropertiescontent'].location= '" + ResolveUrl(dialogUrl) + "';} ");

    }

    #endregion
}