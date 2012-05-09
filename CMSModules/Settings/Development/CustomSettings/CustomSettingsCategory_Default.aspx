<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CustomSettingsCategory_Default.aspx.cs"
    Inherits="CMSModules_Settings_Development_CustomSettings_CustomSettingsCategory_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server" enableviewstate="false">
    <title>Settings - Default</title>
    <style type="text/css">
        body
        {
            margin: 0px;
            padding: 0px;
            height: 100%;
        }
    </style>
</head>
<frameset border="0" rows="<%= TabsBreadFrameHeight %>, *" id="rowsFrameset">
    <frame name="customsettingscategorytabs" src="CustomSettingsCategory_Tabs.aspx?selectedtab=<%=mSelectedTab %>&treeroot=<%=Request.QueryString["treeroot"]%>&categoryid=<%=mTabsCategoryId%>&siteid=<%=Request.QueryString["siteid"]%>"
        scrolling="no" frameborder="0" noresize />
    <frame name="customsettingscategorycontent" src="<%=mContentLink %>" id="contentframe"
        frameborder="0" noresize />
    <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
</frameset>
</html>
