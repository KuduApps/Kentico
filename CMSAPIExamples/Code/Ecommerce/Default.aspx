<%@ Page Title="" Language="C#" MasterPageFile="~/CMSAPIExamples/Pages/APIExamplesPage.Master"
    Theme="Default" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="CMSAPIExamples_Code_Ecommerce_Default" %>

<%@ Register Src="~/CMSAPIExamples/Controls/APIExample.ascx" TagName="APIExample" TagPrefix="cms" %>
<%@ Register Src="~/CMSAPIExamples/Controls/APIExampleSection.ascx" TagName="APIExampleSection" TagPrefix="cms" %>
<%@ Register Assembly="CMS.UIControls" Namespace="CMS.UIControls" TagPrefix="cms" %>
<asp:Content ID="contentLeft" ContentPlaceHolderID="plcLeftContainer" runat="server">
    <%-- Section: Configuration --%>
    <cms:APIExampleSection ID="secManConfiguration" runat="server" Title="Configuration" />
    <%-- Invoice --%>
    <cms:APIExamplePanel ID="pnlGetAndUpdateInvoice" runat="server" GroupingText="Invoice">
        <cms:APIExample ID="apiGetAndUpdateInvoice" runat="server" ButtonText="Get and update invoice" InfoMessage="Invoice was updated." ErrorMessage="Invoice was not found." />
    </cms:APIExamplePanel>
    <%-- Checkout process step --%>
    <cms:APIExamplePanel ID="pnlCreateCheckoutProcessStep" runat="server" GroupingText="Checkout process step">
        <cms:APIExample ID="apiGenerateDefaultCheckoutProcess" runat="server" ButtonText="Generate default process" InfoMessage="Default process was generated." />
        <cms:APIExample ID="apiCreateCheckoutProcessStep" runat="server" ButtonText="Create step" InfoMessage="Step 'My new step' was created." />
        <cms:APIExample ID="apiGetAndUpdateCheckoutProcessStep" runat="server" ButtonText="Get and update step" APIExampleType="ManageAdditional" InfoMessage="Step 'My new step' was updated." ErrorMessage="Step 'My new step' was not found." />
        <cms:APIExample ID="apiGetAndBulkUpdateCheckoutProcessSteps" runat="server" ButtonText="Get and bulk update steps" APIExampleType="ManageAdditional" InfoMessage="All steps matching the condition were updated." ErrorMessage="Steps matching the condition were not found." />
    </cms:APIExamplePanel>
    <%-- Tax class --%>
    <cms:APIExamplePanel ID="pnlCreateTaxClass" runat="server" GroupingText="Tax class">
        <cms:APIExample ID="apiCreateTaxClass" runat="server" ButtonText="Create class" InfoMessage="Class 'My new class' was created." />
        <cms:APIExample ID="apiGetAndUpdateTaxClass" runat="server" ButtonText="Get and update class" APIExampleType="ManageAdditional" InfoMessage="Class 'My new class' was updated." ErrorMessage="Class 'My new class' was not found." />
        <cms:APIExample ID="apiGetAndBulkUpdateTaxClasses" runat="server" ButtonText="Get and bulk update classes" APIExampleType="ManageAdditional" InfoMessage="All classes matching the condition were updated." ErrorMessage="Classes matching the condition were not found." />
    </cms:APIExamplePanel>
    <%-- Tax class value in country --%>
    <cms:APIExamplePanel ID="pnlSetTaxClassValueInCountry" runat="server" GroupingText="Tax class value in country">
        <cms:APIExample ID="apiSetTaxClassValueInCountry" runat="server" ButtonText="Set value" InfoMessage="Value was set." ErrorMessage="Class 'My new class' or country 'USA' were not found." />
        <cms:APIExample ID="apiGetAndUpdateTaxClassValueInCountry" runat="server" ButtonText="Get and update value" APIExampleType="ManageAdditional" InfoMessage="Value was updated." ErrorMessage="Class 'My new class', country 'USA' or their relationship were not found." />
        <cms:APIExample ID="apiGetAndBulkUpdateTaxClassValuesInCountry" runat="server" ButtonText="Get and bulk update values" APIExampleType="ManageAdditional" InfoMessage="All values matching the condition were updated." ErrorMessage="Values matching the condition were not found." />
    </cms:APIExamplePanel>
    <%-- Tax class value in state --%>
    <cms:APIExamplePanel ID="pnlapiSetTaxClassValueInState" runat="server" GroupingText="Tax class value in state">
        <cms:APIExample ID="apiSetTaxClassValueInState" runat="server" ButtonText="Set value" InfoMessage="Value was set." ErrorMessage="Class 'My new class' or state 'Alabama' were not found." />
        <cms:APIExample ID="apiGetAndUpdateTaxClassValueInState" runat="server" ButtonText="Get and update value" APIExampleType="ManageAdditional" InfoMessage="Value was updated." ErrorMessage="Class 'My new class', state 'Alabama' or their relationship were not found." />
        <cms:APIExample ID="apiGetAndBulkUpdateTaxClassValuesInState" runat="server" ButtonText="Get and bulk update values" APIExampleType="ManageAdditional" InfoMessage="All values matching the condition were updated." ErrorMessage="Values matching the condition were not found." />
    </cms:APIExamplePanel>
    <%-- Currency --%>
    <cms:APIExamplePanel ID="pnlCreateCurrency" runat="server" GroupingText="Currency">
        <cms:APIExample ID="apiCreateCurrency" runat="server" ButtonText="Create currency" InfoMessage="Currency 'My new currency' was created." />
        <cms:APIExample ID="apiGetAndUpdateCurrency" runat="server" ButtonText="Get and update currency" APIExampleType="ManageAdditional" InfoMessage="Currency 'My new currency' was updated." ErrorMessage="Currency 'My new currency' was not found." />
        <cms:APIExample ID="apiGetAndBulkUpdateCurrencies" runat="server" ButtonText="Get and bulk update currencies" APIExampleType="ManageAdditional" InfoMessage="All currencies matching the condition were updated." ErrorMessage="Currencies matching the condition were not found." />
    </cms:APIExamplePanel>
    <%-- Exchange table --%>
    <cms:APIExamplePanel ID="pnlCreateExchangeTable" runat="server" GroupingText="Exchange table">
        <cms:APIExample ID="apiCreateExchangeTable" runat="server" ButtonText="Create table" InfoMessage="Table 'My new table' was created." />
        <cms:APIExample ID="apiGetAndUpdateExchangeTable" runat="server" ButtonText="Get and update table" APIExampleType="ManageAdditional" InfoMessage="Table 'My new table' was updated." ErrorMessage="Table 'My new table' was not found." />
        <cms:APIExample ID="apiGetAndBulkUpdateExchangeTables" runat="server" ButtonText="Get and bulk update tables" APIExampleType="ManageAdditional" InfoMessage="All tables matching the condition were updated." ErrorMessage="Tables matching the condition were not found." />
    </cms:APIExamplePanel>
    <%-- Order status --%>
    <cms:APIExamplePanel ID="pnlCreateOrderStatus" runat="server" GroupingText="Order status">
        <cms:APIExample ID="apiCreateOrderStatus" runat="server" ButtonText="Create status" InfoMessage="Status 'My new status' was created." />
        <cms:APIExample ID="apiGetAndUpdateOrderStatus" runat="server" ButtonText="Get and update status" APIExampleType="ManageAdditional" InfoMessage="Status 'My new status' was updated." ErrorMessage="Status 'My new status' was not found." />
        <cms:APIExample ID="apiGetAndBulkUpdateOrderStatuses" runat="server" ButtonText="Get and bulk update statuses" APIExampleType="ManageAdditional" InfoMessage="All statuses matching the condition were updated." ErrorMessage="Statuses matching the condition were not found." />
    </cms:APIExamplePanel>
    <%-- Public status --%>
    <cms:APIExamplePanel ID="pnlCreatePublicStatus" runat="server" GroupingText="Public status">
        <cms:APIExample ID="apiCreatePublicStatus" runat="server" ButtonText="Create status" InfoMessage="Status 'My new status' was created." />
        <cms:APIExample ID="apiGetAndUpdatePublicStatus" runat="server" ButtonText="Get and update status" APIExampleType="ManageAdditional" InfoMessage="Status 'My new status' was updated." ErrorMessage="Status 'My new status' was not found." />
        <cms:APIExample ID="apiGetAndBulkUpdatePublicStatuses" runat="server" ButtonText="Get and bulk update statuses" APIExampleType="ManageAdditional" InfoMessage="All statuses matching the condition were updated." ErrorMessage="Statuses matching the condition were not found." />
    </cms:APIExamplePanel>
    <%-- Internal status --%>
    <cms:APIExamplePanel ID="pnlCreateInternalStatus" runat="server" GroupingText="Internal status">
        <cms:APIExample ID="apiCreateInternalStatus" runat="server" ButtonText="Create status" InfoMessage="Status 'My new status' was created." />
        <cms:APIExample ID="apiGetAndUpdateInternalStatus" runat="server" ButtonText="Get and update status" APIExampleType="ManageAdditional" InfoMessage="Status 'My new status' was updated." ErrorMessage="Status 'My new status' was not found." />
        <cms:APIExample ID="apiGetAndBulkUpdateInternalStatuses" runat="server" ButtonText="Get and bulk update statuses" APIExampleType="ManageAdditional" InfoMessage="All statuses matching the condition were updated." ErrorMessage="Statuses matching the condition were not found." />
    </cms:APIExamplePanel>
    <%-- Department --%>
    <cms:APIExamplePanel ID="pnlCreateDepartment" runat="server" GroupingText="Department">
        <cms:APIExample ID="apiCreateDepartment" runat="server" ButtonText="Create department" InfoMessage="Department 'My new department' was created." />
        <cms:APIExample ID="apiGetAndUpdateDepartment" runat="server" ButtonText="Get and update department" APIExampleType="ManageAdditional" InfoMessage="Department 'My new department' was updated." ErrorMessage="Department 'My new department' was not found." />
        <cms:APIExample ID="apiGetAndBulkUpdateDepartments" runat="server" ButtonText="Get and bulk update departments" APIExampleType="ManageAdditional" InfoMessage="All departments matching the condition were updated." ErrorMessage="Departments matching the condition were not found." />
    </cms:APIExamplePanel>
    <%-- Department user --%>
    <cms:APIExamplePanel ID="pnlAddUserToDepartment" runat="server" GroupingText="Department user">
        <cms:APIExample ID="apiAddUserToDepartment" runat="server" ButtonText="Add user to department" InfoMessage="Current user was added to department 'My new department'." ErrorMessage="Department 'My new department' was not found." />
    </cms:APIExamplePanel>
    <%-- Department default tax class --%>
    <cms:APIExamplePanel ID="pnlAddTaxClassToDepartment" runat="server" GroupingText="Department default tax class">
        <cms:APIExample ID="apiAddTaxClassToDepartment" runat="server" ButtonText="Add tax class to department" InfoMessage="Default tax class was added to department 'My new department'." ErrorMessage="Department 'My new department' or class 'My new class' were not found." />
    </cms:APIExamplePanel>
    <%-- Shipping option --%>
    <cms:APIExamplePanel ID="pnlCreateShippingOption" runat="server" GroupingText="Shipping option">
        <cms:APIExample ID="apiCreateShippingOption" runat="server" ButtonText="Create option" InfoMessage="Option 'My new option' was created." />
        <cms:APIExample ID="apiGetAndUpdateShippingOption" runat="server" ButtonText="Get and update option" APIExampleType="ManageAdditional" InfoMessage="Option 'My new option' was updated." ErrorMessage="Option 'My new option' was not found." />
        <cms:APIExample ID="apiGetAndBulkUpdateShippingOptions" runat="server" ButtonText="Get and bulk update options" APIExampleType="ManageAdditional" InfoMessage="All options matching the condition were updated." ErrorMessage="Options matching the condition were not found." />
    </cms:APIExamplePanel>
    <%-- Payment method --%>
    <cms:APIExamplePanel ID="pnlCreatePaymentMethod" runat="server" GroupingText="Payment method">
        <cms:APIExample ID="apiCreatePaymentMethod" runat="server" ButtonText="Create method" InfoMessage="Method 'My new method' was created." />
        <cms:APIExample ID="apiGetAndUpdatePaymentMethod" runat="server" ButtonText="Get and update method" APIExampleType="ManageAdditional" InfoMessage="Method 'My new method' was updated." ErrorMessage="Method 'My new method' was not found." />
        <cms:APIExample ID="apiGetAndBulkUpdatePaymentMethods" runat="server" ButtonText="Get and bulk update methods" APIExampleType="ManageAdditional" InfoMessage="All methods matching the condition were updated." ErrorMessage="Methods matching the condition were not found." />
    </cms:APIExamplePanel>
    <%-- Manufacturer --%>
    <cms:APIExamplePanel ID="pnlCreateManufacturer" runat="server" GroupingText="Manufacturer">
        <cms:APIExample ID="apiCreateManufacturer" runat="server" ButtonText="Create manufacturer" InfoMessage="Manufacturer 'My new manufacturer' was created." />
        <cms:APIExample ID="apiGetAndUpdateManufacturer" runat="server" ButtonText="Get and update manufacturer" APIExampleType="ManageAdditional" InfoMessage="Manufacturer 'My new manufacturer' was updated." ErrorMessage="Manufacturer 'My new manufacturer' was not found." />
        <cms:APIExample ID="apiGetAndBulkUpdateManufacturers" runat="server" ButtonText="Get and bulk update manufacturers" APIExampleType="ManageAdditional" InfoMessage="All manufacturers matching the condition were updated." ErrorMessage="Manufacturers matching the condition were not found." />
    </cms:APIExamplePanel>
    <%-- Supplier --%>
    <cms:APIExamplePanel ID="pnlCreateSupplier" runat="server" GroupingText="Supplier">
        <cms:APIExample ID="apiCreateSupplier" runat="server" ButtonText="Create supplier" InfoMessage="Supplier 'My new supplier' was created." />
        <cms:APIExample ID="apiGetAndUpdateSupplier" runat="server" ButtonText="Get and update supplier" APIExampleType="ManageAdditional" InfoMessage="Supplier 'My new supplier' was updated." ErrorMessage="Supplier 'My new supplier' was not found." />
        <cms:APIExample ID="apiGetAndBulkUpdateSuppliers" runat="server" ButtonText="Get and bulk update suppliers" APIExampleType="ManageAdditional" InfoMessage="All suppliers matching the condition were updated." ErrorMessage="Suppliers matching the condition were not found." />
    </cms:APIExamplePanel>
    
    <%-- Section: Products --%>
    <cms:APIExampleSection ID="secManProducts" runat="server" Title="Products" />
    <%-- Product --%>
    <cms:APIExamplePanel ID="pnlCreateProduct" runat="server" GroupingText="Product">
        <cms:APIExample ID="apiCreateProduct" runat="server" ButtonText="Create product" InfoMessage="Product 'My new product' was created." ErrorMessage="Department 'My new department' was not found." />
        <cms:APIExample ID="apiGetAndUpdateProduct" runat="server" ButtonText="Get and update product" APIExampleType="ManageAdditional" InfoMessage="Product 'My new product' was updated." ErrorMessage="Product 'My new product' was not found." />
        <cms:APIExample ID="apiGetAndBulkUpdateProducts" runat="server" ButtonText="Get and bulk update products" APIExampleType="ManageAdditional" InfoMessage="All products matching the condition were updated." ErrorMessage="Products matching the condition were not found." />
    </cms:APIExamplePanel>
    <%-- Product document --%>
    <cms:APIExamplePanel ID="pnlCreateProductDocument" runat="server" GroupingText="Product document">
        <cms:APIExample ID="apiCreateProductDocument" runat="server" ButtonText="Create document" InfoMessage="Document 'My new document' was created." ErrorMessage="Product 'My new product' was not found." />
        <cms:APIExample ID="apiGetAndUpdateProductDocument" runat="server" ButtonText="Get and update document" APIExampleType="ManageAdditional" InfoMessage="Document 'My new document' was updated." ErrorMessage="Document 'My new document' was not found." />
    </cms:APIExamplePanel>
    <%-- Product tax class --%>
    <cms:APIExamplePanel ID="pnlAddTaxClassToProduct" runat="server" GroupingText="Tax class">
        <cms:APIExample ID="apiAddTaxClassToProduct" runat="server" ButtonText="Add tax class to product" InfoMessage="Tax class 'My new tax class' was added to product 'My new product'." ErrorMessage="Tax class 'My new tax class' or product 'My new product' were not found." />
    </cms:APIExamplePanel>
    <%-- Volume discount --%>
    <cms:APIExamplePanel ID="pnlCreateVolumeDiscount" runat="server" GroupingText="Volume discount">
        <cms:APIExample ID="apiCreateVolumeDiscount" runat="server" ButtonText="Create discount" InfoMessage="Discount 'My new discount' was created." ErrorMessage="Product 'My new product' was not found." />
        <cms:APIExample ID="apiGetAndUpdateVolumeDiscount" runat="server" ButtonText="Get and update discount" APIExampleType="ManageAdditional" InfoMessage="Discount 'My new discount' was updated." ErrorMessage="Discount 'My new discount' was not found." />
        <cms:APIExample ID="apiGetAndBulkUpdateVolumeDiscounts" runat="server" ButtonText="Get and bulk update discounts" APIExampleType="ManageAdditional" InfoMessage="All discounts matching the condition were updated." ErrorMessage="Discounts matching the condition were not found." />
    </cms:APIExamplePanel>
    <%-- Product option category --%>
    <cms:APIExamplePanel ID="pnlCreateOptionCategory" runat="server" GroupingText="Product option category">
        <cms:APIExample ID="apiCreateOptionCategory" runat="server" ButtonText="Create category" InfoMessage="Category 'My new category' was created." />
        <cms:APIExample ID="apiGetAndUpdateOptionCategory" runat="server" ButtonText="Get and update category" APIExampleType="ManageAdditional" InfoMessage="Category 'My new category' was updated." ErrorMessage="Category 'My new category' was not found." />
        <cms:APIExample ID="apiGetAndBulkUpdateOptionCategories" runat="server" ButtonText="Get and bulk update categories" APIExampleType="ManageAdditional" InfoMessage="All categories matching the condition were updated." ErrorMessage="Categories matching the condition were not found." />
    </cms:APIExamplePanel>
    <%-- Product option --%>
    <cms:APIExamplePanel ID="pnlCreateOption" runat="server" GroupingText="Product option">
        <cms:APIExample ID="apiCreateOption" runat="server" ButtonText="Create option" InfoMessage="Option 'My new option' was created." ErrorMessage="Department 'My new department' or option category 'My new category' were not found." />
        <cms:APIExample ID="apiGetAndUpdateOption" runat="server" ButtonText="Get and update option" APIExampleType="ManageAdditional" InfoMessage="Option 'My new option' was updated." ErrorMessage="Option 'My new option' was not found." />
        <cms:APIExample ID="apiGetAndBulkUpdateOptions" runat="server" ButtonText="Get and bulk update options" APIExampleType="ManageAdditional" InfoMessage="All options matching the condition were updated." ErrorMessage="Options matching the condition were not found." />
    </cms:APIExamplePanel>
    <%-- Option category on product --%>
    <cms:APIExamplePanel ID="pnlAddCategoryToProduct" runat="server" GroupingText="Option category on product">
        <cms:APIExample ID="apiAddCategoryToProduct" runat="server" ButtonText="Add category to product" InfoMessage="Category 'My new category' was added to product 'My new product'." ErrorMessage="Product 'My new product' or option category 'My new category' were not found." />
    </cms:APIExamplePanel>
    
    <%-- Section: Discounts --%>
    <cms:APIExampleSection ID="secManDiscounts" runat="server" Title="Discounts" />
    <%-- Discount coupon --%>
    <cms:APIExamplePanel ID="pnlCreateDiscountCoupon" runat="server" GroupingText="Discount coupon">
        <cms:APIExample ID="apiCreateDiscountCoupon" runat="server" ButtonText="Create coupon" InfoMessage="Coupon 'My new coupon' was created." />
        <cms:APIExample ID="apiGetAndUpdateDiscountCoupon" runat="server" ButtonText="Get and update coupon" APIExampleType="ManageAdditional" InfoMessage="Coupon 'My new coupon' was updated." ErrorMessage="Coupon 'My new coupon' was not found." />
        <cms:APIExample ID="apiGetAndBulkUpdateDiscountCoupons" runat="server" ButtonText="Get and bulk update coupons" APIExampleType="ManageAdditional" InfoMessage="All coupons matching the condition were updated." ErrorMessage="Coupons matching the condition were not found." />
    </cms:APIExamplePanel>
    <%-- Discount coupon products --%>
    <cms:APIExamplePanel ID="pnlAddProductToCoupon" runat="server" GroupingText="Discount coupon products">
        <cms:APIExample ID="apiAddProductToCoupon" runat="server" ButtonText="Add product to coupon" InfoMessage="Product 'My new product' was added to coupon 'My new coupon'." ErrorMessage="Product 'My new product' or coupon 'My new coupon' were not found." />
    </cms:APIExamplePanel>
    <%-- Discount level --%>
    <cms:APIExamplePanel ID="pnlCreateDiscountLevel" runat="server" GroupingText="Discount level">
        <cms:APIExample ID="apiCreateDiscountLevel" runat="server" ButtonText="Create level" InfoMessage="Level 'My new discount level' was created." />
        <cms:APIExample ID="apiGetAndUpdateDiscountLevel" runat="server" ButtonText="Get and update level" APIExampleType="ManageAdditional" InfoMessage="Level 'My new discount level' was updated." ErrorMessage="Level 'My new discount level' was not found." />
        <cms:APIExample ID="apiGetAndBulkUpdateDiscountLevels" runat="server" ButtonText="Get and bulk update levels" APIExampleType="ManageAdditional" InfoMessage="All levels matching the condition were updated." ErrorMessage="Levels matching the condition were not found." />
    </cms:APIExamplePanel>    
    <%-- Discount level department --%>
    <cms:APIExamplePanel ID="pnlAddDepartmentToLevel" runat="server" GroupingText="Discount level department">
        <cms:APIExample ID="apiAddDepartmentToLevel" runat="server" ButtonText="Add department to level" InfoMessage="Department 'My new department' was added to level 'My discount level'." ErrorMessage="Department 'My new department' or discount level 'My new level' were not found." />
    </cms:APIExamplePanel>
    
    <%-- Section: Customers --%>
    <cms:APIExampleSection ID="secManCustomers" runat="server" Title="Customers" />
    <%-- Customer --%>
    <cms:APIExamplePanel ID="pnlCreateCustomer" runat="server" GroupingText="Customer">
        <cms:APIExample ID="apiCreateAnonymousCustomer" runat="server" ButtonText="Create anonymous customer" InfoMessage="Customer 'My new anonymous customer' was created." />
        <cms:APIExample ID="apiCreateRegisteredCustomer" runat="server" ButtonText="Create registered customer" InfoMessage="Customer 'My new registered customer' was created." />
        <cms:APIExample ID="apiGetAndUpdateCustomer" runat="server" ButtonText="Get and update customer" APIExampleType="ManageAdditional" InfoMessage="Customer 'My new registered customer' was updated." ErrorMessage="Customer 'My new registered customer' was not found." />
        <cms:APIExample ID="apiGetAndBulkUpdateCustomers" runat="server" ButtonText="Get and bulk update customers" APIExampleType="ManageAdditional" InfoMessage="All customers matching the condition were updated." ErrorMessage="Customers matching the condition were not found." />
    </cms:APIExamplePanel>
    <%-- Customer address --%>
    <cms:APIExamplePanel ID="pnlCreateAddress" runat="server" GroupingText="Address">
        <cms:APIExample ID="apiCreateAddress" runat="server" ButtonText="Create address" InfoMessage="Address 'My new address' was created." ErrorMessage="Customer 'My new registered customer' or country 'USA' were not found." />
        <cms:APIExample ID="apiGetAndUpdateAddress" runat="server" ButtonText="Get and update address" APIExampleType="ManageAdditional" InfoMessage="Address 'My new address' was updated." ErrorMessage="Address 'My new address' was not found." />
        <cms:APIExample ID="apiGetAndBulkUpdateAddresses" runat="server" ButtonText="Get and bulk update addresses" APIExampleType="ManageAdditional" InfoMessage="All addresses matching the condition were updated." ErrorMessage="Addresses matching the condition were not found." />
    </cms:APIExamplePanel>
    <%-- Customer credit event --%>
    <cms:APIExamplePanel ID="pnlCreateCreditEvent" runat="server" GroupingText="Credit event">
        <cms:APIExample ID="apiCreateCreditEvent" runat="server" ButtonText="Create event" InfoMessage="Event 'My new event' was created." ErrorMessage="Customer 'My new registered customer' was not found." />
        <cms:APIExample ID="apiGetAndUpdateCreditEvent" runat="server" ButtonText="Get and update event" APIExampleType="ManageAdditional" InfoMessage="Event 'My new event' was updated." ErrorMessage="Event 'My new event' was not found." />
        <cms:APIExample ID="apiGetAndBulkUpdateCreditEvents" runat="server" ButtonText="Get and bulk update events" APIExampleType="ManageAdditional" InfoMessage="All events matching the condition were updated." ErrorMessage="Events matching the condition were not found." />
        <cms:APIExample ID="apiGetTotalCredit" runat="server" ButtonText="Get total credit" APIExampleType="ManageAdditional" InfoMessage="Total credit of 'My new event'." ErrorMessage="Event 'My new event' was not found." />
    </cms:APIExamplePanel>
    <%-- Customer newsletter --%>
    <cms:APIExamplePanel ID="pnlSubscribeCustomerToNewsletter" runat="server" GroupingText="Customer newsletter">
        <cms:APIExample ID="apiSubscribeCustomerToNewsletter" runat="server" ButtonText="Subscribe customer to newsletter" InfoMessage="Customer 'My registered customer' was subscribed to newsletter 'Corporate newsletter'." ErrorMessage="Customer 'My new registered customer' or newsletter 'Corporate newsletter' were not found." />
    </cms:APIExamplePanel>
    <%-- Customer discount level --%>
    <cms:APIExamplePanel ID="pnlAssignDiscountLevelToCustomer" runat="server" GroupingText="Customer discount level">
        <cms:APIExample ID="apiAssignDiscountLevelToCustomer" runat="server" ButtonText="Assign discount level to customer" InfoMessage="Level 'My new discount level' was assigned to customer 'My new registered customer'." ErrorMessage="Customer 'My new registered customer' or discount level 'My new discount level' were not found." />
    </cms:APIExamplePanel>
    <%-- Customer wishlist --%>
    <cms:APIExamplePanel ID="pnlAddProductToWishlist" runat="server" GroupingText="Customer wishlist">
        <cms:APIExample ID="apiAddProductToWishlist" runat="server" ButtonText="Add product to wishlist" InfoMessage="Product 'My new product' was added to wishlist." ErrorMessage="Product 'My new product' or customer 'My new registered customer' were not found." />
    </cms:APIExamplePanel>
    
    <%-- Section: Orders --%>
    <cms:APIExampleSection ID="secManOrders" runat="server" Title="Orders" />
    <%-- Order --%>
    <cms:APIExamplePanel ID="pnlCreateOrder" runat="server" GroupingText="Order">
        <cms:APIExample ID="apiCreateOrder" runat="server" ButtonText="Create order" InfoMessage="Order 'My new order' was created." ErrorMessage="Customer 'My new registered customer', address 'My new address', currency 'My new currency' or order status 'My new order status' were not found." />
        <cms:APIExample ID="apiGetAndUpdateOrder" runat="server" ButtonText="Get and update order" APIExampleType="ManageAdditional" InfoMessage="Order 'My new order' was updated." ErrorMessage="Order 'My new order' was not found." />
        <cms:APIExample ID="apiGetAndBulkUpdateOrders" runat="server" ButtonText="Get and bulk update orders" APIExampleType="ManageAdditional" InfoMessage="All orders matching the condition were updated." ErrorMessage="Orders matching the condition were not found." />
    </cms:APIExamplePanel>
    <%-- Order item --%>
    <cms:APIExamplePanel ID="pnlCreateOrderItem" runat="server" GroupingText="Order item">
        <cms:APIExample ID="apiCreateOrderItem" runat="server" ButtonText="Create item" InfoMessage="Order item 'My new item' was created." ErrorMessage="Order 'My new order' was not found." />
        <cms:APIExample ID="apiGetAndUpdateOrderItem" runat="server" ButtonText="Get and update item" APIExampleType="ManageAdditional" InfoMessage="Order item 'My new item' was updated." ErrorMessage="Item 'My new item' was not found." />
        <cms:APIExample ID="apiGetAndBulkUpdateOrderItems" runat="server" ButtonText="Get and bulk update items" APIExampleType="ManageAdditional" InfoMessage="All order items matching the condition were updated." ErrorMessage="Items matching the condition were not found." />
    </cms:APIExamplePanel>
    <%-- Order status history --%>
    <cms:APIExamplePanel ID="pnlChangeOrderStatus" runat="server" GroupingText="Order status history">
        <cms:APIExample ID="apiChangeOrderStatus" runat="server" ButtonText="Change order status" APIExampleType="ManageAdditional" InfoMessage="Status 'My new status' was changed." ErrorMessage="Order 'My new order' or status 'My new status' or target status were not found." />
    </cms:APIExamplePanel>
</asp:Content>
<asp:Content ID="contentRight" ContentPlaceHolderID="plcRightContainer" runat="server">
    <%-- Section: Orders --%>
    <cms:APIExampleSection ID="secCleanOrders" runat="server" Title="Orders" />
    
    <%-- Order status history --%>
    <cms:APIExamplePanel ID="pnlDeleteHistory" runat="server" GroupingText="Order status history" >
        <cms:APIExample ID="apiDeleteHistory" runat="server" ButtonText="Delete history" APIExampleType="CleanUpMain" InfoMessage="Order status history for order 'My new order' was deleted." ErrorMessage="Order 'My new order' was not found." />
    </cms:APIExamplePanel>
    <%-- Order item --%>
    <cms:APIExamplePanel ID="pnlDeleteOrderItem" runat="server" GroupingText="Order item">
        <cms:APIExample ID="apiDeleteOrderItem" runat="server" ButtonText="Delete item" APIExampleType="CleanUpMain" InfoMessage="Order item 'My new item' and all its dependencies were deleted." ErrorMessage="Order item 'My new item' was not found." />
    </cms:APIExamplePanel>
    <%-- Order --%>
    <cms:APIExamplePanel ID="pnlDeleteOrder" runat="server" GroupingText="Order">
        <cms:APIExample ID="apiDeleteOrder" runat="server" ButtonText="Delete order" APIExampleType="CleanUpMain" InfoMessage="Order 'My new order' and all its dependencies were deleted." ErrorMessage="Order 'My new order' was not found." />
    </cms:APIExamplePanel>
    
    <%-- Section: Customers --%>
    <cms:APIExampleSection ID="secCleanCustomers" runat="server" Title="Customers" />
    <%-- Customer discount level --%>
    <cms:APIExamplePanel ID="pnlRemoveProductFromWishlist" runat="server" GroupingText="Customer wishlist">
        <cms:APIExample ID="apiRemoveProductFromWishlist" runat="server" ButtonText="Remove product from wishlist" APIExampleType="CleanUpMain" InfoMessage="Product 'My new product' was removed from wishlist." ErrorMessage="Product 'My new product', customer 'My new registered customer' were not found or wishlist is empty." />
    </cms:APIExamplePanel>
    <%-- Customer discount level --%>
    <cms:APIExamplePanel ID="pnlRemoveDiscountLevelFromCustomer" runat="server" GroupingText="Customer discount level">
        <cms:APIExample ID="apiRemoveDiscountLevelFromCustomer" runat="server" ButtonText="Remove level from customer" APIExampleType="CleanUpMain" InfoMessage="Level 'My new level' was removed from customer 'My new registered customer'." ErrorMessage="Level 'My new level', customer 'My new registered customer' or their relationship were not found." />
    </cms:APIExamplePanel>
    <%-- Customer newsletter --%>
    <cms:APIExamplePanel ID="pnlUnsubscribeCustomerFromNewsletter" runat="server" GroupingText="Customer newsletter">
        <cms:APIExample ID="apiUnsubscribeCustomerFromNewsletter" runat="server" ButtonText="Unsubscribe customer from newsletter" APIExampleType="CleanUpMain" InfoMessage="Customer 'My new registered customer' was unsubscribed from newsletter 'Corporate newsletter'." ErrorMessage="Customer 'My new registered customer', newsletter 'Corporate newsletter' or their relationship were not found." />
    </cms:APIExamplePanel>
    <%-- Customer credit event --%>
    <cms:APIExamplePanel ID="pnlDeleteCreditEvent" runat="server" GroupingText="Credit event">
        <cms:APIExample ID="apiDeleteCreditEvent" runat="server" ButtonText="Delete event" APIExampleType="CleanUpMain" InfoMessage="Event 'My new event' and all its dependencies were deleted." ErrorMessage="Event 'My new event' was not found." />
    </cms:APIExamplePanel>
    <%-- Customer address --%>
    <cms:APIExamplePanel ID="pnlDeleteAddress" runat="server" GroupingText="Address">
        <cms:APIExample ID="apiDeleteAddress" runat="server" ButtonText="Delete address" APIExampleType="CleanUpMain" InfoMessage="Address 'My new address' and all its dependencies were deleted." ErrorMessage="Address 'My new address' was not found." />
    </cms:APIExamplePanel>
    <%-- Customer --%>
    <cms:APIExamplePanel ID="pnlDeleteCustomer" runat="server" GroupingText="Customer">
        <cms:APIExample ID="apiDeleteCustomer" runat="server" ButtonText="Delete customer" APIExampleType="CleanUpMain" InfoMessage="Customers 'My new registered customer', 'My new anonymous customer' and all its dependencies were deleted." ErrorMessage="Customer 'My new registered customer' was not found." />
    </cms:APIExamplePanel>
    
    <%-- Section: Discounts --%>
    <cms:APIExampleSection ID="secCleanDiscounts" runat="server" Title="Discounts" />
    <%-- Discount level department --%>
    <cms:APIExamplePanel ID="pnlRemoveDepartmentFromLevel" runat="server" GroupingText="Discount level department">
        <cms:APIExample ID="apiRemoveDepartmentFromLevel" runat="server" ButtonText="Remove department from level" APIExampleType="CleanUpMain" InfoMessage="Department 'My new department' was removed from level 'My discount level'." ErrorMessage="Department 'My new department', level 'My new level' or their relationship were not found." />
    </cms:APIExamplePanel>
     <%-- Discount level --%>
    <cms:APIExamplePanel ID="pnlDeleteDiscountLevel" runat="server" GroupingText="Discount level">
        <cms:APIExample ID="apiDeleteDiscountLevel" runat="server" ButtonText="Delete level" APIExampleType="CleanUpMain" InfoMessage="Level 'My new level' and all its dependencies were deleted." ErrorMessage="Level 'My new level' was not found." />
    </cms:APIExamplePanel>
    <%-- Discount coupon products --%>
    <cms:APIExamplePanel ID="pnlRemoveProductFromCoupon" runat="server" GroupingText="Discount coupon products">
        <cms:APIExample ID="apiRemoveProductFromCoupon" runat="server" ButtonText="Remove product from coupon" APIExampleType="CleanUpMain" InfoMessage="Product 'My new product' was removed from the coupon 'My new coupon'." ErrorMessage="Product 'My new product', coupon 'My new coupon' or their relationship were not found." />
    </cms:APIExamplePanel>
    <%-- Discount coupon --%>
    <cms:APIExamplePanel ID="pnlDeleteDiscountCoupon" runat="server" GroupingText="Discount coupon">
        <cms:APIExample ID="apiDeleteDiscountCoupon" runat="server" ButtonText="Delete coupon" APIExampleType="CleanUpMain" InfoMessage="Coupon 'My new coupon' and all its dependencies were deleted." ErrorMessage="Coupon 'My new coupon' was not found." />
    </cms:APIExamplePanel>
    
    <%-- Section: Products --%>
    <cms:APIExampleSection ID="secCleanProducts" runat="server" Title="Products" />
    <%-- Option category on product --%>
    <cms:APIExamplePanel ID="pnlRemoveCategoryFromProduct" runat="server" GroupingText="Option category on product">
        <cms:APIExample ID="apiRemoveCategoryFromProduct" runat="server" ButtonText="Remove category from product" APIExampleType="CleanUpMain" InfoMessage="Category 'My new category' was removed from product 'My new product'." ErrorMessage="Category 'My new category', product 'My new product' or their relationship were not found." />
    </cms:APIExamplePanel>
    <%-- Product option --%>
    <cms:APIExamplePanel ID="pnlDeleteOption" runat="server" GroupingText="Product option">
        <cms:APIExample ID="apiDeleteOption" runat="server" ButtonText="Delete option" APIExampleType="CleanUpMain" InfoMessage="Option 'My new option' and all its dependencies were deleted." ErrorMessage="Option 'My new option' was not found." />
    </cms:APIExamplePanel>
    <%-- Product option category --%>
    <cms:APIExamplePanel ID="pnlDeleteOptionCategory" runat="server" GroupingText="Product option category">
        <cms:APIExample ID="apiDeleteOptionCategory" runat="server" ButtonText="Delete category" APIExampleType="CleanUpMain" InfoMessage="Category 'My new category' and all its dependencies were deleted." ErrorMessage="Category 'My new category' was not found." />
    </cms:APIExamplePanel>
    <%-- Volume discount --%>
    <cms:APIExamplePanel ID="pnlDeleteVolumeDiscount" runat="server" GroupingText="Volume discount">
        <cms:APIExample ID="apiDeleteVolumeDiscount" runat="server" ButtonText="Delete discount" APIExampleType="CleanUpMain" InfoMessage="Discount 'My new discount' and all its dependencies were deleted." ErrorMessage="Discount 'My new discount' was not found." />
    </cms:APIExamplePanel>
    <%-- Product tax class --%>
    <cms:APIExamplePanel ID="pnlRemoveTaxClassFromProduct" runat="server" GroupingText="Tax class">
        <cms:APIExample ID="apiRemoveTaxClassFromProduct" runat="server" ButtonText="Remove tax class from product" APIExampleType="CleanUpMain" InfoMessage="Class 'My new tax class' was removed from product 'My new product'." ErrorMessage="Class 'My new tax class', product 'My new product' or their relationship were not found." />
    </cms:APIExamplePanel>
    <%-- Product document --%>
    <cms:APIExamplePanel ID="pnlDeleteProductDocument" runat="server" GroupingText="Product document">
        <cms:APIExample ID="apiDeleteProductDocument" runat="server" ButtonText="Delete document" APIExampleType="CleanUpMain" InfoMessage="Document 'My new document' and all its dependencies were deleted." ErrorMessage="Document 'My new document' was not found." />
    </cms:APIExamplePanel>
    <%-- Product --%>
    <cms:APIExamplePanel ID="pnlDeleteProduct" runat="server" GroupingText="Product">
        <cms:APIExample ID="apiDeleteProduct" runat="server" ButtonText="Delete product" APIExampleType="CleanUpMain" InfoMessage="Product 'My new product' and all its dependencies were deleted." ErrorMessage="Product 'My new product' was not found." />
    </cms:APIExamplePanel>
    
    <%-- Section: Configuration --%>
    <cms:APIExampleSection ID="secCleanConfiguration" runat="server" Title="Configuration" />
    <%-- Supplier --%>
    <cms:APIExamplePanel ID="pnlDeleteSupplier" runat="server" GroupingText="Supplier">
        <cms:APIExample ID="apiDeleteSupplier" runat="server" ButtonText="Delete supplier" APIExampleType="CleanUpMain" InfoMessage="Supplier 'My new supplier' and all its dependencies were deleted." ErrorMessage="Supplier 'My new supplier' was not found." />
    </cms:APIExamplePanel>
    <%-- Manufacturer --%>
    <cms:APIExamplePanel ID="pnlDeleteManufacturer" runat="server" GroupingText="Manufacturer">
        <cms:APIExample ID="apiDeleteManufacturer" runat="server" ButtonText="Delete manufacturer" APIExampleType="CleanUpMain" InfoMessage="Manufacturer 'My new manufacturer' and all its dependencies were deleted." ErrorMessage="Manufacturer 'My new manufacturer' was not found." />
    </cms:APIExamplePanel>
    <%-- Payment method --%>
    <cms:APIExamplePanel ID="pnlDeletePaymentMethod" runat="server" GroupingText="Payment method">
        <cms:APIExample ID="apiDeletePaymentMethod" runat="server" ButtonText="Delete method" APIExampleType="CleanUpMain" InfoMessage="Method 'My new method' and all its dependencies were deleted." ErrorMessage="Method 'My new method' was not found." />
    </cms:APIExamplePanel>
    <%-- Shipping option --%>
    <cms:APIExamplePanel ID="pnlDeleteShippingOption" runat="server" GroupingText="Shipping option">
        <cms:APIExample ID="apiDeleteShippingOption" runat="server" ButtonText="Delete option" APIExampleType="CleanUpMain" InfoMessage="Option 'My new option' and all its dependencies were deleted." ErrorMessage="Option 'My new option' was not found." />
    </cms:APIExamplePanel>
    <%-- Department default tax class --%>
    <cms:APIExamplePanel ID="pnlRemoveTaxClassFromDepartment" runat="server" GroupingText="Department default tax class">
        <cms:APIExample ID="apiRemoveTaxClassFromDepartment" runat="server" ButtonText="Remove tax class from department" APIExampleType="CleanUpMain" InfoMessage=" Class 'My new class' was removed from department 'My new department'." ErrorMessage="Class 'My new class', department 'My new department' or their relationship were not found." />
    </cms:APIExamplePanel>
    <%-- Department user --%>
    <cms:APIExamplePanel ID="pnlRemoveUserFromDepartment" runat="server" GroupingText="Department user">
        <cms:APIExample ID="apiRemoveUserFromDepartment" runat="server" ButtonText="Remove user from department" APIExampleType="CleanUpMain" InfoMessage="Current user was removed from department 'My new department'" ErrorMessage="Department 'My new department' or its relationship to the current user were not found." />
    </cms:APIExamplePanel>
    <%-- Department --%>
    <cms:APIExamplePanel ID="pnlDeleteDepartment" runat="server" GroupingText="Department">
        <cms:APIExample ID="apiDeleteDepartment" runat="server" ButtonText="Delete department" APIExampleType="CleanUpMain" InfoMessage="Department 'My new department' and all its dependencies were deleted." ErrorMessage="Department 'My new department' was not found." />
    </cms:APIExamplePanel>
    <%-- Internal status --%>
    <cms:APIExamplePanel ID="pnlDeleteInternalStatus" runat="server" GroupingText="Internal status">
        <cms:APIExample ID="apiDeleteInternalStatus" runat="server" ButtonText="Delete status" APIExampleType="CleanUpMain" InfoMessage="Status 'My new status' and all its dependencies were deleted." ErrorMessage="Status 'My new status' was not found." />
    </cms:APIExamplePanel>
    <%-- Public status --%>
    <cms:APIExamplePanel ID="pnlDeletePublicStatus" runat="server" GroupingText="Public status">
        <cms:APIExample ID="apiDeletePublicStatus" runat="server" ButtonText="Delete status" APIExampleType="CleanUpMain" InfoMessage="Status 'My new status' and all its dependencies were deleted." ErrorMessage="Status 'My new status' was not found." />
    </cms:APIExamplePanel>
    <%-- Order status --%>
    <cms:APIExamplePanel ID="pnlDeleteOrderStatus" runat="server" GroupingText="Order status">
        <cms:APIExample ID="apiDeleteOrderStatus" runat="server" ButtonText="Delete status" APIExampleType="CleanUpMain" InfoMessage="Status 'My new status' and all its dependencies were deleted." ErrorMessage="Status 'My new status' was not found." />
    </cms:APIExamplePanel>
    <%-- Exchange table --%>
    <cms:APIExamplePanel ID="pnlDeleteExchangeTable" runat="server" GroupingText="Exchange table">
        <cms:APIExample ID="apiDeleteExchangeTable" runat="server" ButtonText="Delete table" APIExampleType="CleanUpMain" InfoMessage="Table 'My new table' and all its dependencies were deleted." ErrorMessage="Table 'My new table' was not found." />
    </cms:APIExamplePanel>
    <%-- Currency --%>
    <cms:APIExamplePanel ID="pnlDeleteCurrency" runat="server" GroupingText="Currency">
        <cms:APIExample ID="apiDeleteCurrency" runat="server" ButtonText="Delete currency" APIExampleType="CleanUpMain" InfoMessage="Currency 'My new currency' and all its dependencies were deleted." ErrorMessage="Currency 'My new currency' was not found." />
    </cms:APIExamplePanel>
    <%-- Tax class value in state --%>
    <cms:APIExamplePanel ID="pnlDeleteTaxClassValueInState" runat="server" GroupingText="Tax class value in state">
        <cms:APIExample ID="apiDeleteTaxClassValueInState" runat="server" ButtonText="Delete value" APIExampleType="CleanUpMain" InfoMessage="Value was deleted." ErrorMessage="Class 'My new class', state 'Alabama' or their relationship were not found." />
    </cms:APIExamplePanel>
    <%-- Tax class value in country --%>
    <cms:APIExamplePanel ID="pnlDeleteTaxClassValueInCountry" runat="server" GroupingText="Tax class value in country">
        <cms:APIExample ID="apiDeleteTaxClassValueInCountry" runat="server" ButtonText="Delete value" APIExampleType="CleanUpMain" InfoMessage="Value was deleted." ErrorMessage="Class 'My new class', country 'USA' or their relationship were not found." />
    </cms:APIExamplePanel>
    <%-- Tax class --%>
    <cms:APIExamplePanel ID="pnlDeleteTaxClass" runat="server" GroupingText="Tax class">
        <cms:APIExample ID="apiDeleteTaxClass" runat="server" ButtonText="Delete class" APIExampleType="CleanUpMain" InfoMessage="Class 'My new class' and all its dependencies were deleted." ErrorMessage="Class 'My new class' was not found." />
    </cms:APIExamplePanel>
    <%-- Checkout process step --%>
    <cms:APIExamplePanel ID="pnlDeleteCheckoutProcessStep" runat="server" GroupingText="Checkout process step">
        <cms:APIExample ID="apiDeleteCheckoutProcessStep" runat="server" ButtonText="Delete step" APIExampleType="CleanUpMain" InfoMessage="Step 'My new step' and all its dependencies were deleted." ErrorMessage="Step 'My new step' was not found." />
    </cms:APIExamplePanel>
</asp:Content>