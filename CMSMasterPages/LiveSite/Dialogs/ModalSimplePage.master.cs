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
using CMS.CMSHelper;
using CMS.GlobalHelper;

public partial class CMSMasterPages_LiveSite_Dialogs_ModalSimplePage : CMSLiveMasterPage
{
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
    /// Body panel.
    /// </summary>
    public override Panel PanelBody
    {
        get
        {
            return this.pnlBody;
        }
    }


    /// <summary>
    /// Body object.
    /// </summary>
    public override HtmlGenericControl Body
    {
        get
        {
            return this.bodyElem;
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


    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        this.PageStatusContainer = this.plcStatus;

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

        // Display panel with additional controls place holder if required
        if (this.DisplayControlsPanel) 
        {
            this.pnlAdditionalControls.Visible = true;
        }
        this.bodyElem.Attributes["class"] = mBodyClass;
    }
}
