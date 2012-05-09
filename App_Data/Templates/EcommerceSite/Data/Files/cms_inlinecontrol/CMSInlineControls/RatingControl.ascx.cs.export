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

public partial class CMSInlineControls_RatingControl : InlineUserControl
{
    #region "Public properties"

    /// <summary>
    /// Gets or sets type of rating scale.
    /// </summary>
    public string RatingType
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("RatingType"), null);
        }
        set
        {
            this.SetValue("RatingType", value);
            elemRating.RatingType = value;
        }
    }


    /// <summary>
    /// Gets or sets control parameter.
    /// </summary>
    public override string Parameter
    {
        get
        {
            return this.RatingType;
        }
        set
        {
            this.RatingType = value;
        }
    }

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        this.elemRating.RatingType = this.RatingType;
    }

    #endregion
}

