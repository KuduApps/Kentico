using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using System.Xml;

using CMS.UIControls;
using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.PortalEngine;
using CMS.SiteProvider;
using CMS.FormEngine;
using CMS.PortalControls;


public partial class CMSModules_PortalEngine_Controls_WebParts_WebPartDocumentation : CMSAdminControl
{

    #region "Variables"

    string webPartId = "";
    string widgetID = String.Empty;
    string plImg = "";
    string minImg = "";
    WidgetZoneTypeEnum zoneType = WidgetZoneTypeEnum.None;
    WebPartInfo wpi = null;
    WidgetInfo wi = null;
    ContextResolver resolver = null;
    string mFooterClientID = String.Empty;

    #endregion


    #region "Properties"

    /// <summary>
    /// ClientID for footer.
    /// </summary>
    public string FooterClientID
    {
        get
        {
            return mFooterClientID;
        }
        set
        {
            mFooterClientID = value;
        }
    }

    #endregion


    #region "Methods"

    /// <summary>
    /// Generate documentation page.
    /// </summary>
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        // Get current resolver
        resolver = CMSContext.CurrentResolver.CreateContextChild();

        plImg = GetImageUrl("CMSModules/CMS_PortalEngine/WebpartProperties/plus.png");
        minImg = GetImageUrl("CMSModules/CMS_PortalEngine/WebpartProperties/minus.png");

        webPartId = QueryHelper.GetString("webPartId", String.Empty);
        if (webPartId != String.Empty)
        {
            wpi = WebPartInfoProvider.GetWebPartInfo(webPartId);
        }

        string aliasPath = QueryHelper.GetString("aliaspath", String.Empty);
        // Ensure correct view mode 
        if (String.IsNullOrEmpty(aliasPath))
        {
            // Ensure the dashboard mode for the dialog
            if (QueryHelper.Contains("dashboard"))
            {
                PortalContext.SetRequestViewMode(ViewModeEnum.DashboardWidgets);
                PortalContext.DashboardName = QueryHelper.GetString("dashboard", String.Empty);
                PortalContext.DashboardSiteName = QueryHelper.GetString("sitename", String.Empty);
            }
            // Ensure the design mode for the dialog
            else
            {
                PortalContext.SetRequestViewMode(ViewModeEnum.Design);
            }
        }

        // If widgetId is in query create widget documentation
        widgetID = QueryHelper.GetString("widgetId", String.Empty);
        if (widgetID != String.Empty)
        {
            // Get widget from instance           
            string zoneId = QueryHelper.GetString("zoneid", String.Empty);
            Guid instanceGuid = QueryHelper.GetGuid("instanceGuid", Guid.Empty);
            int templateID = QueryHelper.GetInteger("templateID", 0);
            bool newItem = QueryHelper.GetBoolean("isNew", false);
            bool isInline = QueryHelper.GetBoolean("Inline", false);

            PageInfo pi = null;
            try
            {
                // Load page info from alias path and page template
                pi = CMSWebPartPropertiesPage.GetPageInfo(aliasPath, templateID);
            }
            catch (PageNotFoundException)
            {
                // Do not throw exception if page info not found (e.g. bad alias path)
            }

            if (pi != null)
            {
                PageTemplateInstance templateInstance = CMSPortalManager.GetTemplateInstanceForEditing(pi);

                if (templateInstance != null)
                {
                    // Get the instance of widget
                    WebPartInstance widgetInstance = templateInstance.GetWebPart(instanceGuid, widgetID);

                    // Info for zone type
                    WebPartZoneInstance zone = templateInstance.GetZone(zoneId);

                    if (zone != null)
                    {
                        zoneType = zone.WidgetZoneType;
                    }

                    if (widgetInstance != null)
                    {
                        // Create widget from webpart instance
                        wi = WidgetInfoProvider.GetWidgetInfo(widgetInstance.WebPartType);
                    }
                }
            }

            // If inline widget display columns as in editor zone
            if (isInline)
            {
                zoneType = WidgetZoneTypeEnum.Editor;
            }


            // If no zone set (only global admins allowed to continue)
            if (zoneType == WidgetZoneTypeEnum.None)
            {
                if (!CMSContext.CurrentUser.UserSiteManagerAdmin)
                {
                    RedirectToAccessDenied(GetString("attach.actiondenied"));
                }
            }

            // If wi is still null (new item f.e.)
            if (wi == null)
            {
                // Try to get widget info directly by ID
                if (!newItem)
                {
                    wi = WidgetInfoProvider.GetWidgetInfo(widgetID);
                }
                else
                {
                    wi = WidgetInfoProvider.GetWidgetInfo(ValidationHelper.GetInteger(widgetID, 0));
                }
            }
        }

        String itemDescription = String.Empty;
        String itemType = String.Empty;
        String itemDisplayName = String.Empty;
        String itemDocumentation = String.Empty;
        int itemID = 0;

        // Check whether webpart was found
        if (wpi != null)
        {
            itemDescription = wpi.WebPartDescription;
            itemType = PortalObjectType.WEBPART;
            itemID = wpi.WebPartID;
            itemDisplayName = wpi.WebPartDisplayName;
            itemDocumentation = wpi.WebPartDocumentation;
        }
        // Or widget was found
        else if (wi != null)
        {
            itemDescription = wi.WidgetDescription;
            itemType = PortalObjectType.WIDGET;
            itemID = wi.WidgetID;
            itemDisplayName = wi.WidgetDisplayName;
            itemDocumentation = wi.WidgetDocumentation;
        }

        if ((wpi != null) || (wi != null))
        {
            // Get WebPart (widget) image
            DataSet ds = MetaFileInfoProvider.GetMetaFiles(itemID, itemType);

            // Set image url of exists
            if (!DataHelper.DataSourceIsEmpty(ds))
            {
                MetaFileInfo mtfi = new MetaFileInfo(ds.Tables[0].Rows[0]);
                if (mtfi != null)
                {
                    if (mtfi.MetaFileImageWidth > 385)
                    {
                        imgTeaser.Width = 385;
                    }

                    imgTeaser.ImageUrl = ResolveUrl("~/CMSPages/GetMetaFile.aspx?fileguid=" + mtfi.MetaFileGUID.ToString());
                }
            }
            else
            {
                // Set default image 
                imgTeaser.ImageUrl = GetImageUrl("CMSModules/CMS_PortalEngine/WebpartProperties/imagenotavailable.png");
            }

            // Additional image information
            imgTeaser.ToolTip = HTMLHelper.HTMLEncode(itemDisplayName);
            imgTeaser.AlternateText = HTMLHelper.HTMLEncode(itemDisplayName);

            // Set description of webpart
            ltlDescription.Text = HTMLHelper.HTMLEncode(itemDescription);

            // Get description from parent weboart if webpart is inherited
            if ((wpi != null) && ((wpi.WebPartDescription == null || wpi.WebPartDescription == "") && (wpi.WebPartParentID > 0)))
            {
                WebPartInfo pwpi = WebPartInfoProvider.GetWebPartInfo(wpi.WebPartParentID);
                if (pwpi != null)
                {
                    ltlDescription.Text = HTMLHelper.HTMLEncode(pwpi.WebPartDescription);
                }
            }

            FormInfo fi = null;

            // Generate properties
            if (wpi != null)
            {
                // Get form info from parent if webpart is inherited
                if (wpi.WebPartParentID != 0)
                {
                    WebPartInfo pwpi = WebPartInfoProvider.GetWebPartInfo(wpi.WebPartParentID);
                    if (pwpi != null)
                    {
                        fi = GetWebPartProperties(pwpi);
                    }
                }
                else
                {
                    fi = GetWebPartProperties(wpi);
                }
            }
            else if (wi != null)
            {
                fi = GetWidgetProperties(wi);
            }

            // Generate properties
            if (fi != null)
            {
                GenerateProperties(fi);
            }

            // Generate documentation text
            if (itemDocumentation == null || itemDocumentation.Trim() == "")
            {
                if ((wpi != null) && (wpi.WebPartParentID != 0))
                {
                    WebPartInfo pwpi = WebPartInfoProvider.GetWebPartInfo(wpi.WebPartParentID);
                    if (pwpi != null && pwpi.WebPartDocumentation.Trim() != "")
                    {
                        ltlContent.Text = HTMLHelper.ResolveUrls(pwpi.WebPartDocumentation, null);
                    }
                    else
                    {
                        ltlContent.Text = "<br /><div style=\"padding-left:5px; font-weight: bold;\">" + GetString("WebPartDocumentation.DocumentationText") + "</div><br />";
                    }
                }
                else
                {
                    ltlContent.Text = "<br /><div style=\"padding-left:5px; font-weight: bold;\">" + GetString("WebPartDocumentation.DocumentationText") + "</div><br />";
                }
            }
            else
            {
                ltlContent.Text = HTMLHelper.ResolveUrls(itemDocumentation, null);
            }
        }
    }


    protected override void OnLoad(EventArgs e)
    {
        ScriptHelper.RegisterJQuery(this.Page);

        string script = @"
            $j(document.body).ready(initializeResize);
           
            function initializeResize ()  { 
                resizeareainternal();
                $j(window).resize(function() { resizeareainternal(); });
            }

            function resizeareainternal () {
                var height = document.body.clientHeight ; 
                var panel = document.getElementById ('" + divScrolable.ClientID + @"');
                var footer = document.getElementById ('" + FooterClientID + @"');;      
                var constHeight = 61;
                panel.style.height= (height-footer.offsetHeight-panel.offsetTop-constHeight) +'px';  
                
            }";

        ScriptHelper.RegisterClientScriptBlock(Page, typeof(Page), "mainScript", ScriptHelper.GetScript(script));

        string[,] tabs = new string[4, 4];
        tabs[0, 0] = GetString("webparts.documentation");
        tabs[1, 0] = GetString("general.properties");

        tabControlElem.Tabs = tabs;
        tabControlElem.UsePostback = true;

        // Disable caching
        Response.Cache.SetNoStore();

        base.OnLoad(e);
    }


    /// <summary>
    /// Returns form info with widget properties.
    /// </summary>
    /// <param name="wi">Widget</param>
    protected FormInfo GetWidgetProperties(WidgetInfo wi)
    {
        WebPartInfo wpi = WebPartInfoProvider.GetWebPartInfo(wi.WidgetWebPartID);
        if (wpi != null)
        {
            string widgetProperties = FormHelper.MergeFormDefinitions(wpi.WebPartProperties, wi.WidgetProperties);

            FormInfo zoneTypeDefinition = PortalHelper.GetPositionFormInfo(zoneType);
            FormInfo fi = FormHelper.GetWidgetFormInfo(wi.WidgetName, Enum.GetName(typeof(WidgetZoneTypeEnum), zoneType), widgetProperties, zoneTypeDefinition, false);
            return fi;
        }
        return null;
    }


    /// <summary>
    /// Returns form info with webpart properties.
    /// </summary>
    /// <param name="wpi">Web part info</param>
    protected FormInfo GetWebPartProperties(WebPartInfo wpi)
    {
        if (wpi != null)
        {
            // Before form                
            string before = PortalHelper.GetWebPartProperties((WebPartTypeEnum)wpi.WebPartType, PropertiesPosition.Before);
            FormInfo bfi = new FormInfo(before);
            // After form
            string after = PortalHelper.GetWebPartProperties((WebPartTypeEnum)wpi.WebPartType, PropertiesPosition.After);
            FormInfo afi = new FormInfo(after);

            // Add general category to first items in webpart without category
            string properties = wpi.WebPartProperties;
            if (!string.IsNullOrEmpty(properties) && (!properties.StartsWith("<form><category", StringComparison.InvariantCultureIgnoreCase)))
            {
                properties = properties.Insert(6, "<category name=\"" + GetString("general.general") + "\" />");
            }

            return FormHelper.GetWebPartFormInfo(wpi.WebPartName, properties, bfi, afi, true);
        }

        return null;
    }


    /// <summary>
    /// Generate properties.
    /// </summary>
    protected void GenerateProperties(FormInfo fi)
    {
        // Generate script to display and hide  properties groups
        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "ShowHideProperties", ScriptHelper.GetScript(@"
            function ShowHideProperties(id) {
                var obj = document.getElementById(id);
                var objlink = document.getElementById(id+'_link');
                if ((obj != null)&&(objlink != null)){
                   if (obj.style.display == 'none' ) { obj.style.display=''; objlink.innerHTML = '<img border=""0"" src=""" + minImg + @""" >'; } 
                   else {obj.style.display='none'; objlink.innerHTML = '<img border=""0"" src=""" + plImg + @""" >';}
                objlink.blur();}}"));

        // Get defintion elements
        ArrayList infos = fi.GetFormElements(true, false);

        bool isOpenSubTable = false;
        bool firstLoad = true;
        bool hasAnyProperties = false;

        string currentGuid = "";
        string newCategory = null;

        // Check all items in object array
        foreach (object contrl in infos)
        {
            // Generate row for form category
            if (contrl is FormCategoryInfo)
            {
                // Load castegory info
                FormCategoryInfo fci = contrl as FormCategoryInfo;
                if (fci != null)
                {
                    if (isOpenSubTable)
                    {
                        ltlProperties.Text += "<tr class=\"PropertyBottom\"><td class=\"PropertyLeftBottom\">&nbsp;</td><td colspan=\"2\" class=\"Center\">&nbsp;</td><td class=\"PropertyRightBottom\">&nbsp;</td></tr></table>";
                        isOpenSubTable = false;
                    }

                    if (currentGuid == "")
                    {
                        currentGuid = Guid.NewGuid().ToString().Replace("-", "_");
                    }

                    // Generate properties content
                    newCategory = @"<br />
                        <table cellpadding=""0"" cellspacing=""0"" class=""CategoryTable"">
                          <tr>
                            <td class=""CategoryLeftBorder"">&nbsp;</td>
                            <td class=""CategoryTextCell"">" + HTMLHelper.HTMLEncode(fci.CategoryCaption) + @"</td>
                            <td class=""HiderCell"">
                              <a id=""" + currentGuid + "_link\" href=\"#\" onclick=\"ShowHideProperties('" + currentGuid + @"'); return false;"">
                              <img border=""0"" src=""" + minImg + @""" ></a>
                            </td>
                           <td class=""CategoryRightBorder"">&nbsp;</td>
                         </tr>
                       </table>";
                }
            }
            else
            {
                hasAnyProperties = true;
                // Get form field info
                FormFieldInfo ffi = contrl as FormFieldInfo;

                if (ffi != null)
                {
                    // If display only in dashboard and not dashbord zone (or none for admins) dont show
                    if ((ffi.DisplayIn == "dashboard") && ((zoneType != WidgetZoneTypeEnum.Dashboard) && (zoneType != WidgetZoneTypeEnum.None)))
                    {
                        continue;
                    }
                    if (newCategory != null)
                    {
                        ltlProperties.Text += newCategory;
                        newCategory = null;

                        firstLoad = false;
                    }
                    else if (firstLoad)
                    {
                        if (currentGuid == "")
                        {
                            currentGuid = Guid.NewGuid().ToString().Replace("-", "_");
                        }

                        // Generate properties content
                        firstLoad = false;
                        ltlProperties.Text += @"<br />
                        <table cellpadding=""0"" cellspacing=""0"" class=""CategoryTable"">
                          <tr>
                            <td class=""CategoryLeftBorder"">&nbsp;</td>
                            <td class=""CategoryTextCell"">Default</td>
                            <td class=""HiderCell"">
                               <a id=""" + currentGuid + "_link\" href=\"#\" onclick=\"ShowHideProperties('" + currentGuid + @"'); return false;"">
                               <img border=""0"" src=""" + minImg + @""" ></a>
                            </td>
                            <td class=""CategoryRightBorder"">&nbsp;</td>
                         </tr>
                       </table>";
                    }

                    if (!isOpenSubTable)
                    {
                        isOpenSubTable = true;
                        ltlProperties.Text += "" +
                            "<table cellpadding=\"0\" cellspacing=\"0\" id=\"" + currentGuid + "\" class=\"PropertiesTable\" >";
                        currentGuid = "";
                    }

                    string doubleDot = "";
                    if (!ffi.Caption.EndsWith(":"))
                    {
                        doubleDot = ":";
                    }

                    ltlProperties.Text +=
                        @"<tr>
                            <td class=""PropertyLeftBorder"" >&nbsp;</td>
                            <td class=""PropertyContent"" style=""width:200px;"">" + HTMLHelper.HTMLEncode(ResHelper.LocalizeString(ffi.Caption)) + doubleDot + @"</td>
                            <td class=""PropertyRow"">" + HTMLHelper.HTMLEncode(ResHelper.LocalizeString(DataHelper.GetNotEmpty(ffi.Description, GetString("WebPartDocumentation.DescriptionNoneAvailable")))) + @"</td>
                            <td class=""PropertyRightBorder"">&nbsp;</td>
                        </tr>";
                }
            }
        }

        if (isOpenSubTable)
        {
            ltlProperties.Text += "<tr class=\"PropertyBottom\"><td class=\"PropertyLeftBottom\">&nbsp;</td><td colspan=\"2\" class=\"Center\">&nbsp;</td><td class=\"PropertyRightBottom\">&nbsp;</td></tr></table>";
        }

        if (!hasAnyProperties)
        {
            ltlProperties.Text = "<br /><div style=\"padding-left:5px;padding-right:5px; font-weight: bold;\">" + GetString("documentation.nopropertiesavaible") + "</div>";
        }
    }


    /// <summary>
    /// Tab clicked event.
    /// </summary>
    /// <param name="sender">Sender object</param>
    /// <param name="ea">Event arguments</param>
    protected void tabControlElem_clicked(object sender, EventArgs ea)
    {
        if (tabControlElem.SelectedTab == 0)
        {
            pnlDoc.Visible = true;
            pnlProperties.Visible = false;
        }
        else
        {
            pnlDoc.Visible = false;
            pnlProperties.Visible = true;
        }
    }

    #endregion
}
