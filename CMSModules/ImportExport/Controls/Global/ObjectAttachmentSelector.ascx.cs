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
using CMS.SiteProvider;
using CMS.PortalEngine;
using CMS.UIControls;

public partial class CMSModules_ImportExport_Controls_Global_ObjectAttachmentSelector : CMSUserControl
{
    protected int mPreviewWidth = 140;
    protected int mPreviewHeight = 108;
    protected int mPanelHeight = 250;
    protected string mIDColumn = null;
    protected string mDisplayNameColumn = null;
    protected string mDescriptionColumn = null;
    protected string mObjectType = SiteObjectType.WEBTEMPLATE;


    /// <summary>
    /// Preview width.
    /// </summary>
    public int PreviewWidth
    {
        get
        {
            return mPreviewWidth;
        }
        set
        {
            mPreviewWidth = value;
        }
    }


    /// <summary>
    /// Preview height.
    /// </summary>
    public int PreviewHeight
    {
        get
        {
            return mPreviewHeight;
        }
        set
        {
            mPreviewHeight = value;
        }
    }


    /// <summary>
    /// Panel height.
    /// </summary>
    public int PanelHeight
    {
        get
        {
            return mPanelHeight;
        }
        set
        {
            mPanelHeight = value;
        }
    }


    /// <summary>
    /// ID column name.
    /// </summary>
    public string IDColumn
    {
        get
        {
            return mIDColumn;
        }
        set
        {
            mIDColumn = value;
        }
    }


    /// <summary>
    /// Display name column name.
    /// </summary>
    public string DisplayNameColumn
    {
        get
        {
            return mDisplayNameColumn;
        }
        set
        {
            mDisplayNameColumn = value;
        }
    }


    /// <summary>
    /// Description column name.
    /// </summary>
    public string DescriptionColumn
    {
        get
        {
            return mDescriptionColumn;
        }
        set
        {
            mDescriptionColumn = value;
        }
    }


    /// <summary>
    /// Template ID.
    /// </summary>
    public int SelectedId
    {
        get
        {
            return ValidationHelper.GetInteger(hdnLastSelected.Value, 0);
        }
        set
        {
            hdnLastSelected.Value = value.ToString();
        }
    }


    /// <summary>
    /// Data source.
    /// </summary>
    public object DataSource
    {
        get
        {
            return rptItems.DataSource;
        }
        set
        {
            rptItems.DataSource = value;
        }
    }


    /// <summary>
    /// Object type.
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


    protected void Page_Load(object sender, EventArgs e)
    {
        ltlScript.Text = ScriptHelper.GetScript("var hdnLastSelected=document.getElementById('" + hdnLastSelected.ClientID + "');");
        ltlScriptAfter.Text = ScriptHelper.GetScript("SelectItem(hdnLastSelected.value);");
    }


    /// <summary>
    /// Bind data.
    /// </summary>
    public void BindData()
    {
        rptItems.DataBind();
    }


    /// <summary>
    /// Gets metafile preview.
    /// </summary>
    /// <param name="objTemplateId">Template ID</param>
    protected string GetPreviewImage(object objTemplateId)
    {
        int templateId = ValidationHelper.GetInteger(objTemplateId, 0);

        DataSet dsPreview = MetaFileInfoProvider.GetMetaFiles(templateId, this.ObjectType, null, null, null, "MetaFileGUID", 0);
        if (!DataHelper.DataSourceIsEmpty(dsPreview))
        {
            string guid = ValidationHelper.GetString(dsPreview.Tables[0].Rows[0]["MetaFileGUID"], "");
            return ResolveUrl("~/CMSPages/GetMetaFile.aspx?fileguid=" + guid);
        }
        else
        {
            return GetImageUrl("Others/Install/no_image.png");
        }
    }
}