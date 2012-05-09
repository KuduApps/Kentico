<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_PortalEngine_UI_PageTemplates_PageTemplate_Edit"
    CodeFile="PageTemplate_Edit.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>Page Templates</title>

    <script type="text/javascript">
        //<![CDATA[
        var IsCMSDesk = true;
        //]]>
    </script>

</head>
<frameset border="0" rows="<%=mHeight %>, *" id="rowsFrameset">
    <frame name="pt_edit_menu" src="PageTemplate_Header.aspx?templateid=<%=Request.QueryString["templateid"] %>&dialog=<%=Request.QueryString["dialog"] %>"
        scrolling="no" frameborder="0" noresize="noresize" />
    <frame name="pt_edit_content" src="PageTemplate_General.aspx?templateid=<%=Request.QueryString["templateid"] %>&dialog=<%=Request.QueryString["dialog"] %>"
        frameborder="0" />
    <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
</frameset>
</html>
