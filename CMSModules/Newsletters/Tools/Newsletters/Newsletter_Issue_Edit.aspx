<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Newsletters_Tools_Newsletters_Newsletter_Issue_Edit"
    ValidateRequest="false" EnableEventValidation="false" Theme="Default" CodeFile="Newsletter_Issue_Edit.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/PageElements/PageTitle.ascx" TagName="PageTitle"
    TagPrefix="cms" %>
<%@ Register Src="Newsletter_ContentEditorFooter.ascx" TagName="Newsletter_ContentEditorFooter"
    TagPrefix="cms" %>
<%@ Register Src="Newsletter_ContentEditor.ascx" TagName="Newsletter_ContentEditor"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/AdminControls/Controls/MetaFiles/FileList.ascx" TagName="FileList"
    TagPrefix="cms" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>Newsletter - Edit issue</title>
    <base target="_self" />
    <style type="text/css">
        body
        {
            margin: 0px;
            padding: 0px;
            height: 100%;
            width: 100%;
        }
        .EditHeader
        {
            position: absolute;
            z-index: 3;
            width: 90%;
            left: 18px;
            padding: 5px;
            background-color: #ffffff;
            border-bottom: solid 1px #cccccc;
            border-right: solid 1px #cccccc;
        }
        .HeaderHeight
        {
            height: 170px;
        }
    </style>

    <script type="text/javascript">
        //<![CDATA[
        var wopener = parent.wopener;

        function RefreshPage() {
            wopener.RefreshPage();
        }

        function ClearToolbar() {
            if ((window.frames['iframeContent'] != null) && (window.frames['iframeContent'].ClearToolbar != null)) {
                window.frames['iframeContent'].ClearToolbar();
            }
        }

        function RememberFocusedRegion() {
            if ((window.frames['iframeContent'] != null) && (window.frames['iframeContent'].RememberFocusedRegion != null)) {
                window.frames['iframeContent'].RememberFocusedRegion();
            }
        }
        //]]>
    </script>

</head>
<body class="<%=mBodyClass%>">
    <form id="form1" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="manScript" runat="server" />
    <asp:Panel runat="server" ID="pnlMenu" CssClass="ContentEditMenu">
        <table cellpadding="0" cellspacing="0">
            <tr>
                <td style="vertical-align: top">
                    <asp:LinkButton ID="lnkSave" runat="server" CssClass="MenuItemEdit">
                        <asp:Image ID="imgSave" runat="server" />
                        <%=mSave%>
                    </asp:LinkButton>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <div class="PageContent">
        <div>
            <asp:Label ID="lblSent" runat="server" EnableViewState="false" CssClass="InfoLabel" />
            <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
                Visible="false" />
            <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
                Visible="false" />
        </div>
        <div style="width: 99%">
            <div class="FloatLeft">
                <cms:LocalizedLabel ID="lblSubject" runat="server" CssClass="FieldLabel" ResourceString="general.subject"
                    DisplayColon="true" EnableViewState="false" Style="display: inline" AssociatedControlID="txtSubject" />
                <cms:CMSTextBox ID="txtSubject" runat="server" CssClass="TextBoxField" MaxLength="450"
                    Width="535" />
            </div>
            <div class="FloatRight">
                <asp:CheckBox runat="server" ID="chkShowInArchive" CssClass="ContentCheckBox" TextAlign="Left" />
            </div>
        </div>
        <br class="ClearBoth" />
        <br />
        <cms:Newsletter_ContentEditor ID="contentBody" runat="server" />
        <cms:Newsletter_ContentEditorFooter ID="contentFooter" runat="server" />
        <br />
        <cms:PageTitle ID="AttachmentTitle" runat="server" TitleCssClass="SubTitleHeader" />
        <br />
        <div class="ClearBorder">
            <cms:FileList ID="AttachmentList" runat="server" />
        </div>
    </div>

    <script type="text/javascript">
        //<![CDATA[
        function PasteImage(imageurl) {
            imageHtml = '<img src="' + imageurl + '" alt="" />';
            return window.frames['iframeContent'].InsertHTML(imageHtml);
        }
        //]]>
    </script>

    </form>
</body>
</html>
