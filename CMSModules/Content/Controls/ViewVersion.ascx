<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_Content_Controls_ViewVersion"
    CodeFile="ViewVersion.ascx.cs" %>

<script type="text/javascript">
    //<![CDATA[
    function TipImage(imageWidth, url, attachmentName) {
        Tip('<div style=\'width:' + imageWidth + 'px;text-align:center;\'><img src=\'' + url + '\' alt=\'' + attachmentName + '\' /></div>');
    }
    //]]>
</script>

<asp:Panel ID="pnlControl" runat="server">
    <style type="text/css">
        .UnsortedLeft
        {
            width: 50%;
            float: left;
        }
        .UnsortedRight
        {
            width: 50%;
            float: right;
        }
        .PageContent .NotIncluded, .PageContent .HTMLNotIncluded
        {
            display: inline;
            visibility: hidden;
        }
        td
        {
            vertical-align: top;
        }
    </style>
    <ajaxToolkit:ToolkitScriptManager ID="ucScriptManager" runat="server" />
    <asp:Panel ID="pnlAdditionalControls" runat="server" CssClass="PageHeaderLine SiteHeaderLine">
        <cms:LocalizedLabel ID="lblCompareTo" runat="server" ResourceString="content.compareto"
            DisplayColon="true" EnableViewState="false" />
        <cms:LocalizedDropDownList ID="drpCompareTo" runat="server" CssClass="DropDownField"
            AutoPostBack="true" />
    </asp:Panel>
</asp:Panel>
<asp:Panel ID="pnlBody" runat="server">
    <div class="PageContent">
        <asp:PlaceHolder ID="plcLabels" runat="server">
            <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false" />
            <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false" />
        </asp:PlaceHolder>
        <asp:Table ID="tblDocument" runat="server" GridLines="Horizontal" CellPadding="3"
            CellSpacing="0" CssClass="UniGridGrid" />
        <br />
    </div>
</asp:Panel>
