using System;

using CMS.Forums;
using CMS.CMSHelper;
using CMS.SettingsProvider;

public partial class CMSModules_Forums_Controls_Layouts_Flat_Forums : ForumViewer
{
    /// <summary>
    /// Load data.
    /// </summary>
    protected override void OnLoad(EventArgs e)
    {
        ReloadData();

        base.OnLoad(e);
    }


    /// <summary>
    /// Reloads the data of the forum control.
    /// </summary>
    public override void ReloadData()
    {
        base.ReloadData();

        ForumContext.GroupID = this.GroupID;

        // Create where condition reflecting permissions
        string where = "";

        // Show all forums for group admin
        if (this.HideForumForUnauthorized)
        {
            where = ForumInfoProvider.CombineSecurityWhereCondition("(ForumOpen = 1) AND (ForumGroupID = " + this.GroupID + ")", this.CommunityGroupID);
        }
        else
        {
            where = "(ForumOpen = 1) AND (ForumGroupID = " + this.GroupID + ")";
        }

        if (ForumContext.CurrentGroup != null)
        {
            listForums.OuterData = ForumContext.CurrentGroup;
            listForums.DataSource = ForumInfoProvider.GetForums(where, "ForumOrder ASC, ForumName ASC", 0, null);

            // Hide control for no forums found
            if (SqlHelperClass.DataSourceIsEmpty(listForums.DataSource))
            {
                this.Visible = false;
            }

            listForums.DataBind();
        }
        else
        {
            this.Visible = false;
        }
    }
}
