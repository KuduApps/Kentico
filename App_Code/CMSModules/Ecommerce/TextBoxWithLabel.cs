using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.ComponentModel;

using CMS.ExtendedControls;
using CMS.GlobalHelper;

namespace CMS.Ecommerce
{
    /// <summary>
    /// TextBoxWithLabel, inherited from System.Web.UI.WebControls.TextBox.
    /// </summary>
    [ToolboxItem(false)]
    public class TextBoxWithLabel : CMSTextBox
    {
        private string mLabelCssClass = string.Empty;
        private bool mLabelFirst = false;


        /// <summary>
        /// CSS class for label.
        /// </summary>
        public string LabelCssClass
        {
            get
            {
                return mLabelCssClass;
            }
            set
            {
                mLabelCssClass = value ?? string.Empty;
            }
        }


        /// <summary>
        /// Label text.
        /// </summary>
        public string LabelText
        {
            get
            {
                return ValidationHelper.GetString(ViewState["LabelText"], string.Empty);
            }
            set
            {
                ViewState["LabelText"] = value ?? string.Empty;
            }
        }


        /// <summary>
        /// Indicates if label has to be placed before text box.
        /// </summary>
        public bool LabelFirst
        {
            get
            {
                return mLabelFirst;
            }
            set
            {
                mLabelFirst = value;
            }
        }


        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            // Render base text box when label goes to the end
            if (!LabelFirst)
            {
                base.Render(writer);
            }

            string attrs = "";
            
            // Append css class attribute
            if(!string.IsNullOrEmpty(LabelCssClass))
            {
                attrs += string.Format("class=\"{0}\"", LabelCssClass);
            }

            // Render label
            writer.Write(string.Format("<span {1}>{0}</span>", LabelText, attrs));

            // Render base text box ad the end
            if (LabelFirst)
            {
                base.Render(writer);
            }
        }
    }
}
