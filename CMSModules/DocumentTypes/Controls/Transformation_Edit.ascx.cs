using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.SettingsProvider;
using CMS.GlobalHelper;
using CMS.CMSHelper;

public partial class CMSModules_DocumentTypes_Controls_Transformation_Edit : CMSAdminEditControl
{
    #region "Variables"

    private int mDocumentTypeID;
    private TransformationInfo mTransformationInfo;

    #endregion


    #region "Public Properties"

    /// <summary>
    /// ID of control's document type.
    /// </summary>
    public int DocumentTypeID
    {
        get
        {
            return mDocumentTypeID;
        }
        set
        {
            mDocumentTypeID = value;
        }
    }


    /// <summary>
    /// Transformation info object.
    /// </summary>
    public TransformationInfo TransInfo
    {
        get
        {
            return mTransformationInfo;
        }
        set
        {
            mTransformationInfo = value;
        }
    }

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        rfvCodeName.ErrorMessage = GetString("general.erroridentificatorformat");
        lblInfo.Text = GetString("general.changessaved");
        if ((TransInfo != null) && (!RequestHelper.IsPostBack()))
        {
            txtName.Text = TransInfo.TransformationName;
        }
    }


    protected void btnOK_Click(object sender, EventArgs e)
    {
        if (TransInfo == null)
        {
            TransInfo = new TransformationInfo();
        }

        string codeName = txtName.Text.Trim();
        //Test if codename not empty
        string errorMessage = new Validator().NotEmpty(codeName, rfvCodeName.ErrorMessage).Result;

        //Test right format
        if ((errorMessage == "") && (!ValidationHelper.IsIdentifier(codeName.Trim())))
        {
            errorMessage = GetString("general.erroridentificatorformat");
        }

        if (errorMessage != String.Empty)
        {
            lblError.Text = errorMessage;
            lblError.Visible = true;
            return;
        }

        TransInfo.TransformationName = txtName.Text;

        //If edit no DocumentTypeID is set
        if (DocumentTypeID != 0)
        {
            TransInfo.TransformationClassID = DocumentTypeID;
        }

        //Save new Transformation
        TransformationInfo ti = TransformationInfoProvider.GetTransformation(TransInfo.TransformationFullName);
        if ((ti != null) && (ti.TransformationID != TransInfo.TransformationID))
        {
            lblError.Text = GetString("DocumentType_Edit_Transformation_Edit.UniqueTransformationNameDocType");
            lblError.Visible = true;
            return;
        }

        //Write info
        TransInfo.TransformationIsHierarchical = true;
        TransformationInfoProvider.SetTransformation(TransInfo);

        lblInfo.Visible = true;

        RaiseOnSaved();

        // Reload header if changes were saved
        ScriptHelper.RefreshTabHeader(Page, GetString("general.general")); 
    }

    #endregion
}