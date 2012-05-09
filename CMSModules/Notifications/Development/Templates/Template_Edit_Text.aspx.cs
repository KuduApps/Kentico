using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.GlobalHelper;
using CMS.Notifications;
using CMS.UIControls;

public partial class CMSModules_Notifications_Development_Templates_Template_Edit_Text : CMSNotificationsPage
{
    #region "Variables"

    protected int templateID = 0;

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Get TemplateID
        templateID = QueryHelper.GetInteger("templateid", 0);

        if (templateID == 0)
        {
            return;
        }

        this.templateTextElem.TemplateID = templateID;

        // Show info message if changes were saved
        if (QueryHelper.GetInteger("saved", 0) == 1)
        {
            lblInfo.Visible = true;
            lblInfo.Text = GetString("general.changessaved");
        }

        if (this.templateTextElem.GatewayCount <= 10)
        {
            // Header actions
            string[,] actions = new string[1, 11];

            // Save button
            actions[0, 0] = HeaderActions.TYPE_SAVEBUTTON;
            actions[0, 1] = GetString("General.Save");
            actions[0, 5] = GetImageUrl("CMSModules/CMS_Content/EditMenu/save.png");
            actions[0, 6] = "save";
            actions[0, 8] = "true";

            CurrentMaster.HeaderActions.LinkCssClass = "ContentSaveLinkButton";
            CurrentMaster.HeaderActions.ActionPerformed += HeaderActions_ActionPerformed;

            if (this.templateTextElem.GatewayCount == 0)
            {
                actions[0, 5] = GetImageUrl("CMSModules/CMS_Content/EditMenu/savedisabled.png");
                actions[0, 9] = "false";
            }

            CurrentMaster.HeaderActions.Actions = actions;

            // Macros help
            this.plcMacros.Visible = true;
            this.lnkMoreMacros.Text = GetString("notification.template.text.helplnk");
            this.lblHelpHeader.Text = GetString("notification.template.text.helpheader");
            DisplayHelperTable();
        }

    }


    /// <summary>
    /// Actions handler.
    /// </summary>
    protected void HeaderActions_ActionPerformed(object sender, CommandEventArgs e)
    {
        switch (e.CommandName.ToLower())
        {
            case "save":
                this.templateTextElem.Save();
                URLHelper.Redirect("Template_Edit_Text.aspx?templateid=" + templateID + "&saved=1");
                break;
        }
    }

    #endregion


    #region "Private methods"

    /// <summary>
    /// Fills table holding additional macros.
    /// </summary>
    /// <param name="tableColumns">Data for table columns</param>
    private void FillHelperTable(string[,] tableColumns)
    {
        for (int i = 0; i <= tableColumns.GetUpperBound(0); i++)
        {
            TableRow tRow = new TableRow();
            TableCell leftCell = new TableCell();
            leftCell.Wrap = false;

            TableCell rightCell = new TableCell();

            Label lblExample = new Label();
            Label lblExplanation = new Label();

            // Init labels
            lblExample.Text = tableColumns[i, 0];
            lblExplanation.Text = tableColumns[i, 1];

            // Add labels to the cells
            leftCell.Controls.Add(lblExample);
            rightCell.Controls.Add(lblExplanation);

            leftCell.Width = new Unit(250);

            // Add cells to the row
            tRow.Cells.Add(leftCell);
            tRow.Cells.Add(rightCell);

            // Add row to the table
            tblHelp.Rows.Add(tRow);
        }
    }


    /// <summary>
    /// Displays helper table with makro examples.
    /// </summary>
    private void DisplayHelperTable()
    {
        // 0 - left column (example), 1 - right column (explanation)
        string[,] tableColumns = new string[5, 2];

        int i = 0;

        //transformation expression examples
        tableColumns[i, 0] = "{%notificationsubscription.SubscriptionID%}";
        tableColumns[i++, 1] = GetString("notification.template.macrohelp.Subscription");

        tableColumns[i, 0] = "{%notificationgateway.GatewayID%}";
        tableColumns[i++, 1] = GetString("notification.template.macrohelp.Gateway");

        tableColumns[i, 0] = "{%notificationuser.UserID%}";
        tableColumns[i++, 1] = GetString("notification.template.macrohelp.User");

        tableColumns[i, 0] = "{%notificationcustomdata.XXX%}";
        tableColumns[i++, 1] = GetString("notification.template.macrohelp.CustomData");

        tableColumns[i, 0] = "{%documentlink%}";
        tableColumns[i, 1] = GetString("notification.template.macrohelp.DocumentLink");

        FillHelperTable(tableColumns);
    }

    #endregion
}
