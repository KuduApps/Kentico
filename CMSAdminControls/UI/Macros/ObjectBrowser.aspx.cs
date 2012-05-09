using System;

using CMS.GlobalHelper;
using CMS.UIControls;

[Title(Text = "Macro browser")]
public partial class CMSAdminControls_UI_Macros_ObjectBrowser : SiteManagerPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        editorElem.Editor.Height = 30;
        editorElem.Editor.ShowToolbar = false;

        treeElem.ContextResolver.CheckIntegrity = false;
        treeElem.MacroExpression = this.editorElem.Text;

        this.txtOutput.Text = treeElem.ContextResolver.ResolveMacros("{%" + this.editorElem.Text + "%}");
    }


    protected void btnRefresh_Click(object sender, EventArgs e)
    {
    }
}
