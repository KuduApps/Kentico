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

public partial class CMSAdminControls_UI_PageElements_FrameCloser : CMSUserControl
{
    protected string minimizeUrl = null;
    protected string maximizeUrl = null;
    private string minSize = null;

    /// <summary>
    /// Property for frameset minimalized size.
    /// </summary>
    public string MinSize
    {
        get
        {
            return minSize;
        }
        set
        {
            minSize = value;
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!RequestHelper.IsPostBack())
        {
            minimizeUrl = GetImageUrl("Design/Controls/FrameCloser/Minimize.png");
            maximizeUrl = GetImageUrl("Design/Controls/FrameCloser/Maximize.png");
            this.ltlScript.Text = ScriptHelper.GetScript("var minCols = '" + minSize + "';");
        }
    }
}
