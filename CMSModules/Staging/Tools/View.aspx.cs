using System;

using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.UIControls;

public partial class CMSModules_Staging_Tools_View : CMSStagingPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!QueryHelper.ValidateHash("hash"))
        {
            URLHelper.Redirect(ResolveUrl("~/CMSMessages/Error.aspx?title=" + GetString("dialogs.badhashtitle") + "&text=" + GetString("dialogs.badhashtext") + "&cancel=1"));
        }
        else
        {
            string element = QueryHelper.GetString("taskType", null);

            // Check permissions for CMS Desk -> Tools -> Staging
            CurrentUserInfo user = CMSContext.CurrentUser;
            if (!user.IsAuthorizedPerUIElement("cms.staging", element))
            {
                RedirectToCMSDeskUIElementAccessDenied("cms.staging", element);
            }

            // Check 'Manage tasks' permission
            if (!CMSContext.CurrentUser.IsAuthorizedPerResource("cms.staging", "Manage" + element + "Tasks"))
            {
                RedirectToAccessDenied("cms.staging", "Manage" + element + "Tasks");
            }

            CurrentMaster.Title.TitleText = GetString("Task.ViewHeader");
            CurrentMaster.Title.TitleImage = GetImageUrl("CMSModules/CMS_Staging/tasks.png");

            int taskId = QueryHelper.GetInteger("taskid", 0);
            ucViewTask.TaskId = taskId;
        }
    }
}