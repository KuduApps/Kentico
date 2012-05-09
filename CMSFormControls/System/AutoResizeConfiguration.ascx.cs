using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Xml;

using CMS.FormControls;
using CMS.SettingsProvider;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.UIControls;


/// <summary>
/// This form control must be used with name 'autoresize' only. Another 3 blank form controls must be used in class to make it working properly. 
/// Blank form controls must have names: 'autoresize_width', 'autoresize_height', 'autoresize_maxsidesize', 'autoresize_hashtable'.
/// </summary>
public partial class CMSFormControls_System_AutoResizeConfiguration : FormEngineUserControl, ICallbackEventHandler
{
    #region "Variables"

    private XmlDocument xmlValue = new XmlDocument();
    private string strValue = null;
    private int width = 0;
    private int height = 0;
    private int maxSideSize = 0;
    private string fieldValue = "";
    protected string dimensions = "";

    #endregion


    #region "Public properties"

    /// <summary>
    /// Indicates if control is enabled.
    /// </summary>
    public override bool Enabled
    {
        get
        {
            return drpSettings.Enabled;
        }
        set
        {
            this.txtWidth.Enabled = value;
            this.txtHeight.Enabled = value;
            this.txtMax.Enabled = value;
            drpSettings.Enabled = value;
        }
    }


    /// <summary>
    /// DropDownList selected item value. Possible options: custom - for custom size, noresize - do not resize, (nothing) - use site settings.
    /// </summary>
    public override object Value
    {
        get
        {
            // Return data form hashtable
            if (ContainsColumn("autoresize_hashtable") && ValidationHelper.GetBoolean(this.Form.Data["autoresize_hashtable"], false))
            {
                return this.drpSettings.SelectedValue;
            }
            // Return XML data
            else
            {
                return UpdateConfiguration(xmlValue);
            }
        }
        set
        {
            strValue = ValidationHelper.GetString(value, "");

            // Try to load data from XML
            if (ContainsColumn("autoresize_hashtable") && ValidationHelper.GetBoolean(this.Form.Data["autoresize_hashtable"], false))
            {
                fieldValue = strValue;
            }
            // Provided data are not in XML format            
            else
            {
                try
                {
                    xmlValue.LoadXml(strValue);
                    LoadConfiguration(xmlValue);
                }
                catch
                {
                }
            }
        }
    }

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.StopProcessing)
        {
            // Load drop-down list
            if ((!RequestHelper.IsPostBack()) || (drpSettings.Items.Count == 0))
            {
                drpSettings.Items.Add(new ListItem("dialogs.resize.donotresize", "noresize"));
                drpSettings.Items.Add(new ListItem("dialogs.resize.usesitesettings", ""));
                drpSettings.Items.Add(new ListItem("dialogs.resize.usecustomsettings", "custom"));
                drpSettings.SelectedValue = fieldValue;
            }

            // Registred scripts
            ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "AutoResize_EnableDisableForm", GetScriptEnableDisableForm());
            ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "AutoResize_ReceiveDimensions", GetScriptReceiveDimensions());
            ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "AutoResize_LoadSiteSettings", ScriptHelper.GetScript("function GetDimensions(txtWidthID, txtHeightID, txtMaxID){ return " + this.Page.ClientScript.GetCallbackEventReference(this, "txtWidthID + ';' + txtHeightID + ';' + txtMaxID", "ReceiveDimensions", null) + " } \n"));

            // Initialize form
            drpSettings.Attributes.Add("onchange", GetEnableDisableFormDefinition());
        }
        else
        {
            this.Visible = false;
        }
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        // Try to load data from settings
        if (ContainsColumn("autoresize_width") && ContainsColumn("autoresize_height") && ContainsColumn("autoresize_maxsidesize"))
        {
            width = ValidationHelper.GetInteger(this.Form.Data["autoresize_width"], 0);
            height = ValidationHelper.GetInteger(this.Form.Data["autoresize_height"], 0);
            maxSideSize = ValidationHelper.GetInteger(this.Form.Data["autoresize_maxsidesize"], 0);
            LoadConfiguration(drpSettings.SelectedValue, width, height, maxSideSize);
        }

        ScriptHelper.RegisterStartupScript(this, typeof(string), "EnableDisableFields", ScriptHelper.GetScript(GetEnableDisableFormDefinition()));
    }


    /// <summary>
    /// Returns true if user control is valid.
    /// </summary>
    public override bool IsValid()
    {
        bool isValid = true;

        if (drpSettings.SelectedValue == "custom")
        {
            // Check if all required fields are defined
            if (ContainsColumn("autoresize_hashtable") && ValidationHelper.GetBoolean(this.Form.Data["autoresize_hashtable"], false))
            {
                if (!ContainsColumn("autoresize_width"))
                {
                    this.ValidationError += String.Format(GetString("formcontrol.missingcolumn"), "autoresize_width", GetString("templatedesigner.fieldtypes.integer"));
                    isValid = false;
                }
                if (!ContainsColumn("autoresize_height"))
                {
                    this.ValidationError += String.Format(GetString("formcontrol.missingcolumn"), "autoresize_height", GetString("templatedesigner.fieldtypes.integer"));
                    isValid = false;
                }
                if (!ContainsColumn("autoresize_maxsidesize"))
                {
                    this.ValidationError += String.Format(GetString("formcontrol.missingcolumn"), "autoresize_maxsidesize", GetString("templatedesigner.fieldtypes.integer"));
                    isValid = false;
                }
            }

            // Validate width
            if (txtWidth.Text.Trim() != "")
            {
                if (ValidationHelper.GetInteger(txtWidth.Text.Trim(), 0) <= 0)
                {
                    isValid = false;
                }
            }

            // Validate height
            if (isValid && (txtHeight.Text.Trim() != ""))
            {
                if (ValidationHelper.GetInteger(txtHeight.Text.Trim(), 0) <= 0)
                {
                    isValid = false;
                }
            }

            // Validate max side size
            if (isValid && (txtMax.Text.Trim() != ""))
            {
                if (ValidationHelper.GetInteger(txtMax.Text.Trim(), 0) <= 0)
                {
                    isValid = false;
                }
            }

            if (!isValid)
            {
                // Display error message                   
                ValidationError += GetString("dialogs.resize.wrongformat");
            }
        }

        return isValid;
    }


    /// <summary>
    /// Returns other values related to this form control.
    /// </summary>
    /// <returns>Returns an array where first dimension is attribute name and the second dimension is its value.</returns>
    public override object[,] GetOtherValues()
    {
        // Save custom settings
        if (ContainsColumn("autoresize_hashtable") && ValidationHelper.GetBoolean(this.Form.Data["autoresize_hashtable"], false) && (Value.ToString() == "custom"))
        {
            // Set properties names
            object[,] values = new object[3, 2];
            values[0, 0] = "autoresize_width";
            values[0, 1] = ValidationHelper.GetInteger(txtWidth.Text.Trim(), 0);
            values[1, 0] = "autoresize_height";
            values[1, 1] = ValidationHelper.GetInteger(txtHeight.Text.Trim(), 0);
            values[2, 0] = "autoresize_maxsidesize";
            values[2, 1] = ValidationHelper.GetInteger(txtMax.Text.Trim(), 0);

            return values;
        }
        return null;
    }

    #endregion


    #region "Private methods"

    /// <summary>
    /// Prepare javaScript for dimensions.
    /// </summary>
    private string GetScriptReceiveDimensions()
    {
        StringBuilder builder = new StringBuilder();
        builder.Append("function ReceiveDimensions(rValue, context){");
        builder.Append("var dimensions = rValue.split(\";\");");

        builder.Append("var width = (dimensions[0] > 0) ? dimensions[0]: '';");
        builder.Append("var txtWidth = document.getElementById(dimensions[3]);\n");
        builder.Append("if (txtWidth != null)\n");
        builder.Append("{ txtWidth.value = width; }\n");

        builder.Append("var height = (dimensions[1] > 0) ? dimensions[1]: '';");
        builder.Append("var txtHeight = document.getElementById(dimensions[4]);\n");
        builder.Append("if (txtHeight != null)\n");
        builder.Append("{ txtHeight.value = height; }\n");

        builder.Append("var max = (dimensions[2] > 0) ? dimensions[2]: '';");
        builder.Append("var txtMax = document.getElementById(dimensions[5]);\n");
        builder.Append("if (txtMax != null)\n");
        builder.Append("{ txtMax.value = max; }\n");

        builder.Append("txtWidth.disabled=true;\n");
        builder.Append("txtHeight.disabled=true;\n");
        builder.Append("txtMax.disabled=true;\n}");

        return ScriptHelper.GetScript(builder.ToString());
    }


    /// <summary>
    /// Prepare JavaScript for disabling form.
    /// </summary>
    /// <returns></returns>
    private string GetScriptEnableDisableForm()
    {
        StringBuilder builder = new StringBuilder();
        builder.Append("function EnableDisableForm(drpSettingsID, txtWidthID, txtHeightID, txtMaxID){");
        builder.Append("var drpSettings = document.getElementById(drpSettingsID);\n");
        builder.Append("var txtWidth = document.getElementById(txtWidthID);\n");
        builder.Append("var txtHeight = document.getElementById(txtHeightID);\n");
        builder.Append("var txtMax = document.getElementById(txtMaxID);\n");

        builder.Append("if ((drpSettings != null) && (txtWidth != null) && (txtHeight != null) && (txtMax != null)) {\n");
        builder.Append("var disabled = (drpSettings.value != 'custom');\n");
        builder.Append("txtWidth.disabled = disabled;\n");
        builder.Append("txtHeight.disabled = disabled;\n");
        builder.Append("txtMax.disabled = disabled;\n");
        builder.Append("if (drpSettings.value == '') {\n");
        builder.Append("  GetDimensions(txtWidthID, txtHeightID, txtMaxID); }\n");
        builder.Append("else if (drpSettings.value == 'noresize') {\n");
        builder.Append("  txtWidth.value = ''; txtHeight.value = ''; txtMax.value = '';}\n");
        builder.Append("}}\n");

        return ScriptHelper.GetScript(builder.ToString());
    }


    /// <summary>
    /// Prepare control IDs.
    /// </summary>
    private string GetEnableDisableFormDefinition()
    {
        return string.Format("EnableDisableForm('{0}', '{1}', '{2}', '{3}');",
            this.drpSettings.ClientID,
            this.txtWidth.ClientID,
            this.txtHeight.ClientID,
            this.txtMax.ClientID);
    }


    /// <summary>
    /// Sets inner controls according to the parameters and their values included in configuration collection. Parameters collection will be passed from Field editor.
    /// </summary>    
    private void LoadConfiguration(string autoresize, int width, int height, int maxSideSize)
    {
        switch (autoresize.ToLower())
        {
            case "noresize":
                width = 0;
                height = 0;
                maxSideSize = 0;
                break;

            // Use custom settings
            case "custom":
                break;

            // Use site settings
            default:
                string siteName = CMSContext.CurrentSiteName;
                width = ImageHelper.GetAutoResizeToWidth(siteName);
                height = ImageHelper.GetAutoResizeToHeight(siteName);
                maxSideSize = ImageHelper.GetAutoResizeToMaxSideSize(siteName);
                break;
        }

        SetDimension(txtWidth, width);
        SetDimension(txtHeight, height);
        SetDimension(txtMax, maxSideSize);
    }


    /// <summary>
    /// Sets inner controls according to the parameters and their values included in configuration collection.
    /// </summary>
    /// <param name="config">Parameters collection</param>
    private void LoadConfiguration(XmlDocument config)
    {
        int width = ValidationHelper.GetInteger(config["AutoResize"]["autoresize_width"].InnerText, 0);
        int height = ValidationHelper.GetInteger(config["AutoResize"]["autoresize_height"].InnerText, 0);
        int maxSideSize = ValidationHelper.GetInteger(config["AutoResize"]["autoresize_maxsidesize"].InnerText, 0);
        string autoresize = ValidationHelper.GetString(config["AutoResize"]["autoresize"].InnerText, "");

        fieldValue = autoresize;
        LoadConfiguration(autoresize, width, height, maxSideSize);
    }


    /// <summary>
    /// Updates parameters collection of parameters and values according to the values of the inner controls.
    /// </summary>
    /// <param name="config">Parameters collection</param>
    private string UpdateConfiguration(XmlDocument config)
    {
        // Create proper XML structure
        config.LoadXml("<AutoResize><autoresize /><autoresize_width /><autoresize_height /><autoresize_maxsidesize /></AutoResize>");

        XmlNode nodeAutoresize = config.SelectSingleNode("AutoResize/autoresize");
        XmlNode nodeAutoresizeWidth = config.SelectSingleNode("AutoResize/autoresize_width");
        XmlNode nodeAutoresizeHeight = config.SelectSingleNode("AutoResize/autoresize_height");
        XmlNode nodeAutoresizeMax = config.SelectSingleNode("AutoResize/autoresize_maxsidesize");

        // Save custom settings
        if (drpSettings.SelectedValue == "custom")
        {
            nodeAutoresize.InnerText = "custom";

            if (txtWidth.Text.Trim() != "")
            {
                nodeAutoresizeWidth.InnerText = txtWidth.Text.Trim();
            }

            if (txtHeight.Text.Trim() != "")
            {
                nodeAutoresizeHeight.InnerText = txtHeight.Text.Trim();
            }

            if (txtMax.Text.Trim() != "")
            {
                nodeAutoresizeMax.InnerText = txtMax.Text.Trim();
            }
        }
        // Save no resize settings
        else if (drpSettings.SelectedValue == "noresize")
        {
            nodeAutoresize.InnerText = "noresize";
        }

        return config.InnerXml;
    }



    /// <summary>
    /// Sets specified dimension to the specified text box.
    /// </summary>
    /// <param name="txt">Textbox the dimensions should be set to</param>
    /// <param name="dimension">Dimension to be set</param>
    private void SetDimension(TextBox txt, int dimension)
    {
        if (dimension > 0)
        {
            txt.Text = dimension.ToString();
        }
        else
        {
            txt.Text = "";
        }
    }

    #endregion


    #region "Callback handling"

    public string GetCallbackResult()
    {
        return dimensions;
    }


    public void RaiseCallbackEvent(string eventArgument)
    {
        // Get site settings
        string siteName = CMSContext.CurrentSiteName;
        int width = ImageHelper.GetAutoResizeToWidth(siteName);
        int height = ImageHelper.GetAutoResizeToHeight(siteName);
        int max = ImageHelper.GetAutoResizeToMaxSideSize(siteName);

        string[] IDs = eventArgument.Split(';');

        // Returns site settings back to the client
        dimensions = string.Format("{0};{1};{2};{3};{4};{5}", width, height, max, IDs[0], IDs[1], IDs[2]);
    }

    #endregion
}
