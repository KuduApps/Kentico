<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_BizForms_Tools_BizForm_Edit_EditRecord" Theme="Default"
    ValidateRequest="false" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master"
    Title="BizForm edit - New record" EnableEventValidation="false" CodeFile="BizForm_Edit_EditRecord.aspx.cs" %>

<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <div class="PageHeaderLine BizFormRecord">
        <table cellspacing="0" cellpadding="0" border="0">
            <tbody>
                <tr>
                    <td>
                        <cms:LocalizedCheckBox ID="chkSendNotification" runat="server" ResourceString="bizform.sendnotification" />
                    </td>
                    <td>
                        <cms:LocalizedCheckBox ID="chkSendAutoresponder" runat="server" ResourceString="bizform.sendautoresponder" />
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <asp:Label ID="lblError" runat="server" EnableViewState="false" />
    <cms:BizForm ID="formElem" runat="server" IsLiveSite="false" />
</asp:Content>
