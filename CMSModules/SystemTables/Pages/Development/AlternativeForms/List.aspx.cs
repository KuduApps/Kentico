using System;

using CMS.FormEngine;
using CMS.GlobalHelper;
using CMS.UIControls;

public partial class CMSModules_SystemTables_Pages_Development_AlternativeForms_List : SiteManagerPage
{
    private int classId;


    protected void Page_Load(object sender, EventArgs e)
    {
        classId = QueryHelper.GetInteger("classid", 0);

        // Init alternative forms listing
        listElem.FormClassID = classId;
        listElem.OnEdit += listElem_OnEdit;
        listElem.OnDelete += listElem_OnDelete;

        // New item link
        string[,] actions = new string[1, 6];
        actions[0, 0] = HeaderActions.TYPE_HYPERLINK;
        actions[0, 1] = GetString("altforms.newformlink");
        actions[0, 2] = null;
        actions[0, 3] = ResolveUrl("New.aspx?classid=" + classId.ToString());
        actions[0, 4] = null;
        actions[0, 5] = GetImageUrl("Objects/CMS_AlternativeForm/add.png");
        this.CurrentMaster.HeaderActions.Actions = actions;
    }


    void listElem_OnEdit(object sender, object actionArgument)
    {
        URLHelper.Redirect("Frameset.aspx?classid=" + classId.ToString() + "&altformid=" + ValidationHelper.GetInteger(actionArgument, 0));
    }


    void listElem_OnDelete(object sender, object actionArgument)
    {
        AlternativeFormInfoProvider.DeleteAlternativeFormInfo(ValidationHelper.GetInteger(actionArgument, 0));
    }
}