<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_AdminControls_Controls_ObjectRelationships_ObjectRelationships"
    CodeFile="ObjectRelationships.ascx.cs" %>
<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/UI/UniSelector/UniSelector.ascx" TagName="UniSelector"
    TagPrefix="cms" %>
<%@ Register Namespace="CMS.UIControls.UniGridConfig" TagPrefix="ug" Assembly="CMS.UIControls" %>

<script type="text/javascript">
    //<![CDATA[
    function SetVal(id, value) {
        document.getElementById(id).value = value;
    }

    function SetItems(lObj, ltype, relname, rObj, rtype) {
        SetVal('leftObjId', lObj);
        SetVal('leftObjType', ltype);
        SetVal('relationshipId', relname);
        SetVal('rightObjId', rObj);
        SetVal('rightObjType', rtype);
    }
    //]]>
</script>

<asp:Panel ID="pnlHeader" runat="server" Visible="true">
    <asp:Panel runat="server" ID="pnlSelection" CssClass="PageHeaderLine">
        <table>
            <asp:PlaceHolder runat="server" ID="pnlTypes">
                <tr>
                    <td>
                        <cms:LocalizedLabel runat="server" ID="lblObjType" EnableViewState="false" ResourceString="ObjectRelationships.RelatedObjType" />
                    </td>
                    <td>
                        <asp:DropDownList ID="drpRelatedObjType" CssClass="DropDownField" runat="server"
                            AutoPostBack="True" OnSelectedIndexChanged="drpRelatedObjType_SelectedIndexChanged" />
                    </td>
                </tr>
            </asp:PlaceHolder>
            <asp:PlaceHolder runat="server" ID="pnlSite" Visible="false">
                <tr>
                    <td>
                        <cms:LocalizedLabel runat="server" ID="lblSite" EnableViewState="false" ResourceString="ObjectRelationships.RelatedObjectSite" />
                    </td>
                    <td>
                        <cms:UniSelector ID="siteSelector" runat="server" ObjectType="cms.site" ResourcePrefix="siteselect"
                            AllowEmpty="false" AllowAll="false" />
                    </td>
                </tr>
            </asp:PlaceHolder>
        </table>
    </asp:Panel>
    <asp:Panel runat="server" ID="pnlNew" CssClass="PageHeaderLine">
        <asp:Image ID="imgNewRelationship" runat="server" CssClass="NewItemImage" EnableViewState="false" />
        <asp:LinkButton ID="lnkNewRelationship" runat="server" OnClick="lnkNewRelationship_Click"
            EnableViewState="false" />
    </asp:Panel>
</asp:Panel>
<asp:Panel runat="server" ID="pnlContent" CssClass="PageContent">
    <input type="hidden" id="leftObjId" name="leftObjId" />
    <input type="hidden" id="leftObjType" name="leftObjType" />
    <input type="hidden" id="relationshipId" name="relationshipId" />
    <input type="hidden" id="rightObjId" name="rightObjId" />
    <input type="hidden" id="rightObjType" name="rightObjType" />
    <asp:Panel ID="pnlEditList" runat="server">
        <cms:UniGrid ID="gridItems" runat="server" OrderBy="RelationshipNameID" ObjectType="cms.objectrelationship" DelayedReload="true"
            IsLiveSite="false" Columns="RelationshipLeftObjectType, RelationshipLeftObjectID, RelationshipNameID, RelationshipRightObjectType, RelationshipRightObjectID">
            <GridActions Parameters="RelationshipLeftObjectID;RelationshipLeftObjectType;RelationshipNameID;RelationshipRightObjectID;RelationshipRightObjectType">
                <ug:Action Name="delete" Caption="$Contribution.Actions.Delete$" Icon="Delete.png"
                    OnClick="SetItems({0},'{1}',{2},{3},'{4}');" Confirmation="$General.ConfirmDelete$" />
            </GridActions>
            <GridColumns>
                <ug:Column Source="##ALL##" ExternalSourceName="leftobject" Caption="$ObjRelationship.LeftSide$"
                    Wrap="false" />
                <ug:Column Source="RelationshipNameID" AllowSorting="false" ExternalSourceName="relationshipname"
                    Caption="$Relationship.RelationshipName$" Wrap="false" />
                <ug:Column Source="##ALL##" ExternalSourceName="rightobject" Caption="$ObjRelationship.RightSide$"
                    Wrap="false" />
                <ug:Column Width="100%" />
            </GridColumns>
            <PagerConfig ShowFirstLastButtons="false" ShowDirectPageControl="false" PageSizeOptions="10,25,50,100,##ALL##"
                DefaultPageSize="10" />
            <GridOptions DisplayFilter="false" />
        </cms:UniGrid>
    </asp:Panel>
    <asp:Panel ID="pnlAddNew" runat="server" Visible="false">
        <cms:CMSUpdatePanel runat="server" ID="pnlUpdate" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
                    Visible="false" />
                <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
                    Visible="false" />
                <asp:Table ID="TableRelationship" runat="server" CssClass="UniGridGrid" CellPadding="5"
                    CellSpacing="0" Width="100%">
                    <asp:TableHeaderRow CssClass="UniGridHead" EnableViewState="false">
                        <asp:TableHeaderCell ID="leftCell" HorizontalAlign="Left" Wrap="false" />
                        <asp:TableHeaderCell ID="middleCell" HorizontalAlign="Center" Wrap="false" />
                        <asp:TableHeaderCell ID="rightCell" HorizontalAlign="Right" Wrap="false" />
                    </asp:TableHeaderRow>
                    <asp:TableRow>
                        <asp:TableCell HorizontalAlign="Left" Width="40%" VerticalAlign="Top">
                            <asp:Label ID="lblLeftObj" runat="server" />
                            <cms:UniSelector ID="selLeftObj" runat="server" SelectionMode="SingleDropDownList"
                                AllowEmpty="false" IsLiveSite="false" />
                        </asp:TableCell><asp:TableCell HorizontalAlign="center" Width="20%">
                            <asp:DropDownList ID="drpRelationship" runat="server" CssClass="SelectorDropDown" />
                            <div style="padding-top: 5px">
                                <cms:CMSButton ID="btnSwitchSides" runat="server" CssClass="LongButton" OnClick="btnSwitchSides_Click" />
                            </div>
                        </asp:TableCell><asp:TableCell HorizontalAlign="right" Width="40%" VerticalAlign="Top">
                            <asp:Label ID="lblRightObj" runat="server" />
                            <cms:UniSelector ID="selRightObj" runat="server" SelectionMode="SingleDropDownList"
                                AllowEmpty="false" IsLiveSite="false" />
                        </asp:TableCell></asp:TableRow>
                </asp:Table>
                <br />
                <table width="100%">
                    <tr>
                        <td class="AlignLeft">
                            <cms:CMSButton ID="btnAnother" runat="server" CssClass="LongButton" OnClick="btnAnother_Click"
                                EnableViewState="false" />
                        </td>
                        <td class="AlignRight">
                            <cms:CMSButton ID="btnOk" runat="server" CssClass="SubmitButton" OnClick="btnOk_Click"
                                EnableViewState="false" />
                            <cms:CMSButton ID="btnCancel" runat="server" CssClass="SubmitButton" OnClick="btnCancel_Click"
                                EnableViewState="false" />
                        </td>
                    </tr>
                </table>
                <asp:Literal ID="ltlScript" runat="server" EnableViewState="false" />
            </ContentTemplate>
        </cms:CMSUpdatePanel>
    </asp:Panel>
</asp:Panel>
