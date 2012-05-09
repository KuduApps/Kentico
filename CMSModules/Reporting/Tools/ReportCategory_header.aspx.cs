using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;


public partial class CMSModules_Reporting_Tools_ReportCategory_header : CMSReportingPage 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Title = "Report header";
        this.CurrentMaster.Title.HelpTopicName = "ReportCategories";
        this.CurrentMaster.Title.HelpName = "helpTopic";

        this.CurrentMaster.Title.TitleText = GetString("tools.ui.reporting");
        this.CurrentMaster.Title.TitleImage = GetImageUrl("Objects/Reporting_ReportCategory/object.png");
    }

}

