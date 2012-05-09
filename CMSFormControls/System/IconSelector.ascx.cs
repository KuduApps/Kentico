using System;
using System.Web.UI;
using System.Collections;

using CMS.FormControls;
using CMS.ExtendedControls;
using CMS.GlobalHelper;
using CMS.IO;

public partial class CMSFormControls_System_IconSelector : FormEngineUserControl
{
    #region "Constants"

    /// <summary>
    /// Value for 'Do not display any icon'.
    /// </summary>
    private const string NOT_DISPLAY_ICON = "##NONE##";

    #endregion


    #region "Private variables"

    private string mIconsFolder = "~/App_Themes/Default/Images/Design/Controls/IconSelector/RSS";
    private string mAllowedIconExtensions = "png";

    // Default value for RSS
    private string mFolderPreviewImageName = "24.png";
    private string mFullIconFolderPath = String.Empty;
    private string mCurrentIconFolder = String.Empty;
    private string mMainPanelResourceName = "general.color";
    private string mChildPanelResourceName = "general.size";

    #endregion


    #region "Properties"

    /// <summary>
    /// Returns full path to filesystem.
    /// </summary>
    private string FullIconFolderPath
    {
        get
        {
            if (this.mFullIconFolderPath == String.Empty)
            {
                this.mFullIconFolderPath = this.IconsFolder;
                if (this.mFullIconFolderPath.StartsWith("~"))
                {
                    this.mFullIconFolderPath = Server.MapPath(this.mFullIconFolderPath);
                }

            }
            return this.mFullIconFolderPath;
        }
    }


    /// <summary>
    /// Gets current action name.
    /// </summary>
    private string CurrentAction
    {
        get
        {
            return this.hdnAction.Value.ToLower().Trim();
        }
        set
        {
            this.hdnAction.Value = value;
        }
    }


    /// <summary>
    /// Gets current action argument value.
    /// </summary>
    private string CurrentArgument
    {
        get
        {
            return this.hdnArgument.Value;
        }
    }


    /// <summary>
    /// Gets or set value of selected predefined icon folder.
    /// </summary>
    private string CurrentIconFolder
    {
        get
        {
            return ValidationHelper.GetString(ViewState["CurrentIconFolder"], String.Empty);
        }
        set
        {
            ViewState["CurrentIconFolder"] = value;
        }
    }


    /// <summary>
    /// Gets or sets value of selected predefined icon.
    /// </summary>
    private string CurrentIcon
    {
        get
        {
            return ValidationHelper.GetString(ViewState["CurrentIcon"], String.Empty);
        }
        set
        {
            ViewState["CurrentIcon"] = value;
        }
    }

    #endregion


    #region "Public properties"

    /// <summary>
    /// Gets or sets folder name from which icons will be taken.
    /// </summary>
    public string IconsFolder
    {
        get
        {
            return mIconsFolder;
        }
        set
        {
            mIconsFolder = value;
        }
    }


    /// <summary>
    /// Gets or sets the files pattern for the icons files.
    /// </summary>
    public string AllowedIconExtensions
    {
        get
        {
            return mAllowedIconExtensions;
        }
        set
        {
            mAllowedIconExtensions = value;
        }
    }


    /// <summary>
    /// Gets or sets default image displayed as preview of icon image set.
    /// </summary>
    public string FolderPreviewImageName
    {
        get
        {
            return mFolderPreviewImageName;
        }
        set
        {
            mFolderPreviewImageName = value;
        }
    }


    /// <summary>
    /// Gets or set resource name for main panel.
    /// </summary>
    public string MainPanelResourceName
    {
        get
        {
            return mMainPanelResourceName;
        }
        set
        {
            mMainPanelResourceName = value;
        }
    }


    /// <summary>
    /// Gets or set resource name for main panel.
    /// </summary>
    public string ChildPanelResourceName
    {
        get
        {
            return mChildPanelResourceName;
        }
        set
        {
            mChildPanelResourceName = value;
        }
    }


    /// <summary>
    /// Gets or sets field value.
    /// </summary>
    public override object Value
    {
        get
        {
            // Predefined icon
            if (this.UsingPredefinedIcon)
            {
                return this.IconsFolder + "/" + this.CurrentIconFolder + "/" + this.CurrentIcon;
            }
            // Custom icon
            else if (this.radCustomIcon.Checked)
            {
                string url = this.mediaSelector.Value;
                if (url.StartsWith("/"))
                {
                    return "~" + URLHelper.RemoveApplicationPath(url);
                }

                // Return value for 'Do not display icon'
                if (string.IsNullOrEmpty(url))
                {
                    return NOT_DISPLAY_ICON;
                }

                return url;
            }

            // None icon
            return NOT_DISPLAY_ICON;
        }
        set
        {
            // Initialize only for regular postback
            if (!RequestHelper.IsAsyncPostback())
            {
                string stringValue = ValidationHelper.GetString(value, String.Empty);

                // Check for String.Empty is because of backward compatibility(String.Empty was previous value for not displaying any icon)
                if ((stringValue != String.Empty) && (stringValue != NOT_DISPLAY_ICON) && (value != null))
                {
                    string virtualPath = URLHelper.GetVirtualPath(stringValue);

                    // Check if same with starting path for local icon set
                    if (virtualPath.StartsWith(this.IconsFolder))
                    {
                        string[] parts = virtualPath.Replace(this.IconsFolder, String.Empty).TrimStart('/').Split('/');
                        if (parts.Length == 2)
                        {
                            try
                            {
                                FileInfo fi = FileInfo.New(Server.MapPath(virtualPath));
                                if (fi.Exists)
                                {
                                    this.CurrentIconFolder = parts[0];
                                    this.CurrentIcon = parts[1];
                                }
                            }
                            catch (Exception ex)
                            {
                                this.lblError.Text += "[IconSelector.SetValue]: Error accessing selected icon. Original exception: " + ex.Message;
                            }
                        }

                        // Ensure controls are available
                        if (this.radPredefinedIcon == null)
                        {
                            this.pnlUpdateContent.LoadContainer();
                            this.pnlUpdate.LoadContainer();
                        }
                        this.radPredefinedIcon.Checked = true;
                    }
                    else
                    {
                        // Ensure controls are available
                        if (this.radCustomIcon == null)
                        {
                            this.pnlUpdateContent.LoadContainer();
                            this.pnlUpdate.LoadContainer();
                        }
                        this.radCustomIcon.Checked = true;
                        this.mediaSelector.Value = stringValue;
                    }
                }
                // First load when webpart is added to design tab
                else if (value == null)
                {
                    // Ensure controls are available
                    if (this.radPredefinedIcon == null)
                    {
                        this.pnlUpdateContent.LoadContainer();
                        this.pnlUpdate.LoadContainer();
                    }
                    this.radPredefinedIcon.Checked = true;
                }
                // Empty value or 'None icon'
                else if ((stringValue == String.Empty) || (stringValue == NOT_DISPLAY_ICON))
                {
                    // Ensure controls are available
                    if (this.radDoNotDisplay == null)
                    {
                        this.pnlUpdateContent.LoadContainer();
                        this.pnlUpdate.LoadContainer();
                    }
                    this.radDoNotDisplay.Checked = true;
                }
            }
        }
    }


    /// <summary>
    /// Indicates if predefined icons are used instead of custom icon.
    /// </summary>
    public bool UsingPredefinedIcon
    {
        get
        {
            return this.radPredefinedIcon.Checked;
        }
    }

    #endregion


    #region "Page events"

    /// <summary>
    /// Page load event.
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.StopProcessing)
        {
            InitializeControlScripts();
            SetupControls();
        }
    }


    /// <summary>
    /// PreRender event handler.
    /// </summary>
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        if (!String.IsNullOrEmpty(this.lblError.Text))
        {
            this.lblError.Visible = true;
            this.pnlUpdate.Visible = false;

            this.pnlUpdateContent.Update();
        }
    }

    #endregion


    #region "Control methods"

    /// <summary>
    /// Setup all contained controls.
    /// </summary>
    private void SetupControls()
    {
        // Reset error label
        this.lblError.Text = String.Empty;
        this.lblError.Visible = false;

        // Setup main radio button controls
        this.radCustomIcon.Text = GetString("iconselector.custom");
        this.radCustomIcon.Attributes.Add("onclick", "SetAction_" + this.ClientID + "('switch','');RaiseHiddenPostBack_" + this.ClientID + "();");
        this.radPredefinedIcon.Text = GetString("iconselector.predefined");
        this.radPredefinedIcon.Attributes.Add("onclick", "SetAction_" + this.ClientID + "('switch','');RaiseHiddenPostBack_" + this.ClientID + "();");
        this.radDoNotDisplay.Text = GetString("iconselector.donotdisplay");
        this.radDoNotDisplay.Attributes.Add("onclick", "SetAction_" + this.ClientID + "('switch','');RaiseHiddenPostBack_" + this.ClientID + "();");

        // Setup panels
        this.pnlMain.GroupingText = GetString(this.MainPanelResourceName);
        this.pnlChild.GroupingText = GetString(this.ChildPanelResourceName);

        // Configuration of media dialog
        DialogConfiguration config = new DialogConfiguration();
        config.SelectableContent = SelectableContentEnum.OnlyImages;
        config.OutputFormat = OutputFormatEnum.URL;
        config.HideWeb = false;
        config.ContentSites = AvailableSitesEnum.All;
        config.DialogWidth = 90;
        config.DialogHeight = 80;
        config.UseRelativeDimensions = true;
        config.LibSites = AvailableSitesEnum.All;


        this.mediaSelector.UseCustomDialogConfig = true;
        this.mediaSelector.DialogConfig = config;
        this.mediaSelector.ShowPreview = false;
        this.mediaSelector.IsLiveSite = this.IsLiveSite;

        if (!RequestHelper.IsAsyncPostback())
        {
            // Load initial data and ensure something is selected
            if ((!this.radCustomIcon.Checked) && (!this.radDoNotDisplay.Checked) && (!this.radPredefinedIcon.Checked))
            {
                this.radPredefinedIcon.Checked = true;
            }
            HandleSwitchAction();
        }
    }


    /// <summary>
    /// Initializes all the script required for communication between controls.
    /// </summary>
    private void InitializeControlScripts()
    {
        // SetAction function setting action name and passed argument
        string setAction = "function SetAction_" + this.ClientID + "(action, argument) {                                              " +
                           "    var hdnAction = document.getElementById('" + this.hdnAction.ClientID + "');     " +
                           "    var hdnArgument = document.getElementById('" + this.hdnArgument.ClientID + "'); " +
                           @"    if ((hdnAction != null) && (hdnArgument != null)) {                             
                                   if (action != null) {                                                       
                                       hdnAction.value = action;                                               
                                   }                                                                           
                                   if (argument != null) {                                                     
                                       hdnArgument.value = argument;                                           
                                   }                                                                           
                               }                                                                               
                           }                                                                                    ";

        // Get reffernce causing postback to hidden button
        string postBackRef = ControlsHelper.GetPostBackEventReference(this.hdnButton, String.Empty);
        string raiseOnAction = " function RaiseHiddenPostBack_" + this.ClientID + "(){" + postBackRef + ";}\n";

        this.ltlScript.Text = ScriptHelper.GetScript(setAction + raiseOnAction);
    }

    #endregion


    #region "Helper methods"

    /// <summary>
    /// Check if file extension is allowed.
    /// </summary>
    /// <param name="info">FileInfo to check</param>
    /// <returns>True if extension isallowed otherwise false</returns>
    private bool IsAllowedFileExtension(FileInfo info)
    {
        string allowedExt = ";" + this.AllowedIconExtensions.Trim(';').ToLower() + ";";
        try
        {
            return allowedExt.Contains(";" + info.Extension.ToLower().TrimStart('.') + ";");
        }
        catch
        {
            return false;
        }
    }


    /// <summary>
    /// Gets Arraylist filled with folder icons data.
    /// </summary>
    /// <returns>ArrayList with data for icon sets</returns>
    private ArrayList GetPredefinedIconFoldersSet()
    {
        ArrayList directories = new ArrayList();

        try
        {
            if (!String.IsNullOrEmpty(this.FullIconFolderPath))
            {
                DirectoryInfo di = DirectoryInfo.New(this.FullIconFolderPath);

                foreach (DirectoryInfo dir in di.GetDirectories())
                {
                    bool containIcons = false;
                    string previewIconName = this.FolderPreviewImageName.ToLower();
                    string firstIconName = String.Empty;

                    // Get files array and filter it
                    FileInfo[] files = dir.GetFiles();
                    files = Array.FindAll(files, IsAllowedFileExtension);

                    foreach (FileInfo fi in files)
                    {
                        firstIconName = String.Empty;

                        // Store first icon name to be used if default preview icon is missing
                        if (firstIconName == String.Empty)
                        {
                            firstIconName = fi.Name;
                            containIcons = true;
                        }

                        // Check for default icon
                        if (fi.Name.ToLower() == previewIconName)
                        {
                            firstIconName = fi.Name;
                            break;
                        }
                    }

                    if (containIcons)
                    {
                        directories.Add(new string[] { dir.Name, firstIconName });
                    }
                }
            }
        }
        catch (Exception ex)
        {
            this.lblError.Text += "[IconSelector.GetPredefinedIconFoldersSet]: Error loading predefined icon set. Original exception: " + ex.Message;
        }

        return directories;
    }


    /// <summary>
    /// Render Folder icon preview HTML.
    /// </summary>
    /// <param name="disabled">Determine if input fields should be rendered as disabled</param>
    private void GetFolderIconPreview(bool disabled)
    {
        GetFolderIconPreview(disabled, this.CurrentIconFolder);
    }


    /// <summary>
    /// Render Folder icon preview HTML.
    /// </summary>
    /// <param name="disabled">Determine if input fields should be rendered as disabled</param>
    /// <param name="defaultValue">Determine default value which should be checked</param>
    private void GetFolderIconPreview(bool disabled, string defaultValue)
    {
        this.ltlFolders.Text = "<table><tr class=\"Row\">";
        int counter = 0;
        string defaultFolder = defaultValue;
        foreach (string[] folderInfo in GetPredefinedIconFoldersSet())
        {
            string dirName = folderInfo[0];
            string iconName = folderInfo[1];

            // For each folder td element with icon preview is generated 
            this.ltlFolders.Text += "<td class=\"Cell\"><img src=\"" + ResolveUrl(this.IconsFolder) +
                                    "/" + dirName + "/" + iconName +
                                    "\" alt=\"" + iconName + "\" /><br />" +
                                    "<input type=\"radio\" id=\"" + this.ClientID + "_" + "folder" +
                                    counter + "\" name=\"" + this.ClientID + "_" + "folders\" value=\"" +
                                    dirName + "\" ";

            // Check for selected value
            if ((defaultValue == String.Empty) || (dirName.ToLower() == defaultValue.ToLower()))
            {
                this.ltlFolders.Text += "checked=\"checked\" ";
                defaultValue = dirName.ToString();
            }

            // Check if disabled
            if (disabled)
            {
                this.ltlFolders.Text += "disabled=\"disabled\" ";
            }
            else
            {
                this.ltlFolders.Text += "onclick=\"SetAction_" + this.ClientID + "('changefolder','" +
                                        dirName.ToString() + "');RaiseHiddenPostBack_" + this.ClientID + "();\" ";
            }
            this.ltlFolders.Text += "/></td>";
            counter++;

        }
        this.ltlFolders.Text += "</tr></table>";

        // Aktualize value of current icon folder
        this.CurrentIconFolder = defaultValue;
    }


    /// <summary>
    /// Gets Array List with icons located in specified directory.
    /// </summary>
    /// <param name="di">DirectoryInfo of particular directory</param>
    /// <returns>ArrayList with contained icons</returns>
    private ArrayList GetIconsInFolder(DirectoryInfo di)
    {
        ArrayList icons = new ArrayList();
        try
        {
            // Get files array and filter it
            FileInfo[] files = di.GetFiles();
            files = Array.FindAll(files, IsAllowedFileExtension);
            foreach (FileInfo fi in files)
            {
                icons.Add(fi.Name);
            }
        }
        catch (Exception ex)
        {
            this.lblError.Text = "[IconSelector.GetIconsInFolder]: Error getting icons in source icon folder. Original exception: " + ex.Message;
        }
        return icons;
    }

    #endregion


    #region "Handler methods"

    /// <summary>
    /// Generate apropriate icons if source folder is changed.
    /// </summary>
    /// <param name="folderName">Name of selected folder</param>
    private void HandleChangeFolderAction(string folderName)
    {
        HandleChangeFolderAction(folderName, false, this.CurrentIcon);
    }


    /// <summary>
    /// Generate apropriate icons if source folder is changed.
    /// </summary>
    /// <param name="folderName">Name of selected folder</param>
    /// <param name="disabled">Determine if input fields should be rendered as disabled</param>
    /// <param name="defaultValue">Determine default value which should be checked</param>
    private void HandleChangeFolderAction(string folderName, bool disabled, string defaultValue)
    {
        try
        {
            DirectoryInfo di = DirectoryInfo.New(DirectoryHelper.CombinePath(this.FullIconFolderPath, folderName));
            string directoryName = di.Name;
            int counter = 0;
            ArrayList iconList = GetIconsInFolder(di);
            string defaultIcon = (iconList.Contains(defaultValue)) ? defaultValue : String.Empty;
            string labels = "<tr class=\"Row\">";
            string icons = "<tr class=\"Row\">";
            this.ltlIcons.Text = "<table><tr class=\"Row\">";
            foreach (string fileInfo in iconList)
            {
                // For each icon td element is generated
                labels += "<td class=\"Cell\">" + GetString("iconcaption." +
                          fileInfo.Remove(fileInfo.LastIndexOf('.'))) + "</td>";

                icons += "<td class=\"Cell\"><div class=\"Icon\"><img src=\"" + ResolveUrl(this.IconsFolder) +
                         "/" + directoryName + "/" + fileInfo +
                         "\" alt=\"" + fileInfo + "\" /><br />" +
                         "<input type=\"radio\" id=\"" + this.ClientID + "_" + "icon" +
                         counter + "\" name=\"" + this.ClientID + "_" + "icons\" value=\"" +
                         directoryName + "\" ";

                // Check for selected value
                if ((defaultIcon == String.Empty) || (fileInfo.ToLower() == defaultValue.ToLower()))
                {
                    icons += "checked=\"checked\" ";
                    defaultIcon = fileInfo;
                }

                // Check if disabled
                if (disabled)
                {
                    icons += "disabled=\"disabled\"";
                }
                else
                {
                    icons += "onclick=\"SetAction_" + this.ClientID + "('select','" + fileInfo + "');RaiseHiddenPostBack_" + this.ClientID + "();\"";
                }

                icons += " /></div></td>";
                counter++;
            }
            this.ltlIcons.Text += labels + "</tr>" + icons + "</tr></table>";
            this.CurrentIcon = defaultIcon;
        }
        catch (Exception ex)
        {
            this.lblError.Text += "[IconSelector.HandleChangeFolderAction]: Error getting icons in selected icon folder. Original exception: " + ex.Message;
        }
        this.CurrentIconFolder = folderName;
        this.pnlUpdateIcons.Update();
    }


    /// <summary>
    /// Handle situation when type of choosing is changed.
    /// </summary>
    private void HandleSwitchAction()
    {
        if (this.radCustomIcon.Checked)
        {
            GetFolderIconPreview(true, this.CurrentIconFolder);
            HandleChangeFolderAction(this.CurrentIconFolder, true, this.CurrentIcon);
            this.mediaSelector.Enabled = true;
            this.pnlUpdate.Update();
        }
        else if (this.radPredefinedIcon.Checked)
        {
            GetFolderIconPreview(false, this.CurrentIconFolder);
            HandleChangeFolderAction(this.CurrentIconFolder, false, this.CurrentIcon);
            this.mediaSelector.Enabled = false;
            this.pnlUpdate.Update();
        }
        else
        {
            GetFolderIconPreview(true, this.CurrentIconFolder);
            HandleChangeFolderAction(this.CurrentIconFolder, true, this.CurrentIcon);
            this.mediaSelector.Enabled = false;
            this.pnlUpdate.Update();
        }
    }


    /// <summary>
    /// Behaves as mediator in communication line between control taking action and the rest of the same level controls.
    /// </summary>
    protected void hdnButton_Click(object sender, EventArgs e)
    {
        switch (this.CurrentAction)
        {
            // Switch from predefined icon to custom
            case "switch":
                HandleSwitchAction();
                break;

            // Change predefined icon folder
            case "changefolder":
                //this.CurrentIcon = String.Empty;
                HandleChangeFolderAction(this.CurrentArgument);
                break;

            // Select icon
            case "select":
                this.CurrentIcon = this.CurrentArgument;
                ClearActionElems();
                break;

            // By default do nothing
            default:
                break;
        }
    }


    /// <summary>
    /// Clears hidden control elements fo future use.
    /// </summary>
    private void ClearActionElems()
    {
        this.CurrentAction = String.Empty;
        this.hdnArgument.Value = String.Empty;
    }

    #endregion
}
