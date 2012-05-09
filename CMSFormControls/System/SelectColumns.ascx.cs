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
using System.Xml;
using System.Text;

using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.FormEngine;
using CMS.SettingsProvider;

public partial class CMSFormControls_System_SelectColumns : CMS.FormControls.FormEngineUserControl
{
    #region "Properties"

    /// <summary>
    /// Gets or sets the enabled state of the control.
    /// </summary>
    public override bool Enabled
    {
        get
        {
            return base.Enabled;
        }
        set
        {
            base.Enabled = value;
            this.txtColumns.Enabled = value;
            this.btnDesign.Enabled = value;
        }
    }


    ///<summary>Gets or sets field value.</summary>
    public override object Value
    {
        get
        {
            return hdnSelectedColumns.Value;
        }
        set
        {
            hdnSelectedColumns.Value = (string)value;
        }
    }

    #endregion


    #region "Methods"

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        bool IsQuery = true;
        bool IsClassNames = true;
        bool IsCustomTable = true;
        string mJavaScript = "";

        ScriptHelper.RegisterDialogScript(this.Page);

        btnDesign.OnClientClick = "OpenModalDialog('" + hdnSelectedColumns.ClientID.ToString() + "','" + txtColumns.ClientID.ToString() + "'); return false;";

        mJavaScript += "function SetValue(input, txtInput,hdnSelColId,hdnColId){document.getElementById(hdnSelColId).value = input; document.getElementById(hdnColId).value = txtInput;return false }\n";
        mJavaScript += "function GetClassNames(hdnColId)  { return document.getElementById(hdnColId).value; return false;    }\n";
        mJavaScript += "function GetSelectedColumns(hdnSelColId) { return document.getElementById(hdnSelColId).value; return false;   }\n";

        // Try to find QueryName or ClassNames field

        object value = String.Empty;
        bool succ = SqlHelperClass.TryGetDataRowValue(this.Form.DataRow, "QueryName", out value);
        if (succ)
        {
            hdnProperties.Value = value.ToString();
            IsClassNames = false;
            IsCustomTable = false;
        }
        else
        {
            IsQuery = false;
        }

        // If it still can be custom table, try it
        if (IsCustomTable)
        {
            // Fake it as query
            value = String.Empty;
            succ = SqlHelperClass.TryGetDataRowValue(this.Form.DataRow, "CustomTable", out value);
            if (succ)
            {
                hdnProperties.Value = value.ToString();
                IsClassNames = false;
                IsQuery = true;
            }
            else
            {
                IsCustomTable = false;
            }
        }

        // If it still can be class names, try it
        if (IsClassNames)
        {
            value = String.Empty;
            succ = SqlHelperClass.TryGetDataRowValue(this.Form.DataRow, "ClassNames", out value);
            if (succ)
            {
                hdnProperties.Value = value.ToString();
            }
            else
            {
                IsClassNames = false;
            }
        }

        // if QueryName field was found
        if (IsQuery)
        {
            // if query name isnt empty
            if (!String.IsNullOrEmpty(hdnProperties.Value))
            {
                // Custom tables uses selectall query by default
                if (IsCustomTable)
                {
                    hdnProperties.Value += ".selectall";
                }
                mJavaScript += "function OpenModalDialog(hdnSelColId, hdnColId) { modalDialog('" + ResolveUrl("~/CMSFormControls/Selectors/GridColumnDesigner.aspx") + "?queryname=" + hdnProperties.Value + "&SelColId='+hdnSelColId+'&ColId='+hdnColId + '&hash=" + ValidationHelper.GetHashString("?queryname=" + hdnProperties.Value + "&SelColId=" + hdnSelectedColumns.ClientID.ToString() + "&ColId=" + txtColumns.ClientID.ToString()) + "' ,'GridColumnDesigner', 700, 560); return false;}\n";
            }
            else
            {
                String message = String.Empty;
                // Different message for query and custom table
                if (IsCustomTable)
                {
                    DataSet ds = DataClassInfoProvider.GetClasses("ClassID", "ClassIsCustomTable = 1 AND ClassID IN (SELECT ClassID FROM CMS_ClassSite WHERE SiteID = " + CMSContext.CurrentSiteID + ")", String.Empty, 1);
                    message = DataHelper.DataSourceIsEmpty(ds) ? GetString("SelectColumns.nocustomtablesavaible") : GetString("SelectColumns.ApplyFirst");
                }
                else
                {
                    message = GetString("SelectColumns.EmptyQueryName");
                }

                mJavaScript += "function OpenModalDialog(hdnSelColId, hdnColId) { alert('" + message + "'); return false;}\n";
            }
        }
        else if (IsClassNames)
        {
            mJavaScript += "function OpenModalDialog(hdnSelColId, hdnColId) { modalDialog('" + ResolveUrl("~/CMSFormControls/Selectors/GridColumnDesigner.aspx") + "?classnames=" + hdnProperties.Value + "&SelColId=' + hdnSelColId + '&ColId='+hdnColId ,'GridColumnDesigner', 700, 560); return false;}\n";
        }
        else // Cant find QueryName or ClassNames or Custom table fiels 
        {
            mJavaScript += "function OpenModalDialog(hdnSelColId, hdnColId) { alert('" + GetString("SelectColumns.EmptyClassNamesAndQueryName") + "');}\n";
        }

        //Register JavaScript
        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "SelectColumsGlobal", ScriptHelper.GetScript(mJavaScript));

        this.btnDesign.Text = GetString("general.select");

        // Set to Textbox selected columns
        txtColumns.Text = ConvertXML(hdnSelectedColumns.Value.ToString());

    }


    /// <summary>
    /// Convert XML to TextBox.
    /// </summary>
    /// <param name="mXML">XML document</param>
    public string ConvertXML(string mXML)
    {
        if (DataHelper.GetNotEmpty(mXML, "") == "")
        {
            return "";
        }

        StringBuilder mToReturn = new StringBuilder();
        XmlDocument mXMLDocument = new XmlDocument();
        mXMLDocument.LoadXml(mXML);
        XmlNodeList NodeList = mXMLDocument.DocumentElement.GetElementsByTagName("column");

        int i = 0;

        foreach (XmlNode node in NodeList)
        {
            if (i > 0)
            {
                mToReturn.Append(";");
            }

            mToReturn.Append(XmlHelper.GetXmlAttributeValue(node.Attributes["name"], ""));

            i++;
        }

        return mToReturn.ToString();
    }

    #endregion
}
