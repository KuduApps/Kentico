<%@ Control Language="C#" AutoEventWireup="true" CodeFile="StateEdit.ascx.cs" Inherits="CMSModules_Countries_Controls_State_StateEdit" %>
<cms:UIForm runat="server" ID="EditForm" ObjectType="cms.state" RedirectUrlAfterCreate="Edit.aspx?stateid={%EditedObject.ID%}&countryid={%EditedObject.ParentID%}&saved=1" />
