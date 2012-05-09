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

using CMS.GlobalHelper;
using CMS.DataEngine;
using CMS.EventLog;
using CMS.SiteProvider;
using CMS.UIControls;
using CMS.EmailEngine;
using CMS.ExtendedControls;

public partial class CMSModules_EmailQueue_MassEmails_Recipients : CMSModalSiteManagerPage
{
    #region "Protected variables"

    protected int emailId = 0;

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        emailId = QueryHelper.GetInteger("emailid", 0);

        gridElem.WhereCondition = "EmailID=" + emailId.ToString();
        gridElem.OnExternalDataBound += new OnExternalDataBoundEventHandler(gridElem_OnExternalDataBound);
        gridElem.OnAction += new OnActionEventHandler(gridElem_OnAction);

        this.CurrentMaster.Title.TitleText = GetString("emailqueue.sentdetails.title");
        this.CurrentMaster.Title.TitleImage = GetImageUrl("Objects/CMS_User/massemail.png");
        this.CurrentMaster.DisplayControlsPanel = true;
        
        btnDeleteSelected.Click += new EventHandler(btnDeleteSelected_Click);
        btnDeleteSelected.OnClientClick = "if (!confirm(" + ScriptHelper.GetString(GetString("EmailQueue.DeleteSelectedRecipientConfirmation")) + ")) return false;";
        imgDeleted.ImageUrl = GetImageUrl("CMSModules/CMS_EmailQueue/deleteselected.png");
    }


    /// <summary>
    /// Remove selected recipients from mass e-mail.
    /// </summary>
    protected void btnDeleteSelected_Click(object sender, EventArgs e)
    {
        // Get list of selected users
        ArrayList list = gridElem.SelectedItems;
        if (list.Count > 0)
        {
            foreach (string userId in list)
            {
                // Remove specific recipient
                EmailUserInfoProvider.DeleteEmailUserInfo(emailId, ValidationHelper.GetInteger(userId, 0));
            }
            gridElem.ResetSelection();
            gridElem.Pager.UniPager.CurrentPage = 1;
            gridElem.ReloadData();
        }
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        if (gridElem.GridView.Rows.Count < 1)
        {
            gridElem.Visible = false;
            btnDeleteSelected.Enabled = false;
            btnDeleteSelected.OnClientClick = "";
        }
        else
        {
            gridElem.Visible = true;
            btnDeleteSelected.Enabled = true;
        }

        Panel pnlActions = (Panel)this.Master.FindControl("pnlPreviousNext");
        if (pnlActions != null)
        {
            pnlActions.Visible = true;
        }
    }

    #endregion


    #region "Grid events"

    object gridElem_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        switch (sourceName.ToLower())
        {
            case "userid":
                // Get user friendly name instead of id
                UserInfo ui = UserInfoProvider.GetUserInfo(ValidationHelper.GetInteger(parameter, 0));
                if (ui != null)
                {
                    return HTMLHelper.HTMLEncode(Functions.GetFormattedUserName(ui.UserName) + " (" + ui.Email + ")");
                }
                else
                {
                    return GetString("general.na");
                }
            case "result":
                return TextHelper.LimitLength(parameter.ToString(), 50);
            case "resulttooltip":
                return HTMLHelper.HTMLEncodeLineBreaks(parameter.ToString());
        }

        return null;
    }


    void gridElem_OnAction(string actionName, object actionArgument)
    {
        switch (actionName.ToLower())
        {
            case "delete":
                int userId = ValidationHelper.GetInteger(actionArgument, 0);
                if (userId > 0)
                {
                    EmailUserInfoProvider.DeleteEmailUserInfo(emailId, userId);
                }
                break;
        }
    }

    #endregion
}
