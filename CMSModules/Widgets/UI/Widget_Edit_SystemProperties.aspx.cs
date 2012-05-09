using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.FormEngine;
using CMS.PortalEngine;
using CMS.URLRewritingEngine;


public partial class CMSModules_Widgets_UI_Widget_Edit_SystemProperties : SiteManagerPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int widgetID = QueryHelper.GetInteger("widgetID", 0);

        // Default values XML load
        XmlDocument xmlBefore = new XmlDocument();
        XmlDocument xmlAfter = new XmlDocument();
        ucDefaultValueEditor.XMLCreated += new EventHandler(ucDefaultValueEditor_XMLCreated);

        // If saved is found in query string
        if ((!RequestHelper.IsPostBack()) && (QueryHelper.GetInteger("saved", 0) == 1))
        {
            lblInfo.Visible = true;
            lblInfo.Text = GetString("General.ChangesSaved");
        }

        // Load default values XML files
        xmlBefore.Load(Server.MapPath("~/CMSModules/PortalEngine/UI/WebParts/Properties/WebPart_PropertiesBefore.xml"));
        xmlAfter.Load(Server.MapPath("~/CMSModules/PortalEngine/UI/WebParts/Properties/WebPart_PropertiesAfter.xml"));
        string formDef = FormHelper.CombineFormDefinitions(xmlBefore.DocumentElement.OuterXml, xmlAfter.DocumentElement.OuterXml);

        WidgetInfo wi = WidgetInfoProvider.GetWidgetInfo(widgetID);

        if (wi != null)
        {
            // Load default values for current web part
            XmlDocument xmlDefault = LoadDefaultValuesXML(wi, formDef);

            // Set field editor        
            if (wi.WidgetDefaultValues == String.Empty)
            {
                ucDefaultValueEditor.DefaultValueXMLDefinition = "<form></form>";
            }
            else
            {
                // WebPartDefaultValues contains changed fields versus default XML settings (stored in files)
                ucDefaultValueEditor.DefaultValueXMLDefinition = wi.WidgetDefaultValues;
            }

            ucDefaultValueEditor.SourceXMLDefinition = xmlDefault.DocumentElement.OuterXml;
        }
    }


    /// <summary>
    /// Load XML with default values (remove keys already overriden in properties tab).
    /// </summary>
    /// <param name="wi">Web part info</param>
    /// <param name="formDef">String XML definition of default values of webpart</param>
    private XmlDocument LoadDefaultValuesXML(WidgetInfo wi, string formDef)
    {
        // Test if there is any default properties set
        string properties = "<form></form>";
        if (wi.WidgetProperties != String.Empty)
        {
            WebPartInfo wpi = WebPartInfoProvider.GetWebPartInfo(wi.WidgetWebPartID);
            if (wpi != null)
            {
                properties = FormHelper.MergeFormDefinitions(wpi.WebPartProperties, wi.WidgetProperties);
            }
        }
        XmlDocument xmlProperties = new XmlDocument();
        xmlProperties.LoadXml(properties);

        // Load default system xml 
        XmlDocument xmlDefault = new XmlDocument();
        xmlDefault.LoadXml(formDef);

        // Filter overriden properties - remove properties with same name as in system XML
        XmlNodeList defaultList = xmlDefault.SelectNodes(@"//field");
        foreach (XmlNode node in defaultList)
        {
            string columnName = node.Attributes["column"].Value.ToString();

            XmlNodeList propertiesList = xmlProperties.SelectNodes("//field[@column=\"" + columnName + "\"]");
            //This property already set in properties tab
            if (propertiesList.Count > 0)
            {
                node.ParentNode.RemoveChild(node);
            }
        }

        // Filter empty categories            
        XmlNodeList nodes = xmlDefault.DocumentElement.ChildNodes;
        for (int i = 0; i < nodes.Count; i++)
        {
            XmlNode node = nodes[i];
            if (node.Name.ToLower() == "category")
            {
                // Find next category
                if (i < nodes.Count - 1)
                {
                    XmlNode nextNode = nodes[i + 1];
                    if (nextNode.Name.ToLower() == "category")
                    {
                        // Delete actual category
                        node.ParentNode.RemoveChild(node);
                        i--;
                    }
                }
            }
        }

        // Test if last category is not empty           
        nodes = xmlDefault.DocumentElement.ChildNodes;
        if (nodes.Count > 0)
        {
            XmlNode lastNode = nodes[nodes.Count - 1];
            if (lastNode.Name.ToLower() == "category")
            {
                lastNode.ParentNode.RemoveChild(lastNode);
            }
        }
        return xmlDefault;
    }


    /// <summary>
    /// Event loaded after ok button clicked.
    /// </summary>
    /// <param name="sender">Sender</param>
    /// <param name="e">Event args</param>
    void ucDefaultValueEditor_XMLCreated(object sender, EventArgs e)
    {
        int widgetID = QueryHelper.GetInteger("widgetID", 0);
        WidgetInfo wi = WidgetInfoProvider.GetWidgetInfo(widgetID);

        if (wi != null)
        {
            wi.WidgetDefaultValues = ucDefaultValueEditor.DefaultValueXMLDefinition;
            WidgetInfoProvider.SetWidgetInfo(wi);
        }

        // Redirect to apply settings 
        string url = URLHelper.RemoveParameterFromUrl(URLRewriter.CurrentURL, "saved");
        url = URLHelper.AddParameterToUrl(url, "saved", "1");
        URLHelper.Redirect(url);
    }
}
