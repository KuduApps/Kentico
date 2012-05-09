<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Newsletters_Tools_ImportExportSubscribers_Subscriber_Import"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Tools - Newsletter subscribers"
    CodeFile="Subscriber_Import.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/UniSelector/UniSelector.ascx" TagName="UniSelector"
    TagPrefix="cms" %>
<asp:Content ContentPlaceHolderID="plcContent" ID="content" runat="server">
    <cms:CMSUpdatePanel ID="pnlUpdate" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Label ID="lblInfo" runat="server" CssClass="InfoLabel" EnableViewState="false"
                Visible="false" />
            <asp:Label ID="lblError" runat="server" CssClass="ErrorLabel" EnableViewState="false"
                Visible="false" />
            <strong>
                <cms:LocalizedLabel ID="lblActions" runat="server" CssClass="InfoLabel" EnableViewState="false"
                    ResourceString="Subscriber_Import.lblActions" />
            </strong>
            <cms:LocalizedRadioButton ID="radSubscribe" runat="server" GroupName="ImportSubscribers"
                ResourceString="Subscriber_Import.SubscribeImported" CssClass="RadioImport" Checked="true" />
            <cms:LocalizedCheckBox ID="chkDoNotSubscribe" runat="server" ResourceString="Subscriber_Import.DoNotSubscribe"
                CssClass="UnderRadioContent" />
            <br />
            <cms:LocalizedRadioButton ID="radUnsubscribe" runat="server" GroupName="ImportSubscribers"
                ResourceString="Subscriber_Import.UnsubscribeImported" CssClass="RadioImport" />
            <cms:LocalizedRadioButton ID="radDelete" runat="server" GroupName="ImportSubscribers"
                ResourceString="Subscriber_Import.DeleteImported" CssClass="RadioImport" />
            <br />
            <strong>
                <cms:LocalizedLabel ID="lblImportedSub" runat="server" CssClass="InfoLabel" EnableViewState="false"
                    ResourceString="Subscriber_Import.lblImportedSub" />
            </strong>
            <cms:CMSTextBox ID="txtImportSub" runat="server" TextMode="MultiLine" CssClass="TextAreaLarge"
                Height="170px" />
            <br />
            <br />
            <cms:LocalizedLabel ID="lblNote" runat="server" CssClass="ContentLabel" EnableViewState="false"
                ResourceString="Subscriber_Import.lblNote" />
            <br />
            <br />
            <strong>
                <cms:LocalizedLabel ID="lblSelectSub" runat="server" CssClass="InfoLabel" EnableViewState="false"
                    ResourceString="Subscriber_Import.lblSelectSub" />
            </strong>
            <cms:UniSelector ID="usNewsletters" runat="server" IsLiveSite="false" ObjectType="Newsletter.Newsletter"
                SelectionMode="Multiple" ResourcePrefix="newsletterselect" />
            <br />
            <div>
                <cms:LocalizedCheckBox ID="chkSendConfirmation" runat="server" ResourceString="Subscriber_Edit.SendConfirmation" /><br />
                <cms:LocalizedCheckBox ID="chkRequireOptIn" runat="server" ResourceString="newsletter.requireoptin" />
            </div>
            <br />
            <cms:LocalizedButton ID="btnImport" runat="server" CssClass="LongSubmitButton" EnableViewState="false"
                ResourceString="Subscriber_Import.btnImport" OnClick="btnImport_Click" />
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnImport" EventName="Click" />
        </Triggers>
    </cms:CMSUpdatePanel>
</asp:Content>
