<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Membership.ascx.cs" Inherits="CMSModules_Ecommerce_Controls_UI_ProductTypes_Membership" %>
<%@ Register TagPrefix="cms" TagName="SelectValidity" Src="~/CMSAdminControls/UI/Selectors/SelectValidity.ascx" %>
<%@ Register TagPrefix="cms" TagName="SelectMembership" Src="~/CMSModules/Membership/FormControls/Membership/SelectMembership.ascx" %>
<asp:Panel ID="pnlMembership" runat="server">
    <table>
        <%-- Membership group --%>
        <tr>
            <td class="FieldLabel" style="width: 150px;">
                <cms:LocalizedLabel runat="server" ResourceString="com.membership.membershipgroup"
                    DisplayColon="true" />
            </td>
            <td>
                <cms:SelectMembership ID="selectMembershipElem" runat="server" AddNoneRecord="true"
                    UseGUIDForSelection="true" />
            </td>
        </tr>
        <%-- Membership validity --%>
        <tr valign="top">
            <td class="FieldLabel" style="padding-top: 4px;">
                <cms:LocalizedLabel runat="server" ResourceString="com.membership.membershipvalidity"
                    DisplayColon="true" />
            </td>
            <td>
                <cms:SelectValidity ID="selectValidityElem" runat="server" AutoPostBack="true" OnOnValidityChanged="selectValidityElem_OnValidityChanged" />
            </td>
        </tr>
    </table>
</asp:Panel>
