using System;
using System.Data;

using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.CMSHelper;
using CMS.SiteProvider;
using CMS.PortalEngine;

[Title(Text = "Page layouts", ImageUrl = "Objects/CMS_Layout/object.png")]
public partial class CMSAPIExamples_Code_Development_PageLayouts_Default : CMSAPIExamplePage
{
    #region "Initialization"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Layout
        this.apiCreateLayout.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(CreateLayout);
        this.apiGetAndUpdateLayout.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndUpdateLayout);
        this.apiGetAndBulkUpdateLayouts.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndBulkUpdateLayouts);
        this.apiDeleteLayout.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(DeleteLayout);
    }

    #endregion


    #region "Mass actions"

    /// <summary>
    /// Runs all creating and managing examples.
    /// </summary>
    public override void RunAll()
    {
        base.RunAll();

        // Layout
        this.apiCreateLayout.Run();
        this.apiGetAndUpdateLayout.Run();
        this.apiGetAndBulkUpdateLayouts.Run();
    }


    /// <summary>
    /// Runs all cleanup examples.
    /// </summary>
    public override void CleanUpAll()
    {
        base.CleanUpAll();

        // Layout
        this.apiDeleteLayout.Run();
    }

    #endregion


    #region "API examples - Layout"

    /// <summary>
    /// Creates layout. Called when the "Create layout" button is pressed.
    /// </summary>
    private bool CreateLayout()
    {
        // Create new layout object
        LayoutInfo newLayout = new LayoutInfo();

        // Set the properties
        newLayout.LayoutDisplayName = "My new layout";
        newLayout.LayoutCodeName = "MyNewLayout";
        newLayout.LayoutDescription = "This is layout created by API Example";
        newLayout.LayoutCode = "<cc1:CMSWebPartZone ID=\"zoneLeft\" runat=\"server\" />";

        // Save the layout
        LayoutInfoProvider.SetLayoutInfo(newLayout);

        return true;
    }


    /// <summary>
    /// Gets and updates layout. Called when the "Get and update layout" button is pressed.
    /// Expects the CreateLayout method to be run first.
    /// </summary>
    private bool GetAndUpdateLayout()
    {
        // Get the layout
        LayoutInfo updateLayout = LayoutInfoProvider.GetLayoutInfo("MyNewLayout");
        if (updateLayout != null)
        {
            // Update the properties
            updateLayout.LayoutDisplayName = updateLayout.LayoutDisplayName.ToLower();

            // Save the changes
            LayoutInfoProvider.SetLayoutInfo(updateLayout);

            return true;
        }

        return false;
    }


    /// <summary>
    /// Gets and bulk updates layouts. Called when the "Get and bulk update layouts" button is pressed.
    /// Expects the CreateLayout method to be run first.
    /// </summary>
    private bool GetAndBulkUpdateLayouts()
    {
        // Prepare the parameters
        string where = "LayoutCodeName LIKE N'MyNewLayout%'";

        // Get the data
        DataSet layouts = LayoutInfoProvider.GetLayouts(where, null);
        if (!DataHelper.DataSourceIsEmpty(layouts))
        {
            // Loop through the individual items
            foreach (DataRow layoutDr in layouts.Tables[0].Rows)
            {
                // Create object from DataRow
                LayoutInfo modifyLayout = new LayoutInfo(layoutDr);

                // Update the properties
                modifyLayout.LayoutDisplayName = modifyLayout.LayoutDisplayName.ToUpper();

                // Save the changes
                LayoutInfoProvider.SetLayoutInfo(modifyLayout);
            }

            return true;
        }

        return false;
    }


    /// <summary>
    /// Deletes layout. Called when the "Delete layout" button is pressed.
    /// Expects the CreateLayout method to be run first.
    /// </summary>
    private bool DeleteLayout()
    {
        // Get the layout
        LayoutInfo deleteLayout = LayoutInfoProvider.GetLayoutInfo("MyNewLayout");

        // Delete the layout
        LayoutInfoProvider.DeleteLayoutInfo(deleteLayout);

        return (deleteLayout != null);
    }

    #endregion
}
