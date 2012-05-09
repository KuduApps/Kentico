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
using System.Text;
using System.Security.Principal;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.DataEngine;

public partial class CMSModules_System_Debug_System_ViewLog : CMSDebugPage
{
    CMSThread thread = null;
    Guid threadGuid = Guid.Empty;


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        threadGuid = QueryHelper.GetGuid("threadguid", Guid.Empty);
        
        thread = CMSThread.GetThread(threadGuid);
        ctlAsync.OnRequestLog += new EventHandler(ctlAsync_OnRequestLog);

        if (!IsCallback)
        {
            // Set the title
            CurrentMaster.Title.TitleText = GetString("ViewLog.Title");
            CurrentMaster.Title.TitleImage = GetImageUrl("Objects/__GLOBAL__/Object.png");
            Page.Title = GetString("ViewLog.Title");

        if ((thread != null) && (thread.Log != null))
        {
            this.pnlLog.Visible = true;
            this.pnlError.Visible = false;
            btnCancel.Text = GetString("general.cancel");
            btnCancel.OnClientClick = "return confirm('" + GetString("ViewLog.CancelPrompt") + "')";
        

            ctlAsync.RunAsync();
        }
        else
        {
            this.pnlError.Visible = true;
            this.pnlLog.Visible = false;
            this.lblError.Text = GetString("ViewLog.ThreadNotRunning");
        }
    }
    }


    protected void btnCancel_Click(object sender, EventArgs e)
    {
        if (thread != null)
        {
            thread.Stop();
        }

        this.pnlError.Visible = true;
        this.pnlLog.Visible = false;
        this.lblError.Text = GetString("ViewLog.ThreadNotRunning");
    }


    void ctlAsync_OnRequestLog(object sender, EventArgs e)
    {
        if (thread != null)
        {
            ctlAsync.Log = thread.Log.Log;
        }
    }
    
    #endregion
}
