using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.FormEngine;
using CMS.SettingsProvider;

public partial class CMSModules_SmartSearch_Controls_Edit_SearchFields : CMSAdminEditControl
{
    #region "Private variables"

    private DataClassInfo dci = null;
    private DataClassInfo document = null;
    private ArrayList attributes = new ArrayList();
    private FormInfo fi = null;
    private bool mLoadActualValues = false;

    // Contains item list for 'Title' drop-down list.
    private string allowedTitles = "DocumentName;DocumentNamePath;DocumentUrlPath;DocumentPageTitle;DocumentPageDescription;DocumentMenuCaption;DocumentCustomData;DocumentTags;NodeAliasPath;NodeName;NodeAlias;NodeCustomData;SKUNumber;SKUName;SKUDescription;SKUImagePath;SKUCustomData";

    // Contains item list for 'Content' drop-down list.
    string allowedContent = "DocumentName;DocumentNamePath;DocumentUrlPath;DocumentPageTitle;DocumentPageDescription;DocumentMenuCaption;DocumentContent;DocumentCustomData;DocumentTags;NodeAliasPath;NodeName;NodeAlias;NodeCustomData;SKUNumber;SKUName;SKUDescription;SKUImagePath;SKUCustomData";

    // Contains item list for 'Image' field
    string allowedImage = "DocumentContent;SKUImagePath";

    // Contains item list for 'Date' drop-down list.
    string allowedDate = "DocumentModifiedWhen;DocumentCreatedWhen;DocumentCheckedOutWhen;DocumentPublishFrom;DocumentPublishTo;SKULastModified;SKUCreated";

    #endregion


    #region "Public properties"

    /// <summary>
    /// Gets or sets the value that indicates whether dropdown lists should be
    /// filled by actual object values or document values only
    /// </summary>
    public bool LoadActualValues
    {
        get
        {
            return mLoadActualValues;
        }
        set
        {
            mLoadActualValues = value;
        }
    }


    /// <summary>
    /// Indicates if "OK" button should be displayed.
    /// </summary>
    public bool DisplayOkButton
    {
        get
        {
            return ClassFields.DisplayOkButton;
        }
        set
        {
            ClassFields.DisplayOkButton = value;
        }
    }

    /// <summary>
    /// Gets or sets the resource string which is displyed after the save action.
    /// </summary>
    public string SaveResourceString
    {
        get
        {
            return lblInfo.ResourceString;
        }
        set
        {
            lblInfo.ResourceString = value;
        }
    }


    /// <summary>
    /// Resource text for rebuild index label.
    /// </summary>
    public string RebuildIndexResourceString
    {
        get
        {
            return lblRebuildInfo.ResourceString;
        }
        set
        {
            lblRebuildInfo.ResourceString = value;
        }
    }

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        this.ClassFields.OnSaved += new EventHandler(ClassFields_OnSaved);
        this.ClassFields.DisplaySaved = false;

        if (!RequestHelper.IsPostBack())
        {
            this.ReloadData();
        }
    }


    /// <summary>
    /// Reloads data in control.
    /// </summary>
    public override void ReloadData()
    {
        base.ReloadData();
        ReloadSearch(false);
    }


    /// <summary>
    /// Reload data.
    /// </summary>
    /// <param name="setAutomatically">Indicates whether search options should be set automatically</param>
    public void ReloadSearch(bool setAutomatically)
    {
        this.ClassFields.ItemID = this.ItemID;
        this.ClassFields.ReloadData(setAutomatically, true);

        // Initialize properties
        ArrayList itemList = null;
        FormFieldInfo formField = null;

        // Load DataClass
        dci = DataClassInfoProvider.GetDataClass(this.ItemID);

        if (dci != null)
        {
            // Load XML definition
            fi = FormHelper.GetFormInfo(dci.ClassName, true);

            if (String.Compare(dci.ClassName, "cms.user", true) == 0)
            {
                plcImage.Visible = false;
                ClassFields.DisplaySetAutomatically = false;
                pnlIndent.Visible = true;

                document = DataClassInfoProvider.GetDataClass("cms.usersettings");
                if (document != null)
                {
                    FormInfo fiSettings = FormHelper.GetFormInfo(document.ClassName, true);
                    fi.CombineWithForm(fiSettings, true, String.Empty);
                }
            }

            // Get all fields
            itemList = fi.GetFormElements(true, true);
        }

        if (itemList != null)
        {
            if (itemList.Count > 0)
            {
                pnlIndent.Visible = true;
            }

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

        ReloadControls();
    }


    /// <summary>
    /// Reloads drop-down lists with new data.
    /// </summary>
    private void ReloadControls()
    {
        if ((dci != null))
        {
            #region "Load drop-down list 'Title field'"

            drpTitleField.Items.Clear();
            string[] array = null;

            if (!LoadActualValues)
            {
                array = allowedTitles.Split(';');
                foreach (string item in array)
                {
                    if (!String.IsNullOrEmpty(item))
                    {
                        drpTitleField.Items.Add(new ListItem(item));
                    }
                }
            }

            foreach (object[] item in attributes)
            {
                object[] obj = item;
                drpTitleField.Items.Add(new ListItem(obj[0].ToString()));
            }

            // Preselect value
            if (!String.IsNullOrEmpty(dci.ClassSearchTitleColumn))
            {
                drpTitleField.SelectedValue = dci.ClassSearchTitleColumn;
            }
            else
            {
                if (!LoadActualValues)
                {
                    drpTitleField.SelectedValue = "DocumentName";
                }
            }

            #endregion

            #region "Load drop-down list 'Content field'"

            drpContentField.Items.Clear();

            if (!LoadActualValues)
            {
                array = allowedContent.Split(';');
                foreach (string item in array)
                {
                    if (!String.IsNullOrEmpty(item))
                    {
                        drpContentField.Items.Add(new ListItem(item));
                    }
                }
            }
            else
            {
                drpContentField.Items.Add(new ListItem(GetString("general.selectnone"), "0"));
            }

            foreach (object[] item in attributes)
            {
                object[] obj = item;
                drpContentField.Items.Add(new ListItem(obj[0].ToString()));
            }

            // Preselect value
            if (!String.IsNullOrEmpty(dci.ClassSearchContentColumn))
            {
                drpContentField.SelectedValue = dci.ClassSearchContentColumn;
            }
            else
            {
                if (!LoadActualValues)
                {
                    drpContentField.SelectedValue = "DocumentContent";
                }
            }

            #endregion

            #region "Load drop-down list 'Image field'"

            drpImageField.Items.Clear();

            drpImageField.Items.Add(new ListItem(GetString("general.selectnone"), "0"));

            if (!LoadActualValues)
            {
                array = allowedImage.Split(';');
                foreach (string item in array)
                {
                    if (!String.IsNullOrEmpty(item))
                    {
                        drpImageField.Items.Add(new ListItem(item));
                    }
                }
            }

            foreach (object[] item in attributes)
            {
                object[] obj = item;
                drpImageField.Items.Add(new ListItem(obj[0].ToString()));
            }
            // Preselect value
            if (!String.IsNullOrEmpty(dci.ClassSearchImageColumn))
            {
                drpImageField.SelectedValue = dci.ClassSearchImageColumn;
            }

            #endregion

            #region "Load drop-down list 'Date field'"

            drpDateField.Items.Clear();

            if (!LoadActualValues)
            {
                array = allowedDate.Split(';');
                foreach (string item in array)
                {
                    if (!String.IsNullOrEmpty(item))
                    {
                        drpDateField.Items.Add(new ListItem(item));
                    }
                }
            }
            else
            {
                drpDateField.Items.Add(new ListItem(GetString("general.selectnone"), "0"));
            }

            foreach (object[] item in attributes)
            {
                object[] obj = item;
                drpDateField.Items.Add(new ListItem(obj[0].ToString()));
            }

            // Preselect value
            if (!String.IsNullOrEmpty(dci.ClassSearchCreationDateColumn))
            {
                drpDateField.SelectedValue = dci.ClassSearchCreationDateColumn;
            }
            else
            {
                if (!LoadActualValues)
                {
                    drpDateField.SelectedValue = "DocumentCreatedWhen";
                }
            }

            #endregion
        }
    }


    /// <summary>
    /// Calls method from ClassFields control which stores data.
    /// </summary>
    public void SaveData()
    {
        ClassFields.SaveData();
    }

    #endregion


    #region "Events"

    /// <summary>
    /// OK button click handler.
    /// </summary>
    void ClassFields_OnSaved(object sender, EventArgs e)
    {
        if (dci == null)
        {
            dci = DataClassInfoProvider.GetDataClass(this.ItemID);
        }
        if (dci != null)
        {
            dci.ClassSearchTitleColumn = drpTitleField.SelectedValue;
            dci.ClassSearchContentColumn = drpContentField.SelectedValue;
            if (drpImageField.SelectedValue != "0")
            {
                dci.ClassSearchImageColumn = drpImageField.SelectedValue;
            }
            else
            {
                dci.ClassSearchImageColumn = DBNull.Value.ToString();
            }
            dci.ClassSearchCreationDateColumn = drpDateField.SelectedValue;
            DataClassInfoProvider.SetDataClass(dci);
            RaiseOnSaved();
        }

        // Display a message
        if (!string.IsNullOrEmpty(lblInfo.ResourceString)
            || !string.IsNullOrEmpty(lblInfo.Text))
        {
            lblInfo.Visible = true;
        }

        if ((this.ClassFields.Changed) && (!String.IsNullOrEmpty(RebuildIndexResourceString)))
        {
            lblRebuildInfo.Visible = true;
        }
    }

    #endregion
}
