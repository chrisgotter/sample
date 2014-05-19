

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class SavingsCalculator : System.Web.UI.Page
{
  double monthlyInvestment;
  int investmentPeriod;
  double percentInterest;

  private TextBox[] inputs = new TextBox[4];

  bool busted = false;
  //http://stackoverflow.com/questions/307737/asp-net-problem-with-event-handlers-for-dynamically-created-controls
  EventHandler txt_changed_listener;
  protected void txt_TextChanged(object sender, EventArgs args)
  {

    monthlyInvestment = double.Parse(txt_inv.Text);
    double balance = double.Parse(txt_init.Text);
    
    
    for(int i = 1; i < tbl_proj.Rows.Count; i++)
    {
      try
      {
        balance = populateRow(tbl_proj.Rows[i], balance, i);
        ((TextBox)tbl_proj.Rows[i].Cells[2].Controls[0]).Enabled = true;
      }
      catch (FormatException e)
      {

        lockdown(i);
        return;
      }
    }
    totals();
  }

  private void lockdown(int badVal)
  {
    foreach (TextBox input in inputs)
    {
      input.Enabled = false;
    }
    for (int j = 1; j < tbl_proj.Rows.Count; j++)
    {
      ((TextBox) tbl_proj.Rows[j].Cells[2].Controls[0]).Enabled = false;
    }
    TextBox tb_err;

    tb_err = ((TextBox)tbl_proj.Rows[badVal].Cells[2].Controls[0]);
    tbl_proj.Rows[badVal].BackColor = System.Drawing.Color.Red;

    tb_err.Enabled = true;
    tb_err.Focus();
    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
        "<script>alert('" + tb_err.Text +
        " is an invalid input; inputs must be in numeric form');</script>");
  }
  protected void Page_Load(object sender, EventArgs args)
  {
    inputs[0] = txt_init;
    inputs[1] = txt_inv;
    inputs[2] = txt_prc;
    inputs[3] = txt_per;

    try
    {
      monthlyInvestment = double.Parse(txt_inv.Text);
      percentInterest = double.Parse(txt_prc.Text)/12;
      investmentPeriod = int.Parse(txt_per.Text);    
      
      txt_init.Text = double.Parse(txt_init.Text).ToString("C").Substring(1);    
      txt_inv.Text = double.Parse(txt_inv.Text).ToString("C").Substring(1);
      txt_per.Text = int.Parse(txt_per.Text).ToString();
      txt_prc.Text = double.Parse(txt_prc.Text).ToString("C").Substring(1);

      txt_changed_listener = new EventHandler(txt_TextChanged);
    

      populateTable();
      busted = false;
    }
    catch (FormatException e)
    {
      
      busted = true;
    }
  }

  private void totals()
  {
    double[] totals = {double.Parse(txt_init.Text),0};
    
    for (int i = 1; i < tbl_proj.Rows.Count; i++)
    {
      TextBox tb = (TextBox)tbl_proj.Rows[i].Cells[2].Controls[0];
      tb.Text = double.Parse(tb.Text).ToString("C").Substring(1);

      totals[0] += double.Parse(tb.Text);
      totals[1] += double.Parse(tbl_proj.Rows[i].Cells[3].ToolTip);
      
    }
    //lbl_cont.Text = txt_init.Text;
    lbl_cont.Text = totals[0].ToString("C");
    lbl_intr.Text = totals[1].ToString("C");
    lbl_tot.Text = tbl_proj.Rows.Count > 1 ? double.Parse(tbl_proj.Rows[tbl_proj.Rows.Count-1].Cells[4].ToolTip).ToString("C") : "$0.00";
  }

  protected void btn_submit_Click(object sender, EventArgs e)
  {
    btn_clear_Click(null, null);
    if (!busted)
    {
      populateTable();
      totals();
    }
    else
    {
      ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
        "<script>alert('Invalid input; inputs must be in numeric form');</script>");
    }
  }

  private void populateTable()
  {
    foreach (TextBox input in inputs)
    {
      input.Enabled = true;
    }
    double balance = double.Parse(txt_init.Text);
    DateTime period = DateTime.Now;
    bool good_input = true;
    for (int i = 0; i < investmentPeriod * 12; i++)
    {
      TableRow tr = new TableRow();

      for (int j = 0; j < 5; j++)
      {
        tr.Cells.Add(new TableCell());
      }

      TextBox contribution = new TextBox();

      contribution.ID = "row_" + i;
      contribution.Text = txt_inv.Text;
      contribution.AutoCompleteType = AutoCompleteType.None;
      contribution.AutoPostBack = true;
      contribution.TextChanged += txt_changed_listener;
      contribution.Enabled = true;

      tr.Cells[0].Text = period.Year.ToString();
      tr.Cells[1].Text = period.Month.ToString();
      tr.Cells[2].Controls.Add(contribution);
      tbl_proj.Rows.Add(tr);

      tr.Cells[2].Enabled = true;
      if (good_input)
      {
        try
        {
          balance = populateRow(tr, balance, i+1);
        }
        catch (FormatException e)
        {
          good_input = false;
        }
      }
      period = period.AddMonths(1);
    }
  }


  private double populateRow(TableRow row, double balance, int rowNum)
  {
    
    row.Cells[3].ToolTip = ((percentInterest / 100) * balance).ToString();
    row.Cells[3].Text = double.Parse(row.Cells[3].ToolTip).ToString("C");
    balance = double.Parse(row.Cells[3].ToolTip) + balance + double.Parse(((TextBox)row.Cells[2].Controls[0]).Text);

    row.Cells[4].Text = (balance).ToString("C");
    row.Cells[4].ToolTip = (balance).ToString();


    row.BackColor = rowNum%2==1?System.Drawing.Color.White:System.Drawing.Color.LightGray;

    return balance;
  }

  protected void btn_clear_Click(object sender, EventArgs args)
  {

    while (tbl_proj.Rows.Count > 1)
    {
      tbl_proj.Rows.RemoveAt(1);
    }

  }

}
