<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Newsletter_Send.aspx.cs"
    Inherits="CMSModules_Newsletters_Tools_Newsletters_Newsletter_Send" Theme="Default"
    MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Tools - Newsletter send" %>

<asp:Content ContentPlaceHolderID="plcContent" ID="content" runat="server">
    <asp:Label ID="lblInfo" runat="server" EnableViewState="false" Visible="false" CssClass="InfoLabel" />
    <asp:Label ID="lblError" runat="server" EnableViewState="false" Visible="false" CssClass="ErrorLabel" />
    <table>
        <tr>
            <td style="padding-bottom: 20px;">
                <cms:LocalizedRadioButton ID="radSendNow" runat="server" GroupName="Send" 
                    ResourceString="Newsletter_Issue_Send.SendNow" 
                    AutoPostBack="true" OnCheckedChanged="radGroupSend_CheckedChanged"
                    Checked="true" />
            </td>
        </tr>
        <tr>
            <td style="padding-bottom: 20px;">
                <cms:LocalizedRadioButton ID="radSendDraft" runat="server" GroupName="Send"
                    ResourceString="Newsletter_Issue_Send.SendDraft"
                    AutoPostBack="true" OnCheckedChanged="radGroupSend_CheckedChanged" />
                <div class="UnderRadioContent">
                    <cms:LocalizedLabel ID="lblDraftEmail" runat="server" 
                        ResourceString="Newsletter_Issue_Send.Email" DisplayColon="true"
                        AssociatedControlID="txtSendDraft" EnableViewState="false" />
                    <cms:CMSTextBox ID="txtSendDraft" runat="server"
                        MaxLength="450" Enabled="false" CssClass="TextBoxField" />
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div class="UnderRadioContent">
                    <cms:LocalizedButton ID="btnSend" runat="server" CssClass="SubmitButton"
                        ResourceString="Newsletter_Issue_Send.Send" OnClick="btnSend_Click" Enabled="false"
                        EnableViewState="false" />
                </div>
            </td>
        </tr>
    </table>
</asp:Content>       