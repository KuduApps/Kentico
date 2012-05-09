using System;

using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.SiteProvider;

public partial class CMSModules_AbuseReport_AbuseReport_ObjectDetails : CMSAbuseReportPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Check hash
        if (!QueryHelper.ValidateHash("hash"))
        {
            RedirectToAccessDenied(ResHelper.GetString("dialogs.badhashtitle"));
        }
        
        Title = GetString("abuse.ObjectTitle");

        // Set the page title
        CurrentMaster.Title.TitleText = GetString("abuse.ObjectTitle");
        CurrentMaster.Title.TitleImage = GetImageUrl("Design/Controls/ObjectDataViewer/objectdetails.png");

        string objectType = QueryHelper.GetString("ObjectType", string.Empty);

        // Check if object type to be displayed is supproted
        if (AbuseReportInfoProvider.IsObjectTypeSupported(objectType))
        {
            ObjectDataViewer.ObjectType = objectType;
            ObjectDataViewer.ObjectID = QueryHelper.GetInteger("ObjectID", 0);
            lblNotSupported.Visible = false;
        }
        else
        {
            ObjectDataViewer.StopProcessing = true;
            ObjectDataViewer.Visible = false;
            lblNotSupported.Visible = true;
        }
    }
}