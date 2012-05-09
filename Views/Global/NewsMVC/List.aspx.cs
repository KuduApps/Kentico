using System;
using System.Web.Mvc;
using System.Data;

using CMS.TreeEngine;

/// <summary>
/// Sample view for the news.
/// </summary>
public partial class Views_Global_NewsMVC_List : ViewPage
{
    /// <summary>
    /// Returns the News displayed on current page
    /// </summary>
    public TreeNodeDataSet NewsList
    {
        get
        {
            return (TreeNodeDataSet)ViewData["NewsList"];
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
    }
}
 
