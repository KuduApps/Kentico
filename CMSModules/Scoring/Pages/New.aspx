<%@ Page Language="C#" AutoEventWireup="true" CodeFile="New.aspx.cs" Inherits="CMSModules_Scoring_Pages_New"
    MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Scoring - new score" Theme="Default" %>

<%@ Register Src="~/CMSModules/Scoring/Controls/UI/Score/Edit.ascx" TagName="ScoreEdit" TagPrefix="cms" %>
    
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <cms:ScoreEdit ID="editElem" runat="server" IsLiveSite="false" />
</asp:Content>