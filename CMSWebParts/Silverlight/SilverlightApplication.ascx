<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSWebParts_Silverlight_SilverlightApplication" CodeFile="~/CMSWebParts/Silverlight/SilverlightApplication.ascx.cs" %>
<%@ Register Src="~/CMSAdminControls/Silverlight/SilverlightApplication.ascx" TagName="Silverlight"
    TagPrefix="cms" %>
<asp:Literal ID="ltlDesign" runat="server" />
<cms:Silverlight runat="server" ID="silverlightElem" />
