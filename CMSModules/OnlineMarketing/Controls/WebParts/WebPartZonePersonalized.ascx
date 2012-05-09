<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WebPartZonePersonalized.ascx.cs" Inherits="CMSModules_OnlineMarketing_Controls_WebParts_WebPartZonePersonalized" %>
<%@ Register Src="~/CMSModules/OnlineMarketing/Controls/UI/ContentPersonalizationVariant/Edit.ascx"
    TagName="ContentPersonalizationVariantEdit" TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/OnlineMarketing/Controls/UI/MVTVariant/Edit.ascx"
    TagName="MvtVariantEdit" TagPrefix="cms" %>

<asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false" Visible="false" />
<asp:Label runat="server" ID="lblError" CssClass="EditingFormErrorLabel" EnableViewState="false" Visible="false" />
<cms:ContentPersonalizationVariantEdit ID="cpEditElem" runat="server" IsLiveSite="false"
    Visible="false" />
<cms:MvtVariantEdit ID="mvtEditElem" runat="server" IsLiveSite="false" Visible="false" />
