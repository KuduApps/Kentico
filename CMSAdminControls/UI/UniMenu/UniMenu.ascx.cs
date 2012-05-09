using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.SiteProvider;
using CMS.CMSHelper;
using CMS.ExtendedControls;

public partial class CMSAdminControls_UI_UniMenu_UniMenu : CMSUserControl
{
    #region "Variables"

    private bool? mIsRTL = null;
    private int identifier = 0;
    private bool mShowErrors = true;
    private bool mMenuEmpty = true;

    private UIElementInfo mFirstUIElement = null;
    private UIElementInfo mHighlightedUIElement = null;

    private Panel firstPanel = null;
    private Panel preselectedPanel = null;

    #endregion


    #region "Properties"

    /// <summary>
    /// Returns the UIElementInfo representing the first button of first group displayed.
    /// </summary>
    public UIElementInfo FirstUIElement
    {
        get
        {
            return mFirstUIElement;
        }
    }


    /// <summary>
    /// Returns the UIElementInfo representing the explicitly highlighted UI element.
    /// </summary>
    public UIElementInfo HighlightedUIElement
    {
        get
        {
            return mHighlightedUIElement;
        }
    }


    /// <summary>
    /// Returns the UIElementInfo representing the selected (either explicitly highlighted or first) UI element.
    /// </summary>
    public UIElementInfo SelectedUIElement
    {
        get
        {
            return HighlightedUIElement ?? FirstUIElement;
        }
    }


    /// <summary>
    /// Indicates whether at least one group with at least one button is rendered in the menu.
    /// </summary>
    public bool MenuEmpty
    {
        get
        {
            return mMenuEmpty;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether first item should be highligted.
    /// </summary>
    public bool HighlightFirstItem
    {
        get;
        set;
    }


    /// <summary>
    /// Gets or sets the value that indicates which item should be selected (has higher priority than HighlightFirstItem).
    /// </summary>
    public string HighlightItem
    {
        get;
        set;
    }



    /// <summary>
    /// Indicates whether to remember item which was last selected and select it again.
    /// </summary>
    public bool RememberSelectedItem
    {
        get;
        set;
    }


    /// <summary>
    /// Target frameset in which the links generated from UI Elements are opened.
    /// </summary>
    public string TargetFrameset
    {
        get;
        set;
    }


    /// <summary>
    /// Gets or sets groups {[n;0] - Caption, [n;1] - Control path, [n;2] - CSS class, [n;3] - UI Element parent ID (optional)}.
    /// </summary>
    public string[,] Groups
    {
        get;
        set;
    }


    /// <summary>
    /// Description.
    /// </summary>
    public List<CMSUserControl> InnerControls
    {
        get;
        set;
    }


    /// <summary>
    /// Indicates wheter to display errors in the control.
    /// </summary>
    public bool ShowErrors
    {
        get
        {
            return mShowErrors;
        }
        set
        {
            mShowErrors = value;
        }
    }


    /// <summary>
    /// Code name of the module.
    /// </summary>
    public string ModuleName
    {
        get;
        set;
    }


    /// <summary>
    /// Indicates if current UI culture is RTL.
    /// </summary>
    public bool IsRTL
    {
        get
        {
            if (mIsRTL == null)
            {
                mIsRTL = CultureHelper.IsUICultureRTL();
            }
            return (mIsRTL == true ? true : false);
        }
    }

    #endregion


    #region "Custom events"

    /// <summary>
    /// Button filtered delegate.
    /// </summary>
    public delegate bool ButtonFilterEventHandler(UIElementInfo uiElement);


    /// <summary>
    /// Button created delegate.
    /// </summary>
    public delegate void ButtonCreatedEventHandler(UIElementInfo uiElement, string url);


    /// <summary>
    /// Button filtered event handler.
    /// </summary>
    public event ButtonFilterEventHandler OnButtonFiltered;

    /// <summary>
    /// Button created event handler.
    /// </summary>
    public event ButtonCreatedEventHandler OnButtonCreated;

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Groups != null)
        {
            ScriptHelper.RegisterJQuery(Page);
            ScriptHelper.RegisterScriptFile(Page, "jquery/jquery-tools.js");
            ScriptHelper.RegisterScriptFile(Page, "~/CMSAdminControls/UI/UniMenu/UniMenu.js");

            if (RememberSelectedItem)
            {
                StringBuilder selectionScript = new StringBuilder();
                selectionScript.AppendLine("function SelectButton(elem)");
                selectionScript.AppendLine("{");
                selectionScript.AppendLine("    var selected = 'Selected';");
                selectionScript.AppendLine("    var jElem =$j(elem);");
                selectionScript.AppendFormat("    $j('#{0}').find('.'+selected).removeClass(selected);\n", pnlControls.ClientID);
                selectionScript.AppendLine("    jElem.addClass(selected);");
                selectionScript.AppendLine("}");
                ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "UIToolbarSelectionScript", ScriptHelper.GetScript(selectionScript.ToString()));
            }

            // Generate script for activating menu buttons by script
            string buttonSelection = (RememberSelectedItem ? "SelectButton(this);" : "");
            StringBuilder remoteSelectionScript = new StringBuilder();
            remoteSelectionScript.AppendLine("var SelectedItemID = null;");
            remoteSelectionScript.AppendLine("function SelectItem(elementID, elementUrl, forceSelection)");
            remoteSelectionScript.AppendLine("{");
            remoteSelectionScript.AppendLine("  if(forceSelection === undefined) forceSelection = true;");
            remoteSelectionScript.AppendLine("  if(SelectedItemID == elementID && !forceSelection) { return; }");
            remoteSelectionScript.AppendLine("    SelectedItemID = elementID;");
            remoteSelectionScript.AppendLine("    var selected = 'Selected';");
            remoteSelectionScript.AppendFormat("    $j(\"#{0} .\"+selected).removeClass(selected);\n", pnlControls.ClientID);
            remoteSelectionScript.AppendFormat("    $j(\"#{0} div[name='\"+elementID+\"']\").addClass(selected);\n", pnlControls.ClientID);
            remoteSelectionScript.AppendLine("    if(elementUrl != null && elementUrl != '') {");
            if (!String.IsNullOrEmpty(TargetFrameset))
            {
                remoteSelectionScript.AppendLine(String.Format("{0}parent.frames['{1}'].location.href = elementUrl;", buttonSelection, TargetFrameset));
            }
            else
            {
                remoteSelectionScript.AppendLine(buttonSelection + "self.location.href = elementUrl;");
            }
            remoteSelectionScript.AppendLine("    }");
            remoteSelectionScript.AppendLine("}");
            ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "UIToolbarRemoteSelectionScript", ScriptHelper.GetScript(remoteSelectionScript.ToString()));

            ArrayList controlsList = new ArrayList();
            int groupsCount = Groups.GetUpperBound(0) + 1;
            InnerControls = new List<CMSUserControl>(groupsCount);

            // Process the groups
            for (identifier = 0; identifier < groupsCount; identifier++)
            {
                // Check array dimensions
                if (Groups.GetUpperBound(1) < 2)
                {
                    if (ShowErrors)
                    {
                        Controls.Add(GetError(GetString("unimenu.wrongdimensions")));
                    }
                    continue;
                }

                string captionText = Groups[identifier, 0];
                string contentControlPath = Groups[identifier, 1];
                string cssClass = Groups[identifier, 2];
                int uiElementId = (Groups.GetUpperBound(1) == 3 ? ValidationHelper.GetInteger(Groups[identifier, 3], 0) : 0);

                // Caption and path to content control have to be entered
                if (string.IsNullOrEmpty(captionText))
                {
                    if (ShowErrors)
                    {
                        Controls.Add(GetError(GetString("unimenu.captionempty")));
                    }
                    continue;
                }
                if (string.IsNullOrEmpty(contentControlPath) && (uiElementId <= 0))
                {
                    if (ShowErrors)
                    {
                        Controls.Add(GetError(GetString("unimenu.pathempty")));
                    }
                    continue;
                }

                CMSPanel groupPanel = new CMSPanel()
                {
                    ID = "pnlGroup" + identifier,
                    ShortID = "g" + identifier
                };

                // Add controls to main panel
                groupPanel.Controls.Add(GetLeftBorder());

                bool createGroup = false;

                if (!string.IsNullOrEmpty(contentControlPath))
                {
                    CMSUserControl contentControl = null;
                    try
                    {
                        // Try to load content control
                        contentControl = (CMSUserControl)Page.LoadControl(contentControlPath);
                        contentControl.ID = "ctrlContent" + identifier;
                        contentControl.ShortID = "c" + identifier;
                    }
                    catch
                    {
                        Controls.Add(GetError(GetString("unimenu.errorloadingcontrol")));
                        continue;
                    }
                    Panel innerPanel = GetContent(contentControl, 0, captionText);
                    if (innerPanel != null)
                    {
                        groupPanel.Controls.Add(innerPanel);
                        createGroup = true;
                    }

                    InnerControls.Insert(identifier, (CMSUserControl)contentControl);
                }
                else if (uiElementId > 0)
                {
                    Panel innerPanel = GetContent(null, uiElementId, captionText);
                    if (innerPanel != null)
                    {
                        groupPanel.Controls.Add(innerPanel);
                        createGroup = true;
                    }
                }

                groupPanel.Controls.Add(GetRightBorder());

                groupPanel.CssClass = cssClass;

                // Add group panel to list of controls
                if (createGroup)
                {
                    mMenuEmpty = false;
                    controlsList.Add(groupPanel);

                    // Insert separator after group
                    if (groupsCount > 1)
                    {
                        controlsList.Add(GetGroupSeparator());
                    }
                }
            }

            // Handle the preselection
            if (preselectedPanel != null)
            {
                // Add the selected class to the preselected button
                preselectedPanel.CssClass += " Selected";
            }
            else if ((firstPanel != null) && HighlightFirstItem)
            {
                // Add the selected class to the first button
                firstPanel.CssClass += " Selected";
            }

            // Add group panels to to the control
            foreach (Control control in controlsList)
            {
                pnlControls.Controls.Add(control);
            }
        }
        else
        {
            Controls.Add(GetError(GetString("unimenu.wrongdimensions")));
        }
    }


    /// <summary>
    /// Generates div with left border.
    /// </summary>
    /// <returns>Panel with left border</returns>
    protected Panel GetLeftBorder()
    {
        // Create panel and set up
        return new CMSPanel()
        {
            ID = "lb" + identifier,
            EnableViewState = false,
            CssClass = "UniMenuLeftBorder"
        };
    }


    /// <summary>
    /// Generates div with right border.
    /// </summary>
    /// <returns>Panel with right border</returns>
    protected Panel GetRightBorder()
    {
        // Create panel and set up
        return new CMSPanel()
        {
            ID = "pnlRightBorder" + identifier,
            ShortID = "rb" + identifier,
            EnableViewState = false,
            CssClass = "UniMenuRightBorder"
        };
    }


    /// <summary>
    /// Generates separator between groups.
    /// </summary>
    /// <returns>Panel separating groups</returns>
    protected Panel GetGroupSeparator()
    {
        // Create panel and set up
        return new CMSPanel()
        {
            ID = "pnlGroupSeparator" + identifier,
            ShortID = "gs" + identifier,
            EnableViewState = false,
            CssClass = "UniMenuSeparator"
        };
    }


    /// <summary>
    /// Generates div with right border.
    /// </summary>
    /// <param name="captionText">Caption of group</param>
    /// <returns>Panel with right border</returns>
    protected Panel GetCaption(string captionText)
    {
        // Create literal with caption
        Literal caption = new Literal()
        {
            ID = "ltlCaption" + identifier,
            EnableViewState = false,
            Text = captionText
        };

        // Create panel and add caption literal
        CMSPanel captionPanel = new CMSPanel()
        {
            ID = "pnlCaption" + identifier,
            ShortID = "cp" + identifier,
            EnableViewState = false,
            CssClass = "UniMenuDescription"
        };
        captionPanel.Controls.Add(caption);

        return captionPanel;
    }


    /// <summary>
    /// Generates panel with buttons loaded from given UI Element.
    /// </summary>
    /// <param name="uiElementId">ID of the UI Element</param>
    protected Panel GetButtons(int uiElementId)
    {
        const int bigButtonMinimalWidth = 40;
        const int smallButtonMinimalWidth = 66;

        Panel pnlButtons = null;

        // Load the buttons manually from UI Element
        DataSet ds = UIElementInfoProvider.GetChildUIElements(uiElementId);

        // When no child found
        if (DataHelper.DataSourceIsEmpty(ds))
        {
            // Try to use group element as button
            ds = UIElementInfoProvider.GetUIElements("ElementID = " + uiElementId, null);

            if (!DataHelper.DataSourceIsEmpty(ds))
            {
                DataRow dr = ds.Tables[0].Rows[0];
                string url = ValidationHelper.GetString(dr["ElementTargetURL"], "");

                // Use group element as button only if it has URL specified
                if (string.IsNullOrEmpty(url))
                {
                    ds = null;
                }
            }
        }

        if (!DataHelper.DataSourceIsEmpty(ds))
        {
            // Filter the dataset according to UI Profile
            FilterElements(ds);

            int small = 0;
            int count = ds.Tables[0].Rows.Count;

            // No buttons, render nothing
            if (count == 0)
            {
                return null;
            }

            // Prepare the panel
            pnlButtons = new Panel();
            pnlButtons.CssClass = "ActionButtons";

            // Prepare the table
            Table tabGroup = new Table();
            TableRow tabGroupRow = new TableRow();

            tabGroup.CellPadding = 0;
            tabGroup.CellSpacing = 0;
            tabGroup.EnableViewState = false;
            tabGroupRow.EnableViewState = false;
            tabGroup.Rows.Add(tabGroupRow);

            List<Panel> panels = new List<Panel>();

            for (int i = 0; i < count; i++)
            {
                // Get current and next button
                UIElementInfo uiElement = new UIElementInfo(ds.Tables[0].Rows[i]);
                UIElementInfo uiElementNext = null;
                if (i < count - 1)
                {
                    uiElementNext = new UIElementInfo(ds.Tables[0].Rows[i + 1]);
                }

                // Set the first button
                if (mFirstUIElement == null)
                {
                    mFirstUIElement = uiElement;
                }

                // Get the sizes of current and next button. Button is large when it is the only in the group
                bool isSmall = (uiElement.ElementSize == UIElementSizeEnum.Regular) && (count > 1);
                bool isResized = (uiElement.ElementSize == UIElementSizeEnum.Regular) && (!isSmall);
                bool isNextSmall = (uiElementNext != null) && (uiElementNext.ElementSize == UIElementSizeEnum.Regular);

                // Set the CSS class according to the button size
                string cssClass = (isSmall ? "SmallButton" : "BigButton");
                string elementName = uiElement.ElementName;

                // Create main button panel
                CMSPanel pnlButton = new CMSPanel()
                {
                    ID = "pnlButton" + elementName,
                    ShortID = "b" + elementName
                };

                pnlButton.Attributes.Add("name", elementName);
                pnlButton.CssClass = cssClass;

                // Remember the first button
                if (firstPanel == null)
                {
                    firstPanel = pnlButton;
                }

                // Remember the selected button
                if ((preselectedPanel == null) && elementName.Equals(HighlightItem, StringComparison.InvariantCultureIgnoreCase))
                {
                    preselectedPanel = pnlButton;

                    // Set the selected button
                    if (mHighlightedUIElement == null)
                    {
                        mHighlightedUIElement = uiElement;
                    }
                }

                // URL or behavior
                string url = uiElement.ElementTargetURL;

                if (!string.IsNullOrEmpty(url) && url.StartsWith("javascript:", StringComparison.InvariantCultureIgnoreCase))
                {
                    pnlButton.Attributes["onclick"] = url.Substring("javascript:".Length);
                }
                else if (!string.IsNullOrEmpty(url) && !url.StartsWith("javascript:", StringComparison.InvariantCultureIgnoreCase))
                {
                    string buttonSelection = (RememberSelectedItem ? "SelectButton(this);" : "");

                    // Ensure hash code if required
                    string targetUrl = CMSContext.ResolveMacros(URLHelper.EnsureHashToQueryParameters(url));

                    if (!String.IsNullOrEmpty(TargetFrameset))
                    {
                        pnlButton.Attributes["onclick"] = String.Format("{0}parent.frames['{1}'].location.href = '{2}';", buttonSelection, TargetFrameset, URLHelper.ResolveUrl(targetUrl));
                    }
                    else
                    {
                        pnlButton.Attributes["onclick"] = String.Format("{0}self.location.href = '{1}';", buttonSelection, URLHelper.ResolveUrl(targetUrl));
                    }

                    if (OnButtonCreated != null)
                    {
                        OnButtonCreated(uiElement, targetUrl);
                    }
                }

                // Tooltip
                if (!string.IsNullOrEmpty(uiElement.ElementDescription))
                {
                    pnlButton.ToolTip = ResHelper.LocalizeString(uiElement.ElementDescription);
                }
                else
                {
                    pnlButton.ToolTip = ResHelper.LocalizeString(uiElement.ElementCaption);
                }
                pnlButton.EnableViewState = false;

                // Ensure correct grouping of small buttons
                if (isSmall && (small == 0))
                {
                    small++;

                    Panel pnlSmallGroup = new Panel()
                    {
                        ID = "pnlGroupSmall" + uiElement.ElementName
                    };
                    if (IsRTL)
                    {
                        pnlSmallGroup.Style.Add("float", "right");
                        pnlSmallGroup.Style.Add("text-align", "right");
                    }
                    else
                    {
                        pnlSmallGroup.Style.Add("float", "left");
                        pnlSmallGroup.Style.Add("text-align", "left");
                    }

                    pnlSmallGroup.EnableViewState = false;
                    pnlSmallGroup.Controls.Add(pnlButton);
                    panels.Add(pnlSmallGroup);

                }

                // Generate button image
                Image buttonImage = new Image()
                {
                    ID = "imgButton" + uiElement.ElementName,
                    ImageAlign = (isSmall ? ImageAlign.AbsMiddle : ImageAlign.Top)
                };
                if (!string.IsNullOrEmpty(uiElement.ElementIconPath))
                {
                    string iconPath = GetImageUrl(uiElement.ElementIconPath);
                    
                    // Check if element size was changed
                    if(isResized)
                    {
                        // Try to get larger icon
                        string largeIconPath = iconPath.Replace("list.png", "module.png");
                        try
                        {
                            if (CMS.IO.File.Exists(MapPath(largeIconPath)))
                            {
                                iconPath = largeIconPath;
                            }
                        }
                        catch { }
                    }

                    buttonImage.ImageUrl = iconPath;
                }
                else
                {
                    // Load defaul module icon if ElementIconPath is not specified
                    buttonImage.ImageUrl = GetImageUrl("CMSModules/module.png");
                }
                buttonImage.EnableViewState = false;

                // Generate button text
                Literal captionLiteral = new Literal()
                {
                    ID = "ltlCaption" + uiElement.ElementName,
                    Text = (isSmall ? "\n" : "<br />") + ResHelper.LocalizeString(uiElement.ElementCaption),
                    EnableViewState = false
                };

                // Generate button link
                HyperLink buttonLink = new HyperLink()
                {
                    ID = "lnkButton" + uiElement.ElementName
                };
                buttonLink.Controls.Add(buttonImage);
                buttonLink.Controls.Add(captionLiteral);
                buttonLink.EnableViewState = false;

                //Generate button table (IE7 issue)
                Table tabButton = new Table();
                TableRow tabRow = new TableRow();
                TableCell tabCellLeft = new TableCell();
                TableCell tabCellMiddle = new TableCell();
                TableCell tabCellRight = new TableCell();

                tabButton.CellPadding = 0;
                tabButton.CellSpacing = 0;

                tabButton.EnableViewState = false;
                tabRow.EnableViewState = false;
                tabCellLeft.EnableViewState = false;
                tabCellMiddle.EnableViewState = false;
                tabCellRight.EnableViewState = false;

                tabButton.Rows.Add(tabRow);
                tabRow.Cells.Add(tabCellLeft);
                tabRow.Cells.Add(tabCellMiddle);
                tabRow.Cells.Add(tabCellRight);

                // Generate left border
                Panel pnlLeft = new Panel()
                {
                    ID = "pnlLeft" + uiElement.ElementName,
                    CssClass = "Left" + cssClass,
                    EnableViewState = false
                };

                // Generate middle part of button
                Panel pnlMiddle = new Panel()
                {
                    ID = "pnlMiddle" + uiElement.ElementName,
                    CssClass = "Middle" + cssClass
                };
                pnlMiddle.Controls.Add(buttonLink);
                Panel pnlMiddleTmp = new Panel()
                {
                    EnableViewState = false
                };
                if (isSmall)
                {
                    pnlMiddle.Style.Add("min-width", smallButtonMinimalWidth + "px");
                    // IE7 issue with min-width
                    pnlMiddleTmp.Style.Add("width", smallButtonMinimalWidth + "px");
                    pnlMiddle.Controls.Add(pnlMiddleTmp);
                }
                else
                {
                    pnlMiddle.Style.Add("min-width", bigButtonMinimalWidth + "px");
                    // IE7 issue with min-width
                    pnlMiddleTmp.Style.Add("width", bigButtonMinimalWidth + "px");
                    pnlMiddle.Controls.Add(pnlMiddleTmp);
                }
                pnlMiddle.EnableViewState = false;

                // Generate right border
                Panel pnlRight = new Panel()
                {
                    ID = "pnlRight" + uiElement.ElementName,
                    CssClass = "Right" + cssClass,
                    EnableViewState = false
                };

                // Add inner controls
                tabCellLeft.Controls.Add(pnlLeft);
                tabCellMiddle.Controls.Add(pnlMiddle);
                tabCellRight.Controls.Add(pnlRight);

                pnlButton.Controls.Add(tabButton);

                // If there were two small buttons in a row end the grouping div
                if ((small == 2) || (isSmall && !isNextSmall))
                {
                    small = 0;

                    // Add the button to the small buttons grouping panel
                    panels[panels.Count - 1].Controls.Add(pnlButton);
                }
                else
                {
                    if (small == 0)
                    {
                        // Add the generated button into collection
                        panels.Add(pnlButton);
                    }
                }
                if (small == 1)
                {
                    small++;
                }
            }

            // Add all panels to control
            foreach (Panel panel in panels)
            {
                TableCell tabGroupCell = new TableCell()
                {
                    VerticalAlign = VerticalAlign.Top,
                    EnableViewState = false
                };

                tabGroupCell.Controls.Add(panel);
                tabGroupRow.Cells.Add(tabGroupCell);
            }

            pnlButtons.Controls.Add(tabGroup);
        }

        return pnlButtons;
    }


    /// <summary>
    /// Generates panel with content and caption.
    /// </summary>
    /// <param name="control">Control to add to the content</param>
    /// <param name="captionText">Caption of group</param>
    /// <param name="uiElementId">ID of the UI Element, if the content should be loaded from UI Elements</param>
    protected Panel GetContent(Control control, int uiElementId, string captionText)
    {
        // Create panel and set up
        Panel content = new Panel()
        {
            ID = "pnlContent" + identifier,
            CssClass = "UniMenuContent"
        };

        // Add inner content control
        if (control != null)
        {
            content.Controls.Add(control);
        }
        else if (uiElementId > 0)
        {
            Panel innerPanel = GetButtons(uiElementId);
            if (innerPanel == null)
            {
                return null;
            }
            else
            {
                content.Controls.Add(innerPanel);
            }
        }

        // Add caption
        content.Controls.Add(GetCaption(captionText));
        return content;
    }


    /// <summary>
    /// Generates error label.
    /// </summary>
    /// <param name="message">Error message to display</param>
    /// <returns>Label with error message</returns>
    protected Label GetError(string message)
    {
        // If error occures skip this group
        return new Label()
        {
            ID = "lblError" + identifier,
            EnableViewState = false,
            Text = message,
            CssClass = "ErrorLabel"
        };
    }


    /// <summary>
    /// Filters the dataset with UI Elements according to UI Profile of current user by default and according to custom event (if defined).
    /// </summary>
    protected void FilterElements(DataSet dsElements)
    {
        // For all tables in dataset
        foreach (DataTable dt in dsElements.Tables)
        {
            ArrayList deleteRows = new ArrayList();

            // Find rows to filter out
            foreach (DataRow dr in dt.Rows)
            {
                UIElementInfo uiElement = new UIElementInfo(dr);
                bool allowed = CMSContext.CurrentUser.IsAuthorizedPerUIElement(ModuleName, uiElement.ElementName);

                if (OnButtonFiltered != null)
                {
                    allowed = allowed && OnButtonFiltered(uiElement);
                }

                if (!allowed)
                {
                    deleteRows.Add(dr);
                }
            }

            // Delete the filtered rows
            foreach (DataRow dr in deleteRows)
            {
                dt.Rows.Remove(dr);
            }
        }
    }

    #endregion
}
