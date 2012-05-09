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

using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.SiteProvider;
using CMS.UIControls;
using CMS.FormControls;
using CMS.CMSHelper;
using CMS.IO;

public partial class CMSModules_Membership_Controls_Users_UserFilter : CMSUserControl
{
    #region "Variables"

    bool showGroups = false;
    private const string pathToGroupselector = "~/CMSModules/Groups/FormControls/MembershipGroupSelector.ascx";
    private string mAlphabetSeparator = "";

    private int mSiteId = 0;

    private bool isAdvancedMode = false;
    private Hashtable alphabetHash = new Hashtable();
    FormEngineUserControl selectInGroups = null;
    FormEngineUserControl selectNotInGroups = null;

    #endregion


    #region "Public properties"

    /// <summary>
    /// Gets or sets the site id for which the users should be filtered.
    /// </summary>
    public int SiteID
    {
        get
        {
            return this.mSiteId;
        }
        set
        {
            this.mSiteId = value;
        }
    }


    /// <summary>
    /// Gets or sets the visibility of alphabet panel.
    /// </summary>
    public bool AlphabetVisible
    {
        get
        {
            return this.pnlAlphabet.Visible;
        }
        set
        {
            this.pnlAlphabet.Visible = value;
        }
    }


    /// <summary>
    /// Gets or sets the alphabet separator.
    /// </summary>
    public string AlphabetSeparator
    {
        get
        {
            return this.mAlphabetSeparator;
        }
        set
        {
            this.mAlphabetSeparator = value;
        }
    }


    /// <summary>
    /// Gets the where condition created using filtered parameters.
    /// </summary>
    public string WhereCondition
    {
        get
        {
            return GenerateWhereCondition();
        }
    }

    #endregion


    #region "Page methods"

    protected override void OnInit(EventArgs e)
    {
        if (File.Exists(HttpContext.Current.Request.MapPath(ResolveUrl(pathToGroupselector))))
        {
            Control ctrl = this.LoadControl(pathToGroupselector);
            if (ctrl != null)
            {
                selectInGroups = ctrl as FormEngineUserControl;
                ctrl.ID = "selGroups";
                ctrl = this.LoadControl(pathToGroupselector);
                selectNotInGroups = ctrl as FormEngineUserControl;
                ctrl.ID = "selNoGroups";

                this.plcGroups.Visible = true;
                plcSelectInGroups.Controls.Add(selectInGroups);
                plcSelectNotInGroups.Controls.Add(selectNotInGroups);

                this.selectNotInGroups.SetValue("UseFriendlyMode", true);
                this.selectInGroups.IsLiveSite = false;
                this.selectInGroups.SetValue("UseFriendlyMode", true);
                this.selectNotInGroups.IsLiveSite = false;

            }
        }

        base.OnInit(e);
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        // Check "read" permission
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.Users", "Read"))
        {
            RedirectToAccessDenied("CMS.Users", "Read");
        }

        InitializeForm();

        // Show alphaphet filter if enabled
        if (this.pnlAlphabet.Visible)
        {
            this.pnlAlphabet.Controls.Add(CreateAlphabetTable());
        }

        // Show correct filter panel
        EnsureFilterMode();
        pnlAdvancedFilter.Visible = isAdvancedMode;
        pnlSimpleFilter.Visible = !isAdvancedMode;

        // Show group filter only if enabled
        if (this.mSiteId > 0)
        {
            SiteInfo si = SiteInfoProvider.GetSiteInfo(this.mSiteId);
            if ((si != null) && isAdvancedMode)
            {
                showGroups = ModuleCommands.CommunitySiteHasGroup(si.SiteID);
            }
        }

        // Setup role selector
        this.selectNotInRole.SiteID = this.mSiteId;
        this.selectRoleElem.SiteID = this.mSiteId;
        this.selectRoleElem.CurrentSelector.ResourcePrefix = "addroles";
        this.selectNotInRole.CurrentSelector.ResourcePrefix = "addroles";
        this.selectRoleElem.UseFriendlyMode = true;
        this.selectNotInRole.UseFriendlyMode = true;

        // Setup groups selectors
        plcGroups.Visible = showGroups;
        if (selectInGroups != null)
        {
            selectInGroups.StopProcessing = !showGroups;
            this.selectInGroups.FormControlParameter = this.mSiteId;
        }

        if (selectNotInGroups != null)
        {
            selectNotInGroups.StopProcessing = !showGroups;
            this.selectNotInGroups.FormControlParameter = this.mSiteId;
        }
    }

    #endregion


    #region "UI methods"

    /// <summary>
    /// Initializes items in "all/any" dropdown list
    /// </summary>
    /// <param name="drp">Dropdown list to initialize</param>
    private void InitAllAnyDropDown(DropDownList drp)
    {
        if (drp.Items.Count <= 0)
        {
            drp.Items.Add(new ListItem(GetString("General.selectall"), "ALL"));
            drp.Items.Add(new ListItem(GetString("General.Any"), "ANY"));
        }
    }


    /// <summary>
    /// Initializes the layout of the form.
    /// </summary>
    private void InitializeForm()
    {
        // General UI
        this.btnSimpleSearch.Text = GetString("General.Search");
        this.btnAdvancedSearch.Text = GetString("General.Search");
        this.lnkShowAdvancedFilter.Text = GetString("user.filter.showadvanced");
        this.imgShowAdvancedFilter.ImageUrl = GetImageUrl("Design/Controls/UniGrid/Actions/SortDown.png");
        this.lnkShowSimpleFilter.Text = GetString("user.filter.showsimple");
        this.imgShowSimpleFilter.ImageUrl = GetImageUrl("Design/Controls/UniGrid/Actions/SortUp.png");
        this.pnlSimpleFilter.Visible = !isAdvancedMode;
        this.pnlAdvancedFilter.Visible = isAdvancedMode;

        // Initialize advanced filter dropdownlists
        if (!RequestHelper.IsPostBack())
        {
            InitAllAnyDropDown(drpTypeSelectInRoles);
            InitAllAnyDropDown(drpTypeSelectNotInRoles);
            InitAllAnyDropDown(drpTypeSelectInGroups);
            InitAllAnyDropDown(drpTypeSelectNotInGroups);
        }

        // Labels
        this.lblFullName.Text = GetString("general.FullName") + ResHelper.Colon;
        this.lblNickName.Text = GetString("userlist.NickName") + ResHelper.Colon;
        this.lblEmail.Text = GetString("userlist.Email") + ResHelper.Colon;
        this.lblInRoles.Text = GetString("userlist.InRoles") + ResHelper.Colon;
        this.lblNotInRoles.Text = GetString("userlist.NotInRoles") + ResHelper.Colon;
        this.lblInGroups.Text = GetString("userlist.InGroups") + ResHelper.Colon;
        this.lblNotInGroups.Text = GetString("userlist.NotInGroups") + ResHelper.Colon;
    }


    /// <summary>
    /// Creates table element with alphabet.
    /// </summary>
    private Table CreateAlphabetTable()
    {
        // Register javascript for alphabet filtering
        string postbackScript = Page.ClientScript.GetPostBackEventReference(this, null);
        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "alphabetSelect", ScriptHelper.GetScript(
            @"function SetAlphabetLetter(letter) {
                var hiddenElem = document.getElementById('" + hdnAlpha.ClientID + @"');
                if (hiddenElem != null) {
                    hiddenElem.value = letter;
                    " + postbackScript + @";
                }
                return false;
              }"));

        Table table = new Table();
        table.EnableViewState = false;
        table.Attributes.Add("style", "width: 100%");
        table.Rows.Add(new TableRow());

        TableCell cell = new TableCell();
        HyperLink link = new HyperLink();

        // Create "ALL" link
        link.NavigateUrl = "javascript:SetAlphabetLetter('ALL');";
        link.Text = GetString("general.all");

        cell.Controls.Add(link);
        table.Rows[0].Cells.Add(cell);
        alphabetHash["ALL"] = link;

        // Create alphabet links
        for (char c = 'A'; c <= 'Z'; c++)
        {
            // Add separator            
            //TableCell cellSep = new TableCell();
            //cellSep.Controls.Add(new LiteralControl(this.mAlphabetSeparator));
            //table.Rows[0].Cells.Add(cellSep);

            // Add link
            cell = new TableCell();
            link = new HyperLink();
            table.Rows[0].Cells.Add(cell);

            link.NavigateUrl = "javascript:SetAlphabetLetter('" + c + "');";

            link.Text = c.ToString();
            cell.Controls.Add(link);
            table.Rows[0].Cells.Add(cell);

            // Add to hashtable
            alphabetHash[c.ToString()] = link;
        }

        // If something is already selected, hightlight the letter, otherwise all
        string key = String.IsNullOrEmpty(hdnAlpha.Value) ? "ALL" : hdnAlpha.Value;
        link = alphabetHash[key] as HyperLink;
        if (link != null)
        {
            link.CssClass = "ActiveLink";
        }

        return table;
    }


    /// <summary>
    /// Ensures correct filter mode flag if filter mode was just changed.
    /// </summary>
    private void EnsureFilterMode()
    {
        if (URLHelper.IsPostback())
        {
            // Get current event target
            string uniqieId = ValidationHelper.GetString(Request.Params["__EVENTTARGET"], String.Empty);
            uniqieId = uniqieId.Replace("$", "_");

            // If postback was fired by mode switch, update isAdvancedMode variable
            if (uniqieId == lnkShowAdvancedFilter.ClientID)
            {
                isAdvancedMode = true;
            }
            else if (uniqieId == lnkShowSimpleFilter.ClientID)
            {
                isAdvancedMode = false;
            }
            else
            {
                isAdvancedMode = ValidationHelper.GetBoolean(ViewState["IsAdvancedMode"], false);
            }
        }
    }


    /// <summary>
    /// Sets the advanced mode.
    /// </summary>
    protected void lnkShowAdvancedFilter_Click(object sender, EventArgs e)
    {
        isAdvancedMode = true;
        ViewState["IsAdvancedMode"] = isAdvancedMode;
        this.pnlSimpleFilter.Visible = !isAdvancedMode;
        this.pnlAdvancedFilter.Visible = isAdvancedMode;
    }


    /// <summary>
    /// Sets the simple mode.
    /// </summary>
    protected void lnkShowSimpleFilter_Click(object sender, EventArgs e)
    {
        isAdvancedMode = false;
        ViewState["IsAdvancedMode"] = isAdvancedMode;
        this.pnlSimpleFilter.Visible = !isAdvancedMode;
        this.pnlAdvancedFilter.Visible = isAdvancedMode;
    }

    #endregion


    #region "Search methods - where condition"

    /// <summary>
    /// Generates complete filter where condition.
    /// </summary>    
    private string GenerateWhereCondition()
    {
        // Get mode from viewstate
        EnsureFilterMode();

        string whereCond = null;

        // Create first where condition depending on mode
        if (isAdvancedMode)
        {
            whereCond = AdvancedSearch();
        }
        else
        {
            whereCond = SimpleSearch();
        }

        // Append site condition if siteid given.
        if (this.mSiteId > 0)
        {
            // Get site related condition
            if (this.mSiteId != 0)
            {
                whereCond += (String.IsNullOrEmpty(whereCond) ? "" : " AND ") + "(UserID IN (SELECT UserID FROM CMS_UserSite WHERE SiteID=" + this.mSiteId + "))";
            }
        }

        return whereCond;
    }

    /// <summary>
    /// Generates where condition for advanced filter.
    /// </summary>
    public string AdvancedSearch()
    {
        // Get condition parts
        string roleWhere = GetMultipleSelectorCondition(drpTypeSelectInRoles.SelectedValue, "roles", selectRoleElem.Value.ToString().Trim(), false);
        string roleWhereNot = GetMultipleSelectorCondition(drpTypeSelectNotInRoles.SelectedValue, "roles", selectNotInRole.Value.ToString().Trim(), true);

        string groupWhere = (showGroups) ? GetMultipleSelectorCondition(drpTypeSelectInGroups.SelectedValue, "groups", selectInGroups.Value.ToString().Trim(), false) : "";
        string groupWhereNot = (showGroups) ? GetMultipleSelectorCondition(drpTypeSelectNotInGroups.SelectedValue, "groups", selectNotInGroups.Value.ToString().Trim(), true) : "";

        // Join where conditions
        if ((roleWhere != "") && (roleWhereNot != ""))
        {
            roleWhere = roleWhere + " AND " + roleWhereNot;
        }
        else
        {
            roleWhere = roleWhere + roleWhereNot;
        }

        string whereCond = "";
        whereCond = SqlHelperClass.AddWhereCondition(whereCond, fltUserName.GetCondition());
        whereCond = SqlHelperClass.AddWhereCondition(whereCond, fltFullName.GetCondition());
        whereCond = SqlHelperClass.AddWhereCondition(whereCond, fltEmail.GetCondition());
        whereCond = SqlHelperClass.AddWhereCondition(whereCond, fltNickName.GetCondition());
        whereCond = SqlHelperClass.AddWhereCondition(whereCond, roleWhere);
        whereCond = SqlHelperClass.AddWhereCondition(whereCond, roleWhereNot);
        whereCond = SqlHelperClass.AddWhereCondition(whereCond, groupWhere);
        whereCond = SqlHelperClass.AddWhereCondition(whereCond, groupWhereNot);

        // Starting letter for username
        string firstLetter = hdnAlpha.Value;
        if (firstLetter != "" && firstLetter != "ALL")
        {
            whereCond += (String.IsNullOrEmpty(whereCond) ? "" : " AND ") + "(UserName LIKE N'" + SqlHelperClass.GetSafeQueryString(firstLetter, false) + "%')";
        }

        return whereCond;
    }


    /// <summary>
    /// Returns where condition for specialized role and group conditions.
    /// </summary>
    /// <param name="op">Condition to use (ANY/ALL)</param>
    /// <param name="type">Type of condition to create (roles,groups)</param>
    /// <param name="valuesStr">Values separated with semicolon</param>    
    /// <param name="negate">If true add negation to where condition (NOT)</param>    
    private string GetMultipleSelectorCondition(string op, string type, string valuesStr, bool negate)
    {
        string retval = String.Empty;
        if (!String.IsNullOrEmpty(valuesStr))
        {
            string itemsWhere = String.Empty;
            string having = String.Empty;

            string[] items = valuesStr.Split(';');
            string not = negate ? "NOT" : String.Empty;

            switch (type.ToLower())
            {
                case "roles":
                    // Create where condition for roles
                    // Global roles start with prefix '.'                    
                    StringBuilder sbSite = new StringBuilder();
                    StringBuilder sbGlobal = new StringBuilder();

                    // First split both groups (of roles) to different string builders 
                    foreach (String item in items)
                    {
                        if (item.StartsWith("."))
                        {
                            sbGlobal.Append(",'", item.TrimStart('.').Replace("'", "''"), "'");
                        }
                        else
                        {
                            sbSite.Append(",'", item.Replace("'", "''"), "'");
                        }
                    }

                    // Convert builders to string
                    String siteItem = sbSite.ToString().Trim(',');
                    String globalItem = sbGlobal.ToString().Trim(',');

                    // Create where condition for site roles. Empty string if no site role selected
                    String siteWhere = (siteItem != String.Empty) ? "RoleName IN (" + siteItem + " )" : String.Empty;

                    // Create global roles where condition. Only if user selects any global roels
                    String globalWhere = (globalItem != String.Empty) ? " (RoleName IN (" + globalItem + ") AND SiteID IS NULL ) " : String.Empty;

                    // If user selected both site and global roles add 'OR' between these two conditions
                    if ((globalItem != String.Empty) && (siteWhere != String.Empty))
                    {
                        siteWhere += " OR ";
                    }                                       

                    having = (op.ToLower() == "all") ? "HAVING COUNT(RoleID) = " + items.Length : String.Empty;

                    // Select users assigned to roles by given names (no matter what site) or user assigned to global role (only global roles accepted)                                       
                    retval = " UserID " + not + @" IN (SELECT UserID FROM CMS_UserRole
                                                WHERE RoleID IN (SELECT RoleID FROM CMS_Role WHERE " + siteWhere + globalWhere + @")  AND
                                                    (ValidTo IS NULL OR ValidTo > @Now)
                                                GROUP BY UserID " + having + ")";
                    break;

                case "groups":
                    having = (op.ToLower() == "all") ? "HAVING COUNT (MemberGroupID) =" + items.Length : String.Empty;
                    itemsWhere = SqlHelperClass.GetWhereCondition("GroupName", items);
                    retval = " UserID " + not + @" IN (SELECT MemberUserID FROM Community_GroupMember 
                                         WHERE MemberGroupID IN (SELECT GroupID FROM Community_Group WHERE " + itemsWhere + @")
                                         GROUP BY MemberUserID " + having + ")";
                    break;
            }

        }
        return retval;
    }



    /// <summary>
    /// Generates where condition for simple filter.
    /// </summary>    
    public string SimpleSearch()
    {
        string where = String.Empty;
        string searchExpression = null;
        string queryOperator = "LIKE";

        if (txtSearch.Text != String.Empty)
        {
            // Create skeleton of where condition (ensure also site and starting letter)
            where = "((UserName {0} N'{1}') OR (Email {0} N'{1}') OR (FullName {0} N'{1}') OR (UserNickName {0} N'{1}'))";

            // Avoid SQL Injenction
            searchExpression = txtSearch.Text.Trim().Replace("'", "''");

            // Choose the operator (if surrounded with quotes use '=' operator instead of LIKE)            
            if (searchExpression.StartsWith("\"") && searchExpression.EndsWith("\""))
            {
                queryOperator = "=";

                // Remove quotes
                searchExpression = searchExpression.Substring(1, searchExpression.Length - 2);
            }
            else
            {
                searchExpression = "%" + searchExpression + "%";
            }
        }

        // Starting letter for username
        string firstLetter = hdnAlpha.Value;
        if (firstLetter != "" && firstLetter != "ALL")
        {
            if (!String.IsNullOrEmpty(where))
            {
                where += "AND ";
            }
            where += "(UserName LIKE N'" + SqlHelperClass.GetSafeQueryString(firstLetter, false) + "%')";
        }

        // Get final where condition
        return String.Format(where, queryOperator, searchExpression);
    }

    #endregion
}
