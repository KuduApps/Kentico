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
using CMS.UIControls;

public partial class CMSAdminControls_UI_System_RequireScript : CMSUserControl
{
    /// <summary>
    /// Indicates if the control should use the string from resource file.
    /// </summary>
    public bool UseFileStrings
    {
        get;
        set;
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        this.ScriptTitle.TitleText = GetString("RequireScript.Title");
        this.lblInfo.Text = GetString("RequireScript.Information");
        this.ScriptTitle.TitleImage = GetImageUrl("Design/Controls/RequireScript/javascript.png");
        this.btnContinue.Text = GetString("RequireScript.Continue");
    }


    /// <summary>
    /// Returns localized string.
    /// </summary>
    /// <param name="stringName">String to localize</param>
    public override string GetString(string stringName)
    {
        if (this.UseFileStrings)
        {
            return ResHelper.GetFileString(stringName);
        }
        else
        {
            return base.GetString(stringName);
        }
    }
}
