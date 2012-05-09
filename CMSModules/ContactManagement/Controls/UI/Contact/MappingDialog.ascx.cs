using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.DataEngine;
using CMS.ExtendedControls;
using CMS.FormControls;
using CMS.FormEngine;
using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.UIControls;

public partial class CMSModules_ContactManagement_Controls_UI_Contact_MappingDialog : CMSUserControl
{
    private Hashtable customControls = null;
    private string mClassName = null;


    /// <summary>
    /// Name of a class that should be mapped to the contact.
    /// </summary>
    private string ClassName
    {
        get
        {
            if (string.IsNullOrEmpty(mClassName))
            {
                mClassName = ValidationHelper.GetString(GetValue("classname"), string.Empty);
            }
            return mClassName;
        }
    }


    /// <summary>
    /// Returns the value of the given property.
    /// </summary>
    /// <param name="propertyName">Property name</param>
    public override object GetValue(string propertyName)
    {
        switch (propertyName.ToLower())
        {
            case "mappingdefinition":
                return GetMappingDefinition();

            case "allowoverwrite":
                return chkOverwrite.Checked;

            default:
                return base.GetValue(propertyName);
        }
    }


    /// <summary>
    /// Sets the property value of the control, setting the value affects only local property value.
    /// </summary>
    /// <param name="propertyName">Property name</param>
    /// <param name="value">Value</param>
    public override void SetValue(string propertyName, object value)
    {
        switch (propertyName.ToLower())
        {
            // Set the ability to overwrite contact info
            case "allowoverwrite":
                chkOverwrite.Checked = ValidationHelper.GetBoolean(value, false);
                break;

            default:
                base.SetValue(propertyName, value);
                break;
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        // Init grouping texts
        pnlGeneral.GroupingText = GetString("general.general");
        pnlPersonal.GroupingText = GetString("om.contact.personal");
        pnlAddress.GroupingText = GetString("general.address");
        pnlCustom.GroupingText = GetString("general.customfields");

        // Check if 'om.contact' class has any custom fields
        FormInfo contactFormInfo = FormHelper.GetFormInfo("om.contact", false);
        if (contactFormInfo != null)
        {
            ArrayList fields = contactFormInfo.GetFormElements(true, false, true);
            if (fields.Count > 0)
            {
                pnlCustom.Visible = true;
                // Add contact's custom fields to the dialog
                AddCustomFields(fields);
            }
        }

        if (!RequestHelper.IsPostBack())
        {
            // Initialize controls
            InitializeControls();
        }
    }


    /// <summary>
    /// Initializes controls on the page.
    /// </summary>
    protected void InitializeControls()
    {
        DataClassInfo classInfo = DataClassInfoProvider.GetDataClass(ClassName);
        if (classInfo == null)
        {
            return;
        }

        // Set class names
        fldAddress1.ClassName = ClassName;
        fldAddress2.ClassName = ClassName;
        fldBirthday.ClassName = ClassName;
        fldBusinessPhone.ClassName = ClassName;
        fldCity.ClassName = ClassName;
        fldCountry.ClassName = ClassName;
        fldEmail.ClassName = ClassName;
        fldFirstName.ClassName = ClassName;
        fldGender.ClassName = ClassName;
        fldHomePhone.ClassName = ClassName;
        fldJobTitle.ClassName = ClassName;
        fldLastName.ClassName = ClassName;
        fldMiddleName.ClassName = ClassName;
        fldMobilePhone.ClassName = ClassName;
        fldSalutation.ClassName = ClassName;
        fldState.ClassName = ClassName;
        fldTitleAfter.ClassName = ClassName;
        fldTitleBefore.ClassName = ClassName;
        fldURL.ClassName = ClassName;
        fldZip.ClassName = ClassName;

        if (!string.IsNullOrEmpty(classInfo.ClassContactMapping))
        {
            // Prepare form info based on mapping data
            FormInfo mapInfo = new FormInfo(classInfo.ClassContactMapping);
            if (mapInfo.ItemsList.Count > 0)
            {
                FormEngineUserControl customControl = null;
                // Get all mapped fields
                FormFieldInfo[] fields = mapInfo.GetFields(true, true);

                // Name property contains a column of contact object
                // and MappedToField property contains form field mapped to the contact column
                foreach (FormFieldInfo ffi in fields)
                {
                    // Set mapping values...
                    switch (ffi.Name.ToLower())
                    {
                        case "contactaddress1":
                            // ... Address1
                            fldAddress1.Value = ffi.MappedToField;
                            break;
                        case "contactaddress2":
                            // ... Address2
                            fldAddress2.Value = ffi.MappedToField;
                            break;
                        case "contactbirthday":
                            // ... birthday
                            fldBirthday.Value = ffi.MappedToField;
                            break;
                        case "contactbusinessphone":
                            // ... business phone
                            fldBusinessPhone.Value = ffi.MappedToField;
                            break;
                        case "contactcity":
                            // ... city
                            fldCity.Value = ffi.MappedToField;
                            break;
                        case "contactcountryid":
                            // ... country
                            fldCountry.Value = ffi.MappedToField;
                            break;
                        case "contactemail":
                            // ... email
                            fldEmail.Value = ffi.MappedToField;
                            break;
                        case "contactfirstname":
                            // ... first name
                            fldFirstName.Value = ffi.MappedToField;
                            break;
                        case "contactgender":
                            // ... gender
                            fldGender.Value = ffi.MappedToField;
                            break;
                        case "contacthomephone":
                            // ... home phone
                            fldHomePhone.Value = ffi.MappedToField;
                            break;
                        case "contactjobtitle":
                            // ... job title
                            fldJobTitle.Value = ffi.MappedToField;
                            break;
                        case "contactlastname":
                            // ... last name
                            fldLastName.Value = ffi.MappedToField;
                            break;
                        case "contactmiddlename":
                            // ... middle name
                            fldMiddleName.Value = ffi.MappedToField;
                            break;
                        case "contactmobilephone":
                            // ... mobile phone
                            fldMobilePhone.Value = ffi.MappedToField;
                            break;
                        case "contactsalutation":
                            // ... salutation
                            fldSalutation.Value = ffi.MappedToField;
                            break;
                        case "contactstateid":
                            // ... state
                            fldState.Value = ffi.MappedToField;
                            break;
                        case "contacttitleafter":
                            // ... title after
                            fldTitleAfter.Value = ffi.MappedToField;
                            break;
                        case "contacttitlebefore":
                            // ... title before
                            fldTitleBefore.Value = ffi.MappedToField;
                            break;
                        case "contactwebsite":
                            // ... web site
                            fldURL.Value = ffi.MappedToField;
                            break;
                        case "contactzip":
                            // ... ZIP
                            fldZip.Value = ffi.MappedToField;
                            break;
                        default:
                            // ... contact's custom fields
                            if (customControls != null)
                            {
                                customControl = (FormEngineUserControl)customControls[ffi.Name];
                                if (customControl != null)
                                {
                                    customControl.Value = ffi.MappedToField;
                                }
                            }
                            break;
                    }
                }
            }
        }
    }


    /// <summary>
    /// Adds contact's custom fields to the dialog.
    /// </summary>
    /// <param name="fields">Array list with custom fields (FormFieldInfo)</param>
    protected void AddCustomFields(ArrayList fields)
    {
        TableRow tabRow = null;
        TableCell tabCell = null;
        LocalizedLabel label = null;
        CMSModules_AdminControls_Controls_Class_ClassFields fieldControl = null;
        // Initialize hashtable that will store controls for custom fields
        customControls = new Hashtable();

        // Add table to the placeholder 'Custom'
        Table table = new Table();
        plcCustom.Controls.Add(table);

        int i = 0;
        bool newRow = true;
        foreach (FormFieldInfo field in fields)
        {
            newRow = (i % 2) == 0;
            // Create new row - each row contains two controls
            if (newRow)
            {
                tabRow = new TableRow();
                table.Rows.Add(tabRow);
            }

            // Create lable cell
            tabCell = new TableCell();
            tabCell.CssClass = "ContactLabel";
            tabRow.Cells.Add(tabCell);

            // Create label
            label = new LocalizedLabel();
            label.Text = ResHelper.LocalizeString(field.Caption);
            label.EnableViewState = false;
            label.DisplayColon = true;
            tabCell.Controls.Add(label);

            // Create control cell
            tabCell = new TableCell();
            if (newRow)
            {
                tabCell.CssClass = "ContactControl";
            }
            else
            {
                tabCell.CssClass = "ContactControlRight";
            }
            tabRow.Cells.Add(tabCell);

            // Create control
            fieldControl = (CMSModules_AdminControls_Controls_Class_ClassFields)Page.LoadControl("~/CMSModules/AdminControls/Controls/Class/ClassFields.ascx");
            fieldControl.ID = "fld" + field.Name;
            fieldControl.ClassName = ClassName;
            fieldControl.FieldDataType = field.DataType;
            tabCell.Controls.Add(fieldControl);

            // Store the control to hashtable
            customControls.Add(field.Name, fieldControl);

            i++;
        }
    }


    /// <summary>
    /// Returns contact mapping definition.
    /// </summary>
    protected string GetMappingDefinition()
    {
        StringBuilder sb = new StringBuilder();
        string pattern = "<field column=\"{0}\" mappedtofield=\"{1}\" />";

        // Get mapped fields...
        if (!string.IsNullOrEmpty(fldAddress1.Value.ToString()))
        {
            // ... address1
            sb.AppendFormat(pattern, "ContactAddress1", fldAddress1.Value.ToString());
        }
        if (!string.IsNullOrEmpty(fldAddress2.Value.ToString()))
        {
            // ... address1
            sb.AppendFormat(pattern, "ContactAddress2", fldAddress2.Value.ToString());
        }
        if (!string.IsNullOrEmpty(fldBirthday.Value.ToString()))
        {
            // ... birthday
            sb.AppendFormat(pattern, "ContactBirthday", fldBirthday.Value.ToString());
        }
        if (!string.IsNullOrEmpty(fldBusinessPhone.Value.ToString()))
        {
            // ... business phone
            sb.AppendFormat(pattern, "ContactBusinessPhone", fldBusinessPhone.Value.ToString());
        }
        if (!string.IsNullOrEmpty(fldCity.Value.ToString()))
        {
            // ... city
            sb.AppendFormat(pattern, "ContactCity", fldCity.Value.ToString());
        }
        if (!string.IsNullOrEmpty(fldCountry.Value.ToString()))
        {
            // ... country
            sb.AppendFormat(pattern, "ContactCountryID", fldCountry.Value.ToString());
        }
        if (!string.IsNullOrEmpty(fldEmail.Value.ToString()))
        {
            // ... email
            sb.AppendFormat(pattern, "ContactEmail", fldEmail.Value.ToString());
        }
        if (!string.IsNullOrEmpty(fldFirstName.Value.ToString()))
        {
            // ... first name
            sb.AppendFormat(pattern, "ContactFirstName", fldFirstName.Value.ToString());
        }
        if (!string.IsNullOrEmpty(fldGender.Value.ToString()))
        {
            // ... gender
            sb.AppendFormat(pattern, "ContactGender", fldGender.Value.ToString());
        }
        if (!string.IsNullOrEmpty(fldHomePhone.Value.ToString()))
        {
            // ... home phone
            sb.AppendFormat(pattern, "ContactHomePhone", fldHomePhone.Value.ToString());
        }
        if (!string.IsNullOrEmpty(fldJobTitle.Value.ToString()))
        {
            // ... job title
            sb.AppendFormat(pattern, "ContactJobTitle", fldJobTitle.Value.ToString());
        }
        if (!string.IsNullOrEmpty(fldLastName.Value.ToString()))
        {
            // ... last name
            sb.AppendFormat(pattern, "ContactLastName", fldLastName.Value.ToString());
        }
        if (!string.IsNullOrEmpty(fldMiddleName.Value.ToString()))
        {
            // ... middle name
            sb.AppendFormat(pattern, "ContactMiddleName", fldMiddleName.Value.ToString());
        }
        if (!string.IsNullOrEmpty(fldMobilePhone.Value.ToString()))
        {
            // ... mobile phone
            sb.AppendFormat(pattern, "ContactMobilePhone", fldMobilePhone.Value.ToString());
        }
        if (!string.IsNullOrEmpty(fldSalutation.Value.ToString()))
        {
            // ... salutation
            sb.AppendFormat(pattern, "ContactSalutation", fldSalutation.Value.ToString());
        }
        if (!string.IsNullOrEmpty(fldState.Value.ToString()))
        {
            // ... state
            sb.AppendFormat(pattern, "ContactStateID", fldState.Value.ToString());
        }
        if (!string.IsNullOrEmpty(fldTitleAfter.Value.ToString()))
        {
            // ... title after
            sb.AppendFormat(pattern, "ContactTitleAfter", fldTitleAfter.Value.ToString());
        }
        if (!string.IsNullOrEmpty(fldTitleBefore.Value.ToString()))
        {
            // ... title before
            sb.AppendFormat(pattern, "ContactTitleBefore", fldTitleBefore.Value.ToString());
        }
        if (!string.IsNullOrEmpty(fldURL.Value.ToString()))
        {
            // ... web site
            sb.AppendFormat(pattern, "ContactWebSite", fldURL.Value.ToString());
        }
        if (!string.IsNullOrEmpty(fldZip.Value.ToString()))
        {
            // ... ZIP
            sb.AppendFormat(pattern, "ContactZIP", fldZip.Value.ToString());
        }
        if (customControls != null)
        {
            // ... contact's custom fields
            FormEngineUserControl control = null;
            foreach (DictionaryEntry entry in customControls)
            {
                control = (FormEngineUserControl)entry.Value;
                if ((control != null) && (!string.IsNullOrEmpty(control.Value.ToString())))
                {
                    sb.AppendFormat(pattern, entry.Key, control.Value.ToString());
                }
            }
        }

        if (sb.Length > 0)
        {
            // Surround the mapping definition with 'form' element
            sb.Insert(0, "<form>");
            sb.Append("</form>");
        }

        return sb.ToString();
    }
}