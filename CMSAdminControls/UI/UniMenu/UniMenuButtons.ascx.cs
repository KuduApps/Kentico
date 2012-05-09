using System;
using System.Text;
using System.Web.UI.WebControls;
using System.Collections.Generic;

using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.CMSHelper;
using CMS.ExtendedControls;

public partial class CMSAdminControls_UI_UniMenu_UniMenuButtons : CMSUserControl
{
    #region "Variables"

    /// <summary>
    /// Buttons.
    /// </summary>
    private string[,] mButtons = null;

    private bool mAllowSelection = false;

    private bool mAllowToggle = false;
    
    private int identifier = 0;

    protected int mSelectedIndex = 0;

    private List<Panel> mInnerControls = null;

    private int mMaximumItems = 1;

    private bool mHorizontalLayout = true;

    #endregion


    #region "Constants"

    public const string SelectedSuffix = " Selected";

    private const string BUTTON_PANEL_SHORTID = "pb";

    #endregion


    #region "Public properties"

    /// <summary>
    /// Determines whether buttons act as toggle buttons.
    /// </summary>
    public bool AllowToggle
    {
        get
        {
            return mAllowToggle;
        }
        set
        {
            mAllowToggle = value;
        }
    }


    /// <summary>
    /// Determines whether buttons are selectable.
    /// </summary>
    public bool AllowSelection
    {
        get
        {
            return mAllowSelection;
        }
        set
        {
            mAllowSelection = value;
        }
    }


    /// <summary>
    /// If selection is enabled, determines which button is implicitly selected.
    /// </summary>
    public int SelectedIndex
    {
        get
        {
            return mSelectedIndex;
        }
        set
        {
            mSelectedIndex = value;
        }
    }


    /// <summary>
    /// Indicates if function 'CheckChanges' should be called before action.
    /// </summary>
    public bool CheckChanges
    {
        get;
        set;
    }


    /// <summary>
    /// Array of buttons. 
    /// {
    /// [n, 0] - Caption of button
    /// [n, 1] - Button tooltip (string.empty: empty, null: copies caption or custom string)
    /// [n, 2] - CSS class of button
    /// [n, 3] - JavaScript OnClick action
    /// [n, 4] - Button's redirect URL
    /// [n, 5] - Image path of button
    /// [n, 6] - Image alternate text (string.empty: empty, null: copies caption or custom string
    /// [n, 7] - ImageAlign enum (default: NotSet)
    /// [n, 8] - Minimal width of middle part of the button (in pixels)
    /// }
    /// </summary> 
    public string[,] Buttons
    {
        get
        {
            return mButtons;
        }
        set
        {
            mButtons = value;
        }
    }


    /// <summary>
    /// Description.
    /// </summary>
    public List<Panel> InnerControls
    {
        get
        {
            return mInnerControls;
        }
        set
        {
            mInnerControls = value;
        }
    }


    /// <summary>
    /// Indicates whether to repeat items horizontally. Value is true by default. Otherwise items will be rendered vertically.
    /// </summary>
    public bool HorizontalLayout
    {
        get
        {
            return mHorizontalLayout;
        }
        set
        {
            mHorizontalLayout = value;
        }
    }


    /// <summary>
    /// Specifies number of menu items to be rendered in single row or column (depending on RepeatHorizontal property).
    /// </summary>
    public int MaximumItems
    {
        get
        {
            return mMaximumItems;
        }
        set
        {
            mMaximumItems = value;
        }
    }

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        if (StopProcessing)
        {
            // Do nothing
        }
        else
        {
            if (Buttons != null)
            {
                if (AllowSelection)
                {
                    ScriptHelper.RegisterJQuery(Page);
                    StringBuilder selectionScript = new StringBuilder();
                    selectionScript.AppendLine("function SelectButton(elem)");
                    selectionScript.AppendLine("{");
                    selectionScript.AppendLine("    var selected = '" + SelectedSuffix + "';");
                    selectionScript.AppendLine("    var jElem =$j(elem);");
                    // Get first parent table
                    selectionScript.AppendLine("    var parentTable = jElem.parents('table').get(0);");
                    selectionScript.AppendLine("    if (parentTable != null) {");
                    // Remove selected class from current group of buttons
                    selectionScript.AppendLine("        $j(parentTable).find('.' + elem.className).removeClass(selected);");
                    selectionScript.AppendLine("    }");
                    selectionScript.AppendLine("    jElem.addClass(selected);");
                    selectionScript.AppendLine("}");
                    ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "selectionScript", ScriptHelper.GetScript(selectionScript.ToString()));

                    StringBuilder indexSelection = new StringBuilder();
                    indexSelection.AppendLine("function SelectButtonIndex_" + ClientID + "(index)");
                    indexSelection.AppendLine("{");
                    indexSelection.AppendLine("    var elem = document.getElementById('" + ClientID + "_" + BUTTON_PANEL_SHORTID + "'+index);");
                    indexSelection.AppendLine("    SelectButton(elem);");
                    indexSelection.AppendLine("");
                    indexSelection.AppendLine("}");
                    ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "indexSelection_" + ClientID, ScriptHelper.GetScript(indexSelection.ToString()));
                }

                if (AllowToggle)
                {
                    // Toggle script
                    StringBuilder toggleScript = new StringBuilder();
                    toggleScript.AppendLine("function ToggleButton(elem)");
                    toggleScript.AppendLine("{");
                    toggleScript.AppendLine("    var selected = '" + SelectedSuffix.Trim() + "';");
                    toggleScript.AppendLine("    var jElem =$j(elem);");
                    // Get first parent table
                    toggleScript.AppendLine("    jElem.toggleClass(selected);");
                    toggleScript.AppendLine("}");
                    ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "toggleScript", ScriptHelper.GetScript(toggleScript.ToString()));
                }

                int buttonsCount = Buttons.GetUpperBound(0) + 1;
                InnerControls = new List<Panel>(buttonsCount);
                for (identifier = 0; identifier < buttonsCount; identifier++)
                {
                    // Check array dimensions
                    if (Buttons.GetUpperBound(1) != 8)
                    {
                        Controls.Add(GetError(GetString("unimenubuttons.wrongdimensions")));
                        continue;
                    }

                    string caption = Buttons[identifier, 0];
                    string tooltip = Buttons[identifier, 1];
                    string cssClass = Buttons[identifier, 2];
                    string onClick = Buttons[identifier, 3];
                    string redirectUrl = Buttons[identifier, 4];
                    string imagePath = Buttons[identifier, 5];
                    string alt = Buttons[identifier, 6];
                    string align = Buttons[identifier, 7];
                    string minWidth = Buttons[identifier, 8];
                    ImageAlign imageAlign = ParseImageAlign(align);

                    // Generate button image
                    Image buttonImage = new Image();
                    buttonImage.ID = "img" + identifier;
                    buttonImage.EnableViewState = false;
                    buttonImage.AlternateText = alt ?? caption;
                    if (!string.IsNullOrEmpty(imagePath))
                    {
                        buttonImage.ImageUrl = ResolveUrl(imagePath);
                    }
                    buttonImage.ImageAlign = imageAlign;

                    // Generate button text
                    Literal captionLiteral = new Literal();
                    captionLiteral.ID = "ltlCaption" + identifier;
                    captionLiteral.EnableViewState = false;
                    string separator = (imageAlign == ImageAlign.Top) ? "<br />" : "\n";
                    captionLiteral.Text = separator + caption;

                    // Generate button link
                    HyperLink buttonLink = new HyperLink();
                    buttonLink.ID = "btn" + identifier;
                    buttonLink.EnableViewState = false;

                    buttonLink.Controls.Add(buttonImage);
                    buttonLink.Controls.Add(captionLiteral);

                    if (!string.IsNullOrEmpty(redirectUrl))
                    {
                        buttonLink.NavigateUrl = ResolveUrl(redirectUrl);
                    }

                    // Generate left border
                    CMSPanel pnlLeft = new CMSPanel();
                    pnlLeft.ID = "pnlLeft" + identifier;
                    pnlLeft.ShortID = "pl" + identifier;

                    pnlLeft.EnableViewState = false;
                    pnlLeft.CssClass = "Left" + cssClass;

                    // Generate middle part of button
                    CMSPanel pnlMiddle = new CMSPanel();
                    pnlMiddle.ID = "pnlMiddle" + identifier;
                    pnlMiddle.ShortID = "pm" + identifier;

                    pnlMiddle.EnableViewState = false;
                    pnlMiddle.CssClass = "Middle" + cssClass;
                    pnlMiddle.Controls.Add(buttonLink);
                    if (!string.IsNullOrEmpty(minWidth))
                    {
                        pnlMiddle.Style.Add("min-width", minWidth + "px");

                        // IE7 issue with min-width
                        CMSPanel pnlMiddleTmp = new CMSPanel();
                        pnlMiddleTmp.EnableViewState = false;
                        pnlMiddleTmp.Style.Add("width", minWidth + "px");
                        pnlMiddle.Controls.Add(pnlMiddleTmp);
                    }

                    // Generate right border
                    CMSPanel pnlRight = new CMSPanel();
                    pnlRight.ID = "pnlRight" + identifier;
                    pnlRight.ShortID = "pr" + identifier;

                    pnlRight.EnableViewState = false;
                    pnlRight.CssClass = "Right" + cssClass;

                    // Generate whole button
                    CMSPanel pnlButton = new CMSPanel();
                    pnlButton.ID = "pnlButton" + identifier;
                    pnlButton.ShortID = BUTTON_PANEL_SHORTID + identifier;

                    pnlButton.EnableViewState = false;
                    if ((AllowSelection || AllowToggle) && (identifier == SelectedIndex))
                    {
                        cssClass += SelectedSuffix;
                    }

                    pnlButton.CssClass = cssClass;

                    if (AllowToggle)
                    {
                        pnlButton.CssClass += " Toggle";
                    }

                    //Generate button table (IE7 issue)
                    Table tabButton = new Table();
                    TableRow tabRow = new TableRow();
                    TableCell tabCellLeft = new TableCell();
                    TableCell tabCellMiddle = new TableCell();
                    TableCell tabCellRight = new TableCell();

                    tabButton.CellPadding = 0;
                    tabButton.CellSpacing = 0;

                    tabButton.Rows.Add(tabRow);
                    tabRow.Cells.Add(tabCellLeft);
                    tabRow.Cells.Add(tabCellMiddle);
                    tabRow.Cells.Add(tabCellRight);

                    // Add inner controls
                    tabCellLeft.Controls.Add(pnlLeft);
                    tabCellMiddle.Controls.Add(pnlMiddle);
                    tabCellRight.Controls.Add(pnlRight);

                    pnlButton.Controls.Add(tabButton);

                    pnlButton.ToolTip = tooltip ?? caption;
                    
                    if (AllowSelection)
                    {
                        onClick = "SelectButton(this);" + onClick;
                    }
                    else if (AllowToggle)
                    {
                        onClick = "ToggleButton(this);" + onClick;
                    }

                    pnlButton.Attributes["onclick"] = CheckChanges ? "if (CheckChanges()) {" + onClick + "}" : onClick;

                    // In case of horizontal layout
                    if (HorizontalLayout)
                    {
                        // Stack buttons underneath
                        pnlButton.Style.Add("clear", "both");
                    }
                    else
                    {
                        // Stack buttons side-by-sode
                        pnlButton.Style.Add("float", "left");
                    }

                    // Fill collection of buttons
                    InnerControls.Insert(identifier, pnlButton);
                }

                // Calculate number of needed panels
                int panelCount = InnerControls.Count / MaximumItems;
                if ((InnerControls.Count % MaximumItems) > 0)
                {
                    panelCount++;
                }

                // Initialize list of panels
                List<Panel> panels = new List<Panel>();

                Table tabGroup = new Table();
                TableRow tabGroupRow = new TableRow();

                tabGroup.CellPadding = 0;
                tabGroup.CellSpacing = 0;
                tabGroup.Rows.Add(tabGroupRow);

                // Fill each panel
                for (int panelIndex = 0; panelIndex < panelCount; panelIndex++)
                {
                    // Create new instance of panel
                    CMSPanel outerPanel = new CMSPanel();
                    outerPanel.EnableViewState = false;
                    outerPanel.ID = "pnlOuter" + panelIndex;
                    outerPanel.ShortID = "po" + panelIndex;

                    // In case of horizontal layout
                    if (HorizontalLayout)
                    {
                        // Stack panels side-by-side
                        outerPanel.Style.Add("float", "left");
                    }
                    else
                    {
                        // Stack panels underneath
                        outerPanel.Style.Add("clear", "both");
                    }
                    // Add buttons to panel
                    for (int buttonIndex = (panelIndex * MaximumItems); buttonIndex < (panelCount * MaximumItems) + panelCount; buttonIndex++)
                    {
                        if (((buttonIndex % MaximumItems) < MaximumItems) && (InnerControls.Count > buttonIndex))
                        {
                            outerPanel.Controls.Add(InnerControls[buttonIndex]);
                        }
                    }
                    // Add panel to collection of panels
                    panels.Add(outerPanel);
                }

                // Add all panels to control
                foreach (Panel panel in panels)
                {
                    TableCell tabGroupCell = new TableCell();
                    tabGroupCell.VerticalAlign = VerticalAlign.Top;

                    tabGroupCell.Controls.Add(panel);
                    tabGroupRow.Cells.Add(tabGroupCell);
                }

                Controls.Add(tabGroup);
            }
            else
            {
                Controls.Add(GetError(GetString("unimenubuttons.wrongdimensions")));
            }
        }
    }

    #endregion


    #region "Other methods"

    /// <summary>
    /// Parses image align from string value.
    /// </summary>
    /// <param name="align">Value to parse</param>
    /// <returns>Align of an image</returns>
    private static ImageAlign ParseImageAlign(string align)
    {
        ImageAlign imageAlign = default(ImageAlign);
        if (align != null)
        {
            try
            {
                // Try to get image align
                imageAlign = (ImageAlign)Enum.Parse(typeof(ImageAlign), align);

                // RTL switch
                if (CultureHelper.IsUICultureRTL())
                {
                    if (imageAlign == ImageAlign.Left)
                    {
                        imageAlign = ImageAlign.Right;
                    }
                    else if (imageAlign == ImageAlign.Right)
                    {
                        imageAlign = ImageAlign.Left;
                    }
                }
            }
            catch
            {
                imageAlign = ImageAlign.NotSet;
            }
        }
        else
        {
            imageAlign = ImageAlign.NotSet;
        }
        return imageAlign;
    }


    /// <summary>
    /// Generates error label.
    /// </summary>
    /// <param name="message">Error message to display</param>
    /// <returns>Label with error message</returns>
    protected Label GetError(string message)
    {
        // If error occures skip this group
        Label errorLabel = new Label();
        errorLabel.ID = "lblError" + identifier;
        errorLabel.EnableViewState = false;
        errorLabel.Text = message;
        errorLabel.CssClass = "ErrorLabel";
        return errorLabel;
    }

    #endregion
}
