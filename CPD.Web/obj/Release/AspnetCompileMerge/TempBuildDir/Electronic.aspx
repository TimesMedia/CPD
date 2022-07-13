<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Electronic.aspx.cs" Inherits="CPD.Web.Electronic" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<asp:Table ID="Main"  Width="993px" runat="server" Height="67px">
    <asp:TableRow>
        <asp:TableCell style="vertical-align:top;">
  

          <asp:Label ID="Label1" CssClass="subheading" runat="server" Text="Catalogue of available electronic publications" /> 
          <br />

         <asp:GridView ID="GridViewCatalogue" runat="server" AutoGenerateColumns="False" 
                    DataKeyNames="ModuleId"
                    AllowSorting="True" AutoGenerateSelectButton="True" 
                    SelectedRowStyle-BackColor="Red" Width="675px"
                    onselectedindexchanged="GridViewCatalogue_SelectedIndexChanged" >
                        <Columns>
                            <asp:ButtonField  Text="Read" />
                            <asp:ButtonField  Text="Test" />
                            <asp:BoundField DataField="ModuleId" HeaderText="ModuleId" ReadOnly="True" visible="false"
                                SortExpression="ModuleId" />
                            <asp:BoundField DataField="Publication" HeaderText="Publication" 
                                SortExpression="Publication" visible="false"/>
                            <asp:BoundField DataField="Issue" HeaderText="Issue" SortExpression="Issue" visible="false"/>
                            <asp:BoundField DataField="Module" HeaderText="Module" 
                                SortExpression="Module" />
                            <asp:BoundField DataField="NormalPoints" HeaderText="NormalPoints" 
                                SortExpression="NormalPoints" />
                            <asp:BoundField DataField="EthicsPoints" HeaderText="Ethics Points"
                                SortExpression="EthicsPoints"/>
                            <asp:BoundField DataField="PassRate" HeaderText="Pass Rate" 
                                SortExpression="PassRate" />
                            <asp:BoundField DataField="Expiration" HeaderText="Expiration" SortExpression="Expiration" visible="false" />
                        </Columns>
                    <SelectedRowStyle BackColor="Red"></SelectedRowStyle>
            </asp:GridView>

        </asp:TableCell>
    </asp:TableRow>
</asp:Table>


</asp:Content>
