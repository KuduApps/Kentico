<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_Newsletters_Tools_Newsletters_Newsletter_List" Theme="Default"
    MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Tools - Newsletters" CodeFile="Newsletter_List.aspx.cs" %>
    
<%@ Register src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" tagname="UniGrid" tagprefix="cms" %>
<%@ Register Namespace="CMS.UIControls.UniGridConfig" TagPrefix="ug" Assembly="CMS.UIControls" %>

<asp:Content ContentPlaceHolderID="plcContent" ID="content" runat="server">
    <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" Visible="false" EnableViewState="false" />
    <cms:UniGrid runat="server" ID="UniGrid" ShortID="g" OrderBy="NewsletterDisplayName" IsLiveSite="false"
        ObjectType="newsletter.newsletter" 
        Columns="NewsletterID, NewsletterDisplayName, (SELECT COUNT(NewsletterID) FROM Newsletter_SubscriberNewsletter WHERE NewsletterID = Newsletter_Newsletter.NewsletterID AND (SubscriptionApproved = 1 OR SubscriptionApproved IS NULL)) AS Subscribers, (SELECT MAX(IssueMailoutTime) FROM Newsletter_NewsletterIssue WHERE IssueNewsletterID = Newsletter_Newsletter.NewsletterID ) AS LastIssue">
        <GridActions>
            <ug:Action Name="edit" Caption="$General.Edit$" Icon="Edit.png" />
            <ug:Action Name="delete" Caption="$General.Delete$" Icon="Delete.png" Confirmation="$General.ConfirmDelete$" />
            <ug:Action Name="clone" Caption="$Unigrid.Newsletter.Actions.Clone$" Icon="Clone.png" />
        </GridActions>
        <GridColumns>
            <ug:Column Source="NewsletterDisplayName" Caption="$Unigrid.Newsletter.Columns.NewsletterDisplayName$" Wrap="false" Localize="true">
                <Filter Type="text" />
            </ug:Column>
            <ug:Column Source="Subscribers" Caption="$Unigrid.Newsletter.Columns.Subscribers$" Wrap="false" AllowSorting="false" CssClass="TableCell" />
            <ug:Column Source="LastIssue" Caption="$Unigrid.Newsletter.Columns.LastIssue$" Wrap="false" AllowSorting="false" />
            <ug:Column Width="100%" />            
        </GridColumns>   
        <GridOptions DisplayFilter="true" />
    </cms:UniGrid>                             
</asp:Content>