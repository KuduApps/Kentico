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

public partial class CMSModules_Groups_Controls_Members_MemberFilter : CMSUserControl
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
    
    /// <summary>
    /// Page_load event.
    /// </summary>
    /// <param name="sender">Sender</param>
    /// <param name="e">Arguments</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        InitializeForm();
        BuildWhereCondition();
    }
    
    #endregion


    #region "Private methods"

    private void InitializeForm()
    {
        if (this.drpMemberName.Items.Count <= 0)
        {
            this.drpMemberName.Items.Add(new ListItem("LIKE", "0"));
            this.drpMemberName.Items.Add(new ListItem("NOT LIKE", "1"));
            this.drpMemberName.Items.Add(new ListItem("=", "2"));
            this.drpMemberName.Items.Add(new ListItem("<>", "3"));
        }

        if (this.drpMemberStatus.Items.Count <= 0)
        {
            this.drpMemberStatus.Items.Add(new ListItem(GetString("general.selectall"), "0"));
            this.drpMemberStatus.Items.Add(new ListItem(GetString("groups.status.waitingforapproval"), "1"));
            this.drpMemberStatus.Items.Add(new ListItem(GetString("general.approved"), "2"));
            this.drpMemberStatus.Items.Add(new ListItem(GetString("general.rejected"), "3"));
            
            // Preselect all
            this.drpMemberStatus.SelectedIndex = 0;
        }       
        
    }


    /// <summary>
    /// Builds where condition and raises search event.
    /// </summary>
    private void BuildWhereCondition()
    {
        // Member name
        string memberName = this.txtMemberName.Text.Trim().Replace("'", "''");
        if (!String.IsNullOrEmpty(memberName))
        {
            // Get proper operator name
            int sqlOperatorNumber = ValidationHelper.GetInteger(this.drpMemberName.SelectedValue, 0);
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
                memberName = "%" + memberName + "%";
            }

            this.mWhereCondition = "(MemberUserID IN (SELECT UserID FROM CMS_User WHERE UserName " + sqlOperatorName + " N'" + memberName + "'))";
        }

        // Member status
        int sqlStatusNumber = ValidationHelper.GetInteger(this.drpMemberStatus.SelectedValue, 0);
        string sqlStatusCode = "";
        switch (sqlStatusNumber)
        {
            case 1:
                sqlStatusCode = "MemberStatus = 2";
                break;
            case 2:
                sqlStatusCode = "MemberStatus = 0";
                break;
            case 3:
                sqlStatusCode = "MemberStatus = 1";
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
