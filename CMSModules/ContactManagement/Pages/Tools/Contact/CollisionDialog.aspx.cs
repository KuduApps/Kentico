using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Collections;
using System.Data;
using System.Text;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.OnlineMarketing;
using CMS.CMSHelper;
using CMS.SiteProvider;
using CMS.SettingsProvider;
using CMS.ExtendedControls;
using CMS.FormControls;
using CMS.FormEngine;

public partial class CMSModules_ContactManagement_Pages_Tools_Contact_CollisionDialog : CMSModalPage
{
    #region "Variables"

    private string identificator = null;
    private DataSet mergedContacts = null;
    private ContactInfo parentContact = null;
    private string stamp;
    private Hashtable roleControls = new Hashtable();
    private Hashtable roles = new Hashtable();
    private Hashtable customFields = new Hashtable();
    private bool isSitemanager = false;

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Set title
        CurrentMaster.Title.TitleText = GetString("om.contact.collision");
        CurrentMaster.Title.TitleImage = GetImageUrl("CMSModules/CMS_ContactManagement/collisiondialog.png");
        CurrentMaster.Title.HelpTopicName = "contact_collision";
        CurrentMaster.Title.HelpName = "helpTopic";

        // Validate hash
        Regex re = RegexHelper.GetRegex(@"[\w\d_$$]*");
        identificator = QueryHelper.GetString("params", "");
        if (!QueryHelper.ValidateHash("hash") || !re.IsMatch(identificator))
        {
            pnlContent.Visible = false;
            return;
        }

        // Load dialog parameters
        Hashtable parameters = (Hashtable)WindowHelper.GetItem(identificator.ToString());
        if (parameters != null)
        {
            mergedContacts = (DataSet)parameters["MergedContacts"];
            parentContact = (ContactInfo)parameters["ParentContact"];
            isSitemanager = ValidationHelper.GetBoolean(parameters["issitemanager"], false);
            contactStatusSelector.IsSiteManager = isSitemanager;

            if (isSitemanager)
            {
                stamp = SettingsKeyProvider.GetStringValue("CMSCMStamp");
            }
            else
            {
                stamp = SettingsKeyProvider.GetStringValue(CMSContext.CurrentSiteName + ".CMSCMStamp");
            }
            stamp = CMSContext.CurrentResolver.ResolveMacros(stamp);

            if (parentContact != null)
            {
                // Check permissions
                ContactHelper.AuthorizedReadContact(parentContact.ContactSiteID, true);

                // Load data
                Initialize();
                LoadContactCollisions();
                LoadContactGroups();
                LoadCustomFields();

                // Init controls
                btnMerge.Click += new EventHandler(btnMerge_Click);
                btnCancel.Attributes.Add("onclick", "window.close(); return false;");
                btnStamp.OnClientClick = "AddStamp('" + htmlNotes.CurrentEditor.ClientID + "'); return false;";
                ScriptHelper.RegisterTooltip(Page);
                RegisterScripts();

                // Set groupping text
                pnlGeneral.GroupingText = GetString("general.general");
                pnlPersonal.GroupingText = GetString("om.contact.personal");
                pnlSettings.GroupingText = GetString("om.contact.settings");
                pnlAddress.GroupingText = GetString("general.address");
                pnlNotes.GroupingText = GetString("om.contact.notes");

                // Set tabs
                tabFields.HeaderText = GetString("om.contact.fields");
                tabContacts.HeaderText = GetString("om.account.list");
                tabContactGroups.HeaderText = GetString("om.contactgroup.list");
                tabCustomFields.HeaderText = GetString("general.customfields");
            }
        }
    }


    /// <summary>
    /// Loads account-contact role collisions.
    /// </summary>
    private void LoadContactCollisions()
    {
        StringBuilder resultQuery = new StringBuilder("ContactID IN (" + parentContact.ContactID);
        foreach (DataRow dr in mergedContacts.Tables[0].Rows)
        {
            resultQuery.Append("," + dr["ContactID"]);
        }
        resultQuery.Append(")");
        // Get all account-contact relations
        DataSet relations = new ContactAccountListInfo().Generalized.GetData(null, resultQuery.ToString(), null, -1, "AccountID,AccountName,ContactRoleID", false);

        // Group by AccountID to get distinct results
        DataTable result = relations.Tables[0].DefaultView.ToTable(true, "AccountID");
        int totalMerging = 0;

        // Display contact-account relations
        if (!DataHelper.DataSourceIsEmpty(result))
        {
            // Display prefix
            Literal prepend = new Literal();
            prepend.Text = "<table class=\"CollisionPanel\">";
            plcAccountContact.Controls.Add(prepend);

            // Display collisions
            foreach (DataRow dr in result.Rows)
            {
                totalMerging += DisplayRoleCollisions(ValidationHelper.GetInteger(dr[0], 0), relations);
            }

            // Display suffix if any relation found
            if (totalMerging > 0)
            {
                Literal append = new Literal();
                append.Text = "</table>";
                plcAccountContact.Controls.Add(append);
                lblContactInfo.ResourceString = "om.contact.accountroles";
            }
            else
            {
                tabContacts.HeaderText = null;
                tabContacts.Visible = false;
                plcAccountContact.Visible = false;
            }
        }
        // Hide content
        else
        {
            tabContacts.HeaderText = null;
            tabContacts.Visible = false;
            plcAccountContact.Visible = false;
        }
    }


    /// <summary>
    /// Loads contact groups of merged contacts into checkboxlist.
    /// </summary>
    private void LoadContactGroups()
    {
        if (!RequestHelper.IsPostBack())
        {
            StringBuilder idList = new StringBuilder("(");
            foreach (DataRow dr in mergedContacts.Tables[0].Rows)
            {
                idList.Append(dr["ContactID"] + ",");
            }
            // Remove last comma
            idList.Remove(idList.Length - 1, 1);
            idList.Append(")");

            // Remove site contact groups for global contact
            string addWhere = null;
            if (parentContact.ContactSiteID == 0)
            {
                addWhere = " AND ContactGroupMemberContactGroupID IN (SELECT ContactGroupID FROM OM_ContactGroup WHERE ContactGroupSiteID IS NULL)";
            }

            string where = " ContactGroupMemberType = 0 AND ContactGroupMemberRelatedID IN " + idList.ToString() + " AND ContactGroupMemberContactGroupID NOT IN (SELECT ContactGroupMemberContactGroupID FROM OM_ContactGroupMember WHERE ContactGroupMemberRelatedID = " + parentContact.ContactID + " AND ContactGroupMemberType = 0)" + addWhere;

            // Show only manually added contact groups
            where = SqlHelperClass.AddWhereCondition(where, "ContactGroupMemberFromManual = 1");

            // Limit selection of contact groups according to current user's persmissions
            if (!CMSContext.CurrentUser.UserSiteManagerAdmin)
            {
                bool readModifySite = ContactGroupHelper.AuthorizedReadContactGroup(parentContact.ContactSiteID, false) && ContactGroupHelper.AuthorizedModifyContactGroup(parentContact.ContactSiteID, false);
                bool readModifyGlobal = ContactGroupHelper.AuthorizedReadContactGroup(UniSelector.US_GLOBAL_RECORD, false) && ContactGroupHelper.AuthorizedModifyContactGroup(UniSelector.US_GLOBAL_RECORD, false);

                if (!readModifySite && !readModifyGlobal)
                {
                    tabContactGroups.Visible = false;
                    tabContactGroups.HeaderText = null;
                }
                else if (readModifySite && !readModifyGlobal)
                {
                    where = SqlHelperClass.AddWhereCondition(where, " ContactGroupMemberContactGroupID IN (SELECT ContactGroupID FROM OM_ContactGroup WHERE ContactGroupSiteID = " + CMSContext.CurrentSiteID + ")");
                }
                else if (!readModifySite && readModifyGlobal)
                {
                    where = SqlHelperClass.AddWhereCondition(where, " ContactGroupMemberContactGroupID IN (SELECT ContactGroupID FROM OM_ContactGroup WHERE ContactGroupSiteID IS NULL)");
                }
                else
                {
                    where = SqlHelperClass.AddWhereCondition(where, " ContactGroupMemberContactGroupID IN (SELECT ContactGroupID FROM OM_ContactGroup WHERE ContactGroupSiteID IS NULL OR ContactGroupSiteID = " + CMSContext.CurrentSiteID + ")");
                }
            }

            // Get contact group relations
            DataSet result = ContactGroupMemberInfoProvider.GetRelationships(where, null, -1, "DISTINCT ContactGroupMemberContactGroupID");

            if (!DataHelper.DataSourceIsEmpty(result))
            {
                ListItem contactGroup;
                ContactGroupInfo cg;
                foreach (DataRow dr in result.Tables[0].Rows)
                {
                    contactGroup = new ListItem();
                    contactGroup.Value = ValidationHelper.GetString(dr["ContactGroupMemberContactGroupID"], "0");
                    contactGroup.Selected = true;
                    cg = ContactGroupInfoProvider.GetContactGroupInfo(ValidationHelper.GetInteger(dr["ContactGroupMemberContactGroupID"], 0));
                    if (cg != null)
                    {
                        contactGroup.Text = HTMLHelper.HTMLEncode(cg.ContactGroupDisplayName);
                        chkContactGroups.Items.Add(contactGroup);
                    }
                }
            }
            else
            {
                tabContactGroups.Visible = false;
                tabContactGroups.HeaderText = null;
            }
        }
    }


    /// <summary>
    /// Loads custom fields collisions.
    /// </summary>
    private void LoadCustomFields()
    {
        // Check if contact has any custom fields
        FormInfo formInfo = FormHelper.GetFormInfo(parentContact.ClassName, false);
        ArrayList list = formInfo.GetFormElements(true, false, true);
        if (list.Count > 0)
        {
            FormFieldInfo ffi;
            Literal content;
            LocalizedLabel lbl;
            TextBox txt;
            Image img;
            content = new Literal();
            content.Text = "<table class=\"CollisionPanel\">";
            plcCustomFields.Controls.Add(content);

            for (int i = 0; i < list.Count; i++)
            {
                ffi = list[i] as FormFieldInfo;
                if (ffi != null)
                {
                    // Display layout
                    content = new Literal();
                    content.Text = "<tr class=\"CollisionRow\"><td class=\"LabelColumn\">";
                    plcCustomFields.Controls.Add(content);
                    lbl = new LocalizedLabel();
                    lbl.Text = ffi.Caption;
                    lbl.DisplayColon = true;
                    lbl.EnableViewState = false;
                    lbl.CssClass = "ContentLabel";
                    content = new Literal();
                    content.Text = "</td><td class=\"ComboBoxColumn\"><div class=\"ComboBox\">";
                    txt = new TextBox();
                    txt.ID = "txt" + ffi.Name;
                    txt.CssClass = "TextBoxField";
                    lbl.AssociatedControlID = txt.ID;
                    plcCustomFields.Controls.Add(lbl);
                    plcCustomFields.Controls.Add(content);
                    plcCustomFields.Controls.Add(txt);
                    content = new Literal();
                    content.Text = "</div></td><td>";
                    plcCustomFields.Controls.Add(content);
                    customFields.Add(ffi.Name, new object[] { txt, ffi.DataType });
                    DataTable dt;

                    // Get grouped dataset
                    mergedContacts.Tables[0].DefaultView.Sort = ffi.Name + " ASC";
                    if ((ffi.DataType == FormFieldDataTypeEnum.LongText) || (ffi.DataType == FormFieldDataTypeEnum.Text))
                    {
                        mergedContacts.Tables[0].DefaultView.RowFilter = ffi.Name + " NOT LIKE ''";
                    }
                    else
                    {
                        mergedContacts.Tables[0].DefaultView.RowFilter = ffi.Name + " IS NOT NULL";
                    }

                    dt = mergedContacts.Tables[0].DefaultView.ToTable(true, ffi.Name);

                    // Load value into textbox
                    txt.Text = ValidationHelper.GetString(parentContact.GetValue(ffi.Name), null);
                    if (string.IsNullOrEmpty(txt.Text) && (dt.Rows.Count > 0))
                    {
                        txt.Text = ValidationHelper.GetString(dt.Rows[0][ffi.Name], null);
                    }

                    img = new Image();
                    img.CssClass = "ResolveButton";

                    DisplayTooltip(img, dt, ffi.Name, ValidationHelper.GetString(parentContact.GetValue(ffi.Name), ""), ffi.DataType);
                    plcCustomFields.Controls.Add(img);
                    content = new Literal();
                    content.Text = "</td></tr>";
                    plcCustomFields.Controls.Add(content);
                    mergedContacts.Tables[0].DefaultView.RowFilter = null;
                }
            }
            content = new Literal();
            content.Text = "</table>";
            plcCustomFields.Controls.Add(content);
        }
        else
        {
            tabCustomFields.Visible = false;
            tabCustomFields.HeaderText = null;
        }
    }


    /// <summary>
    /// Displays account relation which has more than 1 role.
    /// </summary>
    private int DisplayRoleCollisions(int accountID, DataSet relations)
    {
        DataRow[] drs = relations.Tables[0].Select("AccountID = " + accountID + " AND ContactRoleID > 0", "ContactRoleID");

        // Account is specified more than once
        if ((drs != null) && (drs.Length > 1))
        {
            // Find out if contact roles are different
            ArrayList roleIDs = new ArrayList();
            int id;
            roleIDs.Add(ValidationHelper.GetInteger(drs[0]["ContactRoleID"], 0));
            roles.Add(drs[0]["AccountID"], drs[0]["ContactRoleID"]);
            foreach (DataRow dr in drs)
            {
                id = ValidationHelper.GetInteger(dr["ContactRoleID"], 0);
                if (!roleIDs.Contains(id))
                {
                    roleIDs.Add(id);
                }
            }

            // Display relation only for contacts with more roles
            if (roleIDs.Count > 1)
            {
                // Display table first part
                Literal ltl = new Literal();
                ltl.Text = "<tr class=\"CollisionRow\"><td>" + drs[0]["AccountName"] + "</td>";
                ltl.Text += "<td class=\"ComboBoxColumn\"><div class=\"ComboBox\">";
                plcAccountContact.Controls.Add(ltl);

                // Display role selector
                FormEngineUserControl roleSelector = Page.LoadControl("~/CMSModules/ContactManagement/FormControls/ContactRoleSelector.ascx") as FormEngineUserControl;
                roleSelector.SetValue("siteid", parentContact.ContactSiteID);
                plcAccountContact.Controls.Add(roleSelector);
                roleControls.Add(drs[0]["AccountID"], roleSelector);

                // Display table middle part
                Literal ltlMiddle = new Literal();
                ltlMiddle.Text = "</div></td><td>";
                plcAccountContact.Controls.Add(ltlMiddle);

                // Display icon with tooltip
                Image imgTooltip = new Image();
                AccountContactTooltip(imgTooltip, roleIDs);
                imgTooltip.ImageUrl = GetImageUrl("CMSModules/CMS_ContactManagement/collision.png");
                imgTooltip.Style.Add("cursor", "help");
                ScriptHelper.AppendTooltip(imgTooltip, imgTooltip.ToolTip, "help");
                plcAccountContact.Controls.Add(imgTooltip);

                // Display table last part
                Literal ltlLast = new Literal();
                ltlLast.Text = "</td></tr>";
                plcAccountContact.Controls.Add(ltlLast);

                return 1;
            }
        }

        return 0;
    }


    /// <summary>
    /// Fills tooltip with appropriate data.
    /// </summary>
    private void AccountContactTooltip(Image image, ArrayList roleIDs)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendFormat("<em>{0}</em><br />", GetString("om.contactrole.roles"));
        string[] array = new string[roleIDs.Count];
        for (int i = 0; i < roleIDs.Count; i++)
        {
            array[i] = ValidationHelper.GetString(roleIDs[i], "0");
        }

        // Get contact role display names
        DataSet ds = ContactRoleInfoProvider.GetContactRoles(SqlHelperClass.GetWhereCondition<int>("ContactRoleID", array, false), "ContactRoleDisplayName", -1, "ContactRoleDisplayName");
        if (!DataHelper.DataSourceIsEmpty(ds))
        {
            // Loop through all distinct values of given column
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                sb.AppendFormat("<br />&nbsp;-&nbsp;{0}", HTMLHelper.HTMLEncode(ValidationHelper.GetString(row["ContactRoleDisplayName"], string.Empty)));
            }
        }

        image.ToolTip += sb.ToString();
    }


    /// <summary>
    /// Initializes window with data
    /// </summary>
    private void Initialize()
    {
        if (!DataHelper.DataSourceIsEmpty(mergedContacts) && (parentContact != null))
        {
            if (!RequestHelper.IsPostBack())
            {
                BindCombobox(cmbContactFirstName, "ContactFirstName", parentContact.ContactFirstName, imgContactFirstName);
                BindCombobox(cmbContactMiddleName, "ContactMiddleName", parentContact.ContactMiddleName, imgContactMiddleName);
                BindCombobox(cmbContactLastName, "ContactLastName", parentContact.ContactLastName, imgContactLastName);
                BindCombobox(cmbContactSalutation, "ContactSalutation", parentContact.ContactSalutation, imgContactSalutation);
                BindCombobox(cmbContactTitleBefore, "ContactTitleBefore", parentContact.ContactTitleBefore, imgContactTitleBefore);
                BindCombobox(cmbContactTitleAfter, "ContactTitleAfter", parentContact.ContactTitleAfter, imgContactTitleAfter);
                BindCombobox(cmbContactJobTitle, "ContactJobTitle", parentContact.ContactJobTitle, imgContactJobTitle);
                BindCombobox(cmbContactAddress1, "ContactAddress1", parentContact.ContactAddress1, imgContactAddress1);
                BindCombobox(cmbContactAddress2, "ContactAddress2", parentContact.ContactAddress2, imgContactAddress2);
                BindCombobox(cmbContactCity, "ContactCity", parentContact.ContactCity, imgContactCity);
                BindCombobox(cmbContactZIP, "ContactZIP", parentContact.ContactZIP, imgContactZIP);
                BindCombobox(cmbContactMobilePhone, "ContactMobilePhone", parentContact.ContactMobilePhone, imgContactMobilePhone);
                BindCombobox(cmbContactHomePhone, "ContactHomePhone", parentContact.ContactHomePhone, imgContactHomePhone);
                BindCombobox(cmbContactBusinessPhone, "ContactBusinessPhone", parentContact.ContactBusinessPhone, imgContactBusinessPhone);
                BindCombobox(cmbContactEmail, "ContactEmail", parentContact.ContactEmail, imgContactEmail);
                BindCombobox(cmbContactWebSite, "ContactWebSite", parentContact.ContactWebSite, imgContactWebSite);

                InitCountry();
                InitBirthday();
                InitGender();
                InitNotes();
                InitMonitored();
                InitContactStatus();
                InitCampaign();

                lblOwner.Text = CMSContext.CurrentUser.FullName;
            }
            contactStatusSelector.SiteID = parentContact.ContactSiteID;
        }
    }


    /// <summary>
    /// Binds combobox control with data.
    /// </summary>
    private void BindCombobox(CMSFormControls_Basic_DropDownListControl control, string fieldName, string fieldValue, Image image)
    {
        DataTable dt;
        // Get grouped dataset
        mergedContacts.Tables[0].DefaultView.Sort = fieldName + " ASC";
        mergedContacts.Tables[0].DefaultView.RowFilter = fieldName + " NOT LIKE ''";
        dt = mergedContacts.Tables[0].DefaultView.ToTable(true, fieldName);

        // Bind control with data
        control.DropDownList.DataSource = dt;
        control.DropDownList.DataTextField = fieldName;
        control.DropDownList.DataValueField = fieldName;
        control.DataBind();

        // Insert parent value to first position
        if (!String.IsNullOrEmpty(fieldValue) && !control.DropDownList.Items.Contains(new ListItem(fieldValue)))
        {
            control.DropDownList.Items.Insert(0, fieldValue);
        }
        // Preselect parent value
        if (!String.IsNullOrEmpty(fieldValue) && control.DropDownList.Items.Contains(new ListItem(fieldValue)))
        {
            control.SelectedValue = fieldValue;
        }

        foreach (ListItem item in control.DropDownList.Items)
        {
            item.Text = HTMLHelper.HTMLEncode(item.Text);
        }

        // Display appropriate icon
        DisplayTooltip(image, dt, fieldName, fieldValue, FormFieldDataTypeEnum.Unknown);
    }


    /// <summary>
    /// Displays corresponding tooltip image for 'monitored' checkbox.
    /// </summary>
    private void DisplayTooltipForMonitored(Image image, DataTable tableTrueValues, DataTable tableFalseValues, bool fieldValue)
    {
        FillTooltipDataForMonitored(image, fieldValue);

        // Single value - not collision
        if ((fieldValue && DataHelper.DataSourceIsEmpty(tableFalseValues)) || (!fieldValue && DataHelper.DataSourceIsEmpty(tableTrueValues)))
        {
            image.ImageUrl = GetImageUrl("CMSModules/CMS_ContactManagement/resolved.png");
            image.Visible = true;
        }
        // Multiple values - collision
        else
        {
            image.ImageUrl = GetImageUrl("CMSModules/CMS_ContactManagement/collision.png");
            image.Visible = true;
        }

        image.Style.Add("cursor", "help");
        ScriptHelper.AppendTooltip(image, image.ToolTip, "help");

        // Reset row filter
        mergedContacts.Tables[0].DefaultView.RowFilter = null;
    }


    /// <summary>
    /// Fill tooltip data for 'monitored' checkbox.
    /// </summary>
    private void FillTooltipDataForMonitored(Image image, bool fieldValue)
    {
        // Insert header into tooltip with parent value
        image.ToolTip += "<em>" + GetString("om.contact.parentvalue") + "</em> <strong>";
        if (fieldValue)
        {
            image.ToolTip += GetString("om.contact.ismonitored");
        }
        else
        {
            image.ToolTip += GetString("om.contact.isnotmonitored");
        }
        image.ToolTip += "</strong>";

        // Merged values
        image.ToolTip += "<br /><br /><em>" + GetString("om.contact.mergedvalues") + "</em><br />";

        // Loop through all TRUE values of given column
        image.ToolTip += "<br />";
        image.ToolTip += "<strong>" + GetString("om.contact.ismonitored") + "</strong><br />";
        // Sort contacts by full name
        mergedContacts.Tables[0].DefaultView.Sort = "ContactFullNameJoined";
        mergedContacts.Tables[0].DefaultView.RowFilter = "ContactMonitored = 1";
        // Display all contacts
        DataTable contacts = mergedContacts.Tables[0].DefaultView.ToTable(false, "ContactFullNameJoined");
        foreach (DataRow contactRow in contacts.Rows)
        {
            image.ToolTip += "&nbsp;-&nbsp;" + HTMLHelper.HTMLEncode(ValidationHelper.GetString(contactRow["ContactFullNameJoined"], "").Trim()) + "<br />";
        }

        // Loop through all FALSE values of given column
        image.ToolTip += "<br />";
        image.ToolTip += "<strong>" + GetString("om.contact.isnotmonitored") + "</strong><br />";
        // Sort contacts by full name
        mergedContacts.Tables[0].DefaultView.Sort = "ContactFullNameJoined";
        mergedContacts.Tables[0].DefaultView.RowFilter = "(ContactMonitored = 0) OR (ContactMonitored IS NULL)";
        // Display all contacts
        contacts = mergedContacts.Tables[0].DefaultView.ToTable(false, "ContactFullNameJoined");
        foreach (DataRow contactRow in contacts.Rows)
        {
            image.ToolTip += "&nbsp;-&nbsp;" + HTMLHelper.HTMLEncode(ValidationHelper.GetString(contactRow["ContactFullNameJoined"], "").Trim()) + "<br />";
        }
    }


    /// <summary>
    /// Displays corresponding tooltip image.
    /// </summary>
    private bool DisplayTooltip(Image image, DataTable dt, string fieldName, string fieldValue, FormFieldDataTypeEnum dataType)
    {
        // Single value - not collision
        if (((!String.IsNullOrEmpty(fieldValue)) && (DataHelper.DataSourceIsEmpty(dt) || ((dt.Rows.Count == 1) && (ValidationHelper.GetString(dt.Rows[0][fieldName], null) == fieldValue))))
            || ((String.IsNullOrEmpty(fieldValue) || (((fieldName == "ContactCountryID") || (fieldName == "ContactStateID")) && (ValidationHelper.GetInteger(fieldValue, -1) == 0))) && (dt.Rows.Count == 1)))
        {
            FillTooltipData(image, dt, fieldName, fieldValue, dataType);
            image.ImageUrl = GetImageUrl("CMSModules/CMS_ContactManagement/resolved.png");
            image.Visible = true;
        }
        // Hide icon - no data for given field
        else if (String.IsNullOrEmpty(fieldValue) && DataHelper.DataSourceIsEmpty(dt))
        {
            image.Visible = false;
        }
        // Multiple values - collision
        else
        {
            FillTooltipData(image, dt, fieldName, fieldValue, dataType);
            image.ImageUrl = GetImageUrl("CMSModules/CMS_ContactManagement/collision.png");
            image.Visible = true;
        }
        image.Style.Add("cursor", "help");
        ScriptHelper.AppendTooltip(image, image.ToolTip, "help");

        // Reset row filter
        mergedContacts.Tables[0].DefaultView.RowFilter = null;
        return image.Visible;
    }


    /// <summary>
    /// Fills tooltip with appropriate data.
    /// </summary>
    private void FillTooltipData(Image image, DataTable dt, string fieldName, string fieldValue, FormFieldDataTypeEnum dataType)
    {
        // Insert header into tooltip with parent value
        if (!String.IsNullOrEmpty(fieldValue))
        {
            // Display birthday
            if (fieldName == "ContactBirthday")
            {
                image.ToolTip += "<em>" + GetString("om.contact.parentvalue") + "</em> <strong>" + HTMLHelper.HTMLEncode(ValidationHelper.GetDateTime(fieldValue, DateTimeHelper.ZERO_TIME).ToShortDateString()) + "</strong>";
            }
            // Display gender 
            else if (fieldName == "ContactGender")
            {
                int gender = ValidationHelper.GetInteger(fieldValue, 0);
                if (gender == (int)UserGenderEnum.Male)
                {
                    image.ToolTip += "<em>" + GetString("om.contact.parentvalue") + "</em> <strong>" + GetString("general.male") + "</strong>";

                }
                else if (gender == (int)UserGenderEnum.Female)
                {
                    image.ToolTip += "<em>" + GetString("om.contact.parentvalue") + "</em> <strong>" + GetString("general.female") + "</strong>";
                }
                else
                {
                    image.ToolTip += "<em>" + GetString("om.contact.parentvalue") + "</em> <strong>" + GetString("general.unknown") + "</strong>";
                }
            }
            // Datetime values
            else if (dataType == FormFieldDataTypeEnum.DateTime)
            {
                image.ToolTip += "<em>" + GetString("om.contact.parentvalue") + "</em> <strong>" + ValidationHelper.GetDateTime(fieldValue, DateTimeHelper.ZERO_TIME) + "</strong>";
            }
            // Get all contacts which have same string value
            else
            {
                image.ToolTip += "<em>" + GetString("om.contact.parentvalue") + "</em> ";
                if (fieldName == "ContactCountryID")
                {
                    CountryInfo country = CountryInfoProvider.GetCountryInfo(ValidationHelper.GetInteger(fieldValue, 0));
                    if (country != null)
                    {
                        image.ToolTip += "<strong>" + HTMLHelper.HTMLEncode(country.CountryDisplayName) + "</strong>";
                    }
                    else
                    {
                        image.ToolTip += GetString("general.na");
                    }
                }
                else if (fieldName == "ContactStateID")
                {
                    StateInfo state = StateInfoProvider.GetStateInfo(ValidationHelper.GetInteger(fieldValue, 0));
                    if (state != null)
                    {
                        image.ToolTip += "<strong>" + HTMLHelper.HTMLEncode(state.StateDisplayName) + "</strong>";
                    }
                    else
                    {
                        image.ToolTip += GetString("general.na");
                    }
                }
                else
                {
                    image.ToolTip += "<strong>" + HTMLHelper.HTMLEncode(fieldValue) + "</strong>";
                }
            }
        }
        else
        {
            image.ToolTip += "<em>" + GetString("om.contact.parentvalue") + "</em> " + GetString("general.na");
        }
        image.ToolTip += "<br /><br /><em>" + GetString("om.contact.mergedvalues") + "</em><br />";

        // Display N/A for empty merged records
        if (DataHelper.DataSourceIsEmpty(dt))
        {
            image.ToolTip += "<br /> " + GetString("general.na");
        }
        // Display values of merged records
        else
        {
            DataTable contacts;
            // Loop through all distinct values of given column
            foreach (DataRow dr in dt.Rows)
            {
                image.ToolTip += "<br />";
                // Sort contacts by full name
                mergedContacts.Tables[0].DefaultView.Sort = "ContactFullNameJoined";
                mergedContacts.CaseSensitive = true;

                if (fieldName == "ContactBirthday")
                {
                    // Get all contacts which have same ContactBirthday value
                    image.ToolTip += "<strong>" + HTMLHelper.HTMLEncode(ValidationHelper.GetDateTime(dr[fieldName], DateTimeHelper.ZERO_TIME).ToShortDateString()) + "</strong><br />";
                    mergedContacts.Tables[0].DefaultView.RowFilter = fieldName + " = '" + dr[fieldName] + "'";
                }
                // Display gender 
                else if (fieldName == "ContactGender")
                {
                    int gender = ValidationHelper.GetInteger(dr[fieldName], 0);
                    if (gender == (int)UserGenderEnum.Male)
                    {
                        image.ToolTip += "<strong>" + GetString("general.male") + "</strong><br />";
                    }
                    else if (gender == (int)UserGenderEnum.Female)
                    {
                        image.ToolTip += "<strong>" + GetString("general.female") + "</strong><br />";
                    }
                    else
                    {
                        image.ToolTip += "<strong>" + GetString("general.unknown") + "</strong><br />";
                    }

                    if (String.IsNullOrEmpty(ValidationHelper.GetString(dr[fieldName], null)))
                    {
                        mergedContacts.Tables[0].DefaultView.RowFilter = fieldName + " IS NULL";
                    }
                    else
                    {
                        mergedContacts.Tables[0].DefaultView.RowFilter = fieldName + " = " + dr[fieldName];
                    }
                }
                // Need to transform status ID to displayname
                else if (fieldName == "ContactStatusID")
                {
                    ContactStatusInfo status = ContactStatusInfoProvider.GetContactStatusInfo((int)dr[fieldName]);
                    image.ToolTip += "<strong>" + HTMLHelper.HTMLEncode(status.ContactStatusDisplayName) + "</strong><br />";
                    mergedContacts.Tables[0].DefaultView.RowFilter = fieldName + " = '" + status.ContactStatusID.ToString() + "'";
                }
                // Need to transform country ID to displayname
                else if (fieldName == "ContactCountryID")
                {
                    CountryInfo country = CountryInfoProvider.GetCountryInfo((int)dr[fieldName]);
                    image.ToolTip += "<strong>" + HTMLHelper.HTMLEncode(country.CountryDisplayName) + "</strong><br />";
                    mergedContacts.Tables[0].DefaultView.RowFilter = fieldName + " = '" + country.CountryID.ToString() + "'";
                }
                // Need to transform state ID to displayname
                else if (fieldName == "ContactStateID")
                {
                    StateInfo state = StateInfoProvider.GetStateInfo((int)dr[fieldName]);
                    image.ToolTip += "<strong>" + HTMLHelper.HTMLEncode(state.StateDisplayName) + "</strong><br />";
                    mergedContacts.Tables[0].DefaultView.RowFilter = fieldName + " = '" + state.StateID.ToString() + "'";
                }
                // Date time type
                else if (dataType == FormFieldDataTypeEnum.DateTime)
                {
                    image.ToolTip += "<strong>" + ValidationHelper.GetDateTime(dr[fieldName], DateTimeHelper.ZERO_TIME) + "</strong><br />";
                    mergedContacts.Tables[0].DefaultView.RowFilter = fieldName + " = '" + dr[fieldName] + "'";
                }
                // Integer data type and boolean
                else if ((dataType == FormFieldDataTypeEnum.Integer) || (dataType == FormFieldDataTypeEnum.Boolean) || (dataType == FormFieldDataTypeEnum.Decimal) || (dataType == FormFieldDataTypeEnum.GUID) || (dataType == FormFieldDataTypeEnum.LongInteger))
                {
                    image.ToolTip += "<strong>" + HTMLHelper.HTMLEncode(ValidationHelper.GetString(dr[fieldName], null)) + "</strong><br />";
                    mergedContacts.Tables[0].DefaultView.RowFilter = fieldName + " = '" + dr[fieldName] + "'";
                }
                // Get all contacts which have same string value
                else
                {
                    image.ToolTip += "<strong>" + HTMLHelper.HTMLEncode(ValidationHelper.GetString(dr[fieldName], null)) + "</strong><br />";
                    mergedContacts.Tables[0].DefaultView.RowFilter = fieldName + " LIKE '" + ContactHelper.EscapeString((string)dr[fieldName]) + "'";
                }

                // Display all contact 
                contacts = mergedContacts.Tables[0].DefaultView.ToTable(false, "ContactFullNameJoined");
                foreach (DataRow contactRow in contacts.Rows)
                {
                    image.ToolTip += "&nbsp;-&nbsp;" + HTMLHelper.HTMLEncode(ValidationHelper.GetString(contactRow["ContactFullNameJoined"], "").Trim()) + "<br />";
                }
            }
        }
    }


    /// <summary>
    /// Initializes country selector with tooltip.
    /// </summary>
    private void InitCountry()
    {
        // Get conflicting countries
        mergedContacts.Tables[0].DefaultView.Sort = "ContactCountryID";
        mergedContacts.Tables[0].DefaultView.RowFilter = "ContactCountryID > 0";
        DataTable table = mergedContacts.Tables[0].DefaultView.ToTable(true, "ContactCountryID");

        // Preselect country according to parent
        if (parentContact.ContactCountryID > 0)
        {
            countrySelector.CountryID = parentContact.ContactCountryID;
        }
        // Preselect country according to other values
        else if ((table.Rows != null) && (table.Rows.Count > 0))
        {
            countrySelector.CountryID = ValidationHelper.GetInteger(table.Rows[0][0], 0);
        }

        // Display tooltip for country
        bool display = DisplayTooltip(imgContactCountry, table, "ContactCountryID", parentContact.ContactCountryID.ToString(), FormFieldDataTypeEnum.Unknown);

        // Preselect state
        mergedContacts.Tables[0].DefaultView.Sort = "ContactStateID";
        mergedContacts.Tables[0].DefaultView.RowFilter = "ContactStateID > 0";
        table = mergedContacts.Tables[0].DefaultView.ToTable(true, "ContactStateID");
        if (parentContact.ContactStateID > 0)
        {
            countrySelector.StateID = parentContact.ContactStateID;
        }
        else if ((table.Rows != null) && (table.Rows.Count > 0))
        {
            countrySelector.StateID = ValidationHelper.GetInteger(table.Rows[0][0], 0);
        }

        // Display tooltip for state
        display &= DisplayTooltip(imgContactState, table, "ContactStateID", parentContact.ContactStateID.ToString(), FormFieldDataTypeEnum.Unknown);
    }


    /// <summary>
    /// Initializes calendar with birthday data.
    /// </summary>
    private void InitBirthday()
    {
        mergedContacts.Tables[0].DefaultView.Sort = "ContactBirthday";
        mergedContacts.Tables[0].DefaultView.RowFilter = "ContactBirthday IS NOT NULL";
        DataTable table = mergedContacts.Tables[0].DefaultView.ToTable(true, "ContactBirthday");

        // Preselect calendar with birthday data from parent         
        if (parentContact.ContactBirthday != DateTime.MinValue)
        {
            calendarControl.Value = parentContact.ContactBirthday;
        }
        // Preselect calendar with birthday data from merged contacts only if single value exists in merged items
        else if ((table.Rows != null) && (table.Rows.Count > 0))
        {
            calendarControl.Value = ValidationHelper.GetDateTime(table.Rows[0][0], DateTimeHelper.ZERO_TIME);
        }

        DisplayTooltip(imgContactBirthday, table, "ContactBirthday", parentContact.ContactBirthday == DateTimeHelper.ZERO_TIME ? null : parentContact.ContactBirthday.ToString(), FormFieldDataTypeEnum.Unknown);
    }


    /// <summary>
    /// Initializes gender selector.
    /// </summary>
    private void InitGender()
    {
        mergedContacts.Tables[0].DefaultView.Sort = "ContactGender";
        DataTable table = mergedContacts.Tables[0].DefaultView.ToTable(true, "ContactGender");

        // Preselect gender with data from parent
        if (parentContact.ContactGender != (int)UserGenderEnum.Unknown)
        {
            genderSelector.Value = parentContact.ContactGender;
        }
        // Preselect gender with data from merged contacts only if single value exists in merged items
        else if ((table.Rows != null) && (table.Rows.Count > 0))
        {
            genderSelector.Value = ValidationHelper.GetInteger(table.Rows[0][0], 0);
        }

        DisplayTooltip(imgContactGender, table, "ContactGender", parentContact.ContactGender == (int)UserGenderEnum.Unknown ? null : parentContact.ContactGender.ToString(), FormFieldDataTypeEnum.Unknown);
    }


    /// <summary>
    /// Initializes contact status selector.
    /// </summary>
    private void InitContactStatus()
    {
        mergedContacts.Tables[0].DefaultView.Sort = "ContactStatusID";
        mergedContacts.Tables[0].DefaultView.RowFilter = "ContactStatusID > 0";
        DataTable table = mergedContacts.Tables[0].DefaultView.ToTable(true, "ContactStatusID");

        // Preselect contact status with data from parent
        if (parentContact.ContactStatusID > 0)
        {
            contactStatusSelector.Value = parentContact.ContactStatusID;
        }
        // Preselect contact status with data from merged contacts only if single value exists in merged items
        else if ((table.Rows != null) && (table.Rows.Count > 0))
        {
            contactStatusSelector.Value = ValidationHelper.GetInteger(table.Rows[0][0], 0);
        }

        ContactStatusInfo csi = ContactStatusInfoProvider.GetContactStatusInfo(parentContact.ContactStatusID);
        string statusName = csi == null ? null : HTMLHelper.HTMLEncode(csi.ContactStatusDisplayName);
        DisplayTooltip(imgContactStatus, table, "ContactStatusID", statusName, FormFieldDataTypeEnum.Unknown);
    }


    /// <summary>
    /// Initializes monitored checkbox with tooltip.
    /// </summary>
    private void InitMonitored()
    {
        DataTable tableTrueValues = SortGroupContactsByColumn("ContactMonitored", "ContactMonitored = 1", "ContactMonitored");
        DataTable tableFalseValues = SortGroupContactsByColumn("ContactMonitored", "(ContactMonitored = 0) OR (ContactMonitored IS NULL)", "ContactMonitored");

        chkContactMonitored.Checked = parentContact.ContactMonitored;
        DisplayTooltipForMonitored(imgContactMonitored, tableTrueValues, tableFalseValues, parentContact.ContactMonitored);
    }


    /// <summary>
    /// Sorts, filters and groups contacts by specified rules
    /// </summary>
    /// <param name="sortRule">Sorting rule by specified column</param>
    /// <param name="rowFilter">Filtering rule by specified column</param>
    /// <param name="groupByColumn">Grouping by specified column</param>
    /// <returns>Returns sorted DataTable</returns>
    private DataTable SortGroupContactsByColumn(string sortRule, string rowFilter, string groupByColumn)
    {
        mergedContacts.Tables[0].DefaultView.Sort = sortRule;
        mergedContacts.Tables[0].DefaultView.RowFilter = rowFilter;
        return mergedContacts.Tables[0].DefaultView.ToTable(true, groupByColumn);
    }


    /// <summary>
    /// Initializes campaign selector with tooltip.
    /// </summary>
    private void InitCampaign()
    {
        mergedContacts.Tables[0].DefaultView.Sort = "ContactCampaign";
        mergedContacts.Tables[0].DefaultView.RowFilter = "ContactCampaign IS NOT NULL AND ContactCampaign <> ''";
        DataTable table = mergedContacts.Tables[0].DefaultView.ToTable(true, "ContactCampaign");

        // Preselect contact campaign with data from parent
        if (parentContact.ContactCampaign != String.Empty)
        {
            cCampaign.Value = parentContact.ContactCampaign;
        }
        // Preselect contact campaign with data from merged contacts only if single value exists in merged items
        else if ((table.Rows != null) && (table.Rows.Count > 0))
        {
            cCampaign.Value = ValidationHelper.GetString(table.Rows[0][0], String.Empty);
        }

        DisplayTooltip(imgContactCampaign, table, "ContactCampaign", parentContact.ContactCampaign, FormFieldDataTypeEnum.Text);
    }


    /// <summary>
    /// Initializes notes.
    /// </summary>
    private void InitNotes()
    {
        // Merge contact notes
        htmlNotes.Value = parentContact.ContactNotes;
        mergedContacts.Tables[0].DefaultView.RowFilter = "ContactNotes NOT LIKE ''";
        DataTable table = mergedContacts.Tables[0].DefaultView.ToTable(true, "ContactNotes");

        // Preselect value only if single value exists in merged items
        if ((table.Rows != null) && (table.Rows.Count > 0))
        {
            foreach (DataRow dr in table.Rows)
            {
                htmlNotes.Value += ValidationHelper.GetString(dr[0], "");
            }

            htmlNotes.Value += "<br />" + stamp + "<br />" + GetString("om.contact.notesmerged");
        }
    }


    /// <summary>
    /// Button merge click.
    /// </summary>
    void btnMerge_Click(object sender, EventArgs e)
    {
        // Check permissions
        ContactHelper.AuthorizedModifyContact(parentContact.ContactSiteID, true);

        // Validate form
        if (ValidateForm())
        {
            // Change parent contact values
            Save();
            // Update hashtable with account-contact roles
            UpdateRoles();

            // Merge contacts
            ContactHelper.Merge(parentContact, mergedContacts, roles, GetContactGroups());

            // Close window and refresh parent window
            ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "WOpenerRefresh", ScriptHelper.GetScript(
@"wopener.RefreshPage(); 
window.close();
"));
        }
    }



    /// <summary>
    /// Returns hashtable based on selected values of contact groups.
    /// </summary>
    private Hashtable GetContactGroups()
    {
        Hashtable selectedContactGroups = new Hashtable();
        foreach (ListItem cg in chkContactGroups.Items)
        {
            selectedContactGroups.Add(ValidationHelper.GetInteger(cg.Value, 0), cg.Selected);
        }
        return selectedContactGroups;
    }


    /// <summary>
    /// Updates contact roles with roles selected by user.
    /// </summary>
    private void UpdateRoles()
    {
        foreach (int key in roleControls.Keys)
        {
            roles[key] = ((FormEngineUserControl)roleControls[key]).Value;
        }
    }

    /// <summary>
    /// Saves merge changes.
    /// </summary>
    private void Save()
    {
        parentContact.ContactFirstName = cmbContactFirstName.Text.Trim();
        parentContact.ContactMiddleName = cmbContactMiddleName.Text.Trim();
        parentContact.ContactLastName = cmbContactLastName.Text.Trim();
        parentContact.ContactSalutation = cmbContactSalutation.Text.Trim();
        parentContact.ContactTitleBefore = cmbContactTitleBefore.Text.Trim();
        parentContact.ContactTitleAfter = cmbContactTitleAfter.Text.Trim();
        parentContact.ContactJobTitle = cmbContactJobTitle.Text.Trim();
        parentContact.ContactAddress1 = cmbContactAddress1.Text.Trim();
        parentContact.ContactAddress2 = cmbContactAddress2.Text.Trim();
        parentContact.ContactCity = cmbContactCity.Text.Trim();
        parentContact.ContactZIP = cmbContactZIP.Text.Trim();
        parentContact.ContactStateID = countrySelector.StateID;
        parentContact.ContactCountryID = countrySelector.CountryID;
        parentContact.ContactMobilePhone = cmbContactMobilePhone.Text.Trim();
        parentContact.ContactHomePhone = cmbContactHomePhone.Text.Trim();
        parentContact.ContactBusinessPhone = cmbContactBusinessPhone.Text.Trim();
        parentContact.ContactEmail = cmbContactEmail.Text.Trim();
        parentContact.ContactWebSite = cmbContactWebSite.Text.Trim();
        parentContact.ContactBirthday = ValidationHelper.GetDateTime(calendarControl.Value, DateTimeHelper.ZERO_TIME);
        parentContact.ContactGender = genderSelector.Gender;
        parentContact.ContactStatusID = contactStatusSelector.ContactStatusID;
        parentContact.ContactCampaign = (string)cCampaign.Value;
        parentContact.ContactNotes = (string)htmlNotes.Value;
        parentContact.ContactOwnerUserID = CMSContext.CurrentUser.UserID;
        parentContact.ContactMonitored = chkContactMonitored.Checked;

        // Save cusotm fields
        foreach (string key in customFields.Keys)
        {
            // Get value from
            object value = ((object[])customFields[key])[0];
            FormFieldDataTypeEnum datatype = (FormFieldDataTypeEnum)((object[])customFields[key])[1];
            string text = ((TextBox)value).Text;

            if (!String.IsNullOrEmpty(text))
            {
                // Save value according to specific data types
                switch (datatype)
                {
                    case FormFieldDataTypeEnum.DateTime:
                        parentContact.SetValue(key, ValidationHelper.GetDateTime(text, DateTime.Now));
                        break;

                    case FormFieldDataTypeEnum.Boolean:
                        parentContact.SetValue(key, ValidationHelper.GetBoolean(text, false));
                        break;

                    case FormFieldDataTypeEnum.GUID:
                        parentContact.SetValue(key, ValidationHelper.GetGuid(text, Guid.Empty));
                        break;

                    case FormFieldDataTypeEnum.Integer:
                    case FormFieldDataTypeEnum.LongInteger:
                        parentContact.SetValue(key, ValidationHelper.GetInteger(text, 0));
                        break;

                    case FormFieldDataTypeEnum.Decimal:
                        parentContact.SetValue(key, ValidationHelper.GetDouble(text, 0));
                        break;

                    default:
                        parentContact.SetValue(key, text);
                        break;
                }
            }
        }
        ContactInfoProvider.SetContactInfo(parentContact);
    }


    /// <summary>
    /// Performs custom validation and displays error in top of the page.
    /// </summary>
    /// <returns>Returns true if validation is successful.</returns>
    private bool ValidateForm()
    {
        // Validate name
        string errorMessage = new Validator().NotEmpty(cmbContactLastName.Text.Trim(), GetString("om.contact.enterlastname")).Result;
        if (!String.IsNullOrEmpty(errorMessage.ToString()))
        {
            lblError.Text = errorMessage;
            lblError.Visible = true;
            return false;
        }

        // Validates birthday
        if (!calendarControl.IsValid())
        {
            lblError.Text = GetString("om.contact.enterbirthday");
            lblError.Visible = true;
            return false;
        }

        // Validates email
        if (!String.IsNullOrEmpty(cmbContactEmail.Text) && !ValidationHelper.IsEmail(cmbContactEmail.Text))
        {
            lblError.Text = GetString("om.contact.enteremail");
            lblError.Visible = true; return false;
        }

        return true;
    }


    /// <summary>
    /// Registers JavaScripts on page.
    /// </summary>
    private void RegisterScripts()
    {
        ScriptHelper.RegisterClientScriptBlock(this.Page, typeof(string), "AddStamp", ScriptHelper.GetScript(
@"function InsertHTML(htmlString, ckClientID)
{
    // Get the editor instance that we want to interact with.
    var oEditor = oEditor = window.CKEDITOR.instances[ckClientID];
    // Check the active editing mode.
    if (oEditor != null) {
        // Check the active editing mode.
        if (oEditor.mode == 'wysiwyg') {
            // Insert the desired HTML.
            oEditor.focus();
            oEditor.insertHtml(htmlString);        
        }
    }    
    return false;
}   

function AddStamp(ckClientID)
{
    InsertHTML('<div>" + CMSContext.CurrentResolver.ResolveMacros(stamp).Replace("'", @"\'") + @"</div>', ckClientID);
}"
));
    }

    #endregion
}