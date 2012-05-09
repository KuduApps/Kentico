using System;

using CMS.GlobalHelper;
using CMS.CMSImportExport;
using CMS.UIControls;
using CMS.CMSHelper;
using CMS.EventLog;

public partial class CMSSiteManager_Development_WebTemplates_WebTemplate_Export : SiteManagerPage
{
    #region "Protected variables"

    protected int webTemplateId = 0;

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        Title = "Export Web Template";

        // Set site selector        
        siteSelector.UseCodeNameForSelection = true;
        siteSelector.AllowAll = false;

        CurrentMaster.Title.TitleText = GetString("Administration-WebTemplate_Export.Title");
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        lblError.Visible = (lblError.Text != string.Empty);
        lblInfo.Visible = (lblInfo.Text != string.Empty);
    }

    #endregion


    #region "Button handling"

    protected void btnExport_Click(object sender, EventArgs e)
    {
        try
        {
            string selectedSite = ValidationHelper.GetString(siteSelector.Value, String.Empty);

            string targetPath = Server.MapPath("~/App_Data/Templates/" + selectedSite);
            string[] excludedNames = txtExcluded.Text.Trim().Split(';');

            ExportProvider.ExportWebTemplate(selectedSite, null, targetPath, excludedNames, CMSContext.CurrentUser);

            lblInfo.Text = "Template has been exported to " + targetPath;
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
            lblError.ToolTip = EventLogProvider.GetExceptionLogMessage(ex);
        }
    }

    #endregion
}
