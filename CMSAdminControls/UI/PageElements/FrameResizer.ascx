<%@ Control Language="C#" AutoEventWireup="true" 
            Inherits="CMSAdminControls_UI_PageElements_FrameResizer" CodeFile="FrameResizer.ascx.cs" %>
    
<asp:PlaceHolder runat="server" ID="plcStandard" EnableViewState="false">
    <div class="<%= CssPrefix + "FrameResizer" %>">
        <img src="<%= minimizeUrl %>" alt="" id="imgMinimize" onclick="Minimize();" 
            onmouseover="document.body.style.cursor='Hand';" onmouseout="document.body.style.cursor='auto';" />
         <img src="<%= maximizeUrl %>" alt="" id="imgMaximize" onclick="Maximize();" style="display: none;"
            onmouseover="document.body.style.cursor='Hand';" onmouseout="document.body.style.cursor='auto';" />
    </div>
    <div id="resizerBorder" class="<%= CssPrefix + "ResizerBorder" %>" style="display: none;">&nbsp;</div>
    <asp:Literal ID="ltlScript" runat="server" EnableViewState="false" />
    <input type="hidden" id="originalSize" value="<%=originalSize%>" />
</asp:PlaceHolder>

<asp:PlaceHolder runat="server" ID="plcAll" EnableViewState="false" Visible="false">
    <a class="<%= CssPrefix + "AllFrameResizer" %>" href="#">
        <span class="ResizerContent">
            <img src="<%= minimizeUrl %>" alt="" id="imgMinimizeAll" onclick="MinimizeAll();"
                onmouseover="document.body.style.cursor='Hand';" onmouseout="document.body.style.cursor='auto';" />
            <img src="<%= maximizeUrl %>" alt="" id="imgMaximizeAll" onclick="MaximizeAll();" style="display: none;" 
                onmouseover="document.body.style.cursor='Hand';" onmouseout="document.body.style.cursor='auto';" />
        </span>
    </a>
</asp:PlaceHolder>