<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="AdoNetDemo._default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <h1><asp:Label ID="TitleLabel" runat="server" Text="Positions:" /></h1>
    <form id="form1" runat="server">
    <div>
      <asp:GridView ID="PositionsGrid" runat="server" AutoGenerateColumns="True" CellPadding="4" ForeColor="#333333" GridLines="None">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
          <asp:HyperLinkField HeaderText="Map Link" Text="map" DataNavigateUrlFields="Latitude,Longitude" DataNavigateUrlFormatString="http://maps.bing.com/?q={0},{1}" Target="_blank" />
        </Columns>
        <EditRowStyle BackColor="#2461BF" />
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#EFF3FB" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#F5F7FB" />
        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
        <SortedDescendingCellStyle BackColor="#E9EBEF" />
        <SortedDescendingHeaderStyle BackColor="#4870BE" />
      </asp:GridView>
    </div>
    </form>
</body>
</html>
