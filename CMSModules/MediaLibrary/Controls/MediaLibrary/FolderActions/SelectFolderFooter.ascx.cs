using System;

using CMS.ExtendedControls;
using CMS.GlobalHelper;
using CMS.UIControls;

public partial class CMSModules_MediaLibrary_Controls_MediaLibrary_FolderActions_SelectFolderFooter : CMSUserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.StopProcessing)
        {
            this.Visible = false;
        }
        else
        {
            // Setup controls
            SetupControls();
        }
    }


    #region "Private methods"

    /// <summary>
    /// Initializes all the nested controls.
    /// </summary>
    private void SetupControls()
    {        
        CMSDialogHelper.RegisterDialogHelper(this.Page);

        // Register for events
        this.btnInsert.Click += new EventHandler(btnInsert_Click);
        this.btnCancel.Click += new EventHandler(btnCancel_Click);

        switch (QueryHelper.GetString("action", "").ToLower().Trim())
        {
            case "copy":
                this.btnInsert.ResourceString = "general.copy";
                break;

            case "move":
                this.btnInsert.ResourceString = "general.move";
                break;

            default:
                this.btnInsert.ResourceString = "general.select";
                break;
        }
    }

    #endregion


    #region "Event handlers"

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        this.ltlScript.Text = ScriptHelper.GetScript("window.parent.close();");
    }


    protected void btnInsert_Click(object sender, EventArgs e)
    {
        this.ltlScript.Text = ScriptHelper.GetScript("if ((window.parent.frames['selectFolderContent'])&&(window.parent.frames['selectFolderContent'].RaiseAction)) {window.parent.frames['selectFolderContent'].RaiseAction();}");
    }

    #endregion
}
