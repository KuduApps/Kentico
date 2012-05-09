using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.ExtendedControls;

public partial class CMSAPIExamples_Pages_APIExamplesPage : CMSMasterPage
{
    #region "Properties"

    public override PageTitle Title
    {
        get
        {
            return this.titleElem;
        }
    }

    #endregion


    #region "Page methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        this.Title.HelpTopicName = "CMS_API_examples_overview";

        this.lblCreate.Text = "Creating and managing";
        this.lblClean.Text = "Cleanup";
        this.lblCreateInfo.Text = "This section provides API examples for creating, getting and updating objects. It is recommended to follow the API examples order.";
        this.lblCleanInfo.Text = "This section provides API examples for deleting objects and their dependencies. The order of the cleanup examples is usually reversed.";
        btnRunAll.ImageUrl = UIHelper.GetImageUrl(Page, "/Others/APIExamples/run.png");
        btnCleanAll.ImageUrl = UIHelper.GetImageUrl(Page, "/Others/APIExamples/clean.png");

        if (!RequestHelper.IsPostBack())
        {
            this.txtCode.Text = @"
//
// Source code of the example will be displayed after clicking the 'View code' button.
//";
        }
    }

    #endregion


    #region Events

    /// <summary>
    /// Runs all create and update examples on the page.
    /// </summary>
    protected void btnRunAll_Click(object sender, EventArgs e)
    {
        CMSAPIExamplePage examplePage = this.Page as CMSAPIExamplePage;
        if (examplePage != null)
        {
            examplePage.RunAll();
        }
    }


    /// <summary>
    /// Runs all cleanup examples on the page.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCleanAll_Click(object sender, EventArgs e)
    {
        CMSAPIExamplePage examplePage = this.Page as CMSAPIExamplePage;
        if (examplePage != null)
        {
            examplePage.CleanUpAll();
        }
    }

    #endregion
}