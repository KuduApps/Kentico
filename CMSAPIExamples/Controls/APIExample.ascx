<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSAPIExamples_Controls_APIExample"
    CodeFile="APIExample.ascx.cs" %>
<cms:CMSUpdatePanel ID="pnlUpdate" runat="server">
    <ContentTemplate>
        <div class="Example">
            <div>
                <asp:Label ID="lblNumber" runat="server" EnableViewState="false" CssClass="ExampleNumber" />
                <asp:Button ID="btnAction" runat="server" EnableViewState="false" OnClick="btnAction_Click" />
                <asp:ImageButton ID="btnShowCode" runat="server" EnableViewState="false" Visible="false"
                    OnClick="btnShowCode_Click" ToolTip="View code" />
            </div>
            <asp:Label ID="lblInfo" runat="server" EnableViewState="false" Visible="false" CssClass="InfoLabel" />
            <asp:Label ID="lblError" runat="server" EnableViewState="false" Visible="false" CssClass="ErrorLabel" />
        </div>
    </ContentTemplate>
</cms:CMSUpdatePanel>
