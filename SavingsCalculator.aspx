<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SavingsCalculator.aspx.cs"
  Inherits="SavingsCalculator" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Savings or Loan Calculator</title>
  <style type="text/css">

    .ttl_in
    {

      width: 200px;
      text-align: right;
    }
    .ttl_out
    {
      width: 100px;
    }


  </style>
</head>
<body>
  <form id="form1" runat="server">

  <div>
    <table>
      <tr>
        <td align="center">
          <h1>Savings Calculator</h1>
          <br />
        </td>
      </tr>
      <tr>
        <td>
    <table>
      <tr>
        <td>
          <table style="width: 100%;">
            <tr>
              <td class="ttl_in">
                Initial Balance ($)
              </td>
              <td>
                <asp:TextBox ID="txt_init" runat="server" Text="0"></asp:TextBox>
              </td>
              <td >
              </td>
              <td class="ttl_out">
              </td>
              <td >
              </td>
            </tr>
            <tr>
              <td class="ttl_in">
                Monthly Investment ($)
              </td>
              <td>
                <asp:TextBox ID="txt_inv" runat="server" Text="0"></asp:TextBox>
              </td>
              <td>
                Contribution:
              </td>
              <td class="ttl_out">
                <asp:Label ID="lbl_cont" runat="server"></asp:Label>
              </td>
              <td>
              </td>
            </tr>
            <tr>
              <td class="ttl_in">
                Interest Rate (annual percent)
              </td>
              <td>
                <asp:TextBox ID="txt_prc" runat="server" Text="0"></asp:TextBox>
              </td>
              <td>
                Interest Growth:
              </td>
              <td class="ttl_out">
                <asp:Label ID="lbl_intr" runat="server"></asp:Label>
              </td>
              <td>
                <asp:Button ID="btn_submit" runat="server" Text="Submit" 
                  onclick="btn_submit_Click" />
              </td>
            </tr>
            <tr>
              <td class="ttl_in">
                Investment Period (years)
              </td>
              <td>
                <asp:TextBox ID="txt_per" runat="server" Text="0"></asp:TextBox>
              </td>
              <td>
                Ending Balance:
              </td>
              <td class="ttl_out">
                <asp:Label ID="lbl_tot" runat="server"></asp:Label>
              </td>
              <td>
                <asp:Button ID="btn_clear" runat="server" Text="Clear" Width="100%" 
                  onclick="btn_clear_Click" />
              </td>
            </tr>
          </table>
        </td>
      </tr>
      <tr>
        <td>
          <asp:Table ID="tbl_proj" runat="server" GridLines="Both" Width="100%">
            <asp:TableHeaderRow>
              <asp:TableHeaderCell Text="Year">
          
              </asp:TableHeaderCell>
              <asp:TableHeaderCell Text="Month">
          
              </asp:TableHeaderCell>
              <asp:TableHeaderCell Text="Monthly Investment">
          
              </asp:TableHeaderCell>
              <asp:TableHeaderCell Text="Growth From Interest">
          
              </asp:TableHeaderCell>
              <asp:TableHeaderCell Text="Investment Balance">
          
              </asp:TableHeaderCell>
            </asp:TableHeaderRow>

          </asp:Table>
        </td>
      </tr>
    </table>
        </td>
      </tr>
    </table>
  </div>
  </form>
</body>
</html>
