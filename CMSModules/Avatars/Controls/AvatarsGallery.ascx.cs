using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.DataEngine;
using CMS.UIControls;

public partial class CMSModules_Avatars_Controls_AvatarsGallery : CMSAdminEditControl
{
    #region "Variables"

    private AvatarTypeEnum mAvatarType = AvatarTypeEnum.All;
    private int maxSideSize = 100;

    public string avatarUrl = null;

    #endregion


    #region "Public properties"

    /// <summary>
    /// Avatar type.
    /// </summary>
    public AvatarTypeEnum AvatarType
    {
        get
        {
            return mAvatarType;
        }
        set
        {
            mAvatarType = value;
        }
    }


    /// <summary>
    /// Indicates if control buttons should be displayed.
    /// </summary>
    public bool DisplayButtons
    {
        get
        {
            return plcButtons.Visible;
        }
        set
        {
            plcButtons.Visible = value;
        }
    }

    #endregion


    #region "Events and public methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptHelper.RegisterWOpenerScript(Page);

        // Get resource strings        
        this.Page.Title = GetString("avatars.gallery.title");
        lblInfo.ResourceString = "avat.noavatarsfound";

        // Resolve avatar url
        avatarUrl = ResolveUrl("~/CMSModules/Avatars/CMSPages/GetAvatar.aspx?maxsidesize=" + maxSideSize + "&avatarguid=");
        avatarUrl = URLHelper.EncodeQueryString(avatarUrl);
        // Get avatar type from querystring
        string avatarType = QueryHelper.GetString("avatartype", "all");
        this.AvatarType = AvatarInfoProvider.GetAvatarTypeEnum(avatarType);

        // Get client ID from querystring
        string clientID = QueryHelper.GetString("clientid", String.Empty);

        // Avoid XSS injection
        if (!ValidationHelper.IsValidClientID(clientID))
        {
            clientID = String.Empty;
        }

        // Get all avatars form database
        DataSet ds = getAvatars();
        if (!DataHelper.DataSourceIsEmpty(ds))
        {
            repAvatars.DataSource = ds;
            repAvatars.DataBind();
            pnlAvatars.Visible = true;
        }
        else
        {
            lblInfo.Visible = true;
            repAvatars.Visible = false;
            btnOk.Enabled = false;
        }

        // Javacript code to mark selected pictures and to fill hidden input value
        string avatarScript = ScriptHelper.GetScript(
            @"function markImage(id) {
                hidden = document.getElementById('" + hiddenAvatarGuid.ClientID + @"');
                if( hidden.value != '') {
                    img = document.getElementById(hidden.value);
                    img.style.border = '5px solid #FFFFFF' ;
                }
                img = document.getElementById(id);
                img.style.border ='5px solid #A8BACF';
                fillHiddenfield(id);
            }
            
            function fillHiddenfield(id)
            {
                hidden = document.getElementById('" + hiddenAvatarGuid.ClientID + @"');
                hidden.value = id ;
            }");

        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "avatarScript", avatarScript);

        // Javacript code which handles window closing and assignment of guid to parent window 
        string addToHiddenScript = ScriptHelper.GetScript(
            @"function CloseAndRefresh() {                 
                if((wopener!=null) && (wopener.UpdateForm!=null))
                { 
                    wopener.UpdateForm();
                }                
                window.close();
              }

              function addToHidden() {
                 hidden = document.getElementById('" + hiddenAvatarGuid.ClientID + @"'); 
                 wopener." + clientID + "updateHidden(hidden.value, '" + clientID + @"');
                 CloseAndRefresh(); 
              }");

        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "addToHiddenAndClose", addToHiddenScript);

        // Setup up buttons click action
        btnOk.OnClientClick = "addToHidden()";
        btnCancel.OnClientClick = "window.close();";
    }


    /// <summary>
    ///  Creates URL for.
    /// </summary>
    /// <param name="param"></param>
    /// <param name="number"></param>
    public string CreateUrl(string param, object number)
    {
        string url = URLHelper.CurrentURL;
        url = URLHelper.UpdateParameterInUrl(url, param, ValidationHelper.GetString(number, "1"));
        return url;
    }


    /// <summary>
    /// Indicates if there are avatars to be displayed.
    /// </summary>
    /// <returns></returns>
    public bool HasData()
    {
        return repAvatars.HasData();
    }

    #endregion


    #region "Private methods"

    /// <summary>
    /// Gets avatars from database.
    /// </summary>
    /// <returns>dataset</returns>
    private DataSet getAvatars()
    {
        // Get the data
        int currentPage = QueryHelper.GetInteger("tpage", pgrAvatars.CurrentPage);

        int currentGroup = currentPage / pgrAvatars.GroupSize + 1;
        int topN = currentGroup * pgrAvatars.GroupSize * pgrAvatars.PageSize + pgrAvatars.PageSize;
        // Execute query
        DataSet ds = null;
        switch (this.AvatarType)
        {
            case AvatarTypeEnum.Group:
                ds = AvatarInfoProvider.GetAvatars("AvatarName, AvatarGUID", "AvatarType IN ('group','all') AND AvatarIsCustom = 0", null, topN);
                break;

            case AvatarTypeEnum.User:
                ds = AvatarInfoProvider.GetAvatars("AvatarName, AvatarGUID", "AvatarType IN ('user','all') AND AvatarIsCustom = 0", null, topN);
                break;

            case AvatarTypeEnum.All:
            default:
                ds = AvatarInfoProvider.GetAvatars("AvatarName, AvatarGUID", "AvatarType = 'all' AND AvatarIsCustom = 0", null, topN);
                break;
        }
        return ds;
    }


    private void CloseWindow()
    {
        ltlScript.Text = ScriptHelper.GetScript("addToHidden(); CloseAndRefresh();");
    }

    #endregion
}
