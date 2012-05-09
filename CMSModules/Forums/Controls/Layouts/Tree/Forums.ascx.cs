using System;

using CMS.Forums;
using CMS.CMSHelper;

public partial class CMSModules_Forums_Controls_Layouts_Tree_Forums : ForumViewer
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
        if (this.HideForumForUnauthorized)
        {
            where = ForumInfoProvider.CombineSecurityWhereCondition("(ForumOpen = 1) AND (ForumGroupID = " + this.GroupID + ")", this.CommunityGroupID);
        }
        else
        {
            where = "(ForumOpen = 1) AND (ForumGroupID = " + this.GroupID + ")";
        }

        listForums.OuterData = ForumContext.CurrentGroup;
        listForums.DataSource = ForumInfoProvider.GetForums(where, "ForumOrder ASC, ForumName ASC", 0, null);
        listForums.DataBind();
    }
}
