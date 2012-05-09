using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using CMS.GlobalHelper;
using CMS.MessageBoard;

public partial class CMSModules_MessageBoards_Controls_MessageActions : BoardMessageActions
{
    protected void Page_PreRender(object sender, EventArgs e)
    {
        // Initialize control elements
        SetupControl();
    }


    #region "Private methods"

    private void SetupControl()
    {
        // Initialize link button labels
        this.lnkApprove.Text = GetString("general.approve");
        this.lnkDelete.Text = GetString("general.delete");
        this.lnkEdit.Text = GetString("general.edit");
        this.lnkReject.Text = GetString("general.reject");

        // Set visibility according to the properties
        this.lnkEdit.Visible = this.ShowEdit;       
        this.lnkDelete.Visible = this.ShowDelete;
        this.lnkApprove.Visible = this.ShowApprove;
        this.lnkReject.Visible = this.ShowReject;

        // Get client script
        ProcessData();

        // Register client script
        ScriptHelper.RegisterDialogScript(this.Page);
        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "DeleteBoardMessageConfirmation", ScriptHelper.GetScript("function ConfirmDelete(){ return confirm(" + ScriptHelper.GetString(GetString("general.confirmdelete")) + ");}"));
    }


    /// <summary>
    /// Generate a client JavaScript for displaying modal window for message editing.
    /// </summary>
    private void ProcessData()
    {
        lnkEdit.OnClientClick = "EditBoardMessage('" + ResolveUrl("~/CMSModules/MessageBoards/CMSPages/Message_Edit.aspx") + "?messageid=" + this.MessageID.ToString() + "&messageboardid=" + this.MessageBoardID.ToString() + "');return false;";
    }

    #endregion


    #region "Event handlers"

    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        FireOnBoardMessageAction("delete", this.MessageID);
    }


    protected void lnkApprove_Click(object sender, EventArgs e)
    {
        FireOnBoardMessageAction("approve", this.MessageID);
    }


    protected void lnkReject_Click(object sender, EventArgs e)
    {
        FireOnBoardMessageAction("reject", this.MessageID);
    }

    #endregion
}