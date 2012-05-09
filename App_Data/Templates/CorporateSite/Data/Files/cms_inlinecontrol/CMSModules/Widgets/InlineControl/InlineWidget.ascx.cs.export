using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Data;
using System.Collections;

using CMS.ExtendedControls;
using CMS.GlobalHelper;
using CMS.PortalEngine;
using CMS.FormEngine;
using CMS.PortalControls;

public partial class CMSModules_Widgets_InlineControl_InlineWidget : InlineUserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Hashtable decodedProperties = new Hashtable();
        foreach (DictionaryEntry param in mProperties)
        {
            // Decode special CK editor char
            String str = String.Empty;
            if (param.Value != null)
            {
                str = param.Value.ToString().Replace("%25", "%");
            }

            decodedProperties[param.Key] = HttpUtility.UrlDecode(str);
        }
        mProperties = decodedProperties;

        string widgetName = ValidationHelper.GetString(mProperties["name"], String.Empty);

        // Widget name must be specified
        if (String.IsNullOrEmpty(widgetName))
        {
            AddErrorWebPart("widgets.invalidname", null);
            return;
        }

        WidgetInfo wi = WidgetInfoProvider.GetWidgetInfo(widgetName);
        if (wi == null)
        {
            AddErrorWebPart("widget.failedtoload", null);
            return;
        }

        WebPartInfo wpi = WebPartInfoProvider.GetWebPartInfo(wi.WidgetWebPartID);
        if (wpi == null)
        {
            return;
        }

        //no widgets can be used as inline
        if (!wi.WidgetForInline)
        {
            AddErrorWebPart("widgets.cantbeusedasinline", null);
            return;
        }

        try
        {
            // Merge widget and it's parent webpart properties
            string properties = FormHelper.MergeFormDefinitions(wpi.WebPartProperties, wi.WidgetProperties);

            // Prepare form
            WidgetZoneTypeEnum zoneType = WidgetZoneTypeEnum.Editor;
            FormInfo zoneTypeDefinition = PortalHelper.GetPositionFormInfo(zoneType);
            FormInfo fi = FormHelper.GetWidgetFormInfo(wi.WidgetName, Enum.GetName(typeof(WidgetZoneTypeEnum), zoneType), properties, zoneTypeDefinition, true);

            // Apply changed values
            DataRow dr = fi.GetDataRow();
            fi.LoadDefaultValues(dr);

            // Incorporate inline parameters to datarow
            string publicFields = ";containertitle;container;";
            if (wi.WidgetPublicFileds != null)
            {
                publicFields += ";" + wi.WidgetPublicFileds.ToLower() + ";";
            }

            // Load the webpart(widget) control
            string url = WebPartInfoProvider.GetWebPartUrl(wpi, false);

            CMSAbstractWebPart control = (CMSAbstractWebPart)Page.LoadControl(url);
            control.PartInstance = new WebPartInstance();

            // Set all form values to webpart          
            foreach (DataColumn column in dr.Table.Columns)
            {
                object value = dr[column];
                string columnName = column.ColumnName.ToLower();

                //Resolve set values by user
                if (mProperties.Contains(columnName))
                {
                    FormFieldInfo ffi = fi.GetFormField(columnName);
                    if ((ffi != null) && ffi.Visible && (!ffi.DisplayIn.Contains(FormInfo.DISPLAY_CONTEXT_DASHBOARD)))
                    {
                        value = mProperties[columnName];
                    }
                }

                // Resolve macros in defined in default values
                if (!String.IsNullOrEmpty(value as string))
                {
                    // Do not resolve macros for public fields
                    if (publicFields.IndexOf(";" + columnName + ";") < 0)
                    {
                        // Check whether current column 
                        bool avoidInjection = control.SQLProperties.Contains(";" + columnName + ";");

                        // Resolve macros
                        value = control.ContextResolver.ResolveMacros(value.ToString(), avoidInjection);
                    }
                }

                control.PartInstance.SetValue(column.ColumnName, value);
            }

            // Load webpart content
            control.OnContentLoaded();

            // Add webpart to controls collection
            this.Controls.Add(control);
        }

        catch (Exception ex)
        {
            AddErrorWebPart("widget.failedtoload", ex);
        }
    }


    /// <summary>
    /// Add error web part to collection.
    /// </summary>
    /// <param name="tittle">Tittle of webpart</param>
    public void AddErrorWebPart(string tittle, Exception ex)
    {
        WebPartError err = new WebPartError();
        //If ex is defined, let ex.message show the error type    
        err.ErrorTitle = GetString(tittle);
        if (ex != null)
        {
            err.InnerException = ex;
        }
        this.Controls.Add(err);

        // Load webpart content
        err.OnContentLoaded();
    }
}
