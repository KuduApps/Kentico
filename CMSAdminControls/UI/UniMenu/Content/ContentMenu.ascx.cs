using System;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.ExtendedControls;
using CMS.CMSHelper;

public partial class CMSAdminControls_UI_UniMenu_Content_ContentMenu : CMSUserControl, ICallbackEventHandler
{
    #region "Variables"

    private DialogConfiguration mConfig = null;
    private const string separator = "##SEP##";

    #endregion


    #region "Enums"

    protected enum Action
    {
        Move = 0,
        Copy = 1,
        Link = 2
    }

    #endregion


    #region "Private properties"

    /// <summary>
    /// Gets the configuration for Copy and Move dialog.
    /// </summary>
    private DialogConfiguration Config
    {
        get
        {
            if (mConfig == null)
            {
                mConfig = new DialogConfiguration();
                mConfig.ContentSelectedSite = CMSContext.CurrentSiteName;
                mConfig.OutputFormat = OutputFormatEnum.Custom;
                mConfig.SelectableContent = SelectableContentEnum.AllContent;
                mConfig.HideAttachments = false;
            }
            return mConfig;
        }
    }

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Prepare scripts for Copy / Move
        string copyRef = Page.ClientScript.GetCallbackEventReference(this, "GetSelectedNodeId()", "CopyMoveItem", "'copy'");
        string moveRef = Page.ClientScript.GetCallbackEventReference(this, "GetSelectedNodeId()", "CopyMoveItem", "'move'");
        const string script = "function CopyMoveItem(content, context) { \n" +
                              "    var arr = content.split('" + separator + "'); \n" +
                              "    if (context == 'copy') { \n" +
                              "        modalDialog(arr[0], 'contentselectnode', '90%', '85%'); \n" +
                              "    } else { \n" +
                              "        modalDialog(arr[1], 'contentselectnode', '90%', '85%'); \n" +
                              "    } \n" +
                              "}";
        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "SetCopyMoveUrl", ScriptHelper.GetScript(script));

        const int bigButtonMinimalWidth = 40;
        const int smallButtonMinimalWidth = 66;

        string[,] bigButtons = new string[2, 9];
        bigButtons[0, 0] = GetString("general.new");
        bigButtons[0, 1] = GetString("documents.newtooltip");
        bigButtons[0, 2] = "BigButton";
        bigButtons[0, 3] = "if (!NewItem()) return;";
        bigButtons[0, 4] = null;
        bigButtons[0, 5] = GetImageUrl("CMSModules/CMS_Content/Menu/New.png");
        bigButtons[0, 6] = GetString("documents.newtooltip");
        bigButtons[0, 7] = ImageAlign.Top.ToString();
        bigButtons[0, 8] = bigButtonMinimalWidth.ToString();

        bigButtons[1, 0] = GetString("general.delete");
        bigButtons[1, 1] = GetString("documents.deletetooltip");
        bigButtons[1, 2] = "BigButton";
        bigButtons[1, 3] = "if(!DeleteItem()) return;";
        bigButtons[1, 4] = null;
        bigButtons[1, 5] = GetImageUrl("CMSModules/CMS_Content/Menu/Delete.png");
        bigButtons[1, 6] = GetString("documents.deletetooltip");
        bigButtons[1, 7] = ImageAlign.Top.ToString();
        bigButtons[0, 8] = bigButtonMinimalWidth.ToString();

        buttonsBig.Buttons = bigButtons;

        string[,] smallButtons = new string[4, 9];

        int buttonIndex = 0;

        // Get real index of button
        smallButtons[buttonIndex, 0] = GetString("general.copy");
        smallButtons[buttonIndex, 1] = GetString("documents.copytooltip");
        smallButtons[buttonIndex, 2] = "SmallButton";
        smallButtons[buttonIndex, 3] = "if(CheckChanges()) {" + copyRef + "};";
        smallButtons[buttonIndex, 4] = null;
        smallButtons[buttonIndex, 5] = GetImageUrl("CMSModules/CMS_Content/Menu/Copy.png");
        smallButtons[buttonIndex, 6] = GetString("documents.copytooltip");
        smallButtons[buttonIndex, 7] = ImageAlign.AbsMiddle.ToString();
        smallButtons[buttonIndex, 8] = smallButtonMinimalWidth.ToString();

        buttonIndex++;
        smallButtons[buttonIndex, 0] = GetString("general.move");
        smallButtons[buttonIndex, 1] = GetString("documents.movetooltip");
        smallButtons[buttonIndex, 2] = "SmallButton";
        smallButtons[buttonIndex, 3] = "if(CheckChanges()) {" + moveRef + "};";
        smallButtons[buttonIndex, 4] = null;
        smallButtons[buttonIndex, 5] = GetImageUrl("CMSModules/CMS_Content/Menu/Move.png");
        smallButtons[buttonIndex, 6] = GetString("documents.movetooltip");
        smallButtons[buttonIndex, 7] = ImageAlign.AbsMiddle.ToString();
        smallButtons[buttonIndex, 8] = smallButtonMinimalWidth.ToString();

        buttonIndex++;
        smallButtons[buttonIndex, 0] = GetString("general.up");
        smallButtons[buttonIndex, 1] = GetString("documents.uptooltip");
        smallButtons[buttonIndex, 2] = "SmallButton";
        smallButtons[buttonIndex, 3] = "if(!MoveUp()) return;";
        smallButtons[buttonIndex, 4] = null;
        smallButtons[buttonIndex, 5] = GetImageUrl("CMSModules/CMS_Content/Menu/Up.png");
        smallButtons[buttonIndex, 6] = GetString("documents.uptooltip");
        smallButtons[buttonIndex, 7] = ImageAlign.AbsMiddle.ToString();
        smallButtons[buttonIndex, 8] = smallButtonMinimalWidth.ToString();

        buttonIndex++;
        smallButtons[buttonIndex, 0] = GetString("general.down");
        smallButtons[buttonIndex, 1] = GetString("documents.downtooltip");
        smallButtons[buttonIndex, 2] = "SmallButton";
        smallButtons[buttonIndex, 3] = "if(!MoveDown()) return;";
        smallButtons[buttonIndex, 4] = null;
        smallButtons[buttonIndex, 5] = GetImageUrl("CMSModules/CMS_Content/Menu/Down.png");
        smallButtons[buttonIndex, 6] = GetString("documents.downtooltip");
        smallButtons[buttonIndex, 7] = ImageAlign.AbsMiddle.ToString();
        smallButtons[buttonIndex, 8] = smallButtonMinimalWidth.ToString();

        buttonsSmall.Buttons = smallButtons;
    }

    #endregion


    #region "Dialog handling"

    /// <summary>
    /// Returns Correct URL of the copy or move dialog.
    /// </summary>
    /// <param name="nodeId">ID Of the node to be copied or moved</param>
    /// <param name="CurrentAction">Action which should be performed</param>
    private string GetDialogUrl(int nodeId, Action CurrentAction)
    {
        Config.CustomFormatCode = CurrentAction.ToString().ToLower();

        string url = CMSDialogHelper.GetDialogUrl(Config, false, false, null, false);

        url = URLHelper.RemoveParameterFromUrl(url, "hash");
        url = URLHelper.AddParameterToUrl(url, "sourcenodeids", nodeId.ToString());
        url = URLHelper.AddParameterToUrl(url, "hash", QueryHelper.GetHash(url));
        url = URLHelper.EncodeQueryString(url);

        return url;
    }

    #endregion


    #region "Callback handling"

    string mCallbackResult = string.Empty;

    /// <summary>
    /// Raises the callback event.
    /// </summary>
    /// <param name="eventArgument">Event argument</param>
    public void RaiseCallbackEvent(string eventArgument)
    {
        int nodeId = ValidationHelper.GetInteger(eventArgument, 0);

        string copyUrl = GetDialogUrl(nodeId, Action.Copy);
        string moveUrl = GetDialogUrl(nodeId, Action.Move);

        mCallbackResult = copyUrl + separator + moveUrl;
    }


    /// <summary>
    /// Returns the result of a callback.
    /// </summary>
    public string GetCallbackResult()
    {
        return mCallbackResult;
    }

    #endregion
}
