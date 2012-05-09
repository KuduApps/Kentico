<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Ecommerce_Pages_Tools_ProductOptions_OptionCategory_Edit"
    CodeFile="OptionCategory_Edit.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>Option Category - Edit</title>
</head>
<frameset border="0" rows="<%= TabsBreadHeadFrameHeight %>,*" frameborder="0" id="rowsFrameset" runat="server">
    <frame name="OptionCategoryHeader" src="OptionCategory_Edit_Header.aspx<%=Request.Url.Query%>"
        noresize="noresize" frameborder="0" scrolling="no" />
    <frame name="OptionCategoryEdit" src="OptionCategory_Edit_General.aspx<%=Request.Url.Query%>"
        noresize="noresize" frameborder="0" />
    <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
</frameset>
</html>
