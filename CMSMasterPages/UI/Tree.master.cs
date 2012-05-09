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

using CMS.UIControls;

public partial class CMSMasterPages_UI_Tree : CMSMasterPage
{
    /// <summary>
    /// Resizer control.
    /// </summary>
    public override CMSUserControl FrameResizer
    {
        get
        {
            return borderElem.FrameResizer;
        }
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
    }


    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        PageStatusContainer = plcStatus;
    }
}
