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
using System.Text;

using CMS.SiteProvider;
using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.FormEngine;
using CMS.SettingsProvider;

public partial class CMSModules_CustomTables_Controls_CustomTableViewItem : CMSUserControl
{
    #region "Variables"

    private CustomTableItem mCustomTableItem;

    #endregion


    #region "Properties"

    public CustomTableItem CustomTableItem
    {
        get
        {
            return mCustomTableItem;
        }
        set
        {
            mCustomTableItem = value;
        }
    }

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.CustomTableItem != null)
        {
            StringBuilder sb = new StringBuilder();

            DataClassInfo dci = DataClassInfoProvider.GetDataClass(CustomTableItem.CustomTableClassName);
            if (dci != null)
            {
                sb.Append("<table cellspacing=\"0\" cellpadding=\"3\" rules=\"rows\" border=\"1\" class=\"UniGridGrid\" style=\"border-collapse:collapse;\" width=\"100%\">");
                // Get class form definition
                FormInfo fi = FormHelper.GetFormInfo(dci.ClassName, false);
                string fieldCaption = "";

                FormFieldInfo ffi = null;
                IDataContainer item = CustomTableItem;

                // Table header
                string headerContent = "<tr class=\"UniGridHead\"><th>" + GetString("customtable.data.nametitle") + "</th><th>" + GetString("customtable.data.namevalue") + "</th></tr>";
                sb.Append(headerContent);

                // Go through the columns
                int i = 0;
                foreach (string columnName in item.ColumnNames)
                {
                    // Get field caption
                    ffi = fi.GetFormField(columnName);
                    if (ffi == null)
                    {
                        fieldCaption = columnName;
                    }
                    else
                    {
                        if (ffi.Caption == "")
                        {
                            fieldCaption = columnName;
                        }
                        else
                        {
                            fieldCaption = ResHelper.LocalizeString(ffi.Caption);
                        }
                    }

                    string className = ((i % 2) == 0) ? "EvenRow" : "OddRow";
                    string rowContent = "<tr class=\"" + className + "\"><td style=\"font-weight:bold;white-space: nowrap;\">{0}</td><td width=\"100%\">{1}</td></tr>";
                    sb.Append(String.Format(rowContent, fieldCaption, HTMLHelper.HTMLEncode(ValidationHelper.GetString(item.GetValue(columnName), ""))));
                    ++i;
                }
                sb.Append("</table>");
            }

            string resultTable = sb.ToString();
            if (!string.IsNullOrEmpty(resultTable))
            {
                ltlContent.Text = resultTable;
            }
        }
        else
        {
            ltlContent.Text = GetString("editedobject.notexists");
            return;
        }
    }
}
