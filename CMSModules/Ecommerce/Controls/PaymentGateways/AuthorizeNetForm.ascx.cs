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

using CMS.GlobalHelper;
using CMS.Ecommerce;
using CMS.EcommerceProvider;

public partial class CMSModules_Ecommerce_Controls_PaymentGateways_AuthorizeNetForm : CMSPaymentGatewayForm
{
    #region "Private properties"

    /// <summary>
    /// Credit card number.
    /// </summary>
    private string CreditCardNumber
    {
        get
        {
            return txtCardNumber.Text.Trim();
        }
        set
        {
            txtCardNumber.Text = value;
        }
    }


    /// <summary>
    /// Credit card CCV (Card Code Verification).
    /// </summary>
    private string CreditCardCCV
    {
        get
        {
            return txtCCV.Text.Trim();
        }
        set
        {
            txtCCV.Text = value;
        }
    }


    /// <summary>
    /// Credit card expiration date.
    /// </summary>
    private DateTime CreditCardExpiration
    {
        get
        {
            if ((drpMonths.SelectedValue == "") || (drpYears.SelectedValue == ""))
            {
                return DataHelper.DATETIME_NOT_SELECTED;
            }
            else
            {
                DateTime result = DateTime.MinValue;
                result = result.AddYears(Convert.ToInt32(drpYears.SelectedValue) - 1);
                result = result.AddMonths(Convert.ToInt32(drpMonths.SelectedValue) - 1);
                return result;
            }
        }
        set
        {
            DateTime date = ValidationHelper.GetDateTime(value, DataHelper.DATETIME_NOT_SELECTED);
            if (date != DataHelper.DATETIME_NOT_SELECTED)
            {
                drpMonths.SelectedValue = date.Month.ToString();
                drpYears.SelectedValue = date.Year.ToString();
            }
        }
    }

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        // Title
        lblTitle.Text = ResHelper.GetString("AuthorizeNetForm.Title");

        // Labels
        lblCardNumber.Text = ResHelper.GetString("AuthorizeNetForm.CardNumber");
        lblExpiration.Text = ResHelper.GetString("AuthorizeNetForm.CardExpiration");
        lblCCV.Text = ResHelper.GetString("AuthorizeNetForm.CardCCV");
        lblCCV.ToolTip = ResHelper.GetString("AuthorizeNetForm.CardCCVTooltip");
        lblCCV.Attributes["style"] = "cursor:help;";

        // Validators
        rfvCardNumber.ErrorMessage = ResHelper.GetString("AuthorizeNetForm.ErrorCreditCardNumber");
        rfvCCV.ErrorMessage = ResHelper.GetString("AuthorizeNetForm.ErrorCreditCardCCV");

        // Load dropdown lists
        if ((this.drpMonths.Items.Count == 0) || (this.drpYears.Items.Count == 0))
        {
            InitializeLists();
        }

        // Add required fields mark
        if (this.ShoppingCartControl.RequiredFieldsMark != "")
        {
            string mark = this.ShoppingCartControl.RequiredFieldsMark;
            lblMark1.Text = mark;
            lblMark2.Text = mark;
            lblMark3.Text = mark;
        }
    }


    /// <summary>
    /// Process customer payment data.
    /// </summary>
    public override string ProcessData()
    {
        this.ShoppingCartInfoObj.PaymentGatewayCustomData[AuthorizeNetParameters.CARD_NUMBER] = this.CreditCardNumber;
        this.ShoppingCartInfoObj.PaymentGatewayCustomData[AuthorizeNetParameters.CARD_CCV] = this.CreditCardCCV;
        this.ShoppingCartInfoObj.PaymentGatewayCustomData[AuthorizeNetParameters.CARD_EXPIRATION] = this.CreditCardExpiration;
        return "";
    }


    /// <summary>
    /// Loads customer payment data to form controls.
    /// </summary>
    public override void LoadData()
    {        
        this.CreditCardNumber = ValidationHelper.GetString(this.ShoppingCartInfoObj.PaymentGatewayCustomData[AuthorizeNetParameters.CARD_NUMBER], "");
        this.CreditCardCCV = ValidationHelper.GetString(this.ShoppingCartInfoObj.PaymentGatewayCustomData[AuthorizeNetParameters.CARD_CCV], "");        
        this.CreditCardExpiration = ValidationHelper.GetDateTime(this.ShoppingCartInfoObj.PaymentGatewayCustomData[AuthorizeNetParameters.CARD_EXPIRATION], DataHelper.DATETIME_NOT_SELECTED);        
    }


    /// <summary>
    /// Validates customer payment data.
    /// </summary>
    public override string ValidateData()
    {
        string errorMessage = "";

        if (this.CreditCardNumber == "")
        {
            errorMessage = ResHelper.GetString("AuthorizeNetForm.ErrorCreditCardNumber");
        }

        if (this.CreditCardCCV == "")
        {
            errorMessage = ResHelper.GetString("AuthorizeNetForm.ErrorCreditCardCCV");
        }
        
        if (errorMessage != "")
        {
            // Show error message
            lblError.Visible = true;
            lblError.Text = errorMessage;
        }

        if (this.CreditCardExpiration == DataHelper.DATETIME_NOT_SELECTED)
        {
            lblErrorDate.Text = ResHelper.GetString("AuthorizeNetForm.ErrorCreditCardExpiration");
            lblErrorDate.Visible = true;
            errorMessage = lblErrorDate.Text;
        }

        return errorMessage;
    }


    /// <summary>
    /// Loads data to dropdownlists.
    /// </summary>
    private void InitializeLists()
    {
        // Load years
        for (int i = 0; i < 10; i++)
        {
            string year = Convert.ToString(DateTime.Now.Year + i);
            drpYears.Items.Add(new ListItem(year, year));
        }
        drpYears.Items.Insert(0, new ListItem("-", ""));


        // Load months
        for (int i = 1; i <= 12; i++)
        {
            string text = (i < 10) ? "0" + i.ToString() : i.ToString();
            drpMonths.Items.Add(new ListItem(text, i.ToString()));
        }
        drpMonths.Items.Insert(0, new ListItem("-", ""));
    }
}
