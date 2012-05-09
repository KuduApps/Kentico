using System;

using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.UIControls;
using CMS.SettingsProvider;
using CMS.LicenseProvider;
using CMS.Messaging;

public partial class CMSModules_Messaging_CMSPages_SendMessage : CMSLiveModalPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Check license
        LicenseHelper.CheckFeatureAndRedirect(URLHelper.GetCurrentDomain(), FeatureEnum.Messaging);


        // Initializes page title control
        CurrentMaster.Title.TitleText = GetString("messaging.sendmessage");
        CurrentMaster.Title.TitleImage = GetImageUrl("CMSModules/CMS_Messaging/sendmessage.png");
        Title = GetString("messaging.sendmessage");

        int requestedUserId = QueryHelper.GetInteger("requestid", 0);
        if (requestedUserId != 0)
        {
            UserInfo requestedUser = UserInfoProvider.GetFullUserInfo(requestedUserId);
            string fullUserName = HTMLHelper.HTMLEncode(Functions.GetFormattedUserName(requestedUser.UserName, requestedUser.FullName, requestedUser.UserNickName, true));
            Page.Title = GetString("messaging.sendmessageto") + " " + fullUserName;
            CurrentMaster.Title.TitleText = Page.Title;
        }

        // Initilaize new message
        ucSendMessage.DefaultRecipient = QueryHelper.GetString("requestid", string.Empty);
        ucSendMessage.SendButtonClick += SendButon;
        ucSendMessage.CloseButtonClick += CloseButon;
        ucSendMessage.SendMessageMode = MessageActionEnum.New;
        ucSendMessage.DisplayCloseButton = true;
        ucSendMessage.UsePromptDialog = false;
    }


    private void SendButon(object sender, EventArgs e)
    {
        if (ucSendMessage.ErrorMessage == string.Empty)
        {
            ucSendMessage.SendButton.Enabled = false;
            ucSendMessage.BBEditor.Enabled = false;
            ucSendMessage.SubjectBox.Enabled = false;
            ucSendMessage.FromBox.Enabled = false;
            ucSendMessage.CancelButton.Attributes.Add("onclick", "wopener.location.replace(wopener.location);");
            ucSendMessage.CancelButton.ResourceString = "general.Close";
        }
    }


    private void CloseButon(object sender, EventArgs e)
    {
        // Close
        ScriptHelper.RegisterStartupScript(this, GetType(), "closeSendDialog", ScriptHelper.GetScript("window.close();"));
    }
}
