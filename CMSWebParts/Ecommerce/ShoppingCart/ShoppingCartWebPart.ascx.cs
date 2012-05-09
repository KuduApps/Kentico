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

using CMS.PortalControls;
using CMS.GlobalHelper;
using CMS.PortalEngine;
using CMS.CMSHelper;
using CMS.WebAnalytics;
using CMS.EcommerceProvider;

public partial class CMSWebParts_Ecommerce_ShoppingCart_ShoppingCartWebPart : CMSAbstractWebPart
{
    #region "Public properties"

    /// <summary>
    /// Gets or sets the url where customer is redirected after purchase.
    /// </summary>
    public string RedirectAfterPurchase
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("RedirectAfterPurchase"), this.cartElem.RedirectAfterPurchase);
        }
        set
        {
            this.SetValue("RedirectAfterPurchase", value);
            this.cartElem.RedirectAfterPurchase = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether retrieval of forgotten password is enabled.
    /// </summary>
    public bool PasswordRetrieval
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("PasswordRetrieval"), true);
        }
        set
        {
            this.SetValue("PasswordRetrieval", value);
            this.cartElem.EnablePasswordRetrieval = value;
        }
    }


    /// <summary>
    /// Gets or sets the conversion track name used after successful registration.
    /// </summary>
    public string RegistrationTrackConversionName
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("RegistrationTrackConversionName"), "");
        }
        set
        {
            if (value.Length > 400)
            {
                value = value.Substring(0, 400);
            }
            this.SetValue("RegistrationTrackConversionName", value);
            this.cartElem.RegistrationTrackConversionName = value;
        }
    }


    /// <summary>
    /// Gets or sets the conversion track name used after successful order.
    /// </summary>
    public string OrderTrackConversionName
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("OrderTrackConversionName"), "");
        }
        set
        {
            if (value.Length > 400)
            {
                value = value.Substring(0, 400);
            }
            this.SetValue("OrderTrackConversionName", value);
            this.cartElem.OrderTrackConversionName = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether product price detail link is displayed.
    /// </summary>
    public bool EnableProductPriceDetail
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("EnableProductPriceDetail"), false);
        }
        set
        {
            this.SetValue("EnableProductPriceDetail", value);
            this.cartElem.EnableProductPriceDetail = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether step images should be displayed.
    /// </summary>
    public bool DisplayStepImages
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("DisplayStepImages"), false);
        }
        set
        {
            this.SetValue("DisplayStepImages", value);
            this.cartElem.DisplayStepImages = value;
        }
    }


    /// <summary>
    /// Gets or sets the HTML code of the image step separator.
    /// </summary>
    public string ImageStepSeparator
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("ImageStepSeparator"), this.cartElem.ImageStepSeparator);
        }
        set
        {
            this.SetValue("ImageStepSeparator", value);
            this.cartElem.ImageStepSeparator = value;
        }
    }


    /// <summary>
    /// Gets or sets the text which is displayed for required fields in form.
    /// </summary>
    public string RequiredFieldsMark
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("RequiredFieldsMark"), "");
        }
        set
        {
            this.SetValue("RequiredFieldsMark", value);
            this.cartElem.RequiredFieldsMark = value;
        }
    }


    /// <summary>
    /// XML definition of the custom checkout process
    /// </summary>
    public string CheckoutProcess
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("CheckoutProcess"), "");
        }
        set
        {
            this.SetValue("CheckoutProcess", value);            
        }
    }

    #endregion


    #region "Registration properties"

    /// <summary>
    /// Gets or sets the email where the new registration notification should be sent to.
    /// </summary>
    public string SendNewRegistrationNotificationToAddress
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("SendNewRegistrationNotificationToAddress"), this.cartElem.SendNewRegistrationNotificationToAddress);
        }
        set
        {
            this.SetValue("SendNewRegistrationNotificationToAddress", value);
            this.cartElem.SendNewRegistrationNotificationToAddress = value;
        }
    }


    /// <summary>
    /// Gets or sets the roles where the new user should be assign to after the registration.
    /// </summary>
    public string AssignToRoles
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("AssignToRoles"), this.cartElem.AssignToRoles);
        }
        set
        {
            this.SetValue("AssignToRoles", value);
            this.cartElem.AssignToRoles = value;
        }
    }


    /// <summary>
    /// Gets or sets the sites where the new user should be assign to after the registration.
    /// </summary>
    public string AssignToSites
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("AssignToSites"), this.cartElem.AssignToSites);
        }
        set
        {
            this.SetValue("AssignToSites", value);
            this.cartElem.AssignToSites = value;
        }
    }

    #endregion


    /// <summary>
    /// Content loaded event handler.
    /// </summary>
    public override void OnContentLoaded()
    {
        base.OnContentLoaded();
        SetupControl();
    }


    /// <summary>
    /// Initializes the control properties.
    /// </summary>
    protected void SetupControl()
    {
        if (this.StopProcessing)
        {
            // Do nothing
        }
        else
        {
            this.NotResolveProperties = "AddToShoppingCartConversionValue;OrderConversionValue;RegistrationConversionValue";
            // Set shopping cart properties
            this.cartElem.RedirectAfterPurchase = this.RedirectAfterPurchase;
            this.cartElem.EnablePasswordRetrieval = this.PasswordRetrieval;
            this.cartElem.RegistrationTrackConversionName = this.RegistrationTrackConversionName;
            this.cartElem.OrderTrackConversionName = this.OrderTrackConversionName;
            this.cartElem.SendNewRegistrationNotificationToAddress = this.SendNewRegistrationNotificationToAddress;
            this.cartElem.AssignToRoles = this.AssignToRoles;
            this.cartElem.AssignToSites = this.AssignToSites;
            this.cartElem.DisplayStepImages = this.DisplayStepImages;
            this.cartElem.ImageStepSeparator = this.ImageStepSeparator;
            this.cartElem.EnableProductPriceDetail = this.EnableProductPriceDetail;
            this.cartElem.RequiredFieldsMark = this.RequiredFieldsMark;

            if (string.IsNullOrEmpty(this.CheckoutProcess))
            {
                // Load checkout process from e-commerce configuration
                this.cartElem.CheckoutProcessType = CheckoutProcessEnum.LiveSite;
            }
            else
            {
                // Load custom checkout process from web part
                this.cartElem.CheckoutProcessType = CheckoutProcessEnum.Custom;
                CheckoutProcessInfo process = new CheckoutProcessInfo(this.CheckoutProcess);
                this.cartElem.LoadCheckoutProcess(process);
            }
        }
    }


    /// <summary>
    /// Reload data.
    /// </summary>
    public override void ReloadData()
    {
        base.ReloadData();
        SetupControl();
    }
}
