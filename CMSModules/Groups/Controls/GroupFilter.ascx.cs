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

using CMS.UIControls;
using CMS.SiteProvider;
using CMS.GlobalHelper;

public partial class CMSModules_Groups_Controls_GroupFilter : CMSUserControl
{
    #region "Variables"

    private string mWhereCondition = string.Empty;

    #endregion


    #region "Public properties"

    /// <summary>
    /// Where condition built unsig the filter options.
    /// </summary>
    public string WhereCondition
    {
        get
        {
            return mWhereCondition;
        }
    }

    #endregion


    #region "Events"
    

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!RequestHelper.IsPostBack())
        {
            InitializeForm();
        }

        // Perform search on page load (because of paging)
        BuildWhereCondition();
    }
   

    #endregion


    #region "Private methods"

    private void InitializeForm()
    {
        // Initialize first dropdown lists
        this.drpGroupName.Items.Add(new ListItem("LIKE", "0"));
        this.drpGroupName.Items.Add(new ListItem("NOT LIKE", "1"));
        this.drpGroupName.Items.Add(new ListItem("=", "2"));
        this.drpGroupName.Items.Add(new ListItem("<>", "3"));

        this.drpGroupStatus.Items.Add(new ListItem(GetString("general.selectall"), "0"));
        this.drpGroupStatus.Items.Add(new ListItem(GetString("groups.status.waitingforapproval"), "1"));
        this.drpGroupStatus.Items.Add(new ListItem(GetString("general.approved"), "2"));
        this.drpGroupStatus.Items.Add(new ListItem(GetString("general.rejected"), "3"));
        
        // Preselect all
        this.drpGroupStatus.SelectedIndex = 0;
    }


    /// <summary>
    /// Builds where condition.
    /// </summary>
    private void BuildWhereCondition()
    {
        // Group name
        string groupName = this.txtGroupName.Text.Trim().Replace("'", "''");
        if (!String.IsNullOrEmpty(groupName))
        {
            // Get proper operator name
            int sqlOperatorNumber = ValidationHelper.GetInteger(this.drpGroupName.SelectedValue, 0);
            string sqlOperatorName = "LIKE";
            switch (sqlOperatorNumber)
            {
                case 1:
                    sqlOperatorName = "NOT LIKE";
                    break;
                case 2:
                    sqlOperatorName = "=";
                    break;
                case 3:
                    sqlOperatorName = "<>";
                    break;
                default:
                    sqlOperatorName = "LIKE";
                    break;
            }

            if ((sqlOperatorName == "LIKE") || (sqlOperatorName == "NOT LIKE"))
            {
                groupName = "%" + groupName + "%";
            }

            this.mWhereCondition = "(GroupDisplayName " + sqlOperatorName + " N'" + groupName + "')";
        }

        // Group status
        int sqlStatusNumber = ValidationHelper.GetInteger(this.drpGroupStatus.SelectedValue, 0);
        string sqlStatusCode = "";
        switch (sqlStatusNumber)
        {
            case 1:
                sqlStatusCode = "GroupApproved IS NULL";
                break;
            case 2:
                sqlStatusCode = "GroupApproved = 1";
                break;
            case 3:
                sqlStatusCode = "GroupApproved = 0";
                break;
            default:
                sqlStatusCode = "";
                break;
        }

        if (!String.IsNullOrEmpty(sqlStatusCode))
        {
            if (!String.IsNullOrEmpty(this.mWhereCondition))
            {
                this.mWhereCondition += " AND ";
            }

            this.mWhereCondition += "(" + sqlStatusCode + ")";
        }        
    }

    #endregion
}
