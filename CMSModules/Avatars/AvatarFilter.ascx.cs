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
using CMS.SettingsProvider;

public partial class CMSModules_Avatars_AvatarFilter : CMSUserControl
{
    #region "Variables"

    private string mWhereCondition = string.Empty;

    #endregion


    #region "Public properties"

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

        BuildWhereCondition();
    }

    #endregion


    #region "Private methods"

    private void InitializeForm()
    {
        // Initialize first dropdown lists
        this.drpAvatarName.Items.Add(new ListItem("LIKE", "0"));
        this.drpAvatarName.Items.Add(new ListItem("NOT LIKE", "1"));
        this.drpAvatarName.Items.Add(new ListItem("=", "2"));
        this.drpAvatarName.Items.Add(new ListItem("<>", "3"));

        DataHelper.FillListControlWithEnum(typeof(AvatarTypeEnum), drpAvatarType, "avat.type", AvatarInfoProvider.GetAvatarTypeString);
        this.drpAvatarType.Items.Insert(0, new ListItem(GetString("general.selectall"), ""));

        this.drpAvatarKind.Items.Add(new ListItem(GetString("general.selectall"), "0"));
        this.drpAvatarKind.Items.Add(new ListItem(GetString("avat.filter.shared"), "1"));
        this.drpAvatarKind.Items.Add(new ListItem(GetString("avat.filter.custom"), "2"));
        // Preselect shared
        this.drpAvatarKind.SelectedIndex = 1;
    }


    /// <summary>
    /// Builds where condition and raises search event.
    /// </summary>
    private void BuildWhereCondition()
    {
        // Avatar name
        string avatarName = this.txtAvatarName.Text.Trim().Replace("'", "''");
        if (!String.IsNullOrEmpty(avatarName))
        {
            // Get proper operator name
            int sqlOperatorNumber = ValidationHelper.GetInteger(this.drpAvatarName.SelectedValue, 0);
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
                avatarName = "%" + avatarName + "%";
            }

            this.mWhereCondition = "(AvatarName " + sqlOperatorName + " N'" + avatarName + "')";
        }

        // Avatar type
        if (!String.IsNullOrEmpty(this.drpAvatarType.SelectedValue))
        {
            if (!string.IsNullOrEmpty(this.mWhereCondition))
            {
                this.mWhereCondition += " AND ";
            }
            this.mWhereCondition += "(AvatarType = '" + SqlHelperClass.GetSafeQueryString(this.drpAvatarType.SelectedValue, false) + "')";
        }

        // Avatar kind
        int sqlKindNumber = ValidationHelper.GetInteger(this.drpAvatarKind.SelectedValue, 0);
        string sqlKindCode = "";
        switch (sqlKindNumber)
        {
            case 1:
                sqlKindCode = "AvatarIsCustom = 0";
                break;
            case 2:
                sqlKindCode = "AvatarIsCustom = 1";
                break;
            default:
                sqlKindCode = "";
                break;
        }

        if (!String.IsNullOrEmpty(sqlKindCode))
        {
            if (!String.IsNullOrEmpty(this.mWhereCondition))
            {
                this.mWhereCondition += " AND ";
            }
            this.mWhereCondition += "(" + sqlKindCode + ")";
        }
    }

    #endregion
}
