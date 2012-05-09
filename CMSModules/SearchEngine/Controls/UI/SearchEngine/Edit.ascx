<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_SearchEngine_Controls_UI_SearchEngine_Edit" CodeFile="Edit.ascx.cs" %>
    
<cms:UIForm runat="server" ID="EditForm" ObjectType="cms.SearchEngine" RedirectUrlAfterCreate="Edit.aspx?engineid={%EditedObject.ID%}&saved=1" />
