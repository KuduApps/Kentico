using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;

using CMS.UIControls;
using CMS.FormEngine;
using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.DataEngine;
using CMS.SettingsProvider;
using CMS.FormControls;

public partial class CMSModules_AdminControls_Controls_Class_FieldEditor_ControlSettings : CMSUserControl
{
    #region "Variables"

    private FormInfo fi = null;
    private static Hashtable mSettings = null;

    #endregion


    #region "Properties"

    /// <summary>
    /// FormInfo for specific control.
    /// </summary>
    public FormInfo FormInfo
    {
        get
        {
            return fi;
        }
        set
        {
            fi = value;
        }
    }


    /// <summary>
    /// Shows in what control is this form used.
    /// </summary>
    public FormTypeEnum FormType
    {
        get
        {
            return form.FormType;
        }
        set
        {
            form.FormType = value;
        }
    }


    /// <summary>
    /// Field settings hashtable.
    /// </summary>
    public Hashtable Settings
    {
        get
        {
            return mSettings;
        }
        set
        {
            mSettings = new Hashtable(value, StringComparer.InvariantCultureIgnoreCase);
        }
    }


    /// <summary>
    /// Basic form data.
    /// </summary>
    public DataRow FormData
    {
        get
        {
            return form.DataRow;
        }
    }

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        pnlSettings.GroupingText = GetString("templatedesigner.section.settings");
    }


    /// <summary>
    /// Checks if form is loaded with any controls and returns appropriate value.
    /// </summary>
    public bool CheckVisibility()
    {
        if (!form.IsAnyFieldVisible())
        {
            this.Visible = false;
        }
        else
        {
            this.Visible = true;
        }
        return this.Visible;
    }


    /// <summary>
    /// Reloads control.
    /// </summary>
    public void Reload()
    {
        if (fi != null)
        {
            form.SubmitButton.Visible = false;
            form.SiteName = CMSContext.CurrentSiteName;
            form.FormInformation = this.FormInfo;
            form.Data = GetData();
            form.ShowPrivateFields = true;
        }
        else
        {
            form.DataRow = null;
            form.FormInformation = null;
        }
        form.ReloadData();
    }


    /// <summary>
    /// Saves basic form data.
    /// </summary>
    public bool SaveData()
    {
        if (form.Visible)
        {
            return form.SaveData(null);
        }

        return true;
    }


    /// <summary>
    /// Loads DataRow for BasicForm with data from FormFieldInfo settings.
    /// </summary>
    private DataRowContainer GetData()
    {
        DataRowContainer result = new DataRowContainer(this.FormInfo.GetDataRow());

        if (this.Settings != null)
        {
            foreach (string columnName in result.ColumnNames)
            {
                if (Settings.ContainsKey(columnName) && !String.IsNullOrEmpty(Convert.ToString(Settings[columnName])))
                {
                    result[columnName] = Settings[columnName];
                }
            }
        }
        return result;
    }

    #endregion
}