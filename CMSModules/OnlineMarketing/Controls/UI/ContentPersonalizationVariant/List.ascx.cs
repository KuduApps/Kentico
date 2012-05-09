using System;
using System.Data;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.OnlineMarketing;
using CMS.SettingsProvider;
using CMS.TreeEngine;
using CMS.PortalEngine;
using CMS.WorkflowEngine;

using TreeNode = CMS.TreeEngine.TreeNode;

public partial class CMSModules_OnlineMarketing_Controls_UI_ContentPersonalizationVariant_List : CMSAdminListControl
{
    #region "Variables"

    private TreeNode mNode = null;
    private int mNodeID = 0;
    private int mPageTemplateID = 0;
    private string mZoneID = string.Empty;
    private Guid mInstanceGUID = Guid.Empty;
    private VariantTypeEnum mVariantType = VariantTypeEnum.Zone;
    private TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);

    #endregion


    #region "Private properties"

    /// <summary>
    /// Gets or sets the current node.
    /// </summary>
    private TreeNode Node
    {
        get
        {
            if (mNode == null)
            {
                mNode = tree.SelectSingleNode(NodeID, CMSContext.PreferredCultureCode, tree.CombineWithDefaultCulture);
            }

            return mNode;
        }
        set
        {
            mNode = value;
        }
    }

    #endregion


    #region "Properties"

    /// <summary>
    /// Inner grid.
    /// </summary>
    public UniGrid Grid
    {
        get
        {
            return this.gridElem;
        }
    }


    /// <summary>
    /// Indicates if the control should perform the operations.
    /// </summary>
    public override bool StopProcessing
    {
        get
        {
            return base.StopProcessing;
        }
        set
        {
            base.StopProcessing = value;
            this.gridElem.StopProcessing = value;
        }
    }


    /// <summary>
    /// Indicates if the control is used on the live site.
    /// </summary>
    public override bool IsLiveSite
    {
        get
        {
            return base.IsLiveSite;
        }
        set
        {
            base.IsLiveSite = value;
            gridElem.IsLiveSite = value;
        }
    }


    /// <summary>
    /// NodeID of the current document. (Used for checking the access permissions).
    /// </summary>
    public int NodeID
    {
        get
        {
            return mNodeID;
        }
        set
        {
            mNodeID = value;
            mNode = null;
        }
    }


    /// <summary>
    /// Page template ID of the document which this variants belong to.
    /// </summary>
    public int PageTemplateID
    {
        get
        {
            if ((mPageTemplateID <= 0) && (Node != null))
            {
                // Get the template id from the TreeNode
                PageInfo pi = PageInfoProvider.GetPageInfo(CMSContext.CurrentSiteName, Node.NodeAliasPath, CMSContext.PreferredCultureCode, null, Node.NodeID, tree.CombineWithDefaultCulture);
                if ((pi != null) && (pi.PageTemplateInfo != null))
                {
                    mPageTemplateID = pi.PageTemplateInfo.PageTemplateId;
                }
            }

            return mPageTemplateID;
        }
        set
        {
            mPageTemplateID = value;
        }
    }


    /// <summary>
    /// Gets or sets the zone ID.
    /// </summary>
    public string ZoneID
    {
        get
        {
            return mZoneID;
        }
        set
        {
            mZoneID = value;
        }
    }


    /// <summary>
    /// Gets or sets the instance GUID. If the variant is a zone, then the InstanceGuid is Guid.Empty
    /// </summary>
    public Guid InstanceGUID
    {
        get
        {
            return mInstanceGUID;
        }
        set
        {
            mInstanceGUID = value;
        }
    }

    /// <summary>
    /// Gets or sets the type of the variant (webPart/zone/widget).
    /// </summary>
    public VariantTypeEnum VariantType
    {
        get
        {
            return mVariantType;
        }
        set
        {
            mVariantType = value;
        }
    }


    /// <summary>
    /// Gets or sets a value indicating whether sorting of the unigrid is allowed.
    /// </summary>
    public bool AllowSorting
    {
        get
        {
            return gridElem.GridView.AllowSorting;
        }
        set
        {
            gridElem.GridView.AllowSorting = value;
        }
    }

    #endregion


    #region "Methods"

    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        gridElem.EditActionUrl = "Edit.aspx?variantid={0}&nodeid=" + this.NodeID;

        // Grid initialization                
        gridElem.OnAction += new OnActionEventHandler(gridElem_OnAction);

        // If not set, get the page template id for the current node and its document template
        if ((PageTemplateID <= 0) && (Node != null))
        {
            PageTemplateID = Node.DocumentPageTemplateID;
        }

        // Build where condition
        string where = "VariantPageTemplateID = " + PageTemplateID;

        // Display only variants for the current document
        if (Node != null)
        {
            where = SqlHelperClass.AddWhereCondition(where, "(VariantDocumentID = " + Node.DocumentID + ") OR VariantDocumentID IS NULL");
        }

        // Display variants just for a specific zone/webpart/widget
        if (!string.IsNullOrEmpty(ZoneID))
        {
            where = SqlHelperClass.AddWhereCondition(where, "VariantZoneID = '" + SqlHelperClass.GetSafeQueryString(ZoneID, false) + "'");

            if (InstanceGUID != Guid.Empty)
            {
                // Web part/widget condition
                where = SqlHelperClass.AddWhereCondition(where, "VariantInstanceGUID = '" + InstanceGUID + "'");
            }
        }

        gridElem.WhereCondition = where;
    }


    /// <summary>
    /// PreRender event handler.
    /// </summary>
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        this.lblError.Visible = !string.IsNullOrEmpty(this.lblError.Text);
    }


    /// <summary>
    /// Handles UniGrid's OnAction event.
    /// </summary>
    /// <param name="actionName">Name of the action which should be performed</param>
    /// <param name="actionArgument">ID of the item the action should be performed with</param>
    protected void gridElem_OnAction(string actionName, object actionArgument)
    {
        if (!CheckPermissions("CMS.ContentPersonalization", CMSAdminControl.PERMISSION_MODIFY))
        {
            return;
        }

        int variantId = ValidationHelper.GetInteger(actionArgument, 0);
        if (variantId > 0)
        {
            string action = actionName.ToLower();
            switch (action)
            {
                case "delete":
                    {
                        // Get the instance in order to clear the cache
                        ContentPersonalizationVariantInfo vi = ContentPersonalizationVariantInfoProvider.GetContentPersonalizationVariant(variantId);

                        // Delete the object
                        ContentPersonalizationVariantInfoProvider.DeleteContentPersonalizationVariant(variantId);
                        this.RaiseOnAction(string.Empty, null);

                        // Log widget variant synchronization
                        if ((vi != null) && (vi.VariantDocumentID > 0))
                        {
                            // Log synchronization
                            DocumentSynchronizationHelper.LogDocumentChange(Node, TaskTypeEnum.UpdateDocument, tree);
                        }

                        // Clear the variants from the cache
                        if (vi != null)
                        {
                            ReloadWebPartCache(vi.VariantZoneID, vi.VariantInstanceGUID);
                        }

                        // Reload data
                        gridElem.ReloadData();
                    }
                    break;

                case "up":
                case "down":
                    {
                        // Get the instance in order to clear the cache
                        ContentPersonalizationVariantInfo vi = ContentPersonalizationVariantInfoProvider.GetContentPersonalizationVariant(variantId);

                        // Use try/catch due to license check
                        try
                        {
                            if (action == "up")
                            {
                                // Move up
                                ContentPersonalizationVariantInfoProvider.MoveVariantUp(variantId);
                            }
                            else
                            {
                                // Move down
                                ContentPersonalizationVariantInfoProvider.MoveVariantDown(variantId);
                            }

                            this.RaiseOnAction(string.Empty, null);

                            // Log widget variant synchronization
                            if ((vi != null) && (vi.VariantDocumentID > 0))
                            {
                                // Log synchronization
                                DocumentSynchronizationHelper.LogDocumentChange(Node, TaskTypeEnum.UpdateDocument, tree);
                            }
                        }
                        catch (Exception ex)
                        {
                            lblError.Visible = true;
                            lblError.Text = ex.Message;
                        }

                        // Clear the variants from the cache
                        if (vi != null)
                        {
                            ReloadWebPartCache(vi.VariantZoneID, vi.VariantInstanceGUID);
                        }
                    }
                    break;
            }
        }
    }


    /// <summary>
    /// Clear web part variants from the cache and reloads them.
    /// </summary>
    /// <param name="instanceGuid">The instance GUID.</param>
    private void ReloadWebPartCache(string zoneId, Guid instanceGuid)
    {
        // Delete cached web part variants
        if (Node != null)
        {
            bool found = false;

            // Get page template instance and find the webpart instance.
            PageTemplateInfo pti = PageTemplateInfoProvider.GetPageTemplateInfo(Node.DocumentPageTemplateID);
            if ((pti != null)
                && (pti.TemplateInstance != null)
                && (pti.TemplateInstance.WebPartZones != null))
            {
                // Set the CurrentDocument - will be used for detection of the VariantMode (MVT/ContentPersonalization)
                CMSContext.CurrentDocument = Node;

                foreach (WebPartZoneInstance zone in pti.TemplateInstance.WebPartZones)
                {
                    if (found)
                    {
                        break;
                    }

                    // Is zone variant
                    if (instanceGuid == Guid.Empty)
                    {
                        if (zoneId == zone.ZoneID)
                        {
                            // Reload the zone variants
                            zone.LoadVariants(true, VariantModeEnum.ContentPersonalization);
                            break;
                        }
                    }
                    // Is web part variant
                    else
                    {
                        foreach (WebPartInstance wInstance in zone.WebParts)
                        {
                            // Find the web part instance which is to be cleared
                            if (wInstance.InstanceGUID == instanceGuid)
                            {
                                // Reload the web part variants
                                wInstance.LoadVariants(true, VariantModeEnum.ContentPersonalization);
                                found = true;
                                break;
                            }
                        }
                    }
                }
            }
        }
    }

    #endregion
}