<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_EmailQueue_SentEmails"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" CodeFile="SentEmails.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<%@ Register Namespace="CMS.UIControls.UniGridConfig" TagPrefix="ug" Assembly="CMS.UIControls" %>
<%@ Register Src="~/CMSAdminControls/UI/UniGrid/Filters/TextSimpleFilter.ascx" TagName="TextSimpleFilter" TagPrefix="cms" %>
    
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <span class="InfoLabel">
        <cms:LocalizedLabel ID="lblText" runat="server" EnableViewState="false" ResourceString="emailqueue.archive.text" />
    </span>
    <span class="InfoLabel">
        <asp:Image runat="server" ID="imgShowFilter" CssClass="NewItemImage" />
        <cms:LocalizedLinkButton ID="btnShowFilter" runat="server" OnClick="btnShowFilter_Click" />
    </span>
    <asp:PlaceHolder ID="plcFilter" runat="server" Visible="false">
        <table>
            <tr runat="server">
                <td class="FieldLabel">
                    <cms:LocalizedLabel ID="lblFrom" runat="server" EnableViewState="false" DisplayColon="true"
                        ResourceString="general.from" />
                </td>
                <td>
                    <cms:TextSimpleFilter ID="fltFrom" runat="server" Column="EmailFrom" />
                </td>
            </tr>
            <tr runat="server">
                <td class="FieldLabel">
                    <cms:LocalizedLabel ID="lblTo" runat="server" EnableViewState="false" DisplayColon="true"
                        ResourceString="general.toemail" />
                </td>
                <td>
                    <cms:TextSimpleFilter ID="fltTo" runat="server" Column="EmailTo" />
                </td>
            </tr>
            <tr runat="server">
                <td class="FieldLabel">
                    <cms:LocalizedLabel ID="lblSubject" runat="server" EnableViewState="false" DisplayColon="true"
                        ResourceString="general.subject" />
                </td>
                <td>
                    <cms:TextSimpleFilter ID="fltSubject" runat="server" Column="EmailSubject" />
                </td>
            </tr>
            <tr runat="server">
                <td class="FieldLabel">
                    <cms:LocalizedLabel ID="lblBody" runat="server" EnableViewState="false" DisplayColon="true"
                        ResourceString="general.body" />
                </td>
                <td>
                    <cms:TextSimpleFilter ID="fltBody" runat="server" Column="EmailBody" />
                </td>
            </tr>
            <tr>
                <td class="FieldLabel">
                    <cms:LocalizedLabel ID="lblPriority" runat="server" EnableViewState="false" DisplayColon="true"
                        ResourceString="emailqueue.priority" />
                </td>
                <td>
                    <asp:DropDownList ID="drpPriority" runat="server" CssClass="DropDownFieldFilter" />
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    <cms:LocalizedButton ID="btnFilter" runat="server" OnClick="btnFilter_Clicked" CssClass="ContentButton"
                        EnableViewState="false" ResourceString="General.Show" />
                </td>
            </tr>
        </table>
    </asp:PlaceHolder>
    <br />
    
    <cms:UniGrid ID="gridElem" runat="server" ShortID="g" ObjectType="cms.email" IsLiveSite="false"
        OrderBy="EmailLastSendAttempt DESC"
        Columns="EmailID, EmailSubject, EmailTo, EmailPriority, EmailLastSendAttempt, EmailIsMass" >
        <GridActions Parameters="EmailID">
            <ug:Action Name="resend" Caption="$general.resend$" Icon="ResendEmail.png" />
            <ug:Action Name="delete" Caption="$General.Delete$" Icon="Delete.png" Confirmation="$General.ConfirmDelete$" />
            <ug:Action ExternalSourceName="edit" Name="display" Caption="$general.view$" Icon="View.png" />
        </GridActions>
        <GridColumns>
            <ug:Column Source="EmailSubject" ExternalSourceName="subject" Caption="$general.subject$" Wrap="false" Width="100%" />
            <ug:Column Source="##ALL##" ExternalSourceName="EmailTo" Caption="$EmailQueue.To$" Wrap="false" />
            <ug:Column Source="EmailPriority" ExternalSourceName="priority" Caption="$EmailQueue.Priority$" Wrap="false" />
            <ug:Column Source="EmailLastSendAttempt" Caption="$EmailQueue.LastSendAttempt$" Wrap="false" />            
        </GridColumns>    
        <GridOptions DisplayFilter="false" ShowSelection="true" />        
    </cms:UniGrid>        
        
    <asp:HiddenField ID="hdnIdentificator" runat="server" EnableViewState="false" />
    
</asp:Content>