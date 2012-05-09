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

using CMS.Notifications;
using CMS.GlobalHelper;
using CMS.FormEngine;
using CMS.CMSHelper;
using CMS.UIControls;

public partial class CMSModules_Notifications_Controls_TemplateText : CMSUserControl
{
    #region "Variables"

    private int mTemplateID = 0;
    private int mGatewayCount = 0;

    private DataSet dsGateways = null;

    #endregion


    #region "Public properties"

    /// <summary>
    /// Returns number of gateways being edited.
    /// </summary>
    public int GatewayCount
    {
        get
        {
            return this.mGatewayCount;
        }
    }

    /// <summary>
    /// Gets or sets ID of the template to edit.
    /// </summary>
    public int TemplateID
    {
        get
        {
            return this.mTemplateID;
        }
        set
        {
            this.mTemplateID = value;
        }
    }

    #endregion


    #region "Page Events"


    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        dsGateways = NotificationGatewayInfoProvider.GetGateways(null, null, 11, "GatewayID,GatewayDisplayName");
        if (!DataHelper.DataSourceIsEmpty(dsGateways))
        {
            this.mGatewayCount = dsGateways.Tables[0].Rows.Count;
        }
        else
        {
            this.mGatewayCount = 0;
        }

    }


    protected void Page_Load(object sender, EventArgs e)
    {
        // For up to 10 Gateways edit all on one page, hide UniGrid
        if (this.GatewayCount <= 10)
        {
            this.gridGateways.StopProcessing = true;
            this.pnlGrid.Visible = false;

            // Generate controls
            foreach (DataRow dr in dsGateways.Tables[0].Rows)
            {
                try
                {
                    Panel pnlGrouping = new Panel();
                    if (CultureHelper.IsCultureRTL(CMSContext.CurrentUser.PreferredUICultureCode))
                    {
                        pnlGrouping.GroupingText = "<strong>" + HTMLHelper.HTMLEncode(dr["GatewayDisplayName"].ToString()) + "</strong> :" + ValidationHelper.GetString(GetString("notification.template.gateway"), "");
                    }
                    else
                    {
                        pnlGrouping.GroupingText = ValidationHelper.GetString(GetString("notification.template.gateway") + ": <strong>" + HTMLHelper.HTMLEncode(dr["GatewayDisplayName"].ToString()) + "</strong>", "");
                    }
                    pnlGrouping.Attributes.Add("style", "margin-bottom: 15px;");

                    Panel gatewayContent = new Panel();
                    gatewayContent.Attributes.Add("style", "margin-top: 15px;");

                    TemplateTextEdit ctrl = Page.LoadControl("~/CMSModules/Notifications/Controls/TemplateTextEdit.ascx") as TemplateTextEdit;
                    if (ctrl != null)
                    {
                        ctrl.ID = "templateEdit" + ValidationHelper.GetInteger(dr["GatewayID"], 0);
                        ctrl.TemplateID = this.TemplateID;
                        ctrl.GatewayID = ValidationHelper.GetInteger(dr["GatewayID"], 0);
                        gatewayContent.Controls.Add(ctrl);

                        // Add gateway edit control to the container panel
                        pnlGrouping.Controls.Add(gatewayContent);
                        plcTexts.Controls.Add(pnlGrouping);
                    }
                }
                catch { }
            }
        }
        else
        {
            this.gridGateways.OnExternalDataBound += new OnExternalDataBoundEventHandler(gridGateways_OnExternalDataBound);
            this.gridGateways.OnAction += new OnActionEventHandler(gridGateways_OnAction);
        }
    }

    #endregion


    #region "Unigrid events"

    protected object gridGateways_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        if (sourceName.ToLower() == "gatewayenabled")
        {
            return UniGridFunctions.ColoredSpanYesNo(parameter);
        }
        return parameter;
    }


    protected void gridGateways_OnAction(string actionName, object actionArgument)
    {
        int gatewayId = ValidationHelper.GetInteger(actionArgument, 0);

        switch (actionName.ToLower())
        {
            case "edit":
                URLHelper.Redirect("Template_Edit_Text_Text.aspx?gatewayid=" + gatewayId + "&templateid=" + this.TemplateID);
                break;
        }
    }

    #endregion


    #region "Public methods"

    /// <summary>
    /// Saves template texts.
    /// </summary>
    public void Save()
    {
        if (this.GatewayCount > 10)
        {
            return;
        }

        foreach (Control ctrl in plcTexts.Controls)
        {
            Panel pnl = ctrl as Panel;
            if (pnl != null)
            {
                Panel gatewayPnl = pnl.Controls[0] as Panel;

                if (gatewayPnl != null)
                {
                    TemplateTextEdit tte = gatewayPnl.Controls[0] as TemplateTextEdit;
                    if (tte != null)
                    {
                        tte.SaveData();
                    }
                }
            }
        }
    }

    #endregion
}
