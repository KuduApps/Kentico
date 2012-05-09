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

using CMS.FormEngine;
using CMS.FormControls;
using CMS.GlobalHelper;

public partial class CMSModules_Groups_FormControls_ViewGroupAccess : FormEngineUserControl
{
    private int mValue;

    #region "Public properties"

    /// <summary>
    /// Gets or sets field value.
    /// </summary>
    public override object Value
    {
        get
        {
            return mValue;
        }
        set
        {
            mValue = ValidationHelper.GetInteger(value, 0);
        }
    }

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        switch ((int)this.Value)
        {
            case 0:
                lblAccess.Text = GetString("group.group.accessanybody");
                break;

            case 1:
                lblAccess.Text = GetString("group.group.accesssitemembers");
                break;

            case 3:
                lblAccess.Text = GetString("group.group.accessgroupmembers");
                break;
            default:
            case 2:
                break;
        }
    }

    #endregion
}