<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Purchase.ascx.cs" Inherits="CMSModules_ContactManagement_Controls_UI_ActivityDetails_Purchase" %>
<table>
    <tr>
        <td class="ActivityDetailsLabel">
            <cms:LocalizedLabel runat="server" ID="lblRecord" ResourceString="om.activitydetails.invoice"
                EnableViewState="false" DisplayColon="true" />
        </td>
        <td>
            <cms:LocalizedLinkButton runat="server" ID="btnView" ResourceString="om.activitydetails.viewinvoice" />
        </td>
    </tr>
</table>
