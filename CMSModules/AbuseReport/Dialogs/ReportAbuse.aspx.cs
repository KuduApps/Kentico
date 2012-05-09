using System;
using System.Collections;
using System.Web;

using CMS.GlobalHelper;
using CMS.UIControls;

public partial class CMSModules_AbuseReport_Dialogs_ReportAbuse : CMSModalPage
{
    #region "Variables"

    private Hashtable parameters = null;

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        string identifier = QueryHelper.GetString("params", null);
        parameters = (Hashtable)WindowHelper.GetItem(identifier);

        if (parameters != null)
        {
            // Initialize reporting control
            reportElem.ConfirmationText = ValidationHelper.GetString(parameters["confirmationtext"], string.Empty);
            reportElem.ReportTitle = ValidationHelper.GetString(parameters["reporttitle"], string.Empty);
            reportElem.ReportObjectID = ValidationHelper.GetInteger(parameters["reportobjectid"], 0);
            reportElem.ReportObjectType = ValidationHelper.GetString(parameters["reportobjecttype"], string.Empty);
            reportElem.ReportURL = ValidationHelper.GetString(parameters["reporturl"], string.Empty);
            reportElem.DisplayButtons = false;
            reportElem.ShowCancelButton = true;
            reportElem.BodyPanel.CssClass = "DialogAbuseBody";
            reportElem.ReportButton = btnReport;
            reportElem.CancelButton = btnCancel;

            // Initialize buttons
            btnCancel.OnClientClick = "window.close();return false;";
            btnReport.Click += btnReport_Click;

            // Set title
            string reportDialogTitle = ValidationHelper.GetString(parameters["reportdialogtitle"], string.Empty);
            CurrentMaster.Title.TitleText = reportDialogTitle;
            CurrentMaster.Title.AlternateText = reportDialogTitle;
            CurrentMaster.Title.TitleImage = GetImageUrl("Objects/CMS_AbuseReport/object.png");
        }
        else
        {
            reportElem.Visible = false;
        }
    }

    #endregion


    protected void btnReport_Click(object sender, EventArgs e)
    {
        reportElem.PerformAction();
    }
}
