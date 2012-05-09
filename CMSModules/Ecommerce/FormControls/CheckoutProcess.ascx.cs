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

using CMS.GlobalHelper;
using CMS.FormControls;
using CMS.EcommerceProvider;
using CMS.UIControls;

/// <summary>
/// Checkout definition update event handler.
/// </summary>
public delegate void OnCheckoutProcessDefinitionUpdateEventHandler(string action);


public partial class CMSModules_Ecommerce_FormControls_CheckoutProcess : FormEngineUserControl
{
    #region "Variables"

    private string mImageFolder = "";
    private CheckoutProcessInfo mCheckoutProcess = null;

    #endregion


    #region "Events"

    /// <summary>
    /// Occurs when mode (listing/edit) is changed.
    /// </summary>
    /// <param name="isListingMode">Indicates if control is in listing mode</param>
    public delegate void ModeChangedHandler(bool isListingMode);
    public event ModeChangedHandler OnModeChanged;

    #endregion


    #region "Private Properties"

    private CheckoutProcessInfo CheckoutProcess
    {
        get
        {
            if (mCheckoutProcess == null)
            {
                mCheckoutProcess = new CheckoutProcessInfo();
                mCheckoutProcess.LoadXmlDefinition(this.CheckoutProcessXml);
            }
            return mCheckoutProcess;
        }
    }


    /// <summary>
    /// Checkout process XML - shopping cart steps definition in XML format.
    /// </summary>
    private string CheckoutProcessXml
    {
        get
        {
            //object obj = ViewState["CheckoutProcessXml"];
            //if (obj != null)
            //{
            //    return Convert.ToString(obj);
            //}
            //else
            //{
            //    ViewState["CheckoutProcessXml"] = "";
            //    return Convert.ToString(ViewState["CheckoutProcessXml"]);
            //}
            return this.hdnCheckoutProcessXml.Value;
        }
        set
        {
            //ViewState["CheckoutProcessXml"] = value;
            this.hdnCheckoutProcessXml.Value = value;
        }
    }


    /// <summary>
    /// Checkout process type.
    /// </summary>
    private CheckoutProcessEnum CheckoutProcessType
    {
        get
        {
            object obj = ViewState["CheckoutProcessType"];
            if (obj != null)
            {
                return (CheckoutProcessEnum)(obj);
            }
            else
            {
                ViewState["CheckoutProcessType"] = 0;
                return (CheckoutProcessEnum)ViewState["CheckoutProcessType"];
            }
        }
        set
        {
            ViewState["CheckoutProcessType"] = value;
        }
    }


    /// <summary>
    /// Original step name.
    /// </summary>
    private string OriginalStepName
    {
        get
        {
            return Convert.ToString(ViewState["OriginalStepName"]);
        }
        set
        {
            ViewState["OriginalStepName"] = value;
        }
    }


    /// <summary>
    /// Indicates if control is in listing mode or in edit/insert mode.
    /// </summary>
    private bool ListingMode
    {
        get
        {
            return plcList.Visible;
        }
        set
        {
            plcList.Visible = value;
            plcEdit.Visible = !value;

            if (OnModeChanged != null)
            {
                OnModeChanged(value);
            }
        }
    }



    #endregion


    #region "Public Properties"

    /// <summary>
    /// Checkout definition update event handler.
    /// </summary>
    public event OnCheckoutProcessDefinitionUpdateEventHandler OnCheckoutProcessDefinitionUpdate;


    /// <summary>
    /// Image folder.
    /// </summary>
    public string ImageFolder
    {
        get
        {
            return mImageFolder;
        }
        set
        {
            mImageFolder = value;
        }
    }


    /// <summary>
    /// Information.
    /// </summary>
    public string Information
    {
        set
        {
            lblInfo.Visible = true;
            lblInfo.Text = value;
        }
    }


    /// <summary>
    /// Gets or sets field value.
    /// </summary>
    public override object Value
    {
        get
        {
            try
            {
                XmlDocument xml = new XmlDocument();
                xml.LoadXml(this.CheckoutProcessXml);
                if (xml.DocumentElement.SelectNodes("step").Count == 0)
                {
                    return "";
                }
            }
            catch
            {
                return "";
            }

            return this.CheckoutProcessXml;
        }
        set
        {
            this.CheckoutProcessXml = Convert.ToString(value);
        }
    }


    /// <summary>
    /// Indicates whether checkout process types should be visible and editable to user. 
    /// FALSE - Default value. Step is created without relation to any of the default checkout process. 
    /// Use this option when control is used to generate custom shopping cart webpart checkout process. 
    /// TRUE - User can choose from the default checkout process the step will be included in.
    /// </summary>
    public bool EnableDefaultCheckoutProcessTypes
    {
        get
        {
            return ValidationHelper.GetBoolean(ViewState["EnableDefaultCheckoutProcessTypes"], false);
        }
        set
        {
            ViewState["EnableDefaultCheckoutProcessTypes"] = value;
        }
    }


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
        }
    }


    /// <summary>
    /// Gets ClientID of the hidden field which stores the actual value of the control.
    /// </summary>
    public override string ValueElementID
    {
        get
        {
            return this.hdnCheckoutProcessXml.ClientID;
        }
    }


    /// <summary>
    /// Indicates, if actions are visible. Set to false, when handling actions programaticaly.
    /// </summary>
    public bool ShowActions
    {
        get
        {
            return pnlHeaderLine.Visible;
        }
        set
        {
            pnlHeaderLine.Visible = value;
        }
    }

    #endregion


    #region "Template Properties"

    protected string mActions = "";
    protected string mName = "";
    protected string mOrder = "";
    protected string mCaption = "";
    protected string mShowOnLiveSite = "";
    protected string mShowInCMSDeskOrder = "";
    protected string mShowInCMSDeskOrderItems = "";
    protected string mShowInCMSDeskCustomer = "";

    protected string btnEditImageUrl = "";
    protected string btnDeleteImageUrl = "";
    protected string btnMoveUpImageUrl = "";
    protected string btnMoveDownImageUrl = "";

    protected string btnEditToolTip = "";
    protected string btnDeleteToolTip = "";
    protected string btnMoveUpToolTip = "";
    protected string btnMoveDownToolTip = "";

    #endregion


    #region "Page Events"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Register javascript to confirm delete action
        string script = "function ConfirmDelete() {return confirm(" + ScriptHelper.GetString(GetString("CheckoutProcess.ConfirmDeleteStep")) + ");}";
        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "ConfirmDeleteStep", ScriptHelper.GetScript(script));

        if (this.EnableDefaultCheckoutProcessTypes)
        {
            btnDefaultProcess.Visible = true;
            btnDefaultProcess.Text = GetString("CheckoutProcess.DefaultProcess");

            // Register javascript to confirm generate default checkout process
            script = "function ConfirmDefaultProcess() {return confirm(" + ScriptHelper.GetString(GetString("CheckoutProcess.ConfirmDefaultProcess")) + ");}";
            ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "ConfirmDefaultProcess", ScriptHelper.GetScript(script));
        }

        // Initialize button images
        if (this.ImageFolder == "")
        {
            this.ImageFolder = GetImageUrl("Design/Controls/UniGrid/Actions/", IsLiveSite, true);
        }

        if (CultureHelper.IsUICultureRTL())
        {
            gridSteps.RowStyle.HorizontalAlign = HorizontalAlign.Right;
        }
        else
        {
            gridSteps.RowStyle.HorizontalAlign = HorizontalAlign.Left;
        }

        btnEditImageUrl = this.ImageFolder.TrimEnd('/') + "/Edit.png";
        btnDeleteImageUrl = this.ImageFolder.TrimEnd('/') + "/Delete.png";
        btnMoveUpImageUrl = this.ImageFolder.TrimEnd('/') + "/Up.png";
        btnMoveDownImageUrl = this.ImageFolder.TrimEnd('/') + "/Down.png";

        // Initialize button tooltips
        btnEditToolTip = GetString("general.edit");
        btnDeleteToolTip = GetString("general.delete");
        btnMoveUpToolTip = GetString("CheckoutProcess.btnMoveUpToolTip");
        btnMoveDownToolTip = GetString("CheckoutProcess.btnMoveDownToolTip");

        // Initialize grid labels
        mActions = GetString("unigrid.actions");
        mName = GetString("general.name");
        mOrder = GetString("CheckoutProcess.Order");
        mCaption = GetString("CheckoutProcess.Caption");
        mShowOnLiveSite = GetString("CheckoutProcess.ShowOnLiveSite");
        mShowInCMSDeskOrder = GetString("CheckoutProcess.ShowInCMSDeskOrder");
        mShowInCMSDeskOrderItems = GetString("CheckoutProcess.ShowInCMSDeskOrderItems");
        mShowInCMSDeskCustomer = GetString("CheckoutProcess.ShowInCMSDeskCustomer");

        // Initialize validators
        rfvStepCaption.ErrorMessage = GetString("CheckoutProcess.ErrorStepCaptionEmpty");
        rfvStepControlPath.ErrorMessage = GetString("CheckoutProcess.ErrorStepControlPathEmpty");
        rfvStepName.ErrorMessage = GetString("CheckoutProcess.ErrorStepNameEmpty");

        // Initialize other controls
        lnkNewStep.Text = GetString("CheckoutProcess.lnkNewStep");
        imgNewItem.ImageUrl = GetImageUrl("CMSModules/CMS_Ecommerce/addstep.png");
        imgNewItem.AlternateText = GetString("general.new");
        lnkList.Text = GetString("CheckoutProcess.lnkList");
        lblStepCaption.Text = GetString("CheckoutProcess.lblStepCaption");
        lblStepControlPath.Text = GetString("CheckoutProcess.lblStepControlPath");
        lblStepImageUrl.Text = GetString("CheckoutProcess.lblStepImageUrl");
        lblStepName.Text = GetString("general.codename") + ResHelper.Colon;
        lblLiveSite.Text = GetString("CheckoutProcess.lblLiveSite");
        lblCMSDeskCustomer.Text = GetString("CheckoutProcess.lblCMSDeskCustomer");
        lblCMSDeskOrder.Text = GetString("CheckoutProcess.lblCMSDeskOrder");
        lblCMSDeskOrderItems.Text = GetString("CheckoutProcess.lblCMSDeskOrderItems");
        btnOk.Text = GetString("General.Ok");

        gridSteps.RowDataBound += new GridViewRowEventHandler(gridSteps_RowDataBound);

        // Hide default checkout process types
        if (!EnableDefaultCheckoutProcessTypes)
        {
            // Step list
            gridSteps.Columns[3].Visible = false;
            gridSteps.Columns[4].Visible = false;
            gridSteps.Columns[5].Visible = false;
            gridSteps.Columns[6].Visible = false;
        }

        if (!RequestHelper.IsPostBack())
        {
            ListingMode = true;
            ReloadData();
        }

        // Enabling and disabling controls
        this.lnkNewStep.Enabled = this.Enabled;
        if (!pnlHeaderLine.Visible)
        {
            btnDefaultProcess.Visible = false;
        }
    }

    #endregion


    #region "Event Handlers"

    void gridSteps_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Text = Convert.ToString(e.Row.RowIndex + 1);
            
            // Apply even/odd row CSS class
            e.Row.CssClass = (e.Row.RowIndex % 2 > 0) ? "OddRow" : "EvenRow";
        }
    }


    /// <summary>
    /// LnkNewStep click event handler.
    /// </summary>
    protected void lnkNewStep_Click(object sender, EventArgs e)
    {
        NewStep();
    }


    /// <summary>
    /// LnkList click event handler.
    /// </summary>
    protected void lnkList_Click(object sender, EventArgs e)
    {
        ListingMode = true;
    }


    /// <summary>
    /// BtnDefaultProcess click event handler.
    /// </summary>
    protected void btnDefaultProcess_Click(object sender, EventArgs e)
    {
        GenerateDefaultProcess();
    }


    /// <summary>
    /// BtnOk click event handler.
    /// </summary>
    protected void btnOk_Click(object sender, EventArgs e)
    {
        lblCurrentStep.Text = GetString("CheckoutProcess.NewStep");

        string errorMessage = ValidateForm();

        if (errorMessage == "")
        {
            CheckoutProcessStepInfo stepObj = this.CheckoutProcess.GetCheckoutProcessStepInfo(txtStepName.Text.Trim());
            if ((stepObj == null) || (stepObj.Name.ToLower() == this.OriginalStepName.ToLower()))
            {
                if (stepObj == null)
                {
                    stepObj = new CheckoutProcessStepInfo();
                }

                // Get step data from form
                stepObj.Caption = txtStepCaption.Text.Trim();
                stepObj.Name = txtStepName.Text.Trim();
                stepObj.ControlPath = txtStepControlPath.Text.Trim();
                stepObj.Icon = txtStepImageUrl.Text.Trim();
                stepObj.ShowInCMSDeskCustomer = chkCMSDeskCustomer.Checked;
                stepObj.ShowInCMSDeskOrder = chkCMSDeskOrder.Checked;
                stepObj.ShowOnLiveSite = chkLiveSite.Checked;
                stepObj.ShowInCMSDeskOrderItems = chkCMSDeskOrderItems.Checked;

                if ((this.OriginalStepName != "") && (this.OriginalStepName.ToLower() != txtStepName.Text.ToLower()))
                {
                    // Replace node
                    this.CheckoutProcess.ReplaceCheckoutProcessStepNode(stepObj, this.OriginalStepName);
                }
                else
                {
                    // Update or insert node
                    this.CheckoutProcess.SetCheckoutProcessStepNode(stepObj);
                }

                // Update Xml definition in viewstate
                this.CheckoutProcessXml = this.CheckoutProcess.GetXmlDefinition();

                if (OnCheckoutProcessDefinitionUpdate != null)
                {
                    OnCheckoutProcessDefinitionUpdate("update");
                }

                //lblInfo.Visible = true;
                //lblInfo.Text = GetString("General.ChangesSaved");

                lblCurrentStep.Text = HTMLHelper.HTMLEncode(ResHelper.LocalizeString(stepObj.Caption));

                ListingMode = true;
                ReloadData();
            }
            else
            {
                errorMessage = GetString("CheckoutProcess.ErrorStepNameNotUnique");
            }
        }

        // Show error message
        if (errorMessage != "")
        {
            lblErrorEdit.Visible = true;
            lblErrorEdit.Text = errorMessage;

            // If error during editing, set original caption to breadcrumbs
            if (!string.IsNullOrEmpty(this.OriginalStepName))
            {
                CheckoutProcessStepInfo stepObj = this.CheckoutProcess.GetCheckoutProcessStepInfo(this.OriginalStepName);
                if (stepObj != null)
                {
                    lblCurrentStep.Text = HTMLHelper.HTMLEncode(ResHelper.LocalizeString(stepObj.Caption));
                }
            }
        }
    }


    /// <summary>
    /// Validates form input data and returns error message if some error occures.
    /// </summary>
    private string ValidateForm()
    {
        return new Validator().NotEmpty(txtStepCaption.Text.Trim(), rfvStepCaption.ErrorMessage).
            NotEmpty(txtStepName.Text.Trim(), rfvStepName.ErrorMessage).
            NotEmpty(txtStepControlPath.Text.Trim(), rfvStepControlPath.ErrorMessage).
            IsCodeName(txtStepName.Text.Trim(), GetString("General.ErrorCodeNameInIdentificatorFormat")).Result;
    }


    /// <summary>
    /// Reloads data in gridview.
    /// </summary>
    public void ReloadData()
    {
        // Load xml definition from viewstate
        this.CheckoutProcess.LoadXmlDefinition(this.CheckoutProcessXml);

        gridSteps.DataSource = this.CheckoutProcess.GetDataTableFromXmlDefinition(this.CheckoutProcessType);
        gridSteps.DataBind();
    }


    /// <summary>
    /// BtnEdit click event handler.
    /// </summary>
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        ListingMode = false;
        plcDefaultTypes.Visible = this.EnableDefaultCheckoutProcessTypes;

        // Load step data to the form
        CheckoutProcessStepInfo stepObj = this.CheckoutProcess.GetCheckoutProcessStepInfo(((ImageButton)(sender)).CommandArgument);
        if (stepObj != null)
        {
            lblCurrentStep.Text = HTMLHelper.HTMLEncode(ResHelper.LocalizeString(stepObj.Caption));

            txtStepCaption.Text = stepObj.Caption;
            txtStepControlPath.Text = stepObj.ControlPath;
            txtStepImageUrl.Text = stepObj.Icon;
            txtStepName.Text = stepObj.Name;
            chkLiveSite.Checked = stepObj.ShowOnLiveSite;
            chkCMSDeskOrder.Checked = stepObj.ShowInCMSDeskOrder;
            chkCMSDeskCustomer.Checked = stepObj.ShowInCMSDeskCustomer;
            chkCMSDeskOrderItems.Checked = stepObj.ShowInCMSDeskOrderItems;

            // Save original step name
            this.OriginalStepName = stepObj.Name;
        }
    }


    /// <summary>
    /// BtnDelete click event handler.
    /// </summary>
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        // Remove node from xml
        this.CheckoutProcess.RemoveCheckoutProcessStepNode(((ImageButton)sender).CommandArgument);
        // Update xml definition in viewstate
        this.CheckoutProcessXml = this.CheckoutProcess.GetXmlDefinition();

        if (OnCheckoutProcessDefinitionUpdate != null)
        {
            OnCheckoutProcessDefinitionUpdate("delete");
        }

        ReloadData();
    }


    /// <summary>
    /// BtnMoveUp click event handler.
    /// </summary>
    protected void btnMoveUp_Click(object sender, EventArgs e)
    {
        // Move node up in xml
        this.CheckoutProcess.MoveCheckoutProcessStepNodeUp(((ImageButton)sender).CommandArgument);
        // Update xml definition in viewstate
        this.CheckoutProcessXml = this.CheckoutProcess.GetXmlDefinition();

        if (OnCheckoutProcessDefinitionUpdate != null)
        {
            OnCheckoutProcessDefinitionUpdate("moveup");
        }

        ReloadData();
    }


    /// <summary>
    /// BtnMoveDown click event handler.
    /// </summary>
    protected void btnMoveDown_Click(object sender, EventArgs e)
    {
        // Move node down in xml definition
        this.CheckoutProcess.MoveCheckoutProcessStepNodeDown(((ImageButton)sender).CommandArgument);
        // Update xml definition in viewstate
        this.CheckoutProcessXml = this.CheckoutProcess.GetXmlDefinition();

        if (OnCheckoutProcessDefinitionUpdate != null)
        {
            OnCheckoutProcessDefinitionUpdate("movedown");
        }

        ReloadData();
    }


    /// <summary>
    /// Check validity of the control.
    /// </summary>
    public override bool IsValid()
    {
        if ((string)this.Value == "")
        {
            this.ValidationError = GetString("CheckoutProcess.ErrorProcessEmpty");
            return false;
        }
        else
        {
            this.ValidationError = "";
            return true;
        }
    }


    /// <summary>
    /// Returns colored boolean value string (TRUE -> green color, FALSE - red color)
    /// </summary>
    /// <param name="val">Boolean value string</param>
    protected string GetColoredBooleanString(object val)
    {
        return UniGridFunctions.ColoredSpanYesNo(val);
    }


    /// <summary>
    /// Opens new step form.
    /// </summary>
    public void NewStep()
    {
        ListingMode = false;
        plcDefaultTypes.Visible = this.EnableDefaultCheckoutProcessTypes;
        lblCurrentStep.Text = GetString("CheckoutProcess.NewStep");

        // Set default values
        txtStepCaption.Text = "";
        txtStepControlPath.Text = "";
        txtStepImageUrl.Text = "";
        txtStepName.Text = "";
        chkCMSDeskCustomer.Checked = false;
        chkCMSDeskOrder.Checked = false;
        chkLiveSite.Checked = false;
        chkCMSDeskOrderItems.Checked = false;

        // Clear original step name
        OriginalStepName = "";
    }


    /// <summary>
    /// Generates default process.
    /// </summary>
    public void GenerateDefaultProcess()
    {
        OnCheckoutProcessDefinitionUpdate("defaultprocess");
    }


    /// <summary>
    /// Generates process from global.
    /// </summary>
    public void GenerateFromGlobalProcess()
    {
        OnCheckoutProcessDefinitionUpdate("fromglobalprocess");
    }

    #endregion
}
