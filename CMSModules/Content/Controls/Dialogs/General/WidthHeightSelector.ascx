<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_Content_Controls_Dialogs_General_WidthHeightSelector" CodeFile="WidthHeightSelector.ascx.cs" %>

<table border="0" cellpadding="0" cellspacing="0">
    <tr>
        <td style="white-space: nowrap;">
            <cms:LocalizedLabel ID="lblWidth" runat="server" DisplayColon="true" ResourceString="general.width" />
            <cms:CMSTextBox ID="txtWidth" runat="server" />
            <asp:Literal ID="ltlBreak" runat="server" />
            <cms:LocalizedLabel ID="lblHeight" runat="server" DisplayColon="true" ResourceString="general.height" />
            <cms:CMSTextBox ID="txtHeight" runat="server" />
        </td>
        <td>
            <div style="width: 45px; height: 20px;">
                &nbsp;
                <asp:ImageButton ID="imgLock" runat="server" EnableViewState="false" />
                <asp:ImageButton ID="imgRefresh" runat="server" EnableViewState="false" />
            </div>
            <asp:HiddenField ID="hdnWidth" runat="server" />
            <asp:HiddenField ID="hdnHeight" runat="server" />
            <asp:HiddenField ID="hdnLocked" runat="server" />
        </td>
    </tr>
</table>
