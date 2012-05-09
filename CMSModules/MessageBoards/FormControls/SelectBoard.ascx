<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_MessageBoards_FormControls_SelectBoard" CodeFile="SelectBoard.ascx.cs" %>
<%@ Register src="~/CMSAdminControls/UI/UniSelector/UniSelector.ascx" tagname="UniSelector" tagprefix="cms" %>

<cms:CMSUpdatePanel ID="pnlUpdate" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <cms:UniSelector ID="uniSelector" runat="server" ReturnColumnName="BoardID" DisplayNameFormat="{%BoardDisplayName%}"
            ObjectType="board.board" ResourcePrefix="boardselector" />
    </ContentTemplate>
</cms:CMSUpdatePanel>
