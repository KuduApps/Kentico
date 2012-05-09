<%@ Control Language="C#" AutoEventWireup="true" CodeFile="~/CMSModules/MessageBoards/FormControls/SelectBoardWithAll.ascx.cs"
    Inherits="CMSModules_MessageBoards_FormControls_SelectBoardWithAll" %>
<%@ Register Src="~/CMSModules/MessageBoards/FormControls/SelectBoard.ascx" TagName="SelectBoard"
    TagPrefix="cms" %>
    
<cms:CMSUpdatePanel ID="pnlUpdate" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <cms:SelectBoard runat="server" ID="selectBoard" />
    </ContentTemplate>
</cms:CMSUpdatePanel>
