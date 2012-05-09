<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_BizForms_Tools_BizForm_Edit_Layout" Theme="Default" EnableEventValidation="false" CodeFile="BizForm_Edit_Layout.aspx.cs" %>
<%@ Register src="~/CMSAdminControls/UI/PageElements/PageTitle.ascx" tagname="PageTitle" tagprefix="cms" %>

<%@ Register Src="~/CMSModules/AdminControls/Controls/Class/Layout.ascx" TagName="Layout" TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/AdminControls/Controls/MetaFiles/FileList.ascx" TagName="FileList" TagPrefix="cms" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>BizForm layout</title>
    <style type="text/css">
        body
        {
            margin: 0px;
            padding: 0px;
            height: 100%;
        }
    </style>
</head>
<body class="TabsBody <%=mBodyClass%>">
    <form id="form1" runat="server">
    <asp:Panel runat="server" ID="pnlTabsBody" CssClass="TabsPageBody">
        <asp:Panel runat="server" ID="pnlTabsScroll" CssClass="TabsPageScrollArea">
            <asp:Panel runat="server" ID="pnlTab" CssClass="TabsPageContent">
                <cms:Layout ID="layoutElem" runat="server" />
                <asp:Panel ID="pnlAttachments" runat="server" CssClass="PageContent">
                    <cms:PageTitle ID="AttachmentTitle" runat="server" TitleCssClass="SubTitleHeader" />
                    <br />
                    <cms:FileList ID="AttachmentList" runat="server" />
                </asp:Panel>

                <script type="text/javascript">
                    <!--
                        // Pasting image URL to CKEditor - requires other function 'InsertHTML' -  see Layout control
                        function PasteImage(imageurl)
                        {
                            imageurl = '<img src="'+ imageurl +'" />';
                            return InsertHTML(imageurl);
                        }     
                    -->
                </script>

            </asp:Panel>
        </asp:Panel>
    </asp:Panel>
    </form>
</body>
</html>
