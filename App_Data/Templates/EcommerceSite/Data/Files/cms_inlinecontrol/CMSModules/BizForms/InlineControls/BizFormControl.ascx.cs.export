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

using CMS.GlobalHelper;
using CMS.ExtendedControls;

public partial class CMSModules_BizForms_InlineControls_BizFormControl : InlineUserControl
{
    /// <summary>
    /// Form name.
    /// </summary>
    public string FormName
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("FormName"), null);
        }
        set
        {
            this.SetValue("FormName", value);
            this.Bizform1.FormName = value;
        }
    }


    /// <summary>
    /// Control parameter.
    /// </summary>
    public override string Parameter
    {
        get
        {
            return this.FormName;
        }
        set
        {
            this.FormName = value;
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        SetupControl();
    }


    /// <summary>
    /// Initializes the control properties.
    /// </summary>
    protected void SetupControl()
    {
        this.Bizform1.FormName = this.FormName;
        this.Bizform1.IsLiveSite = this.IsLiveSite;
    }
}
