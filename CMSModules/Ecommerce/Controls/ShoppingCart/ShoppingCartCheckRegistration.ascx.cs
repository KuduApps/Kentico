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

using CMS.CMSHelper;
using CMS.Ecommerce;
using CMS.EcommerceProvider;
using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.SettingsProvider;
using CMS.DataEngine;
using CMS.EmailEngine;
using CMS.EventLog;
using CMS.WebAnalytics;
using CMS.PortalEngine;

using TreeNode = CMS.TreeEngine.TreeNode;

public partial class CMSModules_Ecommerce_Controls_ShoppingCart_ShoppingCartCheckRegistration : ShoppingCartStep
{
    private bool mDataLoaded = false;

    /// <summary>
    /// '0' for Sign in using existing account.
    /// '1' for Create a new account.
    /// '2' for Continue as anonymous customer.
    /// </summary>
    public ShopingCartModeEnum RegistrationMode
    {
        get
        {
            if (radSignIn.Checked)
            {
                return ShopingCartModeEnum.ExistingAccount;
            }
            else if (radNewReg.Checked)
            {
                return ShopingCartModeEnum.NewAccount;
            }
            else
            {
                return ShopingCartModeEnum.AnonymousCustomer;
            }
        }
    }


    private bool mRequireOrgTaxRegIDs = false;
    private bool mShowTaxRegistrationIDField = false;
    private bool mShowOrganizationIDField = false;

    protected override void OnPreRender(EventArgs e)
    {
        string script = null;
        if (radSignIn.Checked)
        {
            script = ScriptHelper.GetScript("showHideForm('tblSignIn','" + radSignIn.ClientID + "');");
        }
        if (radNewReg.Checked)
        {
            script = ScriptHelper.GetScript("showHideForm('tblRegistration','" + radNewReg.ClientID + "');");
        }
        if (radAnonymous.Checked)
        {
            script = ScriptHelper.GetScript("showHideForm('tblAnonymous','" + radAnonymous.ClientID + "');");
        }

        ScriptHelper.RegisterStartupScript(this.Page, typeof(string), "ShowHideFormInit", script);

        base.OnPreRender(e);
    }


    /// <summary>
    /// On page load.
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptHelper.RegisterClientScriptBlock(this, this.GetType(), "showHide", ScriptHelper.GetScript(@"
            /* Shows and hides tables with forms*/
            function showHideForm(obj, rad)
            {
                var tblSignInStat = '';
                var tblRegistrationStat = '';
                var tblAnonymousStat = '';
                if( obj != null && obj != '' && rad != null)
                {
                    switch(obj)
                    {
                        case 'tblSignIn':
                            tblSignInStat = '';
                            tblRegistrationStat = 'none';
                            tblAnonymousStat = 'none';
                            break;

                        case 'tblRegistration':
                            tblSignInStat = 'none';
                            tblRegistrationStat = '';
                            tblAnonymousStat = 'none';
                            break;

                        case 'tblAnonymous':
                            tblSignInStat = 'none';
                            tblRegistrationStat = 'none';
                            tblAnonymousStat = '';
                            break;                
                    }

                    if(document.getElementById('tblSignIn') != null)
                        document.getElementById('tblSignIn').style.display = tblSignInStat;
                    if(document.getElementById('tblRegistration') != null)
                        document.getElementById('tblRegistration').style.display = tblRegistrationStat;
                    if(document.getElementById('tblAnonymous') != null)
                        document.getElementById('tblAnonymous').style.display = tblAnonymousStat;
                    if(document.getElementById(rad) != null)
                        document.getElementById(rad).setAttribute('checked','true');
                }
            }
            function showElem(id)
            {
                style = document.getElementById(id).style;
                style.display = (style.display == 'block')?'none':'block';
                return false;
            }
            function showHideChk(id)
            {
                var elem = document.getElementById(id);
                if(elem.style.display == 'block')
                {
                    elem.style.display = 'none';
                }
                else
                {
                    elem.style.display = 'block';
                }
            }"));

        SiteInfo si = CMSContext.CurrentSite;
        if (si != null)
        {
            this.mRequireOrgTaxRegIDs = ECommerceSettings.RequireCompanyInfo(si.SiteName);
            this.mShowOrganizationIDField = ECommerceSettings.ShowOrganizationID(si.SiteName);
            this.mShowTaxRegistrationIDField = ECommerceSettings.ShowTaxRegistrationID(si.SiteName);
        }

        this.PreRender += new EventHandler(CMSEcommerce_ShoppingCartCheckRegistration_PreRender);
        InitializeLabels();

        LoadStep(false);

        // Initialize onclick events
        radSignIn.Attributes.Add("onclick", "showHideForm('tblSignIn','" + radSignIn.ClientID + "');");
        radNewReg.Attributes.Add("onclick", "showHideForm('tblRegistration','" + radNewReg.ClientID + "');");
        radAnonymous.Attributes.Add("onclick", "showHideForm('tblAnonymous','" + radAnonymous.ClientID + "');");
        lnkPasswdRetrieval.Attributes.Add("onclick", "return showElem('" + pnlPasswdRetrieval.ClientID + "');");
        //chkCorporateBody.Attributes.Add("onclick", "showHideChk('" + pnlCompanyAccount1.ClientID + "');");
        //chkEditCorpBody.Attributes.Add("onclick", "showHideChk('" + pnlCompanyAccount2.ClientID + "');");            
    }


    void CMSEcommerce_ShoppingCartCheckRegistration_PreRender(object sender, EventArgs e)
    {
        if (!this.mDataLoaded && !this.ShoppingCartControl.IsCurrentStepPostBack)
        {
            LoadData();
        }
    }


    protected void LoadData()
    {
        this.mDataLoaded = true;
        // If user ID specified, load the given user ID
        if (!this.ShoppingCartControl.UserInfo.IsPublic())
        {
            // Get the customer data
            CustomerInfo ci = CustomerInfoProvider.GetCustomerInfoByUserID(this.ShoppingCartControl.UserInfo.UserID);

            // Set the fields
            if (ci != null)
            {
                this.txtEditCompany.Text = ci.CustomerCompany;
                this.txtEditEmail.Text = ci.CustomerEmail;
                this.txtEditFirst.Text = ci.CustomerFirstName;
                this.txtEditLast.Text = ci.CustomerLastName;
                this.txtEditOrgID.Text = ci.CustomerOrganizationID;
                this.txtEditTaxRegID.Text = ci.CustomerTaxRegistrationID;

                if (!DataHelper.IsEmpty(txtEditCompany.Text.Trim()) || !DataHelper.IsEmpty(txtEditOrgID.Text.Trim()) || !DataHelper.IsEmpty(txtEditTaxRegID.Text.Trim()))
                {
                    chkEditCorpBody.Checked = true;
                    pnlCompanyAccount2.Visible = true;
                }
            }
            else
            {
                this.txtEditFirst.Text = this.ShoppingCartControl.UserInfo.FirstName;
                this.txtEditLast.Text = this.ShoppingCartControl.UserInfo.LastName;
                this.txtEditEmail.Text = this.ShoppingCartControl.UserInfo.Email;
            }
        }
    }


    /// <summary>
    /// Loads anonymous customer data from view state.
    /// </summary>
    protected void LoadAnonymousCustomerData()
    {
        if (this.ShoppingCartInfoObj.CustomerInfoObj != null)
        {
            this.txtFirstName2.Text = this.ShoppingCartInfoObj.CustomerInfoObj.CustomerFirstName;
            this.txtLastName2.Text = this.ShoppingCartInfoObj.CustomerInfoObj.CustomerLastName;
            this.txtEmail3.Text = this.ShoppingCartInfoObj.CustomerInfoObj.CustomerEmail;
            this.txtCompany2.Text = this.ShoppingCartInfoObj.CustomerInfoObj.CustomerCompany;
            this.txtOrganizationID2.Text = this.ShoppingCartInfoObj.CustomerInfoObj.CustomerOrganizationID;
            this.txtTaxRegistrationID2.Text = this.ShoppingCartInfoObj.CustomerInfoObj.CustomerTaxRegistrationID;
        }
    }


    protected void LoadStep(bool loadData)
    {
        // If user logged in, edit the customer data
        if (!this.ShoppingCartControl.UserInfo.IsPublic())
        {
            this.plcEditCustomer.Visible = true;
            this.plcEditOrgID.Visible = mShowOrganizationIDField;
            this.plcEditTaxRegID.Visible = mShowTaxRegistrationIDField;
            this.plcAccount.Visible = false;
            this.lblTitle.Text = GetString("ShoppingCart.CheckRegistrationEdit");

            if (loadData)
            {
                LoadData();
            }
        }
        else
        {
            // Display/Hide the form for anonymous customer
            if (CMSContext.CurrentSite != null)
            {
                plhAnonymous.Visible = ECommerceSettings.AllowAnonymousCustomers(CMSContext.CurrentSite.SiteName);
            }

            if (!this.ShoppingCartControl.IsCurrentStepPostBack)
            {
                // If anonymous customer data were already saved -> display them
                if ((plhAnonymous.Visible) && (this.ShoppingCartInfoObj.ShoppingCartCustomerID > 0))
                {
                    // Mark 'Continue as anonymous customer' radio button
                    radAnonymous.Checked = true;

                    // Disable 'Sign in' section
                    //AvailabilityOfSignInSection(false);
                    // Disable 'New registration' section
                    //AvailabilityOfNewRegistrationSection(false);
                    // Enable 'Continue as anonymous customer' section
                    //AvailabilityOfAnonymousSection(true);

                    LoadAnonymousCustomerData();
                }
                else
                {
                    // Mark 'Sign in using your existing account' radio button
                    radSignIn.Checked = true;

                    // Enable 'Sign in' section
                    //AvailabilityOfSignInSection(true);
                    // Disable 'New registration' section
                    //AvailabilityOfNewRegistrationSection(false);
                    // Disable 'Continue as anonymous customer' section
                    //AvailabilityOfAnonymousSection(false);
                }
            }

            this.plcEditCustomer.Visible = false;
            this.plcAccount.Visible = true;

            this.plcTaxRegistrationID.Visible = this.mShowTaxRegistrationIDField;
            this.plcOrganizationID.Visible = this.mShowOrganizationIDField;

            this.plcTaxRegistrationID2.Visible = this.mShowTaxRegistrationIDField;
            this.plcOrganizationID2.Visible = this.mShowOrganizationIDField;

            this.lblTitle.Text = GetString("ShoppingCart.CheckRegistration");


            // Set strings
            lnkPasswdRetrieval.Text = GetString("LogonForm.lnkPasswordRetrieval");
            lblPasswdRetrieval.Text = GetString("LogonForm.lblPasswordRetrieval");
            btnPasswdRetrieval.Text = GetString("LogonForm.btnPasswordRetrieval");
            rqValue.ErrorMessage = GetString("LogonForm.rqValue");


            this.lnkPasswdRetrieval.Visible = this.ShoppingCartControl.EnablePasswordRetrieval;
            btnPasswdRetrieval.Click += new EventHandler(btnPasswdRetrieval_Click);

            this.pnlPasswdRetrieval.Attributes.Add("style", "display:none;");
            //this.pnlCompanyAccount1.Attributes.Add("style", "display:none;");
            //this.pnlCompanyAccount2.Attributes.Add("style", "display:none;");
        }

        //this.TitleText = GetString("Order_new.ShoppingCartCheckRegistration.Title");
    }


    /// <summary>
    /// Retrieve the user password.
    /// </summary>
    void btnPasswdRetrieval_Click(object sender, EventArgs e)
    {
        string value = txtPasswordRetrieval.Text.Trim();
        if ((value != String.Empty) && (CMSContext.CurrentSite != null))
        {
            bool success;
            lblResult.Text = UserInfoProvider.ForgottenEmailRequest(value, CMSContext.CurrentSiteName, "ECOMMERCE", ECommerceSettings.SendEmailsFrom(CMSContext.CurrentSite.SiteName), CMSContext.CurrentResolver, UserInfoProvider.GetResetPasswordUrl(CMSContext.CurrentSiteName), out success);

            plcResult.Visible = true;
            plcErrorResult.Visible = false;

            this.pnlPasswdRetrieval.Attributes.Add("style", "display:block;");
        }
    }


    /// <summary>
    /// Initialization of labels.
    /// </summary>
    protected void InitializeLabels()
    {
        if (mRequireOrgTaxRegIDs)
        {
            lblCompany1.Text = GetString("ShoppingCartCheckRegistration.CompanyRequired");
            lblOrganizationID.Text = GetString("ShoppingCartCheckRegistration.lblOrganizationIDRequired");
            lblTaxRegistrationID.Text = GetString("ShoppingCartCheckRegistration.lblTaxRegistrationIDRequired");
            lblMark15.Visible = true;
            lblMark16.Visible = true;
            lblMark17.Visible = true;
            lblMark21.Visible = true;
            lblMark22.Visible = true;
            lblMark23.Visible = true;

            lblEditCompany.Text = lblCompany1.Text;
            lblEditOrgID.Text = lblOrganizationID.Text;
            lblEditTaxRegID.Text = lblTaxRegistrationID.Text;
            lblMark18.Visible = true;
            lblMark19.Visible = true;
            lblMark20.Visible = true;
        }
        else
        {
            lblCompany1.Text = GetString("ShoppingCartCheckRegistration.Company");
            lblOrganizationID.Text = GetString("ShoppingCartCheckRegistration.lblOrganizationID");
            lblTaxRegistrationID.Text = GetString("ShoppingCartCheckRegistration.lblTaxRegistrationID");
            lblMark15.Visible = false;
            lblMark16.Visible = false;
            lblMark17.Visible = false;

            lblEditCompany.Text = lblCompany1.Text;
            lblEditOrgID.Text = lblOrganizationID.Text;
            lblEditTaxRegID.Text = lblTaxRegistrationID.Text;
            lblMark18.Visible = false;
            lblMark19.Visible = false;
            lblMark20.Visible = false;
        }

        radSignIn.Text = GetString("ShoppingCartCheckRegistration.SignIn");
        lblUsername.Text = GetString("ShoppingCartCheckRegistration.Username");
        lblPsswd1.Text = GetString("ShoppingCartCheckRegistration.Psswd");
        radNewReg.Text = GetString("ShoppingCartCheckRegistration.NewReg");
        lblFirstName1.Text = GetString("ShoppingCartCheckRegistration.FirstName");
        lblLastName1.Text = GetString("ShoppingCartCheckRegistration.LastName");
        lblEmail2.Text = GetString("ShoppingCartCheckRegistration.EmailUsername");
        lblEmail3.Text = GetString("ShoppingCartCheckRegistration.EmailUsername");
        lblPsswd2.Text = lblPsswd1.Text;
        lblConfirmPsswd.Text = GetString("ShoppingCartCheckRegistration.ConfirmPsswd");
        radAnonymous.Text = GetString("ShoppingCartCheckRegistration.Anonymous");
        lblFirstName2.Text = lblFirstName1.Text;
        lblLastName2.Text = lblLastName1.Text;
        lblCompany2.Text = lblCompany1.Text;

        lblEditFirst.Text = lblFirstName1.Text;
        lblEditLast.Text = lblLastName1.Text;
        lblEditEmail.Text = lblEmail3.Text;

        lblTaxRegistrationID2.Text = lblTaxRegistrationID.Text;

        lblOrganizationID2.Text = lblOrganizationID.Text;

        lblCorporateBody.Text = GetString("ShoppingCartCheckRegistration.lblCorporateBody");
        lblEditCorpBody.Text = lblCorporateBody.Text;

        // Mark required fields
        if (this.ShoppingCartControl.RequiredFieldsMark != "")
        {
            string mark = this.ShoppingCartControl.RequiredFieldsMark;
            this.lblMark1.Text = mark;
            this.lblMark2.Text = mark;
            this.lblMark3.Text = mark;
            this.lblMark4.Text = mark;
            this.lblMark5.Text = mark;
            this.lblMark6.Text = mark;
            this.passStrength.RequiredFieldMark = mark;
            this.lblMark8.Text = mark;
            this.lblMark9.Text = mark;
            this.lblMark10.Text = mark;
            this.lblMark11.Text = mark;
            this.lblMark12.Text = mark;
            this.lblMark13.Text = mark;
            this.lblMark14.Text = mark;
            this.lblMark15.Text = mark;
            this.lblMark16.Text = mark;
            this.lblMark17.Text = mark;
            this.lblMark18.Text = mark;
            this.lblMark19.Text = mark;
            this.lblMark20.Text = mark;
            this.lblMark21.Text = mark;
            this.lblMark22.Text = mark;
            this.lblMark23.Text = mark;
        }
    }


    /// <summary>
    /// On chkCorporateBody checkbox checked changed.
    /// </summary>
    protected void chkCorporateBody_CheckChanged(object sender, EventArgs e)
    {
        pnlCompanyAccount1.Visible = chkCorporateBody.Checked;
    }


    /// <summary>
    /// On chkEditCorpBody checkbox checked changed.
    /// </summary>
    protected void chkEditCorpBody_CheckChanged(object sender, EventArgs e)
    {
        pnlCompanyAccount2.Visible = chkEditCorpBody.Checked;
    }


    /// <summary>
    /// Validate values in textboxes.
    /// </summary>
    public override bool IsValid()
    {
        Validator val = new Validator();
        string result = null;

        if (this.plcAccount.Visible)
        {
            // Validate registration data
            if (radSignIn.Checked)
            {
                ScriptHelper.RegisterStartupScript(this, this.GetType(), "checkSignIn", ScriptHelper.GetScript("showHideForm('tblSignIn','" + radSignIn.ClientID + "');"));

                // Check banned IP
                if (!BannedIPInfoProvider.IsAllowed(CMSContext.CurrentSiteName, BanControlEnum.Login))
                {
                    result = GetString("banip.ipisbannedlogin");
                }

                // Check user name
                if (string.IsNullOrEmpty(result))
                {
                    result = val.NotEmpty(txtUsername.Text.Trim(), GetString("ShoppingCartCheckRegistration.ErrorMissingUsername")).Result;
                }

                if (!string.IsNullOrEmpty(result))
                {
                    lblError.Text = result;
                    lblError.Visible = true;
                    return false;
                }
            }
            // Check 'New registration' section
            else if (radNewReg.Checked)
            {
                ScriptHelper.RegisterStartupScript(this, this.GetType(), "checkRegistration", ScriptHelper.GetScript("showHideForm('tblRegistration','" + radNewReg.ClientID + "');"));

                // Check banned IP
                if (!BannedIPInfoProvider.IsAllowed(CMSContext.CurrentSiteName, BanControlEnum.Registration))
                {
                    result = GetString("banip.ipisbannedregistration");
                }

                if (string.IsNullOrEmpty(result) && !BannedIPInfoProvider.IsAllowed(CMSContext.CurrentSiteName, BanControlEnum.Login))
                {
                    result = GetString("banip.ipisbannedlogin");
                } 

                // Check registration form
                if (string.IsNullOrEmpty(result))
                {
                    result = val.NotEmpty(txtFirstName1.Text.Trim(), GetString("ShoppingCartCheckRegistration.FirstNameErr"))
                        .NotEmpty(txtLastName1.Text.Trim(), GetString("ShoppingCartCheckRegistration.LastNameErr"))
                        .NotEmpty(txtEmail2.Text.Trim(), GetString("ShoppingCartCheckRegistration.EmailErr"))
                        .NotEmpty(passStrength.Text.Trim(), GetString("ShoppingCartCheckRegistration.PsswdErr")).Result;
                }

                // Check company properties
                if (string.IsNullOrEmpty(result) && mRequireOrgTaxRegIDs && chkCorporateBody.Checked)
                {
                    result = val.NotEmpty(txtCompany1.Text.Trim(), GetString("ShoppingCartCheckRegistration.CompanyErr")).Result;
                    if ((result == "") && plcOrganizationID.Visible)
                    {
                        result = val.NotEmpty(txtOrganizationID.Text.Trim(), GetString("ShoppingCartCheckRegistration.OrganizationIDErr")).Result;
                    }

                    if ((result == "") && plcTaxRegistrationID.Visible)
                    {
                        result = val.NotEmpty(txtTaxRegistrationID.Text.Trim(), GetString("ShoppingCartCheckRegistration.TaxRegistrationIDErr")).Result;
                    }
                }
                if (result == "")
                {
                    if (!ValidationHelper.IsEmail(txtEmail2.Text.Trim()))
                    {
                        lblEmail2Err.Text = GetString("ShoppingCartCheckRegistration.EmailErr");
                        lblEmail2Err.Visible = true;
                    }
                    // Password and confirmed password must be same
                    if (passStrength.Text != txtConfirmPsswd.Text)
                    {
                        lblPsswdErr.Text = GetString("ShoppingCartCheckRegistration.DifferentPsswds");
                        lblPsswdErr.Visible = true;
                    }

                    // Check policy
                    if (!passStrength.IsValid())
                    {
                        lblPsswdErr.Text = UserInfoProvider.GetPolicyViolationMessage(CMSContext.CurrentSiteName);
                        lblPsswdErr.Visible = true;
                    }


                    if ((!DataHelper.IsEmpty(lblEmail2Err.Text.Trim())) || (!DataHelper.IsEmpty(lblPsswdErr.Text.Trim())))
                    {
                        return false;
                    }
                }
                else
                {
                    lblError.Text = result;
                    lblError.Visible = true;
                    return false;
                }
            }
            // Check 'Continue as anonymous customer' section
            else if (radAnonymous.Checked)
            {
                ScriptHelper.RegisterStartupScript(this, this.GetType(), "checkAnonymous", ScriptHelper.GetScript("showHideForm('tblAnonymous','" + radAnonymous.ClientID + "');"));

                result = val.NotEmpty(txtFirstName2.Text.Trim(), GetString("ShoppingCartCheckRegistration.FirstNameErr"))
                    .NotEmpty(txtLastName2.Text.Trim(), GetString("ShoppingCartCheckRegistration.LastNameErr"))
                    .NotEmpty(txtEmail3.Text.Trim(), GetString("ShoppingCartCheckRegistration.EmailErr")).Result;

                if (result == "" && mRequireOrgTaxRegIDs)
                {
                    result = val.NotEmpty(txtCompany2.Text.Trim(), ResHelper.GetString("ShoppingCartCheckRegistration.CompanyErr")).Result;
                    // Check organization ID only if visible
                    if ((result == "") && plcOrganizationID2.Visible)
                    {
                        result = val.NotEmpty(txtOrganizationID2.Text.Trim(), ResHelper.GetString("ShoppingCartCheckRegistration.OrganizationIDErr")).Result;
                    }
                    // Check tax ID only if visible
                    if ((result == "") && plcTaxRegistrationID2.Visible)
                    {
                        result = val.NotEmpty(txtTaxRegistrationID2.Text.Trim(), ResHelper.GetString("ShoppingCartCheckRegistration.TaxRegistrationIDErr")).Result;
                    }
                }

                if (result == "")
                {
                    if (!ValidationHelper.IsEmail(txtEmail3.Text.Trim()))
                    {
                        lblEmail3Err.Text = GetString("ShoppingCartCheckRegistration.EmailErr");
                        lblEmail3Err.Visible = true;
                        return false;
                    }
                }
                else
                {
                    lblError.Text = result;
                    lblError.Visible = true;
                    return false;
                }
            }
        }
        else
        {
            // Validate customer data
            result = val.NotEmpty(txtEditFirst.Text.Trim(), GetString("ShoppingCartCheckRegistration.FirstNameErr"))
                    .NotEmpty(txtEditLast.Text.Trim(), GetString("ShoppingCartCheckRegistration.LastNameErr"))
                    .IsEmail(txtEditEmail.Text.Trim(), GetString("ShoppingCartCheckRegistration.EmailErr")).Result;

            if (result == "" && mRequireOrgTaxRegIDs && chkEditCorpBody.Checked)
            {
                result = val.NotEmpty(txtEditCompany.Text.Trim(), GetString("ShoppingCartCheckRegistration.CompanyErr")).Result;
                // Check organization id only if visible
                if ((result == "") && plcEditOrgID.Visible)
                {
                    result = val.NotEmpty(txtEditOrgID.Text.Trim(), GetString("ShoppingCartCheckRegistration.OrganizationIDErr")).Result;
                }
                // Check tax id only if visible
                if ((result == "") && plcEditTaxRegID.Visible)
                {
                    result = val.NotEmpty(txtEditTaxRegID.Text.Trim(), GetString("ShoppingCartCheckRegistration.TaxRegistrationIDErr")).Result;
                }
            }
            if (result == "")
            {
                return true;
            }
            else
            {
                lblError.Text = result;
                lblError.Visible = true;
                return false;
            }
        }

        return true;
    }


    /// <summary>
    /// Process valid values of this step.
    /// </summary>
    public override bool ProcessStep()
    {
        if (this.plcAccount.Visible)
        {
            string siteName = CMSContext.CurrentSiteName;

            // Existing account
            if (radSignIn.Checked)
            {
                // Authenticate user
                UserInfo ui = UserInfoProvider.AuthenticateUser(txtUsername.Text.Trim(), txtPsswd1.Text, CMSContext.CurrentSiteName, false);
                if (ui == null)
                {
                    lblError.Text = GetString("ShoppingCartCheckRegistration.LoginFailed");
                    lblError.Visible = true;
                    return false;
                }

                // Sign in customer with existing account
                CMSContext.AuthenticateUser(ui.UserName, false);

                // Registered user has already started shopping as anonymous user -> Drop his stored shopping cart
                ShoppingCartInfoProvider.DeleteShoppingCartInfo(ui.UserID, siteName);

                // Assign current user to the current shopping cart
                this.ShoppingCartInfoObj.UserInfoObj = ui;

                // Save changes to database
                if (!this.ShoppingCartControl.IsInternalOrder)
                {
                    ShoppingCartInfoProvider.SetShoppingCartInfo(this.ShoppingCartInfoObj);
                }

                // Log "login" activity
                if ((CMSContext.ViewMode == ViewModeEnum.LiveSite) && ActivitySettingsHelper.ActivitiesEnabledAndModuleLoaded(siteName))
                {
                    this.ContactID = ModuleCommands.OnlineMarketingGetUserLoginContactID(ui);
                    ActivityLogHelper.UpdateContactLastLogon(this.ContactID);
                    if (ActivitySettingsHelper.ActivitiesEnabledForThisUser(ui) && ActivitySettingsHelper.UserLoginEnabled(siteName))
                    {
                        TreeNode currentDoc = CMSContext.CurrentDocument;
                        int nodeId = (currentDoc != null ? currentDoc.NodeID : 0);
                        string culture = (currentDoc != null ? currentDoc.DocumentCulture : null);
                        ActivityLogProvider.LogLoginActivity(this.ContactID, ui, URLHelper.CurrentRelativePath, nodeId, siteName, ui.UserCampaign, culture);
                    }
                }

                LoadStep(true);

                // Return false to get to Edit customer page
                return false;
            }
            // New registration
            else if (radNewReg.Checked)
            {
                txtEmail2.Text = txtEmail2.Text.Trim();
                pnlCompanyAccount1.Visible = chkCorporateBody.Checked;

                // Check if user exists
                UserInfo ui = UserInfoProvider.GetUserInfo(txtEmail2.Text);
                if (ui != null)
                {
                    lblError.Visible = true;
                    lblError.Text = GetString("ShoppingCartUserRegistration.ErrorUserExists");
                    return false;
                }

                // Check all sites where user will be assigned
                string checkSites = (String.IsNullOrEmpty(this.ShoppingCartControl.AssignToSites)) ? CMSContext.CurrentSiteName : this.ShoppingCartControl.AssignToSites;
                if (!UserInfoProvider.IsEmailUnique(txtEmail2.Text.Trim(), checkSites, 0))
                {
                    lblError.Visible = true;
                    lblError.Text = GetString("UserInfo.EmailAlreadyExist");
                    return false;
                }

                // Create new customer and user account and sign in
                // User
                ui = new UserInfo();
                ui.UserName = txtEmail2.Text.Trim();
                ui.Email = txtEmail2.Text.Trim();
                ui.FirstName = txtFirstName1.Text.Trim();
                ui.LastName = txtLastName1.Text.Trim();
                ui.FullName = ui.FirstName + " " + ui.LastName;
                ui.Enabled = true;
                ui.UserIsGlobalAdministrator = false;
                ui.UserURLReferrer = CMSContext.CurrentUser.URLReferrer;
                ui.UserCampaign = CMSContext.Campaign;
                ui.UserSettings.UserRegistrationInfo.IPAddress = HTTPHelper.UserHostAddress;
                ui.UserSettings.UserRegistrationInfo.Agent = HttpContext.Current.Request.UserAgent;

                int nodeId = 0;
                string culture = null;

                try
                {
                    UserInfoProvider.SetPassword(ui, passStrength.Text);

                    string[] siteList;

                    // If AssignToSites field set
                    if (!String.IsNullOrEmpty(this.ShoppingCartControl.AssignToSites))
                    {
                        siteList = this.ShoppingCartControl.AssignToSites.Split(';');
                    }
                    else // If not set user current site 
                    {
                        siteList = new string[] { siteName };
                    }

                    foreach (string site in siteList)
                    {
                        UserInfoProvider.AddUserToSite(ui.UserName, site);

                        // Add user to roles
                        if (this.ShoppingCartControl.AssignToRoles != "")
                        {
                            AssignUserToRoles(ui.UserName, this.ShoppingCartControl.AssignToRoles, site);
                        }
                    }

                    // Log registered user
                    AnalyticsHelper.LogRegisteredUser(siteName, ui);

                    // Log "user registered" activity
                    if ((CMSContext.ViewMode == ViewModeEnum.LiveSite) && ActivitySettingsHelper.ActivitiesEnabledAndModuleLoaded(siteName)
                        && ActivitySettingsHelper.ActivitiesEnabledForThisUser(ui) && ActivitySettingsHelper.UserRegistrationEnabled(siteName))
                    {
                        TreeNode currentDoc = CMSContext.CurrentDocument;
                        this.ContactID = ModuleCommands.OnlineMarketingGetUserLoginContactID(ui);
                        ModuleCommands.OnlineMarketingUpdateContactFromExternalData(ui, this.ContactID);
                        nodeId = (currentDoc != null ? currentDoc.NodeID : 0);
                        culture = (currentDoc != null ? currentDoc.DocumentCulture : null);
                        ActivityLogProvider.LogRegistrationActivity(this.ContactID, ui, URLHelper.CurrentRelativePath, nodeId, siteName, ui.UserCampaign, culture);
                    }
                }
                catch (Exception ex)
                {
                    lblError.Visible = true;
                    lblError.Text = ex.Message;
                    return false;
                }

                // Customer
                CustomerInfo ci = new CustomerInfo();
                ci.CustomerFirstName = this.txtFirstName1.Text.Trim();
                ci.CustomerLastName = this.txtLastName1.Text.Trim();
                ci.CustomerEmail = this.txtEmail2.Text.Trim();

                ci.CustomerCompany = "";
                ci.CustomerOrganizationID = "";
                ci.CustomerTaxRegistrationID = "";
                if (chkCorporateBody.Checked)
                {
                    ci.CustomerCompany = this.txtCompany1.Text.Trim();
                    if (mShowOrganizationIDField)
                    {
                        ci.CustomerOrganizationID = this.txtOrganizationID.Text.Trim();
                    }
                    if (mShowTaxRegistrationIDField)
                    {
                        ci.CustomerTaxRegistrationID = this.txtTaxRegistrationID.Text.Trim();
                    }
                }

                ci.CustomerUserID = ui.UserID;
                ci.CustomerSiteID = 0;
                ci.CustomerEnabled = true;
                ci.CustomerCreated = DateTime.Now;
                CustomerInfoProvider.SetCustomerInfo(ci);

                // Track successful registration conversion
                string name = this.ShoppingCartControl.RegistrationTrackConversionName;
                ECommerceHelper.TrackRegistrationConversion(this.ShoppingCartInfoObj.SiteName, name);

                // Log "customer registration" activity and update profile
                if ((CMSContext.ViewMode == ViewModeEnum.LiveSite) && ActivitySettingsHelper.ActivitiesEnabledAndModuleLoaded(siteName)
                    && ActivitySettingsHelper.ActivitiesEnabledForThisUser(ui) && ActivitySettingsHelper.CustomerRegistrationEnabled(siteName))
                {
                    if (this.ContactID <= 0)
                    {
                        this.ContactID = ModuleCommands.OnlineMarketingGetUserLoginContactID(ui);
                    }
                    ModuleCommands.OnlineMarketingUpdateContactFromExternalData(ci, this.ContactID);
                    this.ShoppingCartControl.TrackActivityCustomerRegistration(ci, ui, this.ContactID, siteName, URLHelper.CurrentRelativePath);
                }

                // Sign in
                if (ui.UserEnabled)
                {
                    CMSContext.AuthenticateUser(ui.UserName, false);
                    this.ShoppingCartInfoObj.UserInfoObj = ui;

                    // Log "login" activity
                    if ((CMSContext.ViewMode == ViewModeEnum.LiveSite) && ActivitySettingsHelper.ActivitiesEnabledAndModuleLoaded(siteName))
                    {
                        this.ContactID = ModuleCommands.OnlineMarketingGetUserLoginContactID(ui);
                        ActivityLogHelper.UpdateContactLastLogon(this.ContactID);
                        if (ActivitySettingsHelper.ActivitiesEnabledForThisUser(ui) && ActivitySettingsHelper.UserLoginEnabled(siteName))
                        {
                            if (nodeId <= 0)
                            {
                                TreeNode currentDoc = CMSContext.CurrentDocument;
                                nodeId = (currentDoc != null ? currentDoc.NodeID : 0);
                                culture = (currentDoc != null ? currentDoc.DocumentCulture : null);
                            }
                            ActivityLogProvider.LogLoginActivity(this.ContactID, ui, URLHelper.CurrentRelativePath, nodeId, siteName, ui.UserCampaign, culture);
                        }
                    }
                }

                this.ShoppingCartInfoObj.ShoppingCartCustomerID = ci.CustomerID;

                // Send new registration notification email
                if (this.ShoppingCartControl.SendNewRegistrationNotificationToAddress != "")
                {
                    SendRegistrationNotification(ui);
                }
            }
            // Anonymous customer
            else if (radAnonymous.Checked)
            {
                CustomerInfo ci = null;
                if (this.ShoppingCartInfoObj.ShoppingCartCustomerID > 0)
                {
                    // Update existing customer account
                    ci = CustomerInfoProvider.GetCustomerInfo(this.ShoppingCartInfoObj.ShoppingCartCustomerID);
                }
                if (ci == null)
                {
                    // Create new customer account 
                    ci = new CustomerInfo();
                }

                ci.CustomerFirstName = this.txtFirstName2.Text.Trim();
                ci.CustomerLastName = this.txtLastName2.Text.Trim();
                ci.CustomerEmail = this.txtEmail3.Text.Trim();

                ci.CustomerCompany = "";
                ci.CustomerOrganizationID = "";
                ci.CustomerTaxRegistrationID = "";
                ci.CustomerCompany = this.txtCompany2.Text.Trim();
                if (mShowOrganizationIDField)
                {
                    ci.CustomerOrganizationID = this.txtOrganizationID2.Text.Trim();
                }
                if (mShowTaxRegistrationIDField)
                {
                    ci.CustomerTaxRegistrationID = this.txtTaxRegistrationID2.Text.Trim();
                }

                ci.CustomerEnabled = true;
                ci.CustomerCreated = DateTime.Now;
                ci.CustomerSiteID = CMSContext.CurrentSiteID;
                CustomerInfoProvider.SetCustomerInfo(ci);

                // Log "customer registration" activity
                if ((CMSContext.ViewMode == ViewModeEnum.LiveSite) && ActivitySettingsHelper.ActivitiesEnabledAndModuleLoaded(siteName)
                    && ActivitySettingsHelper.ActivitiesEnabledForThisUser(CMSContext.CurrentUser) && ActivitySettingsHelper.CustomerRegistrationEnabled(siteName))
                {
                    this.ContactID = ModuleCommands.OnlineMarketingGetCurrentContactID();
                    this.ShoppingCartControl.TrackActivityCustomerRegistration(ci, CMSContext.CurrentUser, this.ContactID, siteName, URLHelper.CurrentRelativePath);
                }

                // Assign customer to shoppingcart
                this.ShoppingCartInfoObj.ShoppingCartCustomerID = ci.CustomerID;
            }
            else
            {
                return false;
            }
        }
        else
        {
            // Save the customer data
            bool newCustomer = false;
            CustomerInfo ci = CustomerInfoProvider.GetCustomerInfoByUserID(this.ShoppingCartControl.UserInfo.UserID);
            if (ci == null)
            {
                ci = new CustomerInfo();
                ci.CustomerUserID = this.ShoppingCartControl.UserInfo.UserID;
                ci.CustomerSiteID = 0;
                ci.CustomerEnabled = true;
                newCustomer = true;
            }

            // Old email address
            string oldEmail = ci.CustomerEmail.ToLower();

            ci.CustomerFirstName = this.txtEditFirst.Text.Trim();
            ci.CustomerLastName = this.txtEditLast.Text.Trim();
            ci.CustomerEmail = this.txtEditEmail.Text.Trim();

            pnlCompanyAccount2.Visible = chkEditCorpBody.Checked;

            ci.CustomerCompany = "";
            ci.CustomerOrganizationID = "";
            ci.CustomerTaxRegistrationID = "";
            if (chkEditCorpBody.Checked)
            {
                ci.CustomerCompany = this.txtEditCompany.Text.Trim();
                if (mShowOrganizationIDField)
                {
                    ci.CustomerOrganizationID = this.txtEditOrgID.Text.Trim();
                }
                if (mShowTaxRegistrationIDField)
                {
                    ci.CustomerTaxRegistrationID = this.txtEditTaxRegID.Text.Trim();
                }
            }

            // Update customer data
            CustomerInfoProvider.SetCustomerInfo(ci);

            // Update corresponding user email when required
            if (oldEmail != ci.CustomerEmail.ToLower())
            {
                UserInfo user = UserInfoProvider.GetUserInfo(ci.CustomerUserID);
                if (user != null)
                {
                    user.Email = ci.CustomerEmail;
                    UserInfoProvider.SetUserInfo(user);
                }
            }


            // Log "customer registration" activity and update contact profile
            string siteName = CMSContext.CurrentSiteName;
            if ((CMSContext.ViewMode == ViewModeEnum.LiveSite) && ActivitySettingsHelper.ActivitiesEnabledAndModuleLoaded(siteName)
                && ActivitySettingsHelper.ActivitiesEnabledForThisUser(CMSContext.CurrentUser) && ActivitySettingsHelper.CustomerRegistrationEnabled(siteName))
            {
                this.ContactID = ModuleCommands.OnlineMarketingGetCurrentContactID();
                ModuleCommands.OnlineMarketingUpdateContactFromExternalData(ci, this.ContactID);
                if (newCustomer)
                {
                    this.ShoppingCartControl.TrackActivityCustomerRegistration(ci, CMSContext.CurrentUser, this.ContactID, siteName, URLHelper.CurrentRelativePath);
                }
            }

            // Set the shopping cart customer ID
            this.ShoppingCartInfoObj.ShoppingCartCustomerID = ci.CustomerID;
        }

        try
        {
            if (!this.ShoppingCartControl.IsInternalOrder)
            {
                ShoppingCartInfoProvider.SetShoppingCartInfo(this.ShoppingCartInfoObj);
            }
            return true;
        }
        catch
        {
            return false;
        }
    }


    /// <summary>
    /// Adds user to role
    /// <param name="userName">User name</param>
    /// <param name="roles">Role names the user should be assign to. Role names are separated by the char of ';'</param>
    /// <param name="siteName">Site name</param>
    /// </summary>
    private void AssignUserToRoles(string userName, string roles, string siteName)
    {
        string[] roleList = roles.Split(';');
        if ((siteName != null) && (siteName != ""))
        {
            if ((roleList != null) && (roleList.Length > 0))
            {
                for (int i = 0; i < roleList.Length; i++)
                {
                    String roleName = roleList[i];
                    String sn = roleName.StartsWith(".") ? "" : siteName;

                    if (RoleInfoProvider.RoleExists(roleName, sn))
                    {
                        UserInfoProvider.AddUserToRole(userName, roleName, sn);
                    }

                }
            }
        }
    }


    /// <summary>
    /// Sends new registration notification e-mail to administrator.
    /// </summary>
    private void SendRegistrationNotification(UserInfo ui)
    {
        SiteInfo currentSite = CMSContext.CurrentSite;

        // Notify administrator
        if ((ui != null) && (currentSite != null) && (this.ShoppingCartControl.SendNewRegistrationNotificationToAddress != ""))
        {
            EmailTemplateInfo mEmailTemplate = null;
            if (!ui.UserEnabled)
            {
                mEmailTemplate = EmailTemplateProvider.GetEmailTemplate("Registration.Approve", currentSite.SiteName);
            }
            else
            {
                mEmailTemplate = EmailTemplateProvider.GetEmailTemplate("Registration.New", currentSite.SiteName);
            }

            EventLogProvider ev = new EventLogProvider();

            if (mEmailTemplate == null)
            {
                // Email template not exist
                ev.LogEvent("E", DateTime.Now, "RegistrationForm", "GetEmailTemplate", HTTPHelper.GetAbsoluteUri());
            }

            // Initialize email message
            EmailMessage message = new EmailMessage();
            message.EmailFormat = EmailFormatEnum.Default;

            message.From = ECommerceSettings.SendEmailsFrom(currentSite.SiteName);
            message.Subject = GetString("RegistrationForm.EmailSubject");

            message.Recipients = this.ShoppingCartControl.SendNewRegistrationNotificationToAddress;
            message.Body = mEmailTemplate.TemplateText;

            // Init macro resolving
            string[,] replacements = new string[4, 2];
            replacements[0, 0] = "firstname";
            replacements[0, 1] = ui.FirstName;
            replacements[1, 0] = "lastname";
            replacements[1, 1] = ui.LastName;
            replacements[2, 0] = "email";
            replacements[2, 1] = ui.Email;
            replacements[3, 0] = "username";
            replacements[3, 1] = ui.UserName;

            ContextResolver resolver = CMSContext.CurrentResolver;
            resolver.SourceParameters = replacements;

            try
            {
                // Add template metafiles to e-mail
                MetaFileInfoProvider.ResolveMetaFileImages(message, mEmailTemplate.TemplateID, EmailObjectType.EMAILTEMPLATE, MetaFileInfoProvider.OBJECT_CATEGORY_TEMPLATE);
                // Send e-mail
                EmailSender.SendEmailWithTemplateText(currentSite.SiteName, message, mEmailTemplate, resolver, false);
            }
            catch
            {
                // Email sending failed
                ev.LogEvent("E", DateTime.Now, "Membership", "RegistrationEmail", CMSContext.CurrentSite.SiteID);
            }
        }
    }
}
