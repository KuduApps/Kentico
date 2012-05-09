<%@ Control Language="C#" AutoEventWireup="True"
    Inherits="CMSAdminControls_UI_HeaderLinks" CodeFile="HeaderLinks.ascx.cs" %>
<%@ Register TagPrefix="cms" Namespace="CMS.UIControls" Assembly="CMS.UIControls" %>
<%@ Register Src="~/CMSAdminControls/UI/UniSelector/UniSelector.ascx" TagName="UniSelector"
    TagPrefix="cms" %>
<asp:PlaceHolder runat="server" ID="pnlCultures">
    <td>
        <div class="UIHeaderSelector">
            <cms:UniSelector ID="ucUICultures" ShortID="cs" ObjectType="CMS.UICulture" ResourcePrefix="cultureselect"
                runat="server" ReturnColumnName="UICultureCode" SelectionMode="SingleButton"
                IsLiveSite="false" />
        </div>
    </td>
</asp:PlaceHolder>
<cms:developmentmode runat="server" id="plcLinks">
    <td>
        <div>
            &nbsp;<asp:HyperLink runat="server" ID="lnkLog" Target="_blank" EnableViewState="false">
                <asp:Image runat="server" ID="imgLog" EnableViewState="false" ImageUrl="~/App_Themes/Default/Images/Objects/CMS_EventLog/list.png" />
            </asp:HyperLink>&nbsp;
            <asp:HyperLink runat="server" ID="lnkDebug" Target="_blank" EnableViewState="false">
                <asp:Image runat="server" ID="imgDebug" EnableViewState="false" ImageUrl="~/App_Themes/Default/Images/CMSModules/CMS_System/debug.png" />
            </asp:HyperLink>
        </div>
    </td>
</cms:developmentmode>
