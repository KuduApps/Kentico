using System;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.SettingsProvider;
using CMS.DataEngine;

public partial class CMSAdminControls_UI_PageElements_Help : HelpControl
{
    #region "Variables"

    protected string mTopicName = "";
    protected string mHelpName = null;
    protected string mIconName = "helplarge.png";
    protected string mIconUrl = null;
    protected string mText = null;

    #endregion


    #region "Public properties"

    /// <summary>
    /// Documentation URL.
    /// </summary>
    public override string DocumentationUrl
    {
        get
        {
            return UIHelper.DocumentationUrl;
        }
    }


    /// <summary>
    /// Help name to identify the help within the javascript.
    /// </summary>
    public override string HelpName
    {
        get
        {
            if (mHelpName == null)
            {
                return ID;
            }
            else
            {
                return mHelpName;
            }
        }
        set
        {
            mHelpName = value;
        }
    }


    /// <summary>
    /// Help topic.
    /// </summary>
    public override string TopicName
    {
        get
        {
            return mTopicName;
        }
        set
        {
            mTopicName = value;
        }
    }


    /// <summary>
    /// Text.
    /// </summary>
    public override string Text
    {
        get
        {
            return mText;
        }
        set
        {
            mText = value;
        }
    }


    /// <summary>
    /// Tooltip.
    /// </summary>
    public override string Tooltip
    {
        get
        {
            if (ViewState["Tooltip"] == null)
            {
                if (SqlHelper.IsDatabaseAvailable)
                {
                    return GetString("Help.Tooltip");
                }
                else
                {
                    return ResHelper.GetFileString("Help.Tooltip");
                }
            }
            return (string)ViewState["Tooltip"];
        }
        set
        {
            ViewState["Tooltip"] = value;
        }
    }


    /// <summary>
    /// Icon name.
    /// </summary>
    public override string IconName
    {
        get
        {
            return mIconName;
        }
        set
        {
            mIconName = value;
        }
    }


    /// <summary>
    /// Icon URL.
    /// </summary>
    public override string IconUrl
    {
        get
        {
            return mIconUrl;
        }
        set
        {
            mIconUrl = value;
        }
    }

    #endregion


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        if (TopicName != string.Empty)
        {
            // Render the help icon
            Visible = true;

            if (Text != null)
            {
                lnkHelp.Text = Text;
            }
            else
            {
                if (string.IsNullOrEmpty(IconUrl))
                {
                    imgHelp.ImageUrl = GetImageUrl("General/HelpLarge.png");
                }
                else
                {
                    imgHelp.ImageUrl = IconUrl;
                }
                imgHelp.ToolTip = Tooltip;
                imgHelp.AlternateText = Tooltip;
            }
            lnkHelp.NavigateUrl = UIHelper.GetContextHelpUrl(TopicName);

            // Render help name script
            if (!String.IsNullOrEmpty(HelpName))
            {
                string script =
                @"var hLoc = new Array();
                function SetHelpTopic(name, topic) {
                    var l = hLoc[name][0];
                    if (l != null) {
                        l.href = hLoc[name][1].replace(""##TOPIC##"", topic);
                    }
                }
                hLoc['" + HelpName + "'] = new Array(document.getElementById('" + lnkHelp.ClientID + "'), '" + ResolveUrl(this.DocumentationUrl).TrimEnd('/') + "/##TOPIC##.htm');";

                ltlScript.Text = ScriptHelper.GetScript(script);
            }
        }
        else
        {
            Visible = false;
        }
    }
}
