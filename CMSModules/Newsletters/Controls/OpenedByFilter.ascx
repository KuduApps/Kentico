<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_Newsletters_Controls_OpenedByFilter"
    CodeFile="OpenedByFilter.ascx.cs" %>    
    
<%@ Register Src="~/CMSAdminControls/UI/UniGrid/Filters/TextSimpleFilter.ascx" TagName="TextSimpleFilter"
    TagPrefix="cms" %>
    
<%@ Register Src="~/CMSAdminControls/UI/UniGrid/Filters/TimeSimpleFilter.ascx" TagName="TimeSimpleFilter"
    TagPrefix="cms" %>
    
<asp:Panel ID="pnl" runat="server" DefaultButton="btnShow">
    <table cellpadding="0" cellspacing="2">
        <tr>
            <td>
                <cms:LocalizedLabel ID="lblSubscriberName" runat="server" ResourceString="unigrid.subscribers.columns.subscribername"
                    DisplayColon="true" />
            </td>
            <td>
                <cms:TextSimpleFilter ID="fltSubscriberName" runat="server" Column="SubscriberFullName" />
            </td>
        </tr>
        <tr>
            <td>
                <cms:LocalizedLabel ID="lblEmail" runat="server" ResourceString="general.email"
                    DisplayColon="true" />
            </td>
            <td>
                <cms:TextSimpleFilter ID="fltEmail" runat="server" Column="SubscriberEmail" IncludeNULLCondition="false" />
            </td>
        </tr>       
        <tr>
            <td>
                <cms:LocalizedLabel ID="lblOpenedBetween" runat="server" ResourceString="newsletter_issue.openedbetween"
                    DisplayColon="true" />
            </td>
            <td>
                <cms:TimeSimpleFilter ID="fltOpenedBetween" runat="server" Column="OpenedWhen" />
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <cms:LocalizedButton ID="btnShow" runat="server" CssClass="ContentButton" ResourceString="general.show" />
            </td>
        </tr>
    </table>
    <br />
</asp:Panel>