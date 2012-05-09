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

public partial class CMSMasterPages_LiveSite_Dialogs_TabsHeader : CMSLiveMasterPage
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
    /// Prepared for specifying the additional HEAD elements.
    /// </summary>
    public override Literal HeadElements
    {
        get
        {
            return this.ltlHeadElements;
        }
        set
        {
            this.ltlHeadElements = value;
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
    /// Separator panel.
    /// </summary>
    public override Panel PanelSeparator
    {
        get
        {
            return this.pnlSeparator;
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
    /// Panel containing tab menu control.
    /// </summary>
    public override Panel PanelTabs
    {
        get
        {
            return this.pnlWhite;
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


    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        if (this.Page is CMSPage)
        {
            (this.Page as CMSPage)["TabControl"] = this.Tabs;
        }

        // Set dialog CSS class
        SetDialogClass();
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
