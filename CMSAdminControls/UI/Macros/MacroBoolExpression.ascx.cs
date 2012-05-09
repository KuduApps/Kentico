using System;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.FormControls;
using CMS.CMSHelper;
using CMS.ExtendedControls;

public partial class CMSAdminControls_UI_Macros_MacroBoolExpression : MacroBoolExpression
{
    #region "Properties"

    /// <summary>
    /// Enables or disables the control.
    /// </summary>
    public override bool Enabled
    {
        get
        {
            return this.drpOperator.Enabled;
        }
        set
        {
            this.drpOperator.Enabled = value;
            this.leftOperand.Enabled = value;
            this.rightOperand.Enabled = value;
        }
    }


    /// <summary>
    /// Gets or sets the left part of the expression.
    /// </summary>
    public override string LeftExpression
    {
        get
        {
            return this.leftOperand.Text;
        }
        set
        {
            this.leftOperand.Text = value;
        }
    }


    /// <summary>
    /// Gets or sets the left part of the expression.
    /// </summary>
    public override string RightExpression
    {
        get
        {
            return this.rightOperand.Text;
        }
        set
        {
            this.rightOperand.Text = value;
        }
    }


    /// <summary>
    /// Gets or sets the left part of the expression.
    /// </summary>
    public override string Operator
    {
        get
        {
            return (string.IsNullOrEmpty(this.drpOperator.SelectedValue) ? "==" : this.drpOperator.SelectedValue);
        }
        set
        {
            EnsureOperators();
            this.drpOperator.SelectedValue = value;
            this.pnlRightOperand.Style["display"] = (value == "--" ? "none" : "block");
        }
    }


    #endregion


    #region "Page events"

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        EnsureOperators();

        this.leftOperand.Editor.UseSmallFonts = true;
        this.rightOperand.Editor.UseSmallFonts = true;

        this.leftOperand.TopOffset = 37;
        this.rightOperand.TopOffset = 37;
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        string script = @"
function ShowHideRightOperand(elementId, drpId) {
  var ddl = document.getElementById(drpId);
  var e = document.getElementById(elementId);
  if ((e != null) && (ddl != null)) {
    if (ddl.value == '--') {
      e.style.display = 'none';
    } else {
      e.style.display = 'block';
    }
  }
}";

        this.drpOperator.Attributes.Add("onchange", "ShowHideRightOperand('" + this.pnlRightOperand.ClientID + "', '" + this.drpOperator.ClientID + "');");
        ScriptHelper.RegisterStartupScript(this.Page, typeof(string), "ShowHideRightOperand", script, true);
    }

    #endregion


    /// <summary>
    /// Loads the DDL with operators.
    /// </summary>
    private void EnsureOperators()
    {
        if (this.drpOperator.Items.Count == 0)
        {
            this.drpOperator.Items.Add(new ListItem("==", "=="));
            this.drpOperator.Items.Add(new ListItem("!=", "!="));
            this.drpOperator.Items.Add(new ListItem(">", ">"));
            this.drpOperator.Items.Add(new ListItem("<", "<"));
            this.drpOperator.Items.Add(new ListItem(">=", ">="));
            this.drpOperator.Items.Add(new ListItem("<=", "<="));
            this.drpOperator.Items.Add(new ListItem("(no operation)", "--"));
        }
    }
}