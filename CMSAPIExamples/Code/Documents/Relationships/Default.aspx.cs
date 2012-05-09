using System;
using System.Data;

using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.CMSHelper;
using CMS.SiteProvider;
using CMS.TreeEngine;

[Title(Text = "Context management - Relationships", ImageUrl = "Objects/CMS_RelationshipName/object.png")]
public partial class CMSAPIExamples_Code_Documents_Relationships_Default : CMSAPIExamplePage
{
    #region "Initialization"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Relationship name
        this.apiCreateRelationshipName.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(CreateRelationshipName);
        this.apiGetAndUpdateRelationshipName.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndUpdateRelationshipName);
        this.apiGetAndBulkUpdateRelationshipNames.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndBulkUpdateRelationshipNames);
        this.apiDeleteRelationshipName.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(DeleteRelationshipName);

        // Relationship name on site
        this.apiAddRelationshipNameToSite.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(AddRelationshipNameToSite);
        this.apiRemoveRelationshipNameFromSite.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(RemoveRelationshipNameFromSite);
    
        // Relationship
        this.apiCreateRelationship.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(CreateRelationship);
        this.apiDeleteRelationship.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(DeleteRelationship);
    }

    #endregion


    #region "Mass actions"

    /// <summary>
    /// Runs all creating and managing examples.
    /// </summary>
    public override void RunAll()
    {
        base.RunAll();

        // Relationship name
        this.apiCreateRelationshipName.Run();
        this.apiGetAndUpdateRelationshipName.Run();
        this.apiGetAndBulkUpdateRelationshipNames.Run();

        // Relationship name on site
        this.apiAddRelationshipNameToSite.Run();

        // Relationship
        this.apiCreateRelationship.Run();
    }


    /// <summary>
    /// Runs all cleanup examples.
    /// </summary>
    public override void CleanUpAll()
    {
        base.CleanUpAll();
        
        // Relationship
        this.apiDeleteRelationship.Run();

        // Relationship name on site
        this.apiRemoveRelationshipNameFromSite.Run();

        // Relationship name
        this.apiDeleteRelationshipName.Run();
    }

    #endregion


    #region "API examples - Relationship name"

    /// <summary>
    /// Creates relationship name. Called when the "Create name" button is pressed.
    /// </summary>
    private bool CreateRelationshipName()
    {
        // Create new relationship name object
        RelationshipNameInfo newName = new RelationshipNameInfo();

        // Set the properties
        newName.RelationshipDisplayName = "My new relationship name";
        newName.RelationshipName = "MyNewRelationshipName";

        // Save the relationship name
        RelationshipNameInfoProvider.SetRelationshipNameInfo(newName);

        return true;
    }


    /// <summary>
    /// Gets and updates relationship name. Called when the "Get and update name" button is pressed.
    /// Expects the CreateRelationshipName method to be run first.
    /// </summary>
    private bool GetAndUpdateRelationshipName()
    {
        // Get the relationship name
        RelationshipNameInfo updateName = RelationshipNameInfoProvider.GetRelationshipNameInfo("MyNewRelationshipName");
        if (updateName != null)
        {
            // Update the properties
            updateName.RelationshipDisplayName = updateName.RelationshipDisplayName.ToLower();

            // Save the changes
            RelationshipNameInfoProvider.SetRelationshipNameInfo(updateName);

            return true;
        }

        return false;
    }


    /// <summary>
    /// Gets and bulk updates relationship names. Called when the "Get and bulk update names" button is pressed.
    /// Expects the CreateRelationshipName method to be run first.
    /// </summary>
    private bool GetAndBulkUpdateRelationshipNames()
    {
        // Prepare the parameters
        string where = "RelationshipName LIKE N'MyNewRelationshipName%'";

        // Get the data
        DataSet names = RelationshipNameInfoProvider.GetRelationshipNames(where, null);
        if (!DataHelper.DataSourceIsEmpty(names))
        {
            // Loop through the individual items
            foreach (DataRow nameDr in names.Tables[0].Rows)
            {
                // Create object from DataRow
                RelationshipNameInfo modifyName = new RelationshipNameInfo(nameDr);

                // Update the properties
                modifyName.RelationshipDisplayName = modifyName.RelationshipDisplayName.ToUpper();

                // Save the changes
                RelationshipNameInfoProvider.SetRelationshipNameInfo(modifyName);
            }

            return true;
        }

        return false;
    }


    /// <summary>
    /// Deletes relationship name. Called when the "Delete name" button is pressed.
    /// Expects the CreateRelationshipName method to be run first.
    /// </summary>
    private bool DeleteRelationshipName()
    {
        // Get the relationship name
        RelationshipNameInfo deleteName = RelationshipNameInfoProvider.GetRelationshipNameInfo("MyNewRelationshipName");

        // Delete the relationship name
        RelationshipNameInfoProvider.DeleteRelationshipName(deleteName);

        return (deleteName != null);
    }

    #endregion


    #region "API examples - Relationship name on site"

    /// <summary>
    /// Adds relationship name to site. Called when the "Add name to site" button is pressed.
    /// Expects the CreateRelationshipName method to be run first.
    /// </summary>
    private bool AddRelationshipNameToSite()
    {
        // Get the relationship name
        RelationshipNameInfo name = RelationshipNameInfoProvider.GetRelationshipNameInfo("MyNewRelationshipName");
        if (name != null)
        {
            int nameId = name.RelationshipNameId;
            int siteId = CMSContext.CurrentSiteID;

            // Save the binding
            RelationshipNameSiteInfoProvider.AddRelationshipNameToSite(nameId, siteId);

            return true;
        }

        return false;
    }



    /// <summary>
    /// Removes relationship name from site. Called when the "Remove name from site" button is pressed.
    /// Expects the AddRelationshipNameToSite method to be run first.
    /// </summary>
    private bool RemoveRelationshipNameFromSite()
    {
        // Get the relationship name
        RelationshipNameInfo removeName = RelationshipNameInfoProvider.GetRelationshipNameInfo("MyNewRelationshipName");
        if (removeName != null)
        {
            int siteId = CMSContext.CurrentSiteID;

            // Get the binding
            RelationshipNameSiteInfo nameSite = RelationshipNameSiteInfoProvider.GetRelationshipNameSiteInfo(removeName.RelationshipNameId, siteId);

            // Delete the binding
            RelationshipNameSiteInfoProvider.DeleteRelationshipNameSiteInfo(nameSite);

            return true;
        }

        return false;
    }

    #endregion


    #region "API examples - Relationship"

    /// <summary>
    /// Creates relationship between documents. Called when the "Create relationship" button is pressed.
    /// Expects the CreateRelationshipName method to be run first.
    /// </summary>
    private bool CreateRelationship()
    {
        // Get the relationship name
        RelationshipNameInfo relationshipName = RelationshipNameInfoProvider.GetRelationshipNameInfo("MyNewRelationshipName");
        if (relationshipName != null)
        {
            // Get the tree structure
            TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);

            // Get documents for relationship (the Root document is used for both in this example) 
            TreeNode root = tree.SelectSingleNode(CMSContext.CurrentSiteName, "/", null, true);

            // Create the relationship between documents
            RelationshipProvider.AddRelationship(root.NodeID, root.NodeID, relationshipName.RelationshipNameId);

            return true;
        }

        return false;
    }


    /// <summary>
    /// Deletes relationship between documents. Called when the "Delete relationship" button is pressed.
    /// Expects the CreateRelationshipName and the CreateRelationship methods to be run first.
    /// </summary>
    private bool DeleteRelationship()
    {
        // Get the relationship name
        RelationshipNameInfo relationshipName = RelationshipNameInfoProvider.GetRelationshipNameInfo("MyNewRelationshipName");
        if (relationshipName != null)
        {
            // Get the tree structure
            TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);

            // Get documents which are in relationship (the Root document is used for both in this example) 
            TreeNode root = tree.SelectSingleNode(CMSContext.CurrentSiteName, "/", null, true);

            // Delete the relationship between documents
            RelationshipProvider.RemoveRelationship(root.NodeID, root.NodeID, relationshipName.RelationshipNameId);

            return true;
        }

        return false;
    }

    #endregion

}
