using System;
using System.Data;
using System.Xml;

using CMS.FormEngine;
using CMS.GlobalHelper;
using CMS.PortalEngine;
using CMS.UIControls;
using CMS.URLRewritingEngine;

public partial class CMSModules_PortalEngine_UI_WebParts_Development_WebPart_Edit_Properties : SiteManagerPage
{
    private int webPartId;
    private WebPartInfo wi;

    protected void Page_Load(object sender, EventArgs e)
    {
        this.Title = "WebPart Edit - Properties";
        CurrentMaster.BodyClass += " FieldEditorBody";

        // get webpart ID
        webPartId = QueryHelper.GetInteger("webpartid", 0);
        FieldEditor.Visible = false;

        // If saved is found in query string
        if ((!RequestHelper.IsPostBack()) && (QueryHelper.GetInteger("saved", 0) == 1))
        {
            lblInfo.Visible = true;
            lblInfo.Text = GetString("General.ChangesSaved");
        }

        if (webPartId > 0)
        {
            // Get web part info
            wi = WebPartInfoProvider.GetWebPartInfo(webPartId);

            // Check if info object exists
            if (wi != null)
            {
                // For inherited webpart display DefaultValue Editor
                if (wi.WebPartParentID > 0)
                {
                    FieldEditor.Visible = false;
                    pnlDefaultEditor.Visible = true;
                    DefaultValueEditor1.Visible = true;
                    DefaultValueEditor1.ParentWebPartID = wi.WebPartParentID;
                    if (wi.WebPartParentID > 0)
                    {
                        DefaultValueEditor1.DefaultValueXMLDefinition = wi.WebPartDefaultValues;
                    }
                    else
                    {
                        DefaultValueEditor1.DefaultValueXMLDefinition = wi.WebPartProperties;
                    }
                    // Add handler to 
                    DefaultValueEditor1.XMLCreated += new EventHandler(DefaultValueEditor1_XMLCreated);
                }
                else
                {
                    // set fieldeditor                
                    FieldEditor.WebPartId = webPartId;
                    FieldEditor.Mode = FieldEditorModeEnum.WebPartProperties;
                    FieldEditor.Visible = true;
                    pnlDefaultEditor.Visible = false;

                    // Check newly created field for widgets update
                    FieldEditor.OnFieldCreated += UpdateWidgetsDefinition;
                }
            }
        }
    }


    /// <summary>
    /// Handles OnFieldCreated action and updates form definition of all widgets based on current webpart.
    /// Newly created field is set to be disabled in widget definition for security reasons.
    /// </summary>
    /// <param name="newField">Newly created field</param>
    protected void UpdateWidgetsDefinition(object sender, FormFieldInfo newField)
    {
        if ((wi != null) && (newField != null))
        {
            // Get widgets based on this webpart
            DataSet ds = WidgetInfoProvider.GetWidgets("WidgetWebPartID = " + webPartId, null, 0, "WidgetID");

            // Continue only if there are some widgets
            if (!DataHelper.DataSourceIsEmpty(ds))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    int widgetId = ValidationHelper.GetInteger(dr["WidgetID"], 0);
                    WidgetInfo widget = WidgetInfoProvider.GetWidgetInfo(widgetId);
                    if (widget != null)
                    {
                        // Prepare disabled field definition
                        string disabledField = String.Format("<form><field column=\"{0}\" visible=\"false\" /></form>", newField.Name);

                        // Incorporate disabled field into original definition of widget
                        widget.WidgetProperties = FormHelper.CombineFormDefinitions(widget.WidgetProperties, disabledField);

                        // Update widget
                        WidgetInfoProvider.SetWidgetInfo(widget);
                    }
                }
            }
        }
    }


    /// <summary>
    /// XML created, save it.
    /// </summary>
    protected void DefaultValueEditor1_XMLCreated(object sender, EventArgs e)
    {
        if (wi != null)
        {
            // Load xml definition
            if (wi.WebPartParentID > 0)
            {
                XmlDocument xmlBefore = new XmlDocument();
                XmlDocument xmlAfter = new XmlDocument();

                xmlBefore.Load(Server.MapPath("~/CMSModules/PortalEngine/UI/WebParts/Properties/WebPart_PropertiesBefore.xml"));
                xmlAfter.Load(Server.MapPath("~/CMSModules/PortalEngine/UI/WebParts/Properties/WebPart_PropertiesAfter.xml"));

                string formDef = FormHelper.CombineFormDefinitions(xmlBefore.DocumentElement.OuterXml, xmlAfter.DocumentElement.OuterXml);
                // Web part default values contains either properties or changed "system" values
                // First Remove records with same name as "system properties" => only actual webpart's properties remains
                string filteredDef = this.DefaultValueEditor1.FitlerDefaultValuesDefinition(wi.WebPartDefaultValues, formDef, false);

                WebPartInfo wpi = WebPartInfoProvider.GetWebPartInfo(wi.WebPartParentID);

                // Remove records with same name as parent's property - its already stored in webpart's properties
                if (wpi != null)
                {
                    filteredDef = this.DefaultValueEditor1.FitlerDefaultValuesDefinition(filteredDef, wpi.WebPartProperties, true);
                }
                // If inherited web part merge webpart's properties hier with default system values
                wi.WebPartDefaultValues = FormHelper.CombineFormDefinitions(filteredDef, this.DefaultValueEditor1.DefaultValueXMLDefinition);
            }
            else
            {
                wi.WebPartProperties = this.DefaultValueEditor1.DefaultValueXMLDefinition;
            }

            // Sav web part info
            WebPartInfoProvider.SetWebPartInfo(wi);

            // Redirect with saved assign
            string url = URLHelper.RemoveParameterFromUrl(URLRewriter.CurrentURL, "saved");
            url = URLHelper.AddParameterToUrl(url, "saved", "1");
            URLHelper.Redirect(url);
        }
    }
}

