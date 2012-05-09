using System;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using CMS.GlobalHelper;
using CMS.UIControls;

public partial class CMSMasterPages_UI_Dialogs_ModalDialogPage : CMSMasterPage
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
    /// Footer panel.
    /// </summary>
    public override Panel PanelFooter
    {
        get
        {
            return pnlFooter;
        }
    }


    /// <summary>
    /// Gets the header panel
    /// </summary>
    public override Panel PanelHeader
    {
        get
        {
            return pnlHeader;
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
    /// Body object.
    /// </summary>
    public override HtmlGenericControl Body
    {
        get
        {
            return bodyElem;
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
    /// Panel containing title actions disaplyed above scrolling content.
    /// </summary>
    public override Panel PanelTitleActions
    {
        get
        {
            return pnlTitleActions;
        }
    }

    #endregion


    #region "Methods"

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

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

        // Display panel with site selector
        if (DisplaySiteSelectorPanel)
        {
            pnlSiteSelector.Visible = true;
        }

        bodyElem.Attributes["class"] = mBodyClass;

        this.titleElem.HelpIconUrl = UIHelper.GetImageUrl(this.Page,"General/HelpLargeDark.png");

        StringBuilder resizeScript = new StringBuilder();
        resizeScript.Append(@"
var headerElem = null;
var footerElem = null;
var contentElem = null;
var oldClientWidth = 0;
var oldClientHeight = 0;

function ResizeWorkingAreaIE()
{
   ResizeWorkingArea();
   window.onresize = function() { ResizeWorkingArea(); };
}

function ResizeWorkingArea()
{
   if (headerElem == null)
   {
      headerElem = document.getElementById('divHeader');
   }
   if (footerElem == null)
   {
      footerElem = document.getElementById('divFooter');
   }
   if (contentElem == null)
   {
      contentElem = document.getElementById('divContent');
   }
   if ((headerElem != null) && (footerElem != null) && (contentElem != null))
   {
       var headerHeight = headerElem.offsetHeight;
       var footerHeight = footerElem.offsetHeight;
       var height = (document.body.offsetHeight - headerHeight - footerHeight);
       if (height > 0)
       {
           var h = (height > 0 ? height : '0') + 'px';
           if (contentElem.style.height != h)
           {
               contentElem.style.height = h;
           }
       }");
        if (BrowserHelper.IsIE())
        {
            resizeScript.AppendFormat(@"
       var pnlBody = null;
       var formElem = null;
       var bodyElement = null;
       if (pnlBody == null)
       {{
          pnlBody = document.getElementById('{0}');
       }}
       if (formElem == null)
       {{
          formElem = document.getElementById('{1}');
       }}
       if (bodyElement == null)
       {{
          bodyElement = document.getElementById('{2}');
       }}
       if ((bodyElement != null) && (formElem != null) && (pnlBody != null))
       {{
           var newClientWidth = document.documentElement.clientWidth;
           var newClientHeight = document.documentElement.clientHeight;
           if  (newClientWidth != oldClientWidth)
           {{
               bodyElement.style.width = newClientWidth;
               formElem.style.width = newClientWidth;
               pnlBody.style.width = newClientWidth;
               headerElem.style.width = newClientWidth;
               contentElem.style.width = newClientWidth;
               oldClientWidth = newClientWidth;
           }}
           if  (newClientHeight != oldClientHeight)
           {{
               bodyElement.style.height = newClientHeight;
               formElem.style.height = newClientHeight;
               pnlBody.style.height = newClientHeight;
               oldClientHeight = newClientHeight;
           }}
       }}", pnlBody.ClientID, form1.ClientID, bodyElem.ClientID);
        }

        resizeScript.Append(@"
    }
    if (window.afterResize) {
        window.afterResize();
    }
}");
        if (BrowserHelper.IsIE())
        {
            resizeScript.Append(@"
var timer = setInterval('ResizeWorkingAreaIE();', 50);");
        }
        else
        {
            resizeScript.Append(@"
window.onresize = function() { ResizeWorkingArea(); };
window.onload = function() { ResizeWorkingArea(); };");
        }

        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "resizeScript", ScriptHelper.GetScript(resizeScript.ToString()));

        if (BrowserHelper.IsIE7())
        {
            ScriptHelper.RegisterStartupScript(this, typeof(string), "ie7ResizeFix", ScriptHelper.GetScript("document.getElementById('divContent').style.height = '0px';"));
        }
    }

    #endregion
}