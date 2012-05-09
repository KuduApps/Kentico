<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_SMTPServers_Pages_Administration_List"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="SMTP Servers"
    CodeFile="List.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<%@ Register Namespace="CMS.UIControls.UniGridConfig" TagPrefix="ug" Assembly="CMS.UIControls" %>

<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <cms:CMSUpdatePanel runat="server" ID="pnlUpdate">
        <ContentTemplate>
            <cms:UniGrid runat="server" ID="UniGrid" ShortID="g" ObjectType="cms.smtpserver"
                Columns="ServerID, ServerName, ServerEnabled, ServerIsGlobal" OrderBy="ServerName"
                IsLiveSite="false" EditActionUrl="Frameset.aspx?smtpserverid={0}">
                <GridActions Parameters="ServerID">
                    <ug:Action Name="edit" Caption="$General.Edit$" Icon="Edit.png" />
                    <ug:Action Name="delete" Caption="$General.Delete$" Icon="Delete.png" 
                        Confirmation="$smtpserver.ConfirmDelete$" />
                    <ug:Action Name="enable" ExternalSourceName="enable" Caption="$General.Enable$" 
                        Icon="Enable.png" Confirmation="$smtpserver.ConfirmEnable$" />
                    <ug:Action Name="disable" ExternalSourceName="disable" Caption="$General.Disable$" 
                        Icon="Disable.png" Confirmation="$smtpserver.ConfirmDisable$" />
                </GridActions>
                <GridColumns>
                    <ug:Column Source="ServerName" Caption="$Unigrid.SMTPServer.Columns.ServerName$" 
                        Wrap="false" Localize="true">
                        <Filter Type="text" />
                    </ug:Column>
                    <ug:Column Source="ServerEnabled" ExternalSourceName="#yesno" 
                        Caption="$Unigrid.SMTPServer.Columns.ServerEnabled$" Wrap="false">
                        <Filter Type="bool" />
                    </ug:Column>
                    <ug:Column Source="ServerIsGlobal" ExternalSourceName="#yesno" 
                        Caption="$Unigrid.SMTPServer.Columns.ServerIsGlobal$" Wrap="false">
                        <Filter Type="bool" />
                    </ug:Column>
                    <ug:Column Width="100%" />
                </GridColumns>
                <GridOptions DisplayFilter="true" />
            </cms:UniGrid>
        </ContentTemplate>
    </cms:CMSUpdatePanel>
</asp:Content>