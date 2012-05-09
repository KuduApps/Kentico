<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_Content_FormControls_Relationships_RelationshipConfiguration" CodeFile="RelationshipConfiguration.ascx.cs" %>
<table>
    <tr>
        <td style="width: 30px">
            <asp:RadioButton runat="server" ID="radNoRel" GroupName="Relationship" />
        </td>
        <td>
            <cms:LocalizedLabel ID="lblNoRel" runat="server" ResourceString="RelationshipConfiguration.NoRelationship" />
        </td>
    </tr>
    <tr>
        <td>
            <asp:RadioButton runat="server" ID="radCurrentDoc" GroupName="Relationship" />
        </td>
        <td>
            <cms:LocalizedLabel ID="lblCurrentDoc" runat="server" ResourceString="RelationshipConfiguration.CurrentDocument" />
        </td>
    </tr>
    <tr>
        <td>
            <asp:RadioButton runat="server" ID="radDocWithNodeID" GroupName="Relationship" />
        </td>
        <td>
            <cms:LocalizedLabel ID="lblDocWithNodeID" runat="server" ResourceString="RelationshipConfiguration.DocumentWithNodeID" />
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;
        </td>
        <td>
            <cms:CMSTextBox runat="server" ID="txtNodeID" Enabled="false" CssClass="TextBoxField" />
        </td>
    </tr>
</table>
<asp:Literal runat="server" ID="ltlScript" />

<script type="text/javascript">
    //<![CDATA[
    function RadiobuttonChange() {
        if (radValueElem.checked) {
            txtValueElem.disabled = false;
        }
        else {
            txtValueElem.disabled = true;
        }
    }
    //]]>
</script>

