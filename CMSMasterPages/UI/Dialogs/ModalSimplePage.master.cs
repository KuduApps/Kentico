using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using CMS.GlobalHelper;
using CMS.UIControls;

public partial class CMSMasterPages_UI_Dialogs_ModalSimplePage : CMSMasterPage
{
    #region "Properties"

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


    /// <summary>
    /// Info label control.
    /// </summary>
    public override Label InfoLabel
    {
        get
        {
            if (base.InfoLabel != null)
            {
                return base.InfoLabel;
            }

            return this.lblInfoLabel;
        }
        set
        {
            base.InfoLabel = value;
        }
    }


    /// <summary>
    /// Error label control.
    /// </summary>
    public override Label ErrorLabel
    {
        get
        {
            if (base.ErrorLabel != null)
            {
                return base.ErrorLabel;
            }

            return this.lblErrorLabel;
        }
        set
        {
            base.ErrorLabel = value;
        }
    }

    #endregion


    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        this.PageStatusContainer = this.plcStatus;
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        this.titleElem.HelpIconUrl = UIHelper.GetImageUrl(this.Page, "General/HelpLargeDark.png");

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

        // Display panel with site selector
        if (DisplaySiteSelectorPanel)
        {
            pnlSiteSelector.Visible = true;
        }

        this.bodyElem.Attributes["class"] = mBodyClass;
    }
}
