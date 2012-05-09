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
using CMS.Controls;

public partial class CMSMasterPages_LiveSite_LiveTabsHeader : CMSLiveMasterPage
{
    /// <summary>
    /// Tabs control.
    /// </summary>
    public override UITabs Tabs
    {
        get
        {
            return this.tabControlElem;
        }
    }


    /// <summary>
    /// PageTitle control.
    /// </summary>
    public override PageTitle Title
    {
        get
        {
            return this.titleElem;
        }
    }


    /// <summary>
    /// HeaderActions control.
    /// </summary>
    public override HeaderActions HeaderActions
    {
        get
        {
            return this.actionsElem;
        }
    }


    /// <summary>
    /// Left tabs panel.
    /// </summary>
    public override Panel PanelLeft
    {
        get
        {
            return this.pnlLeft;
        }
    }


    /// <summary>
    /// Right tabs panel.
    /// </summary>
    public override Panel PanelRight
    {
        get
        {
            return this.pnlRight;
        }
    }


    /// <summary>
    /// Tab master page doesn't hide page title.
    /// </summary>
    public override bool TabMode
    {
        get
        {
            return false;
        }
    }


    /// <summary>
    /// Panel containig title.
    /// </summary>
    public override Panel PanelTitle
    {
        get
        {
            return this.pnlTitle;
        }
    }


    /// <summary>
    /// Frame resizer.
    /// </summary>
    public override CMSUserControl FrameResizer
    {
        get
        {
            return this.frmResizer;
        }
    }


    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        if (this.Page is CMSPage)
        {
            (this.Page as CMSPage)["TabControl"] = this.Tabs;
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        // Hide actions panel if no actions are present and DisplayActionsPanel is false
        if (!this.DisplayActionsPanel)
        {
            if ((this.actionsElem.Actions == null) || (this.actionsElem.Actions.Length == 0))
            {
                this.pnlActions.Visible = false;
            }
        }
    }
}
