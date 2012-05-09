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

public partial class CMSModules_ContactManagement_Pages_Tools_Account_CollisionDialog : CMSModalPage
{
    #region "Variables"

    private string identificator = null;
    private DataSet mergedAccounts = null;
    private AccountInfo parentAccount = null;
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
        CurrentMaster.Title.HelpTopicName = "account_collision";
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
            mergedAccounts = (DataSet)parameters["MergedAccounts"];
            parentAccount = (AccountInfo)parameters["ParentAccount"];
            isSitemanager = ValidationHelper.GetBoolean(parameters["issitemanager"], false);

            if (isSitemanager)
            {
                stamp = SettingsKeyProvider.GetStringValue("CMSCMStamp");
            }
            else
            {
                stamp = SettingsKeyProvider.GetStringValue(CMSContext.CurrentSiteName + ".CMSCMStamp");
            }

            stamp = CMSContext.CurrentResolver.ResolveMacros(stamp);

            if (parentAccount != null)
            {
                // Check permissions
                AccountHelper.AuthorizedReadAccount(parentAccount.AccountSiteID, true);

                // Load data
                if (!RequestHelper.IsPostBack())
                {
                    Initialize();
                }
                LoadContactCollisions();
                LoadContactGroups();
                LoadCustomFields();

                // Init controls
                btnMerge.Click += new EventHandler(btnMerge_Click);
                btnCancel.Attributes.Add("onclick", "window.close(); return false;");
                btnStamp.OnClientClick = "AddStamp('" + htmlNotes.CurrentEditor.ClientID + "'); return false;";
                ScriptHelper.RegisterTooltip(Page);
                RegisterScripts();
                accountStatusSelector.SiteID = parentAccount.AccountSiteID;
                accountSelector.SiteID = parentAccount.AccountSiteID;
                accountSelector.WhereCondition = "((AccountMergedWithAccountID IS NULL) AND (AccountSiteID > 0)) OR ((AccountGlobalAccountID IS NULL) AND (AccountSiteID IS NULL))";
                accountSelector.WhereCondition = GetSubsidiaryWhere(accountSelector.WhereCondition);
                accountStatusSelector.IsSiteManager = isSitemanager;

                // Set groupping text
                pnlGeneral.GroupingText = GetString("general.general");
                pnlAddress.GroupingText = GetString("general.address");
                pnlNotes.GroupingText = GetString("om.contact.notes");

                // Set tabs
                tabFields.HeaderText = GetString("om.contact.fields");
                tabContacts.HeaderText = GetString("om.contact.list");
                tabContactGroups.HeaderText = GetString("om.contactgroup.list");
                tabCustomFields.HeaderText = GetString("general.customfields");
            }
        }
    }


    /// <summary>
    /// Initializes window with data
    /// </summary>
    private void Initialize()
    {
        if (!DataHelper.DataSourceIsEmpty(mergedAccounts) && (parentAccount != null))
        {
            BindCombobox(cmbAccountName, "AccountName", parentAccount.AccountName, imgAccountName);
            BindCombobox(cmbAccountAddress1, "AccountAddress1", parentAccount.AccountAddress1, imgAccountAddress1);
            BindCombobox(cmbAccountAddress2, "AccountAddress2", parentAccount.AccountAddress2, imgAccountAddress2);
            BindCombobox(cmbAccountCity, "AccountCity", parentAccount.AccountCity, imgAccountCity);
            BindCombobox(cmbAccountZIP, "AccountZIP", parentAccount.AccountZIP, imgAccountZIP);
            BindCombobox(cmbAccountPhone, "AccountPhone", parentAccount.AccountPhone, imgAccountPhone);
            BindCombobox(cmbAccountFax, "AccountFax", parentAccount.AccountFax, imgAccountFax);
            BindCombobox(cmbAccountEmail, "AccountEmail", parentAccount.AccountEmail, imgAccountEmail);
            BindCombobox(cmbAccountWebSite, "AccountWebSite", parentAccount.AccountWebSite, imgAccountWebSite);

            InitAccountStatus();
            InitHeadquarters();
            InitCountry();
            InitNotes();

            lblOwner.Text = CMSContext.CurrentUser.FullName;
        }
    }


    /// <summary>
    /// Loads account-contact role collisions.
    /// </summary>
    private void LoadContactCollisions()
    {
        StringBuilder resultQuery = new StringBuilder("AccountID IN (" + parentAccount.AccountID);
        foreach (DataRow dr in mergedAccounts.Tables[0].Rows)
        {
            resultQuery.Append("," + dr["AccountID"]);
        }
        resultQuery.Append(")");
        // Get all account-contact relations
        DataSet relations = new AccountContactListInfo().Generalized.GetData(null, resultQuery.ToString(), null, -1, "ContactID,ContactFirstName,ContactMiddleName,ContactLastName,ContactRoleID", false);

        // Group by contactID to get distinct results
        DataTable result = relations.Tables[0].DefaultView.ToTable(true, "ContactID");
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
            }
            else
            {
                tabContacts.Visible = false;
                tabContacts.HeaderText = null;
                plcAccountContact.Visible = false;
            }
        }
        // Hide content
        else
        {
            tabContacts.Visible = false;
            tabContacts.HeaderText = null;
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
            foreach (DataRow dr in mergedAccounts.Tables[0].Rows)
            {
                idList.Append(dr["AccountID"] + ",");
            }
            // Remove last comma
            idList.Remove(idList.Length - 1, 1);
            idList.Append(")");

            // Remove site contact groups
            string addWhere = null;
            if (parentAccount.AccountSiteID == 0)
            {
                addWhere = " AND ContactGroupMemberContactGroupID IN (SELECT ContactGroupID FROM OM_ContactGroup WHERE ContactGroupSiteID IS NULL)";
            }

            string where = "ContactGroupMemberType = 1 AND ContactGroupMemberRelatedID IN " + idList.ToString() + " AND ContactGroupMemberContactGroupID NOT IN (SELECT ContactGroupMemberContactGroupID FROM OM_ContactGroupMember WHERE ContactGroupMemberRelatedID = " + parentAccount.AccountID + " AND ContactGroupMemberType = 1)" + addWhere;

            // Limit selection of contact groups according to current user's persmissions
            if (!CMSContext.CurrentUser.UserSiteManagerAdmin)
            {
                bool readModifySite = ContactGroupHelper.AuthorizedReadContactGroup(parentAccount.AccountSiteID, false) && ContactGroupHelper.AuthorizedModifyContactGroup(parentAccount.AccountSiteID, false);
                bool readGlobal = ContactGroupHelper.AuthorizedReadContactGroup(UniSelector.US_GLOBAL_RECORD, false) && ContactGroupHelper.AuthorizedModifyContactGroup(UniSelector.US_GLOBAL_RECORD, false);
                if (!readModifySite && !readGlobal)
                {
                    tabContactGroups.Visible = false;
                    tabContactGroups.HeaderText = null;
                }
                else if (readModifySite && !readGlobal)
                {
                    where = SqlHelperClass.AddWhereCondition(where, " ContactGroupMemberContactGroupID IN (SELECT ContactGroupID FROM OM_ContactGroup WHERE ContactGroupSiteID = " + CMSContext.CurrentSiteID + ")");
                }
                else if (!readModifySite && readGlobal)
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

                    // Fill in checkbox list
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
        // Check if account has any custom fields
        FormInfo formInfo = FormHelper.GetFormInfo(parentAccount.ClassName, false);
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

            // Display all custom fields
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
                    content.Text = "<td class=\"ComboBoxColumn\"><div class=\"ComboBox\">";
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
                    mergedAccounts.Tables[0].DefaultView.Sort = ffi.Name + " ASC";
                    if ((ffi.DataType == FormFieldDataTypeEnum.LongText) || (ffi.DataType == FormFieldDataTypeEnum.Text))
                    {
                        mergedAccounts.Tables[0].DefaultView.RowFilter = ffi.Name + " NOT LIKE ''";
                    }
                    else
                    {
                        mergedAccounts.Tables[0].DefaultView.RowFilter = ffi.Name + " IS NOT NULL";
                    }
                    dt = mergedAccounts.Tables[0].DefaultView.ToTable(true, ffi.Name);

                    // Load value into textbox
                    txt.Text = ValidationHelper.GetString(parentAccount.GetValue(ffi.Name), null);
                    if (string.IsNullOrEmpty(txt.Text) && (dt.Rows.Count > 0))
                    {
                        txt.Text = ValidationHelper.GetString(dt.Rows[0][ffi.Name], null);
                    }

                    img = new Image();
                    img.CssClass = "ResolveButton";

                    // Display tooltip
                    DisplayTooltip(img, dt, ffi.Name, ValidationHelper.GetString(parentAccount.GetValue(ffi.Name), ""), ffi.DataType);
                    plcCustomFields.Controls.Add(img);
                    content = new Literal();
                    content.Text = "</td></tr>";
                    plcCustomFields.Controls.Add(content);
                    mergedAccounts.Tables[0].DefaultView.RowFilter = null;
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
    /// Displays contact relation which has more than 1 role.
    /// </summary>
    private int DisplayRoleCollisions(int contactID, DataSet relations)
    {
        DataRow[] drs = relations.Tables[0].Select("ContactID = " + contactID + " AND ContactRoleID > 0", "ContactRoleID");

        // Contact is specified more than once
        if ((drs != null) && (drs.Length > 1))
        {
            // Find out if contact roles are different
            ArrayList roleIDs = new ArrayList();
            int id;
            roleIDs.Add(ValidationHelper.GetInteger(drs[0]["ContactRoleID"], 0));
            roles.Add(drs[0]["ContactID"], drs[0]["ContactRoleID"]);
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
                string contactName = drs[0]["ContactFirstName"] + " " + drs[0]["ContactMiddleName"];
                contactName = contactName.Trim() + " " + drs[0]["ContactLastName"];
                ltl.Text = "<tr class=\"CollisionRow\"><td>" + contactName.Trim() + "</td>";
                ltl.Text += "<td class=\"ComboBoxColumn\"><div class=\"ComboBox\">";
                plcAccountContact.Controls.Add(ltl);

                // Display role selector
                FormEngineUserControl roleSelector = Page.LoadControl("~/CMSModules/ContactManagement/FormControls/ContactRoleSelector.ascx") as FormEngineUserControl;
                roleSelector.SetValue("siteid", parentAccount.AccountSiteID);
                plcAccountContact.Controls.Add(roleSelector);
                roleControls.Add(drs[0]["ContactID"], roleSelector);

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
    /// Binds combobox control with data.
    /// </summary>
    private void BindCombobox(CMSFormControls_Basic_DropDownListControl control, string fieldName, string fieldValue, Image image)
    {
        DataTable dt;
        // Get grouped dataset
        mergedAccounts.Tables[0].DefaultView.Sort = fieldName + " ASC";
        mergedAccounts.Tables[0].DefaultView.RowFilter = fieldName + " NOT LIKE ''";
        dt = mergedAccounts.Tables[0].DefaultView.ToTable(true, fieldName);

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
    /// Displays corresponding tooltip image.
    /// </summary>
    private bool DisplayTooltip(Image image, DataTable dt, string fieldName, string fieldValue, FormFieldDataTypeEnum dataType)
    {
        // Single value - not collision
        if (((!String.IsNullOrEmpty(fieldValue)) && (DataHelper.DataSourceIsEmpty(dt) || ((dt.Rows.Count == 1) && (ValidationHelper.GetString(dt.Rows[0][fieldName], null) == fieldValue))))
            || ((String.IsNullOrEmpty(fieldValue) || (((fieldName == "AccountCountryID") || (fieldName == "AccountStateID")) && (ValidationHelper.GetInteger(fieldValue, -1) == 0))) && (dt.Rows.Count == 1)))
        {
            FillTooltipData(image, dt, fieldName, fieldValue, dataType);
            image.ImageUrl = GetImageUrl("CMSModules/CMS_ContactManagement/resolved.png");
            image.Visible = true;
        }
        // No data - hide icon
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
        mergedAccounts.Tables[0].DefaultView.RowFilter = null;
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
            // Datetime values
            if (dataType == FormFieldDataTypeEnum.DateTime)
            {
                image.ToolTip += "<em>" + GetString("om.account.parentvalue") + "</em> <strong>" + ValidationHelper.GetDateTime(fieldValue, DateTimeHelper.ZERO_TIME) + "</strong>";
            }
            else
            {
                // Country
                image.ToolTip += "<em>" + GetString("om.account.parentvalue") + "</em> ";
                if (fieldName == "AccountCountryID")
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
                // State
                else if (fieldName == "AccountStateID")
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
                // Otherwise
                else
                {
                    image.ToolTip += "<em>" + GetString("om.account.parentvalue") + "</em> <strong>" + HTMLHelper.HTMLEncode(fieldValue) + "</strong>";
                }
            }
        }
        else
        {
            image.ToolTip += "<em>" + GetString("om.account.parentvalue") + "</em> " + GetString("general.na");
        }
        image.ToolTip += "<br /><br /><em>" + GetString("om.account.mergedvalues") + "</em><br />";


        // Display N/A for empty merged records
        if (DataHelper.DataSourceIsEmpty(dt))
        {
            image.ToolTip += "<br /> " + GetString("general.na");
        }
        // Display values of merged records
        else
        {
            DataTable accounts;
            // Loop through all distinct values of given column
            foreach (DataRow dr in dt.Rows)
            {
                image.ToolTip += "<br />";
                // Sort accounts by full name
                mergedAccounts.Tables[0].DefaultView.Sort = "AccountName";
                mergedAccounts.CaseSensitive = true;

                // Need to transform status ID to displayname
                if (fieldName == "AccountStatusID")
                {
                    AccountStatusInfo status = AccountStatusInfoProvider.GetAccountStatusInfo((int)dr[fieldName]);
                    image.ToolTip += "<strong>" + HTMLHelper.HTMLEncode(status.AccountStatusDisplayName) + "</strong><br />";
                    mergedAccounts.Tables[0].DefaultView.RowFilter = fieldName + " = '" + status.AccountStatusID.ToString() + "'";
                }
                // Need to transform country ID to displayname
                else if (fieldName == "AccountCountryID")
                {
                    CountryInfo country = CountryInfoProvider.GetCountryInfo((int)dr[fieldName]);
                    image.ToolTip += "<strong>" + HTMLHelper.HTMLEncode(country.CountryDisplayName) + "</strong><br />";
                    mergedAccounts.Tables[0].DefaultView.RowFilter = fieldName + " = '" + country.CountryID.ToString() + "'";
                }
                // Need to transform state ID to displayname
                else if (fieldName == "AccountStateID")
                {
                    StateInfo state = StateInfoProvider.GetStateInfo((int)dr[fieldName]);
                    image.ToolTip += "<strong>" + HTMLHelper.HTMLEncode(state.StateDisplayName) + "</strong><br />";
                    mergedAccounts.Tables[0].DefaultView.RowFilter = fieldName + " = '" + state.StateID.ToString() + "'";
                }
                // Date time type
                else if (dataType == FormFieldDataTypeEnum.DateTime)
                {
                    image.ToolTip += "<strong>" + ValidationHelper.GetDateTime(dr[fieldName], DateTimeHelper.ZERO_TIME) + "</strong><br />";
                    mergedAccounts.Tables[0].DefaultView.RowFilter = fieldName + " = '" + dr[fieldName] + "'";
                }
                // Integer data type and boolean
                else if ((dataType == FormFieldDataTypeEnum.Integer) || (dataType == FormFieldDataTypeEnum.Boolean) || (dataType == FormFieldDataTypeEnum.Decimal) || (dataType == FormFieldDataTypeEnum.GUID) || (dataType == FormFieldDataTypeEnum.LongInteger))
                {
                    image.ToolTip += "<strong>" + HTMLHelper.HTMLEncode(ValidationHelper.GetString(dr[fieldName], null)) + "</strong><br />";
                    mergedAccounts.Tables[0].DefaultView.RowFilter = fieldName + " = '" + dr[fieldName] + "'";
                }
                // Get all contacts which have same string value
                else
                {
                    image.ToolTip += "<strong>" + HTMLHelper.HTMLEncode(ValidationHelper.GetString(dr[fieldName], null)) + "</strong><br />";
                    mergedAccounts.Tables[0].DefaultView.RowFilter = fieldName + " LIKE '" + ContactHelper.EscapeString((string)dr[fieldName]) + "'";
                }

                // Display all accounts 
                accounts = mergedAccounts.Tables[0].DefaultView.ToTable(false, "AccountName");
                foreach (DataRow row in accounts.Rows)
                {
                    image.ToolTip += "&nbsp;-&nbsp;" + HTMLHelper.HTMLEncode(((string)row["AccountName"]).Trim()) + "<br />";
                }
            }
        }
    }


    /// <summary>
    /// Initializes account status selector.
    /// </summary>
    private void InitAccountStatus()
    {
        mergedAccounts.Tables[0].DefaultView.Sort = "AccountStatusID";
        mergedAccounts.Tables[0].DefaultView.RowFilter = "AccountStatusID > 0";
        DataTable table = mergedAccounts.Tables[0].DefaultView.ToTable(true, "AccountStatusID");

        // Preselect account status with data from parent
        if (parentAccount.AccountStatusID > 0)
        {
            accountStatusSelector.Value = parentAccount.AccountStatusID;
        }
        // Preselect account status with data from merged accounts only if single value exists in merged items
        else if ((table.Rows != null) && (table.Rows.Count > 0))
        {
            accountStatusSelector.Value = ValidationHelper.GetInteger(table.Rows[0][0], 0);
        }

        AccountStatusInfo asi = AccountStatusInfoProvider.GetAccountStatusInfo(parentAccount.AccountStatusID);
        string statusName = asi == null ? null : HTMLHelper.HTMLEncode(asi.AccountStatusDisplayName);
        DisplayTooltip(imgAccountStatus, table, "AccountStatusID", statusName, FormFieldDataTypeEnum.Unknown);
    }



    /// <summary>
    /// Initializes headquarters selector
    /// </summary>
    private void InitHeadquarters()
    {
        mergedAccounts.Tables[0].DefaultView.Sort = "AccountSubsidiaryOfID";
        mergedAccounts.Tables[0].DefaultView.RowFilter = "AccountSubsidiaryOfID > 0";
        DataTable table = mergedAccounts.Tables[0].DefaultView.ToTable(true, new string[] { "AccountSubsidiaryOfID", "SubsidiaryOfName" });

        // Preselect headquarters with data from parent
        if (parentAccount.AccountSubsidiaryOfID > 0)
        {
            accountSelector.Value = parentAccount.AccountSubsidiaryOfID;
        }
        // Preselect account status with data from merged accounts only if single value exists in merged items
        else if ((table.Rows != null) && (table.Rows.Count > 0))
        {
            accountSelector.Value = ValidationHelper.GetInteger(table.Rows[0][0], 0);
        }

        AccountInfo ai = AccountInfoProvider.GetAccountInfo(parentAccount.AccountSubsidiaryOfID);
        string name = ai == null ? null : ai.AccountName;
        DisplayTooltip(imgAccountHeadquarters, table, "SubsidiaryOfName", name, FormFieldDataTypeEnum.Unknown);
    }


    /// <summary>
    /// Returns contact's full name joined from first, middle and last name.
    /// </summary>
    private string GetContactFullName(ContactInfo ci)
    {
        if (ci != null)
        {
            string name = ci.ContactFirstName + " " + ci.ContactMiddleName;
            name = name.Trim() + " " + ci.ContactLastName;
            return name.Trim();
        }
        return null;
    }


    /// <summary>
    /// Initializes country selector with tooltip.
    /// </summary>
    private void InitCountry()
    {
        // Get conflicting countries
        mergedAccounts.Tables[0].DefaultView.Sort = "AccountCountryID";
        mergedAccounts.Tables[0].DefaultView.RowFilter = "AccountCountryID > 0";
        DataTable table = mergedAccounts.Tables[0].DefaultView.ToTable(true, "AccountCountryID");

        // Preselect country according to parent
        if (parentAccount.AccountCountryID > 0)
        {
            countrySelector.CountryID = parentAccount.AccountCountryID;
        }
        else if ((table.Rows != null) && (table.Rows.Count > 0))
        {
            countrySelector.CountryID = ValidationHelper.GetInteger(table.Rows[0][0], 0);
        }

        // Display tooltip for country
        bool display = DisplayTooltip(imgAccountCountry, table, "AccountCountryID", parentAccount.AccountCountryID.ToString(), FormFieldDataTypeEnum.Unknown);

        // Preselect state
        mergedAccounts.Tables[0].DefaultView.Sort = "AccountStateID";
        mergedAccounts.Tables[0].DefaultView.RowFilter = "AccountStateID > 0";
        table = mergedAccounts.Tables[0].DefaultView.ToTable(true, new string[] { "AccountStateID" });
        if (parentAccount.AccountStateID > 0)
        {
            countrySelector.StateID = parentAccount.AccountStateID;
        }
        else if ((table.Rows != null) && (table.Rows.Count > 0))
        {
            countrySelector.StateID = ValidationHelper.GetInteger(table.Rows[0][0], 0);
        }

        // Display tooltip for state
        display &= DisplayTooltip(imgAccountState, table, "AccountStateID", parentAccount.AccountStateID.ToString(), FormFieldDataTypeEnum.Unknown);
    }


    /// <summary>
    /// Initializes notes.
    /// </summary>
    private void InitNotes()
    {
        // Merge accounts notes
        htmlNotes.Value = parentAccount.AccountNotes;
        mergedAccounts.Tables[0].DefaultView.RowFilter = "AccountNotes NOT LIKE ''";
        DataTable table = mergedAccounts.Tables[0].DefaultView.ToTable(true, "AccountNotes");

        // Preselect value only if single value exists in merged items
        if ((table.Rows != null) && (table.Rows.Count > 0))
        {
            foreach (DataRow dr in table.Rows)
            {
                htmlNotes.Value += ValidationHelper.GetString(dr[0], null);
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
        if (AccountHelper.AuthorizedModifyAccount(parentAccount.AccountSiteID, true))
        {
            // Validate form
            if (ValidateForm())
            {
                // Change parent contact values
                Save();

                // Update hashtable with account-contact roles
                UpdateRoles();

                // Merge contacts
                AccountHelper.Merge(parentAccount, mergedAccounts, roles, GetContactGroups());

                // Close window and refresh parent window
                ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "WOpenerRefresh", ScriptHelper.GetScript(@"wopener.RefreshPage(); window.close();"));
            }
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
        parentAccount.AccountName = cmbAccountName.Text.Trim();
        parentAccount.AccountAddress1 = cmbAccountAddress1.Text.Trim();
        parentAccount.AccountAddress2 = cmbAccountAddress2.Text.Trim();
        parentAccount.AccountCity = cmbAccountCity.Text.Trim();
        parentAccount.AccountZIP = cmbAccountZIP.Text.Trim();
        parentAccount.AccountStateID = countrySelector.StateID;
        parentAccount.AccountCountryID = countrySelector.CountryID;
        parentAccount.AccountWebSite = cmbAccountWebSite.Text.Trim();
        parentAccount.AccountPhone = cmbAccountPhone.Text.Trim();
        parentAccount.AccountEmail = cmbAccountEmail.Text.Trim();
        parentAccount.AccountFax = cmbAccountFax.Text.Trim();
        parentAccount.AccountStatusID = accountStatusSelector.AccountStatusID;
        parentAccount.AccountNotes = (string)htmlNotes.Value;
        parentAccount.AccountOwnerUserID = CMSContext.CurrentUser.UserID;
        parentAccount.AccountSubsidiaryOfID = accountSelector.AccountID;

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
                        parentAccount.SetValue(key, ValidationHelper.GetDateTime(text, DateTime.Now));
                        break;

                    case FormFieldDataTypeEnum.Boolean:
                        parentAccount.SetValue(key, ValidationHelper.GetBoolean(text, false));
                        break;

                    case FormFieldDataTypeEnum.GUID:
                        parentAccount.SetValue(key, ValidationHelper.GetGuid(text, Guid.Empty));
                        break;

                    case FormFieldDataTypeEnum.Integer:
                    case FormFieldDataTypeEnum.LongInteger:
                        parentAccount.SetValue(key, ValidationHelper.GetInteger(text, 0));
                        break;

                    case FormFieldDataTypeEnum.Decimal:
                        parentAccount.SetValue(key, ValidationHelper.GetDouble(text, 0));
                        break;

                    default:
                        parentAccount.SetValue(key, text);
                        break;
                }
            }
        }
        AccountInfoProvider.SetAccountInfo(parentAccount);
    }


    /// <summary>
    /// Performs custom validation and displays error in top of the page.
    /// </summary>
    /// <returns>Returns true if validation is successful.</returns>
    private bool ValidateForm()
    {
        // Validate name
        string errorMessage = new Validator().NotEmpty(cmbAccountName.Text.Trim(), GetString("om.account.entername")).Result;
        if (!String.IsNullOrEmpty(errorMessage.ToString()))
        {
            lblError.Text = errorMessage;
            lblError.Visible = true;
            return false;
        }

        // Validates email
        if (!String.IsNullOrEmpty(cmbAccountEmail.Text) && !ValidationHelper.IsEmail(cmbAccountEmail.Text))
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


    /// <summary>
    /// Returns WHERE condition limiting account selector for subsidiaries.
    /// </summary>
    /// <param name="currentWhere">WHERE condition</param>
    /// <returns>Modified WHERE condition</returns>
    private string GetSubsidiaryWhere(string currentWhere)
    {
        foreach (int id in GetAccountIDs())
        {
            currentWhere = SqlHelperClass.AddWhereCondition(currentWhere, "AccountID NOT IN (SELECT * FROM Func_OM_Account_GetSubsidiaries(" + id + ", 1))");
        }

        return currentWhere;
    }


    /// <summary>
    /// Returns list of all merged account IDs including parent account ID separated by colon.
    /// </summary>
    /// <returns>String of IDs separated by colon</returns>
    private List<int> GetAccountIDs()
    {
        List<int> IDlist = new List<int>();
        IDlist.Add(parentAccount.AccountID);

        foreach (DataRow dr in mergedAccounts.Tables[0].Rows)
        {
            IDlist.Add(ValidationHelper.GetInteger(dr["AccountID"], 0));
        }

        return IDlist;
    }

    #endregion
}