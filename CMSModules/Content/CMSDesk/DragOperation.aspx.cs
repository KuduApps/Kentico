using System;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.GlobalHelper;

using TreeNode = CMS.TreeEngine.TreeNode;

public partial class CMSModules_Content_CMSDesk_DragOperation : CMSContentPage
{
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        if (!IsCallback)
        {
            string action = QueryHelper.GetString("action", "");

            switch (action.ToLower())
            {
                case "movenode":
                case "movenodeposition":
                case "movenodefirst":
                    {
                        // Setup page title text and image
                        CurrentMaster.Title.TitleText = GetString("dialogs.header.title.movedoc");
                        CurrentMaster.Title.TitleImage = GetImageUrl("CMSModules/CMS_Content/Dialogs/titlemove.png");
                    }
                    break;

                case "copynode":
                case "copynodeposition":
                case "copynodefirst":
                    {
                        // Setup page title text and image
                        CurrentMaster.Title.TitleText = GetString("dialogs.header.title.copydoc");
                        CurrentMaster.Title.TitleImage = GetImageUrl("CMSModules/CMS_Content/Dialogs/titlecopy.png");
                    }
                    break;

                case "linknode":
                case "linknodeposition":
                case "linknodefirst":
                    {
                        // Setup page title text and image
                        CurrentMaster.Title.TitleText = GetString("dialogs.header.title.linkdoc");
                        CurrentMaster.Title.TitleImage = GetImageUrl("CMSModules/CMS_Content/Dialogs/titlelink.png");
                    }
                    break;
            }

            TreeNode node = opDrag.Node;
            if (node != null)
            {
                string documentName = ValidationHelper.GetString(node.GetValue("DocumentLastVersionName"), node.DocumentName);
                CurrentMaster.Title.TitleText += " \"" + HTMLHelper.HTMLEncode(documentName) + "\"";
            }

            ((Panel)CurrentMaster.PanelBody.FindControl("pnlContent")).CssClass = string.Empty;
        }
    }
}
