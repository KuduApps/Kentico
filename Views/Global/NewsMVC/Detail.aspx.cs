using System;
using System.Web.Mvc;
using System.Data;

using CMS.TreeEngine;

 /// <summary>
 /// Sample view for the news.
 /// </summary>
public partial class Views_Global_NewsMVC_Detail : ViewPage
{
    /// <summary>
    /// Returns the displayed document
    /// </summary>
    public TreeNode Document
    {
        get
        {
            return (TreeNode)ViewData["Document"];
        }
    }
}
 
