using System;
using System.Data;
using System.Web.UI.WebControls;

using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.UIControls;
using CMS.Messaging;
using CMS.SiteProvider;

public partial class CMSModules_Messaging_Controls_SelectFromContactList : CMSUserControl
{
    #region "Public properties"

    /// <summary>
    /// Zero rows text.
    /// </summary>
    public string ZeroRowsText
    {
        get
        {
            EnsureChildControls();
            return gridContactList.ZeroRowsText;
        }
        set
        {
            EnsureChildControls();
            gridContactList.ZeroRowsText = value;
        }
    }


    /// <summary>
    /// Page size values separated with comma.
    /// </summary>
    public string PageSize
    {
        get
        {
            EnsureChildControls();
            return gridContactList.PageSize;
        }
        set
        {
            EnsureChildControls();
            gridContactList.PageSize = value;
        }
    }

    #endregion


    #region "Page events"

    protected override void EnsureChildControls()
    {
        base.EnsureChildControls();
        if (gridContactList == null)
        {
            pnlContactList.LoadContainer();
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (StopProcessing)
        {
            // Stop processing
        }
        else
        {
            // Content is visible only for authenticated users
            if (CMSContext.CurrentUser.IsAuthenticated())
            {
                if (string.IsNullOrEmpty(ZeroRowsText))
                {
                    ZeroRowsText = GetString("messaging.contactlist.nodatafound");
                }

                // Register modal dialog JS function
                ScriptHelper.RegisterDialogScript(this.Page);

                // Setup ungrid
                gridContactList.IsLiveSite = IsLiveSite;
                gridContactList.OnDataReload += new OnDataReloadEventHandler(gridContactList_OnDataReload);
                gridContactList.OnAction += gridContactList_OnAction;
                gridContactList.OnExternalDataBound += gridContactList_OnExternalDataBound;
            }
            else
            {
                Visible = false;
            }
        }
    }

    #endregion


    #region "Grid methods"

    protected object gridContactList_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        switch (sourceName)
        {
            case "formattedusername":
                DataRowView drv = parameter as DataRowView;
                int userId = ValidationHelper.GetInteger(drv["ContactListContactUserID"], 0);
                return GetItemText(userId, drv["UserName"], drv["FullName"], drv["UserNickName"]);
        }
        return parameter;
    }


    protected void gridContactList_OnAction(string actionName, object actionArgument)
    {
        switch (actionName)
        {
            case "delete":
                int deletedUserId = ValidationHelper.GetInteger(actionArgument, 0);

                // If something is wrong return
                if (CMSContext.CurrentUser == null)
                {
                    return;
                }

                try
                {
                    // Deletes from contact list
                    ContactListInfoProvider.RemoveFromContactList(CMSContext.CurrentUser.UserID, deletedUserId);
                    pnlInfo.Visible = true;
                    lblInfo.Text = GetString("Messaging.ContactList.DeleteSuccessful");
                }
                catch (Exception ex)
                {
                    pnlInfo.Visible = true;
                    lblError.Text = ex.Message;
                }
                break;
        }
    }


    protected DataSet gridContactList_OnDataReload(string completeWhere, string currentOrder, int currentTopN, string columns, int currentOffset, int currentPageSize, ref int totalRecords)
    {
        return ContactListInfoProvider.GetContactList(CMSContext.CurrentUser.UserID, completeWhere, currentOrder, currentTopN, "UserName, UserNickname, FullName, ContactListContactUserID", currentOffset, currentPageSize, ref totalRecords);
    }


    /// <summary>
    /// Renders row item according to control settings.
    /// </summary>
    protected string GetItemText(int userId, object username, object fullname, object usernickname)
    {
        string usrName = ValidationHelper.GetString(username, string.Empty);
        string nick = HTMLHelper.HTMLEncode(Functions.GetFormattedUserName(usrName, fullname.ToString(), usernickname.ToString(), IsLiveSite));

        return "<a href=\"javascript: window.parent.CloseAndRefresh(" + userId + ", " + ScriptHelper.GetString(usrName) + ", " +
            ScriptHelper.GetString(QueryHelper.GetText("mid", String.Empty)) +
            ", " +
            ScriptHelper.GetString(QueryHelper.GetText("hidid", String.Empty)) +
            ")\">" + nick + "</a>";
    }

    #endregion
}
