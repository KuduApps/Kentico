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
using CMS.UIControls;
using CMS.PortalEngine;
using CMS.SiteProvider;

public partial class CMSModules_Widgets_Controls_WidgetGeneral : CMSAdminEditControl
{
    #region "Variables"

    private WidgetInfo mWidgetInfo = null;
    private int mWidgetWebpartId = 0;
    private int mWidgetCategoryId = 0;

    #endregion


    #region "Properties"

    /// <summary>
    /// Gets or sets the widgetinfo which is edited. If not loaded yet, ItemID is used for get it from DB.
    /// </summary>
    public WidgetInfo WidgetInfo
    {
        get
        {
            // If not loaded yet
            if ((mWidgetInfo == null) && (this.ItemID > 0))
            {
                mWidgetInfo = WidgetInfoProvider.GetWidgetInfo(this.ItemID);
            }

            return mWidgetInfo;
        }

        set
        {
            mWidgetInfo = value;
        }
    }


    /// <summary>
    /// Gets or sets the parent webpart(Id) of widget.
    /// </summary>
    public int WidgetWebpartId
    {
        get
        {
            return mWidgetWebpartId;
        }
        set
        {
            mWidgetWebpartId = value;
        }
    }


    /// <summary>
    /// Gets or sets the widget category of widget.
    /// </summary>
    public int WidgetCategoryId
    {
        get
        {
            return mWidgetCategoryId;
        }
        set
        {
            mWidgetCategoryId = value;
        }
    }

    #endregion"


    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.WidgetInfo != null)
        {
            if (!RequestHelper.IsPostBack())
            {
                txtCodeName.Text = this.WidgetInfo.WidgetName;
                txtDisplayName.Text = this.WidgetInfo.WidgetDisplayName;
                txtDescription.Text = this.WidgetInfo.WidgetDescription;

                WebPartInfo webpart = WebPartInfoProvider.GetWebPartInfo(this.WidgetInfo.WidgetWebPartID);
                if (webpart != null)
                {
                    txtBasedOn.Text = webpart.WebPartDisplayName;
                }

                categorySelector.Value = this.WidgetInfo.WidgetCategoryID;


                // Init file uploader
                lblThumbnail.Visible = true;
                thumbnailFile.Visible = true;
                thumbnailFile.ObjectID = this.ItemID;
                thumbnailFile.ObjectType = PortalObjectType.WIDGET;
                thumbnailFile.Category = MetaFileInfoProvider.OBJECT_CATEGORY_THUMBNAIL;
            }

            // Validator's texts
            this.rfvDisplayName.ErrorMessage = GetString("general.requiresdisplayname");
            this.rfvCodeName.ErrorMessage = GetString("general.requirescodename");
        }
        else
        {
            this.Visible = false;
        }
    }


    /// <summary>
    /// Sets data to database.
    /// </summary>
    protected void btnOK_Click(object sender, EventArgs e)
    {
        if (!CheckPermissions("cms.widgets", CMSAdminControl.PERMISSION_MODIFY))
        {
            return;
        }

        // Create new widget info if new widget
        if (this.WidgetInfo == null)
        {
            // Parent webpart must be set
            if ((this.WidgetWebpartId == 0) || (this.WidgetCategoryId == 0))
            {
                return;
            }

            this.WidgetInfo = new WidgetInfo();
            this.WidgetInfo.WidgetWebPartID = this.WidgetWebpartId;
            this.WidgetInfo.WidgetCategoryID = this.WidgetCategoryId;
        }

        txtCodeName.Text = TextHelper.LimitLength(txtCodeName.Text.Trim(), 100, "");
        txtDisplayName.Text = TextHelper.LimitLength(txtDisplayName.Text.Trim(), 100, "");

        // Perform validation
        string errorMessage = new Validator().NotEmpty(txtCodeName.Text, rfvCodeName.ErrorMessage).IsCodeName(txtCodeName.Text, GetString("general.invalidcodename"))
            .NotEmpty(txtDisplayName.Text, rfvDisplayName.ErrorMessage).Result;

        if (errorMessage == "")
        {
            // If name changed, check if new name is unique
            if (String.Compare(this.WidgetInfo.WidgetName, txtCodeName.Text, true) != 0)
            {
                WidgetInfo widget = WidgetInfoProvider.GetWidgetInfo(txtCodeName.Text);
                if (widget != null)
                {
                    lblError.Visible = true;
                    lblError.Text = GetString("general.codenameexists");
                    return;
                }
            }

            this.WidgetInfo.WidgetName = txtCodeName.Text;
            this.WidgetInfo.WidgetDisplayName = txtDisplayName.Text;
            this.WidgetInfo.WidgetDescription = txtDescription.Text;

            this.WidgetInfo.WidgetCategoryID = ValidationHelper.GetInteger(categorySelector.Value, WidgetInfo.WidgetCategoryID);

            WidgetInfoProvider.SetWidgetInfo(this.WidgetInfo);

            lblInfo.Text = GetString("general.changessaved");
            lblInfo.Visible = true;

            // Raise save for frame reload
            RaiseOnSaved();

        }
        else
        {
            lblError.Visible = true;
            lblError.Text = errorMessage;
        }
    }
}
