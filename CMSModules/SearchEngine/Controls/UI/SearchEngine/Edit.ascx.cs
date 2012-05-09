using System;
using System.Data;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.FormControls;
using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.SettingsProvider;

public partial class CMSModules_SearchEngine_Controls_UI_SearchEngine_Edit : CMSAdminEditControl
{
    #region "Properties"

    /// <summary>
    /// UIForm control used for editing objects properties.
    /// </summary>
    public UIForm UIFormControl
    {
        get
        {
            return this.EditForm;
        }
    }


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
            this.EditForm.StopProcessing = value;
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
            EditForm.IsLiveSite = value;
        }
    }

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    #endregion
}

