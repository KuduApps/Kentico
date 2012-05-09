using System;

using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.UIControls;

public partial class CMSModules_CustomTables_CustomTable_Edit_General : CMSCustomTablesPage
{
    #region "Private fields"

    private int mClassId = 0;
    private DataClassInfo mCurrentClass = null;

    #endregion


    #region "Private properties"

    /// <summary>
    /// ID of the current class.
    /// </summary>
    private int ClassID
    {
        get
        {
            if (mClassId == 0)
            {
                mClassId = QueryHelper.GetInteger("customtableid", 0);
            }

            return mClassId;
        }
    }


    /// <summary>
    /// Indicates whether the changes were saved.
    /// </summary>
    private bool WasSaved
    {
        get
        {
            return !String.IsNullOrEmpty(QueryHelper.GetString("saved", String.Empty));
        }
    }


    /// <summary>
    /// Gets the info on current class.
    /// </summary>
    private DataClassInfo CurrentClass
    {
        get
        {
            if (mCurrentClass == null)
            {
                if (ClassID > 0)
                {
                    mCurrentClass = DataClassInfoProvider.GetDataClass(ClassID);
                    // Set edited object
                    EditedObject = mCurrentClass;
                }
            }

            return mCurrentClass;
        }
    }

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Initialize controls
        SetupControl();

        revCodeNameName.ErrorMessage = GetString("customtable.newwizzard.CodeNameIdentificator");
        revNameNamespace.ErrorMessage = GetString("customtable.newwizzard.NamespaceNameIdentificator");
        revCodeNameName.ValidationExpression = ValidationHelper.IdentifierRegExp.ToString();
        revNameNamespace.ValidationExpression = ValidationHelper.IdentifierRegExp.ToString();

        if (!RequestHelper.IsPostBack())
        {
            // Fills the existing class data
            LoadData();


            if (WasSaved)
            {
                // Display information on success to the user                                
                lblInfo.Visible = true;
                lblInfo.Text = GetString("general.changessaved");
                // Refresh parent frame header (due to data list page)
                ScriptHelper.RefreshTabHeader(Page, null);
            }
        }
    }

    #endregion


    #region "Event handling"

    protected void btnOk_Click(object sender, EventArgs e)
    {
        if (CurrentClass != null)
        {
            // Validate the form entries
            string errMsg = ValidateForm();
            if (errMsg == String.Empty)
            {
                CurrentClass.ClassDisplayName = txtDisplayName.Text;
                CurrentClass.ClassName = txtCodeNameNamespace.Text + "." + txtCodeNameName.Text;

                mCurrentClass.ClassNewPageURL = txtNewPage.Text;
                mCurrentClass.ClassViewPageUrl = txtViewPage.Text;
                mCurrentClass.ClassEditingPageURL = txtEditingPage.Text;
                mCurrentClass.ClassListPageURL = txtListPage.Text;

                DataClassInfoProvider.SetDataClass(CurrentClass);

                string editUrl = "~/CMSModules/CustomTables/CustomTable_Edit_General.aspx?customtableid=" + ClassID + "&saved=1";

                URLHelper.Redirect(editUrl);
            }
            else
            {
                // Display error message to the user
                lblError.Visible = true;
                lblError.Text = errMsg;
            }
        }
    }

    #endregion


    #region "Private methods"

    /// <summary>
    /// Initializes the controls on the page.
    /// </summary>
    private void SetupControl()
    {
        // Set the validators' error messages
        rfvDisplayName.ErrorMessage = GetString("sysdev.class_edit_gen.displayname");
        rfvCodeNameNamespace.ErrorMessage = GetString("sysdev.class_edit_gen.namespace");
        rfvCodeNameName.ErrorMessage = GetString("sysdev.class_edit_gen.name");
    }


    /// <summary>
    /// Obtain the class data and fill the appropriate fields.
    /// </summary>
    private void LoadData()
    {
        if (CurrentClass != null)
        {
            txtDisplayName.Text = CurrentClass.ClassDisplayName;

            // Fill class name info
            int classNameIndex = CurrentClass.ClassName.IndexOf('.');
            txtCodeNameNamespace.Text = CurrentClass.ClassName.Substring(0, classNameIndex);
            txtCodeNameName.Text = CurrentClass.ClassName.Substring(classNameIndex + 1);
            lblTableNameValue.Text = CurrentClass.ClassTableName;

            txtNewPage.Text = mCurrentClass.ClassNewPageURL;
            txtViewPage.Text = mCurrentClass.ClassViewPageUrl;
            txtEditingPage.Text = mCurrentClass.ClassEditingPageURL;
            txtListPage.Text = mCurrentClass.ClassListPageURL;
        }
    }


    /// <summary>
    /// Validates entries.
    /// </summary>
    /// <returns>Returns empty string on success and error message otherwise</returns>
    private string ValidateForm()
    {
        string errMsg = String.Empty;

        if (CurrentClass != null)
        {
            // Validate using validators
            errMsg = new Validator().NotEmpty(txtCodeNameName.Text, rfvCodeNameName.ErrorMessage).NotEmpty(txtCodeNameNamespace.Text, rfvCodeNameNamespace.ErrorMessage).
                NotEmpty(txtDisplayName.Text, rfvDisplayName.ErrorMessage).IsIdentificator(txtCodeNameNamespace.Text, GetString("general.invalidcodename")).
                IsCodeName(txtCodeNameName.Text, GetString("general.invalidcodename")).Result;

            string classFullName = txtCodeNameNamespace.Text + "." + txtCodeNameName.Text;

            // Check if class with specified code name already exist
            DataClassInfo existingDataClass = DataClassInfoProvider.GetDataClass(classFullName);
            if (existingDataClass != null)
            {
                if (CurrentClass.ClassID != existingDataClass.ClassID)
                {
                    errMsg = ResHelper.GetString("sysdev.class_edit_gen.codenameunique");
                }
            }
        }

        return errMsg;
    }

    #endregion
}
