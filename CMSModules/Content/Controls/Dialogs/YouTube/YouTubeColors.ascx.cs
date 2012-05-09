using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.GlobalHelper;

public partial class CMSModules_Content_Controls_Dialogs_YouTube_YouTubeColors : CMSUserControl
{
    #region "Variables"

    private string mOnSelectedItemClick = "";

    #endregion


    #region "Public properties"


    /// <summary>
    /// Gets or sets the JavaScript code which is prcessed when some color is clicked.
    /// </summary>
    public string OnSelectedItemClick
    {
        get
        {
            return this.mOnSelectedItemClick;
        }
        set
        {
            this.mOnSelectedItemClick = value;
        }
    }

    /// <summary>
    /// Gets or sets the first color.
    /// </summary>
    public string SelectedColor1
    {
        get
        {
            return this.hdnColor1.Value;
        }
        set
        {
            this.hdnColor1.Value = value;
        }
    }


    /// <summary>
    /// Gets or sets the second color.
    /// </summary>
    public string SelectedColor2
    {
        get
        {
            return this.hdnColor2.Value;
        }
        set
        {
            this.hdnColor2.Value = value;
        }
    }

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        // Register scripts
        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "YouTubeColors", ScriptHelper.GetScript(
            "function setColors(color1, color2) { \n" +
            "  var color1Elem = document.getElementById('" + this.hdnColor1.ClientID + "'); \n" +
            "  var color2Elem = document.getElementById('" + this.hdnColor2.ClientID + "'); \n" +
            "  if ((color1Elem != null) && (color2Elem != null)) { \n" +
            "    color1Elem.value = color1; \n" +
            "    color2Elem.value = color2; \n" + this.OnSelectedItemClick + "; \n" +
            "  } \n" +
            "} \n"));
    }


    #region "Public methods"

    /// <summary>
    /// Loads the color previews.
    /// </summary>
    /// <param name="colors">Array with colors (two colors per box)</param>
    public void LoadColors(string[] colors)
    {
        this.plcColors.Controls.Clear();
        this.plcColors.Controls.Add(new LiteralControl("<div style=\"width: 250px;\">"));
        for (int i = 0; i < colors.Length; i += 2)
        {
            this.plcColors.Controls.Add(new LiteralControl(
                "<div style=\"float: left; padding: 5px; width: 40px;\"><div style=\"cursor: pointer; float: left; width: 20px; height: 20px; background-color: " + colors[i] + ";\" onclick=\"setColors('" + colors[i] + "', '" + colors[i + 1] + "');\"></div>" +
                "<div style=\"cursor: pointer; float:right; width: 20px; height: 20px; background-color: " + colors[i + 1] + ";\" onclick=\"setColors('" + colors[i] + "', '" + colors[i + 1] + "');\"></div></div>"));
        }
        this.plcColors.Controls.Add(new LiteralControl("</div>"));
    }

    #endregion
}
