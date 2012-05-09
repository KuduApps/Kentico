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
using CMS.PortalEngine;
using CMS.FormEngine;
using CMS.CMSHelper;
using CMS.SiteProvider;
using CMS.UIControls;

public partial class CMSModules_PortalEngine_Controls_WebParts_WebpartBinding : CMSUserControl
{
    #region "Variables"

    protected string mAliasPath = null;
    protected int mPageTemplateId = 0;
    protected string mZoneId = null;
    protected Guid mInstanceGUID = Guid.Empty;
    protected string mWebpartId = null;    

    /// <summary>
    /// Current page info.
    /// </summary>
    PageInfo pi = null;

    /// <summary>
    /// Page template info.
    /// </summary>
    PageTemplateInfo pti = null;


    /// <summary>
    /// Current bindings.
    /// </summary>
    Hashtable bindings = null;

    /// <summary>
    /// Current web part.
    /// </summary>
    WebPartInstance webPart = null;

    #endregion


    #region "Public properties"

    /// <summary>
    /// Page alias path.
    /// </summary>
    public string AliasPath
    {
        get
        {
            return mAliasPath;
        }
        set
        {
            mAliasPath = value;
        }
    }


    /// <summary>
    /// Page template ID.
    /// </summary>
    public int PageTemplateId
    {
        get
        {
            return mPageTemplateId;
        }
        set
        {
            mPageTemplateId = value;
        }
    }


    /// <summary>
    /// Zone ID.
    /// </summary>
    public string ZoneId
    {
        get
        {
            return mZoneId;
        }
        set
        {
            mZoneId = value;
        }
    }


    /// <summary>
    /// Web part ID.
    /// </summary>
    public string WebpartId
    {
        get
        {
            return mWebpartId;
        }
        set
        {
            mWebpartId = value;
        }
    }


    /// <summary>
    /// Instance GUID.
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

    #endregion


    protected override void OnInit(EventArgs e)
    {
        gvBinding.RowDataBound += new GridViewRowEventHandler(gvBinding_RowDataBound);

        base.OnInit(e);

        gvBinding.Columns[0].HeaderText = GetString("general.action");
        gvBinding.Columns[1].HeaderText = GetString("WebPartBinding.HeaderLocalProperty");
        gvBinding.Columns[2].HeaderText = GetString("WebPartBinding.HeaderSourceProperty");
        gvBinding.GridLines = GridLines.Horizontal;

        BindData();
    }


    /// <summary>
    /// Page load.
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptHelper.RegisterWOpenerScript(Page);
        // delete confirmation
        ltlScript.Text = ScriptHelper.GetScript("var deleteConfirmation = '" + GetString("WebPartBinding.DeleteConfirmation") + "';");

        btnOk.Click += new EventHandler(btnOK_Click);
        gvBinding.RowDeleting += new GridViewDeleteEventHandler(gvBinding_RowDeleting);

        btnOnOK.Click += new EventHandler(btnOnOK_Click);
        btnOnApply.Click += new EventHandler(btnOnApply_Click);

        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "ApplyButton", ScriptHelper.GetScript("function OnApplyButton(){" + Page.ClientScript.GetPostBackEventReference(btnOnApply, "") + "}"));
        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "OKButton", ScriptHelper.GetScript("function OnOKButton(){" + Page.ClientScript.GetPostBackEventReference(btnOnOK, "") + "}"));

        lblCaption.Text = GetString("WebPartBinding.Caption");
        lblProperty.Text = GetString("WebPartBinding.lblProperty");
        lblSourceId.Text = GetString("WebPartBinding.lblSourceId");
        lblSourceProprety.Text = GetString("WebPartBinding.lblSourceProperty");
        rfvSourceId.ErrorMessage = GetString("WebPartBinding.rfvSourceId");
        rfvSourceProperty.ErrorMessage = GetString("WebPartBinding.rfvSourceProperty");
    }


    void gvBinding_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        ImageButton btn = (ImageButton)e.Row.FindControl("lnkDelete");
        if (btn != null)
        {
            btn.ImageUrl = GetImageUrl("Design/Controls/UniGrid/Actions/Delete.png");
        }
    }


    void gvBinding_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        BindData();
    }


    void btnOK_Click(object sender, EventArgs e)
    {
        WebPartBindingInfo bi = new WebPartBindingInfo();
        bi.SourceProperty = txtSourceProperty.Text;
        bi.SourceWebPart = txtSourceId.Text;
        bi.TargetProperty = drpProperty.SelectedValue;
        webPart.Bindings[drpProperty.SelectedValue.ToLower()] = bi;
        Save();
        BindData();
    }


    void btnOnApply_Click(object sender, EventArgs e)
    {
        Save();
        URLHelper.Redirect(URLHelper.Url.AbsoluteUri);
    }


    void btnOnOK_Click(object sender, EventArgs e)
    {
        Save();
        string script = "";
        // Close the window
        ltlScript.Text = ScriptHelper.GetScript(script + "top.window.close();");
    }


    /// <summary>
    /// Saves webpart properties.
    /// </summary>
    public void Save()
    {
        // Update page template
        PageTemplateInfoProvider.SetPageTemplateInfo(pti);
        txtSourceId.Text = "";
        txtSourceProperty.Text = "";
        drpProperty.SelectedIndex = 0;
    }


    public void BindData()
    {
        if (WebpartId != "")
        {
            // Get page info
            pi = CMSWebPartPropertiesPage.GetPageInfo(AliasPath, PageTemplateId);
            if (pi == null)
            {
                this.Visible = false;
                return;
            }

            // Get page template info
            pti = pi.PageTemplateInfo;
            if (pti != null)
            {
                // Get web part instance
                webPart = pti.GetWebPart(InstanceGUID, WebpartId);
                if (webPart == null)
                {
                    return;
                }

                // Get the web part info
                WebPartInfo wpi = WebPartInfoProvider.GetBaseWebPart(webPart.WebPartType);
                if (wpi == null)
                {
                    return;
                }

                // Get webpart properties (XML)
                string wpProperties = wpi.WebPartProperties;
                FormInfo fi = FormHelper.GetWebPartFormInfo(wpi.WebPartName + FormHelper.CORE, wpi.WebPartProperties, null, null, false);

                // Get datarow with required columns
                DataRow dr = fi.GetDataRow();

                // Bind drop down list
                if (!RequestHelper.IsPostBack())
                {
                    DataTable dropTable = new DataTable();
                    dropTable.Columns.Add("name");

                    foreach (DataColumn column in dr.Table.Columns)
                    {
                        dropTable.Rows.Add(column.ColumnName);
                    }

                    dropTable.DefaultView.Sort = "name";
                    drpProperty.DataTextField = "name";
                    drpProperty.DataValueField = "name";
                    drpProperty.DataSource = dropTable.DefaultView;
                    drpProperty.DataBind();
                }

                // Bind grid view
                DataTable table = new DataTable();
                table.Columns.Add("LocalProperty");
                table.Columns.Add("SourceProperty");
                bindings = webPart.Bindings;

                foreach (DataColumn column in dr.Table.Columns)
                {
                    string propertyName = column.ColumnName.ToLower();
                    if (bindings.ContainsKey(propertyName))
                    {
                        WebPartBindingInfo bi = (WebPartBindingInfo)bindings[propertyName];
                        table.Rows.Add(column.ColumnName, bi.SourceWebPart + "." + bi.SourceProperty);
                    }
                }

                gvBinding.DataSource = table;
                gvBinding.DataBind();
            }
        }
    }


    /// <summary>
    /// Delete selected item.
    /// </summary>
    protected void lnkDelete_OnCommand(object sender, CommandEventArgs e)
    {
        string propertyName = e.CommandArgument.ToString().ToLower();
        if (bindings.ContainsKey(propertyName))
        {
            bindings.Remove(propertyName);
            Save();
        }
    }
}
