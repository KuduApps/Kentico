using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.FormEngine;
using CMS.ExtendedControls;

public partial class CMSModules_SmartSearch_Controls_Edit_ClassFields : CMSAdminEditControl
{
    #region "Private variables"

    private const string CONTENT = "Content";
    private const string SEARCHABLE = "Searchable";
    private const string TOKENIZED = "Tokenized";
    private const string IFIELDNAME = "iFieldname";

    private SearchSettings fields = null;
    private DataClassInfo dci = null;
    private bool mDisplayIField = true;
    private DataSet infos = null;
    private FormInfo fi = null;
    private ArrayList attributes = new ArrayList();
    private bool loaded = false;
    private bool mDisplaySaved = true;
    private SearchSettings ss = null;
    private bool mDisplaySetAutomatically = true;
    private bool changed = false;

    #endregion


    #region "Public properties"

    /// <summary>
    /// Indicates if iField column should be displayed. Default true.
    /// </summary>
    public bool DisplayIField
    {
        get
        {
            return mDisplayIField;
        }
        set
        {
            mDisplayIField = value;
        }
    }


    /// <summary>
    /// Gets or sets the value which indicates whether "Set automatically" button should be visible.
    /// </summary>
    public bool DisplaySetAutomatically
    {
        get
        {
            return mDisplaySetAutomatically;
        }
        set
        {
            mDisplaySetAutomatically = value;
        }
    }


    /// <summary>
    /// Indicates if "Changes were saved" info label should be displayed.
    /// </summary>
    public bool DisplaySaved
    {
        get
        {
            return mDisplaySaved;
        }
        set
        {
            mDisplaySaved = value;
        }
    }


    /// <summary>
    /// Indicates if "OK" button should be displayed.
    /// </summary>
    public bool DisplayOkButton
    {
        get
        {
            return ValidationHelper.GetBoolean(ViewState["DisplayOkButton"], true);
        }
        set
        {
            ViewState["DisplayOkButton"] = value;
        }
    }


    /// <summary>
    /// Use after item saved, if true, relevant data for index rebuilt was changed.
    /// </summary>
    public bool Changed
    {
        get
        {
            return changed;
        }
    }

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.DisplaySetAutomatically)
        {
            pnlButton.Visible = false;
        }

        ReloadData(false, false);
    }


    /// <summary>
    /// Reloads data.
    /// </summary>
    /// <param name="SetAutomatically">Indicates if table should be pre-set according to field type</param>
    public void ReloadData(bool setAutomatically, bool forceReload)
    {
        if ((!loaded) || ((loaded) && (forceReload)))
        {
            loaded = true;
            pnlContent.Controls.Clear();
            LoadData();
            this.btnAutomatically.Click += new EventHandler(btnAutomatically_Click);

            // Display checkbox matrix only if field names array is not empty
            if (attributes.Count > 0)
            {
                // Setup controls
                btnAutomatically.Visible = true;
                if (this.fields == null)
                {
                    this.fields = new SearchSettings();
                }

                if (dci != null)
                {
                    this.fields.LoadData(dci.ClassSearchSettings);
                }
                infos = this.fields.GetAllSettingsInfos();

                CreateTable(setAutomatically);

                Literal ltl = new Literal();
                ltl.Text = "<br />";
                pnlContent.Controls.Add(ltl);
            }

            // Setup OK button
            Button btnOk = new CMSButton();
            btnOk.ID = "btnOK";
            btnOk.Text = GetString("general.ok");
            btnOk.Click += new EventHandler(btnOK_Click);
            btnOk.CssClass = "SubmitButton";
            btnOk.Visible = this.DisplayOkButton;
            pnlContent.Controls.Add(btnOk);
        }
    }


    /// <summary>
    /// Loads data.
    /// </summary>
    private void LoadData()
    {
        // Initialize properties
        ArrayList itemList = null;
        FormFieldInfo formField = null;
        attributes.Clear();

        // Load DataClass
        dci = DataClassInfoProvider.GetDataClass(this.ItemID);

        DataClassInfo tree = null;

        // For 'cms.document' add 'ecommerce.sku' fields, too
        if ((dci != null) && (dci.ClassName == "cms.document"))
        {

            // For 'cms.document' add 'ecommerce.sku' fields, too
            tree = DataClassInfoProvider.GetDataClass("ecommerce.sku");

            if (tree != null)
            {
                // Load XML definition
                fi = FormHelper.GetFormInfo(tree.ClassName, false);
                // Get all fields
                itemList = fi.GetFormElements(true, true);
            }

            if (itemList != null)
            {
                // Store each field to array
                foreach (object item in itemList)
                {
                    if (item is FormFieldInfo)
                    {
                        formField = ((FormFieldInfo)(item));
                        object[] obj = { formField.Name, FormHelper.GetDataType(formField.DataType) };
                        attributes.Add(obj);
                    }
                }
            }
        }

        if (dci != null)
        {
            // Load XML definition
            fi = FormHelper.GetFormInfo(dci.ClassName, false);
            // Get all fields
            itemList = fi.GetFormElements(true, true);
            ss = new SearchSettings();
            ss.LoadData(dci.ClassSearchSettings);
        }

        if (itemList != null)
        {
            // Store each field to array
            foreach (object item in itemList)
            {
                if (item is FormFieldInfo)
                {
                    formField = ((FormFieldInfo)(item));
                    object[] obj = { formField.Name, FormHelper.GetDataType(formField.DataType) };
                    attributes.Add(obj);
                }
            }
        }

        // For 'cms.document' add 'cms.tree' fields, too
        if ((dci != null) && (dci.ClassName == "cms.document"))
        {
            tree = DataClassInfoProvider.GetDataClass("cms.tree");

            if (tree != null)
            {
                // Load XML definition
                fi = FormHelper.GetFormInfo(tree.ClassName, false);
                // Get all fields
                itemList = fi.GetFormElements(true, true);
            }

            if (itemList != null)
            {
                // Store each field to array
                foreach (object item in itemList)
                {
                    if (item is FormFieldInfo)
                    {
                        formField = ((FormFieldInfo)(item));
                        object[] obj = { formField.Name, FormHelper.GetDataType(formField.DataType) };
                        attributes.Add(obj);
                    }
                }
            }
        }


        // For 'cms.user' add 'cms.usersettings' fields, too
        if ((dci != null) && (dci.ClassName.ToLower() == "cms.user"))
        {
            tree = DataClassInfoProvider.GetDataClass("cms.usersettings");

            if (tree != null)
            {
                // Load XML definition
                fi = FormHelper.GetFormInfo(tree.ClassName, false);
                // Get all fields
                itemList = fi.GetFormElements(true, true);
            }

            if (itemList != null)
            {
                // Store each field to array
                foreach (object item in itemList)
                {
                    if (item is FormFieldInfo)
                    {
                        formField = ((FormFieldInfo)(item));
                        object[] obj = { formField.Name, FormHelper.GetDataType(formField.DataType) };
                        attributes.Add(obj);
                    }
                }
            }
        }
    }


    /// <summary>
    /// Creates table.
    /// </summary>
    private void CreateTable(bool setAutomatically)
    {
        Table table = new Table();
        table.GridLines = GridLines.Horizontal;
        table.CssClass = "UniGridGrid";
        table.CellPadding = 3;
        table.CellSpacing = 0;
        table.BorderWidth = 1;

        // Create table header
        TableHeaderRow header = new TableHeaderRow();
        header.HorizontalAlign = HorizontalAlign.Left;
        header.CssClass = "UniGridHead";
        TableHeaderCell thc = new TableHeaderCell();
        thc.HorizontalAlign = HorizontalAlign.Center;
        thc.Text = GetString("srch.settings.fieldname");
        thc.Scope = TableHeaderScope.Column;
        thc.Style.Add("text-align", "left");
        thc.Wrap = false;
        thc.CssClass = "ClassFieldsHeaderCell";
        header.Cells.Add(thc);
        thc = new TableHeaderCell();
        thc.Text = GetString("development.content");
        thc.Scope = TableHeaderScope.Column;
        thc.Wrap = false;
        thc.CssClass = "ClassFieldsHeaderCell";
        header.Cells.Add(thc);
        thc = new TableHeaderCell();
        thc.Text = GetString("srch.settings.searchable");
        thc.Scope = TableHeaderScope.Column;
        thc.Wrap = false;
        thc.CssClass = "ClassFieldsHeaderCell";
        header.Cells.Add(thc);
        thc = new TableHeaderCell();
        thc.Text = GetString("srch.settings.tokenized");
        thc.Scope = TableHeaderScope.Column;
        thc.Wrap = false;
        thc.CssClass = "ClassFieldsHeaderCell";
        header.Cells.Add(thc);

        if (this.DisplayIField)
        {
            thc = new TableHeaderCell();
            thc.Text = GetString("srch.settings.ifield");
            header.Cells.Add(thc);
        }

        thc = new TableHeaderCell();
        thc.Width = Unit.Percentage(100);
        header.Cells.Add(thc);

        table.Rows.Add(header);
        pnlContent.Controls.Add(table);

        // Create table content
        if ((attributes != null) && (attributes.Count > 0))
        {
            TableRow tr = null;
            TableCell tc = null;
            Label lbl = null;
            CheckBox chk = null;
            TextBox txt = null;
            SearchSettingsInfo ssi = null;
            int i = 0;

            // Create row for each field
            foreach (object[] item in attributes)
            {
                ssi = null;
                object[] obj = item;
                tr = new TableRow();
                tr.CssClass = ((i % 2) == 0) ? "EvenRow" : "OddRow";
                if (!DataHelper.DataSourceIsEmpty(infos))
                {
                    DataRow[] dr = infos.Tables[0].Select("name = '" + (string)obj[0] + "'");
                    if ((dr != null) && (dr.Length > 0) && (ss != null))
                    {
                        ssi = ss.GetSettingsInfo((string)dr[0]["id"]);
                    }
                }

                // Add cell with field name
                tc = new TableCell();
                tc.CssClass = "ClassFieldsTableCell";
                lbl = new Label();
                lbl.Text = obj[0].ToString();
                tc.Controls.Add(lbl);
                tr.Cells.Add(tc);

                // Add cell with 'Content' value
                tc = new TableCell();
                tc.HorizontalAlign = HorizontalAlign.Center;
                chk = new CheckBox();
                chk.ID = obj[0] + CONTENT;

                if (setAutomatically)
                {
                    chk.Checked = GetPreset(CONTENT, (Type)obj[1]);
                }
                else if (ssi != null)
                {
                    chk.Checked = ssi.Content;
                }

                tc.Controls.Add(chk);
                tr.Cells.Add(tc);

                // Add cell with 'Searchable' value
                tc = new TableCell();
                tc.HorizontalAlign = HorizontalAlign.Center;
                chk = new CheckBox();
                chk.ID = obj[0] + SEARCHABLE;

                if (setAutomatically)
                {
                    chk.Checked = GetPreset(SEARCHABLE, (Type)obj[1]);
                }
                else if (ssi != null)
                {
                    chk.Checked = ssi.Searchable;
                }

                tc.Controls.Add(chk);
                tr.Cells.Add(tc);

                // Add cell with 'Tokenized' value
                tc = new TableCell();
                tc.HorizontalAlign = HorizontalAlign.Center;
                chk = new CheckBox();
                chk.ID = obj[0] + TOKENIZED;

                if (setAutomatically)
                {
                    chk.Checked = GetPreset(TOKENIZED, (Type)obj[1]);
                }
                else if (ssi != null)
                {
                    chk.Checked = ssi.Tokenized;
                }

                tc.Controls.Add(chk);
                tr.Cells.Add(tc);

                // Add cell with 'iFieldname' value
                if (this.DisplayIField)
                {
                    tc = new TableCell();
                    tc.HorizontalAlign = HorizontalAlign.Center;
                    txt = new TextBox();
                    txt.ID = obj[0] + IFIELDNAME;
                    txt.MaxLength = 200;
                    if (ssi != null)
                    {
                        txt.Text = ssi.FieldName;
                    }
                    tc.Controls.Add(txt);
                    tr.Cells.Add(tc);
                }
                tc = new TableCell();
                tr.Cells.Add(tc);
                table.Rows.Add(tr);
                ++i;
            }
        }
    }


    /// <summary>
    /// Gets value which should be preselected for given column.
    /// </summary>
    /// <param name="column">Column name</param>
    /// <param name="formFieldDataTypeEnum">Type of field</param>
    /// <returns>Returns true or false.</returns>
    private bool GetPreset(string column, Type formFieldDataTypeEnum)
    {
        switch (column)
        {
            case CONTENT:
                return (formFieldDataTypeEnum == typeof(String));

            case SEARCHABLE:

                return ((formFieldDataTypeEnum != typeof(Guid)) && (formFieldDataTypeEnum != typeof(String)));

            case TOKENIZED:
                return (formFieldDataTypeEnum == typeof(String));

            default:
                return false;
        }
    }


    /// <summary>
    /// Stores data and raises OnSaved event.
    /// </summary>
    public void SaveData()
    {      
        // Clear old values
        this.fields = new SearchSettings();
        SearchSettingsInfo ssi;
        CheckBox chk;
        TextBox txt;
        changed = false;

        // Create new SearchSettingInfos
        foreach (object[] item in attributes)
        {
            object[] obj = item;
            ssi = new SearchSettingsInfo();
            SearchSettingsInfo ssiOld = null;
            ssi.ID = Guid.NewGuid();
            ssi.Name = obj[0].ToString();

            // Return old data to compare changes
            if (infos != null)
            {
                DataRow[] dr = infos.Tables[0].Select("name = '" + (string)obj[0] + "'");
                if ((dr != null) && (dr.Length > 0) && (ss != null))
                {
                    ssiOld = ss.GetSettingsInfo((string)dr[0]["id"]);
                }
            }

            // Store 'Content' value
            chk = (CheckBox)pnlContent.FindControl(obj[0] + CONTENT);
            if (chk != null)
            {
                // Check change
                if ((ssiOld != null) && (ssiOld.Content != chk.Checked))
                {
                    changed = true;
                }

                ssi.Content = chk.Checked;
            }

            // Store 'Searchable' value
            chk = (CheckBox)pnlContent.FindControl(obj[0] + SEARCHABLE);
            if (chk != null)
            {
                // Check change
                if ((ssiOld != null) && (ssiOld.Searchable != chk.Checked))
                {
                    changed = true;
                }

                ssi.Searchable = chk.Checked;
            }

            // Store 'Tokenized' value
            chk = (CheckBox)pnlContent.FindControl(obj[0] + TOKENIZED);
            if (chk != null)
            {
                // Check change
                if ((ssiOld != null) && (ssiOld.Tokenized != chk.Checked))
                {
                    changed = true;
                }

                ssi.Tokenized = chk.Checked;
            }

            // Store 'iFieldname' value
            txt = (TextBox)pnlContent.FindControl(obj[0] + IFIELDNAME);
            if (txt != null)
            {
                string fieldname = ValidationHelper.GetCodeName(txt.Text.Trim(), null, null);
                if (fieldname.Length > 200)
                {
                    fieldname = fieldname.Substring(0, 200);
                }
                if (!String.IsNullOrEmpty(fieldname))
                {
                    // Check change
                    if ((ssiOld != null) && (ssiOld.FieldName != fieldname))
                    {
                        changed = true;
                    }

                    ssi.FieldName = fieldname;
                }
            }

            this.fields.SetSettingsInfo(ssi);            
        }

        // Store values to DB
        if (dci != null)
        {
            dci.ClassSearchSettings = this.fields.GetData();
            DataClassInfoProvider.SetDataClass(dci);
        }

        if (this.DisplaySaved)
        {
            lblInfo.Visible = true;
        }
        RaiseOnSaved();
    }

    #endregion


    #region "Events"

    public void btnOK_Click(object sender, EventArgs e)
    {
        SaveData();
    }


    /// <summary>
    /// Sets automatically button - click event handler.
    /// </summary>
    void btnAutomatically_Click(object sender, EventArgs e)
    {
        this.ReloadData(true, true);
    }

    #endregion
}
