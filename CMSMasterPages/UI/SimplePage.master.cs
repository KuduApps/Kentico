using System;
using System.Web.UI.WebControls;

using CMS.UIControls;

public partial class CMSMasterPages_UI_SimplePage : CMSMasterPage
{
    #region "Properties"

    /// <summary>
    /// PageTitle control.
    /// </summary>
    public override PageTitle Title
    {
        get
        {
            return titleElem;
        }
    }


    /// <summary>
    /// HeaderActions control.
    /// </summary>
    public override HeaderActions HeaderActions
    {
        get
        {
            return actionsElem;
        }
    }


    /// <summary>
    /// Body panel.
    /// </summary>
    public override Panel PanelBody
    {
        get
        {
            return pnlBody;
        }
    }


    /// <summary>
    /// Gets the content panel.
    /// </summary>
    public override Panel PanelContent
    {
        get
        {
            return pnlContent;
        }
    }


    /// <summary>
    /// Gets the labels container.
    /// </summary>
    public override PlaceHolder PlaceholderLabels
    {
        get
        {
            return plcLabels;
        }
    }


    /// <summary>
    /// Gets UIPlaceHolder which wrapps up HeaderActions and allows to modify visibility of its children.
    /// </summary>
    public override UIPlaceHolder HeaderActionsPlaceHolder
    {
        get
        {
            return plcActionsPermissions;
        }
    }


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

    #endregion


    #region "Page Events"

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        PageStatusContainer = plcStatus;
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        // Hide actions panel if no actions are present
        if (!actionsElem.Visible && (plcActions.Controls.Count == 0))
        {
            pnlActions.Visible = false;
        }

        // Display panel with additional controls place holder if required
        if (DisplayControlsPanel)
        {
            pnlAdditionalControls.Visible = true;
        }

        // Display panel with site selector
        if (DisplaySiteSelectorPanel)
        {
            pnlSiteSelector.Visible = true;
        }
    }

    #endregion
}