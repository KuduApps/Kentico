using System;
using System.Web;

using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.DataEngine;

public partial class CMSModules_SystemTables_Pages_Development_Views_View_Edit : SiteManagerPage
{
    #region "Private variables"

    /// <summary>
    /// Type of database object (0 - view; 1 - stored procedure).
    /// </summary>
    private int objType = -1;

    string objName = null;

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        // Query param validation
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


        objName = HttpUtility.HtmlDecode(QueryHelper.GetString("objname", null));
        objType = QueryHelper.GetInteger("objtype", -1);

        // Check if edited view exists and redirect to error page if not
        if (!String.IsNullOrEmpty(objName) && !TableManager.ViewExists(objName))
        {
            EditedObject = null;
        }

        // Init edit area
        editSQL.ObjectName = objName;
        editSQL.HideSaveButton = objName != null;
        editSQL.IsView = true;
        editSQL.OnSaved += new EventHandler(editSQL_OnSaved);
        bool loadedCorrectly = true;
        if (!RequestHelper.IsPostBack())
        {
            loadedCorrectly = editSQL.SetupControl();
        }

        if (objName == null)
        {
            string[,] breadcrumbs = new string[2, 3];
            breadcrumbs[0, 0] = GetString("sysdev.views");
            breadcrumbs[0, 1] = "~/CMSModules/SystemTables/Pages/Development/Views/Views_List.aspx";
            breadcrumbs[0, 2] = "";
            breadcrumbs[1, 0] = GetString("general.new");
            breadcrumbs[1, 1] = "";
            breadcrumbs[1, 2] = "";

            this.CurrentMaster.Title.Breadcrumbs = breadcrumbs;
            this.CurrentMaster.Title.HelpName = "helpTopic";
            this.CurrentMaster.Title.HelpTopicName = "systemtables_views_new";
        }
        else
        {
            // Header actions
            string[,] actions = new string[2, 11];

            // Save button
            actions[0, 0] = HeaderActions.TYPE_SAVEBUTTON;
            actions[0, 1] = GetString("General.Save");
            actions[0, 5] = GetImageUrl("CMSModules/CMS_Content/EditMenu/" + (loadedCorrectly?"save.png":"savedisabled.png"));
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
        }
    }


    #region "Event handlers"

    void HeaderActions_ActionPerformed(object sender, System.Web.UI.WebControls.CommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "rollback":
                editSQL.Rollback();
                editSQL.SaveQuery();
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
            URLHelper.Redirect("~/CMSModules/SystemTables/Pages/Development/Views/ViewEdit_Frameset.aspx" + query);
        }

        lblInfo.Visible = true;
        lblInfo.Text = GetString("general.changessaved");
    }

    #endregion
}
