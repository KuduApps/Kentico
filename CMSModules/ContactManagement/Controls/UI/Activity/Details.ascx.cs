using System;
using System.Data;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.OnlineMarketing;
using CMS.IO;

public partial class CMSModules_ContactManagement_Controls_UI_Activity_Details : CMSAdminListControl
{
    #region "Constants"

    private const string PATH_TO_CONTROLS = "~/CMSModules/ContactManagement/Controls/UI/ActivityDetails/{0}.ascx";

    #endregion


    #region "Private variables"

    private ActivityDetail ucDetails = null;

    #endregion


    #region "Properties"

    /// <summary>
    /// Indicates if the control should perform the operations.
    /// </summary>
    public override bool StopProcessing
    {
        get
        {
            return base.StopProcessing;
        }
        set
        {
            base.StopProcessing = value;
        }
    }


    /// <summary>
    /// Indicates if the control is used on the live site.
    /// </summary>
    public override bool IsLiveSite
    {
        get 
        { 
             return base.IsLiveSite;
        }
        set 
        { 
            base.IsLiveSite = value;
        }
    }


    /// <summary>
    /// Gets or sets activity ID.
    /// </summary>
    public int ActivityID
    {
        get;
        set;
    }

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        LoadData();
    }

    private void LoadData()
    {
        ActivityInfo ai = ActivityInfoProvider.GetActivityInfo(this.ActivityID);
        if (ai == null)
        {
            return;
        }

        string pathToControl = String.Format(PATH_TO_CONTROLS, ai.ActivityType);
        try
        {
            ucDetails = this.Page.LoadControl(pathToControl) as ActivityDetail;
            bool dataLoaded = ucDetails.LoadData(ai);
            pnlDetails.GroupingText = GetString("om.activity.details.groupdetails");
            pnlDetails.Controls.Add(ucDetails);
            ucDetails.Visible = dataLoaded;
            pnlDetails.Visible = dataLoaded;
        }
        catch(HttpException)
        {
            // Failed to load the control
        }
    }

    #endregion
}