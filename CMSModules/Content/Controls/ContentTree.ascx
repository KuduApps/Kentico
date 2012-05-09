<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_Content_Controls_ContentTree" CodeFile="ContentTree.ascx.cs" %>
<asp:Label runat="server" ID="lblError" ForeColor="Red" EnableViewState="false" />
<asp:PlaceHolder runat="server" ID="plcDrag" EnableViewState="false">
    <asp:Literal runat="server" ID="ltlCaptureCueCtrlShift" EnableViewState="false" />

    <script type="text/javascript">
        //<![CDATA[
        var recursiveDragAndDrop = true;
        var dropAfter = true;
        var dragYOffset = 15;
        var dragKeepCopy = true;
        var leftCueOffset = 16;
        var captureCueCtrl = true;
        var tagDraggedElem = true;

        function OnDropNode(sender, e) {
            var item = e.get_droppedItem();
            var target = item.parentNode;

            var itemNodeId = item.id.substring(5);
            var targetNodeId = target.id.substring(7);

            if (/^[0-9]+$/.test(targetNodeId) && /^[0-9]+$/.test(itemNodeId)) {
                var copy = false;
                var link = false;
                if (window._event != null) {
                    copy = _event.ctrlKey;
                    link = (_event.ctrlKey && _event.shiftKey);
                }

                if (target.isOnLeft) {
                    MoveNodeAsync(itemNodeId, targetNodeId, true, copy, link);
                }
                else {
                    MoveNodeAsync(itemNodeId, targetNodeId, false, copy, link);
                }
            }
        }

        function DragActionDone(rvalue, context) {
            if ((rvalue != null) && (rvalue != '')) {
                setTimeout(rvalue, 0);
            }
        }
        //]]>
    </script>

    <div style="display: none;">
        <asp:Panel runat="server" ID="pnlCue" CssClass="DDCue" EnableViewState="false">
            <div class="DDCueInside">
                <div class="MoveHere">
                    <cms:LocalizedLabel runat="server" ID="lblMoveHere" ResourceString="ContentTree.MoveHere" /></div>
                <div class="CopyHere">
                    <cms:LocalizedLabel runat="server" ID="lblCopyHere" ResourceString="ContentTree.CopyHere" /></div>
                <div class="LinkHere">
                    <cms:LocalizedLabel runat="server" ID="lblLinkHere" ResourceString="ContentTree.LinkHere" /></div>
                &nbsp;</div>
        </asp:Panel>
    </div>
</asp:PlaceHolder>
<asp:Panel runat="server" ID="pnlTree">
    <cms:UITreeView ID="treeElem" ShortID="t" runat="server" PopulateNodesFromClient="True" ShowLines="True"
        CssClass="ContentTree" OnTreeNodePopulate="treeElem_TreeNodePopulate" EnableViewState="False" />
</asp:Panel>
<asp:Literal runat="server" ID="ltlScript" EnableViewState="false" />
