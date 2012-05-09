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
using CMS.GlobalHelper;
using CMS.SiteProvider;

public partial class CMSFormControls_Viewers_ViewUserGender : FormEngineUserControl
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
        UserGenderEnum gender = (UserGenderEnum)mValue;

        switch (gender)
        {
            case UserGenderEnum.Male:
                lblGender.Text = GetString("general.male");
                break;

            case UserGenderEnum.Female:
                lblGender.Text = GetString("general.female");
                break;

            case UserGenderEnum.Unknown:
            default:
                lblGender.Text = GetString("general.na");
                break;
        }
    }

    #endregion
}
