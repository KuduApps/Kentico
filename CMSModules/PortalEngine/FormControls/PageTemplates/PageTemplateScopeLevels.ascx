<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_PortalEngine_FormControls_PageTemplates_PageTemplateScopeLevels" CodeFile="PageTemplateScopeLevels.ascx.cs" %>
<%@ Register Src="~/CMSModules/PortalEngine/FormControls/PageTemplates/LevelTree.ascx" TagPrefix="cms"
    TagName="LevelTree" %>

<script type="text/javascript">
    //<![CDATA[
    function ShowOrHideTree(show) {
        if (show) {
            document.getElementById('treeSpan').style.display = "inline";
        }
        else {
            document.getElementById('treeSpan').style.display = "none";
        }
    }    
    //]]>
</script>

<table style="width: 100%">
    <tr>
        <td colspan="2">
            <asp:RadioButton runat="server" ID="radAllowAll" GroupName="Allow" />
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:RadioButton runat="server" ID="radSelect" GroupName="Allow" />
        </td>
    </tr>
    <tr id="treeSpan" style="display: none;">
        <td>
            &nbsp;
        </td>
        <td>
            <div>
                <cms:LevelTree runat="server" ID="treeElem" />
            </div>
        </td>
    </tr>
</table>
<asp:Literal ID="ltlScript" runat="server" EnableViewState="false" />