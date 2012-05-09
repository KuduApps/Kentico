using System;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.PortalControls;
using CMS.GlobalHelper;

public partial class CMSWebParts_CommunityServices_LiveWebMessenger : CMSAbstractWebPart
{
    #region "Public properties"

    /// <summary>
    /// Unique ID from windows live messenger web.
    /// </summary>
    public string MessengerID
    {
        get
        {
            return ValidationHelper.GetString(GetValue("MessengerID"), "");
        }
        set
        {
            SetValue("MessengerID", value);
        }
    }


    /// <summary>
    /// Type of the rendered control.
    /// </summary>
    public string ControlType
    {
        get
        {
            return ValidationHelper.GetString(GetValue("ControlType"), "imwindow");
        }
        set
        {
            SetValue("ControlType", value);
        }
    }


    /// <summary>
    /// Theme color of the control.
    /// </summary>
    public string Theme
    {
        get
        {
            return ValidationHelper.GetString(GetValue("Theme"), "default");
        }
        set
        {
            SetValue("Theme", value);
        }
    }


    /// <summary>
    /// Width of the IM window.
    /// </summary>
    public int Width
    {
        get
        {
            return ValidationHelper.GetInteger(GetValue("Width"), 300);
        }
        set
        {
            SetValue("Width", value);
        }
    }


    /// <summary>
    /// Height of the IM window.
    /// </summary>
    public int Height
    {
        get
        {
            return ValidationHelper.GetInteger(GetValue("Height"), 300);
        }
        set
        {
            SetValue("Height", value);
        }
    }

    #endregion


    #region "Page events"

    /// <summary>
    /// Loads the web part content.
    /// </summary>
    public override void OnContentLoaded()
    {
        base.OnContentLoaded();
        SetupControl();
    }

    #endregion


    #region "Other methods"

    /// <summary>
    /// Sets up the control.
    /// </summary>
    protected void SetupControl()
    {
        if (StopProcessing)
        {
            // Do nothing
        }
        else
        {
            if (MessengerID != "")
            {
                string width = Width.ToString();
                string height = Height.ToString();
                string culture = CultureHelper.GetPreferredCulture();
                string mscBackColor = "";
                string src = "";

                switch (Theme)
                {
                    case "default":
                        src = "";
                        mscBackColor = "#D7E8EC";
                        break;

                    case "blue":
                        src = "&amp;useTheme=true&amp;themeName=blue&amp;foreColor=333333&amp;backColor=E8F1F8&amp;linkColor=333333&amp;borderColor=AFD3EB&amp;";
                        src += "buttonForeColor=333333&amp;buttonBackColor=EEF7FE&amp;buttonBorderColor=AFD3EB&amp;buttonDisabledColor=EEF7FE&amp;headerForeColor=0066A7&amp;";
                        src += "headerBackColor=8EBBD8&amp;menuForeColor=333333&amp;menuBackColor=FFFFFF&amp;chatForeColor=333333&amp;chatBackColor=FFFFFF&amp;";
                        src += "chatDisabledColor=F6F6F6&amp;chatErrorColor=760502&amp;chatLabelColor=6E6C6C";
                        mscBackColor = "#77ADCF";
                        break;
                    case "green":
                        src = "&amp;useTheme=true&amp;themeName=green&amp;foreColor=333333&amp;backColor=DCF2E5&amp;linkColor=333333&amp;borderColor=8ED4AB&amp;";
                        src += "buttonForeColor=2C0034&amp;buttonBackColor=CFE9D9&amp;buttonBorderColor=8ED4AB&amp;buttonDisabledColor=CFE9D9&amp;headerForeColor=006629&amp;";
                        src += "headerBackColor=92D6AE&amp;menuForeColor=006629&amp;menuBackColor=FFFFFF&amp;chatForeColor=333333&amp;chatBackColor=F4FBF7&amp;";
                        src += "chatDisabledColor=F6F6F6&amp;chatErrorColor=760502&amp;chatLabelColor=6E6C6C";
                        mscBackColor = "#92D6AE";
                        break;
                    case "orange":
                        src = "&amp;useTheme=true&amp;themeName=orange&amp;foreColor=333333&amp;backColor=FDC098&amp;linkColor=333333&amp;borderColor=FB8233&amp;";
                        src += "buttonForeColor=333333&amp;buttonBackColor=FFC9A5&amp;buttonBorderColor=FB8233&amp;buttonDisabledColor=FFC9A5&amp;headerForeColor=333333&amp;";
                        src += "headerBackColor=FC9E60&amp;menuForeColor=333333&amp;menuBackColor=FFFFFF&amp;chatForeColor=333333&amp;chatBackColor=FFFFFF&amp;";
                        src += "chatDisabledColor=F6F6F6&amp;chatErrorColor=760502&amp;chatLabelColor=6E6C6C";
                        mscBackColor = "#FC9E60";
                        break;
                    case "pink":
                        src = "&amp;useTheme=true&amp;themeName=pink&amp;foreColor=444444&amp;backColor=FFD5D5&amp;linkColor=444444&amp;borderColor=ED7B7B&amp;";
                        src += "buttonForeColor=AA3636&amp;buttonBackColor=FAD6D6&amp;buttonBorderColor=AA3636&amp;buttonDisabledColor=FAD6D6&amp;headerForeColor=444444&amp;";
                        src += "headerBackColor=F9A3A3&amp;menuForeColor=E45A5A&amp;menuBackColor=FFFFFF&amp;chatForeColor=444444&amp;chatBackColor=FEF6F6&amp;";
                        src += "chatDisabledColor=F6F6F6&amp;chatErrorColor=760502&amp;chatLabelColor=6E6C6C";
                        mscBackColor = "#F9A3A3";
                        break;
                    case "purple":
                        src = "&amp;useTheme=true&amp;themeName=purple&amp;foreColor=333333&amp;backColor=F1EFF4&amp;linkColor=333333&amp;borderColor=AFA9B4&amp;";
                        src += "buttonForeColor=333333&amp;buttonBackColor=DED6DE&amp;buttonBorderColor=AFA9B4&amp;buttonDisabledColor=DED6DE&amp;headerForeColor=513663&amp;";
                        src += "headerBackColor=AEA1B9&amp;menuForeColor=333333&amp;menuBackColor=FFFFFF&amp;chatForeColor=333333&amp;chatBackColor=FFFFFF&amp;";
                        src += "chatDisabledColor=F6F6F6&amp;chatErrorColor=760502&amp;chatLabelColor=6E6C6C";
                        mscBackColor = "#AEA1B9";
                        break;
                    case "gray":
                        src = "&amp;useTheme=true&amp;themeName=gray&amp;foreColor=676769&amp;backColor=DBDBDB&amp;linkColor=444444&amp;borderColor=8D8D8D&amp;";
                        src += "buttonForeColor=99CC33&amp;buttonBackColor=676769&amp;buttonBorderColor=99CC33&amp;buttonDisabledColor=F1F1F1&amp;headerForeColor=729527&amp;";
                        src += "headerBackColor=B2B2B2&amp;menuForeColor=676769&amp;menuBackColor=BBBBBB&amp;chatForeColor=99CC33&amp;chatBackColor=EAEAEA&amp;";
                        src += "chatDisabledColor=B2B2B2&amp;chatErrorColor=760502&amp;chatLabelColor=6E6C6C";
                        mscBackColor = "#DBDBDB";
                        break;
                }

                if (ControlType == "imwindow")
                {
                    ltlWindowsMessenger.Text = "<iframe src=\"http://settings.messenger.live.com/Conversation/IMMe.aspx?invitee=" + MessengerID;
                    ltlWindowsMessenger.Text += "@apps.messenger.live.com&amp;mkt=" + culture + src + "\" width=\"" + width + "\" height=\"" + height;
                    ltlWindowsMessenger.Text += "\" style=\"border: solid 1px black; width: " + width + "px; height: " + height + "px;\" frameborder=\"0\" scrolling=\"no\"></iframe>";
                }
                else if (ControlType == "button")
                {
                    ltlWindowsMessenger.Text = "<script type=\"text/javascript\" src=\"http://settings.messenger.live.com/controls/1.0/PresenceButton.js\"></script>";
                    ltlWindowsMessenger.Text += "<div id=\"Microsoft_Live_Messenger_PresenceButton_" + MessengerID + "\" msgr:width=\"100\" msgr:backColor=\"" + mscBackColor;
                    ltlWindowsMessenger.Text += "\" msgr:altBackColor=\"#FFFFFF\" msgr:foreColor=\"#424542\" msgr:conversationUrl=\"http://settings.messenger.live.com/Conversation/IMMe.aspx?invitee=";
                    ltlWindowsMessenger.Text += MessengerID + "@apps.messenger.live.com&mkt=" + culture + src + "\"></div><script type=\"text/javascript\"";
                    ltlWindowsMessenger.Text += "src=\"http://messenger.services.live.com/users/" + MessengerID + "@apps.messenger.live.com/presence?dt=&amp;mkt=";
                    ltlWindowsMessenger.Text += culture + "&amp;cb=Microsoft_Live_Messenger_PresenceButton_onPresence\"></script>";
                }
                else
                {
                    ltlWindowsMessenger.Text = "<a target=\"_blank\" href=\"http://settings.messenger.live.com/Conversation/IMMe.aspx?invitee=" + MessengerID;
                    ltlWindowsMessenger.Text += "@apps.messenger.live.com&mkt=" + culture + "\"><img style=\"border-style: none;\"";
                    ltlWindowsMessenger.Text += "src=\"http://messenger.services.live.com/users/" + MessengerID + "@apps.messenger.live.com/presenceimage?mkt=" + culture;
                    ltlWindowsMessenger.Text += "\" width=\"16\" height=\"16\" /></a>";
                }
            }
        }
    }

    #endregion
}