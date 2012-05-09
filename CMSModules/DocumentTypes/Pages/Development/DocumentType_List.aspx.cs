using System;
using System.Web.UI.WebControls;

using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.DataEngine;
using CMS.UIControls;
using CMS.EventLog;
using CMS.IO;


public partial class CMSModules_DocumentTypes_Pages_Development_DocumentType_List : SiteManagerPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        RegisterExportScript();

        CurrentMaster.Title.TitleText = GetString("DocumentType_List.Title");
        CurrentMaster.Title.TitleImage = GetImageUrl("Objects/CMS_DocumentType/object.png");
        CurrentMaster.Title.HelpTopicName = "document_types_list";
        CurrentMaster.Title.HelpName = "helpTopic";

        // New item link
        string[,] actions = new string[1, 6];
        actions[0, 0] = HeaderActions.TYPE_HYPERLINK;
        actions[0, 1] = GetString("DocumentType_List.NewDoctype");
        actions[0, 2] = null;
        actions[0, 3] = ResolveUrl("DocumentType_New.aspx");
        actions[0, 4] = null;
        actions[0, 5] = GetImageUrl("Objects/CMS_DocumentType/add.png");
        CurrentMaster.HeaderActions.Actions = actions;

        // Unigrid initialization
        uniGrid.OnAction += uniGrid_OnAction;
        uniGrid.ZeroRowsText = GetString("general.nodatafound");
    }


    /// <summary>
    /// Handles the UniGrid's OnAction event.
    /// </summary>
    /// <param name="actionName">Name of item (button) that throws event</param>
    /// <param name="actionArgument">ID (value of Primary key) of corresponding data row</param>
    protected void uniGrid_OnAction(string actionName, object actionArgument)
    {
        if (actionName == "edit")
        {
            URLHelper.Redirect("DocumentType_Edit.aspx?documenttypeid=" + actionArgument);
        }
        else if (actionName == "delete")
        {
            int classId = ValidationHelper.GetInteger(actionArgument, 0);

            DataClassInfo dci = DataClassInfoProvider.GetDataClass(classId);

            if (dci != null)
            {
                // Check unerasable dependences
                if (DataClassInfoProvider.CheckDependencies(dci.ClassID))
                {
                    lblError.Text = string.Format(GetString("DocumentType_List.Dependences"), dci.ClassName);
                    lblError.Visible = true;
                }
                else
                {
                    // Delete dataclass and its dependeces
                    try
                    {
                        string className = dci.ClassName;
                        DataClassInfoProvider.DeleteDataClass(dci);

                        // Delete view
                        string viewName = DataHelper.GetViewName(dci.ClassTableName, null);
                        TableManager.DropView(viewName);

                        // Delete icons
                        string iconFile = GetDocumentTypeIconUrl(className, false);
                        string iconLargeFile = GetDocumentTypeIconUrl(className, "48x48", false);
                        iconFile = Server.MapPath(iconFile);
                        iconLargeFile = Server.MapPath(iconLargeFile);

                        if (File.Exists(iconFile))
                        {
                            File.Delete(iconFile);
                        }
                        // Ensure that ".gif" file will be deleted
                        iconFile = iconFile.Replace(".png", ".gif");

                        if (File.Exists(iconFile))
                        {
                            File.Delete(iconFile);
                        }

                        if (File.Exists(iconLargeFile))
                        {
                            File.Delete(iconLargeFile);
                        }
                        // Ensure that ".gif" file will be deleted
                        iconLargeFile = iconLargeFile.Replace(".png", ".gif");
                        if (File.Exists(iconLargeFile))
                        {
                            File.Delete(iconLargeFile);
                        }
                    }
                    catch (Exception ex)
                    {
                        EventLogProvider ev = new EventLogProvider();
                        ev.LogEvent("Development", "DeleteDocType", ex);
                        lblError.Text = GetString("DocumentType_List.DeleteFailed") + " " + ex.Message;
                        lblError.Visible = true;
                    }
                }
            }
        }
    }
}
