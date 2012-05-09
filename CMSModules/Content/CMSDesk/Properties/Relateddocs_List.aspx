<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Content_CMSDesk_Properties_Relateddocs_List"
    Theme="Default" CodeFile="Relateddocs_List.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/PageElements/Help.ascx" TagName="Help" TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/Content/FormControls/Relationships/RelatedDocuments.ascx"
    TagName="RelatedDocuments" TagPrefix="cms" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>Relationship - list</title>
    <style type="text/css">
        body
        {
            margin: 0px;
            padding: 0px;
            height: 100%;
        }
    </style>
</head>
<body class="VerticalTabsBody <%=mBodyClass%>">
    <form id="form1" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="scriptMan" runat="server" />
    <asp:Panel ID="pnlBody" runat="server" CssClass="VerticalTabsPageBody">
        <asp:Panel runat="server" ID="pnlTab" CssClass="ContentEditMenu">
            <table width="100%">
                <tr>
                    <td>
                        <div style="height: 24px; padding: 5px;">
                        </div>
                    </td>                
                    <td class="TextRight">
                        <cms:Help ID="helpElem" runat="server" TopicName="related_docs" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="pnlInfo" runat="server" CssClass="UnsortedInfoPanel">
            <asp:Label ID="lblInfo" runat="server" CssClass="InfoLabel" EnableViewState="false" />
        </asp:Panel>
        <div class="Unsorted">
            <div class="RelationshipList">
                <asp:Panel ID="pnlNewItem" runat="server" CssClass="New">
                    <cms:CMSImage ID="imgNewRelationship" runat="server" ImageAlign="AbsMiddle" CssClass="NewItemImage"
                        EnableViewState="false" /><cms:LocalizedHyperlink ID="lnkNewRelationship" runat="server"
                            CssClass="NewItemLink" EnableViewState="false" ResourceString="Relationship.AddRelatedDocument" />
                </asp:Panel>
            </div>
            <cms:RelatedDocuments ID="relatedDocuments" runat="server" ShowAddRelation="false"
                IsLiveSite="false" PageSize="10,25,50,100,##ALL##" DefaultPageSize="25" />
        </div>
    </asp:Panel>
    </form>
</body>
</html>
