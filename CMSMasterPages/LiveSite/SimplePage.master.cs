using System;
using System.Web.UI.WebControls;

using CMS.UIControls;

public partial class CMSMasterPages_LiveSite_SimplePage : CMSLiveMasterPage
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

    #endregion


    #region "Methods"

    protected void Page_Init(object sender, EventArgs e)
    {
        PageStatusContainer = plcStatus;
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        // Hide actions panel if no actions are present and DisplayActionsPanel is false
        if (!DisplayActionsPanel)
        {
            if ((actionsElem.Actions == null) || (actionsElem.Actions.Length == 0))
            {
                pnlActions.Visible = false;
            }
        }

        // Display panel with additional controls place holder if required
        if (DisplayControlsPanel) 
        {
            pnlAdditionalControls.Visible = true;
        }
    }

    #endregion
}