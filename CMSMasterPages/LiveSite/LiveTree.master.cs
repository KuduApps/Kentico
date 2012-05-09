using System;

using CMS.UIControls;

public partial class CMSMasterPages_LiveSite_LiveTree : CMSLiveMasterPage
{
    public override CMSUserControl FrameResizer
    {
        get
        {
            return frmResizer;
        }
    }


    protected override void OnPreRender(EventArgs e)
    {
        this.pnlBorder.Visible = frmResizer.Visible;
        base.OnPreRender(e);
    }


    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        PageStatusContainer = plcStatus;
    }
}
