<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MetaDataEditor.ascx.cs"
    Inherits="CMSAdminControls_ImageEditor_MetaDataEditor" %>
<%@ Register Src="~/CMSFormControls/System/LocalizableTextBox.ascx" TagName="LocalizableTextBox"
    TagPrefix="cms" %>
<asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
    Visible="false" />
<asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
    Visible="false" />
<asp:PlaceHolder ID="plcFileName" runat="server">
    <tr>
        <td class="MetaDataEditorTd">
            <cms:LocalizedLabel ID="lblFileName" runat="server" EnableViewState="false" ResourceString="general.filename"
                DisplayColon="true" />
        </td>
        <td>
            <cms:CMSTextBox ID="txtFileName" runat="server" CssClass="TextBoxField" MaxLength="250" />
        </td>
    </tr>
</asp:PlaceHolder>
<tr>
    <td class="MetaDataEditorTd">
        <cms:LocalizedLabel ID="lblTitle" runat="server" EnableViewState="false" ResourceString="general.title"
            DisplayColon="true" />
    </td>
    <td>
        <cms:LocalizableTextBox ID="txtTitle" runat="server" CssClass="TextBoxField"
            MaxLength="250" />
    </td>
</tr>
<tr>
    <td class="MetaDataEditorTd">
        <cms:LocalizedLabel ID="lblDescription" runat="server" EnableViewState="false" DisplayColon="true"
            ResourceString="general.description" AssociatedControlID="txtDescription" />
    </td>
    <td>
        <cms:LocalizableTextBox ID="txtDescription" runat="server" TextMode="MultiLine" CssClass="TextAreaField"
            MaxLength="4000" EnableViewState="false" />
    </td>
</tr>
<asp:PlaceHolder ID="plcExtensionAndSize" runat="server">
    <tr>
        <td class="MetaDataEditorTd">
            <cms:LocalizedLabel ID="lblExtensionLabel" runat="server" EnableViewState="false"
                ResourceString="img.extension" DisplayColon="true" />
        </td>
        <td>
            <asp:Label ID="lblExtension" runat="server" />
        </td>
    </tr>
    <tr>
        <td class="MetaDataEditorTd">
            <cms:LocalizedLabel ID="lblSizeLabel" runat="server" EnableViewState="false" ResourceString="general.size"
                DisplayColon="true" />
        </td>
        <td>
            <asp:Label ID="lblSize" runat="server" />
        </td>
    </tr>
</asp:PlaceHolder>

<script type="text/javascript">
    //<![CDATA[
    function RefreshMetaFile() {
        if (wopener.UpdatePage) {
            wopener.UpdatePage();
        }
        else {
            wopener.location.replace(wopener.location);
        }
    }

    function RefreshMetaData(clientId, fullRefresh, guid, action) {
        setTimeout("if (wopener.InitRefresh_" + clientId + ") { wopener.InitRefresh_" + clientId + "('', " + fullRefresh + ", 'attachmentguid|" + guid + "', '" + action + "'); }", 0);
    }
    //]]>
</script>

<asp:Literal ID="ltlScript" runat="server" />