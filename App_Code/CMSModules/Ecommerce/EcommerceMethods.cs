using System;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.Caching;
using System.Collections.Generic;

using CMS.Ecommerce;
using CMS.GlobalHelper;

/// <summary>
/// Ecommerce methods - wrapping methods for macro resolver.
/// </summary>
public static class EcommerceMethods
{
    /// <summary>
    /// Registers all ecommerce methods to macro resolver.
    /// </summary>
    public static void RegisterMethods()
    {
        MacroMethods.RegisterMethod("GetAddToShoppingCartLink", GetAddToShoppingCartLink, typeof(string), "Returns link to \"add to shoppingcart\".", GetMethodFormat("GetAddToShoppingCartLink"), 1, new object[,] { { "productId", typeof(object), "Product ID." }, { "enabled", typeof(object), "Indicates whether product is enabled or not." } }, null, new List<Type>() { typeof(TransformationNamespace) });
        MacroMethods.RegisterMethod("GetAddToWishListLink", GetAddToWishListLink, typeof(string), "Returns link to add specified product to the user's wishlist.", GetMethodFormat("GetAddToWishListLink"), 1, new object[,] { { "productId", typeof(object), "Product ID." } }, null, new List<Type>() { typeof(TransformationNamespace) });
        MacroMethods.RegisterMethod("GetRemoveFromWishListLink", GetRemoveFromWishListLink, typeof(string), "Returns link to remove specified product from the user's wishlist.", GetMethodFormat("GetRemoveFromWishListLink"), 1, new object[,] { { "productId", typeof(object), "Product ID." } }, null, new List<Type>() { typeof(TransformationNamespace) });
        MacroMethods.RegisterMethod("GetPublicStatusName", GetPublicStatusName, typeof(string), "Returns the public SKU status display name.", GetMethodFormat("GetPublicStatusName"), 1, new object[,] { { "statusId", typeof(object), "Status ID" } }, null, new List<Type>() { typeof(TransformationNamespace) });
        MacroMethods.RegisterMethod("ShoppingCartURL", ShoppingCartURL, typeof(string), "Returns URL to the shopping cart.", GetMethodFormat("ShoppingCartURL"), 1, new object[,] { { "siteName", typeof(string), "Site name." } }, null, new List<Type>() { typeof(TransformationNamespace) });
        MacroMethods.RegisterMethod("WishlistURL", WishlistURL, typeof(string), "Returns URL to the wish list.", GetMethodFormat("WishlistURL"), 1, new object[,] { { "siteName", typeof(string), "Site name" } }, null, new List<Type>() { typeof(TransformationNamespace) });
        MacroMethods.RegisterMethod("GetProductUrl", GetProductUrl, typeof(string), "Returns product URL.", GetMethodFormat("GetProductUrl"), 1, new object[,] { { "skuId", typeof(object), "SKU ID." } }, null, new List<Type>() { typeof(TransformationNamespace) });
        MacroMethods.RegisterMethod("GetProductUrlByGUID", GetProductUrlByGUID, typeof(string), "Returns user friendly URL of the specified SKU.", GetMethodFormat("GetProductUrlByGUID"), 2, new object[,] { { "skuGuid", typeof(object), "SKU GUID." }, { "skuName", typeof(object), "SKU name." }, { "siteName", typeof(object), "Site name." } }, null, new List<Type>() { typeof(TransformationNamespace) });
        MacroMethods.RegisterMethod("GetManufacturer", GetManufacturer, typeof(object), "Gets object from the specified column of the manufacturer with specific ID.", GetMethodFormat("GetManufacturer"), 2, new object[,] { { "Id", typeof(object), "Manufacturer ID." }, { "column", typeof(string), "Column name." } }, null, new List<Type>() { typeof(TransformationNamespace) });
        MacroMethods.RegisterMethod("GetDepartment", GetDepartment, typeof(object), "Gets object from the specified column of the department with specific ID.", GetMethodFormat("GetDepartment"), 2, new object[,] { { "Id", typeof(object), "Department ID." }, { "column", typeof(string), "Column name." } }, null, new List<Type>() { typeof(TransformationNamespace) });
        MacroMethods.RegisterMethod("GetSupplier", GetSupplier, typeof(object), "Gets object from the specified column of the supplier with specific ID.", GetMethodFormat("GetSupplier"), 2, new object[,] { { "Id", typeof(object), "Supplier ID." }, { "column", typeof(string), "Column name." } }, null, new List<Type>() { typeof(TransformationNamespace) });
        MacroMethods.RegisterMethod("GetInternalStatus", GetInternalStatus, typeof(object), "Gets object from the specified column of the internal status with specific ID.", GetMethodFormat("GetInternalStatus"), 2, new object[,] { { "Id", typeof(object), "Internal status ID." }, { "column", typeof(string), "Column name." } }, null, new List<Type>() { typeof(TransformationNamespace) });
        MacroMethods.RegisterMethod("GetPublicStatus", GetPublicStatus, typeof(object), "Gets object from the specified column of the public status with specific ID.", GetMethodFormat("GetPublicStatus"), 2, new object[,] { { "Id", typeof(object), "Public status ID." }, { "column", typeof(string), "Column name." } }, null, new List<Type>() { typeof(TransformationNamespace) });
        MacroMethods.RegisterMethod("GetProductImage", GetProductImage, typeof(string), "Returns complete HTML code of the specified resized product image, if not such image exists, default image is returned.", GetMethodFormat("GetProductImage"), 2, new object[,] { { "imageUrl", typeof(object), "Product image url." }, { "alt", typeof(object), "Image alternate text." }, { "maxSideSize", typeof(object), "Max side size of the image." }, { "width", typeof(object), "Width of the image." }, { "height", typeof(object), "Height of the image." } }, null, new List<Type>() { typeof(TransformationNamespace) });
    }


    /// <summary>
    /// Returns the method format for registration into the macro methods hashtable.
    /// </summary>
    /// <param name="method">Method name</param>
    private static string GetMethodFormat(string method)
    {
        return "{name} applied to {args}";
    }


    /// <summary>
    /// Returns link to "add to shoppingcart".
    /// </summary>
    /// <param name="parameters">Product ID; Indicates whether product is enabled or not</param>
    public static object GetAddToShoppingCartLink(params object[] parameters)
    {
        switch (parameters.Length)
        {
            case 1:
                return EcommerceFunctions.GetAddToShoppingCartLink(parameters[0]);

            case 2:
                return EcommerceFunctions.GetAddToShoppingCartLink(parameters[0], parameters[1]);

            default:
                throw new NotSupportedException();
        }
    }


    /// <summary>
    /// Returns link to add specified product to the user's wishlist.
    /// </summary>
    /// <param name="parameters">Product ID</param>
    public static object GetAddToWishListLink(params object[] parameters)
    {
        switch (parameters.Length)
        {
            case 1:
                return EcommerceFunctions.GetAddToWishListLink(parameters[0]);

            default:
                throw new NotSupportedException();
        }
    }


    /// <summary>
    /// Returns link to remove specified product from the user's wishlist.
    /// </summary>
    /// <param name="parameters">Product ID</param>
    public static object GetRemoveFromWishListLink(params object[] parameters)
    {
        switch (parameters.Length)
        {
            case 1:
                return EcommerceFunctions.GetRemoveFromWishListLink(parameters[0]);

            default:
                throw new NotSupportedException();
        }
    }


    /// <summary>
    /// Returns the public SKU status display name.
    /// </summary>
    /// <param name="parameters">Status ID</param>
    public static object GetPublicStatusName(params object[] parameters)
    {
        switch (parameters.Length)
        {
            case 1:
                return EcommerceFunctions.GetPublicStatusName(parameters[0]);

            default:
                throw new NotSupportedException();
        }
    }


    /// <summary>
    /// Returns URL to the shopping cart.
    /// </summary>
    /// <param name="parameters">Site name</param>
    public static object ShoppingCartURL(params object[] parameters)
    {
        switch (parameters.Length)
        {
            case 1:
                return EcommerceFunctions.ShoppingCartURL(ValidationHelper.GetString(parameters[0], ""));

            default:
                throw new NotSupportedException();
        }
    }


    /// <summary>
    /// Returns URL to the wish list.
    /// </summary>
    /// <param name="parameters">Site name</param>
    public static object WishlistURL(params object[] parameters)
    {
        switch (parameters.Length)
        {
            case 1:
                return EcommerceFunctions.WishlistURL(ValidationHelper.GetString(parameters[0], ""));

            default:
                throw new NotSupportedException();
        }
    }


    /// <summary>
    /// Returns user friendly URL of the specified SKU and site name.
    /// </summary>
    /// <param name="parameters">SKU ID</param>
    public static object GetProductUrl(params object[] parameters)
    {
        switch (parameters.Length)
        {
            case 1:
                return EcommerceFunctions.GetProductUrl(parameters[0]);

            default:
                throw new NotSupportedException();
        }
    }


    /// <summary>
    /// Returns user friendly URL of the specified SKU and site name.
    /// </summary>
    /// <param name="parameters">(SKU Guid; SKU Name) OR (SKU Guid; SKU Name; Site Name)</param>
    public static object GetProductUrlByGUID(params object[] parameters)
    {
        switch (parameters.Length)
        {
            case 2:
                return EcommerceFunctions.GetProductUrl(parameters[0], parameters[1]);

            case 3:
                return EcommerceFunctions.GetProductUrl(parameters[0], parameters[1], parameters[2]);

            default:
                throw new NotSupportedException();
        }
    }


    /// <summary>
    /// Gets object from the specified column of the manufacturer with specific ID.
    /// </summary>
    /// <param name="parameters">Manufacturer ID; Column name</param>
    public static object GetManufacturer(params object[] parameters)
    {
        switch (parameters.Length)
        {
            case 2:
                return EcommerceFunctions.GetManufacturer(parameters[0], ValidationHelper.GetString(parameters[1], ""));

            default:
                throw new NotSupportedException();
        }
    }


    /// <summary>
    /// Gets object from the specified column of the department with specific ID.
    /// </summary>
    /// <param name="parameters">Department ID; Column name</param>
    public static object GetDepartment(params object[] parameters)
    {
        switch (parameters.Length)
        {
            case 2:
                return EcommerceFunctions.GetDepartment(parameters[0], ValidationHelper.GetString(parameters[1], ""));

            default:
                throw new NotSupportedException();
        }
    }


    /// <summary>
    /// Gets object from the specified column of the supplier with specific ID.
    /// </summary>
    /// <param name="parameters">Supplier ID; Column name</param>
    public static object GetSupplier(params object[] parameters)
    {
        switch (parameters.Length)
        {
            case 2:
                return EcommerceFunctions.GetSupplier(parameters[0], ValidationHelper.GetString(parameters[1], ""));

            default:
                throw new NotSupportedException();
        }
    }


    /// <summary>
    /// Gets object from the specified column of the internal status with specific ID.
    /// </summary>
    /// <param name="parameters">Internal status ID; Column name</param>
    public static object GetInternalStatus(params object[] parameters)
    {
        switch (parameters.Length)
        {
            case 2:
                return EcommerceFunctions.GetInternalStatus(parameters[0], ValidationHelper.GetString(parameters[1], ""));

            default:
                throw new NotSupportedException();
        }
    }


    /// <summary>
    /// Gets object from the specified column of the public status with specific ID.
    /// </summary>
    /// <param name="parameters">Public status ID; Column name</param>
    public static object GetPublicStatus(params object[] parameters)
    {
        switch (parameters.Length)
        {
            case 2:
                return EcommerceFunctions.GetPublicStatus(parameters[0], ValidationHelper.GetString(parameters[1], ""));

            default:
                throw new NotSupportedException();
        }
    }


    /// <summary>
    /// Gets document name of specified nodeid.
    /// </summary>
    /// <param name="parameters">Document node ID</param>
    public static string GetDocumentName(params object[] parameters)
    {
        switch (parameters.Length)
        {
            case 1:
                return EcommerceFunctions.GetDocumentName(parameters[0]);

            default:
                throw new NotSupportedException();
        }
    }


    /// <summary>
    /// Returns complet HTML code of the specified resized product image, if not such image exists, default image is returned.
    /// </summary>
    /// <param name="parameters">
    /// (Product image url; Image alternate text) OR 
    /// (Product image url; Image alternate text; Max side size) OR 
    /// (Product image url; Image alternate text; Max side size; Width of image; Height of image)</param>
    public static object GetProductImage(params object[] parameters)
    {
        switch (parameters.Length)
        {
            case 2:
                return EcommerceFunctions.GetProductImage(parameters[0], parameters[1]);

            case 3:
                return EcommerceFunctions.GetProductImage(parameters[0], parameters[1], parameters[2]);

            case 5:
                if (ValidationHelper.GetInteger(parameters[2], 0) > 0)
                {
                    return EcommerceFunctions.GetProductImage(parameters[0], parameters[2], parameters[1]);
                }
                else
                {
                    return EcommerceFunctions.GetProductImage(parameters[0], parameters[2], parameters[3], parameters[1]);
                }

            default:
                throw new NotSupportedException();
        }
    }
}
