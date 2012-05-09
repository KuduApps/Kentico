using System;

using CMS.SettingsProvider;
using CMS.UIControls;

public partial class CMSModules_Friends_Controls_FriendsSearchBox : CMSUserControl
{
    #region "Variables"

    private bool mFilterComment = true;
    private bool mFilterNickname = true;
    private bool mFilterUserName = true;
    private bool mFilterFullName = true;

    #endregion


    #region "Properties"

    /// <summary>
    /// Gets where condition.
    /// </summary>
    public string WhereCondition
    {
        get
        {
            string where = null;
            if (!string.IsNullOrEmpty(txtSearch.Text))
            {
                if (FilterUserName)
                {
                    where = SqlHelperClass.AddWhereCondition(where, "UserName LIKE '%" + SqlHelperClass.GetSafeQueryString(txtSearch.Text, false) + "%'", "OR");
                }
                if (FilterNickname)
                {
                    where = SqlHelperClass.AddWhereCondition(where, "UserNickname LIKE '%" + SqlHelperClass.GetSafeQueryString(txtSearch.Text, false) + "%'", "OR");
                }
                if (FilterFullName)
                {
                    where = SqlHelperClass.AddWhereCondition(where, "FullName LIKE '%" + SqlHelperClass.GetSafeQueryString(txtSearch.Text, false) + "%'", "OR");
                }
                if (FilterComment)
                {
                    where = SqlHelperClass.AddWhereCondition(where, "FriendComment LIKE '%" + SqlHelperClass.GetSafeQueryString(txtSearch.Text, false) + "%'", "OR");
                }
            }
            return where;
        }
    }


    /// <summary>
    /// Determines whether filter is set.
    /// </summary>
    public bool FilterIsSet
    {
        get
        {
            return !string.IsNullOrEmpty(txtSearch.Text);
        }
    }


    /// <summary>
    /// Determines whether to filter Comment column.
    /// </summary>
    public bool FilterComment
    {
        get
        {
            return mFilterComment;
        }
        set
        {
            mFilterComment = value;
        }
    }


    /// <summary>
    /// Determines whether to filter Nickname column.
    /// </summary>
    public bool FilterNickname
    {
        get
        {
            return mFilterNickname;
        }
        set
        {
            mFilterNickname = value;
        }
    }


    /// <summary>
    /// Determines whether to filter Username column.
    /// </summary>
    public bool FilterUserName
    {
        get
        {
            return mFilterUserName;
        }
        set
        {
            mFilterUserName = value;
        }
    }


    /// <summary>
    /// Determines whether to filter Fullname column.
    /// </summary>
    public bool FilterFullName
    {
        get
        {
            return mFilterFullName;
        }
        set
        {
            mFilterFullName = value;
        }
    }

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {

    }


    protected void btnSearch_OnClick(object sender, EventArgs e)
    {
    }

    #endregion
}
