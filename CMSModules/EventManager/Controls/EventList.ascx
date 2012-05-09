<%@ Control Language="C#" AutoEventWireup="true" CodeFile="~/CMSModules/EventManager/Controls/EventList.ascx.cs"
    Inherits="CMSModules_EventManager_Controls_EventList" %>
<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
    Visible="false" />
<table>
    <tr>
        <td class="FieldLabel">
            <cms:LocalizedLabel ID="lblEventScopt" AssociatedControlID="drpEventScope" runat="server"
                ResourceString="eventmanager.eventscope" EnableViewState="false" DisplayColon="true" />
        </td>
        <td>
            <asp:DropDownList ID="drpEventScope" runat="server" CssClass="DropDownFieldSmall" />
        </td>
    </tr>
    <tr>
        <td>
        </td>
        <td>
            <cms:CMSButton ID="btnFilter" runat="server" CssClass="ContentButton" EnableViewState="false" />
        </td>
    </tr>
    <tr>
        <td colspan="2">
            &nbsp;
        </td>
    </tr>
</table>
<cms:UniGrid runat="server" ID="gridElem" OrderBy="EventDate DESC" DelayedReload="true"
    IsLiveSite="false" />
