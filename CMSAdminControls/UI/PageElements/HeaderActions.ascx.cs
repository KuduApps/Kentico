using System;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.GlobalHelper;
using CMS.UIControls;

public partial class CMSAdminControls_UI_PageElements_HeaderActions : HeaderActions
{
    #region "Variables"

    private string[,] mActions = null;
    private string mIconCssClass = "NewItemImage";
    private string mLinkCssClass = "NewItemLink";
    private string mPanelCssClass = null;
    private int mSeparatorWidth = 20;
    private bool mEnabled = true;
    private bool mUseImageButton = false;

    #endregion


    #region "Properties"

    /// <summary>
    /// Gets or sets the array of actions. The meaning of the indexes:
    /// (0,0): Type of the link (HyperLink or LinkButton), 
    /// (0,1): Text of the action link, 
    /// (0,2): JavaScript command to be performed OnClick action, 
    /// (0,3): NavigationUrl of the HyperLink (or PostBackUrl of LinkButton), 
    /// (0,4): Tooltip of the action link, 
    /// (0,5): Action image url, 
    /// (0,6): CommandName of the LinkButton Command event, 
    /// (0,7): CommandArgument of the LinkButton Command event.
    /// (0,8): Register shortcut action (TRUE/FALSE)
    /// (0,9): Enabled state of the action (TRUE/FALSE)
    /// (0,10): Visibility of the action (TRUE/FALSE)
    /// (0,11): Hyperlink target (only if type is hyperlink).
    /// (0,12): Use ImageButton instead of Image (TRUE/FALSE).
    /// At least first two arguments must be defined.
    /// </summary>
    public override string[,] Actions
    {
        get
        {
            return mActions;
        }
        set
        {
            mActions = value;
        }
    }


    /// <summary>
    /// Gets or sets UseImageButton property. If set to <c>true</c> rendered image icon will fire action too.
    /// </summary>
    public override bool UseImageButton
    {
        get
        {
            return mUseImageButton;
        }
        set
        {
            mUseImageButton = value;
        }
    }


    /// <summary>
    /// Gets or sets the enabled property of all actions.
    /// </summary>
    public override bool Enabled
    {
        get
        {
            return mEnabled;
        }
        set
        {
            Panel pnlActions = FindControl("pnlActions") as Panel;
            if (pnlActions != null)
            {
                foreach (Control c in pnlActions.Controls)
                {
                    LinkButton lnkButton = c as LinkButton;
                    if (lnkButton != null)
                    {
                        lnkButton.Enabled = value;
                        lnkButton.CssClass = (value ? LinkCssClass : (LinkCssClass + "Disabled"));
                        continue;
                    }
                    HyperLink lnkLink = c as HyperLink;
                    if (lnkLink != null)
                    {
                        lnkLink.Enabled = value;
                        lnkLink.CssClass = (value ? LinkCssClass : (LinkCssClass + "Disabled"));
                        continue;
                    }
                    ImageButton lnkImage = c as ImageButton;
                    if ((lnkImage != null) && UseImageButton)
                    {
                        lnkImage.Enabled = value;
                        lnkImage.CssClass = (value ? LinkCssClass : (LinkCssClass + "Disabled"));
                        continue;
                    }
                }
                mEnabled = value;
            }
        }
    }


    /// <summary>
    /// Gets or sets the space between two actions (in pixels).
    /// </summary>
    public override int SeparatorWidth
    {
        get
        {
            return mSeparatorWidth;
        }
        set
        {
            mSeparatorWidth = value;
        }
    }


    /// <summary>
    /// Gets or sets CssClass of the Image element of the action.
    /// </summary>
    public override string IconCssClass
    {
        get
        {
            return mIconCssClass;
        }
        set
        {
            mIconCssClass = value;
        }
    }


    /// <summary>
    /// Gets or sets CssClass of the HyperLink or LinkButton element of the action.
    /// </summary>
    public override string LinkCssClass
    {
        get
        {
            return mLinkCssClass;
        }
        set
        {
            mLinkCssClass = value;
        }
    }


    /// <summary>
    /// Gets or sets CssClass of the panel where all the actions are placed.
    /// </summary>
    public override string PanelCssClass
    {
        get
        {
            return mPanelCssClass;
        }
        set
        {
            mPanelCssClass = value;
        }
    }


    /// <summary>
    /// Help topic name.
    /// </summary>
    public override string HelpTopicName
    {
        get
        {
            return helpElem.TopicName;
        }
        set
        {
            helpElem.TopicName = value;
        }
    }


    /// <summary>
    /// Help name.
    /// </summary>
    public override string HelpName
    {
        get
        {
            return helpElem.HelpName;
        }
        set
        {
            helpElem.HelpName = value;
        }
    }


    /// <summary>
    /// Help icon URL.
    /// </summary>
    public override string HelpIconUrl
    {
        get
        {
            return helpElem.IconUrl;
        }
        set
        {
            helpElem.IconUrl = value;
        }
    }

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        ReloadData();
    }


    /// <summary>
    /// Reloads the actions.
    /// </summary>
    public override void ReloadData()
    {
        if (mActions != null)
        {
            pnlActions.Controls.Clear();

            if (!String.IsNullOrEmpty(mPanelCssClass))
            {
                pnlActions.CssClass = mPanelCssClass;
            }

            // Get the number of actions
            int actionCount = mActions.GetUpperBound(0) + 1;

            // Get the array size (number of arguments)
            int arraySize = mActions.GetUpperBound(1) + 1;

            // Exit if nothing about the action is specified
            if (arraySize < 2)
            {
                return;
            }

            // If there is more then one action render them to table
            bool useTable = (actionCount > 1);
            if (useTable)
            {
                pnlActions.Controls.Add(new LiteralControl(@"<table cellpadding=""0"" cellspacing=""0""><tr>"));
            }

            // Generate the actions
            int count = 0;

            for (int i = 0; i < actionCount; i++)
            {
                // If the caption is not specified or visibility is false, skip the action
                string caption = (mActions[i, 1] ?? null);
                bool visible = (!(arraySize > 10) || ValidationHelper.GetBoolean(mActions[i, 10], true));
                if ((caption == null) || !visible)
                {
                    // Skip empty action
                    continue;
                }

                // Start tag of the table if more then one action is defined
                if (useTable)
                {
                    pnlActions.Controls.Add(new LiteralControl("<td>"));
                }

                // Get the action parameters
                bool useLinkButton = ((mActions[i, 0] != null) && (mActions[i, 0].ToLower() == TYPE_LINKBUTTON));
                bool useSaveButton = ((mActions[i, 0] != null) && (mActions[i, 0].ToLower() == TYPE_SAVEBUTTON));
                string javascript = ((arraySize > 2) ? mActions[i, 2] : null);
                string url = ((arraySize > 3) ? mActions[i, 3] : null);
                string tooltip = ((arraySize > 4) ? mActions[i, 4] : null);
                string iconUrl = ((arraySize > 5) ? mActions[i, 5] : null);
                string actionParamName = ((arraySize > 6) ? mActions[i, 6] : null);
                string actionParamArg = ((arraySize > 7) ? mActions[i, 7] : null);
                bool registerScript = ((arraySize > 8) && ValidationHelper.GetBoolean(mActions[i, 8], false));
                bool enabled = (!(arraySize > 9) || ValidationHelper.GetBoolean(mActions[i, 9], true));
                string target = ((arraySize > 11) ? ValidationHelper.GetString(mActions[i, 11], "") : "");
                bool useImageButton = ((arraySize > 12) && ValidationHelper.GetBoolean(mActions[i, 12], false));

                url = URLHelper.ResolveUrl(url);

                // Create image if url is not empty and add it to panel
                Image img = null;
                if (iconUrl != null)
                {
                    img = new Image();
                    img.ImageUrl = iconUrl;
                    img.ToolTip = tooltip;
                    img.AlternateText = caption;
                    img.CssClass = mIconCssClass;
                    img.Enabled = enabled;
                }

                ImageButton imgButton = null;
                if (iconUrl != null)
                {
                    imgButton = new ImageButton();
                    imgButton.ID = ID + "HeaderActionImage" + i.ToString();
                    imgButton.ImageUrl = iconUrl;
                    imgButton.OnClientClick = javascript;
                    imgButton.ToolTip = tooltip;
                    imgButton.AlternateText = caption;
                    imgButton.CssClass = mIconCssClass;
                    imgButton.Enabled = enabled;
                    imgButton.PostBackUrl = url;
                    imgButton.CommandName = actionParamName;
                    imgButton.CommandArgument = actionParamArg;
                    imgButton.Command += RaiseActionPerformed;
                }


                if (!useSaveButton && !useImageButton && (img != null))
                {
                    pnlActions.Controls.Add(img);
                }
                else if (!useSaveButton && useImageButton && (imgButton != null))
                {
                    pnlActions.Controls.Add(imgButton);
                }

                if (useLinkButton)
                {
                    // Add LinkButton
                    LinkButton link = new LinkButton();
                    link.ID = ID + "HeaderAction" + i.ToString();
                    link.Text = caption;
                    link.OnClientClick = javascript;
                    link.PostBackUrl = url;
                    link.ToolTip = tooltip;
                    link.CommandName = actionParamName;
                    link.CommandArgument = actionParamArg;
                    link.CssClass = enabled ? LinkCssClass : (LinkCssClass + "Disabled");
                    link.Enabled = enabled;
                    link.Command += RaiseActionPerformed;
                    pnlActions.Controls.Add(link);
                }
                else if (useSaveButton)
                {
                    // Add LinkButton
                    LinkButton link = new LinkButton();
                    link.OnClientClick = javascript;
                    link.ID = ID + "HeaderAction" + i.ToString();
                    link.PostBackUrl = url;
                    link.ToolTip = tooltip;
                    link.CommandName = actionParamName;
                    link.CommandArgument = actionParamArg;
                    link.CssClass = enabled ? LinkCssClass : (LinkCssClass + "Disabled");
                    link.Enabled = enabled;
                    link.Command += RaiseActionPerformed;

                    if ((img != null) && !useImageButton)
                    {
                        link.Controls.Add(img);
                    }
                    else if ((imgButton != null) && useImageButton)
                    {
                        link.Controls.Add(imgButton);
                    }

                    link.Controls.Add(new LiteralControl(caption));
                    pnlActions.Controls.Add(link);

                    // Register the CRTL+S shortcut
                    if (registerScript)
                    {
                        ScriptHelper.RegisterSaveShortcut(link, actionParamArg, false);
                    }
                }
                else
                {
                    // Add HyperLink
                    HyperLink link = new HyperLink();
                    link.Text = caption;
                    link.Attributes.Add("onclick", javascript);
                    if (!String.IsNullOrEmpty(url))
                    {
                        link.NavigateUrl = url;
                    }
                    else
                    {
                        link.Attributes.Add("href", "#");
                    }
                    link.ToolTip = tooltip;
                    link.CssClass = enabled ? LinkCssClass : (LinkCssClass + "Disabled");
                    link.Enabled = enabled;
                    link.Target = target;
                    pnlActions.Controls.Add(link);
                }

                // Add free separator cell in table if it's not the last action
                if (useTable)
                {
                    pnlActions.Controls.Add(new LiteralControl((i < actionCount - 1 ? "</td><td style=\"width:" + mSeparatorWidth + "px; \" />" : "</td>")));
                }

                count++;
            }

            // End tag of the table
            if (useTable)
            {
                pnlActions.Controls.Add(new LiteralControl("</tr></table>"));
            }

            Visible = (count > 0);
        }
        else
        {
            Visible = false;
        }

        // Use help icon
        helpElem.Visible = !string.IsNullOrEmpty(HelpTopicName);
    }


    /// <summary>
    /// Clears content rendered by header actions control.
    /// </summary>
    public void Clear()
    {
        pnlActions.Controls.Clear();
    }

    #endregion
}