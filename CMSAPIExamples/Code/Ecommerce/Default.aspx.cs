using System;
using System.Data;
using System.Collections.Generic;

using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.CMSHelper;
using CMS.SiteProvider;
using CMS.Ecommerce;
using CMS.SettingsProvider;
using CMS.EcommerceProvider;
using CMS.TreeEngine;
using CMS.WorkflowEngine;
using CMS.Newsletter;


[Title(Text = "E-commerce", ImageUrl = "CMSModules/CMS_Ecommerce/module.png")]
public partial class CMSAPIExamples_Code_Ecommerce_Default : CMSAPIExamplePage
{
    #region "Initialization"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Section: Configuration

        // Invoice
        this.apiGetAndUpdateInvoice.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndUpdateInvoice);

        // Checkout process step
        this.apiCreateCheckoutProcessStep.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(CreateCheckoutProcessStep);
        this.apiGetAndUpdateCheckoutProcessStep.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndUpdateCheckoutProcessStep);
        this.apiGetAndBulkUpdateCheckoutProcessSteps.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndBulkUpdateCheckoutProcessSteps);
        this.apiGenerateDefaultCheckoutProcess.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GenerateDefaultCheckoutProcess);
        this.apiDeleteCheckoutProcessStep.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(DeleteCheckoutProcessStep);

        // Tax class
        this.apiCreateTaxClass.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(CreateTaxClass);
        this.apiGetAndUpdateTaxClass.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndUpdateTaxClass);
        this.apiGetAndBulkUpdateTaxClasses.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndBulkUpdateTaxClasses);
        this.apiDeleteTaxClass.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(DeleteTaxClass);

        // Tax class value in country
        this.apiSetTaxClassValueInCountry.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(SetTaxClassValueInCountry);
        this.apiGetAndUpdateTaxClassValueInCountry.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndUpdateTaxClassValueInCountry);
        this.apiGetAndBulkUpdateTaxClassValuesInCountry.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndBulkUpdateTaxClassValuesInCountry);
        this.apiDeleteTaxClassValueInCountry.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(DeleteTaxClassValueInCountry);

        // Tax class value in state
        this.apiSetTaxClassValueInState.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(SetTaxClassValueInState);
        this.apiGetAndUpdateTaxClassValueInState.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndUpdateTaxClassValueInState);
        this.apiGetAndBulkUpdateTaxClassValuesInState.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndBulkUpdateTaxClassValuesInState);
        this.apiDeleteTaxClassValueInState.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(DeleteTaxClassValueInState);

        // Currency
        this.apiCreateCurrency.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(CreateCurrency);
        this.apiGetAndUpdateCurrency.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndUpdateCurrency);
        this.apiGetAndBulkUpdateCurrencies.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndBulkUpdateCurrencies);
        this.apiDeleteCurrency.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(DeleteCurrency);

        // Exchange table
        this.apiCreateExchangeTable.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(CreateExchangeTable);
        this.apiGetAndUpdateExchangeTable.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndUpdateExchangeTable);
        this.apiGetAndBulkUpdateExchangeTables.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndBulkUpdateExchangeTables);
        this.apiDeleteExchangeTable.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(DeleteExchangeTable);

        // Order status
        this.apiCreateOrderStatus.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(CreateOrderStatus);
        this.apiGetAndUpdateOrderStatus.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndUpdateOrderStatus);
        this.apiGetAndBulkUpdateOrderStatuses.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndBulkUpdateOrderStatuses);
        this.apiDeleteOrderStatus.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(DeleteOrderStatus);

        // Public status
        this.apiCreatePublicStatus.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(CreatePublicStatus);
        this.apiGetAndUpdatePublicStatus.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndUpdatePublicStatus);
        this.apiGetAndBulkUpdatePublicStatuses.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndBulkUpdatePublicStatuses);
        this.apiDeletePublicStatus.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(DeletePublicStatus);

        // Internal status
        this.apiCreateInternalStatus.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(CreateInternalStatus);
        this.apiGetAndUpdateInternalStatus.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndUpdateInternalStatus);
        this.apiGetAndBulkUpdateInternalStatuses.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndBulkUpdateInternalStatuses);
        this.apiDeleteInternalStatus.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(DeleteInternalStatus);

        // Department
        this.apiCreateDepartment.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(CreateDepartment);
        this.apiGetAndUpdateDepartment.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndUpdateDepartment);
        this.apiGetAndBulkUpdateDepartments.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndBulkUpdateDepartments);
        this.apiDeleteDepartment.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(DeleteDepartment);

        // Department user
        this.apiAddUserToDepartment.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(AddUserToDepartment);
        this.apiRemoveUserFromDepartment.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(RemoveUserFromDepartment);

        // Department default tax class
        this.apiAddTaxClassToDepartment.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(AddTaxClassToDepartment);
        this.apiRemoveTaxClassFromDepartment.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(RemoveTaxClassFromDepartment);

        // Shipping option
        this.apiCreateShippingOption.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(CreateShippingOption);
        this.apiGetAndUpdateShippingOption.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndUpdateShippingOption);
        this.apiGetAndBulkUpdateShippingOptions.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndBulkUpdateShippingOptions);
        this.apiDeleteShippingOption.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(DeleteShippingOption);

        // Payment method
        this.apiCreatePaymentMethod.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(CreatePaymentMethod);
        this.apiGetAndUpdatePaymentMethod.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndUpdatePaymentMethod);
        this.apiGetAndBulkUpdatePaymentMethods.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndBulkUpdatePaymentMethods);
        this.apiDeletePaymentMethod.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(DeletePaymentMethod);

        // Manufacturer
        this.apiCreateManufacturer.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(CreateManufacturer);
        this.apiGetAndUpdateManufacturer.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndUpdateManufacturer);
        this.apiGetAndBulkUpdateManufacturers.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndBulkUpdateManufacturers);
        this.apiDeleteManufacturer.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(DeleteManufacturer);

        // Supplier
        this.apiCreateSupplier.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(CreateSupplier);
        this.apiGetAndUpdateSupplier.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndUpdateSupplier);
        this.apiGetAndBulkUpdateSuppliers.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndBulkUpdateSuppliers);
        this.apiDeleteSupplier.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(DeleteSupplier);

        // Section: Products

        // Product
        this.apiCreateProduct.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(CreateProduct);
        this.apiGetAndUpdateProduct.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndUpdateProduct);
        this.apiGetAndBulkUpdateProducts.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndBulkUpdateProducts);
        this.apiDeleteProduct.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(DeleteProduct);

        // Product document
        this.apiCreateProductDocument.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(CreateProductDocument);
        this.apiGetAndUpdateProductDocument.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndUpdateProductDocument);
        this.apiDeleteProductDocument.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(DeleteProductDocument);

        // Product tax class
        this.apiAddTaxClassToProduct.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(AddTaxClassToProduct);
        this.apiRemoveTaxClassFromProduct.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(RemoveTaxClassFromProduct);

        // Volume discount
        this.apiCreateVolumeDiscount.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(CreateVolumeDiscount);
        this.apiGetAndUpdateVolumeDiscount.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndUpdateVolumeDiscount);
        this.apiGetAndBulkUpdateVolumeDiscounts.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndBulkUpdateVolumeDiscounts);
        this.apiDeleteVolumeDiscount.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(DeleteVolumeDiscount);

        // Product option category
        this.apiCreateOptionCategory.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(CreateOptionCategory);
        this.apiGetAndUpdateOptionCategory.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndUpdateOptionCategory);
        this.apiGetAndBulkUpdateOptionCategories.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndBulkUpdateOptionCategories);
        this.apiDeleteOptionCategory.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(DeleteOptionCategory);

        // Product option
        this.apiCreateOption.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(CreateOption);
        this.apiGetAndUpdateOption.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndUpdateOption);
        this.apiGetAndBulkUpdateOptions.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndBulkUpdateOptions);
        this.apiDeleteOption.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(DeleteOption);

        // Option category on product
        this.apiAddCategoryToProduct.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(AddCategoryToProduct);
        this.apiRemoveCategoryFromProduct.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(RemoveCategoryFromProduct);

        // Section: Discounts

        // Discount coupon
        this.apiCreateDiscountCoupon.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(CreateDiscountCoupon);
        this.apiGetAndUpdateDiscountCoupon.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndUpdateDiscountCoupon);
        this.apiGetAndBulkUpdateDiscountCoupons.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndBulkUpdateDiscountCoupons);
        this.apiDeleteDiscountCoupon.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(DeleteDiscountCoupon);

        // Discount coupon products
        this.apiAddProductToCoupon.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(AddProductToCoupon);
        this.apiRemoveProductFromCoupon.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(RemoveProductFromCoupon);

        // Discount level
        this.apiCreateDiscountLevel.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(CreateDiscountLevel);
        this.apiGetAndUpdateDiscountLevel.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndUpdateDiscountLevel);
        this.apiGetAndBulkUpdateDiscountLevels.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndBulkUpdateDiscountLevels);
        this.apiDeleteDiscountLevel.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(DeleteDiscountLevel);

        // Discount level department
        this.apiAddDepartmentToLevel.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(AddDepartmentToLevel);
        this.apiRemoveDepartmentFromLevel.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(RemoveDepartmentFromLevel);

        // Section: Customers

        // Customer
        this.apiCreateAnonymousCustomer.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(CreateAnonymousCustomer);
        this.apiCreateRegisteredCustomer.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(CreateRegisteredCustomer);
        this.apiGetAndUpdateCustomer.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndUpdateCustomer);
        this.apiGetAndBulkUpdateCustomers.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndBulkUpdateCustomers);
        this.apiDeleteCustomer.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(DeleteCustomer);

        // Customer address
        this.apiCreateAddress.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(CreateAddress);
        this.apiGetAndUpdateAddress.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndUpdateAddress);
        this.apiGetAndBulkUpdateAddresses.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndBulkUpdateAddresses);
        this.apiDeleteAddress.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(DeleteAddress);

        // Customer credit event
        this.apiCreateCreditEvent.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(CreateCreditEvent);
        this.apiGetAndUpdateCreditEvent.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndUpdateCreditEvent);
        this.apiGetAndBulkUpdateCreditEvents.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndBulkUpdateCreditEvents);
        this.apiGetTotalCredit.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetTotalCredit);
        this.apiDeleteCreditEvent.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(DeleteCreditEvent);

        // Customer newsletter
        this.apiSubscribeCustomerToNewsletter.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(SubscribeCustomerToNewsletter);
        this.apiUnsubscribeCustomerFromNewsletter.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(UnsubscribeCustomerFromNewsletter);

        // Customer discount level
        this.apiAssignDiscountLevelToCustomer.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(AssignDiscountLevelToCustomer);
        this.apiRemoveDiscountLevelFromCustomer.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(RemoveDiscountLevelFromCustomer);

        // Customer wishlist
        this.apiAddProductToWishlist.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(AddProductToWishlist);
        this.apiRemoveProductFromWishlist.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(RemoveProductFromWishlist);

        // Section: Orders

        // Order
        this.apiCreateOrder.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(CreateOrder);
        this.apiGetAndUpdateOrder.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndUpdateOrder);
        this.apiGetAndBulkUpdateOrders.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndBulkUpdateOrders);
        this.apiDeleteOrder.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(DeleteOrder);

        // Order item
        this.apiCreateOrderItem.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(CreateOrderItem);
        this.apiGetAndUpdateOrderItem.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndUpdateOrderItem);
        this.apiGetAndBulkUpdateOrderItems.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndBulkUpdateOrderItems);
        this.apiDeleteOrderItem.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(DeleteOrderItem);

        // Order status history
        this.apiChangeOrderStatus.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(ChangeOrderStatus);
        this.apiDeleteHistory.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(DeleteHistory);

    }

    #endregion


    #region "Mass actions"

    /// <summary>
    /// Runs all creating and managing examples.
    /// </summary>
    public override void RunAll()
    {
        base.RunAll();

        // Invoice
        this.apiGetAndUpdateInvoice.Run();

        // Checkout process step
        this.apiGenerateDefaultCheckoutProcess.Run();
        this.apiCreateCheckoutProcessStep.Run();
        this.apiGetAndUpdateCheckoutProcessStep.Run();
        this.apiGetAndBulkUpdateCheckoutProcessSteps.Run();

        // Tax class
        this.apiCreateTaxClass.Run();
        this.apiGetAndUpdateTaxClass.Run();
        this.apiGetAndBulkUpdateTaxClasses.Run();

        // Tax class value in country
        this.apiSetTaxClassValueInCountry.Run();
        this.apiGetAndUpdateTaxClassValueInCountry.Run();
        this.apiGetAndBulkUpdateTaxClassValuesInCountry.Run();

        // Tax class value in state
        this.apiSetTaxClassValueInState.Run();
        this.apiGetAndUpdateTaxClassValueInState.Run();
        this.apiGetAndBulkUpdateTaxClassValuesInState.Run();

        // Currency
        this.apiCreateCurrency.Run();
        this.apiGetAndUpdateCurrency.Run();
        this.apiGetAndBulkUpdateCurrencies.Run();

        // Exchange table
        this.apiCreateExchangeTable.Run();
        this.apiGetAndUpdateExchangeTable.Run();
        this.apiGetAndBulkUpdateExchangeTables.Run();

        // Order status
        this.apiCreateOrderStatus.Run();
        this.apiGetAndUpdateOrderStatus.Run();
        this.apiGetAndBulkUpdateOrderStatuses.Run();

        // Public status
        this.apiCreatePublicStatus.Run();
        this.apiGetAndUpdatePublicStatus.Run();
        this.apiGetAndBulkUpdatePublicStatuses.Run();

        // Internal status
        this.apiCreateInternalStatus.Run();
        this.apiGetAndUpdateInternalStatus.Run();
        this.apiGetAndBulkUpdateInternalStatuses.Run();

        // Department
        this.apiCreateDepartment.Run();
        this.apiGetAndUpdateDepartment.Run();
        this.apiGetAndBulkUpdateDepartments.Run();

        // Department user
        this.apiAddUserToDepartment.Run();

        // Department default tax class
        this.apiAddTaxClassToDepartment.Run();

        // Shipping option
        this.apiCreateShippingOption.Run();
        this.apiGetAndUpdateShippingOption.Run();
        this.apiGetAndBulkUpdateShippingOptions.Run();

        // Payment method
        this.apiCreatePaymentMethod.Run();
        this.apiGetAndUpdatePaymentMethod.Run();
        this.apiGetAndBulkUpdatePaymentMethods.Run();

        // Manufacturer
        this.apiCreateManufacturer.Run();
        this.apiGetAndUpdateManufacturer.Run();
        this.apiGetAndBulkUpdateManufacturers.Run();

        // Supplier
        this.apiCreateSupplier.Run();
        this.apiGetAndUpdateSupplier.Run();
        this.apiGetAndBulkUpdateSuppliers.Run();

        // Product
        this.apiCreateProduct.Run();
        this.apiGetAndUpdateProduct.Run();
        this.apiGetAndBulkUpdateProducts.Run();

        // Product document
        this.apiCreateProductDocument.Run();
        this.apiGetAndUpdateProductDocument.Run();

        // Product tax class
        this.apiAddTaxClassToProduct.Run();

        // Volume discount
        this.apiCreateVolumeDiscount.Run();
        this.apiGetAndUpdateVolumeDiscount.Run();
        this.apiGetAndBulkUpdateVolumeDiscounts.Run();

        // Product option category
        this.apiCreateOptionCategory.Run();
        this.apiGetAndUpdateOptionCategory.Run();
        this.apiGetAndBulkUpdateOptionCategories.Run();

        // Product option
        this.apiCreateOption.Run();
        this.apiGetAndUpdateOption.Run();
        this.apiGetAndBulkUpdateOptions.Run();

        // Option category on product
        this.apiAddCategoryToProduct.Run();

        // Discount coupon
        this.apiCreateDiscountCoupon.Run();
        this.apiGetAndUpdateDiscountCoupon.Run();
        this.apiGetAndBulkUpdateDiscountCoupons.Run();

        // Discount coupon products
        this.apiAddProductToCoupon.Run();

        // Discount level
        this.apiCreateDiscountLevel.Run();
        this.apiGetAndUpdateDiscountLevel.Run();
        this.apiGetAndBulkUpdateDiscountLevels.Run();

        // Discount level department
        this.apiAddDepartmentToLevel.Run();

        // Customer
        this.apiCreateAnonymousCustomer.Run();
        this.apiCreateRegisteredCustomer.Run();
        this.apiGetAndUpdateCustomer.Run();
        this.apiGetAndBulkUpdateCustomers.Run();

        // Address
        this.apiCreateAddress.Run();
        this.apiGetAndUpdateAddress.Run();
        this.apiGetAndBulkUpdateAddresses.Run();

        // Credit event
        this.apiCreateCreditEvent.Run();
        this.apiGetAndUpdateCreditEvent.Run();
        this.apiGetAndBulkUpdateCreditEvents.Run();
        this.apiGetTotalCredit.Run();

        // Customer newsletter
        this.apiSubscribeCustomerToNewsletter.Run();

        // Customer discount level
        this.apiAssignDiscountLevelToCustomer.Run();

        // Customer wishlist
        this.apiAddProductToWishlist.Run();

        // Order
        this.apiCreateOrder.Run();
        this.apiGetAndUpdateOrder.Run();
        this.apiGetAndBulkUpdateOrders.Run();

        // Order item
        this.apiCreateOrderItem.Run();
        this.apiGetAndUpdateOrderItem.Run();
        this.apiGetAndBulkUpdateOrderItems.Run();

        // Order status history
        this.apiChangeOrderStatus.Run();
    }


    /// <summary>
    /// Runs all cleanup examples.
    /// </summary>
    public override void CleanUpAll()
    {
        base.CleanUpAll();

        // Order status history
        this.apiDeleteHistory.Run();

        // Order item
        this.apiDeleteOrderItem.Run();

        // Order
        this.apiDeleteOrder.Run();

        // Customer wishlist
        this.apiRemoveProductFromWishlist.Run();

        // Discount level
        this.apiRemoveDiscountLevelFromCustomer.Run();

        // Customer newsletter
        this.apiUnsubscribeCustomerFromNewsletter.Run();

        // Credit event
        this.apiDeleteCreditEvent.Run();

        // Address
        this.apiDeleteAddress.Run();

        // Customer
        this.apiDeleteCustomer.Run();

        // Discount level department
        this.apiRemoveDepartmentFromLevel.Run();

        // Discount level
        this.apiDeleteDiscountLevel.Run();

        // Discount coupon products
        this.apiRemoveProductFromCoupon.Run();

        // Discount coupon
        this.apiDeleteDiscountCoupon.Run();

        // Option category on product
        this.apiRemoveCategoryFromProduct.Run();

        // Product option
        this.apiDeleteOption.Run();

        // Option category
        this.apiDeleteOptionCategory.Run();

        // Volume discount
        this.apiDeleteVolumeDiscount.Run();

        // Product tax class
        this.apiRemoveTaxClassFromProduct.Run();

        // Product document
        this.apiDeleteProductDocument.Run();

        // Product
        this.apiDeleteProduct.Run();

        // Supplier
        this.apiDeleteSupplier.Run();

        // Manufacturer
        this.apiDeleteManufacturer.Run();

        // Payment method
        this.apiDeletePaymentMethod.Run();

        // Shipping option
        this.apiDeleteShippingOption.Run();

        // Department default tax class
        this.apiRemoveTaxClassFromDepartment.Run();

        // Department user
        this.apiRemoveUserFromDepartment.Run();

        // Department
        this.apiDeleteDepartment.Run();

        // Internal status
        this.apiDeleteInternalStatus.Run();

        // Public status
        this.apiDeletePublicStatus.Run();

        // Order status
        this.apiDeleteOrderStatus.Run();

        // Exchange table
        this.apiDeleteExchangeTable.Run();

        // Currency
        this.apiDeleteCurrency.Run();

        // Tax class value in state
        this.apiDeleteTaxClassValueInState.Run();

        // Tax class value in country
        this.apiDeleteTaxClassValueInCountry.Run();

        // Checkout process step
        this.apiDeleteCheckoutProcessStep.Run();

        // Tax class
        this.apiDeleteTaxClass.Run();
    }

    #endregion


    #region "API examples - Invoice"

    /// <summary>
    /// Gets and updates invoice. Called when the "Get and update invoice" button is pressed.
    /// </summary>
    private bool GetAndUpdateInvoice()
    {
        // Get site name
        string siteName = CMSContext.CurrentSiteName;

        // Get the invoice
        string invoice = ECommerceSettings.InvoiceTemplate(siteName);

        if (!String.IsNullOrEmpty(invoice))
        {
            // Add string to the invoice
            invoice = "<h1>My updated invoice</h1>" + invoice;

            // Update the invoice
            SettingsKeyProvider.SetValue(siteName + "." + ECommerceSettings.INVOICE_TEMPLATE, invoice);

            return true;
        }

        return false;
    }

    #endregion


    #region "API examples - Checkout process step"

    /// <summary>
    /// Generates default checkout process. Called when the "Generate default process" button is pressed.
    /// </summary>
    private bool GenerateDefaultCheckoutProcess()
    {
        // Create new checkout process object
        CheckoutProcessInfo checkoutProcess = new CheckoutProcessInfo();

        // Load default checkout process xml definition
        checkoutProcess.LoadXmlDefinition(ShoppingCart.DEFAULT_CHECKOUT_PROCESS);

        // Save the data
        SettingsKeyProvider.SetValue(CMSContext.CurrentSiteName + "." + ECommerceSettings.CHECKOUT_PROCESS, checkoutProcess.GetXmlDefinition());

        return true;
    }


    /// <summary>
    /// Creates checkout process step. Called when the "Create step" button is pressed.
    /// </summary>
    private bool CreateCheckoutProcessStep()
    {
        // Create new checkout process step object
        CheckoutProcessStepInfo newStep = new CheckoutProcessStepInfo();

        // Set the properties
        newStep.Name = "MyNewStep";
        newStep.Caption = "My new step";
        newStep.ControlPath = "";
        newStep.ShowInCMSDeskCustomer = true;
        newStep.ShowInCMSDeskOrder = true;
        newStep.ShowOnLiveSite = true;
        newStep.ShowInCMSDeskOrderItems = true;

        // Insert node
        CheckoutProcessInfo checkoutProcess = new CheckoutProcessInfo();
        checkoutProcess.LoadXmlDefinition(ECommerceSettings.CheckoutProcess(CMSContext.CurrentSiteName));
        checkoutProcess.SetCheckoutProcessStepNode(newStep);

        // Create the checkout process
        SettingsKeyProvider.SetValue(CMSContext.CurrentSiteName + "." + ECommerceSettings.CHECKOUT_PROCESS, checkoutProcess.GetXmlDefinition());

        return true;
    }


    /// <summary>
    /// Gets and updates checkout process step. Called when the "Get and update step" button is pressed.
    /// Expects the CreateCheckoutProcessStep method to be run first.
    /// </summary>
    private bool GetAndUpdateCheckoutProcessStep()
    {
        CheckoutProcessInfo checkoutProcess = new CheckoutProcessInfo();
        checkoutProcess.LoadXmlDefinition(ECommerceSettings.CheckoutProcess(CMSContext.CurrentSiteName));

        CheckoutProcessStepInfo updateStep = checkoutProcess.GetCheckoutProcessStepInfo("MyNewStep");

        if (updateStep != null)
        {
            // Update the property
            updateStep.Caption = updateStep.Caption.ToLower();

            // Update xml definition
            checkoutProcess.SetCheckoutProcessStepNode(updateStep);

            // Update the checkout process
            SettingsKeyProvider.SetValue(CMSContext.CurrentSiteName + "." + ECommerceSettings.CHECKOUT_PROCESS, checkoutProcess.GetXmlDefinition());

            return true;
        }

        return false;
    }


    /// <summary>
    /// Gets and bulk update checkout process steps. Called when the "Get and bulk update steps" button is pressed.
    /// Expects the CreateCheckoutProcessStep method to be run first.
    /// </summary>
    private bool GetAndBulkUpdateCheckoutProcessSteps()
    {
        bool success = false;

        CheckoutProcessInfo checkoutProcess = new CheckoutProcessInfo();
        checkoutProcess.LoadXmlDefinition(ECommerceSettings.CheckoutProcess(CMSContext.CurrentSiteName));

        // Get the list
        List<CheckoutProcessStepInfo> steps = checkoutProcess.GetCheckoutProcessSteps(CheckoutProcessEnum.CMSDeskOrder);
        foreach (CheckoutProcessStepInfo updateStep in steps)
        {
            if (updateStep.Name == "MyNewStep")
            {
                // Update the property
                updateStep.Caption = updateStep.Caption.ToUpper();

                // Update xml definition
                checkoutProcess.SetCheckoutProcessStepNode(updateStep);

                success = true;
            }
        }

        // Update the checkout process
        SettingsKeyProvider.SetValue(CMSContext.CurrentSiteName + "." + ECommerceSettings.CHECKOUT_PROCESS, checkoutProcess.GetXmlDefinition());

        return success;
    }


    /// <summary>
    /// Deletes checkout process step. Called when the "Delete step" button is pressed.
    /// Expects the CreateCheckoutProcessStep method to be run first.
    /// </summary>
    private bool DeleteCheckoutProcessStep()
    {
        // Create new checkout process
        CheckoutProcessInfo checkoutProcess = new CheckoutProcessInfo();

        // Load checkout process xml definition
        checkoutProcess.LoadXmlDefinition(ECommerceSettings.CheckoutProcess(CMSContext.CurrentSiteName));

        // Get 'My new step' checkout process step
        CheckoutProcessStepInfo deleteStep = checkoutProcess.GetCheckoutProcessStepInfo("MyNewStep");

        if (deleteStep != null)
        {
            // Delete the step
            checkoutProcess.RemoveCheckoutProcessStepNode("MyNewStep");

            // Save the data
            SettingsKeyProvider.SetValue(CMSContext.CurrentSiteName + "." + ECommerceSettings.CHECKOUT_PROCESS, checkoutProcess.GetXmlDefinition());

            return true;
        }

        return false;
    }

    #endregion


    #region "API examples - Tax class"

    /// <summary>
    /// Creates tax class. Called when the "Create class" button is pressed.
    /// </summary>
    private bool CreateTaxClass()
    {
        // Create new tax class
        TaxClassInfo newClass = new TaxClassInfo();

        // Set the properties
        newClass.TaxClassDisplayName = "My new class";
        newClass.TaxClassName = "MyNewClass";
        newClass.TaxClassSiteID = CMSContext.CurrentSiteID;

        // Create the tax class
        TaxClassInfoProvider.SetTaxClassInfo(newClass);

        return true;
    }


    /// <summary>
    /// Gets and updates tax class. Called when the "Get and update class" button is pressed.
    /// Expects the CreateTaxClass method to be run first.
    /// </summary>
    private bool GetAndUpdateTaxClass()
    {
        // Get the tax class
        TaxClassInfo updateClass = TaxClassInfoProvider.GetTaxClassInfo("MyNewClass", CMSContext.CurrentSiteName);
        if (updateClass != null)
        {
            // Update the properties
            updateClass.TaxClassDisplayName = updateClass.TaxClassDisplayName.ToLower();

            // Update the tax class
            TaxClassInfoProvider.SetTaxClassInfo(updateClass);

            return true;
        }

        return false;
    }


    /// <summary>
    /// Gets and bulk updates tax classes. Called when the "Get and bulk update classes" button is pressed.
    /// Expects the CreateTaxClass method to be run first.
    /// </summary>
    private bool GetAndBulkUpdateTaxClasses()
    {
        // Prepare the parameters
        string where = "TaxClassName LIKE N'MyNewClass%'";

        // Get the data
        DataSet classes = TaxClassInfoProvider.GetTaxClasses(where, null);
        if (!DataHelper.DataSourceIsEmpty(classes))
        {
            // Loop through the individual items
            foreach (DataRow classDr in classes.Tables[0].Rows)
            {
                // Create object from DataRow
                TaxClassInfo modifyClass = new TaxClassInfo(classDr);

                // Update the properties
                modifyClass.TaxClassDisplayName = modifyClass.TaxClassDisplayName.ToUpper();

                // Update the tax class
                TaxClassInfoProvider.SetTaxClassInfo(modifyClass);
            }

            return true;
        }

        return false;
    }


    /// <summary>
    /// Deletes tax class. Called when the "Delete class" button is pressed.
    /// Expects the CreateTaxClass method to be run first.
    /// </summary>
    private bool DeleteTaxClass()
    {
        // Get the tax class
        TaxClassInfo deleteClass = TaxClassInfoProvider.GetTaxClassInfo("MyNewClass", CMSContext.CurrentSiteName);

        // Delete the tax class
        TaxClassInfoProvider.DeleteTaxClassInfo(deleteClass);

        return (deleteClass != null);
    }

    #endregion


    #region "API examples - Tax class value in country"

    /// <summary>
    /// Sets tax class value in country. Called when the "Set value" button is pressed.
    /// </summary>
    private bool SetTaxClassValueInCountry()
    {
        // Get the tax class
        TaxClassInfo taxClass = TaxClassInfoProvider.GetTaxClassInfo("MyNewClass", CMSContext.CurrentSiteName);

        // Get the country
        CountryInfo country = CountryInfoProvider.GetCountryInfo("USA");

        if ((taxClass != null) && (country != null))
        {
            // Create new tax class country object
            TaxClassCountryInfo newTaxCountry = new TaxClassCountryInfo();

            // Set the properties
            newTaxCountry.TaxClassID = taxClass.TaxClassID;
            newTaxCountry.CountryID = country.CountryID;
            newTaxCountry.TaxValue = 10;
            newTaxCountry.IsFlatValue = true;

            // Set the tax class value in country
            TaxClassCountryInfoProvider.SetTaxClassCountryInfo(newTaxCountry);

            return true;
        }

        return false;
    }


    /// <summary>
    /// Gets and updates tax class value in country. Called when the "Get and update value" button is pressed.
    /// Expects the SetTaxClassValueInCountry method to be run first.
    /// </summary>
    private bool GetAndUpdateTaxClassValueInCountry()
    {
        // Get the tax class
        TaxClassInfo taxClass = TaxClassInfoProvider.GetTaxClassInfo("MyNewClass", CMSContext.CurrentSiteName);

        // Get the country
        CountryInfo country = CountryInfoProvider.GetCountryInfo("USA");

        if ((taxClass != null) && (country != null))
        {
            // Get the tax class country
            TaxClassCountryInfo updateTaxCountry = TaxClassCountryInfoProvider.GetTaxClassCountryInfo(country.CountryID, taxClass.TaxClassID);
            if (updateTaxCountry != null)
            {
                // Update the property
                updateTaxCountry.TaxValue = 100;

                // Update the tax class value in country
                TaxClassCountryInfoProvider.SetTaxClassCountryInfo(updateTaxCountry);

                return true;
            }
        }

        return false;
    }


    /// <summary>
    /// Gets and bulk updates tax class values in country. Called when the "Get and bulk update values" button is pressed.
    /// Expects the SetTaxClassValueInCountry method to be run first.
    /// </summary>
    private bool GetAndBulkUpdateTaxClassValuesInCountry()
    {
        // Get the tax class
        TaxClassInfo taxClass = TaxClassInfoProvider.GetTaxClassInfo("MyNewClass", CMSContext.CurrentSiteName);

        // Get the country
        CountryInfo country = CountryInfoProvider.GetCountryInfo("USA");

        if ((taxClass != null) && (country != null))
        {
            // Prepare the parameters
            string where = "TaxClassID = " + taxClass.TaxClassID + " AND CountryID = " + country.CountryID;

            // Get the data
            DataSet classCountries = TaxClassCountryInfoProvider.GetTaxClassCountries(where, null);
            if (!DataHelper.DataSourceIsEmpty(classCountries))
            {
                // Loop through the individual items
                foreach (DataRow classCountriesDr in classCountries.Tables[0].Rows)
                {
                    // Create object from DataRow
                    TaxClassCountryInfo modifyClassCountry = new TaxClassCountryInfo(classCountriesDr);

                    // Update the properties
                    modifyClassCountry.TaxValue = 50;

                    // Save the changes
                    TaxClassCountryInfoProvider.SetTaxClassCountryInfo(modifyClassCountry);
                }

                return true;
            }
        }

        return false;
    }


    /// <summary>
    /// Deletes tax class value in country. Called when the "Delete value" button is pressed.
    /// Expects the SetTaxClassValueInCountry method to be run first.
    /// </summary>
    private bool DeleteTaxClassValueInCountry()
    {
        // Get the tax class
        TaxClassInfo taxClass = TaxClassInfoProvider.GetTaxClassInfo("MyNewClass", CMSContext.CurrentSiteName);

        // Get the country
        CountryInfo country = CountryInfoProvider.GetCountryInfo("USA");

        if ((taxClass != null) && (country != null))
        {
            // Get the tax class country
            TaxClassCountryInfo deleteTaxCountry = TaxClassCountryInfoProvider.GetTaxClassCountryInfo(country.CountryID, taxClass.TaxClassID);
            if (deleteTaxCountry != null)
            {
                // Delete the tax class value in country
                TaxClassCountryInfoProvider.DeleteTaxClassCountryInfo(deleteTaxCountry);

                return true;
            }
        }

        return false;
    }

    #endregion


    #region "API examples - Tax class value in state"

    /// <summary>
    /// Sets tax class value in state. Called when the "Set value" button is pressed.
    /// </summary>
    private bool SetTaxClassValueInState()
    {
        // Get the tax class
        TaxClassInfo taxClass = TaxClassInfoProvider.GetTaxClassInfo("MyNewClass", CMSContext.CurrentSiteName);

        // Get the state
        StateInfo state = StateInfoProvider.GetStateInfo("Alabama");

        if ((taxClass != null) && (state != null))
        {
            // Create new tax class state
            TaxClassStateInfo newTaxState = new TaxClassStateInfo();

            // Set the properties
            newTaxState.TaxClassID = taxClass.TaxClassID;
            newTaxState.StateID = state.StateID;
            newTaxState.TaxValue = 10;
            newTaxState.IsFlatValue = true;

            // Set the tax class value in state
            TaxClassStateInfoProvider.SetTaxClassStateInfo(newTaxState);

            return true;
        }

        return false;
    }


    /// <summary>
    /// Gets and updates tax class value in state. Called when the "Get and update values" button is pressed.
    /// Expects the SetTaxClassValueInState method to be run first.
    /// </summary>
    private bool GetAndUpdateTaxClassValueInState()
    {
        // Get the tax class
        TaxClassInfo taxClass = TaxClassInfoProvider.GetTaxClassInfo("MyNewClass", CMSContext.CurrentSiteName);

        // Get the state
        StateInfo state = StateInfoProvider.GetStateInfo("Alabama");

        if ((taxClass != null) && (state != null))
        {
            // Get the tax class state
            TaxClassStateInfo updateTaxState = TaxClassStateInfoProvider.GetTaxClassStateInfo(taxClass.TaxClassID, state.StateID);
            if (updateTaxState != null)
            {
                // Update the property
                updateTaxState.TaxValue = 100;

                // Update the tax class value in state
                TaxClassStateInfoProvider.SetTaxClassStateInfo(updateTaxState);

                return true;
            }
        }

        return false;
    }


    /// <summary>
    /// Gets and bulk updates tax class values in state. Called when the "Get and bulk update values" button is pressed.
    /// Expects the SetTaxClassValueInState method to be run first.
    /// </summary>
    private bool GetAndBulkUpdateTaxClassValuesInState()
    {
        // Get the tax class
        TaxClassInfo taxClass = TaxClassInfoProvider.GetTaxClassInfo("MyNewClass", CMSContext.CurrentSiteName);

        // Get the state
        StateInfo state = StateInfoProvider.GetStateInfo("Alabama");

        if ((taxClass != null) && (state != null))
        {
            // Prepare the parameters
            string where = "TaxClassID = " + taxClass.TaxClassID + " AND StateID = " + state.StateID;

            // Get the data
            DataSet classStates = TaxClassStateInfoProvider.GetTaxClassStates(where, null);
            if (!DataHelper.DataSourceIsEmpty(classStates))
            {
                // Loop through the individual items
                foreach (DataRow classStatesDr in classStates.Tables[0].Rows)
                {
                    // Create object from DataRow
                    TaxClassStateInfo modifyClassState = new TaxClassStateInfo(classStatesDr);

                    // Update the properties
                    modifyClassState.TaxValue = 50;

                    // Save the changes
                    TaxClassStateInfoProvider.SetTaxClassStateInfo(modifyClassState);
                }

                return true;
            }
        }

        return false;
    }


    /// <summary>
    /// Deletes tax class value in state. Called when the "Delete value" button is pressed.
    /// Expects the SetTaxClassValueInState method to be run first.
    /// </summary>
    private bool DeleteTaxClassValueInState()
    {
        // Get the tax class
        TaxClassInfo taxClass = TaxClassInfoProvider.GetTaxClassInfo("MyNewClass", CMSContext.CurrentSiteName);

        // Get the state
        StateInfo state = StateInfoProvider.GetStateInfo("Alabama");

        if ((taxClass != null) && (state != null))
        {
            // Get the tax class state
            TaxClassStateInfo deleteTaxState = TaxClassStateInfoProvider.GetTaxClassStateInfo(taxClass.TaxClassID, state.StateID);
            if (deleteTaxState != null)
            {
                // Delete the tax class value in state
                TaxClassStateInfoProvider.DeleteTaxClassStateInfo(deleteTaxState);

                return true;
            }
        }

        return false;
    }

    #endregion


    #region "API examples - Currency"

    /// <summary>
    /// Creates currency. Called when the "Create currency" button is pressed.
    /// </summary>
    private bool CreateCurrency()
    {
        // Create new currency object
        CurrencyInfo newCurrency = new CurrencyInfo();

        // Set the properties
        newCurrency.CurrencyDisplayName = "My new currency";
        newCurrency.CurrencyName = "MyNewCurrency";
        newCurrency.CurrencyCode = "MNC";
        newCurrency.CurrencySiteID = CMSContext.CurrentSiteID;
        newCurrency.CurrencyEnabled = true;
        newCurrency.CurrencyFormatString = "{0:F} MNC";
        newCurrency.CurrencyIsMain = false;

        // Create the currency
        CurrencyInfoProvider.SetCurrencyInfo(newCurrency);

        return true;
    }


    /// <summary>
    /// Gets and updates currency. Called when the "Get and update currency" button is pressed.
    /// Expects the CreateCurrency method to be run first.
    /// </summary>
    private bool GetAndUpdateCurrency()
    {
        // Get the currency
        CurrencyInfo updateCurrency = CurrencyInfoProvider.GetCurrencyInfo("MyNewCurrency", CMSContext.CurrentSiteName);
        if (updateCurrency != null)
        {
            // Update the properties
            updateCurrency.CurrencyDisplayName = updateCurrency.CurrencyDisplayName.ToLower();

            // Update the currency
            CurrencyInfoProvider.SetCurrencyInfo(updateCurrency);

            return true;
        }

        return false;
    }


    /// <summary>
    /// Gets and bulk updates currencies. Called when the "Get and bulk update currencies" button is pressed.
    /// Expects the CreateCurrency method to be run first.
    /// </summary>
    private bool GetAndBulkUpdateCurrencies()
    {
        // Prepare the parameters
        string where = "CurrencyName LIKE N'MyNewCurrency%'";

        // Get the data
        DataSet currencies = CurrencyInfoProvider.GetCurrencies(where, null, 0, null);
        if (!DataHelper.DataSourceIsEmpty(currencies))
        {
            // Loop through the individual items
            foreach (DataRow currencyDr in currencies.Tables[0].Rows)
            {
                // Create object from DataRow
                CurrencyInfo modifyCurrency = new CurrencyInfo(currencyDr);

                // Update the properties
                modifyCurrency.CurrencyDisplayName = modifyCurrency.CurrencyDisplayName.ToUpper();

                // Update the currency
                CurrencyInfoProvider.SetCurrencyInfo(modifyCurrency);
            }

            return true;
        }

        return false;
    }


    /// <summary>
    /// Deletes currency. Called when the "Delete currency" button is pressed.
    /// Expects the CreateCurrency method to be run first.
    /// </summary>
    private bool DeleteCurrency()
    {
        // Get the currency
        CurrencyInfo deleteCurrency = CurrencyInfoProvider.GetCurrencyInfo("MyNewCurrency", CMSContext.CurrentSiteName);
        if (deleteCurrency != null)
        {
            // Delete the currency
            CurrencyInfoProvider.DeleteCurrencyInfo(deleteCurrency);

            return true;
        }

        return false;
    }

    #endregion


    #region "API examples - Exchange table"

    /// <summary>
    /// Creates exchange table. Called when the "Create table" button is pressed.
    /// </summary>
    private bool CreateExchangeTable()
    {
        // Create new exchange table object
        ExchangeTableInfo newTable = new ExchangeTableInfo();

        // Set the properties
        newTable.ExchangeTableDisplayName = "My new table";
        newTable.ExchangeTableSiteID = CMSContext.CurrentSiteID;
        newTable.ExchangeTableValidFrom = DateTime.Now;
        newTable.ExchangeTableValidTo = DateTime.Now;

        // Create the exchange table
        ExchangeTableInfoProvider.SetExchangeTableInfo(newTable);

        return true;
    }


    /// <summary>
    /// Gets and updates exchange table. Called when the "Get and update table" button is pressed.
    /// Expects the CreateExchangeTable method to be run first.
    /// </summary>
    private bool GetAndUpdateExchangeTable()
    {
        // Get the exchange table
        ExchangeTableInfo updateTable = ExchangeTableInfoProvider.GetExchangeTableInfo("My new table", CMSContext.CurrentSiteName);
        if (updateTable != null)
        {
            // Set time
            TimeSpan time = TimeSpan.FromDays(7);

            // Update the properties
            updateTable.ExchangeTableValidFrom = updateTable.ExchangeTableValidFrom.Add(time);
            updateTable.ExchangeTableValidTo = updateTable.ExchangeTableValidTo.Add(time);

            // Update the exchange table
            ExchangeTableInfoProvider.SetExchangeTableInfo(updateTable);

            return true;
        }

        return false;
    }


    /// <summary>
    /// Gets and bulk updates exchange tables. Called when the "Get and bulk update tables" button is pressed.
    /// Expects the CreateExchangeTable method to be run first.
    /// </summary>
    private bool GetAndBulkUpdateExchangeTables()
    {
        // Prepare the parameters
        string where = "ExchangeTableDisplayName LIKE N'My new table%'";
        where = SqlHelperClass.AddWhereCondition(where, "ExchangeTableSiteID = " + CMSContext.CurrentSiteID, "AND");

        // Get the data
        DataSet tables = ExchangeTableInfoProvider.GetExchangeTables(where, null);
        if (!DataHelper.DataSourceIsEmpty(tables))
        {
            // Set the time
            TimeSpan time = TimeSpan.FromDays(14);

            // Loop through the individual items
            foreach (DataRow tableDr in tables.Tables[0].Rows)
            {
                // Create object from DataRow
                ExchangeTableInfo modifyTable = new ExchangeTableInfo(tableDr);

                // Update the properties
                modifyTable.ExchangeTableValidFrom = modifyTable.ExchangeTableValidFrom.Add(time);
                modifyTable.ExchangeTableValidTo = modifyTable.ExchangeTableValidTo.Add(time);

                // Update the exchange table
                ExchangeTableInfoProvider.SetExchangeTableInfo(modifyTable);
            }

            return true;
        }

        return false;
    }


    /// <summary>
    /// Deletes exchange table. Called when the "Delete table" button is pressed.
    /// Expects the CreateExchangeTable method to be run first.
    /// </summary>
    private bool DeleteExchangeTable()
    {
        // Get the exchange table
        ExchangeTableInfo deleteTable = ExchangeTableInfoProvider.GetExchangeTableInfo("My new table", CMSContext.CurrentSiteName);

        // Delete the exchange table
        ExchangeTableInfoProvider.DeleteExchangeTableInfo(deleteTable);

        return (deleteTable != null);
    }

    #endregion


    #region "API examples - Order status"

    /// <summary>
    /// Creates order status. Called when the "Create status" button is pressed.
    /// </summary>
    private bool CreateOrderStatus()
    {
        // Create new order status object
        OrderStatusInfo newStatus = new OrderStatusInfo();

        // Set the properties
        newStatus.StatusDisplayName = "My new status";
        newStatus.StatusName = "MyNewStatus";
        newStatus.StatusEnabled = true;
        newStatus.StatusSiteID = CMSContext.CurrentSiteID;
        newStatus.StatusOrder = 1;

        // Create the order status
        OrderStatusInfoProvider.SetOrderStatusInfo(newStatus);

        return true;
    }


    /// <summary>
    /// Gets and updates order status. Called when the "Get and update status" button is pressed.
    /// Expects the CreateOrderStatus method to be run first.
    /// </summary>
    private bool GetAndUpdateOrderStatus()
    {
        // Get the order status
        OrderStatusInfo updateStatus = OrderStatusInfoProvider.GetOrderStatusInfo("MyNewStatus", CMSContext.CurrentSiteName);
        if (updateStatus != null)
        {
            // Update the properties
            updateStatus.StatusDisplayName = updateStatus.StatusDisplayName.ToLower();

            // Update the order status
            OrderStatusInfoProvider.SetOrderStatusInfo(updateStatus);

            return true;
        }

        return false;
    }


    /// <summary>
    /// Gets and bulk updates order statuses. Called when the "Get and bulk update statuses" button is pressed.
    /// Expects the CreateOrderStatus method to be run first.
    /// </summary>
    private bool GetAndBulkUpdateOrderStatuses()
    {
        // Prepare the parameters
        string where = "StatusName LIKE N'MyNewStatus%'";
        where = SqlHelperClass.AddWhereCondition(where, "StatusSiteID = " + CMSContext.CurrentSiteID, "AND");

        // Get the data
        DataSet statuses = OrderStatusInfoProvider.GetOrderStatuses(where, null, 0, null);
        if (!DataHelper.DataSourceIsEmpty(statuses))
        {
            // Loop through the individual items
            foreach (DataRow statusDr in statuses.Tables[0].Rows)
            {
                // Create object from DataRow
                OrderStatusInfo modifyStatus = new OrderStatusInfo(statusDr);

                // Update the properties
                modifyStatus.StatusDisplayName = modifyStatus.StatusDisplayName.ToUpper();

                // Update the order status
                OrderStatusInfoProvider.SetOrderStatusInfo(modifyStatus);
            }

            return true;
        }

        return false;
    }


    /// <summary>
    /// Deletes order status. Called when the "Delete status" button is pressed.
    /// Expects the CreateOrderStatus method to be run first.
    /// </summary>
    private bool DeleteOrderStatus()
    {
        // Get the order status
        OrderStatusInfo deleteStatus = OrderStatusInfoProvider.GetOrderStatusInfo("MyNewStatus", CMSContext.CurrentSiteName);

        // Delete the order status
        OrderStatusInfoProvider.DeleteOrderStatusInfo(deleteStatus);

        return (deleteStatus != null);
    }

    #endregion


    #region "API examples - Public status"

    /// <summary>
    /// Creates public status. Called when the "Create status" button is pressed.
    /// </summary>
    private bool CreatePublicStatus()
    {
        // Create new public status object
        PublicStatusInfo newStatus = new PublicStatusInfo();

        // Set the properties
        newStatus.PublicStatusDisplayName = "My new status";
        newStatus.PublicStatusName = "MyNewStatus";
        newStatus.PublicStatusEnabled = true;
        newStatus.PublicStatusSiteID = CMSContext.CurrentSiteID;

        // Create the public status
        PublicStatusInfoProvider.SetPublicStatusInfo(newStatus);

        return true;
    }


    /// <summary>
    /// Gets and updates public status. Called when the "Get and update status" button is pressed.
    /// Expects the CreatePublicStatus method to be run first.
    /// </summary>
    private bool GetAndUpdatePublicStatus()
    {
        // Get the public status
        PublicStatusInfo updateStatus = PublicStatusInfoProvider.GetPublicStatusInfo("MyNewStatus", CMSContext.CurrentSiteName);
        if (updateStatus != null)
        {
            // Update the properties
            updateStatus.PublicStatusDisplayName = updateStatus.PublicStatusDisplayName.ToLower();

            // Update the public status
            PublicStatusInfoProvider.SetPublicStatusInfo(updateStatus);

            return true;
        }

        return false;
    }


    /// <summary>
    /// Gets and bulk updates public statuses. Called when the "Get and bulk update statuses" button is pressed.
    /// Expects the CreatePublicStatus method to be run first.
    /// </summary>
    private bool GetAndBulkUpdatePublicStatuses()
    {
        // Prepare the parameters
        string where = "PublicStatusName LIKE N'MyNewStatus%'";
        where = SqlHelperClass.AddWhereCondition(where, "PublicStatusSiteID = " + CMSContext.CurrentSiteID, "AND");

        // Get the data
        DataSet statuses = PublicStatusInfoProvider.GetPublicStatuses(where, null);
        if (!DataHelper.DataSourceIsEmpty(statuses))
        {
            // Loop through the individual items
            foreach (DataRow statusDr in statuses.Tables[0].Rows)
            {
                // Create object from DataRow
                PublicStatusInfo modifyStatus = new PublicStatusInfo(statusDr);

                // Update the properties
                modifyStatus.PublicStatusDisplayName = modifyStatus.PublicStatusDisplayName.ToUpper();

                // Update the public status
                PublicStatusInfoProvider.SetPublicStatusInfo(modifyStatus);
            }

            return true;
        }

        return false;
    }


    /// <summary>
    /// Deletes public status. Called when the "Delete status" button is pressed.
    /// Expects the CreatePublicStatus method to be run first.
    /// </summary>
    private bool DeletePublicStatus()
    {
        // Get the public status
        PublicStatusInfo deleteStatus = PublicStatusInfoProvider.GetPublicStatusInfo("MyNewStatus", CMSContext.CurrentSiteName);

        // Delete the public status
        PublicStatusInfoProvider.DeletePublicStatusInfo(deleteStatus);

        return (deleteStatus != null);
    }

    #endregion


    #region "API examples - Internal status"

    /// <summary>
    /// Creates internal status. Called when the "Create status" button is pressed.
    /// </summary>
    private bool CreateInternalStatus()
    {
        // Create new internal status object
        InternalStatusInfo newStatus = new InternalStatusInfo();

        // Set the properties
        newStatus.InternalStatusDisplayName = "My new status";
        newStatus.InternalStatusName = "MyNewStatus";
        newStatus.InternalStatusEnabled = true;
        newStatus.InternalStatusSiteID = CMSContext.CurrentSiteID;

        // Create the internal status
        InternalStatusInfoProvider.SetInternalStatusInfo(newStatus);

        return true;
    }


    /// <summary>
    /// Gets and updates internal status. Called when the "Get and update status" button is pressed.
    /// Expects the CreateInternalStatus method to be run first.
    /// </summary>
    private bool GetAndUpdateInternalStatus()
    {
        // Get the internal status
        InternalStatusInfo updateStatus = InternalStatusInfoProvider.GetInternalStatusInfo("MyNewStatus", CMSContext.CurrentSiteName);
        if (updateStatus != null)
        {
            // Update the properties
            updateStatus.InternalStatusDisplayName = updateStatus.InternalStatusDisplayName.ToLower();

            // Update the internal status
            InternalStatusInfoProvider.SetInternalStatusInfo(updateStatus);

            return true;
        }

        return false;
    }


    /// <summary>
    /// Gets and bulk updates internal statuses. Called when the "Get and bulk update statuses" button is pressed.
    /// Expects the CreateInternalStatus method to be run first.
    /// </summary>
    private bool GetAndBulkUpdateInternalStatuses()
    {
        // Prepare the parameters
        string where = "InternalStatusName LIKE N'MyNewStatus%'";
        where = SqlHelperClass.AddWhereCondition(where, "InternalStatusSiteID = " + CMSContext.CurrentSiteID, "AND");

        // Get the data
        DataSet statuses = InternalStatusInfoProvider.GetInternalStatuses(where, null, 0, null);
        if (!DataHelper.DataSourceIsEmpty(statuses))
        {
            // Loop through the individual items
            foreach (DataRow statusDr in statuses.Tables[0].Rows)
            {
                // Create object from DataRow
                InternalStatusInfo modifyStatus = new InternalStatusInfo(statusDr);

                // Update the properties
                modifyStatus.InternalStatusDisplayName = modifyStatus.InternalStatusDisplayName.ToUpper();

                // Update the internal status
                InternalStatusInfoProvider.SetInternalStatusInfo(modifyStatus);
            }

            return true;
        }

        return false;
    }


    /// <summary>
    /// Deletes internal status. Called when the "Delete status" button is pressed.
    /// Expects the CreateInternalStatus method to be run first.
    /// </summary>
    private bool DeleteInternalStatus()
    {
        // Get the internal status
        InternalStatusInfo deleteStatus = InternalStatusInfoProvider.GetInternalStatusInfo("MyNewStatus", CMSContext.CurrentSiteName);

        // Delete the internal status
        InternalStatusInfoProvider.DeleteInternalStatusInfo(deleteStatus);

        return (deleteStatus != null);
    }

    #endregion


    #region "API examples - Department"

    /// <summary>
    /// Creates department. Called when the "Create department" button is pressed.
    /// </summary>
    private bool CreateDepartment()
    {
        // Create new department object
        DepartmentInfo newDepartment = new DepartmentInfo();

        // Set the properties
        newDepartment.DepartmentDisplayName = "My new department";
        newDepartment.DepartmentName = "MyNewDepartment";
        newDepartment.DepartmentSiteID = CMSContext.CurrentSiteID;

        // Create the department
        DepartmentInfoProvider.SetDepartmentInfo(newDepartment);

        return true;
    }


    /// <summary>
    /// Gets and updates department. Called when the "Get and update department" button is pressed.
    /// Expects the CreateDepartment method to be run first.
    /// </summary>
    private bool GetAndUpdateDepartment()
    {
        // Get the department
        DepartmentInfo updateDepartment = DepartmentInfoProvider.GetDepartmentInfo("MyNewDepartment", CMSContext.CurrentSiteName);
        if (updateDepartment != null)
        {
            // Update the properties
            updateDepartment.DepartmentDisplayName = updateDepartment.DepartmentDisplayName.ToLower();

            // Update the department
            DepartmentInfoProvider.SetDepartmentInfo(updateDepartment);

            return true;
        }

        return false;
    }


    /// <summary>
    /// Gets and bulk updates departments. Called when the "Get and bulk update departments" button is pressed.
    /// Expects the CreateDepartment method to be run first.
    /// </summary>
    private bool GetAndBulkUpdateDepartments()
    {
        // Prepare the parameters
        string where = "DepartmentName LIKE N'MyNewDepartment%'";

        // Get the data
        DataSet departments = DepartmentInfoProvider.GetDepartments(where, null);
        if (!DataHelper.DataSourceIsEmpty(departments))
        {
            // Loop through the individual items
            foreach (DataRow departmentDr in departments.Tables[0].Rows)
            {
                // Create object from DataRow
                DepartmentInfo modifyDepartment = new DepartmentInfo(departmentDr);

                // Update the properties
                modifyDepartment.DepartmentDisplayName = modifyDepartment.DepartmentDisplayName.ToUpper();

                // Update the department
                DepartmentInfoProvider.SetDepartmentInfo(modifyDepartment);
            }

            return true;
        }

        return false;
    }


    /// <summary>
    /// Deletes department. Called when the "Delete department" button is pressed.
    /// Expects the CreateDepartment method to be run first.
    /// </summary>
    private bool DeleteDepartment()
    {
        // Get the department
        DepartmentInfo deleteDepartment = DepartmentInfoProvider.GetDepartmentInfo("MyNewDepartment", CMSContext.CurrentSiteName);

        // Delete the department
        DepartmentInfoProvider.DeleteDepartmentInfo(deleteDepartment);

        return (deleteDepartment != null);
    }

    #endregion


    #region "API examples - Department user"

    /// <summary>
    /// Adds user to department. Called when the "Add user to department" button is pressed.
    /// Expects the CreateDepartment method to be run first.
    /// </summary>
    private bool AddUserToDepartment()
    {
        // Get the department
        DepartmentInfo department = DepartmentInfoProvider.GetDepartmentInfo("MyNewDepartment", CMSContext.CurrentSiteName);
        if (department != null)
        {
            // Add user to department
            UserDepartmentInfoProvider.AddUserToDepartment(department.DepartmentID, CMSContext.CurrentUser.UserID);

            return true;
        }

        return false;
    }


    /// <summary>
    /// Removes user from department. Called when the "Remove user from department" button is pressed.
    /// Expects the AddUserToDepartment method to be run first.
    /// </summary>
    private bool RemoveUserFromDepartment()
    {
        // Get the department
        DepartmentInfo department = DepartmentInfoProvider.GetDepartmentInfo("MyNewDepartment", CMSContext.CurrentSiteName);
        if (department != null)
        {
            // Get the user department
            UserDepartmentInfo deleteUserDepartment = UserDepartmentInfoProvider.GetUserDepartmentInfo(department.DepartmentID, CMSContext.CurrentUser.UserID);

            if (deleteUserDepartment != null)
            {
                // Remove user from department
                UserDepartmentInfoProvider.DeleteUserDepartmentInfo(deleteUserDepartment);

                return true;
            }
        }

        return false;
    }

    #endregion


    #region "API examples - Department default tax class"

    /// <summary>
    /// Adds tax class to department. Called when the "Add tax class to department" button is pressed.
    /// Expects CreateDepartment and CreateTaxClass methods to be run first.
    /// </summary>
    private bool AddTaxClassToDepartment()
    {
        // Get the department
        DepartmentInfo department = DepartmentInfoProvider.GetDepartmentInfo("MyNewDepartment", CMSContext.CurrentSiteName);

        // Get the tax class
        TaxClassInfo taxClass = TaxClassInfoProvider.GetTaxClassInfo("MyNewClass", CMSContext.CurrentSiteName);

        if ((department != null) && (taxClass != null))
        {
            // Add tax class to department
            DepartmentTaxClassInfoProvider.AddTaxClassToDepartment(taxClass.TaxClassID, department.DepartmentID);

            return true;
        }

        return false;
    }


    /// <summary>
    /// Removes tax class from department. Called when the "Remove user from department" button is pressed.
    /// Expects the AddUserToDepartment method to be run first.
    /// </summary>
    private bool RemoveTaxClassFromDepartment()
    {
        // Get the department
        DepartmentInfo department = DepartmentInfoProvider.GetDepartmentInfo("MyNewDepartment", CMSContext.CurrentSiteName);

        // Get the tax class
        TaxClassInfo taxClass = TaxClassInfoProvider.GetTaxClassInfo("MyNewClass", CMSContext.CurrentSiteName);

        if ((department != null) && (taxClass != null))
        {
            // Get the department tax class
            DepartmentTaxClassInfo departmentTax = DepartmentTaxClassInfoProvider.GetDepartmentTaxClassInfo(department.DepartmentID, taxClass.TaxClassID);

            if ((departmentTax != null))
            {
                // Remove tax class from department
                DepartmentTaxClassInfoProvider.DeleteDepartmentTaxClassInfo(departmentTax);

                return true;
            }
        }

        return false;
    }

    #endregion


    #region "API examples - Shipping option"

    /// <summary>
    /// Creates shipping option. Called when the "Create option" button is pressed.
    /// </summary>
    private bool CreateShippingOption()
    {
        // Create new shipping option object
        ShippingOptionInfo newOption = new ShippingOptionInfo();

        // Set the properties
        newOption.ShippingOptionDisplayName = "My new option";
        newOption.ShippingOptionName = "MyNewOption";
        newOption.ShippingOptionSiteID = CMSContext.CurrentSiteID;
        newOption.ShippingOptionCharge = 1.0;
        newOption.ShippingOptionEnabled = true;

        // Create the shipping option
        ShippingOptionInfoProvider.SetShippingOptionInfo(newOption);

        return true;
    }


    /// <summary>
    /// Gets and updates shipping option. Called when the "Get and update option" button is pressed.
    /// Expects the CreateShippingOption method to be run first.
    /// </summary>
    private bool GetAndUpdateShippingOption()
    {
        // Get the shipping option
        ShippingOptionInfo updateOption = ShippingOptionInfoProvider.GetShippingOptionInfo("MyNewOption", CMSContext.CurrentSiteName);
        if (updateOption != null)
        {
            // Update the properties
            updateOption.ShippingOptionDisplayName = updateOption.ShippingOptionDisplayName.ToLower();

            // Update the shipping option
            ShippingOptionInfoProvider.SetShippingOptionInfo(updateOption);

            return true;
        }

        return false;
    }


    /// <summary>
    /// Gets and bulk updates shipping options. Called when the "Get and bulk update options" button is pressed.
    /// Expects the CreateShippingOption method to be run first.
    /// </summary>
    private bool GetAndBulkUpdateShippingOptions()
    {
        // Prepare the parameters
        string where = "ShippingOptionName LIKE N'MyNewOption%'";
        where = SqlHelperClass.AddWhereCondition(where, "ShippingOptionSiteID = " + CMSContext.CurrentSiteID, "AND");

        // Get the data
        DataSet options = ShippingOptionInfoProvider.GetShippingOptions(where, null);
        if (!DataHelper.DataSourceIsEmpty(options))
        {
            // Loop through the individual items
            foreach (DataRow optionDr in options.Tables[0].Rows)
            {
                // Create object from DataRow
                ShippingOptionInfo modifyOption = new ShippingOptionInfo(optionDr);

                // Update the properties
                modifyOption.ShippingOptionDisplayName = modifyOption.ShippingOptionDisplayName.ToUpper();

                // Update the shipping option
                ShippingOptionInfoProvider.SetShippingOptionInfo(modifyOption);
            }

            return true;
        }

        return false;
    }


    /// <summary>
    /// Deletes shipping option. Called when the "Delete option" button is pressed.
    /// Expects the CreateShippingOption method to be run first.
    /// </summary>
    private bool DeleteShippingOption()
    {
        // Get the shipping option
        ShippingOptionInfo deleteOption = ShippingOptionInfoProvider.GetShippingOptionInfo("MyNewOption", CMSContext.CurrentSiteName);

        if (deleteOption != null)
        {
            // Delete the shipping option
            ShippingOptionInfoProvider.DeleteShippingOptionInfo(deleteOption);

            return true;
        }

        return false;
    }

    #endregion


    #region "API examples - Payment method"

    /// <summary>
    /// Creates payment method. Called when the "Create method" button is pressed.
    /// </summary>
    private bool CreatePaymentMethod()
    {
        // Create new payment method object
        PaymentOptionInfo newMethod = new PaymentOptionInfo();

        // Set the properties
        newMethod.PaymentOptionDisplayName = "My new method";
        newMethod.PaymentOptionName = "MyNewMethod";
        newMethod.PaymentOptionSiteID = CMSContext.CurrentSiteID;
        newMethod.PaymentOptionEnabled = true;

        // Create the payment method
        PaymentOptionInfoProvider.SetPaymentOptionInfo(newMethod);

        return true;
    }


    /// <summary>
    /// Gets and updates payment method. Called when the "Get and update method" button is pressed.
    /// Expects the CreatePaymentMethod method to be run first.
    /// </summary>
    private bool GetAndUpdatePaymentMethod()
    {
        // Get the payment method
        PaymentOptionInfo updateMethod = PaymentOptionInfoProvider.GetPaymentOptionInfo("MyNewMethod", CMSContext.CurrentSiteName);
        if (updateMethod != null)
        {
            // Update the properties
            updateMethod.PaymentOptionDisplayName = updateMethod.PaymentOptionDisplayName.ToLower();

            // Update the payment method
            PaymentOptionInfoProvider.SetPaymentOptionInfo(updateMethod);

            return true;
        }

        return false;
    }


    /// <summary>
    /// Gets and bulk updates payment methods. Called when the "Get and bulk update methods" button is pressed.
    /// Expects the CreatePaymentMethod method to be run first.
    /// </summary>
    private bool GetAndBulkUpdatePaymentMethods()
    {
        // Prepare the parameters
        string where = "PaymentOptionName LIKE N'MyNewMethod%'";
        where = SqlHelperClass.AddWhereCondition(where, "PaymentOptionSiteID = " + CMSContext.CurrentSiteID, "AND");

        // Get the data
        DataSet methods = PaymentOptionInfoProvider.GetPaymentOptions(where, null);
        if (!DataHelper.DataSourceIsEmpty(methods))
        {
            // Loop through the individual items
            foreach (DataRow methodDr in methods.Tables[0].Rows)
            {
                // Create object from DataRow
                PaymentOptionInfo modifyMethod = new PaymentOptionInfo(methodDr);

                // Update the properties
                modifyMethod.PaymentOptionDisplayName = modifyMethod.PaymentOptionDisplayName.ToUpper();

                // Update the payment method
                PaymentOptionInfoProvider.SetPaymentOptionInfo(modifyMethod);
            }

            return true;
        }

        return false;
    }


    /// <summary>
    /// Deletes payment method. Called when the "Delete method" button is pressed.
    /// Expects the CreatePaymentMethod method to be run first.
    /// </summary>
    private bool DeletePaymentMethod()
    {
        // Get the payment method
        PaymentOptionInfo deleteMethod = PaymentOptionInfoProvider.GetPaymentOptionInfo("MyNewMethod", CMSContext.CurrentSiteName);
        if (deleteMethod != null)
        {
            // Delete the payment method
            PaymentOptionInfoProvider.DeletePaymentOptionInfo(deleteMethod.PaymentOptionID);

            return true;
        }

        return false;
    }

    #endregion


    #region "API examples - Manufacturer"

    /// <summary>
    /// Creates manufacturer. Called when the "Create manufacturer" button is pressed.
    /// </summary>
    private bool CreateManufacturer()
    {
        // Create new manufacturer object
        ManufacturerInfo newManufacturer = new ManufacturerInfo();

        // Set the properties
        newManufacturer.ManufacturerDisplayName = "My new manufacturer";
        newManufacturer.ManufactureHomepage = "www.mynewmanufacturer.com";
        newManufacturer.ManufacturerSiteID = CMSContext.CurrentSiteID;
        newManufacturer.ManufacturerEnabled = true;

        // Create the manufacturer
        ManufacturerInfoProvider.SetManufacturerInfo(newManufacturer);

        return true;
    }


    /// <summary>
    /// Gets and updates manufacturer. Called when the "Get and update manufacturer" button is pressed.
    /// Expects the CreateManufacturer method to be run first.
    /// </summary>
    private bool GetAndUpdateManufacturer()
    {
        // Get the manufacturer
        ManufacturerInfo updateManufacturer = ManufacturerInfoProvider.GetManufacturerInfo("My new manufacturer", CMSContext.CurrentSiteName);
        if (updateManufacturer != null)
        {
            // Update the properties
            updateManufacturer.ManufacturerDisplayName = updateManufacturer.ManufacturerDisplayName.ToLower();

            // Update the manufacturer
            ManufacturerInfoProvider.SetManufacturerInfo(updateManufacturer);

            return true;
        }

        return false;
    }


    /// <summary>
    /// Gets and bulk updates manufacturers. Called when the "Get and bulk update manufacturers" button is pressed.
    /// Expects the CreateManufacturer method to be run first.
    /// </summary>
    private bool GetAndBulkUpdateManufacturers()
    {
        // Prepare the parameters
        string where = "ManufacturerDisplayName LIKE N'My new manufacturer%'";
        where = SqlHelperClass.AddWhereCondition(where, "ManufacturerSiteID = " + CMSContext.CurrentSiteID, "AND");

        // Get the data
        DataSet manufacturers = ManufacturerInfoProvider.GetManufacturers(where, null);
        if (!DataHelper.DataSourceIsEmpty(manufacturers))
        {
            // Loop through the individual items
            foreach (DataRow manufacturerDr in manufacturers.Tables[0].Rows)
            {
                // Create object from DataRow
                ManufacturerInfo modifyManufacturer = new ManufacturerInfo(manufacturerDr);

                // Update the properties
                modifyManufacturer.ManufacturerDisplayName = modifyManufacturer.ManufacturerDisplayName.ToUpper();

                // Update the manufacturer
                ManufacturerInfoProvider.SetManufacturerInfo(modifyManufacturer);
            }

            return true;
        }

        return false;
    }


    /// <summary>
    /// Deletes manufacturer. Called when the "Delete manufacturer" button is pressed.
    /// Expects the CreateManufacturer method to be run first.
    /// </summary>
    private bool DeleteManufacturer()
    {
        // Get the manufacturer
        ManufacturerInfo deleteManufacturer = ManufacturerInfoProvider.GetManufacturerInfo("My new manufacturer", CMSContext.CurrentSiteName);

        // Delete the manufacturer
        ManufacturerInfoProvider.DeleteManufacturerInfo(deleteManufacturer);

        return (deleteManufacturer != null);
    }

    #endregion


    #region "API examples - Supplier"

    /// <summary>
    /// Creates supplier. Called when the "Create supplier" button is pressed.
    /// </summary>
    private bool CreateSupplier()
    {
        // Create new supplier object
        SupplierInfo newSupplier = new SupplierInfo();

        // Set the properties
        newSupplier.SupplierDisplayName = "My new supplier";
        newSupplier.SupplierEmail = "mynewsupplier@supplier.com";
        newSupplier.SupplierSiteID = CMSContext.CurrentSiteID;
        newSupplier.SupplierPhone = "";
        newSupplier.SupplierFax = "";
        newSupplier.SupplierEnabled = true;

        // Create the supplier
        SupplierInfoProvider.SetSupplierInfo(newSupplier);

        return true;
    }


    /// <summary>
    /// Gets and updates supplier. Called when the "Get and update supplier" button is pressed.
    /// Expects the CreateSupplier method to be run first.
    /// </summary>
    private bool GetAndUpdateSupplier()
    {
        // Get the supplier
        SupplierInfo updateSupplier = SupplierInfoProvider.GetSupplierInfo("My new supplier", CMSContext.CurrentSiteName);
        if (updateSupplier != null)
        {
            // Update the properties
            updateSupplier.SupplierDisplayName = updateSupplier.SupplierDisplayName.ToLower();

            // Update the supplier
            SupplierInfoProvider.SetSupplierInfo(updateSupplier);

            return true;
        }

        return false;
    }


    /// <summary>
    /// Gets and bulk updates suppliers. Called when the "Get and bulk update suppliers" button is pressed.
    /// Expects the CreateSupplier method to be run first.
    /// </summary>
    private bool GetAndBulkUpdateSuppliers()
    {
        // Prepare the parameters
        string where = "SupplierDisplayName LIKE N'My new supplier%'";
        where = SqlHelperClass.AddWhereCondition(where, "SupplierSiteID = " + CMSContext.CurrentSiteID, "AND");

        // Get the data
        DataSet suppliers = SupplierInfoProvider.GetSuppliers(where, null);
        if (!DataHelper.DataSourceIsEmpty(suppliers))
        {
            // Loop through the individual items
            foreach (DataRow supplierDr in suppliers.Tables[0].Rows)
            {
                // Create object from DataRow
                SupplierInfo modifySupplier = new SupplierInfo(supplierDr);

                // Update the properties
                modifySupplier.SupplierDisplayName = modifySupplier.SupplierDisplayName.ToUpper();

                // Update the supplier
                SupplierInfoProvider.SetSupplierInfo(modifySupplier);
            }

            return true;
        }

        return false;
    }


    /// <summary>
    /// Deletes supplier. Called when the "Delete supplier" button is pressed.
    /// Expects the CreateSupplier method to be run first.
    /// </summary>
    private bool DeleteSupplier()
    {
        // Get the supplier
        SupplierInfo deleteSupplier = SupplierInfoProvider.GetSupplierInfo("My new supplier", CMSContext.CurrentSiteName);

        // Delete the supplier
        SupplierInfoProvider.DeleteSupplierInfo(deleteSupplier);

        return (deleteSupplier != null);
    }

    #endregion


    #region "API examples - Product"

    /// <summary>
    /// Creates product. Called when the "Create product" button is pressed.
    /// </summary>
    private bool CreateProduct()
    {
        // Get the deparment
        DepartmentInfo department = DepartmentInfoProvider.GetDepartmentInfo("MyNewDepartment", CMSContext.CurrentSiteName);

        if (department != null)
        {
            // Create new product object
            SKUInfo newProduct = new SKUInfo();

            // Set the properties
            newProduct.SKUName = "MyNewProduct";
            newProduct.SKUPrice = 120;
            newProduct.SKUEnabled = true;
            newProduct.SKUDepartmentID = department.DepartmentID;
            newProduct.SKUSiteID = CMSContext.CurrentSiteID;

            // Create the product
            SKUInfoProvider.SetSKUInfo(newProduct);

            return true;
        }

        return false;
    }


    /// <summary>
    /// Gets and updates product. Called when the "Get and update product" button is pressed.
    /// Expects the CreateProduct method to be run first.
    /// </summary>
    private bool GetAndUpdateProduct()
    {
        // Prepare the parameters
        string where = "SKUName LIKE N'MyNewProduct%' AND SKUOptionCategoryID IS NULL";

        // Get the product
        DataSet products = SKUInfoProvider.GetSKUs(where, null);
        if (!DataHelper.DataSourceIsEmpty(products))
        {
            // Create object from DataRow
            SKUInfo updateProduct = new SKUInfo(products.Tables[0].Rows[0]);

            // Update the product name
            updateProduct.SKUName = updateProduct.SKUName.ToLower();

            // Update the product
            SKUInfoProvider.SetSKUInfo(updateProduct);

            return true;
        }

        return false;
    }


    /// <summary>
    /// Gets and bulk updates products. Called when the "Get and bulk update products" button is pressed.
    /// Expects the CreateProduct method to be run first.
    /// </summary>
    private bool GetAndBulkUpdateProducts()
    {
        // Prepare the parameters
        string where = "SKUName LIKE N'MyNewProduct%' AND SKUOptionCategoryID IS NULL";

        // Get the data
        DataSet products = SKUInfoProvider.GetSKUs(where, null);
        if (!DataHelper.DataSourceIsEmpty(products))
        {
            // Loop through the individual items
            foreach (DataRow productDr in products.Tables[0].Rows)
            {
                // Create object from DataRow
                SKUInfo modifyProduct = new SKUInfo(productDr);

                // Update the product name
                modifyProduct.SKUName = modifyProduct.SKUName.ToUpper();

                // Update the product
                SKUInfoProvider.SetSKUInfo(modifyProduct);
            }

            return true;
        }

        return false;
    }


    /// <summary>
    /// Deletes product. Called when the "Delete product" button is pressed.
    /// Expects the CreateProduct method to be run first.
    /// </summary>
    private bool DeleteProduct()
    {
        // Prepare the parameters
        string where = "SKUName LIKE N'MyNewProduct%' AND SKUOptionCategoryID IS NULL";

        // Get the data
        DataSet products = SKUInfoProvider.GetSKUs(where, null);
        if (!DataHelper.DataSourceIsEmpty(products))
        {
            // Create object from DataRow
            SKUInfo deleteProduct = new SKUInfo(products.Tables[0].Rows[0]);

            // Delete the product
            SKUInfoProvider.DeleteSKUInfo(deleteProduct);

            return true;
        }

        return false;
    }

    #endregion


    #region "API examples - Product document"

    /// <summary>
    /// Creates product document. Called when the "Create document" button is pressed.
    /// Expects the CreateProduct method to be run first.
    /// </summary>
    private bool CreateProductDocument()
    {
        // Prepare the parameters
        string where = "SKUName LIKE N'MyNewProduct%'";
        SKUInfo product = null;

        // Get the product
        DataSet products = SKUInfoProvider.GetSKUs(where, null);
        if (!DataHelper.DataSourceIsEmpty(products))
        {
            // Create object from DataRow
            product = new SKUInfo(products.Tables[0].Rows[0]);
        }

        TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);

        // Get the root document
        TreeNode root = tree.SelectSingleNode(CMSContext.CurrentSiteName, "/", null, true);

        if ((root != null) && (product != null))
        {
            // Create a new product document 
            TreeNode node = TreeNode.New("CMS.Product", tree);

            // Set the document properties
            node.DocumentName = "MyNewProduct";
            node.DocumentCulture = CMSContext.PreferredCultureCode;
            node.SetValue("ProductName", "MyNewProduct");

            // Assign product to document
            node.NodeSKUID = product.SKUID;

            // Save the product document
            DocumentHelper.InsertDocument(node, root.NodeID, tree);

            return true;
        }

        return false;
    }


    /// <summary>
    /// Gets and updates product document. Called when the "Get and update document" button is pressed.
    /// Expects the CreateProductDocument method to be run first.
    /// </summary>
    private bool GetAndUpdateProductDocument()
    {
        TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);

        // Get the product document
        TreeNode node = tree.SelectSingleNode(CMSContext.CurrentSiteName, "/MyNewProduct", null, true);

        if (node != null)
        {
            // Set the properties
            node.SetValue("ProductDescription", "Product was updated.");
            node.DocumentName = node.DocumentName.ToLower();

            // Update the product document
            DocumentHelper.UpdateDocument(node, tree);

            return true;
        }

        return false;
    }


    /// <summary>
    /// Deletes product document. Called when the "Delete document" button is pressed.
    /// Expects the CreateProductDocument method to be run first.
    /// </summary>
    private bool DeleteProductDocument()
    {
        TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);

        // Get the product document
        TreeNode node = tree.SelectSingleNode(CMSContext.CurrentSiteName, "/MyNewProduct", null, true);

        if (node != null)
        {
            // Delete the product document
            DocumentHelper.DeleteDocument(node, tree, true, true, false);

            return true;
        }

        return false;
    }

    #endregion


    #region "API examples - Product tax class"

    /// <summary>
    /// Adds tax class to product. Called when the "Create class" button is pressed.
    /// Expects the CreateTaxClass and CreateProduct methods to be run first.
    /// </summary>
    private bool AddTaxClassToProduct()
    {
        // Prepare the parameters
        string where = "SKUName LIKE N'MyNew%'";
        SKUInfo product = null;

        // Get the product
        DataSet products = SKUInfoProvider.GetSKUs(where, null);
        if (!DataHelper.DataSourceIsEmpty(products))
        {
            product = new SKUInfo(products.Tables[0].Rows[0]);
        }

        // Get the tax class
        TaxClassInfo taxClass = TaxClassInfoProvider.GetTaxClassInfo("MyNewClass", CMSContext.CurrentSiteName);

        if ((product != null) && (taxClass != null))
        {
            // Add tax class to product
            SKUTaxClassInfoProvider.AddTaxClassToSKU(taxClass.TaxClassID, product.SKUID);

            return true;
        }

        return false;
    }


    /// <summary>
    /// Removes tax class from product. Called when the "Remove tax class from product" button is pressed.
    /// Expects the AddTaxClassToProduct method to be run first.
    /// </summary>
    private bool RemoveTaxClassFromProduct()
    {
        // Prepare the parameters
        string where = "SKUName LIKE N'MyNew%'";
        SKUInfo product = null;

        // Get the tax class
        TaxClassInfo taxClass = TaxClassInfoProvider.GetTaxClassInfo("MyNewClass", CMSContext.CurrentSiteName);

        // Get the product
        DataSet products = SKUInfoProvider.GetSKUs(where, null);
        if (!DataHelper.DataSourceIsEmpty(products))
        {
            product = new SKUInfo(products.Tables[0].Rows[0]);
        }

        if ((product != null) && (taxClass != null))
        {
            // Get the tax class added to product
            SKUTaxClassInfo skuTaxClass = SKUTaxClassInfoProvider.GetSKUTaxClassInfo(taxClass.TaxClassID, product.SKUID);

            // Remove tax class from product
            SKUTaxClassInfoProvider.DeleteSKUTaxClassInfo(skuTaxClass);

            return (skuTaxClass != null);
        }

        return false;
    }

    #endregion


    #region "API examples - Volume discount"

    /// <summary>
    /// Creates volume discount. Called when the "Create discount" button is pressed.
    /// Expects the CreateProduct method to be run first.
    /// </summary>
    private bool CreateVolumeDiscount()
    {
        // Prepare the parameters
        string where = "SKUName LIKE N'MyNew%'";
        SKUInfo product = null;

        // Get the product
        DataSet products = SKUInfoProvider.GetSKUs(where, null);
        if (!DataHelper.DataSourceIsEmpty(products))
        {
            product = new SKUInfo(products.Tables[0].Rows[0]);
        }

        if (product != null)
        {
            // Create new volume discount object
            VolumeDiscountInfo newDiscount = new VolumeDiscountInfo();

            // Set the properties
            newDiscount.VolumeDiscountMinCount = 100;
            newDiscount.VolumeDiscountValue = 20;
            newDiscount.VolumeDiscountSKUID = product.SKUID;
            newDiscount.VolumeDiscountIsFlatValue = false;

            // Create the volume discount
            VolumeDiscountInfoProvider.SetVolumeDiscountInfo(newDiscount);

            return true;
        }

        return false;
    }


    /// <summary>
    /// Gets and updates volume discount. Called when the "Get and update discount" button is pressed.
    /// Expects the CreateVolumeDiscount method to be run first.
    /// </summary>
    private bool GetAndUpdateVolumeDiscount()
    {
        // Prepare the parameters
        string where = "SKUName LIKE N'MyNew%'";
        SKUInfo product = null;

        // Get the product
        DataSet products = SKUInfoProvider.GetSKUs(where, null);
        if (!DataHelper.DataSourceIsEmpty(products))
        {
            product = new SKUInfo(products.Tables[0].Rows[0]);
        }

        if (product != null)
        {
            // Get the volume discount
            DataSet discounts = VolumeDiscountInfoProvider.GetVolumeDiscounts(product.SKUID);
            if (!DataHelper.DataSourceIsEmpty(discounts))
            {
                // Create object from DataRow
                VolumeDiscountInfo discount = new VolumeDiscountInfo(discounts.Tables[0].Rows[0]);

                // Update the value
                discount.VolumeDiscountMinCount = 800;

                // Update the volume discount
                VolumeDiscountInfoProvider.SetVolumeDiscountInfo(discount);

                return true;
            }
        }

        return false;
    }


    /// <summary>
    /// Gets and bulk updates volume discounts. Called when the "Get and bulk update discounts" button is pressed.
    /// Expects the CreateVolumeDiscount method to be run first.
    /// </summary>
    private bool GetAndBulkUpdateVolumeDiscounts()
    {
        // Prepare the parameters
        string where = "SKUName LIKE N'MyNew%'";
        SKUInfo product = null;

        // Get the product
        DataSet products = SKUInfoProvider.GetSKUs(where, null);
        if (!DataHelper.DataSourceIsEmpty(products))
        {
            product = new SKUInfo(products.Tables[0].Rows[0]);
        }

        if (product != null)
        {
            // Get the volume discount
            DataSet discounts = VolumeDiscountInfoProvider.GetVolumeDiscounts(product.SKUID);
            if (!DataHelper.DataSourceIsEmpty(discounts))
            {
                foreach (DataRow discountDr in discounts.Tables[0].Rows)
                {
                    // Create object from DataRow
                    VolumeDiscountInfo discount = new VolumeDiscountInfo(discountDr);

                    // Update the value
                    discount.VolumeDiscountMinCount = 500;

                    // Update the volume discount
                    VolumeDiscountInfoProvider.SetVolumeDiscountInfo(discount);
                }

                return true;
            }
        }

        return false;
    }


    /// <summary>
    /// Deletes volume discount. Called when the "Delete discount" button is pressed.
    /// Expects the CreateVolumeDiscount method to be run first.
    /// </summary>
    private bool DeleteVolumeDiscount()
    {
        // Prepare the parameters
        string where = "SKUName LIKE N'MyNew%'";
        SKUInfo product = null;

        // Get the product
        DataSet products = SKUInfoProvider.GetSKUs(where, null);
        if (!DataHelper.DataSourceIsEmpty(products))
        {
            product = new SKUInfo(products.Tables[0].Rows[0]);
        }

        if (product != null)
        {
            // Get the volume discount
            DataSet discounts = VolumeDiscountInfoProvider.GetVolumeDiscounts(product.SKUID);
            if (!DataHelper.DataSourceIsEmpty(discounts))
            {
                // Create object from DataRow
                VolumeDiscountInfo discount = new VolumeDiscountInfo(discounts.Tables[0].Rows[0]);

                // Delete the volume discount
                VolumeDiscountInfoProvider.DeleteVolumeDiscountInfo(discount);

                return true;
            }
        }

        return false;
    }

    #endregion


    #region "API examples - Product option category"

    /// <summary>
    /// Creates option category. Called when the "Create category" button is pressed.
    /// </summary>
    private bool CreateOptionCategory()
    {
        // Create new option category object
        OptionCategoryInfo newCategory = new OptionCategoryInfo();

        // Set the properties
        newCategory.CategoryDisplayName = "My new category";
        newCategory.CategoryName = "MyNewCategory";
        newCategory.CategorySelectionType = OptionCategorySelectionTypeEnum.Dropdownlist;
        newCategory.CategoryDisplayPrice = true;
        newCategory.CategoryEnabled = true;
        newCategory.CategoryDefaultRecord = "";
        newCategory.CategorySiteID = CMSContext.CurrentSiteID;

        // Create the option category
        OptionCategoryInfoProvider.SetOptionCategoryInfo(newCategory);

        return true;
    }


    /// <summary>
    /// Gets and updates option category. Called when the "Get and update category" button is pressed.
    /// Expects the CreateOptionCategory method to be run first.
    /// </summary>
    private bool GetAndUpdateOptionCategory()
    {
        // Get the option category
        OptionCategoryInfo updateCategory = OptionCategoryInfoProvider.GetOptionCategoryInfo("MyNewCategory", CMSContext.CurrentSiteName);
        if (updateCategory != null)
        {
            // Update the properties
            updateCategory.CategoryDisplayName = updateCategory.CategoryDisplayName.ToLower();

            // Update the option category
            OptionCategoryInfoProvider.SetOptionCategoryInfo(updateCategory);

            return true;
        }

        return false;
    }


    /// <summary>
    /// Gets and bulk updates option categories. Called when the "Get and bulk update categories" button is pressed.
    /// Expects the CreateOptionCategory method to be run first.
    /// </summary>
    private bool GetAndBulkUpdateOptionCategories()
    {
        // Prepare the parameters
        string where = "CategoryName LIKE N'MyNewCategory%'";

        // Get the data
        DataSet categories = OptionCategoryInfoProvider.GetOptionCategories(where, null);
        if (!DataHelper.DataSourceIsEmpty(categories))
        {
            // Loop through the individual items
            foreach (DataRow categoryDr in categories.Tables[0].Rows)
            {
                // Create object from DataRow
                OptionCategoryInfo modifyCategory = new OptionCategoryInfo(categoryDr);

                // Update the properties
                modifyCategory.CategoryDisplayName = modifyCategory.CategoryDisplayName.ToUpper();

                // Update the option category
                OptionCategoryInfoProvider.SetOptionCategoryInfo(modifyCategory);
            }

            return true;
        }

        return false;
    }


    /// <summary>
    /// Deletes option category. Called when the "Delete category" button is pressed.
    /// Expects the CreateOptionCategory method to be run first.
    /// </summary>
    private bool DeleteOptionCategory()
    {
        // Get the option category
        OptionCategoryInfo deleteCategory = OptionCategoryInfoProvider.GetOptionCategoryInfo("MyNewCategory", CMSContext.CurrentSiteName);

        // Delete the option category
        OptionCategoryInfoProvider.DeleteOptionCategoryInfo(deleteCategory);

        return (deleteCategory != null);
    }

    #endregion


    #region "API examples - Option"

    /// <summary>
    /// Creates option. Called when the "Create option" button is pressed.
    /// Expects the CreateOptionCategory method to be run first.
    /// </summary>
    private bool CreateOption()
    {
        // Get the department
        DepartmentInfo department = DepartmentInfoProvider.GetDepartmentInfo("MyNewDepartment", CMSContext.CurrentSiteName);

        // Get the option category
        OptionCategoryInfo category = OptionCategoryInfoProvider.GetOptionCategoryInfo("MyNewCategory", CMSContext.CurrentSiteName);

        if ((department != null) && (category != null))
        {
            // Create new product option object
            SKUInfo newOption = new SKUInfo();

            // Set the properties
            newOption.SKUName = "MyNewProductOption";
            newOption.SKUPrice = 199;
            newOption.SKUEnabled = true;
            newOption.SKUDepartmentID = department.DepartmentID;
            newOption.SKUOptionCategoryID = category.CategoryID;
            newOption.SKUSiteID = CMSContext.CurrentSiteID;
            newOption.SKUProductType = SKUProductTypeEnum.Product;

            // Create the product option
            SKUInfoProvider.SetSKUInfo(newOption);

            return true;
        }

        return false;
    }


    /// <summary>
    /// Gets and updates option. Called when the "Get and update option" button is pressed.
    /// Expects the CreateOption method to be run first.
    /// </summary>
    private bool GetAndUpdateOption()
    {
        string where = "SKUName LIKE N'MyNewProduct%' AND SKUOptionCategoryID IS NOT NULL";

        // Get the product option
        DataSet options = SKUInfoProvider.GetSKUs(where, null);
        if (!DataHelper.DataSourceIsEmpty(options))
        {
            // Create object from DataRow
            SKUInfo option = new SKUInfo(options.Tables[0].Rows[0]);

            // Update the product option
            option.SKUName = option.SKUName.ToLower();

            // Update the product option
            SKUInfoProvider.SetSKUInfo(option);

            return true;
        }

        return false;
    }


    /// <summary>
    /// Gets and bulk updates options. Called when the "Get and bulk update options" button is pressed.
    /// Expects the CreateOption method to be run first.
    /// </summary>
    private bool GetAndBulkUpdateOptions()
    {
        // Prepare the parameters
        string where = "SKUName LIKE N'MyNewProduct%' AND SKUOptionCategoryID IS NOT NULL";

        // Get the data
        DataSet options = SKUInfoProvider.GetSKUs(where, null);
        if (!DataHelper.DataSourceIsEmpty(options))
        {
            // Loop through the individual items
            foreach (DataRow optionDr in options.Tables[0].Rows)
            {
                // Create object from DataRow
                SKUInfo modifyOption = new SKUInfo(optionDr);

                // Update the properties
                modifyOption.SKUName = modifyOption.SKUName.ToUpper();

                // Update the product option
                SKUInfoProvider.SetSKUInfo(modifyOption);
            }

            return true;
        }

        return false;
    }


    /// <summary>
    /// Deletes option. Called when the "Delete option" button is pressed.
    /// Expects the CreateOption method to be run first.
    /// </summary>
    private bool DeleteOption()
    {
        // Get the product option
        string where = "SKUName LIKE N'MyNewProduct%' AND SKUOptionCategoryID IS NOT NULL";

        // Get the data
        DataSet products = SKUInfoProvider.GetSKUs(where, null);
        if (!DataHelper.DataSourceIsEmpty(products))
        {
            // Create object from DataRow
            SKUInfo deleteProduct = new SKUInfo(products.Tables[0].Rows[0]);

            // Delete the product option
            SKUInfoProvider.DeleteSKUInfo(deleteProduct);

            return true;
        }

        return false;
    }

    #endregion


    #region "API examples - Option category on product"

    /// <summary>
    /// Adds option category to product. Called when the "Add category to product" button is pressed.
    /// Expects the CreateOptionCategory and CreateProduct methods to be run first.
    /// </summary>
    private bool AddCategoryToProduct()
    {
        // Prepare the parameters
        string where = "SKUName LIKE N'MyNew%' AND SKUOptionCategoryID IS NULL";
        SKUInfo product = null;

        // Get the product
        DataSet products = SKUInfoProvider.GetSKUs(where, null);
        if (!DataHelper.DataSourceIsEmpty(products))
        {
            product = new SKUInfo(products.Tables[0].Rows[0]);
        }

        // Get the option category
        OptionCategoryInfo category = OptionCategoryInfoProvider.GetOptionCategoryInfo("MyNewCategory", CMSContext.CurrentSiteName);

        if ((product != null) && (category != null))
        {
            // Add category to product
            SKUOptionCategoryInfoProvider.AddOptionCategoryToSKU(category.CategoryID, product.SKUID);

            return true;
        }

        return false;
    }


    /// <summary>
    /// Removes option category from product. Called when the "Remove category from product" button is pressed.
    /// Expects the AddCategoryToProduct method to be run first.
    /// </summary>
    private bool RemoveCategoryFromProduct()
    {
        // Prepare the parameters
        string where = "SKUName LIKE N'MyNew%'";
        SKUInfo product = null;

        // Get the product
        DataSet products = SKUInfoProvider.GetSKUs(where, null);
        if (!DataHelper.DataSourceIsEmpty(products))
        {
            product = new SKUInfo(products.Tables[0].Rows[0]);
        }

        // Get the option category
        OptionCategoryInfo category = OptionCategoryInfoProvider.GetOptionCategoryInfo("MyNewCategory", CMSContext.CurrentSiteName);

        if ((product != null) && (category != null))
        {
            // Get the option category
            SKUOptionCategoryInfo optionCategory = SKUOptionCategoryInfoProvider.GetSKUOptionCategoryInfo(category.CategoryID, product.SKUID);

            // Remove option category from product
            SKUOptionCategoryInfoProvider.DeleteSKUOptionCategoryInfo(optionCategory);

            return true;
        }

        return false;
    }

    #endregion


    #region "API examples - Discount coupon"

    /// <summary>
    /// Creates discount coupon. Called when the "Create coupon" button is pressed.
    /// </summary>
    private bool CreateDiscountCoupon()
    {
        // Create new discount coupon object
        DiscountCouponInfo newCoupon = new DiscountCouponInfo();

        // Set the properties
        newCoupon.DiscountCouponDisplayName = "My new coupon";
        newCoupon.DiscountCouponCode = "MyNewCoupon";
        newCoupon.DiscountCouponIsExcluded = true;
        newCoupon.DiscountCouponIsFlatValue = true;
        newCoupon.DiscountCouponValue = 200;
        newCoupon.DiscountCouponValidFrom = DateTime.Now;
        newCoupon.DiscountCouponSiteID = CMSContext.CurrentSiteID;

        // Create the discount coupon
        DiscountCouponInfoProvider.SetDiscountCouponInfo(newCoupon);

        return true;
    }


    /// <summary>
    /// Gets and updates discount coupon. Called when the "Get and update coupon" button is pressed.
    /// Expects the CreateDiscountCoupon method to be run first.
    /// </summary>
    private bool GetAndUpdateDiscountCoupon()
    {
        // Get the discount coupon
        DiscountCouponInfo updateCoupon = DiscountCouponInfoProvider.GetDiscountCouponInfo("MyNewCoupon", CMSContext.CurrentSiteName);
        if (updateCoupon != null)
        {
            // Update the properties
            updateCoupon.DiscountCouponDisplayName = updateCoupon.DiscountCouponDisplayName.ToLower();

            // Update the discount coupon
            DiscountCouponInfoProvider.SetDiscountCouponInfo(updateCoupon);

            return true;
        }

        return false;
    }


    /// <summary>
    /// Gets and bulk updates discount coupons. Called when the "Get and bulk update coupons" button is pressed.
    /// Expects the CreateDiscountCoupon method to be run first.
    /// </summary>
    private bool GetAndBulkUpdateDiscountCoupons()
    {
        // Prepare the parameters
        string where = "DiscountCouponCode LIKE N'MyNewCoupon%'";

        // Get the data
        DataSet coupons = DiscountCouponInfoProvider.GetDiscountCoupons(where, null);
        if (!DataHelper.DataSourceIsEmpty(coupons))
        {
            // Loop through the individual items
            foreach (DataRow couponDr in coupons.Tables[0].Rows)
            {
                // Create object from DataRow
                DiscountCouponInfo modifyCoupon = new DiscountCouponInfo(couponDr);

                // Update the properties
                modifyCoupon.DiscountCouponDisplayName = modifyCoupon.DiscountCouponDisplayName.ToUpper();

                // Update the discount coupon
                DiscountCouponInfoProvider.SetDiscountCouponInfo(modifyCoupon);
            }

            return true;
        }

        return false;
    }


    /// <summary>
    /// Deletes discount coupon. Called when the "Delete coupon" button is pressed.
    /// Expects the CreateDiscountCoupon method to be run first.
    /// </summary>
    private bool DeleteDiscountCoupon()
    {
        // Get the discount coupon
        DiscountCouponInfo deleteCoupon = DiscountCouponInfoProvider.GetDiscountCouponInfo("MyNewCoupon", CMSContext.CurrentSiteName);

        // Delete the discount coupon
        DiscountCouponInfoProvider.DeleteDiscountCouponInfo(deleteCoupon);

        return (deleteCoupon != null);
    }

    #endregion


    #region "API examples - Discount coupon products"

    /// <summary>
    /// Adds product to discount coupon. Called when the "Add product to coupon" button is pressed.
    /// </summary>
    private bool AddProductToCoupon()
    {
        // Prepare the parameters
        string where = "SKUName LIKE N'MyNewProduct%'";
        SKUInfo product = null;

        // Get the product
        DataSet products = SKUInfoProvider.GetSKUs(where, null);
        if (!DataHelper.DataSourceIsEmpty(products))
        {
            product = new SKUInfo(products.Tables[0].Rows[0]);
        }

        // Get the discount coupon
        DiscountCouponInfo discountCoupon = DiscountCouponInfoProvider.GetDiscountCouponInfo("MyNewCoupon", CMSContext.CurrentSiteName);

        if ((discountCoupon != null) && (product != null))
        {
            // Add Product to coupon
            SKUDiscountCouponInfoProvider.AddDiscountCouponToSKU(product.SKUID, discountCoupon.DiscountCouponID);

            return true;
        }

        return false;
    }


    /// <summary>
    /// Removes product from discount coupon. Called when the "Remove product from coupon" button is pressed.
    /// Expects the AddProductToCoupon method to be run first.
    /// </summary>
    private bool RemoveProductFromCoupon()
    {
        // Prepare the parameters
        string where = "SKUName LIKE N'MyNewProduct%'";
        SKUInfo product = null;

        // Get the product
        DataSet products = SKUInfoProvider.GetSKUs(where, null);
        if (!DataHelper.DataSourceIsEmpty(products))
        {
            product = new SKUInfo(products.Tables[0].Rows[0]);
        }

        // Get the discount coupon
        DiscountCouponInfo discountCoupon = DiscountCouponInfoProvider.GetDiscountCouponInfo("MyNewCoupon", CMSContext.CurrentSiteName);

        if ((discountCoupon != null) && (product != null))
        {
            // Get product added to coupon
            SKUDiscountCouponInfo skuDiscount = SKUDiscountCouponInfoProvider.GetSKUDiscountCouponInfo(product.SKUID, discountCoupon.DiscountCouponID);

            // Remove product from coupon
            SKUDiscountCouponInfoProvider.DeleteSKUDiscountCouponInfo(skuDiscount);

            return (skuDiscount != null);
        }

        return false;
    }

    #endregion


    #region "API examples - Discount level"

    /// <summary>
    /// Creates discount level. Called when the "Create level" button is pressed.
    /// </summary>
    private bool CreateDiscountLevel()
    {
        // Create new discount level object
        DiscountLevelInfo newLevel = new DiscountLevelInfo();

        // Set the properties
        newLevel.DiscountLevelDisplayName = "My new discount level";
        newLevel.DiscountLevelName = "MyNewDiscountLevel";
        newLevel.DiscountLevelValue = 30;
        newLevel.DiscountLevelEnabled = true;
        newLevel.DiscountLevelSiteID = CMSContext.CurrentSiteID;

        // Create the discount level
        DiscountLevelInfoProvider.SetDiscountLevelInfo(newLevel);

        return true;
    }


    /// <summary>
    /// Gets and updates discount level. Called when the "Get and update level" button is pressed.
    /// Expects the CreateDiscountLevel method to be run first.
    /// </summary>
    private bool GetAndUpdateDiscountLevel()
    {
        // Get the discount level
        DiscountLevelInfo updateLevel = DiscountLevelInfoProvider.GetDiscountLevelInfo("MyNewDiscountLevel", CMSContext.CurrentSiteName);
        if (updateLevel != null)
        {
            // Update the properties
            updateLevel.DiscountLevelDisplayName = updateLevel.DiscountLevelDisplayName.ToLower();

            // Update the discount level
            DiscountLevelInfoProvider.SetDiscountLevelInfo(updateLevel);

            return true;
        }

        return false;
    }


    /// <summary>
    /// Gets and bulk updates discount levels. Called when the "Get and bulk update levels" button is pressed.
    /// Expects the CreateDiscountLevel method to be run first.
    /// </summary>
    private bool GetAndBulkUpdateDiscountLevels()
    {
        // Prepare the parameters
        string where = "DiscountLevelName LIKE N'MyNewDiscountLevel%'";

        // Get the data
        DataSet levels = DiscountLevelInfoProvider.GetDiscountLevels(where, null);
        if (!DataHelper.DataSourceIsEmpty(levels))
        {
            // Loop through the individual items
            foreach (DataRow levelDr in levels.Tables[0].Rows)
            {
                // Create object from DataRow
                DiscountLevelInfo modifyLevel = new DiscountLevelInfo(levelDr);

                // Update the properties
                modifyLevel.DiscountLevelDisplayName = modifyLevel.DiscountLevelDisplayName.ToUpper();

                // Update the discount level
                DiscountLevelInfoProvider.SetDiscountLevelInfo(modifyLevel);
            }

            return true;
        }

        return false;
    }


    /// <summary>
    /// Deletes discount level. Called when the "Delete level" button is pressed.
    /// Expects the CreateDiscountLevel method to be run first.
    /// </summary>
    private bool DeleteDiscountLevel()
    {
        // Get the discount level
        DiscountLevelInfo deleteLevel = DiscountLevelInfoProvider.GetDiscountLevelInfo("MyNewDiscountLevel", CMSContext.CurrentSiteName);

        // Delete the discount level
        DiscountLevelInfoProvider.DeleteDiscountLevelInfo(deleteLevel);

        return (deleteLevel != null);
    }

    #endregion


    #region "API examples - Discount level department"

    /// <summary>
    /// Adds department do discount level. Called when the "Add department to level" button is pressed.
    /// Expects the CreateDiscountLevel method to be run first.
    /// </summary>
    private bool AddDepartmentToLevel()
    {
        // Get the department
        DepartmentInfo department = DepartmentInfoProvider.GetDepartmentInfo("MyNewDepartment", CMSContext.CurrentSiteName);

        // Get the discount level
        DiscountLevelInfo discountLevel = DiscountLevelInfoProvider.GetDiscountLevelInfo("MyNewDiscountLevel", CMSContext.CurrentSiteName);

        if ((department != null) && (discountLevel != null))
        {
            // Add department to discount level
            DiscountLevelDepartmentInfoProvider.AddDiscountLevelToDepartment(discountLevel.DiscountLevelID, department.DepartmentID);

            return true;
        }

        return false;
    }


    /// <summary>
    /// Removes department from discount level. Called when the "Remove department from level" button is pressed.
    /// Expects the AddDepartmentToLevel method to be run first.
    /// </summary>
    private bool RemoveDepartmentFromLevel()
    {
        // Get the department
        DepartmentInfo department = DepartmentInfoProvider.GetDepartmentInfo("MyNewDepartment", CMSContext.CurrentSiteName);

        // Get the discount level
        DiscountLevelInfo discountLevel = DiscountLevelInfoProvider.GetDiscountLevelInfo("MyNewDiscountLevel", CMSContext.CurrentSiteName);

        if ((department != null) || (discountLevel != null))
        {
            // Get the department added to discount level
            DiscountLevelDepartmentInfo departmentLevel = DiscountLevelDepartmentInfoProvider.GetDiscountLevelDepartmentInfo(discountLevel.DiscountLevelID, department.DepartmentID);

            // Remove department from discount level
            DiscountLevelDepartmentInfoProvider.DeleteDiscountLevelDepartmentInfo(departmentLevel);

            return (departmentLevel != null);
        }

        return false;
    }

    #endregion


    #region "API examples - Customer"

    /// <summary>
    /// Creates anonoymous customer. Called when the "Create anonymous customer" button is pressed.
    /// </summary>
    private bool CreateAnonymousCustomer()
    {
        // Create new customer object
        CustomerInfo newCustomer = new CustomerInfo();

        // Set the properties
        newCustomer.CustomerFirstName = "";
        newCustomer.CustomerLastName = "My new anonymous customer";
        newCustomer.CustomerEmail = "MyEmail@localhost.local";
        newCustomer.CustomerEnabled = true;
        newCustomer.CustomerSiteID = CMSContext.CurrentSiteID;

        // Create the anonymous customer
        CustomerInfoProvider.SetCustomerInfo(newCustomer);

        return true;
    }


    /// <summary>
    /// Creates registered customer. Called when the "Create registered customer" button is pressed.
    /// </summary>
    private bool CreateRegisteredCustomer()
    {
        // Prepare the parameters
        int siteId = CMSContext.CurrentSiteID;

        // Create a new user
        UserInfo newUser = new UserInfo();

        // Set the user properties
        newUser.UserName = "My new user";
        newUser.UserEnabled = true;
        newUser.UserIsEditor = true;

        // Save the user
        UserInfoProvider.SetUserInfo(newUser);

        // Add user to current site
        UserInfoProvider.AddUserToSite(newUser.UserName, CMSContext.CurrentSiteName);

        // Create new customer object
        CustomerInfo newCustomer = new CustomerInfo();

        // Set the properties
        newCustomer.CustomerFirstName = "";
        newCustomer.CustomerLastName = "My new registered customer";
        newCustomer.CustomerEmail = "MyEmail@localhost.local";
        newCustomer.CustomerEnabled = true;
        newCustomer.CustomerSiteID = CMSContext.CurrentSiteID;

        // Assign registered user to customer
        newCustomer.CustomerUserID = newUser.UserID;

        // Create the registered customer
        CustomerInfoProvider.SetCustomerInfo(newCustomer);

        return true;
    }


    /// <summary>
    /// Gets and updates customer. Called when the "Get and update customer" button is pressed.
    /// Expects the CreateCustomer method to be run first.
    /// </summary>
    private bool GetAndUpdateCustomer()
    {
        // Prepare the parameters
        string where = "CustomerLastName LIKE N'My New Registered%'";

        // Get the data
        DataSet customers = CustomerInfoProvider.GetCustomers(where, null);
        if (!DataHelper.DataSourceIsEmpty(customers))
        {
            // Create object from DataRow
            CustomerInfo updateCustomer = new CustomerInfo(customers.Tables[0].Rows[0]);

            // Update the properties
            updateCustomer.CustomerLastName = updateCustomer.CustomerLastName.ToLower();

            // Update the customer
            CustomerInfoProvider.SetCustomerInfo(updateCustomer);

            return true;
        }

        return false;
    }


    /// <summary>
    /// Gets and bulk updates customers. Called when the "Get and bulk update customers" button is pressed.
    /// Expects the CreateCustomer method to be run first.
    /// </summary>
    private bool GetAndBulkUpdateCustomers()
    {
        // Prepare the parameters
        string where = "CustomerLastName LIKE N'My New Registered%'";

        // Get the data
        DataSet customers = CustomerInfoProvider.GetCustomers(where, null);
        if (!DataHelper.DataSourceIsEmpty(customers))
        {
            // Loop through the individual items
            foreach (DataRow customerDr in customers.Tables[0].Rows)
            {
                // Create object from DataRow
                CustomerInfo modifyCustomer = new CustomerInfo(customerDr);

                // Update the properties
                modifyCustomer.CustomerLastName = modifyCustomer.CustomerLastName.ToUpper();

                // Update the customer
                CustomerInfoProvider.SetCustomerInfo(modifyCustomer);
            }

            return true;
        }

        return false;
    }


    /// <summary>
    /// Deletes customer. Called when the "Delete customer" button is pressed.
    /// Expects the CreateCustomer method to be run first.
    /// </summary>
    private bool DeleteCustomer()
    {
        // Prepare the parameters
        string where = "CustomerLastName LIKE N'My New%'";

        // Delete user
        UserInfo user = UserInfoProvider.GetUserInfo("My new user");
        UserInfoProvider.DeleteUser(user);

        // Get the data
        DataSet customers = CustomerInfoProvider.GetCustomers(where, null);
        if (!DataHelper.DataSourceIsEmpty(customers))
        {
            foreach (DataRow customerDr in customers.Tables[0].Rows)
            {
                // Create object from DataRow
                CustomerInfo deleteCustomer = new CustomerInfo(customerDr);

                // Delete the customer
                CustomerInfoProvider.DeleteCustomerInfo(deleteCustomer);
            }

            return true;
        }

        return false;
    }

    #endregion


    #region "API examples - Address"

    /// <summary>
    /// Creates address. Called when the "Create address" button is pressed.
    /// Expects the CreateRegisteredCustomer method to be run first.
    /// </summary>
    private bool CreateAddress()
    {
        // Prepare the parameters
        string where = "CustomerLastName LIKE N'My New Registered%'";
        CustomerInfo customer = null;

        // Get the customer
        DataSet customers = CustomerInfoProvider.GetCustomers(where, null);
        if (!DataHelper.DataSourceIsEmpty(customers))
        {
            // Create object from DataRow
            customer = new CustomerInfo(customers.Tables[0].Rows[0]);
        }

        // Get the country
        CountryInfo country = CountryInfoProvider.GetCountryInfo("USA");

        if ((customer != null) && (country != null))
        {
            // Create new address object
            AddressInfo newAddress = new AddressInfo();

            // Set the properties
            newAddress.AddressName = "My new address";
            newAddress.AddressLine1 = "Address line 1";
            newAddress.AddressLine2 = "Address line 2";
            newAddress.AddressCity = "Address city";
            newAddress.AddressZip = "Address ZIP code";
            newAddress.AddressIsBilling = true;
            newAddress.AddressIsShipping = false;
            newAddress.AddressEnabled = true;
            newAddress.AddressPersonalName = "";
            newAddress.AddressCustomerID = customer.CustomerID;
            newAddress.AddressCountryID = country.CountryID;

            // Create the address
            AddressInfoProvider.SetAddressInfo(newAddress);

            return true;
        }

        return false;
    }


    /// <summary>
    /// Gets and updates address. Called when the "Get and update address" button is pressed.
    /// Expects the CreateAddress method to be run first.
    /// </summary>
    private bool GetAndUpdateAddress()
    {
        // Prepare the parameters
        string where = "AddressName LIKE 'My New%'";

        // Get the address
        DataSet addresses = AddressInfoProvider.GetAddresses(where, null);
        if (!DataHelper.DataSourceIsEmpty(addresses))
        {
            // Create object from DataRow
            AddressInfo updateAddress = new AddressInfo(addresses.Tables[0].Rows[0]);

            // Update the properties
            updateAddress.AddressName = updateAddress.AddressName.ToLower();

            // Update the address
            AddressInfoProvider.SetAddressInfo(updateAddress);

            return true;
        }

        return false;
    }


    /// <summary>
    /// Gets and bulk updates addresses. Called when the "Get and bulk update addresses" button is pressed.
    /// Expects the CreateAddress method to be run first.
    /// </summary>
    private bool GetAndBulkUpdateAddresses()
    {
        // Prepare the parameters
        string where = "AddressName LIKE 'My New%'";

        // Get the data
        DataSet addresses = AddressInfoProvider.GetAddresses(where, null);
        if (!DataHelper.DataSourceIsEmpty(addresses))
        {
            // Loop through the individual items
            foreach (DataRow addressDr in addresses.Tables[0].Rows)
            {
                // Create object from DataRow
                AddressInfo modifyAddress = new AddressInfo(addressDr);

                // Update the properties
                modifyAddress.AddressName = modifyAddress.AddressName.ToUpper();

                // Update the address
                AddressInfoProvider.SetAddressInfo(modifyAddress);
            }

            return true;
        }

        return false;
    }


    /// <summary>
    /// Deletes address. Called when the "Delete address" button is pressed.
    /// Expects the CreateAddress method to be run first.
    /// </summary>
    private bool DeleteAddress()
    {
        // Prepare the parameters
        string where = "AddressName LIKE 'My New%'";

        // Get the address
        DataSet addresses = AddressInfoProvider.GetAddresses(where, null);
        if (!DataHelper.DataSourceIsEmpty(addresses))
        {
            // Create object from DataRow
            AddressInfo updateAddress = new AddressInfo(addresses.Tables[0].Rows[0]);

            // Delete the address
            AddressInfoProvider.DeleteAddressInfo(updateAddress);

            return true;
        }

        return false;
    }

    #endregion


    #region "API examples - Credit event"

    /// <summary>
    /// Creates credit event. Called when the "Create event" button is pressed.
    /// Expects the CreateRegisteredCustomer method to be run first.
    /// </summary>
    private bool CreateCreditEvent()
    {
        // Prepare the parameters
        string where = "CustomerLastName LIKE N'My New Registered%'";
        CustomerInfo customer = null;

        // Get the customer
        DataSet customers = CustomerInfoProvider.GetCustomers(where, null);
        if (!DataHelper.DataSourceIsEmpty(customers))
        {
            // Create object from DataRow
            customer = new CustomerInfo(customers.Tables[0].Rows[0]);
        }

        if (customer != null)
        {
            // Create new credit event object
            CreditEventInfo newEvent = new CreditEventInfo();

            // Set the properties
            newEvent.EventName = "My new event";
            newEvent.EventCreditChange = 500;
            newEvent.EventDate = DateTime.Now;
            newEvent.EventDescription = "Credit event description.";
            newEvent.EventCustomerID = customer.CustomerID;

            // Create the credit event
            CreditEventInfoProvider.SetCreditEventInfo(newEvent);

            return true;
        }

        return false;
    }


    /// <summary>
    /// Gets and updates credit event. Called when the "Get and update event" button is pressed.
    /// Expects the CreateCreditEvent method to be run first.
    /// </summary>
    private bool GetAndUpdateCreditEvent()
    {
        // Prepare the parameters
        string where = "EventName LIKE 'My New%'";

        // Get the credit event
        DataSet credits = CreditEventInfoProvider.GetCreditEvents(where, null);
        if (!DataHelper.DataSourceIsEmpty(credits))
        {
            // Create object from DataRow
            CreditEventInfo updateCredit = new CreditEventInfo(credits.Tables[0].Rows[0]);

            // Update the properties
            updateCredit.EventName = updateCredit.EventName.ToLower();

            // Update the credit event
            CreditEventInfoProvider.SetCreditEventInfo(updateCredit);

            return true;
        }

        return false;
    }


    /// <summary>
    /// Gets and bulk updates credit events. Called when the "Get and bulk update events" button is pressed.
    /// Expects the CreateCreditEvent method to be run first.
    /// </summary>
    private bool GetAndBulkUpdateCreditEvents()
    {
        // Prepare the parameters
        string where = "EventName LIKE 'My New%'";

        // Get the data
        DataSet credits = CreditEventInfoProvider.GetCreditEvents(where, null);
        if (!DataHelper.DataSourceIsEmpty(credits))
        {
            // Loop through the individual items
            foreach (DataRow creditDr in credits.Tables[0].Rows)
            {
                // Create object from DataRow
                CreditEventInfo updateCredit = new CreditEventInfo(creditDr);

                // Update the properties
                updateCredit.EventName = updateCredit.EventName.ToUpper();

                // Update the credit event
                CreditEventInfoProvider.SetCreditEventInfo(updateCredit);
            }

            return true;
        }

        return false;
    }


    /// <summary>
    /// Gets total credit. Called when the "get total credit" button is pressed.
    /// Expects the CreateCreditEvent method to be run first.
    /// </summary>
    private bool GetTotalCredit()
    {
        // Prepare the parameters
        string where = "CustomerLastName LIKE N'My New Registered%'";
        double totalCredit = 0;
        CustomerInfo customer = null;

        // Get the customer
        DataSet customers = CustomerInfoProvider.GetCustomers(where, null);
        if (!DataHelper.DataSourceIsEmpty(customers))
        {
            // Create object from DataRow
            customer = new CustomerInfo(customers.Tables[0].Rows[0]);
        }

        if (customer != null)
        {
            // Get total credit
            totalCredit = CreditEventInfoProvider.GetTotalCredit(customer.CustomerID, CMSContext.CurrentSiteID);

            // Show the total credit
            this.apiGetTotalCredit.InfoMessage = "Total customer credit is: " + totalCredit.ToString();

            return true;
        }

        return false;
    }


    /// <summary>
    /// Deletes credit event. Called when the "Delete event" button is pressed.
    /// Expects the CreateCreditEvent method to be run first.
    /// </summary>
    private bool DeleteCreditEvent()
    {
        // Prepare the parameters
        string where = "EventName LIKE 'My New%'";

        // Get the credit event
        DataSet credits = CreditEventInfoProvider.GetCreditEvents(where, null);
        if (!DataHelper.DataSourceIsEmpty(credits))
        {
            // Create object from DataRow
            CreditEventInfo deleteCredit = new CreditEventInfo(credits.Tables[0].Rows[0]);

            // Delete credit event
            CreditEventInfoProvider.DeleteCreditEventInfo(deleteCredit);

            return (deleteCredit != null);
        }

        return false;
    }

    #endregion


    #region "API Examples - Customer newsletter"

    /// <summary>
    /// Subscribes customer to newsletter. Called when the "Subscribe customer to newsletter" button is pressed.
    /// Expects the CreateRegisteredCustomer method to be run first.
    /// </summary>
    private bool SubscribeCustomerToNewsletter()
    {
        // Prepare the parameters
        string where = "CustomerLastName LIKE N'My New Registered%'";
        CustomerInfo customer = null;

        // Get the customer
        DataSet customers = CustomerInfoProvider.GetCustomers(where, null);
        if (!DataHelper.DataSourceIsEmpty(customers))
        {
            // Create object from DataRow
            customer = new CustomerInfo(customers.Tables[0].Rows[0]);
        }

        // Get the newsletter
        Newsletter customerNewsletter = NewsletterProvider.GetNewsletter("CorporateNewsletter", CMSContext.CurrentSiteID);

        if ((customer != null) && (customerNewsletter != null))
        {
            // Create the new subscription object
            SubscriberNewsletterInfo subscription = new SubscriberNewsletterInfo();

            Subscriber customerExistedSubscriber = SubscriberProvider.GetSubscriber("MyEmail@localhost.local", CMSContext.CurrentSiteID);

            // Check if customer is subscriber
            if (customerExistedSubscriber != null)
            {
                // Subscribe existed customer subscriber to newsletter
                subscription.SubscriberID = customerExistedSubscriber.SubscriberID;
            }
            else
            {
                // Create the new subscriber object
                Subscriber customerSubscriber = new Subscriber();

                // Set the properties
                customerSubscriber.SubscriberEmail = customer.CustomerEmail;
                customerSubscriber.SubscriberLastName = customer.CustomerLastName;
                customerSubscriber.SubscriberSiteID = CMSContext.CurrentSiteID;

                // Create the new subscriber
                SubscriberProvider.SetSubscriber(customerSubscriber);

                // Subscribe new customer subscriber to newsletter
                subscription.SubscriberID = customerSubscriber.SubscriberID;
            }

            subscription.NewsletterID = customerNewsletter.NewsletterID;
            subscription.SubscribedWhen = DateTime.Now;

            // Save the data
            SubscriberNewsletterInfoProvider.SetSubscriberNewsletterInfo(subscription);

            return true;
        }

        return false;
    }


    /// <summary>
    /// Unsubscribes customer from newsletter. Called when the "Unsubscribe customer from newsletter" button is pressed.
    /// Expects the SubscribeCustomerToNewsletter method to be run first.
    /// </summary>
    private bool UnsubscribeCustomerFromNewsletter()
    {
        Subscriber subscriber = SubscriberProvider.GetSubscriber("MyEmail@localhost.local", CMSContext.CurrentSiteID);

        if (subscriber != null)
        {
            DataSet subscriptions = SubscriberNewsletterInfoProvider.GetSubscriberNewsletters(subscriber.SubscriberID, null, -1, null);
            if (!DataHelper.DataSourceIsEmpty(subscriptions))
            {
                // Creates object from DataRow
                SubscriberNewsletterInfo subscription = new SubscriberNewsletterInfo(subscriptions.Tables[0].Rows[0]);

                // Unsubscribe customer from newsletter
                SubscriberNewsletterInfoProvider.DeleteSubscriberNewsletterInfo(subscription);

                return true;
            }
        }

        return false;
    }

    #endregion


    #region "API examples - Customer discount level"

    /// <summary>
    /// Assigns discount level to customer. Called when the "Assign discount level to customer" button is pressed.
    /// Expects the CreateRegisteredCustomer method to be run first.
    /// </summary>
    private bool AssignDiscountLevelToCustomer()
    {
        // Prepare the parameters
        string where = "CustomerLastName LIKE N'My New Registered%'";
        CustomerInfo customer = null;

        // Get the discount level
        DiscountLevelInfo discountLevel = DiscountLevelInfoProvider.GetDiscountLevelInfo("MyNewDiscountLevel", CMSContext.CurrentSiteName);

        // Get the data
        DataSet customers = CustomerInfoProvider.GetCustomers(where, null);
        if (!DataHelper.DataSourceIsEmpty(customers))
        {
            // Create object from DataRow
            customer = new CustomerInfo(customers.Tables[0].Rows[0]);
        }

        if ((customer != null) && (discountLevel != null))
        {
            // Assign discount level to customer
            customer.CustomerDiscountLevelID = discountLevel.DiscountLevelID;

            // Assign discount level to customer
            CustomerInfoProvider.SetCustomerInfo(customer);

            return true;
        }

        return false;
    }


    /// <summary>
    /// Removes discount level from customer. Called when the "Remove discount level from customer" button is pressed.
    /// Expects the AssignDiscountLevelToCustomer method to be run first.
    /// </summary>
    private bool RemoveDiscountLevelFromCustomer()
    {
        // Prepare the parameters
        string where = "CustomerLastName LIKE N'My New Registered%'";

        // Get the data
        DataSet customers = CustomerInfoProvider.GetCustomers(where, null);
        if (!DataHelper.DataSourceIsEmpty(customers))
        {
            // Create object from DataRow
            CustomerInfo customer = new CustomerInfo(customers.Tables[0].Rows[0]);

            // if discount level is assigned to customer
            if (customer.CustomerDiscountLevelID > 0)
            {
                // Remove discount level from customer
                customer.CustomerDiscountLevelID = 0;

                // Save the data
                CustomerInfoProvider.SetCustomerInfo(customer);

                return true;
            }
        }

        return false;
    }

    #endregion


    #region "API examples - Customer wishlist"

    /// <summary>
    /// Adds product to wishlist. Called when the "Assign discount level to customer" button is pressed.
    /// Expects the CreateProduct and CreateRegisteredCustomer methods to be run first.
    /// </summary>
    private bool AddProductToWishlist()
    {
        // Prepare the parameters
        string whereCustomer = "CustomerLastName LIKE N'My New Registered%'";
        string whereProduct = "SKUName LIKE N'MyNewProduct%'";
        CustomerInfo customer = null;
        SKUInfo product = null;

        // Get the product
        DataSet products = SKUInfoProvider.GetSKUs(whereProduct, null);
        if (!DataHelper.DataSourceIsEmpty(products))
        {
            // Create object from DataRow
            product = new SKUInfo(products.Tables[0].Rows[0]);
        }

        // Get the customer
        DataSet customers = CustomerInfoProvider.GetCustomers(whereCustomer, null);
        if (!DataHelper.DataSourceIsEmpty(customers))
        {
            // Create object from DataRow
            customer = new CustomerInfo(customers.Tables[0].Rows[0]);
        }

        if ((customer != null) && (product != null))
        {
            // Add product to wishlist
            WishlistItemInfoProvider.AddSKUToWishlist(customer.CustomerUserID, product.SKUID, CMSContext.CurrentSiteID);

            return true;
        }

        return false;
    }


    /// <summary>
    /// Removes product from wishlist. Called when the "Remove product from wishlist" button is pressed.
    /// Expects the AddProductToWishlist method to be run first.
    /// </summary>
    private bool RemoveProductFromWishlist()
    {
        // Prepare the parameters
        string whereCustomer = "CustomerLastName LIKE N'My New Registered%'";
        string whereProduct = "SKUName LIKE N'MyNewProduct%'";
        CustomerInfo customer = null;
        SKUInfo product = null;

        // Get the product
        DataSet products = SKUInfoProvider.GetSKUs(whereProduct, null);
        if (!DataHelper.DataSourceIsEmpty(products))
        {
            // Create object from DataRow
            product = new SKUInfo(products.Tables[0].Rows[0]);
        }

        // Get the customer
        DataSet customers = CustomerInfoProvider.GetCustomers(whereCustomer, null);
        if (!DataHelper.DataSourceIsEmpty(customers))
        {
            // Create object from DataRow
            customer = new CustomerInfo(customers.Tables[0].Rows[0]);
        }

        if ((customer != null) && (product != null))
        {
            // Get the product from wishlist
            WishlistItemInfo wishlistItem = WishlistItemInfoProvider.GetWishlistItemInfo(customer.CustomerUserID, product.SKUID, CMSContext.CurrentSiteID);

            // Remove product from wishlist
            WishlistItemInfoProvider.DeleteWishlistItemInfo(wishlistItem);

            return (wishlistItem != null);
        }

        return false;
    }

    #endregion


    #region "API examples - Order"

    /// <summary>
    /// Creates order. Called when the "Create order" button is pressed.
    /// Expects the CreateRegisteredCustomer, CreateAddress and CreateOrderStatus methods to be run first.
    /// </summary>
    private bool CreateOrder()
    {
        // Prepare the parameters
        string whereCustomer = "CustomerLastName LIKE N'My New Registered%'";
        string whereAddress = "AddressName LIKE 'My New%'";
        CustomerInfo customer = null;
        AddressInfo address = null;

        // Get the customer
        DataSet customers = CustomerInfoProvider.GetCustomers(whereCustomer, null);
        if (!DataHelper.DataSourceIsEmpty(customers))
        {
            // Create object from DataRow
            customer = new CustomerInfo(customers.Tables[0].Rows[0]);
        }

        // Get the address
        DataSet addresses = AddressInfoProvider.GetAddresses(whereAddress, null);
        if (!DataHelper.DataSourceIsEmpty(addresses))
        {
            // Create object from DataRow
            address = new AddressInfo(addresses.Tables[0].Rows[0]);
        }

        // Get the order status
        OrderStatusInfo orderStatus = OrderStatusInfoProvider.GetOrderStatusInfo("MyNewStatus", CMSContext.CurrentSiteName);

        // Get the currency
        CurrencyInfo currency = CurrencyInfoProvider.GetCurrencyInfo("MyNewCurrency", CMSContext.CurrentSiteName);

        if ((customer != null) && (address != null) && (orderStatus != null) && (currency != null))
        {
            // Create new order object
            OrderInfo newOrder = new OrderInfo();

            // Set the properties
            newOrder.OrderInvoiceNumber = "1";
            newOrder.OrderBillingAddressID = address.AddressID;
            newOrder.OrderShippingAddressID = address.AddressID;
            newOrder.OrderTotalPrice = 200;
            newOrder.OrderTotalTax = 30;
            newOrder.OrderDate = DateTime.Now;
            newOrder.OrderStatusID = orderStatus.StatusID;
            newOrder.OrderCustomerID = customer.CustomerID;
            newOrder.OrderSiteID = CMSContext.CurrentSiteID;
            newOrder.OrderCurrencyID = currency.CurrencyID;

            // Create the order
            OrderInfoProvider.SetOrderInfo(newOrder);

            return true;
        }

        return false;
    }


    /// <summary>
    /// Gets and updates order. Called when the "Get and update order" button is pressed.
    /// Expects the CreateOrder method to be run first.
    /// </summary>
    private bool GetAndUpdateOrder()
    {
        // Prepare the parameters
        string where = "CustomerLastName LIKE N'My New Registered%'";
        CustomerInfo customer = null;

        // Get the customer
        DataSet customers = CustomerInfoProvider.GetCustomers(where, null);
        if (!DataHelper.DataSourceIsEmpty(customers))
        {
            // Create object from DataRow
            customer = new CustomerInfo(customers.Tables[0].Rows[0]);
        }

        if (customer != null)
        {
            string whereOrder = "OrderCustomerID = " + customer.CustomerID;

            // Get the order
            DataSet orders = OrderInfoProvider.GetOrders(whereOrder, null);
            if (!DataHelper.DataSourceIsEmpty(orders))
            {
                // Create object from DataRow
                OrderInfo order = new OrderInfo(orders.Tables[0].Rows[0]);

                // Update the property
                order.OrderTotalPrice = order.OrderTotalPrice + 200;

                // Update the order
                OrderInfoProvider.SetOrderInfo(order);

                return true;
            }
        }

        return false;
    }


    /// <summary>
    /// Gets and bulk updates orders. Called when the "Get and bulk update orders" button is pressed.
    /// Expects the CreateOrder method to be run first.
    /// </summary>
    private bool GetAndBulkUpdateOrders()
    {
        // Prepare the parameters
        string where = "CustomerLastName LIKE N'My New Registered%'";
        CustomerInfo customer = null;

        // Get the customer
        DataSet customers = CustomerInfoProvider.GetCustomers(where, null);
        if (!DataHelper.DataSourceIsEmpty(customers))
        {
            // Create object from DataRow
            customer = new CustomerInfo(customers.Tables[0].Rows[0]);
        }

        if (customer != null)
        {
            string whereOrder = "OrderCustomerID = " + customer.CustomerID;

            // Get the order
            DataSet orders = OrderInfoProvider.GetOrders(whereOrder, null);
            if (!DataHelper.DataSourceIsEmpty(orders))
            {
                foreach (DataRow orderDr in orders.Tables[0].Rows)
                {
                    // Create object from DataRow
                    OrderInfo order = new OrderInfo(orderDr);

                    // Update the property
                    order.OrderTotalPrice = order.OrderTotalPrice + 200;

                    // Update the order
                    OrderInfoProvider.SetOrderInfo(order);
                }

                return true;
            }
        }

        return false;
    }


    /// <summary>
    /// Deletes order. Called when the "Delete order" button is pressed.
    /// Expects the CreateOrder method to be run first.
    /// </summary>
    private bool DeleteOrder()
    {
        // Prepare the parameters
        string where = "CustomerLastName LIKE N'My New Registered%'";
        CustomerInfo customer = null;

        // Get the customer
        DataSet customers = CustomerInfoProvider.GetCustomers(where, null);
        if (!DataHelper.DataSourceIsEmpty(customers))
        {
            // Create object from DataRow
            customer = new CustomerInfo(customers.Tables[0].Rows[0]);
        }

        if (customer != null)
        {
            string whereOrder = "OrderCustomerID='" + customer.CustomerID + "'";

            // Get the order
            DataSet orders = OrderInfoProvider.GetOrders(whereOrder, null);
            if (!DataHelper.DataSourceIsEmpty(orders))
            {
                // Create object from DataRow
                OrderInfo order = new OrderInfo(orders.Tables[0].Rows[0]);

                // Delete the order
                OrderInfoProvider.DeleteOrderInfo(order);

                return true;
            }
        }

        return false;
    }

    #endregion


    #region "API examples - Order item"

    /// <summary>
    /// Creates order item. Called when the "Create item" button is pressed.
    /// </summary>
    private bool CreateOrderItem()
    {
        // Prepare the parameters
        string where = "CustomerLastName LIKE N'My New Registered%'";
        string whereProduct = "SKUName LIKE N'MyNewProduct%'";
        CustomerInfo customer = null;
        OrderInfo order = null;
        SKUInfo product = null;

        // Get the product
        DataSet products = SKUInfoProvider.GetSKUs(whereProduct, null);
        if (!DataHelper.DataSourceIsEmpty(products))
        {
            // Create object from DataRow
            product = new SKUInfo(products.Tables[0].Rows[0]);
        }

        // Get the customer
        DataSet customers = CustomerInfoProvider.GetCustomers(where, null);
        if (!DataHelper.DataSourceIsEmpty(customers))
        {
            // Create object from DataRow
            customer = new CustomerInfo(customers.Tables[0].Rows[0]);
        }

        if (customer != null)
        {
            string whereOrder = "OrderCustomerID = " + customer.CustomerID;

            // Get the order
            DataSet orders = OrderInfoProvider.GetOrders(whereOrder, null);
            if (!DataHelper.DataSourceIsEmpty(orders))
            {
                // Create object from DataRow
                order = new OrderInfo(orders.Tables[0].Rows[0]);
            }

            if ((order != null) && (product != null))
            {
                // Create new order item object
                OrderItemInfo newItem = new OrderItemInfo();

                // Set the properties
                newItem.OrderItemSKUName = "MyNewProduct";
                newItem.OrderItemOrderID = order.OrderID;
                newItem.OrderItemSKUID = product.SKUID;
                newItem.OrderItemUnitPrice = 200;
                newItem.OrderItemUnitCount = 1;

                // Create the order item
                OrderItemInfoProvider.SetOrderItemInfo(newItem);

                return true;
            }
        }

        return false;
    }


    /// <summary>
    /// Gets and updates order item. Called when the "Get and update item" button is pressed.
    /// Expects the CreateOrderItem method to be run first.
    /// </summary>
    private bool GetAndUpdateOrderItem()
    {
        // Prepare the parameters
        string where = "CustomerLastName LIKE N'My New Registered%'";
        CustomerInfo customer = null;
        OrderInfo order = null;

        // Get the customer
        DataSet customers = CustomerInfoProvider.GetCustomers(where, null);
        if (!DataHelper.DataSourceIsEmpty(customers))
        {
            // Create object from DataRow
            customer = new CustomerInfo(customers.Tables[0].Rows[0]);
        }

        if (customer != null)
        {
            string whereOrder = "OrderCustomerID = " + customer.CustomerID;

            // Get the order
            DataSet orders = OrderInfoProvider.GetOrders(whereOrder, null);
            if (!DataHelper.DataSourceIsEmpty(orders))
            {
                // Create object from DataRow
                order = new OrderInfo(orders.Tables[0].Rows[0]);
            }

            if (order != null)
            {
                string whereOrderItem = "OrderItemOrderID = " + order.OrderID;

                // Get the order item
                DataSet orderItems = OrderItemInfoProvider.GetOrderItems(whereOrderItem, null, 0, null);
                if (!DataHelper.DataSourceIsEmpty(orderItems))
                {
                    // Create object from DataRow
                    OrderItemInfo orderItem = new OrderItemInfo(orderItems.Tables[0].Rows[0]);

                    // Update the property
                    orderItem.OrderItemSKUName = orderItem.OrderItemSKUName.ToLower();

                    // Update the order item
                    OrderItemInfoProvider.SetOrderItemInfo(orderItem);

                    return true;
                }
            }
        }

        return false;
    }


    /// <summary>
    /// Gets and bulk updates order items. Called when the "Get and bulk update items" button is pressed.
    /// Expects the CreateOrderItem method to be run first.
    /// </summary>
    private bool GetAndBulkUpdateOrderItems()
    {
        // Prepare the parameters
        string where = "CustomerLastName LIKE N'My New Registered%'";
        CustomerInfo customer = null;
        OrderInfo order = null;

        // Get the customer
        DataSet customers = CustomerInfoProvider.GetCustomers(where, null);
        if (!DataHelper.DataSourceIsEmpty(customers))
        {
            // Create object from DataRow
            customer = new CustomerInfo(customers.Tables[0].Rows[0]);
        }

        if (customer != null)
        {
            string whereOrder = "OrderCustomerID = " + customer.CustomerID;

            // Get the order
            DataSet orders = OrderInfoProvider.GetOrders(whereOrder, null);
            if (!DataHelper.DataSourceIsEmpty(orders))
            {
                // Create object from DataRow
                order = new OrderInfo(orders.Tables[0].Rows[0]);
            }

            if (order != null)
            {
                string whereOrderItem = "OrderItemOrderID = " + order.OrderID;

                // Get the order item
                DataSet orderItems = OrderItemInfoProvider.GetOrderItems(whereOrderItem, null, 0, null);
                if (!DataHelper.DataSourceIsEmpty(orderItems))
                {
                    foreach (DataRow orderItemDr in orderItems.Tables[0].Rows)
                    {
                        // Create object from DataRow
                        OrderItemInfo orderItem = new OrderItemInfo(orderItems.Tables[0].Rows[0]);

                        // Update the property
                        orderItem.OrderItemSKUName = orderItem.OrderItemSKUName.ToUpper();

                        // Update the order item
                        OrderItemInfoProvider.SetOrderItemInfo(orderItem);
                    }

                    return true;
                }
            }
        }

        return false;
    }


    /// <summary>
    /// Deletes order item. Called when the "Delete item" button is pressed.
    /// Expects the CreateOrderItem method to be run first.
    /// </summary>
    private bool DeleteOrderItem()
    {
        // Prepare the parameters
        string where = "CustomerLastName LIKE N'My New Registered%'";
        CustomerInfo customer = null;
        OrderInfo order = null;

        // Get the customer
        DataSet customers = CustomerInfoProvider.GetCustomers(where, null);
        if (!DataHelper.DataSourceIsEmpty(customers))
        {
            // Create object from DataRow
            customer = new CustomerInfo(customers.Tables[0].Rows[0]);
        }

        if (customer != null)
        {
            string whereOrder = "OrderCustomerID = " + customer.CustomerID;

            // Get the order
            DataSet orders = OrderInfoProvider.GetOrders(whereOrder, null);
            if (!DataHelper.DataSourceIsEmpty(orders))
            {
                // Create object from DataRow
                order = new OrderInfo(orders.Tables[0].Rows[0]);
            }

            if (order != null)
            {
                string whereOrderItem = "OrderItemOrderID = " + order.OrderID;

                // Get the order item
                DataSet orderItems = OrderItemInfoProvider.GetOrderItems(whereOrderItem, null, 0, null);
                if (!DataHelper.DataSourceIsEmpty(orderItems))
                {
                    // Create object from DataRow
                    OrderItemInfo orderItem = new OrderItemInfo(orderItems.Tables[0].Rows[0]);

                    // Delete the order item
                    OrderItemInfoProvider.DeleteOrderItemInfo(orderItem);

                    return (orderItem != null);
                }
            }
        }

        return false;
    }

    #endregion


    #region "API Examples - Order status history"

    /// <summary>
    /// Changes order status. Called when the "Change order status" button is pressed.
    /// Expects the CreateOrder method to be run first.
    /// </summary>
    private bool ChangeOrderStatus()
    {
        // Prepare the parameters
        string where = "CustomerLastName LIKE N'My New Registered%'";
        CustomerInfo customer = null;
        OrderInfo order = null;

        // Get the customer
        DataSet customers = CustomerInfoProvider.GetCustomers(where, null);
        if (!DataHelper.DataSourceIsEmpty(customers))
        {
            // Create object from DataRow
            customer = new CustomerInfo(customers.Tables[0].Rows[0]);
        }

        if (customer != null)
        {
            string whereOrder = "OrderCustomerID = " + customer.CustomerID;

            // Get the order
            DataSet orders = OrderInfoProvider.GetOrders(whereOrder, null);
            if (!DataHelper.DataSourceIsEmpty(orders))
            {
                // Create object from DataRow
                order = new OrderInfo(orders.Tables[0].Rows[0]);
            }

            if (order != null)
            {
                // Get next enabled order status
                OrderStatusInfo nextOrderStatus = OrderStatusInfoProvider.GetNextEnabledStatus(order.OrderStatusID);

                if (nextOrderStatus != null)
                {
                    // Create new order status user object
                    OrderStatusUserInfo newUserStatus = new OrderStatusUserInfo();

                    // Set the properties
                    newUserStatus.OrderID = order.OrderID;
                    newUserStatus.ChangedByUserID = CMSContext.CurrentUser.UserID;
                    newUserStatus.FromStatusID = order.OrderStatusID;
                    newUserStatus.ToStatusID = nextOrderStatus.StatusID;
                    newUserStatus.Date = DateTime.Now;

                    // Set the order status user
                    OrderStatusUserInfoProvider.SetOrderStatusUserInfo(newUserStatus);

                    // Set next order status to order
                    order.OrderStatusID = nextOrderStatus.StatusID;

                    // Change the order status
                    OrderInfoProvider.SetOrderInfo(order);

                    return true;
                }
            }
        }

        return false;
    }


    /// <summary>
    /// Deletes order status history. Called when the "Delete history" button is pressed.
    /// Expects the ChangeOrderStatus method to be run first.
    /// </summary>
    private bool DeleteHistory()
    {
        // Prepare the parameters
        string where = "CustomerLastName LIKE N'My New Registered%'";
        CustomerInfo customer = null;
        OrderInfo order = null;

        // Get the customer
        DataSet customers = CustomerInfoProvider.GetCustomers(where, null);
        if (!DataHelper.DataSourceIsEmpty(customers))
        {
            // Create object from DataRow
            customer = new CustomerInfo(customers.Tables[0].Rows[0]);
        }

        if (customer != null)
        {
            string whereOrder = "OrderCustomerID = " + customer.CustomerID;

            // Get the order
            DataSet orders = OrderInfoProvider.GetOrders(whereOrder, null);
            if (!DataHelper.DataSourceIsEmpty(orders))
            {
                // Create object from DataRow
                order = new OrderInfo(orders.Tables[0].Rows[0]);
            }

            if (order != null)
            {
                // Get the order statuses
                DataSet statuses = OrderStatusUserInfoProvider.GetOrderStatusHistory(order.OrderID);
                if (!DataHelper.DataSourceIsEmpty(statuses))
                {
                    foreach (DataRow statusDr in statuses.Tables[0].Rows)
                    {
                        // Create object from DataRow
                        OrderStatusUserInfo status = new OrderStatusUserInfo(statusDr);

                        // Delete the order status
                        OrderStatusUserInfoProvider.DeleteOrderStatusUserInfo(status);
                    }

                    return true;
                }
            }
        }

        return false;
    }

    #endregion
}
