using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.GlobalHelper;

public partial class CMSModules_Content_Controls_Dialogs_YouTube_YouTubeSizes : CMSUserControl
{
    #region "Variables"

    private int mMaxSideSize = 60;
    private string mOnSelectedItemClick = "";

    #endregion


    #region "Public properties"

    /// <summary>
    /// Gets or sets the maximal size of the rectangle in preview.
    /// </summary>
    public int MaxSideSize
    {
        get
        {
            return this.mMaxSideSize;
        }
        set
        {
            this.mMaxSideSize = value;
        }
    }


    /// <summary>
    /// Gets or sets the JavaScript code which is prcessed when some size is clicked.
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
    /// Gets or sets the width.
    /// </summary>
    public int SelectedWidth
    {
        get
        {
            return ValidationHelper.GetInteger(this.hdnWidth.Value, 0);
        }
        set
        {
            this.hdnWidth.Value = value.ToString();
        }
    }


    /// <summary>
    /// Gets or sets the height.
    /// </summary>
    public int SelectedHeight
    {
        get
        {
            return ValidationHelper.GetInteger(this.hdnHeight.Value, 0);
        }
        set
        {
            this.hdnHeight.Value = value.ToString();
        }
    }

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        // Register scripts
        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "YouTubeSizes", ScriptHelper.GetScript(
            "function setSizes(width, height) { \n" +
            "  var widthElem = document.getElementById('" + this.hdnWidth.ClientID + "'); \n" +
            "  var heightElem = document.getElementById('" + this.hdnHeight.ClientID + "'); \n" +
            "  if ((widthElem != null) && (heightElem != null)) { \n" +
            "    widthElem.value = width; \n" +
            "    heightElem.value = height; \n" + this.OnSelectedItemClick + "; \n" +
            "  } \n" +
            "} \n"));
    }


    #region "Public methods"

    /// <summary>
    /// Loads the sizes previews.
    /// </summary>
    /// <param name="sizes">Array with sizes (two items per box)</param>
    public void LoadSizes(int[] sizes)
    {
        this.plcSizes.Controls.Clear();
        if (sizes.Length > 1)
        {
            int max = sizes[0];
            for (int i = 0; i < sizes.Length; i++)
            {
                if (sizes[i] > max)
                {
                    max = sizes[i];
                }
            }
            double coef = ((double)this.MaxSideSize) / max;

            this.plcSizes.Controls.Add(new LiteralControl("<div style=\"width: 250px;\">"));
            for (int i = 0; i < sizes.Length; i += 2)
            {
                this.plcSizes.Controls.Add(new LiteralControl("<div class=\"YouTubeSizeBox\">" +
                    sizes[i] + " x " + sizes[i + 1] + "<div class=\"YouTubeSize\" style=\"width: " + (int)(sizes[i] * coef) + "px; height: " + (int)(sizes[i + 1] * coef) + "px;\" onclick=\"setSizes('" + sizes[i] + "', '" + sizes[i + 1] + "');\"></div></div>"));
            }
            this.plcSizes.Controls.Add(new LiteralControl("</div>"));
        }
    }

    #endregion
}
