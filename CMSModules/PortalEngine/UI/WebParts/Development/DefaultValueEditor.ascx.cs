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
using System.Xml;

using CMS.SiteProvider;
using CMS.GlobalHelper;
using CMS.FormEngine;
using CMS.ExtendedControls;
using CMS.DataEngine;
using CMS.PortalEngine;
using CMS.UIControls;

public partial class CMSModules_PortalEngine_UI_WebParts_Development_DefaultValueEditor : CMSUserControl
{
    private string mDefaultValuesXMLDefinition;
    private int mParentWebPartID;
    private string mSourceXMLDefinition = String.Empty;

    /// <summary>
    /// Default values XML definition.
    /// </summary>
    public string DefaultValueXMLDefinition
    {
        get
        {
            return mDefaultValuesXMLDefinition;
        }
        set
        {
            mDefaultValuesXMLDefinition = value;
        }
    }


    /// <summary>
    /// Direct form XML definition if no parent web part ID is set.
    /// </summary>
    public string SourceXMLDefinition
    {
        get
        {
            return mSourceXMLDefinition;
        }
        set
        {
            mSourceXMLDefinition = value;
        }
    }


    /// <summary>
    /// Parent web part id.
    /// </summary>
    public int ParentWebPartID
    {
        get
        {
            return mParentWebPartID;
        }
        set
        {
            mParentWebPartID = value;
        }
    }


    /// <summary>
    /// Controls.
    /// </summary>
    public new Hashtable Controls
    {
        get
        {
            if (ViewState["Controls"] == null)
            {
                ViewState["Controls"] = new Hashtable();
            }

            return (Hashtable)ViewState["Controls"];
        }
        set
        {
            ViewState["Controls"] = value;
        }
    }


    /// <summary>
    /// Default values xml defintion.
    /// </summary>
    XmlDocument xml = null;


    /// <summary>
    /// Page load.
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        // Resource strings
        btnOk.Text = GetString("General.Ok");

        // Generate table with value fields
        GenerateEditor();

        // javascript to enable or disable inherit + load default values
        string javascript = ScriptHelper.GetScript("function CheckClick(obj, itemId, defaultValue, Itype){" +

            " var mItem = document.getElementById(itemId);" +
            " if (mItem != null){" +
            " if (Itype == 'textbox'){ " +
            " if (obj.checked) { " +
            " mItem.disabled = true; mItem.value = defaultValue; " +
            " }else{" +
            " mItem.disabled = false;" +
            " }" +
            " }" +

            // Boolean type

            " if (Itype == 'calendar'){ " +
            " var txtObj = document.getElementById(itemId + '_txtDateTime');" +
            " var imgObj = document.getElementById(itemId + '_imgCalendar'); " +
            " var btnObj = document.getElementById(itemId + '_btnNow'); " +
            " if (obj.checked) { " +
            " mItem.disabled = true; txtObj.disabled = true; btnObj.disabled=true; imgObj.disabled = true; txtObj.value = defaultValue; imgObj.src='" + GetImageUrl("Design/Controls/Calendar/calendardisabled.png") + "'; " +
            " }else{" +
            " mItem.disabled = false; txtObj.disabled = false; btnObj.disabled=false; imgObj.disabled = false; imgObj.src='" + GetImageUrl("Design/Controls/Calendar/calendar.png") + "'; " +
            " }" +
            " }" +

            // Checkbox type

            " if (Itype == 'checkbox'){ " +
            " var upSpan = document.getElementById(itemId + '_upperSpan');" +
            "  if (obj.checked) { " +
            " mItem.disabled = true; upSpan.disabled = true; mItem.checked = defaultValue; " +
            " }else{ " +
            " mItem.disabled = false; upSpan.disabled = false;" +
            " }" +
            " }" +

            "}}");

        // Register client script to the page
        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "DefaultValueInheritCheckbox", javascript);
    }


    /// <summary>
    /// Generate editor table.
    /// </summary>
    public void GenerateEditor()
    {
        // Get parent web part info
        WebPartInfo wpi = WebPartInfoProvider.GetWebPartInfo(this.ParentWebPartID);
        FormInfo fi = null;
        if (wpi != null)
        {
            // Create form info and load xml definition
            fi = FormHelper.GetWebPartFormInfo(wpi.WebPartName + FormHelper.CORE, wpi.WebPartProperties, null, null, false);
        }
        else
        {
            fi = new FormInfo(SourceXMLDefinition);
        }

        if (fi != null)
        {
            // Get defintion elements
            ArrayList infos = fi.GetFormElements(true, false);

            // create table part
            Literal table1 = new Literal();
            pnlEditor.Controls.Add(table1);
            table1.Text = "<table cellpadding=\"3\" >";

            // Hashtable counter
            int i = 0;

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
                        // Create row html code
                        Literal tabCat = new Literal();
                        pnlEditor.Controls.Add(tabCat);
                        tabCat.Text = "<tr class=\"InheritCategory\"><td>";

                        // Create label control and insert it to the page
                        Label lblCat = new Label();
                        this.pnlEditor.Controls.Add(lblCat);
                        lblCat.Text = fci.CategoryName;
                        lblCat.Font.Bold = true;

                        // End row html code
                        Literal tabCat2 = new Literal();
                        pnlEditor.Controls.Add(tabCat2);
                        tabCat2.Text = "</td><td></td><td></td>";
                    }
                }
                else
                {
                    // Get form field info
                    FormFieldInfo ffi = contrl as FormFieldInfo;

                    if (ffi != null)
                    {
                        // Check if is defined inherited default value
                        bool doNotInherit = IsDefined(ffi.Name);
                        // Get default value
                        string inheritedDefaultValue = GetDefaultValue(ffi.Name);

                        // Current hastable for client id
                        Hashtable currentHashTable = new Hashtable();

                        // First item is name
                        currentHashTable[0] = ffi.Name;

                        // Begin new row and column
                        Literal table2 = new Literal();
                        pnlEditor.Controls.Add(table2);
                        table2.Text = "<tr class=\"InheritWebPart\"><td>";

                        // Property label
                        Label lblName = new Label();
                        pnlEditor.Controls.Add(lblName);
                        lblName.Text = ffi.Caption;
                        if (!lblName.Text.EndsWith(":"))
                        {
                            lblName.Text += ":";
                        }

                        // New column
                        Literal table3 = new Literal();
                        pnlEditor.Controls.Add(table3);
                        table3.Text = "</td><td>";

                        // Type string for javascript function
                        string jsType = "textbox";

                        // Type switcher
                        if (FormHelper.IsFieldOfType(ffi, FormFieldControlTypeEnum.CheckBoxControl))
                        {
                            // Checkbox type field
                            CheckBox chk = new CheckBox();
                            pnlEditor.Controls.Add(chk);
                            chk.Checked = ValidationHelper.GetBoolean(ffi.DefaultValue, false);
                            chk.InputAttributes.Add("disabled", "disabled");

                            chk.Attributes.Add("id", chk.ClientID + "_upperSpan");

                            if (doNotInherit)
                            {
                                chk.InputAttributes.Remove("disabled");
                                chk.Enabled = true;
                                chk.Checked = ValidationHelper.GetBoolean(inheritedDefaultValue, false);
                            }

                            jsType = "checkbox";
                            currentHashTable[1] = chk.ClientID;
                        }
                        else if (FormHelper.IsFieldOfType(ffi, FormFieldControlTypeEnum.CalendarControl))
                        {
                            // Date time picker
                            DateTimePicker dtPick = new DateTimePicker();
                            pnlEditor.Controls.Add(dtPick);
                            dtPick.SelectedDateTime = ValidationHelper.GetDateTime(ffi.DefaultValue, DataHelper.DATETIME_NOT_SELECTED);
                            dtPick.Enabled = false;
                            dtPick.SupportFolder = ResolveUrl("~/CMSAdminControls/Calendar");

                            if (doNotInherit)
                            {
                                dtPick.Enabled = true;
                                dtPick.SelectedDateTime = ValidationHelper.GetDateTime(inheritedDefaultValue, DataHelper.DATETIME_NOT_SELECTED);
                            }

                            jsType = "calendar";
                            currentHashTable[1] = dtPick.ClientID;
                        }
                        else
                        {
                            // Other types represent by textbox
                            TextBox txt = new TextBox();
                            pnlEditor.Controls.Add(txt);
                            txt.Text = ffi.DefaultValue;
                            txt.CssClass = "TextBoxField";
                            txt.Enabled = ffi.Enabled;
                            txt.Enabled = false;

                            if (ffi.DataType == FormFieldDataTypeEnum.LongText)
                            {
                                txt.TextMode = TextBoxMode.MultiLine;
                                txt.Rows = 3;
                            }

                            if (doNotInherit)
                            {
                                txt.Enabled = true;
                                txt.Text = inheritedDefaultValue;
                            }

                            currentHashTable[1] = txt.ClientID;
                        }

                        // New column
                        Literal table4 = new Literal();
                        pnlEditor.Controls.Add(table4);
                        table4.Text = "</td><td>" + ffi.DataType.ToString() + "</td><td>";


                        // Inherit chk
                        CheckBox chkInher = new CheckBox();
                        pnlEditor.Controls.Add(chkInher);
                        chkInher.Checked = true;

                        // Uncheck checkbox if this property is not inherited
                        if (doNotInherit)
                        {
                            chkInher.Checked = false;
                        }

                        chkInher.Text = GetString("DefaultValueEditor.Inherited");

                        // Set default value for javascript function
                        string defValue = "'" + ffi.DefaultValue + "'";

                        if (jsType == "checkbox")
                        {
                            defValue = ValidationHelper.GetBoolean(ffi.DefaultValue, false).ToString().ToLower();
                        }

                        // Add javascript attribute with js function call
                        chkInher.Attributes.Add("onclick", "CheckClick(this, '" + currentHashTable[1].ToString() + "', " + defValue + ", '" + jsType + "' );");

                        // Inser current checkbox id
                        currentHashTable[2] = chkInher.ClientID;

                        // Add current hastable to the controls hashtable
                        ((Hashtable)Controls)[i] = currentHashTable;

                        // End current row
                        Literal table5 = new Literal();
                        pnlEditor.Controls.Add(table5);
                        table5.Text = "</td></tr>";

                        i++;
                    }
                }
            }

            // End table part
            Literal table6 = new Literal();
            pnlEditor.Controls.Add(table6);
            table6.Text = "</table>";
        }
    }


    /// <summary>
    /// Returns true if property is set in default values sheet.
    /// </summary>
    public bool IsDefined(string name)
    {
        // Check if xml document exist, if is not created, create it
        if ((xml == null) && !String.IsNullOrEmpty(this.DefaultValueXMLDefinition))
        {
            xml = new XmlDocument();
            xml.LoadXml(this.DefaultValueXMLDefinition);
        }

        if ((xml != null) && (xml.DocumentElement != null))
        {
            // Get the field
            XmlNode fieldNode = TableManager.SelectFieldNode(xml.DocumentElement, "name", name);
            if (fieldNode != null)
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Remove values from original  (not) found in alternative.
    /// </summary>
    /// <param name="original">Original XML</param>
    /// <param name="alternative">Alternative XML</param>
    /// <param name="deleteExistingRecord">Whether remove (from original XML) found or not found records (in alternative XML)</param>
    public string FitlerDefaultValuesDefinition(string original, string alternative, bool deleteExistingRecord)
    {
        if (string.IsNullOrEmpty(alternative))
        {
            // Return original form definition
            return original;
        }

        // Parse XML for both form definitons
        XmlDocument xmlOrigDoc = new XmlDocument();
        xmlOrigDoc.LoadXml(original);
        XmlDocument xmlAltDoc = new XmlDocument();
        xmlAltDoc.LoadXml(alternative);

        // Iterate through field nodes in alternative form definition
        if (xmlAltDoc.DocumentElement != null)
        {
            XmlNode altField = null;
            //foreach (XmlNode orgField in xmlOrigDoc.DocumentElement.ChildNodes)
            for (int i = 0; i < xmlOrigDoc.DocumentElement.ChildNodes.Count; i++)
            {
                XmlNode orgField = xmlOrigDoc.DocumentElement.ChildNodes[i];
                altField = null;
                if ((orgField.LocalName.ToLower() == "field") && (orgField.Attributes["name"] != null))
                {
                    // Get field name
                    string elemName = orgField.Attributes["name"].Value;

                    // Get field with the same column name from alternative definition
                    altField = TableManager.SelectFieldNode(xmlAltDoc.DocumentElement, "column", elemName);
                }

                // If deleteExistinRecord is set to False and no record is found in alternative XML -> remove from original
                if ((altField == null) && (!deleteExistingRecord))
                {
                    orgField.ParentNode.RemoveChild(orgField);
                    i--;
                }
                // If deleteExistinRecord is set to True and record found in alternative XML -> remove from original
                else if ((altField != null) && (deleteExistingRecord))
                {
                    orgField.ParentNode.RemoveChild(orgField);
                    i--;
                }

            }
        }
        return xmlOrigDoc.OuterXml;

    }


    /// <summary>
    /// Returns default value according to selected name, if value doesn't exists return "".
    /// </summary>
    public string GetDefaultValue(string name)
    {
        // Check if xml document exist, if is not created, create it
        if ((xml == null) && !String.IsNullOrEmpty(this.DefaultValueXMLDefinition))
        {
            xml = new XmlDocument();
            xml.LoadXml(this.DefaultValueXMLDefinition);
        }

        if ((xml != null) && (xml.DocumentElement != null))
        {
            // Get the field
            XmlNode fieldNode = TableManager.SelectFieldNode(xml.DocumentElement, "name", name);
            if (fieldNode != null)
            {
                return fieldNode.Attributes["value"].Value;
            }
        }

        return "";
    }


    /// <summary>
    /// When post is added.
    /// </summary>
    public event EventHandler XMLCreated;


    /// <summary>
    /// OK click handler.
    /// </summary>
    protected void btnOk_Click(object sender, EventArgs e)
    {
        Hashtable mHash = (Hashtable)Controls;
        Hashtable panelControl = new Hashtable();

        // Load panel controls
        foreach (Control ctrl in pnlEditor.Controls)
        {
            panelControl[ctrl.ClientID] = ctrl;
        }

        XmlDocument xmlDom = new XmlDocument();
        xmlDom.AppendChild(xmlDom.CreateElement("", "defaultvalues", ""));
        XmlElement xmlRoot = xmlDom.DocumentElement;

        // Check all inherited checkboxes and if is some unchecked, get value and set it to the xml
        for (int i = 0; i < mHash.Count; i++)
        {
            // Get current hastable with client id
            Hashtable currentHash = (Hashtable)mHash[i];
            if (panelControl[currentHash[2].ToString()] != null)
            {
                // Get inherit checkbox
                CheckBox chkInh = panelControl[currentHash[2].ToString()] as CheckBox;
                if ((chkInh != null) && (!chkInh.Checked))
                {
                    XmlElement node = xmlDom.CreateElement("field");
                    node.SetAttribute("name", currentHash[0].ToString());

                    // Select what is control type
                    Control currCtrl = (Control)panelControl[currentHash[1].ToString()];

                    if (currCtrl is CheckBox)
                    {
                        node.SetAttribute("value", ((CheckBox)currCtrl).Checked.ToString());
                    }
                    else if (currCtrl is DateTimePicker)
                    {
                        node.SetAttribute("value", ((DateTimePicker)currCtrl).SelectedDateTime.ToString());
                    }
                    else if (currCtrl is TextBox)
                    {
                        node.SetAttribute("value", ((TextBox)currCtrl).Text.ToString());
                    }

                    xmlRoot.AppendChild(node);
                }
            }
        }

        this.DefaultValueXMLDefinition = xmlDom.InnerXml;

        // Call handlers
        if (XMLCreated != null)
        {
            XMLCreated(this, null);
        }
    }
}
