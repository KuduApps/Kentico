<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_WebAnalytics_Tools_Analytics_Statistics"
    Theme="Default" CodeFile="Analytics_Statistics.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/Trees/TreeBorder.ascx" TagName="TreeBorder"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/UI/UIProfiles/UIMenu.ascx" TagName="UIMenu"
    TagPrefix="cms" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>Statistics</title>
    <style type="text/css">
        body
        {
            margin: 0px;
            padding: 0px;
            height: 100%;
            width: 100%;
            overflow: hidden;
        }
    </style>
</head>
<body class="TreeBody <%=mBodyClass%>">
    <form id="form1" runat="server">
    <ajaxToolkit:ToolkitScriptManager runat="server" ID="scriptManager" />
    <asp:Panel runat="server" ID="pnlContentTree" CssClass="ContentTree">
        <div class="TreeArea">
            <div class="TreeAreaTree">
                <cms:UIMenu runat="server" ID="menuElem" ModuleName="CMS.WebAnalytics" JavaScriptHandler="LoadItem"
                    MaxRelativeLevel="-1" ModuleAvailabilityForSiteRequired="true" />
            </div>
        </div>
        <cms:TreeBorder ID="borderElem" runat="server" MinSize="10,*" FramesetName="colsFramesetAnalytics" />
    </asp:Panel>
    <asp:Literal runat="server" ID="ltlScript" EnableViewState="false" />
    <asp:HiddenField ID="hdnMonthsSelectedBar" runat="server" />
    <asp:HiddenField ID="hdnDaysSelectedBar" runat="server" />
    <asp:HiddenField ID="hdnYearSelected" runat="server" />
    </form>

    <script type="text/javascript">
        //<![CDATA[
        function SetSelectedBar(hdnName, value) {
            document.getElementById(hdnName).value = value;
        }

        function selectTreeNode(nodeName) {
            SelectNode(nodeName);
        }

        //]]>    
    </script>

</body>
</html>
