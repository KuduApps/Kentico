<%@ Page Language="C#" AutoEventWireup="true" Theme="Default" CodeFile="InsertRating.aspx.cs"
    Inherits="CMSModules_Content_Controls_CKEditor_InsertRating" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server" enableviewstate="false">
    <title>Insert Rating</title>

    <script type="text/javascript">
        //<![CDATA[
        function insertRating(charValue) {
            var oDialog = window.parent.CMSPlugin.currentDialog;
            if (oDialog) {
                oDialog._.editor.insertHtml(charValue || "");
            }
            oDialog.hide();
        }
        //]]>
    </script>

</head>
<body style="background-color: White; height: 94%;" runat="server" id="bodyElem">
    <form id="form1" runat="server">
    <asp:Panel ID="pnlGrid" runat="server" CssClass="UniGridBody">
        <asp:Panel ID="pnlContent" runat="server" CssClass="UniGridContent">
            <cms:basicdatagrid id="gridForms" runat="server" cssclass="UniGridGrid" cellspacing="0"
                cellpadding="3" autogeneratecolumns="false" gridlines="Horizontal" width="100%">
                <HeaderStyle CssClass="UniGridHead" />
                <AlternatingItemStyle CssClass="OddRow" />
                <ItemStyle CssClass="EvenRow" />
                <Columns>
                    <asp:TemplateColumn>
                        <ItemTemplate>
                            <a href="#" onclick='javascript:insertRating("%%control:RatingControl?<%# DataBinder.Eval(Container.DataItem, "UserControlCodeName") %>%%");'>
                                <%# DataBinder.Eval(Container.DataItem, "UserControlDisplayName") %>
                            </a>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
            </cms:basicdatagrid>
        </asp:Panel>
    </asp:Panel>
    </form>
</body>
</html>
