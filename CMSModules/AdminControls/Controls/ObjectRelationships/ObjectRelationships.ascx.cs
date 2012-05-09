using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Collections;
using System.Collections.Generic;

using CMS.SiteProvider;
using CMS.CMSHelper;
using CMS.SettingsProvider;
using CMS.GlobalHelper;
using CMS.Scheduler;
using CMS.EmailEngine;
using CMS.WorkflowEngine;
using CMS.WebFarmSync;
using CMS.PortalEngine;
using CMS.ResourceManager;
using CMS.LicenseProvider;
using CMS.FormEngine;
using CMS.UIControls;
using CMS.ExtendedControls;

public partial class CMSModules_AdminControls_Controls_ObjectRelationships_ObjectRelationships : CMSUserControl
{
    #region "Private fields"

    private TranslationHelper th = null;

    private bool showTypes = false;
    private string translationSiteName = TranslationHelper.NO_SITE;
    private bool loaded = false;

    private GeneralizedInfo mObject = null;
    private GeneralizedInfo mRelatedObject = null;

    private string mObjectType = null;
    private int mObjectID = -1;
    private string[] mObjectTypes = null;

    #endregion


    #region "Private properties"

    /// <summary>
    /// Returns true if the left side is active.
    /// </summary>
    private bool ActiveLeft
    {
        get
        {
            return ValidationHelper.GetBoolean(ViewState["ActiveLeft"], false);
        }
        set
        {
            ViewState["ActiveLeft"] = value;
        }
    }


    /// <summary>
    /// Collection of available object types.
    /// </summary>
    private string[] ObjectTypes
    {
        get
        {
            if (mObjectTypes == null)
            {
                return mObjectTypes =
                    new string[] { 
                        FormObjectType.BIZFORM, 
                        PredefinedObjectType.NEWSLETTER, 
                        PredefinedObjectType.NEWSLETTERISSUE, 
                        PredefinedObjectType.NEWSLETTERSUBSCRIBER, 
                        PredefinedObjectType.NEWSLETTERTEMPLATE,
                        PredefinedObjectType.FORUMGROUP, 
                        PredefinedObjectType.FORUM, 
                        SiteObjectType.ROLE, 
                        SchedulerObjectType.SCHEDULEDTASK, 
                        EmailObjectType.EMAILTEMPLATE,
                        WorkflowObjectType.WORKFLOWSCOPE, 
                        PredefinedObjectType.ORDER, 
                        PredefinedObjectType.SHIPPINGOPTION,
                        PredefinedObjectType.PAYMENTOPTION, 
                        PredefinedObjectType.POLL, 
                        PredefinedObjectType.REPORTCATEGORY,
                        PredefinedObjectType.REPORT, 
                        SiteObjectType.USER,
                        SiteObjectType.SITE,
                        WebFarmObjectType.WEBFARMSERVER, 
                        SiteObjectType.CSSSTYLESHEET, 
                        SiteObjectType.COUNTRY, 
                        SiteObjectType.CULTURE, 
                        SiteObjectType.DOCUMENTTYPE,
                        EmailObjectType.EMAILTEMPLATE, 
                        SiteObjectType.FORMUSERCONTROL, 
                        SiteObjectType.INLINECONTROL,
                        SiteObjectType.RESOURCE, 
                        PortalObjectType.PAGELAYOUT, 
                        PortalObjectType.PAGETEMPLATE, 
                        SiteObjectType.RELATIONSHIPNAME, 
                        SiteObjectType.UICULTURE, 
                        PortalObjectType.WEBPARTCONTAINER,
                        PortalObjectType.WEBPART, 
                        LicenseObjectType.LICENSEKEY, 
                        PredefinedObjectType.CUSTOMER, 
                        PredefinedObjectType.SKU, 
                        PredefinedObjectType.OPTIONCATEGORY, 
                        PredefinedObjectType.MANUFACTURER, 
                        PredefinedObjectType.SUPPLIER,
                        PredefinedObjectType.DISCOUNTCOUPON, 
                        PredefinedObjectType.DISCOUNTLEVEL, 
                        PredefinedObjectType.DEPARTMENT,
                        PredefinedObjectType.TAXCLASS, 
                        PredefinedObjectType.CURRENCY, 
                        PredefinedObjectType.EXCHANGETABLE,
                        PredefinedObjectType.ORDERSTATUS, 
                        PredefinedObjectType.PUBLICSTATUS, 
                        PredefinedObjectType.INTERNALSTATUS
                    };
            }

            return mObjectTypes;
        }
    }

    #endregion


    #region "Public properties"

    /// <summary>
    /// Current object.
    /// </summary>
    public GeneralizedInfo Object
    {
        get
        {
            if (mObject == null)
            {
                GeneralizedInfo infoObj = CMSObjectHelper.GetReadOnlyObject(ObjectType);
                if (infoObj != null)
                {
                    mObject = infoObj.GetObject(ObjectID);
                }
            }

            return mObject;
        }
    }


    /// <summary>
    /// Related object type.
    /// </summary>
    public GeneralizedInfo RelatedObject
    {
        get
        {
            if (mRelatedObject == null)
            {
                string selected = this.drpRelatedObjType.SelectedValue;
                if (!String.IsNullOrEmpty(selected))
                {
                    mRelatedObject = CMSObjectHelper.GetReadOnlyObject(selected);
                }
            }

            return mRelatedObject;
        }
    }


    /// <summary>
    /// Type of the current object.
    /// </summary>
    public string ObjectType
    {
        get
        {
            return mObjectType;
        }
        set
        {
            mObjectType = value;
        }
    }


    /// <summary>
    /// ID of the current object.
    /// </summary>
    public int ObjectID
    {
        get
        {
            return mObjectID;
        }
        set
        {
            mObjectID = value;
        }
    }

    #endregion


    #region "Page events"

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        LoadData();
    }


    protected override void OnPreRender(EventArgs e)
    {
        int selectedSiteId = ValidationHelper.GetInteger(this.siteSelector.Value, 0);

        this.pnlNew.Visible = (!this.pnlSite.Visible || (selectedSiteId >= 0)) && (this.RelatedObject != null);

        string safeObjectType = null;

        string activeType = null;
        string currentType = null;

        // Initialize form labels
        if (this.Object != null)
        {
            safeObjectType = this.Object.ObjectType.Replace(".", "_");
            currentType = " (" + GetString("ObjectType." + safeObjectType) + ")";
        }

        if (this.RelatedObject != null)
        {
            safeObjectType = this.RelatedObject.ObjectType.Replace(".", "_");
            activeType = " (" + GetString("ObjectType." + safeObjectType) + ")";
        }

        // Setup link texts
        imgNewRelationship.ImageUrl = GetImageUrl("CMSModules/CMS_Content/Properties/addrelationship.png");
        lnkNewRelationship.Text = GetString("ObjRelationship.New");

        if (this.pnlAddNew.Visible)
        {
            btnSwitchSides.Text = GetString("Relationship.SwitchSides");

            btnOk.Text = GetString("General.Save");
            btnAnother.Text = GetString("General.SaveAndAnother");
            btnCancel.Text = GetString("General.Cancel");

            this.leftCell.Text = GetString("ObjRelationship.LeftSide");
            this.rightCell.Text = GetString("ObjRelationship.RightSide");
            this.middleCell.Text = GetString("Relationship.RelationshipName");

            // Handle the active items
            if (ActiveLeft)
            {
                this.leftCell.Text += activeType;
                this.rightCell.Text += currentType;

                this.selLeftObj.Visible = true;
                this.lblLeftObj.Visible = false;

                this.selRightObj.Visible = false;
                this.lblRightObj.Visible = true;
            }
            else
            {
                this.leftCell.Text += currentType;
                this.rightCell.Text += activeType;

                this.selLeftObj.Visible = false;
                this.lblLeftObj.Visible = true;

                this.selRightObj.Visible = true;
                this.lblRightObj.Visible = false;
            }
        }
        else
        {
            if (!loaded)
            {
                ReloadData();
            }

            // Init the headers of the grid
            if ((this.gridItems.GridView != null) && (this.gridItems.GridView.HeaderRow != null))
            {
                this.gridItems.GridView.HeaderRow.Cells[1].Text += currentType;
                this.gridItems.GridView.HeaderRow.Cells[3].Text += activeType;
            }
        }

        base.OnPreRender(e);
    }


    public void LoadData()
    {
        if (!RequestHelper.IsPostBack())
        {
            // Initialize controls
            SetupControl();
        }

        // Init events
        gridItems.OnExternalDataBound += gridItems_OnExternalDataBound;
        gridItems.OnAction += gridItems_OnAction;

        // Init site selector
        siteSelector.OnSelectionChanged += siteSelector_OnSelectionChanged;
        siteSelector.DropDownSingleSelect.AutoPostBack = true;

        siteSelector.SpecialFields = new string[,] {
            { GetString("general.selectall"), "-1" },
            { GetString("general.globalobjects"), "0" }
            };

        // Display items that are available
        if (this.pnlAddNew.Visible)
        {
            DisplayAvailableItems();
        }

        ControlsHelper.RegisterPostbackControl(this.btnOk);
        ControlsHelper.RegisterPostbackControl(this.btnCancel);
    }

    #endregion


    #region "Grid view handling"

    protected void gridItems_OnAction(string actionName, object actionArgument)
    {
        switch (actionName.ToLower())
        {
            case "delete":
                // Look for info on relationship being removed
                int leftObjId = ValidationHelper.GetInteger(Request.Params["leftObjId"], -1);
                string leftObjType = ValidationHelper.GetString(Request.Params["leftObjType"], "");
                int relationshipId = ValidationHelper.GetInteger(Request.Params["relationshipId"], -1);
                int rightObjId = ValidationHelper.GetInteger(Request.Params["rightObjId"], -1);
                string rightObjType = ValidationHelper.GetString(Request.Params["rightObjType"], "");

                // Remove the relationship if all the necessary information available
                if ((leftObjId > -1) && (leftObjType.Trim() != "") && (relationshipId > -1) && (rightObjId > -1) && (rightObjType.Trim() != ""))
                {
                    ObjectRelationshipInfoProvider.RemoveRelationship(leftObjId, leftObjType, rightObjId, rightObjType, relationshipId);
                }

                // Reload the data
                ReloadData();
                break;

            default:
                break;
        }
    }


    protected object gridItems_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        DataRowView dr = null;

        switch (sourceName.ToLower())
        {
            case "leftobject":
                {
                    dr = (DataRowView)parameter;
                    string objectType = ValidationHelper.GetString(dr["RelationshipLeftObjectType"], "");
                    int objectId = ValidationHelper.GetInteger(dr["RelationshipLeftObjectID"], 0);

                    return GetObjectString(objectId, objectType);
                }

            case "rightobject":
                {
                    dr = (DataRowView)parameter;
                    string objectType = ValidationHelper.GetString(dr["RelationshipRightObjectType"], "");
                    int objectId = ValidationHelper.GetInteger(dr["RelationshipRightObjectID"], 0);

                    return GetObjectString(objectId, objectType);
                }

            case "relationshipname":
                // Relationship name
                int relationshipId = ValidationHelper.GetInteger(parameter, 0);
                RelationshipNameInfo ri = RelationshipNameInfoProvider.GetRelationshipNameInfo(relationshipId);
                if (ri != null)
                {
                    return ri.RelationshipDisplayName;
                }
                break;
        }

        return parameter;
    }


    /// <summary>
    /// Table of the registered object types [objectType -> true]
    /// </summary>
    Hashtable registeredObjects = new Hashtable();


    /// <summary>
    /// Gets the string for the given object.
    /// </summary>
    /// <param name="objectId">Object ID</param>
    /// <param name="objectType">Object type</param>
    protected string GetObjectString(int objectId, string objectType)
    {
        string result = null;

        if ((objectType == this.ObjectType) && (objectId == this.ObjectID))
        {
            // Identity
            result = this.Object.ObjectDisplayName;
        }
        else
        {
            string safeObjectType = objectType.Replace(".", "_");

            // Prepare the name
            string objectName = null;
            string siteName = null;

            if (th != null)
            {
                // Ensure the registration of the objects
                if (registeredObjects[objectType] == null)
                {
                    DataSet ds = (DataSet)gridItems.GridView.DataSource;
                    if (!DataHelper.DataSourceIsEmpty(ds))
                    {
                        DataView dv = new DataView(ds.Tables[0]);

                        // Register the records of the given type
                        dv.RowFilter = "RelationshipLeftObjectType = '" + objectType + "'";
                        th.RegisterRecords(ds.Tables[0], objectType, "RelationshipLeftObjectID", translationSiteName);

                        dv.RowFilter = "RelationshipRightObjectType = '" + objectType + "'";
                        th.RegisterRecords(ds.Tables[0], objectType, "RelationshipRightObjectID", translationSiteName);
                    }

                    registeredObjects[objectType] = true;
                }

                DataRow rdr = th.GetRecord(safeObjectType, objectId);
                if (rdr != null)
                {
                    objectName = ValidationHelper.GetString(rdr["CodeName"], "");
                    siteName = ValidationHelper.GetString(rdr["SiteName"], "");
                }
            }

            // Prepare the object name
            if (!String.IsNullOrEmpty(objectName))
            {
                if (showTypes)
                {
                    result = GetString("ObjectType." + safeObjectType) + " '" + objectName + "'";
                }
                else
                {
                    result = objectName;
                }
            }
            else
            {
                result = GetString("ObjectType." + safeObjectType) + " '" + objectId + "'";
            }

            if (!String.IsNullOrEmpty(siteName) && (siteName != TranslationHelper.NO_SITE))
            {
                result += " (" + siteName + ")";
            }
        }

        return result;
    }

    #endregion


    #region "Event handlers"

    /// <summary>
    /// Refreshes the selection of site.
    /// </summary>
    protected void RefreshNewSiteSelection()
    {
        if (RelatedObject != null)
        {
            if ((RelatedObject.SiteIDColumn != TypeInfo.COLUMN_NAME_UNKNOWN) && (CMSObjectHelper.GetSiteBindingObject(RelatedObject) == null))
            {
                pnlSite.Visible = true;

                siteSelector_OnSelectionChanged(null, null);
            }
            else
            {
                pnlSite.Visible = false;
            }
        }
    }


    protected void lnkNewRelationship_Click(object sender, EventArgs e)
    {
        // Hide and disable unused controls
        pnlEditList.Visible = false;
        pnlAddNew.Visible = true;

        RefreshNewSiteSelection();

        // Initialize drop-down list with available relationship types
        DisplayAvailableRelationships();

        // Initialize drop=down list with available relationship items
        DisplayAvailableItems();

        // Supply the current object name
        lblLeftObj.Text = Object.ObjectDisplayName;
        lblRightObj.Text = Object.ObjectDisplayName;
    }


    protected void btnSwitchSides_Click(object sender, EventArgs e)
    {
        bool newActiveLeft = !ActiveLeft;
        if (newActiveLeft)
        {
            selLeftObj.Value = selRightObj.Value;
        }
        else
        {
            selRightObj.Value = selLeftObj.Value;
        }

        ActiveLeft = newActiveLeft;

        // Display the items that are available
        DisplayAvailableItems();
    }


    protected void drpRelatedObjType_SelectedIndexChanged(object sender, EventArgs e)
    {
        RefreshNewSiteSelection();
    }


    protected void btnAnother_Click(object sender, EventArgs e)
    {
        // Add the relationship between objects
        if (AddRelationship())
        {
            string safeObjectType = this.RelatedObject.ObjectType.Replace(".", "_");
            string activeType = GetString("ObjectType." + safeObjectType);

            string name = null;
            if (ActiveLeft)
            {
                name = ((ListItem)this.selLeftObj.DropDownSingleSelect.SelectedItem).Text;
            }
            else
            {
                name = ((ListItem)this.selRightObj.DropDownSingleSelect.SelectedItem).Text;
            }

            this.lblInfo.Text = String.Format(ResHelper.GetString("Relationship.Saved"), activeType, name);
            this.lblInfo.Visible = true;
        }
    }


    protected void btnOk_Click(object sender, EventArgs e)
    {
        // Add the relationship between objects
        if (AddRelationship())
        {
            // Load the list dialog
            pnlEditList.Visible = true;
            pnlAddNew.Visible = false;
            drpRelatedObjType.Enabled = true;

            // Reload the data
            ReloadData();
        }
    }


    protected void btnCancel_Click(object sender, EventArgs e)
    {
        // Load the list dialog
        pnlEditList.Visible = true;
        pnlAddNew.Visible = false;
        drpRelatedObjType.Enabled = true;

        // Reload the data
        ReloadData();
    }


    /// <summary>
    /// Handles site selection change event.
    /// </summary>
    protected void siteSelector_OnSelectionChanged(object sender, EventArgs e)
    {
        // If the new relationship is being added
        if (pnlAddNew.Visible)
        {
            DisplayAvailableItems();
        }
        else
        {
            // Reload the data
            ReloadData();
        }

        //pnlUpdate.Update();
    }

    #endregion


    #region "Private methods"

    /// <summary>
    /// Reloads the data for the UniGrid control displaying the objects related to the current object.
    /// </summary>
    private void ReloadData()
    {
        loaded = true;

        registeredObjects.Clear();

        gridItems.WhereCondition = GetWhereCondition(drpRelatedObjType.SelectedItem.Value);

        // Prepare the translations table
        th = new TranslationHelper();
        th.UseDisplayNameAsCodeName = true;

        gridItems.ReloadData();
    }


    /// <summary>
    /// Inserts the new relationship according the selected values.
    /// </summary>
    private bool AddRelationship()
    {
        if (ObjectID > 0)
        {
            if (drpRelationship.SelectedItem == null)
            {
                this.lblError.Text = GetString("ObjRelationship.MustSelect");
                this.lblError.Visible = true;
                return false;
            }

            // Get information on type of the selected relationship
            int selectedRelationshipId = ValidationHelper.GetInteger(drpRelationship.SelectedItem.Value, -1);
            string selectedObjType = null;

            // If the main objectis on the left side selected object is taken from rifht drop-down list
            bool currentOnLeft = !ActiveLeft;
            int selectedObjId = currentOnLeft ? ValidationHelper.GetInteger(selRightObj.Value, -1) : ValidationHelper.GetInteger(selLeftObj.Value, -1);

            // Get information on type of the selected object
            selectedObjType = drpRelatedObjType.SelectedItem.Value;

            // If all the necessary information are present
            if ((selectedObjId <= 0) || (selectedRelationshipId <= 0) || (selectedObjType == null))
            {
                this.lblError.Text = GetString("ObjRelationship.MustSelect");
                this.lblError.Visible = true;
                return false;
            }

            if (currentOnLeft)
            {
                ObjectRelationshipInfoProvider.AddRelationship(ObjectID, ObjectType, selectedObjId, selectedObjType, selectedRelationshipId);
                return true;
            }
            else
            {
                ObjectRelationshipInfoProvider.AddRelationship(selectedObjId, selectedObjType, ObjectID, ObjectType, selectedRelationshipId);
                return true;
            }
        }

        return false;
    }


    /// <summary>
    /// Initializes the control properties.
    /// </summary>
    private void SetupControl()
    {
        // Information on current object supplied
        if ((ObjectID != 0) && (ObjectType != null))
        {
            // Fill in the available objects into the filter
            DisplayAvailableObjects();
        }
        else
        {
            lblError.Visible = true;
            lblError.Text = "[ObjectRelationships.ascx] SetupControl()- ObjectID or ObjectType wasn't initialized.";
        }
    }


    /// <summary>
    /// Gets the where condition for the selected object type.
    /// </summary>
    /// <param name="selectedObjectType">Selected object type</param>
    public string GetWhereCondition(string selectedObjectType)
    {
        if (Object != null)
        {
            string where = null;
            translationSiteName = TranslationHelper.NO_SITE;

            if (RelatedObject != null)
            {
                // Get the site name
                int selectedSiteId = 0;

                if ((RelatedObject.SiteIDColumn != TypeInfo.COLUMN_NAME_UNKNOWN) && (CMSObjectHelper.GetSiteBindingObject(RelatedObject) == null))
                {
                    if (siteSelector.DropDownSingleSelect.Items.Count == 0)
                    {
                        siteSelector.Value = CMSContext.CurrentSiteID;
                    }

                    if (this.siteSelector.HasData)
                    {
                        // Set the site name for registration
                        selectedSiteId = ValidationHelper.GetInteger(this.siteSelector.Value, 0);
                        if (selectedSiteId < 0)
                        {
                            // Automatic site name in case of 
                            translationSiteName = TranslationHelper.AUTO_SITENAME;
                        }
                        else
                        {
                            string siteWhere = QueryProvider.GetQuery(RelatedObject.ObjectType + ".selectall", RelatedObject.IDColumn, SqlHelperClass.GetSiteIDWhereCondition(RelatedObject.SiteIDColumn, selectedSiteId), null, 0);

                            // Where condition for the left object
                            string rightWhere = ObjectRelationshipInfoProvider.GetWhereCondition(ObjectID, ObjectType, 0, false, true, selectedObjectType);
                            rightWhere += " AND RelationshipLeftObjectID IN (" + siteWhere + ")";

                            // Where condition for the left object
                            string leftWhere = ObjectRelationshipInfoProvider.GetWhereCondition(ObjectID, ObjectType, 0, true, false, selectedObjectType);
                            leftWhere += " AND RelationshipRightObjectID IN (" + siteWhere + ")";

                            // --- Add site conditions here

                            where = SqlHelperClass.AddWhereCondition(leftWhere, rightWhere, "OR");
                        }
                    }
                }

                showTypes = false;
            }
            else
            {
                showTypes = true;
            }

            if (String.IsNullOrEmpty(where))
            {
                // Get using regular where
                where = ObjectRelationshipInfoProvider.GetWhereCondition(ObjectID, ObjectType, 0, true, true, selectedObjectType);
            }

            return where;
        }

        return null;
    }


    /// <summary>
    /// Fills the given drop-down list with the available relationship types.
    /// </summary>
    private void DisplayAvailableRelationships()
    {
        if (drpRelationship.Items.Count == 0)
        {
            // Get the relationships from DB
            DataSet ds = RelationshipNameInfoProvider.GetAllRelationshipNames(CMSContext.CurrentSite.SiteID, "RelationshipAllowedObjects LIKE '%;##OBJECTS##;%'");
            if (!DataHelper.DataSourceIsEmpty(ds))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    drpRelationship.Items.Add(new ListItem(dr["RelationshipDisplayName"].ToString(), dr["RelationshipNameID"].ToString()));
                }

                drpRelationship.Enabled = true;
            }
            else
            {
                drpRelationship.Items.Add(new ListItem(GetString("General.NoneAvailable"), ""));
                drpRelationship.Enabled = false;
            }
        }
    }


    /// <summary>
    /// Fills given drop-down list with the items of particular type.
    /// </summary>
    private void DisplayAvailableItems()
    {
        if (RelatedObject != null)
        {
            // Prepare the site where
            string where = null;
            if (RelatedObject.SiteIDColumn != TypeInfo.COLUMN_NAME_UNKNOWN)
            {
                int selectedSiteId = -1;

                // Add where 
                if (CMSObjectHelper.GetSiteBindingObject(RelatedObject) == null)
                {
                    selectedSiteId = ValidationHelper.GetInteger(this.siteSelector.Value, -1);
                }

                if (selectedSiteId == 0)
                {
                    where = RelatedObject.SiteIDColumn + " IS NULL";
                }
                else
                {
                    where = RelatedObject.SiteIDColumn + " = " + selectedSiteId;
                }
            }

            // Load the object selectors
            if (ActiveLeft)
            {
                // Active left selector
                selLeftObj.Enabled = true;
                selLeftObj.ObjectType = RelatedObject.ObjectType;
                selLeftObj.WhereCondition = where;
                selLeftObj.Reload(true);

                if (!selLeftObj.HasData)
                {
                    selLeftObj.DropDownSingleSelect.Items.Add(new ListItem(GetString("General.NoneAvailable"), ""));
                    selLeftObj.Enabled = false;
                }
            }
            else
            {
                // Active right selector
                selRightObj.Enabled = true;
                selRightObj.ObjectType = RelatedObject.ObjectType;
                selRightObj.WhereCondition = where;
                selRightObj.Reload(true);

                if (!selRightObj.HasData)
                {
                    selRightObj.DropDownSingleSelect.Items.Add(new ListItem(GetString("General.NoneAvailable"), ""));
                    selRightObj.Enabled = false;
                }
            }
        }
    }


    /// <summary>
    /// Fills given drop-down list with the available object types.
    /// </summary>
    private void DisplayAvailableObjects()
    {
        // For each type of available objects create a new item and insert it to the list
        if (ObjectTypes != null)
        {
            // All selection
            ListItem li = new ListItem(ResHelper.GetString("General.SelectAnything"), "");
            drpRelatedObjType.Items.Add(li);

            SortedDictionary<string, string> types = new SortedDictionary<string, string>();

            // Prepare the list of items
            foreach (string objType in ObjectTypes)
            {
                types[GetString("ObjectTasks." + objType.Replace(".", "_").Replace("#", "_"))] = objType;
            }

            // Fill in the list
            foreach (KeyValuePair<string, string> item in types)
            {
                string objType = item.Value;

                li = new ListItem(item.Key, objType);
                drpRelatedObjType.Items.Add(li);

                // Preselect the same object type as the main object if available
                if (!RequestHelper.IsPostBack() && objType.Equals(this.ObjectType, StringComparison.InvariantCultureIgnoreCase))
                {
                    li.Selected = true;
                }
            }

            drpRelatedObjType.DataBind();
        }
    }

    #endregion
}
