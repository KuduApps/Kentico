using System;

using CMS.GlobalHelper;
using CMS.ExtendedControls;
using CMS.UIControls;

public partial class CMSAdminControls_UI_PageElements_FrameResizer : CMSUserControl
{
    #region "Variables"

    protected string minimizeUrl = null;
    protected string maximizeUrl = null;

    protected string originalSize = null;
    protected string minSize = null;

    protected string mFramesetName = null;
    protected bool mVertical = false;
    protected bool mAll = false;
    protected string mCssPrefix = "";
    protected int mParentLevel = 1;

    #endregion


    #region "Properties"

    /// <summary>
    /// Frameset minimalized size.
    /// </summary>
    public string MinSize
    {
        get
        {
            return minSize;
        }
        set
        {
            minSize = value;
        }
    }


    /// <summary>
    /// Vertical / horizontal mode
    /// </summary>
    public bool Vertical
    {
        get
        {
            return mVertical;
        }
        set
        {
            mVertical = value;
        }
    }


    /// <summary>
    /// Minimize / maximize all the resizers on the page
    /// </summary>
    public bool All
    {
        get
        {
            return mAll;
        }
        set
        {
            mAll = value;
        }
    }


    /// <summary>
    /// Frameset name.
    /// </summary>
    public string FramesetName
    {
        get
        {
            return ValidationHelper.GetString(mFramesetName, (Vertical ? "rowsFrameset" : "colsFrameset"));
        }
        set
        {
            mFramesetName = value;
        }
    }


    /// <summary>
    /// Css prefix.
    /// </summary>
    public string CssPrefix
    {
        get
        {
            return mCssPrefix;
        }
        set
        {
            mCssPrefix = value;
        }
    }


    /// <summary>
    /// Parent level (1 = immediate parent).
    /// </summary>
    public int ParentLevel
    {
        get
        {
            return mParentLevel;
        }
        set
        {
            mParentLevel = value;
        }
    }

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptHelper.RegisterResizer(Page);

        if (All)
        {
            minimizeUrl = GetImageUrl("Design/Controls/FrameResizer/All/minimizeall.png");
            maximizeUrl = GetImageUrl("Design/Controls/FrameResizer/All/maximizeall.png");

            plcAll.Visible = true;
            plcStandard.Visible = false;
        }
        else
        {
            plcStandard.Visible = true;
            plcAll.Visible = false;

            if (Vertical)
            {
                // Vertical mode
                minimizeUrl = GetImageUrl("Design/Controls/FrameResizer/Vertical/minimize.png");
                maximizeUrl = GetImageUrl("Design/Controls/FrameResizer/Vertical/maximize.png");
            }
            else
            {
                // Horizontal mode
                if (CultureHelper.IsUICultureRTL())
                {
                    minSize = ControlsHelper.GetReversedColumns(minSize);
                }
                minimizeUrl = GetImageUrl("Design/Controls/FrameResizer/Horizontal/minimize.png");
                maximizeUrl = GetImageUrl("Design/Controls/FrameResizer/Horizontal/maximize.png");
            }

            // Define javascript variables
            string varsScript = string.Format("var minSize = '{0}'; var framesetName = '{1}'; var resizeVertical = {2}; var parentLevel = {3}; ",
                                              minSize,
                                              FramesetName,
                                              (Vertical ? "true" : "false"),
                                              ParentLevel);
            ltlScript.Text = ScriptHelper.GetScript(varsScript);

            if (RequestHelper.IsPostBack())
            {
                originalSize = Request.Params["originalsize"];
            }
        }
    }

    #endregion
}