using System;
using System.Web.UI.WebControls;

using CMS.UIControls;

public partial class CMSMasterPages_UI_EmptyPage : CMSMasterPage
{
    /// <summary>
    /// Prepared for specifying the additional HEAD elements.
    /// </summary>
    public override Literal HeadElements
    {
        get
        {
            return ltlHeadElements;
        }
        set
        {
            ltlHeadElements = value;
        }
    }


    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        PageStatusContainer = plcStatus;
    }
}
