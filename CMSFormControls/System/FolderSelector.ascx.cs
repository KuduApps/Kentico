using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.ExtendedControls;
using CMS.FormControls;
using CMS.GlobalHelper;

public partial class CMSFormControls_System_FolderSelector : FormEngineUserControl
{
    #region "Variables"

    private string mSelectedPath = "";
    private string mStartingPath = "";
    private string mDefaultPath = "";
    private string mExcludedFolders = "";
    private string mAllowedFolders = "";

    // Dialog dimensions
    private int mDialogWidth = 95;
    private int mDialogHeight = 86;
    private bool mUseRelativeDimensions = true;

    private bool mAllowEmptyValue = true;
    private bool mEnabled = true;

    #endregion


    #region "Public properties"

    /// <summary>
    /// Starting path of file system tree.
    /// </summary>
    public string StartingPath
    {
        get
        {
            return mStartingPath;
        }
        set
        {
            mStartingPath = value;
        }
    }


    /// <summary>
    /// Value of selected folder.
    /// </summary>
    public string SelectedPath
    {
        get
        {
            return mSelectedPath;
        }
        set
        {
            mSelectedPath = value;
        }
    }


    /// <summary>
    /// Path in filesystem tree selected by default.
    /// </summary>
    public string DefaultPath
    {
        get
        {
            return mDefaultPath;
        }
        set
        {
            mDefaultPath = value;
        }
    }


    /// <summary>
    /// String containing list of allowed folders.
    /// </summary>
    public string AllowedFolders
    {
        get
        {
            return mAllowedFolders;
        }
        set
        {
            mAllowedFolders = value;
        }
    }


    /// <summary>
    /// String containing list of excluded folders.
    /// </summary>
    public string ExcludedFolders
    {
        get
        {
            return mExcludedFolders;
        }
        set
        {
            mExcludedFolders = value;
        }
    }


    /// <summary>
    /// Width of the dialog.
    /// </summary>
    public int DialogWidth
    {
        get
        {
            return mDialogWidth;
        }
        set
        {
            mDialogWidth = value;
        }
    }


    /// <summary>
    /// Height of the dialog.
    /// </summary>
    public int DialogHeight
    {
        get
        {
            return mDialogHeight;
        }
        set
        {
            mDialogHeight = value;
        }
    }


    /// <summary>
    /// Indicates if dialog width/height are set as relative to the total width/height of the screen.
    /// </summary>
    public bool UseRelativeDimensions
    {
        get
        {
            return mUseRelativeDimensions;
        }
        set
        {
            mUseRelativeDimensions = value;
        }
    }


    /// <summary>
    /// Path to selected folder.
    /// </summary>
    public override object Value
    {
        get
        {
            return FileSystemSelector.Value;
        }
        set
        {
            FileSystemSelector.Value = value;
        }
    }


    /// <summary>
    /// Gets or sets if value of form control could be empty.
    /// </summary>
    public bool AllowEmptyValue
    {
        get
        {
            return this.mAllowEmptyValue;
        }
        set
        {
            this.mAllowEmptyValue = value;
        }
    }


    /// <summary>
    /// Gets or sets if value can be changed.
    /// </summary>
    public override bool Enabled
    {
        get
        {
            return mEnabled;
        }
        set
        {
            mEnabled = value;
            FileSystemSelector.Enabled = value;
        }
    }

    #endregion


    #region "Control methods"

    /// <summary>
    /// Page load event.
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (StopProcessing)
        {
            FileSystemSelector.StopProcessing = true;
        }
        else
        {
            // Set properties of inner filesystem selector
            FileSystemSelector.DialogConfig.ShowFolders = true;
            FileSystemSelector.DialogConfig.AllowedFolders = AllowedFolders;
            FileSystemSelector.DialogConfig.DefaultPath = DefaultPath;
            FileSystemSelector.DialogConfig.DialogHeight = DialogHeight;
            FileSystemSelector.DialogConfig.DialogWidth = DialogWidth;
            FileSystemSelector.DialogConfig.ExcludedFolders = ExcludedFolders;
            FileSystemSelector.DialogConfig.SelectedPath = SelectedPath;
            FileSystemSelector.DialogConfig.StartingPath = ValidationHelper.GetString(StartingPath, "~/"); ;
            FileSystemSelector.DialogConfig.UseRelativeDimensions = UseRelativeDimensions;
            FileSystemSelector.AllowEmptyValue = AllowEmptyValue;
        }
    }


    /// <summary>
    /// Validates the return value of form control.
    /// </summary>
    public override bool IsValid()
    {
        return FileSystemSelector.IsValid();
    }

    #endregion
}
