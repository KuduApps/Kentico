<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSAdminControls_UI_Selectors_SelectDocumentGroup" CodeFile="SelectDocumentGroup.ascx.cs" %>
<table>
    <tr>
        <td>
            <cms:LocalizedLabel ID="lblGroups" runat="server" CssClass="ContentLabel" ResourceString="community.group.choosedocumentowner"
                DisplayColon="true" />
        </td>
    </tr>
    <tr>
        <td>
            <asp:PlaceHolder runat="server" ID="plcGroupSelector" />
        </td>
    </tr>
    <tr>
        <td>
            <cms:LocalizedCheckBox ID="chkInherit" runat="server" CssClass="ContentCheckBox"
                Checked="false" ResourceString="community.group.inheritdocumentowner" />
        </td>
    </tr>
    <asp:PlaceHolder ID="plcButtons" runat="server">
        <tr>
            <td>
                <br />
                <cms:LocalizedButton ID="btnOk" runat="server" CssClass="SubmitButton" OnClick="btnOk_Click"
                    ResourceString="general.ok" />&nbsp;<cms:LocalizedButton ID="btnCancel" runat="server"
                        CssClass="ContentButton" OnClientClick="window.close(); return false;" ResourceString="general.cancel" />
            </td>
        </tr>
    </asp:PlaceHolder>
</table>
<asp:Literal runat="server" ID="ltlScript" />
