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

using CMS.FormControls;
using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.SiteProvider;

public partial class CMSFormControls_Cultures_UICultureSelector : FormEngineUserControl
{
    /// <summary>
    /// Gets or sets field value.
    /// </summary>
    public override object Value
    {
        get
        {
            return uniSelector.Value;
        }
        set
        {
            uniSelector.Value = ValidationHelper.GetString(value, "");
        }
    }


    /// <summary>
    /// OnLoad override - load dropdown and set current value.
    /// </summary>
    /// <param name="e"></param>
    protected override void OnLoad(EventArgs e)
    {
        string[,] specialField = { { GetString("Administration-User_Edit.Default"), "" } };
        uniSelector.SpecialFields = specialField;
        uniSelector.AllowEmpty = false;
        uniSelector.AllowAll = false;
        uniSelector.ResourcePrefix = "UISelector";
        uniSelector.IsLiveSite = this.IsLiveSite;
    }

}
