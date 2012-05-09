using System;

using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.UIControls;
using CMS.DataEngine;

public partial class CMSModules_SystemTables_Pages_Development_Views_Proc_Edit : SiteManagerPage
{
    #region "Private variables"

    private string objName = null;

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!QueryHelper.ValidateHash("hash"))
        {
            lblError.Visible = true;
            lblError.Text = GetString("sysdev.views.corruptedparameters");
            editSQL.Visible = false;
            return;
        }

        if (QueryHelper.GetInteger("saved", 0) == 1)
        {
            lblInfo.Visible = true;
            lblInfo.Text = GetString("general.changessaved");
        }

        objName = QueryHelper.GetString("objname", null);
        if (!String.IsNullOrEmpty(objName) && !TableManager.StoredProcedureExists(objName))
        {
            EditedObject = null;
        }

        // Init edit area
        editSQL.ObjectName = objName;
        editSQL.HideSaveButton = objName != null;
        editSQL.IsView = false;
        editSQL.OnSaved += new EventHandler(editSQL_OnSaved);
        bool loadedCorrectly = true;
        if (!RequestHelper.IsPostBack())
        {
            loadedCorrectly = editSQL.SetupControl();
        }

        string[,] breadcrumbs = new string[2, 3];
        breadcrumbs[0, 0] = (GetString("sysdev.procedures"));
        breadcrumbs[0, 1] = "~/CMSModules/SystemTables/Pages/Development/Views/Proc_List.aspx";
        breadcrumbs[0, 2] = "";
        breadcrumbs[1, 0] = (String.IsNullOrEmpty(objName) ? GetString("general.new") : objName);
        breadcrumbs[1, 1] = "";
        breadcrumbs[1, 2] = "";

        this.CurrentMaster.Title.Breadcrumbs = breadcrumbs;

        // Edit menu
        string[,] actions = new string[2, 11];

        if (objName != null)
        {
            // Save button
            actions[0, 0] = HeaderActions.TYPE_SAVEBUTTON;
            actions[0, 1] = GetString("General.Save");
            actions[0, 5] = GetImageUrl("CMSModules/CMS_Content/EditMenu/" + (loadedCorrectly ? "save.png" : "savedisabled.png"));
            actions[0, 6] = "save";
            actions[0, 8] = loadedCorrectly.ToString();
            actions[0, 9] = loadedCorrectly.ToString();

            // Undo button
            actions[1, 0] = HeaderActions.TYPE_SAVEBUTTON;
            actions[1, 1] = GetString("General.Rollback");
            actions[1, 5] = GetImageUrl("CMSModules/CMS_Content/EditMenu/undo.png");
            actions[1, 6] = "rollback";
            actions[1, 10] = editSQL.RollbackAvailable.ToString();

            this.CurrentMaster.HeaderActions.Actions = actions;
            this.CurrentMaster.HeaderActions.ActionPerformed += new System.Web.UI.WebControls.CommandEventHandler(HeaderActions_ActionPerformed);
            this.CurrentMaster.Title.HelpName = "helpTopic";
            this.CurrentMaster.Title.HelpTopicName = "systemtables_procs_edit";
        }
        else
        {
            this.CurrentMaster.Title.HelpName = "helpTopic";
            this.CurrentMaster.Title.HelpTopicName = "systemtables_procs_new";
        }
    }


    #region "Event handlers"

    void HeaderActions_ActionPerformed(object sender, System.Web.UI.WebControls.CommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "rollback":
                editSQL.Rollback();
                break;
            case "save":
                editSQL.SaveQuery();
                break;
        }
    }


    void editSQL_OnSaved(object sender, EventArgs e)
    {
        if (objName == null)
        {
            string query = "?objname=" + editSQL.ObjectName + "&saved=1";
            query += "&hash=" + QueryHelper.GetHash(query);
            URLHelper.Redirect("~/CMSModules/SystemTables/Pages/Development/Views/Proc_Edit.aspx" + query);
        }

        lblInfo.Visible = true;
        lblInfo.Text = GetString("general.changessaved");
    }

    #endregion
}
