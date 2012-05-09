using System;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.Ecommerce;
using CMS.EcommerceProvider;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.SiteProvider;
using CMS.ExtendedControls;

public partial class CMSModules_Ecommerce_Controls_ShoppingCart_ShoppingCart : ShoppingCart
{
    private bool mDisplayStepImages = false;
    private bool mDisplayStepIndexes = true;


    /// <summary>
    /// Indicates whether step images should be displayed.
    /// </summary>
    public bool DisplayStepImages
    {
        get
        {
            return mDisplayStepImages;
        }
        set
        {
            mDisplayStepImages = value;
        }
    }


    /// <summary>
    /// Indicates whether step indexes should be displayed.
    /// </summary>
    public bool DisplayStepIndexes
    {
        get
        {
            return mDisplayStepIndexes;
        }
        set
        {
            mDisplayStepIndexes = value;
        }
    }


    /// <summary>
    /// Back button.
    /// </summary>
    public override CMSButton ButtonBack
    {
        get
        {
            return this.btnBack;
        }
        set
        {
            this.btnBack = value;
        }
    }


    /// <summary>
    /// Next button.
    /// </summary>
    public override CMSButton ButtonNext
    {
        get
        {
            return this.btnNext;
        }
        set
        {
            this.btnNext = value;
        }
    }


    /// <summary>
    /// Page load.
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        // If shopping cart is created -> create empty one
        if ((this.ShoppingCartInfoObj == null) && (CMSContext.CurrentSite != null))
        {
            this.ShoppingCartInfoObj = ShoppingCartInfoProvider.CreateShoppingCartInfo(CMSContext.CurrentSiteID);

            // Set customer preffered options   
            CustomerInfo currentCustomer = ECommerceContext.CurrentCustomer;
            if ((currentCustomer != null) && (currentCustomer.CustomerUser != null))
            {
                this.ShoppingCartInfoObj.ShoppingCartCurrencyID = currentCustomer.CustomerUser.GetUserPreferredCurrencyID(CMSContext.CurrentSiteName);
            }
        }

        if (this.CurrentStepIndex == 0)
        {
            this.ShoppingCartInfoObj.PrivateDataCleared = false;
        }

        // Display / hide checkout process images
        this.plcCheckoutProcess.Visible = this.DisplayStepImages;

        // Load current step data
        LoadCurrentStep();

        // If shopping cart information exist
        if (this.ShoppingCartInfoObj != null)
        {
            // Get order information
            OrderInfo oi = OrderInfoProvider.GetOrderInfo(this.ShoppingCartInfoObj.OrderId);

            // If order is paid
            if ((oi != null) && (oi.OrderIsPaid))
            {
                // Disable specific controls
                this.btnNext.Enabled = false;
                this.CurrentStepControl.Enabled = false;
            }
        }
    }


    /// <summary>
    /// On page pre-render event.
    /// </summary>
    protected void Page_Prerender(object sender, EventArgs e)
    {
        if ((CheckoutProcessSteps != null) && (CurrentStepControl != null))
        {
            if (this.DisplayStepIndexes)
            {
                lblStepTitle.Text = HTMLHelper.HTMLEncode(String.Format(GetString("Order_New.CurrentStep"), CurrentStepIndex + 1, CheckoutProcessSteps.Count) + " - " + ResHelper.LocalizeString(CheckoutProcessSteps[CurrentStepIndex].Caption));
            }
            else
            {
                lblStepTitle.Text = HTMLHelper.HTMLEncode(ResHelper.LocalizeString(CheckoutProcessSteps[CurrentStepIndex].Caption));
            }
        }
        else
        {
            ButtonBack.Visible = false;
            ButtonNext.Visible = false;

            // Display error mesage, when no steps found
            if ((CheckoutProcessSteps == null) || (CheckoutProcessSteps.Count == 0))
            {
                lblError.Text = GetString("com.checkoutprocess.nosteps");
                lblError.Visible = true;
            }
        }

        // Save previous page url
        if (!RequestHelper.IsPostBack() && (Request.UrlReferrer != null))
        {
            string path = URLHelper.GetAppRelativePath(Request.UrlReferrer);
            if (!URLHelper.IsExcludedSystem(path))
            {
                // It previous page was another shopping cart step
                this.PreviousPageUrl = CMSContext.RawUrl.ToLower() == Request.UrlReferrer.PathAndQuery.ToLower() ? "~/" : Request.UrlReferrer.AbsoluteUri;
            }
        }
        else
        {
            // Try to find the Previeous page in session
            string prevPage = ValidationHelper.GetString(SessionHelper.GetValue("ShoppingCartUrlReferrer"), "");
            if (!String.IsNullOrEmpty(prevPage))
            {
                this.PreviousPageUrl = prevPage;
            }
        }
    }


    /// <summary>
    /// Back button clicked.
    /// </summary>
    protected void btnBack_Click(object sender, EventArgs e)
    {
        // Load first checkout process step if private data was cleared
        if (this.ShoppingCartInfoObj.PrivateDataCleared && (this.CurrentStepIndex > 0))
        {
            this.ShoppingCartInfoObj.PrivateDataCleared = false;
            this.LoadStep(0);

            lblError.Visible = true;
            lblError.Text = GetString("com.shoppingcart.sessiontimedout");
            return;
        }

        this.CurrentStepControl.ButtonBackClickAction();
    }


    /// <summary>
    /// Next button clicked.
    /// </summary>
    protected void btnNext_Click(object sender, EventArgs e)
    {
        // Load first checkout process step if private data was cleared
        if (this.ShoppingCartInfoObj.PrivateDataCleared && (this.CurrentStepIndex > 0))
        {
            this.ShoppingCartInfoObj.PrivateDataCleared = false;
            this.LoadStep(0);

            lblError.Visible = true;
            lblError.Text = GetString("com.shoppingcart.sessiontimedout");
            return;
        }

        // Check banned ip
        if (!BannedIPInfoProvider.IsAllowed(CMSContext.CurrentSiteName, BanControlEnum.AllNonComplete))
        {
            lblError.Visible = true;
            lblError.Text = GetString("General.BannedIP");
            return;
        }

        this.CurrentStepControl.ButtonNextClickAction();
    }


    /// <summary>
    /// Loads current step control.
    /// </summary>    
    public override void LoadCurrentStep()
    {
        if ((this.CurrentStepIndex >= 0) && (this.CurrentStepIndex < this.CheckoutProcessSteps.Count))
        {
            // Shopping cart container
            this.ShoppingCartContainer = pnlShoppingCart;

            // Default button settings
            this.ButtonBack.Enabled = true;
            this.ButtonNext.Enabled = true;
            this.ButtonBack.Visible = true;
            this.ButtonNext.Visible = true;
            this.ButtonBack.Text = GetString("general.back");
            this.ButtonNext.Text = GetString("general.next");
            this.ButtonBack.CssClass = "SubmitButton";
            this.ButtonNext.CssClass = "SubmitButton";

            if (this.CurrentStepControl != null)
            {
                // Display checkout process images
                if (this.DisplayStepImages)
                {
                    LoadCheckoutProcessImages();
                }

                // Set shopping cart step container
                this.CurrentStepControl.StepContainer = pnlCartStepInner;

                // Display current control      
                pnlCartStepInner.Controls.Clear();
                pnlCartStepInner.Controls.Add(this.CurrentStepControl);
            }
            else
            {
                lblError.Visible = true;
                lblError.Text = GetString("ShoppingCart.ErrorLoadingStep");
            }
        }
    }


    /// <summary>
    /// Loads checkout process images.
    /// </summary>
    private void LoadCheckoutProcessImages()
    {
        if ((this.CurrentStepControl != null) && (this.CurrentStepControl.CheckoutProcessStep != null))
        {
            // Get current step code name
            string currentStepName = this.CurrentStepControl.CheckoutProcessStep.Name.ToLower();

            // Clears image collection
            this.plcStepImages.Controls.Clear();

            // Go through the checkout process steps
            foreach (CheckoutProcessStepInfo step in CheckoutProcessSteps)
            {
                Image imgStep = new Image();
                string imageName = "";

                // If step is equal to Current step
                if (currentStepName == step.Name.ToLower())
                {

                    int dotIndex = step.Icon.IndexOf('.');
                    if (dotIndex > 1)
                    {
                        // Image name = [filename]_selected.[extension]
                        imageName = step.Icon.Insert(dotIndex, "_selected");
                    }
                    else
                    {
                        // Image name = [filename]_selected
                        imageName = step.Icon + "_selected";
                    }

                }
                // If step is different from Current step
                else
                {
                    // Image name = [filename].[extension]
                    imageName = step.Icon;
                }

                // Add step image to the collection
                imgStep.ID = "img" + step.Name;
                imgStep.ImageUrl = this.ImageFolderPath.TrimEnd('/') + "/" + imageName;
                imgStep.CssClass = "ShoppingCartStepImage";
                imgStep.ToolTip = imgStep.AlternateText = ResHelper.LocalizeString(step.Caption);
                plcStepImages.Controls.Add(imgStep);

                // Add image step separator
                if ((step.StepIndex < CheckoutProcessSteps.Count - 1) && (this.ImageStepSeparator != ""))
                {
                    LiteralControl separator = new LiteralControl(this.ImageStepSeparator);
                    plcStepImages.Controls.Add(separator);
                }
            }
        }
    }
}
