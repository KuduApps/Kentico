<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_Content_Controls_Dialogs_Web_WebLinkSelector" CodeFile="WebLinkSelector.ascx.cs" %>
<%@ Register src="~/CMSAdminControls/UI/PageElements/Help.ascx" tagname="Help" tagprefix="cms" %>
<%@ Register Src="~/CMSModules/Content/Controls/Dialogs/General/URLSelector.ascx" TagName="URLSelector"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/Content/Controls/Dialogs/Properties/HTMLLinkProperties.ascx"
    TagName="HTMLLinkProperties" TagPrefix="cms" %>

<script type="text/javascript" language="javascript">
    function insertItem() {
        RaiseHiddenPostBack();
    }
</script>

<div class="DialogWebContent">
    <div class="LeftAlign">
        <cms:URLSelector runat="server" ID="urlSelectElem" />
    </div>
    <div class="RightAlign">
        <cms:Help ID="helpElem" runat="server" TopicName="dialogs_link_web" />
    </div>
</div>
<div class="DialogLinkWebProperties">
    <div>
        <asp:Panel ID="pnlProperties" runat="server">
            <cms:HTMLLinkProperties runat="server" ID="propLinkProperties" ShowGeneralTab="false"
                IsWeb="true" />
        </asp:Panel>
        <cms:CMSButton ID="hdnButton" runat="server" OnClick="hdnButton_Click" CssClass="HiddenButton" />
        <asp:Literal ID="ltlScript" runat="server" EnableViewState="false" />
    </div>
</div>
