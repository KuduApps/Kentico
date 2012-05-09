using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.ExtendedControls;
using CMS.UIControls;

public partial class CMSAdminControls_UI_Trees_TreeBorder : CMSUserControl
{
    #region "Properties"

    protected bool mAllowMouseResizing = true;


    /// <summary>
    /// Resizer control.
    /// </summary>
    public CMSUserControl FrameResizer
    {
        get
        {
            return frmResizer;
        }
    }


    /// <summary>
    /// Frameset minimalized size.
    /// </summary>
    public string MinSize
    {
        get
        {
            return this.frmResizer.MinSize;
        }
        set
        {
            this.frmResizer.MinSize = value;
        }
    }


    /// <summary>
    /// Frameset name.
    /// </summary>
    public string FramesetName
    {
        get
        {
            return this.frmResizer.FramesetName;
        }
        set
        {
            this.frmResizer.FramesetName = value;
        }
    }


    /// <summary>
    /// If true, resizing with mouse is enabled.
    /// </summary>
    public bool AllowMouseResizing
    {
        get
        {
            return mAllowMouseResizing;
        }
        set
        {
            mAllowMouseResizing = value;
        }
    }

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {    
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        if (AllowMouseResizing && this.FrameResizer.Visible)
        {
            this.pnlBorder.Attributes.Add("onmousemove", "InitFrameResizer(this); return false;");
            this.pnlBorder.Attributes.Add("unselectable", "on");
        }
        else
        {
            this.pnlBorder.Style.Add("cursor", "default");
        }
    }
}
